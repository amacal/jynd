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
  Newtonsoft-static    : 00:00:08.7454309
  Jil-static           : 00:00:02.2443971
  NetJSON-static       : 00:00:02.5596427
  ServiceStack-static  : 00:00:06.8985175
  Newtonsoft-dynamic   : 00:00:15.3782884
  Jil-dynamic          : 00:00:06.5621247
  Jynd-dynamic         : 00:00:02.0620973

Benchmark-name         : github-team
Benchmark-iterations   : 1000000
Benchmark-source       : https://developer.github.com/v3/orgs/teams/
  Newtonsoft-static    : 00:00:09.8406619
  Jil-static           : 00:00:04.1763972
  NetJSON-static       : 00:00:05.3764264
  ServiceStack-static  : 00:00:10.5926059
  Newtonsoft-dynamic   : 00:00:17.2665255
  Jil-dynamic          : 00:00:08.9321158
  Jynd-dynamic         : 00:00:03.3167687

Benchmark-name         : github-primes
Benchmark-iterations   : 100000
Benchmark-source       : https://gist.github.com/miguelmota/ffa20854b1258cd27d7e
  Newtonsoft-static    : 00:00:15.1332802
  Jil-static           : 00:00:03.0307667
  NetJSON-static       : 00:00:05.5457491
  ServiceStack-static  : 00:00:14.3192217
  Newtonsoft-dynamic   : 00:00:42.8471472
  Jil-dynamic          : 00:00:17.6298882
  Jynd-dynamic         : 00:00:08.6635763

Benchmark-name         : fixer-euro
Benchmark-iterations   : 1000000
Benchmark-source       : http://fixer.io/
  Newtonsoft-static    : 00:00:16.9925532
  Jil-static           : 00:00:03.6656931
  NetJSON-static       : 00:00:10.7256258
  ServiceStack-static  : 00:00:12.7880777
  Newtonsoft-dynamic   : 00:00:26.9451385
  Jil-dynamic          : 00:00:10.5281193
  Jynd-dynamic         : 00:00:02.3496758
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

