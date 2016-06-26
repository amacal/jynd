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
Newtonsoft-static    : 00:00:19.0318520
Jil-static           : 00:00:04.7298654
NetJSON-static       : 00:00:04.9543968
ServiceStack-static  : 00:00:15.7536934
Newtonsoft-dynamic   : 00:00:34.1806022
Jil-dynamic          : 00:00:14.3318563
Jynd-dynamic         : 00:00:04.4783020

Benchmark            : github-team / 1000000
Newtonsoft-static    : 00:00:19.1118558
Jil-static           : 00:00:08.6239376
NetJSON-static       : 00:00:10.6875480
ServiceStack-static  : 00:00:22.4862749
Newtonsoft-dynamic   : 00:00:34.1209780
Jil-dynamic          : 00:00:18.3963518
Jynd-dynamic         : 00:00:06.2053519

Benchmark            : github-primes / 100000
Newtonsoft-static    : 00:00:26.6857642
Jil-static           : 00:00:06.4572273
NetJSON-static       : 00:00:09.4056174
ServiceStack-static  : 00:00:27.2869314
Newtonsoft-dynamic   : 00:01:13.6742811
Jil-dynamic          : 00:00:33.1943844
Jynd-dynamic         : 00:00:13.3867741
````

## restrictions

* maximum json size: 64kB
* parsed text is valid json
* white characters between tokens are not accepted
* property name character escaping is not accepted
* numbers are deserialized currently only to Int32 or Int64
* deserialized object should be consumed before deserializing next one
* only json primitives; no datetime, no guid etc.

## contribution

* if something can be optimized, just suggest it with test case
* if you found bug, just report it with test case
* if something is not supported, just suggest it
* if you do not agree with published benchmarks, just suggest new one

## comparison

