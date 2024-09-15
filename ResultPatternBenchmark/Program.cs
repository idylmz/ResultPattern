// See https://aka.ms/new-console-template for more information


using BenchmarkDotNet.Running;
using ResultPatternBenchmark;

BenchmarkRunner.Run<ResultPatternAndExceptionBenchmark>();

Console.WriteLine("Press any key to continue...");
Console.ReadKey();