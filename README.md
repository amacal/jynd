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
  Newtonsoft-static    : 00:00:08.4715046
  Jil-static           : 00:00:02.2096915
  NetJSON-static       : 00:00:02.4266845
  ServiceStack-static  : 00:00:06.9966332
  Newtonsoft-dynamic   : 00:00:14.6977741
  Jil-dynamic          : 00:00:06.4721997
  Jynd-dynamic         : 00:00:01.8908531

Benchmark-name         : github-team
Benchmark-iterations   : 1000000
Benchmark-source       : https://developer.github.com/v3/orgs/teams/
  Newtonsoft-static    : 00:00:09.7894550
  Jil-static           : 00:00:04.3630510
  NetJSON-static       : 00:00:05.3863673
  ServiceStack-static  : 00:00:11.4116519
  Newtonsoft-dynamic   : 00:00:16.8710579
  Jil-dynamic          : 00:00:08.7350959
  Jynd-dynamic         : 00:00:03.0279669

Benchmark-name         : github-primes
Benchmark-iterations   : 100000
Benchmark-source       : https://gist.github.com/miguelmota/ffa20854b1258cd27d7e
  Newtonsoft-static    : 00:00:14.2209308
  Jil-static           : 00:00:02.9633484
  NetJSON-static       : 00:00:05.3728332
  ServiceStack-static  : 00:00:13.8302390
  Newtonsoft-dynamic   : 00:00:44.2566514
  Jil-dynamic          : 00:00:16.9079032
  Jynd-dynamic         : 00:00:07.3567065
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

