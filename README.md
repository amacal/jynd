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
Newtonsoft-static    : 00:00:19.1861077
Newtonsoft-dynamic   : 00:00:34.1148832
Jil-static           : 00:00:04.7849318
Jil-dynamic          : 00:00:15.0446824
NetJSON-static       : 00:00:04.9173038
ServiceStack-static  : 00:00:16.0641011
Jynd-dynamic         : 00:00:04.8204089
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

## contribution

* if something can be optimized, just suggest it with test case
* if you found bug, just report it with test case
* if something is not supported, just suggest it
* if you do not agree with published benchmarks, just suggest new one

## comparison

