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
Benchmark            : wikipedia-person / 1000000
Newtonsoft-static    : 00:00:19.6272074
Jil-static           : 00:00:04.7679532
NetJSON-static       : 00:00:04.9244612
ServiceStack-static  : 00:00:16.1347406
Newtonsoft-dynamic   : 00:00:34.6393721
Jil-dynamic          : 00:00:14.6421480
Jynd-dynamic         : 00:00:04.5511174

Benchmark            : github-team / 1000000
Newtonsoft-static    : 00:00:19.4548725
Jil-static           : 00:00:08.7893441
NetJSON-static       : 00:00:10.9093994
ServiceStack-static  : 00:00:22.8296541
Newtonsoft-dynamic   : 00:00:36.1920137
Jil-dynamic          : 00:00:18.5099919
Jynd-dynamic         : 00:00:06.1588427
````

## restrictions

* parsed text is valid json
* white characters between tokens are not accepted
* property name character escaping is not accepted
* numbers are deserialized currently only as Int32 or Int64
* deserialized object should be consumed before deserializing next one
* maximum total number of properties and array items: 65536
* only json primitives; no datetime, no guid etc.
* only object as a root

## contribution

* if something can be optimized, just suggest it with test case
* if you found bug, just report it with test case
* if something is not supported, just suggest it
* if you do not agree with published benchmarks, just suggest new one

## comparison

