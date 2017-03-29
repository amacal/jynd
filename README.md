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
  Newtonsoft-static    : 00:00:20.0913329
  Jil-static           : 00:00:04.7922523
  NetJSON-static       : 00:00:05.2041902
  ServiceStack-static  : 00:00:15.4295675
  Newtonsoft-dynamic   : 00:00:35.4852685
  Jil-dynamic          : 00:00:15.2092177
  Jynd-dynamic         : 00:00:05.0953514

Benchmark-name         : github-team
Benchmark-iterations   : 1000000
Benchmark-source       : https://developer.github.com/v3/orgs/teams/
  Newtonsoft-static    : 00:00:19.8313953
  Jil-static           : 00:00:09.0153911
  NetJSON-static       : 00:00:11.4127063
  ServiceStack-static  : 00:00:22.6657516
  Newtonsoft-dynamic   : 00:00:37.2469127
  Jil-dynamic          : 00:00:18.5499242
  Jynd-dynamic         : 00:00:06.9722213

Benchmark-name         : github-primes
Benchmark-iterations   : 100000
Benchmark-source       : https://gist.github.com/miguelmota/ffa20854b1258cd27d7e
  Newtonsoft-static    : 00:00:31.1746872
  Jil-static           : 00:00:06.5717341
  NetJSON-static       : 00:00:09.6468195
  ServiceStack-static  : 00:00:27.0097992
  Newtonsoft-dynamic   : 00:01:18.1358461
  Jil-dynamic          : 00:00:34.1763395
  Jynd-dynamic         : 00:00:16.5507528

Benchmark-name         : fixer-euro
Benchmark-iterations   : 1000000
Benchmark-source       : http://fixer.io/
  Newtonsoft-static    : 00:00:28.5787154
  Jil-static           : 00:00:09.2379089
  NetJSON-static       : 00:00:22.8609298
  ServiceStack-static  : 00:00:25.6122554
  Newtonsoft-dynamic   : 00:00:51.9982219
  Jil-dynamic          : 00:00:21.2168637
  Jynd-dynamic         : 00:00:05.0173793
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

