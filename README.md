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
  Newtonsoft-static    : 00:00:19.4917942
  Jil-static           : 00:00:04.7872898
  NetJSON-static       : 00:00:05.1051999
  ServiceStack-static  : 00:00:13.8844154
  Newtonsoft-dynamic   : 00:00:34.9880921
  Jil-dynamic          : 00:00:14.6299449
  Jynd-dynamic         : 00:00:04.8633800

Benchmark-name         : github-team
Benchmark-iterations   : 1000000
Benchmark-source       : https://developer.github.com/v3/orgs/teams/
  Newtonsoft-static    : 00:00:19.6257514
  Jil-static           : 00:00:08.6489856
  NetJSON-static       : 00:00:11.0725983
  ServiceStack-static  : 00:00:17.7973787
  Newtonsoft-dynamic   : 00:00:35.1309126
  Jil-dynamic          : 00:00:18.3049450
  Jynd-dynamic         : 00:00:06.9044215

Benchmark-name         : github-primes
Benchmark-iterations   : 100000
Benchmark-source       : https://gist.github.com/miguelmota/ffa20854b1258cd27d7e
  Newtonsoft-static    : 00:00:30.6044887
  Jil-static           : 00:00:06.4500777
  NetJSON-static       : 00:00:09.4385082
  ServiceStack-static  : 00:00:24.6889355
  Newtonsoft-dynamic   : 00:01:23.5274470
  Jil-dynamic          : 00:00:32.7903495
  Jynd-dynamic         : 00:00:16.7569155

Benchmark-name         : fixer-euro
Benchmark-iterations   : 1000000
Benchmark-source       : http://fixer.io/
  Newtonsoft-static    : 00:00:32.8417478
  Jil-static           : 00:00:09.1047174
  NetJSON-static       : 00:00:22.6581762
  ServiceStack-static  : 00:00:27.4600953
  Newtonsoft-dynamic   : 00:00:59.0926416
  Jil-dynamic          : 00:00:21.0387743
  Jynd-dynamic         : 00:00:05.0358293
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

