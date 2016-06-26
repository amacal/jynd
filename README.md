# jynd

A library aims to deserialize json structure without being json deserializer.
It's designed to sequentially process a lot of json data without accessing all values.

## usage

```` csharp
string json = "{'firstName':'John','lastName':'Smith','isAlive':true,'age':25}".Replace('\'', '\"');
dynamic instance = JyndConvert.Deserialize(text);

if (instance.age > 18)
{
   Console.WriteLine($"first-name: {instance.firstName}");
   Console.WriteLine($"last-name : {instance.lastName}");
}
````

## benchmark

```` text
Benchmark-name         : wikipedia-person
Benchmark-iterations   : 1000000
Benchmark-source       : https://en.wikipedia.org/wiki/JSON
  Newtonsoft-static    : 00:00:19.3933187
  Jil-static           : 00:00:04.6791410
  NetJSON-static       : 00:00:04.9295304
  ServiceStack-static  : 00:00:15.8984467
  Newtonsoft-dynamic   : 00:00:35.7264182
  Jil-dynamic          : 00:00:14.5003835
  Jynd-dynamic         : 00:00:04.5124663

Benchmark-name         : github-team
Benchmark-iterations   : 1000000
Benchmark-source       : https://developer.github.com/v3/orgs/teams/
  Newtonsoft-static    : 00:00:18.9739121
  Jil-static           : 00:00:08.5277956
  NetJSON-static       : 00:00:10.7044743
  ServiceStack-static  : 00:00:22.5258885
  Newtonsoft-dynamic   : 00:00:34.6080537
  Jil-dynamic          : 00:00:18.6696534
  Jynd-dynamic         : 00:00:06.1566325

Benchmark-name         : github-primes
Benchmark-iterations   : 100000
Benchmark-source       : https://gist.github.com/miguelmota/ffa20854b1258cd27d7e
  Newtonsoft-static    : 00:00:26.5581805
  Jil-static           : 00:00:06.6431462
  NetJSON-static       : 00:00:09.3148845
  ServiceStack-static  : 00:00:27.0124813
  Newtonsoft-dynamic   : 00:01:13.2331559
  Jil-dynamic          : 00:00:34.1566038
  Jynd-dynamic         : 00:00:13.2129921
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

