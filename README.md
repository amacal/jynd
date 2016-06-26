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
  Newtonsoft-static    : 00:00:19.6870647
  Jil-static           : 00:00:04.7588724
  NetJSON-static       : 00:00:04.9923150
  ServiceStack-static  : 00:00:16.0720547
  Newtonsoft-dynamic   : 00:00:34.5271513
  Jil-dynamic          : 00:00:14.5876434
  Jynd-dynamic         : 00:00:04.4941863

Benchmark-name         : github-team
Benchmark-iterations   : 1000000
Benchmark-source       : https://developer.github.com/v3/orgs/teams/
  Newtonsoft-static    : 00:00:18.9402005
  Jil-static           : 00:00:08.5502625
  NetJSON-static       : 00:00:10.8937298
  ServiceStack-static  : 00:00:22.4206923
  Newtonsoft-dynamic   : 00:00:33.9639888
  Jil-dynamic          : 00:00:18.6202631
  Jynd-dynamic         : 00:00:06.1832675

Benchmark-name         : github-primes
Benchmark-iterations   : 100000
Benchmark-source       : https://gist.github.com/miguelmota/ffa20854b1258cd27d7e
  Newtonsoft-static    : 00:00:26.5731686
  Jil-static           : 00:00:06.6055105
  NetJSON-static       : 00:00:09.3850718
  ServiceStack-static  : 00:00:26.4729244
  Newtonsoft-dynamic   : 00:01:14.6578668
  Jil-dynamic          : 00:00:34.5503797
  Jynd-dynamic         : 00:00:13.1697605
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

