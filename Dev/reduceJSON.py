import json

inDir = "almanac.json"
outDir = inDir
value = ""
with open(inDir, "r") as inFile:
    value = json.dumps(json.load(inFile), separators=(",", ":"))

with open(outDir, "w") as outFile:
    outFile.write(value)

print("JSON data copied successfully to the output file.")  