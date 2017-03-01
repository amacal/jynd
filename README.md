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
  Newtonsoft-static    : 00:00:08.8868803
  Jil-static           : 00:00:03.1337520
  NetJSON-static       : 00:00:02.7983307
  ServiceStack-static  : 00:00:07.3067884
  Newtonsoft-dynamic   : 00:00:15.0575511
  Jil-dynamic          : 00:00:06.7722280
  Jynd-dynamic         : 00:00:02.1606120

Benchmark-name         : github-team
Benchmark-iterations   : 1000000
Benchmark-source       : https://developer.github.com/v3/orgs/teams/
  Newtonsoft-static    : 00:00:10.7627758
  Jil-static           : 00:00:04.5327098
  NetJSON-static       : 00:00:06.3603875
  ServiceStack-static  : 00:00:11.5320675
  Newtonsoft-dynamic   : 00:00:17.6834921
  Jil-dynamic          : 00:00:09.2801894
  Jynd-dynamic         : 00:00:03.3714595

Benchmark-name         : github-primes
Benchmark-iterations   : 100000
Benchmark-source       : https://gist.github.com/miguelmota/ffa20854b1258cd27d7e
  Newtonsoft-static    : 00:00:15.3371145
  Jil-static           : 00:00:02.9944482
  NetJSON-static       : 00:00:05.4500089
  ServiceStack-static  : 00:00:14.3706244
  Newtonsoft-dynamic   : 00:00:43.2885191
  Jil-dynamic          : 00:00:17.5188066
  Jynd-dynamic         : 00:00:08.6739537

Benchmark-name         : fixer-euro
Benchmark-iterations   : 1000000
Benchmark-source       : http://fixer.io/
  Newtonsoft-static    : 00:00:17.7567485
  Jil-static           : 00:00:03.7180713
  NetJSON-static       : 00:00:12.8383276
  ServiceStack-static  : 00:00:12.8218348
  Newtonsoft-dynamic   : 00:00:27.1009926
  Jil-dynamic          : 00:00:10.7849419
  Jynd-dynamic         : 00:00:02.4208206
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

