﻿

using FetchAllPaginated;

Collector apiDataCollector = new Collector();
Console.WriteLine("==== Parallel ====");
DateTime curTime = DateTime.Now;
PrintDictionary(apiDataCollector.FetchAllApiDataParallel(5));
Console.WriteLine($"Time spent : {(DateTime.Now - curTime).TotalMilliseconds} ms");

curTime = DateTime.Now;
Console.WriteLine("==== Sequential ====");
PrintDictionary(apiDataCollector.FetchAllApiDataSequential(50));
Console.WriteLine($"Time spent : {(DateTime.Now - curTime).TotalMilliseconds} ms");
Console.ReadLine();
void PrintDictionary(Dictionary<int, string> dictionary)
{
    foreach(var item in dictionary) Console.WriteLine(item);
}
