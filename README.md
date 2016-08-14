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
  Newtonsoft-static    : 00:00:20.3348868
  Jil-static           : 00:00:04.6792021
  NetJSON-static       : 00:00:05.3360779
  ServiceStack-static  : 00:00:15.8939173
  Newtonsoft-dynamic   : 00:00:34.7668156
  Jil-dynamic          : 00:00:14.6381721
  Jynd-dynamic         : 00:00:04.7676081

Benchmark-name         : github-team
Benchmark-iterations   : 1000000
Benchmark-source       : https://developer.github.com/v3/orgs/teams/
  Newtonsoft-static    : 00:00:19.4863031
  Jil-static           : 00:00:08.5657622
  NetJSON-static       : 00:00:11.0281440
  ServiceStack-static  : 00:00:22.5673363
  Newtonsoft-dynamic   : 00:00:35.4132158
  Jil-dynamic          : 00:00:18.8312964
  Jynd-dynamic         : 00:00:06.7218710

Benchmark-name         : github-primes
Benchmark-iterations   : 100000
Benchmark-source       : https://gist.github.com/miguelmota/ffa20854b1258cd27d7e
  Newtonsoft-static    : 00:00:27.0052821
  Jil-static           : 00:00:06.7327532
  NetJSON-static       : 00:00:09.7347767
  ServiceStack-static  : 00:00:26.8153826
  Newtonsoft-dynamic   : 00:01:14.6775265
  Jil-dynamic          : 00:00:35.1815907
  Jynd-dynamic         : 00:00:15.4580510

Benchmark-name         : fixer-euro
Benchmark-iterations   : 1000000
Benchmark-source       : http://fixer.io/
  Newtonsoft-static    : 00:00:31.5018565
  Jil-static           : 00:00:09.1944858
  NetJSON-static       : 00:00:18.6886537
  ServiceStack-static  : 00:00:25.5579143
  Newtonsoft-dynamic   : 00:00:55.4589750
  Jil-dynamic          : 00:00:20.2628992
  Jynd-dynamic         : 00:00:04.4761995
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

