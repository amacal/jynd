# jynd

A library aims to deserialize json structure without being json deserializer.

## usage

```` csharp
string json = "{'firstName':'John','lastName':'Smith','isAlive':true,'age':25}";
string text = json.Replace('\'', '\"');
dynamic instance = JyndConvert.Deserialize(text);

if (instance.age > 18)
{
   Console.WriteLint($"first-name: {instance.firstName}");
   Console.WriteLint($"last-name : {instance.lastName}");
}
````

## benchmark

```` text
Newtonsoft-static  : 00:00:08.8787914
Newtonsoft-dynamic : 00:00:15.4254987
Jil-static         : 00:00:02.2103270
Jil-dynamic        : 00:00:06.6172323
NetJSON-static     : 00:00:02.4847555
Jynd-dynamic       : 00:00:02.4997822
````

## restrictions

* white characters between tokens are not accepted
* property name character escaping is not accepted
* numeric are deserialized currently only as positive integers
* only object a a root

## comparison

