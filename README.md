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
  Newtonsoft-static    : 00:00:19.3741938
  Jil-static           : 00:00:04.6296146
  NetJSON-static       : 00:00:05.0305041
  ServiceStack-static  : 00:00:15.8244757
  Newtonsoft-dynamic   : 00:00:36.4604436
  Jil-dynamic          : 00:00:14.3232309
  Jynd-dynamic         : 00:00:05.7046550

Benchmark-name         : github-team
Benchmark-iterations   : 1000000
Benchmark-source       : https://developer.github.com/v3/orgs/teams/
  Newtonsoft-static    : 00:00:19.8315966
  Jil-static           : 00:00:08.5032669
  NetJSON-static       : 00:00:10.8390656
  ServiceStack-static  : 00:00:22.4335956
  Newtonsoft-dynamic   : 00:00:37.3714015
  Jil-dynamic          : 00:00:18.4934840
  Jynd-dynamic         : 00:00:06.7208751

Benchmark-name         : github-primes
Benchmark-iterations   : 100000
Benchmark-source       : https://gist.github.com/miguelmota/ffa20854b1258cd27d7e
  Newtonsoft-static    : 00:00:26.7375319
  Jil-static           : 00:00:06.6424973
  NetJSON-static       : 00:00:09.4769776
  ServiceStack-static  : 00:00:26.2847179
  Newtonsoft-dynamic   : 00:01:11.7011914
  Jil-dynamic          : 00:00:34.2800611
  Jynd-dynamic         : 00:00:14.6937334

Benchmark-name         : fixer-euro
Benchmark-iterations   : 1000000
Benchmark-source       : http://fixer.io/
  Newtonsoft-static    : 00:00:31.2229801
  Jil-static           : 00:00:09.2266034
  NetJSON-static       : 00:00:18.8113365
  ServiceStack-static  : 00:00:24.9754502
  Newtonsoft-dynamic   : 00:00:52.3912384
  Jil-dynamic          : 00:00:20.0414449
  Jynd-dynamic         : 00:00:04.8547517
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

