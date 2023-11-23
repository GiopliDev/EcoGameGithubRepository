using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices; //DLLImport
using UnityEngine;

public class AlmanacManager : MonoBehaviour
{
    
    public static string JSON_DIR = "./Data/almanac.json";
    Almanac almanac;
    bool isShown = false;
    void Start()
    {
        string JSON_DATA = System.IO.File.ReadAllText(AlmanacManager.JSON_DIR);
        //Debug.Log(JSON_DATA);
        var val = JSONParser.From<Almanac>(JSON_DATA);
        Debug.Log(val.Items[1].ID);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (isShown) this.HideAlmanac();
            else this.ShowAlmanac();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && this.isShown) this.HideAlmanac();
    }
    void ShowAlmanac()
    {
        this.isShown = true;
    }
    void HideAlmanac()
    {
        this.isShown = false;
    }
}
class Almanac
{
    public CollectionElement[] Items { get; set; }
}
class CollectionElement
{
    public string ID { get; set; }
    public string Image { get; set; } // ./img/pianta.png
    public string Info { get; set; }
    public string Name { get; set; }
    public string Section { get; set; }
    public bool Unlocked { get; set; }
}
public static class JSONParser
{
    private static readonly System.Random rdm = new();
    private static readonly List<object?[]> arraysID = new();
    private static readonly List<Dictionary<string, object?>> objectsID = new();
    private static ObjectStart getObjectStart(string json)
    {
        int lastObject;
        int lastArray;
        int temp = json.IndexOf("{");
        do
        {
            lastObject = temp;
            temp = json.IndexOf('{', lastObject + 1);
        } while (temp != -1);
        temp = json.IndexOf("[");
        do
        {
            lastArray = temp;
            temp = json.IndexOf("[", lastArray + 1);
        } while (temp != -1);
        return (lastObject > lastArray)
            ? new()
            {
                start = lastObject,
                end = json.IndexOf('}', lastObject),
                isObject = true
            }
            : new()
            {
                start = lastArray,
                end = json.IndexOf(']', lastArray),
                isObject = false
            };
    }
    public static Dictionary<string, object?> From(string jsonString)
    {
        objectsID.Clear();
        arraysID.Clear();
        while (InnerObjects(jsonString) > 1)
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
        jsonString = jsonString.Trim().Substring(1, jsonString.Length - 2).Trim();
        return DecomposeObject(jsonString);
    }
    public static T From<T>(string jsonString) where T : new() => ParseDictionaryToObject<T>(From(jsonString));
    public static T ParseDictionaryToObject<T>(Dictionary<string, object?> dictionary) where T : new()
    {
        T obj = new();

        foreach (var property in typeof(T).GetProperties())
        {
            object? value;
            if (!dictionary.TryGetValue(property.Name, out value))
            {
                continue;
            }
            if (value == null)
            {
                property.SetValue(obj, null);
                continue;
            }
            Type getT = value.GetType();

            if (!getT.Equals(typeof(Dictionary<string, object?>)) && !getT.Equals(typeof(object?[])))
            {
                property.SetValue(obj, value);
                continue;
            }

            if (getT.Equals(typeof(Dictionary<string, object?>)))
            {

                var parseMethod = typeof(JSONParser).GetMethod(
                            nameof(ParseDictionaryToObject)
                        )?.MakeGenericMethod(property.PropertyType);
                var parsedValue = parseMethod?.Invoke(null,
                        new Dictionary<string, object?>[] { (Dictionary<string, object?>)value }
                    );
                property.SetValue(obj, parsedValue);
            }
            else if (getT.Equals(typeof(object?[])))
            {
                object?[] arr = (object?[])value;
                if (arr.Length == 0)
                {
                    property.SetValue(obj, null);
                    continue;
                }
                if (arr[0]?.GetType().Equals(typeof(Dictionary<string, object?>)) != true)
                {
                    property.SetValue(obj, arr);
                    continue;
                }

                Type? tAsNoArr = Type.GetType(property.PropertyType.ToString().TrimEnd('[', ']'));
                if (tAsNoArr == null) { throw new Exception(); }
                var parseMethod = typeof(JSONParser).GetMethod(
                            nameof(ParseDictionaryToObject)
                        )?.MakeGenericMethod(tAsNoArr);
                Array castedArr = Array.CreateInstance(tAsNoArr, arr.Length);
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] != null)
                        castedArr.SetValue(
                            parseMethod?.Invoke(null,
                                new Dictionary<string, object?>[] {
                                    arr[i] as Dictionary<string, object?>
                                }),
                            i);
                    else castedArr.SetValue(null, i);
                }
                property.SetValue(obj, castedArr);
            }
            else property.SetValue(obj, value);
        }

        return obj;
    }
    private static Dictionary<string, object?> DecomposeObject(string data)
    {
        Dictionary<string, object?> jsonObject = new();
        foreach (string pair in data.Split(','))
        {
            string[] keyValue = pair.Split(':');
            string key = keyValue[0].Trim().Trim('"');
            string value = keyValue[1].Trim();
            jsonObject.Add(key, GetValueFromType(value));
        }
        return jsonObject;
    }
    private static object?[] DecomposeArray(string data)
    {
        string[] singleDatas = data.Split(",");
        object?[] elements = new object?[singleDatas.Length];
        for (int i = 0; i < singleDatas.Length; i++)
        {
            string value = singleDatas[i].Trim();
            elements[i] = GetValueFromType(value);
        }
        return elements;
    }
    private static int InnerObjects(string value)
        => value.Count((char c) => c == '{' || c == '[');
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