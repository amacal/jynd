# jynd

A library aims to deserialize json structure without being json deserializer.

## usage

```` csharp
string json = "{'firstName':'John','lastName':'Smith','isAlive':true,'age':25}";
string text = json..Replace('\'', '\"');
dynamic instance = JyndConvert.Deserialize(text);

if (instance.age > 18)
{
   Console.WriteLint($"first-name: {instance.firstName}");
   Console.WriteLint($"last-name : {instance.lastName}");
}
````

## benchmark

```` text
Newtonsoft-static  : 00:00:19.3743379
Newtonsoft-dynamic : 00:00:33.1510260
Jil-static         : 00:00:04.7653259
Jil-dynamic        : 00:00:14.9730451
NetJSON-static     : 00:00:16.1322889
Jynd-dynamic       : 00:00:05.4039146
````

## restrictions

* white characters between tokens are not accepted
* property name character escaping is not accepted
* numeric are deserialized currently only as positive integers

## comparison

