# jynd

A library aims to deserialize json structure without being json deserializer.
It's designed to sequentially process a lot of json data without accessing all values.

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
  Newtonsoft-static    : 00:00:19.1029380
  Jil-static           : 00:00:04.7398846
  NetJSON-static       : 00:00:04.8774207
  ServiceStack-static  : 00:00:16.0090365
  Newtonsoft-dynamic   : 00:00:34.4070329
  Jil-dynamic          : 00:00:14.5117286
  Jynd-dynamic         : 00:00:04.5645920

Benchmark-name         : github-team
Benchmark-iterations   : 1000000
Benchmark-source       : https://developer.github.com/v3/orgs/teams/
  Newtonsoft-static    : 00:00:19.1573296
  Jil-static           : 00:00:08.5166328
  NetJSON-static       : 00:00:10.7997476
  ServiceStack-static  : 00:00:22.5739984
  Newtonsoft-dynamic   : 00:00:35.0931929
  Jil-dynamic          : 00:00:18.9215125
  Jynd-dynamic         : 00:00:06.2692387

Benchmark-name         : github-primes
Benchmark-iterations   : 100000
Benchmark-source       : https://gist.github.com/miguelmota/ffa20854b1258cd27d7e
  Newtonsoft-static    : 00:00:26.5607782
  Jil-static           : 00:00:06.9561507
  NetJSON-static       : 00:00:10.2512594
  ServiceStack-static  : 00:00:27.4852976
  Newtonsoft-dynamic   : 00:01:16.2362967
  Jil-dynamic          : 00:00:34.8085367
  Jynd-dynamic         : 00:00:13.9729942
````

## restrictions

* maximum json size: 64kB
* parsed text is valid json
* white characters between tokens are not accepted
* property name character escaping is not accepted
* numbers are deserialized currently only to Int32, Int64 or BigInteger
* deserialized object should be consumed before deserializing next one
* only json primitives; no datetime, no guid etc.

## contribution

* if something can be optimized, just suggest it with test case
* if you found bug, just report it with test case
* if something is not supported, just suggest it
* if you do not agree with published benchmarks, just suggest new one

## comparison

