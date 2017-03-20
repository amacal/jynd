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
  Newtonsoft-static    : 00:00:19.6971498
  Jil-static           : 00:00:04.7283990
  NetJSON-static       : 00:00:05.1726515
  ServiceStack-static  : 00:00:15.2927588
  Newtonsoft-dynamic   : 00:00:35.3846823
  Jil-dynamic          : 00:00:14.4980828
  Jynd-dynamic         : 00:00:04.9262077

Benchmark-name         : github-team
Benchmark-iterations   : 1000000
Benchmark-source       : https://developer.github.com/v3/orgs/teams/
  Newtonsoft-static    : 00:00:19.2128856
  Jil-static           : 00:00:08.6040438
  NetJSON-static       : 00:00:11.0502558
  ServiceStack-static  : 00:00:22.9208539
  Newtonsoft-dynamic   : 00:00:34.7641828
  Jil-dynamic          : 00:00:18.2716017
  Jynd-dynamic         : 00:00:06.7880928

Benchmark-name         : github-primes
Benchmark-iterations   : 100000
Benchmark-source       : https://gist.github.com/miguelmota/ffa20854b1258cd27d7e
  Newtonsoft-static    : 00:00:31.2887328
  Jil-static           : 00:00:06.4432522
  NetJSON-static       : 00:00:09.3807870
  ServiceStack-static  : 00:00:27.5069740
  Newtonsoft-dynamic   : 00:01:20.3323470
  Jil-dynamic          : 00:00:33.4708535
  Jynd-dynamic         : 00:00:15.9238148

Benchmark-name         : fixer-euro
Benchmark-iterations   : 1000000
Benchmark-source       : http://fixer.io/
  Newtonsoft-static    : 00:00:31.0687009
  Jil-static           : 00:00:09.1076984
  NetJSON-static       : 00:00:22.0068599
  ServiceStack-static  : 00:00:24.8131636
  Newtonsoft-dynamic   : 00:00:50.1393166
  Jil-dynamic          : 00:00:20.1966028
  Jynd-dynamic         : 00:00:04.7108935
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

