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
  Newtonsoft-static    : 00:00:20.5962259
  Jil-static           : 00:00:04.6923455
  NetJSON-static       : 00:00:05.8510103
  ServiceStack-static  : 00:00:15.4513712
  Newtonsoft-dynamic   : 00:00:34.7945072
  Jil-dynamic          : 00:00:14.8699744
  Jynd-dynamic         : 00:00:04.7649017

Benchmark-name         : github-team
Benchmark-iterations   : 1000000
Benchmark-source       : https://developer.github.com/v3/orgs/teams/
  Newtonsoft-static    : 00:00:20.1451000
  Jil-static           : 00:00:08.7036202
  NetJSON-static       : 00:00:12.3868014
  ServiceStack-static  : 00:00:22.5733616
  Newtonsoft-dynamic   : 00:00:34.4905597
  Jil-dynamic          : 00:00:19.1849783
  Jynd-dynamic         : 00:00:06.6832479

Benchmark-name         : github-primes
Benchmark-iterations   : 100000
Benchmark-source       : https://gist.github.com/miguelmota/ffa20854b1258cd27d7e
  Newtonsoft-static    : 00:00:27.2972792
  Jil-static           : 00:00:06.7019662
  NetJSON-static       : 00:00:09.5530544
  ServiceStack-static  : 00:00:26.3849390
  Newtonsoft-dynamic   : 00:01:13.8985969
  Jil-dynamic          : 00:00:36.0602857
  Jynd-dynamic         : 00:00:15.7759978

Benchmark-name         : fixer-euro
Benchmark-iterations   : 1000000
Benchmark-source       : http://fixer.io/
  Newtonsoft-static    : 00:00:33.6203300
  Jil-static           : 00:00:09.1952973
  NetJSON-static       : 00:00:22.4867025
  ServiceStack-static  : 00:00:24.1589371
  Newtonsoft-dynamic   : 00:00:54.9709081
  Jil-dynamic          : 00:00:20.2710476
  Jynd-dynamic         : 00:00:04.7091138
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

