# jynd

A library aims to deserialize json structure without being json deserializer.
It's designed to sequentially process a lot of normalized json data without accessing all values.

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
  Newtonsoft-static    : 00:00:19.6284385
  Jil-static           : 00:00:04.8075561
  NetJSON-static       : 00:00:05.1280916
  ServiceStack-static  : 00:00:15.4008608
  Newtonsoft-dynamic   : 00:00:35.3618547
  Jil-dynamic          : 00:00:15.3072735
  Jynd-dynamic         : 00:00:05.0947314

Benchmark-name         : github-team
Benchmark-iterations   : 1000000
Benchmark-source       : https://developer.github.com/v3/orgs/teams/
  Newtonsoft-static    : 00:00:19.7191821
  Jil-static           : 00:00:08.8004487
  NetJSON-static       : 00:00:11.5438021
  ServiceStack-static  : 00:00:22.7449721
  Newtonsoft-dynamic   : 00:00:36.6035795
  Jil-dynamic          : 00:00:18.5391511
  Jynd-dynamic         : 00:00:06.9575134

Benchmark-name         : github-primes
Benchmark-iterations   : 100000
Benchmark-source       : https://gist.github.com/miguelmota/ffa20854b1258cd27d7e
  Newtonsoft-static    : 00:00:31.0655920
  Jil-static           : 00:00:06.6175760
  NetJSON-static       : 00:00:09.7062584
  ServiceStack-static  : 00:00:27.5592449
  Newtonsoft-dynamic   : 00:01:18.4987408
  Jil-dynamic          : 00:00:33.4526240
  Jynd-dynamic         : 00:00:16.9399338

Benchmark-name         : fixer-euro
Benchmark-iterations   : 1000000
Benchmark-source       : http://fixer.io/
  Newtonsoft-static    : 00:00:33.3833455
  Jil-static           : 00:00:09.1407323
  NetJSON-static       : 00:00:21.9833665
  ServiceStack-static  : 00:00:25.4269708
  Newtonsoft-dynamic   : 00:00:59.4851057
  Jil-dynamic          : 00:00:20.8130505
  Jynd-dynamic         : 00:00:04.9891030
````

## restrictions

* maximum json size: 64kB
* parsed text is valid json
* white characters between tokens is limited to spaces
* property name character escaping is not accepted
* numbers are deserialized only to Int32, Int64, BigInteger or Double
* deserialized object should be consumed before deserializing next one
* only json primitives; no datetime, no guid etc.

## contribution

* if something can be optimized, just suggest it with test case
* if you found bug, just report it with test case
* if something is not supported, just suggest it
* if you do not agree with published benchmarks, just suggest new one

## comparison

