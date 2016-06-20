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
Newtonsoft-static  : 00:00:19.3816103
Newtonsoft-dynamic : 00:00:33.4836211
Jil-static         : 00:00:04.7730814
Jil-dynamic        : 00:00:15.0509291
NetJSON-static     : 00:00:04.8423103
Jynd-dynamic       : 00:00:05.3660046
````

## restrictions

* white characters between tokens are not accepted
* property name character escaping is not accepted
* numeric are deserialized currently only as positive integers
* deserialized object should be consumed before deserializing next one
* only object a a root

## comparison

