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
  Newtonsoft-static    : 00:00:19.0447913
  Jil-static           : 00:00:04.7829816
  NetJSON-static       : 00:00:05.2842786
  ServiceStack-static  : 00:00:15.9180370
  Newtonsoft-dynamic   : 00:00:34.6170376
  Jil-dynamic          : 00:00:14.5715347
  Jynd-dynamic         : 00:00:04.8754398

Benchmark-name         : github-team
Benchmark-iterations   : 1000000
Benchmark-source       : https://developer.github.com/v3/orgs/teams/
  Newtonsoft-static    : 00:00:19.6827415
  Jil-static           : 00:00:08.6094227
  NetJSON-static       : 00:00:11.2008325
  ServiceStack-static  : 00:00:22.8235533
  Newtonsoft-dynamic   : 00:00:35.9419941
  Jil-dynamic          : 00:00:18.7056635
  Jynd-dynamic         : 00:00:06.9388909

Benchmark-name         : github-primes
Benchmark-iterations   : 100000
Benchmark-source       : https://gist.github.com/miguelmota/ffa20854b1258cd27d7e
  Newtonsoft-static    : 00:00:27.2542509
  Jil-static           : 00:00:06.7623538
  NetJSON-static       : 00:00:09.6370117
  ServiceStack-static  : 00:00:27.3137608
  Newtonsoft-dynamic   : 00:01:13.3801238
  Jil-dynamic          : 00:00:34.9200620
  Jynd-dynamic         : 00:00:15.0135962

Benchmark-name         : fixer-euro
Benchmark-iterations   : 1000000
Benchmark-source       : http://fixer.io/
  Newtonsoft-static    : 00:00:31.8487212
  Jil-static           : 00:00:09.2138406
  NetJSON-static       : 00:00:19.3367304
  ServiceStack-static  : 00:00:27.0336486
  Newtonsoft-dynamic   : 00:00:55.2691427
  Jil-dynamic          : 00:00:20.6400026
  Jynd-dynamic         : 00:00:04.9991582
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

