# jynd

A library aims to deserialize json structure without being json deserializer.
It's designed to sequentially process a lot of json data without accessing all values.

## usage

```` csharp
string json = "{'firstName':'John','lastName':'Smith','isAlive':true,'age':25}".Replace('\'', '\"');
dynamic instance = JyndConvert.Deserialize(json);

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
  Newtonsoft-static    : 00:00:08.9546492
  Jil-static           : 00:00:02.2172097
  NetJSON-static       : 00:00:02.4871492
  ServiceStack-static  : 00:00:07.1992085
  Newtonsoft-dynamic   : 00:00:15.0494888
  Jil-dynamic          : 00:00:06.7985713
  Jynd-dynamic         : 00:00:01.9538601

Benchmark-name         : github-team
Benchmark-iterations   : 1000000
Benchmark-source       : https://developer.github.com/v3/orgs/teams/
  Newtonsoft-static    : 00:00:10.1541234
  Jil-static           : 00:00:04.1735911
  NetJSON-static       : 00:00:05.3663155
  ServiceStack-static  : 00:00:11.9032737
  Newtonsoft-dynamic   : 00:00:17.6650277
  Jil-dynamic          : 00:00:09.3150849
  Jynd-dynamic         : 00:00:03.1319912

Benchmark-name         : github-primes
Benchmark-iterations   : 100000
Benchmark-source       : https://gist.github.com/miguelmota/ffa20854b1258cd27d7e
  Newtonsoft-static    : 00:00:15.2778897
  Jil-static           : 00:00:03.0255151
  NetJSON-static       : 00:00:05.5321719
  ServiceStack-static  : 00:00:13.9883933
  Newtonsoft-dynamic   : 00:00:45.9942924
  Jil-dynamic          : 00:00:18.2756314
  Jynd-dynamic         : 00:00:07.5390734
````

## restrictions

* maximum json size: 64kB
* parsed text is valid json
* white characters between tokens are not accepted
* property name character escaping is not accepted
* numbers are deserialized currently only to Int32, Int64 or BigInteger
* deserialized object should be consumed before deserializing next one
* only json primitives; no datetime, no guid etc.

## contribution

* if something can be optimized, just suggest it with test case
* if you found bug, just report it with test case
* if something is not supported, just suggest it
* if you do not agree with published benchmarks, just suggest new one

## comparison

