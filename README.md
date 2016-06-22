# jynd

A library aims to deserialize json structure without being json deserializer.

## usage

```` csharp
string json = "{'firstName':'John','lastName':'Smith','isAlive':true,'age':25}";
string text = json.Replace('\'', '\"');
dynamic instance = JyndConvert.Deserialize(text);

if (instance.age > 18)
{
   Console.WriteLint($"first-name: {instance.firstName}");
   Console.WriteLint($"last-name : {instance.lastName}");
}
````

## benchmark

```` text
Newtonsoft-static    : 00:00:19.6265434
Newtonsoft-dynamic   : 00:00:34.3134509
Jil-static           : 00:00:04.9729328
Jil-dynamic          : 00:00:15.5791588
NetJSON-static       : 00:00:05.0164794
ServiceStack-static  : 00:00:16.4114428
Jynd-dynamic         : 00:00:05.6991778
````

## restrictions

* white characters between tokens are not accepted
* property name character escaping is not accepted
* numbers are deserialized currently only as Int32 or Int64
* deserialized object should be consumed before deserializing next one
* maximum structure depth: 32
* maximum total number of properties and array items: 256
* only json primitives; no datetime, no guid etc.
* only object as a root

## comparison

