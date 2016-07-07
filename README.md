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
  Newtonsoft-static    : 00:00:19.4718043
  Jil-static           : 00:00:04.7601955
  NetJSON-static       : 00:00:05.0674644
  ServiceStack-static  : 00:00:16.2145079
  Newtonsoft-dynamic   : 00:00:35.1612681
  Jil-dynamic          : 00:00:14.9270240
  Jynd-dynamic         : 00:00:04.5552243

Benchmark-name         : github-team
Benchmark-iterations   : 1000000
Benchmark-source       : https://developer.github.com/v3/orgs/teams/
  Newtonsoft-static    : 00:00:19.7743951
  Jil-static           : 00:00:08.8803099
  NetJSON-static       : 00:00:11.8112194
  ServiceStack-static  : 00:00:23.1389613
  Newtonsoft-dynamic   : 00:00:35.9433564
  Jil-dynamic          : 00:00:18.9785865
  Jynd-dynamic         : 00:00:06.4869722

Benchmark-name         : github-primes
Benchmark-iterations   : 100000
Benchmark-source       : https://gist.github.com/miguelmota/ffa20854b1258cd27d7e
  Newtonsoft-static    : 00:00:27.2725249
  Jil-static           : 00:00:06.8932180
  NetJSON-static       : 00:00:09.6496821
  ServiceStack-static  : 00:00:27.0691667
  Newtonsoft-dynamic   : 00:01:14.6536811
  Jil-dynamic          : 00:00:35.0228753
  Jynd-dynamic         : 00:00:15.1807016

Benchmark-name         : fixer-euro
Benchmark-iterations   : 1000000
Benchmark-source       : http://fixer.io/
  Newtonsoft-static    : 00:00:31.8951903
  Jil-static           : 00:00:09.0630034
  NetJSON-static       : 00:00:18.5166973
  ServiceStack-static  : 00:00:25.8890766
  Newtonsoft-dynamic   : 00:00:54.0693575
  Jil-dynamic          : 00:00:33.8668013
  Jynd-dynamic         : 00:00:04.8961307
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

