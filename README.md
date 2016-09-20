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
  Newtonsoft-static    : 00:00:19.4093172
  Jil-static           : 00:00:04.6676834
  NetJSON-static       : 00:00:05.1380347
  ServiceStack-static  : 00:00:15.9946149
  Newtonsoft-dynamic   : 00:00:35.1923902
  Jil-dynamic          : 00:00:14.6251731
  Jynd-dynamic         : 00:00:06.0389644

Benchmark-name         : github-team
Benchmark-iterations   : 1000000
Benchmark-source       : https://developer.github.com/v3/orgs/teams/
  Newtonsoft-static    : 00:00:20.0703090
  Jil-static           : 00:00:08.5984938
  NetJSON-static       : 00:00:10.7687014
  ServiceStack-static  : 00:00:23.1757052
  Newtonsoft-dynamic   : 00:00:36.9394107
  Jil-dynamic          : 00:00:19.1798360
  Jynd-dynamic         : 00:00:06.7375976

Benchmark-name         : github-primes
Benchmark-iterations   : 100000
Benchmark-source       : https://gist.github.com/miguelmota/ffa20854b1258cd27d7e
  Newtonsoft-static    : 00:00:27.8132564
  Jil-static           : 00:00:06.8345417
  NetJSON-static       : 00:00:09.8760014
  ServiceStack-static  : 00:00:26.5537020
  Newtonsoft-dynamic   : 00:01:15.1657331
  Jil-dynamic          : 00:00:34.0717963
  Jynd-dynamic         : 00:00:15.3078622

Benchmark-name         : fixer-euro
Benchmark-iterations   : 1000000
Benchmark-source       : http://fixer.io/
  Newtonsoft-static    : 00:00:32.6460176
  Jil-static           : 00:00:09.1884491
  NetJSON-static       : 00:00:18.6932125
  ServiceStack-static  : 00:00:25.5018634
  Newtonsoft-dynamic   : 00:00:53.0711671
  Jil-dynamic          : 00:00:20.0885800
  Jynd-dynamic         : 00:00:04.7593842
````

## restrictions

* maximum json size: 64kB
* parsed text is valid json
* white characters between tokens are not accepted
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

