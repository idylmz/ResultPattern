using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace ResultPatternBenchmark;

[HardwareCounters(HardwareCounter.TotalCycles, HardwareCounter.BranchMispredictions)]
public class ResultPatternAndExceptionBenchmark {
    private const int IterationCount = 1000;

    [Params(null, "", "short", "this is a longer string input")]
    public string Parameter { get; set; }

    [Benchmark]
    public void ThrowExceptionIfNull() {
        for (int i = 0; i < IterationCount; i++) {
            try {
                var result = ProcessWithException(Parameter);

                // Do something with result to prevent optimization
                _ = result.Status;
            } catch (ArgumentNullException) {
                // Expected exception, do nothing
            }
        }
    }

    [Benchmark]
    public void SendResultObject() {
        for (int i = 0; i < IterationCount; i++) {
            var result = ProcessWithResultObject(Parameter);

            // Do something with result to prevent optimization
            _ = result.Status;
        }
    }

    private ResultObject ProcessWithException(string input) {
        if (string.IsNullOrEmpty(input)) {
            throw new ArgumentNullException(nameof(input));
        }
        return new ResultObject(200, "Success", input);
    }

    private ResultObject ProcessWithResultObject(string input) {
        if (string.IsNullOrEmpty(input)) {
            return new ResultObject(400, "Value cannot be null or empty", "");
        }
        return new ResultObject(200, "Success", input);
    }
}

public record ResultObject(int Status, string Message, object Data);