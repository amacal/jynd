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
  Newtonsoft-static    : 00:00:20.2410348
  Jil-static           : 00:00:04.8677559
  NetJSON-static       : 00:00:06.0959003
  ServiceStack-static  : 00:00:14.5212328
  Newtonsoft-dynamic   : 00:00:35.3597621
  Jil-dynamic          : 00:00:15.3147574
  Jynd-dynamic         : 00:00:05.0032755

Benchmark-name         : github-team
Benchmark-iterations   : 1000000
Benchmark-source       : https://developer.github.com/v3/orgs/teams/
  Newtonsoft-static    : 00:00:19.5621014
  Jil-static           : 00:00:08.8371932
  NetJSON-static       : 00:00:11.9085499
  ServiceStack-static  : 00:00:18.9029491
  Newtonsoft-dynamic   : 00:00:36.0713866
  Jil-dynamic          : 00:00:19.4018485
  Jynd-dynamic         : 00:00:07.0017428

Benchmark-name         : github-primes
Benchmark-iterations   : 100000
Benchmark-source       : https://gist.github.com/miguelmota/ffa20854b1258cd27d7e
  Newtonsoft-static    : 00:00:31.6621354
  Jil-static           : 00:00:06.8020385
  NetJSON-static       : 00:00:09.6937290
  ServiceStack-static  : 00:00:25.1973806
  Newtonsoft-dynamic   : 00:01:22.9414035
  Jil-dynamic          : 00:00:34.7418442
  Jynd-dynamic         : 00:00:16.9868031

Benchmark-name         : fixer-euro
Benchmark-iterations   : 1000000
Benchmark-source       : http://fixer.io/
  Newtonsoft-static    : 00:00:33.9060616
  Jil-static           : 00:00:09.2360484
  NetJSON-static       : 00:00:22.4918854
  ServiceStack-static  : 00:00:28.5325402
  Newtonsoft-dynamic   : 00:00:56.8755878
  Jil-dynamic          : 00:00:34.2072302
  Jynd-dynamic         : 00:00:05.1063225
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

