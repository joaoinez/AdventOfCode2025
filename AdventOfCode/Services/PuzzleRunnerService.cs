using System.Reflection;

namespace AdventOfCode.Services;

public static class PuzzleRunnerService
{
  private static void Run<TDay>(int part, bool useExample = true)
  {
    var filename = useExample ? "example" : "input";
    var methodName = $"Part{part}";
    var partLabel = $"[[Part {part}]]";
    var method =
      typeof(TDay).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static)
      ?? throw new InvalidOperationException(
        $"Method {methodName} not found on {typeof(TDay).Name}"
      );

    Console.WriteLine(partLabel);

    var result = method.Invoke(null, [filename]);
    var label = useExample ? "Example" : "Problem";
    var fieldName = $"Part{part}ExpectedValue";
    var expectedValue =
      typeof(TDay).GetField(fieldName, BindingFlags.Public | BindingFlags.Static)
      ?? throw new InvalidOperationException($"Field {fieldName} not found on {typeof(TDay).Name}");
    ;
    var line =
      $"{label} Answer: {result}"
      + (useExample ? $". Expected: {expectedValue.GetValue(null)}" : "");

    Console.WriteLine(line);
  }

  public static void Part1Example<TDay>() => Run<TDay>(1, useExample: true);

  public static void Part1<TDay>() => Run<TDay>(1, useExample: false);

  public static void Part2Example<TDay>() => Run<TDay>(2, useExample: true);

  public static void Part2<TDay>() => Run<TDay>(2, useExample: false);
}
