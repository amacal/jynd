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
Newtonsoft-static    : 00:00:18.9277581
Jil-static           : 00:00:04.7054016
NetJSON-static       : 00:00:04.9326692
ServiceStack-static  : 00:00:15.8308685
Newtonsoft-dynamic   : 00:00:34.2476799
Jil-dynamic          : 00:00:14.4239826
Jynd-dynamic         : 00:00:04.4421680

Benchmark            : github-team / 1000000
Newtonsoft-static    : 00:00:19.0036560
Jil-static           : 00:00:08.5937214
NetJSON-static       : 00:00:10.8219491
ServiceStack-static  : 00:00:22.4996151
Newtonsoft-dynamic   : 00:00:34.0980454
Jil-dynamic          : 00:00:18.3799502
Jynd-dynamic         : 00:00:06.1867880

Benchmark            : github-primes / 100000
Newtonsoft-static    : 00:00:26.5159627
Jil-static           : 00:00:06.4497767
NetJSON-static       : 00:00:09.3411779
ServiceStack-static  : 00:00:26.3383107
Newtonsoft-dynamic   : 00:01:14.5194623
Jil-dynamic          : 00:00:33.3409051
Jynd-dynamic         : 00:00:13.4317191
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

