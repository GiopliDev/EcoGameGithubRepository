using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
#nullable enable
public static class JSONParser
{
    public static T FromAsObject<T>(string jsonString)where T : new()
    {
        object obj = (JSONParser_DecomposeJSON.From(jsonString)) ?? throw new Exception();
        return JSONParser_ObjectToParam.ParseObjectTo<T>(obj);
    }
    public static T[] FromAsArray<T>(string jsonString) where T : new()
    {
        object obj = (JSONParser_DecomposeJSON.From(jsonString)) ?? throw new Exception();
        return (T[])JSONParser_ObjectToParam.ParseArrayTo<T>((object?[])obj);
    }
    public static object? From(string jsonString) => JSONParser_DecomposeJSON.From(jsonString);

    public static string? To(object obj) => JSONParser_SerializeJSON.Serialize(obj);
    public static string? To<T>(T obj) where T : new() => JSONParser_SerializeJSON.Serialize(JSONParser_ParamToObject.ParseToDictionary(obj));
    public static string? To<T>(T[] arr) where T : new() => JSONParser_SerializeJSON.Serialize(JSONParser_ParamToObject.ParseToArray(arr));
}
internal static class JSONParser_StringExtensions
{
    public static int IndexOfExternalToStrings(string value, char indexChar, int start = 0)
    {
        bool inString = false;
        for (int i = start; i < value.Length; i++)
        {
            if (value[i] == '"' && (i == 0 || value[i - 1] != '\\')) inString = !inString;
            if (value[i] == indexChar && !inString) return i;
        }
        return -1;
    }
    public static int LastIndexOfExternalToStrings(string value, char indexChar)
    {
        bool inString = false;
        for (int i = value.Length - 1; i >= 0; i--)
        {
            if (value[i] == '"' && (i == 0 || value[i - 1] != '\\')) inString = !inString;
            if (value[i] == indexChar && !inString) return i;
        }
        return -1;
    }
    public static string[] SplitExternalToStrings(string value, char splitChar)
    {
        List<string> split = new();
        bool inString = value[0] == '"';
        int lastIndex = 0;
        if (value[0] == splitChar)
        {
            split.Add("");
            lastIndex++;
        }
        for (int i = 1; i < value.Length; i++)
        {
            if (value[i] == '"' && (i == 0 || value[i - 1] != '\\')) inString = !inString;
            if (value[i] == splitChar && !inString)
            {
                split.Add(value.Substring(lastIndex, i - lastIndex));
                lastIndex = i + 1;
            }
        }
        split.Add(value.Substring(lastIndex));
        return split.ToArray();
    }
    public static int InnerObjects(string value)
    {
        bool inString = false;
        int count = 0;
        for (int i = 0; i < value.Length; i++)
        {
            if (value[i] == '"' && (i == 0 || value[i - 1] != '\\')) inString = !inString;
            if (!inString && (value[i] == '[' || value[i] == '{')) count++;
        }
        return count;
    }
}
internal static class JSONParser_DecomposeJSON
{
    private static readonly List<object?[]> arraysID = new();
    private static readonly List<Dictionary<string, object?>> objectsID = new();
    private static ObjectStart getObjectStart(string json)
    {
        int lastObject = JSONParser_StringExtensions.LastIndexOfExternalToStrings(json, '{');
        int lastArray = JSONParser_StringExtensions.LastIndexOfExternalToStrings(json, '[');
        return (lastObject > lastArray)
            ? new()
            {
                start = lastObject,
                end = JSONParser_StringExtensions.IndexOfExternalToStrings(json, '}', lastObject),
                isObject = true
            }
            : new()
            {
                start = lastArray,
                end = JSONParser_StringExtensions.IndexOfExternalToStrings(json, ']', lastArray),
                isObject = false
            };
    }
    public static object? From(string jsonString)
    {
        objectsID.Clear();
        arraysID.Clear();
        while (JSONParser_StringExtensions.InnerObjects(jsonString) > 1)
        {
            ObjectStart objStart = getObjectStart(jsonString);
            string value = jsonString.Substring(objStart.start, objStart.end - objStart.start + 1);
            if (objStart.isObject)
            {
                objectsID.Add(
                    DecomposeObject(value.Substring(1, value.Length - 2).Trim())
                );
                jsonString = jsonString.Replace(value, $"${objectsID.Count - 1}");
            }
            else
            {
                arraysID.Add(
                    DecomposeArray(value.Substring(1, value.Length - 2).Trim())
                );
                jsonString = jsonString.Replace(value, $"@{arraysID.Count - 1}");
            }
        }
        bool isObj = jsonString.StartsWith( "{" );
        jsonString = jsonString.Trim().Substring(1, jsonString.Length - 2).Trim();
        return isObj ? DecomposeObject(jsonString) : DecomposeArray(jsonString) ;
    }
    private static Dictionary<string, object?> DecomposeObject(string data)
    {
        Dictionary<string, object?> jsonObject = new();
        foreach (string pair in JSONParser_StringExtensions.SplitExternalToStrings(data, ','))
        {
            string[] keyValue = JSONParser_StringExtensions.SplitExternalToStrings(pair, ':');
            string key = keyValue[0].Trim().Trim('"');
            string value = keyValue[1].Trim();
            jsonObject.Add(key, GetValueFromType(value));
        }
        return jsonObject;
    }
    private static object?[] DecomposeArray(string data)
    {
        string[] singleDatas = JSONParser_StringExtensions.SplitExternalToStrings(data, ',');
        object?[] elements = new object?[singleDatas.Length];
        for (int i = 0; i < singleDatas.Length; i++)
        {
            string value = singleDatas[i].Trim();
            elements[i] = GetValueFromType(value);
        }
        return elements;
    }
    private static object? GetValueFromType(string value)
    {
        value = value.Trim();
        if (value == "null") return null;
        if (value == "true" || value == "false") return value == "true";
        if (value.StartsWith('"') && value.EndsWith('"')) return value;
        if (value.StartsWith('$')) return objectsID[int.Parse(value.Substring(1))];
        if (value.StartsWith('@')) return arraysID[int.Parse(value.Substring(1))];
        if (long.TryParse(value, out long longVal)) return longVal;
        if (double.TryParse(value, out double doubleVal)) return doubleVal;
        throw new NotImplementedException($"Valore {value} senza tipo apparente");
    }
    private struct ObjectStart
    {
        public int start { get; set; }
        public int end { get; set; }
        public bool isObject { get; set; }
    }
    
}
internal static class JSONParser_ObjectToParam
{
    public static T ParseObjectTo<T>(object obj) where T : new()
    {
        T parsedObj = new();
        if (obj == null) return parsedObj;
        if (obj.GetType().Equals(typeof(Dictionary<string, object?>)))
        {
            parsedObj = ParseDictionaryTo<T>((Dictionary<string, object?>)obj);
        }
        else
        {
            parsedObj = (T)obj;
        }
        return parsedObj;
    }
    public static T ParseDictionaryTo<T>(Dictionary<string, object?> dictionary) where T : new()
    {
        T parsed = new();

        foreach (var property in typeof(T).GetProperties())
        {
            object? value;
            if (!dictionary.TryGetValue(property.Name, out value))
            {
                continue;
            }
            if (value == null)
            {
                property.SetValue(parsed, null);
                continue;
            }
            Type getT = value.GetType();

            if (getT.Equals(typeof(Dictionary<string, object?>)))
            {
                var parseMethod = typeof(JSONParser_ObjectToParam).GetMethod(
                            nameof(ParseDictionaryTo)
                        )?.MakeGenericMethod(property.PropertyType);
                var parsedValue = parseMethod?.Invoke(null,
                        new Dictionary<string, object?>[] { (Dictionary<string, object?>)value }
                    );
                property.SetValue(parsed, parsedValue);
            }
            else if (getT.Equals(typeof(object?[])))
            {
                Type arrTypeAsElement = (property.PropertyType.GetElementType()) ?? throw new Exception();
                var parseMethod = typeof(JSONParser_ObjectToParam).GetMethod(
                            nameof(ParseArrayTo)
                        )?.MakeGenericMethod(arrTypeAsElement);
                Array castedArray = (Array?)(parseMethod?.Invoke(null, new object?[][] { (object?[])value })) ?? throw new Exception();
                property.SetValue(parsed, castedArray);
            }
            else property.SetValue(parsed, value);
        }

        return parsed;
    }
    public static Array ParseArrayTo<T>(object?[] arr) where T : new()
    {
        if (arr.Length == 0)
        {
            return new object?[0];
        }
        Array castedArr = Array.CreateInstance(typeof(T), arr.Length);
        if (arr[0]?.GetType().Equals(typeof(Dictionary<string, object?>)) == true)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == null) castedArr.SetValue(null, i);
                else
                {
                    Dictionary<string, object?>? dict = (Dictionary<string, object?>?)arr[i];
                    if(dict == null)
                    {
                        castedArr.SetValue(null, i);
                        continue;
                    }
                    castedArr.SetValue(
                        ParseDictionaryTo<T>(
                                dict
                            ),
                        i);

                }
            }
        }
        else if (arr[0]?.GetType().Equals(typeof(object?[])) == true)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                object?[]? val = (object?[]?)arr[i];
                if (val == null)
                {
                    castedArr.SetValue(null, i);
                    continue;
                }
                castedArr.SetValue(
                    ParseArrayTo<T>(
                            val
                    ),
                    i
                );
            }
        }
        return castedArr;
    }

}
internal static class JSONParser_SerializeJSON
{
    public static string? Serialize(object? data)
    {
        if (data == null) return null;
        Type t = data.GetType();
        if (t.Equals(typeof(object?[]))) return Serialize((object?[])data);
        if (t.Equals(typeof(Dictionary<string, object?>))) return Serialize((Dictionary<string, object?>)data);
        throw new Exception();
    }
    public static string Serialize(object?[] data)
    {
        string serialized = "[";
        for (int i = 0; i < data.Length; i++)
        {
            serialized += $"{ParseObjectToString(data[i])},";
        }
        return serialized.Substring(0, serialized.Length - 1) + "]";
    }
    public static string Serialize(Dictionary<string, object?> data) {
        string[] keys = data.Keys.ToArray();
        object?[] values = data.Values.ToArray();
        string serialized = "{";
        for (int i = 0; i < data.Count; i++)
        {
            serialized += $"\"{keys[i]}\":{ParseObjectToString(values[i])},";
        }
        return serialized.Substring(0, serialized.Length - 1) + "}";
    }
    private static string ParseObjectToString(object? obj)
    {
        if (obj == null) return "null";
        Type t = obj.GetType(); 
        if (t.Equals(typeof(string))) return (string)obj;
        if (t.Equals(typeof(bool))) return ((bool)obj).ToString().ToLower();
        if (t.Equals(typeof(long))) return ((long)obj).ToString();
        if (t.Equals(typeof(double))) return ((double)obj).ToString();
        if (t.Equals(typeof(object?[]))) return Serialize((object?[])obj);
        if (t.Equals(typeof(Dictionary<string, object?>))) return Serialize((Dictionary<string, object?>)obj);
        throw new NotImplementedException($"Valore {obj} senza tipo apparente");
    }
}
internal static class JSONParser_ParamToObject
{
    public static Dictionary<string, object?> ParseToDictionary<T>(T data) where T : new()
    {
        Dictionary<string, object?> parsed = new();
        foreach (var property in typeof(T).GetProperties())
        {
            parsed.Add(property.Name, GetValue(property, data));
        }
        return parsed;
    }
    public static object?[] ParseToArray<T>(T[] data) where T : new()
    {
        object?[] parsed = new object?[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            parsed[i] = GetValue(typeof(T), data[i]);
        }
        return parsed;
    }
    private static object? GetValue(Type t, object? value)
    {
        if (IsSimpleType(t) || value == null)
        {
            return value;
        }
        if (t.IsArray)
        {
            Type elementT = t.GetElementType() ?? throw new Exception();
            return typeof(JSONParser_ParamToObject)
                    ?.GetMethod(nameof(ParseToArray))
                    ?.MakeGenericMethod(elementT)
                    ?.Invoke(null, new object?[][] { (object?[])value });
        }

        return typeof(JSONParser_ParamToObject)
            ?.GetMethod(nameof(ParseToDictionary))
            ?.MakeGenericMethod(t)
            ?.Invoke(null, new object?[] { value });
    }
    private static object? GetValue(System.Reflection.PropertyInfo property, object? data) => GetValue(property.PropertyType, property.GetValue(data));

    private static bool IsSimpleType(Type t)
    {
        return t.Equals(typeof(string))
            || t.Equals(typeof(bool))
            || t.Equals(typeof(byte))
            || t.Equals(typeof(short))
            || t.Equals(typeof(int))
            || t.Equals(typeof(long))
            || t.Equals(typeof(float))
            || t.Equals(typeof(double))
            || t.Equals(typeof(decimal));
    }
}
#nullable enable