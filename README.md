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
  Newtonsoft-static    : 00:00:19.0845094
  Jil-static           : 00:00:04.6111309
  NetJSON-static       : 00:00:04.8835336
  ServiceStack-static  : 00:00:15.3714663
  Newtonsoft-dynamic   : 00:00:34.8332028
  Jil-dynamic          : 00:00:14.8798369
  Jynd-dynamic         : 00:00:04.7542307

Benchmark-name         : github-team
Benchmark-iterations   : 1000000
Benchmark-source       : https://developer.github.com/v3/orgs/teams/
  Newtonsoft-static    : 00:00:19.5910957
  Jil-static           : 00:00:08.5065789
  NetJSON-static       : 00:00:10.8244840
  ServiceStack-static  : 00:00:22.1203117
  Newtonsoft-dynamic   : 00:00:34.6481264
  Jil-dynamic          : 00:00:18.3323233
  Jynd-dynamic         : 00:00:06.6386896

Benchmark-name         : github-primes
Benchmark-iterations   : 100000
Benchmark-source       : https://gist.github.com/miguelmota/ffa20854b1258cd27d7e
  Newtonsoft-static    : 00:00:26.5229805
  Jil-static           : 00:00:06.6044311
  NetJSON-static       : 00:00:09.4034561
  ServiceStack-static  : 00:00:26.0160010
  Newtonsoft-dynamic   : 00:01:17.8782727
  Jil-dynamic          : 00:00:34.2943780
  Jynd-dynamic         : 00:00:15.8269748

Benchmark-name         : fixer-euro
Benchmark-iterations   : 1000000
Benchmark-source       : http://fixer.io/
  Newtonsoft-static    : 00:00:34.3373270
  Jil-static           : 00:00:09.2445809
  NetJSON-static       : 00:00:18.1904642
  ServiceStack-static  : 00:00:24.6911311
  Newtonsoft-dynamic   : 00:00:54.9241576
  Jil-dynamic          : 00:00:20.3235170
  Jynd-dynamic         : 00:00:04.7359671
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

