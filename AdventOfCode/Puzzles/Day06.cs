using AdventOfCode.Services;

namespace AdventOfCode.Puzzles;

public class Day06
{
  public static readonly string Part1ExpectedValue = "4277556";
  public static readonly string Part2ExpectedValue = "3263827";

  private static readonly char SUM = '+';
  private static readonly char MULT = '*';

  public static ulong Part1(string filename = "example")
  {
    var lines = InputParserService.GetInputLines("06", filename);

    List<List<ulong>> columns = [];
    char[] operations = [.. lines[^1].Split(" ").Where(l => l != "").Select(char.Parse)];

    for (var i = 0; i < lines.Length - 1; i++)
    {
      var line = lines[i];

      var lineColumns = line.Split(" ").Where(c => c != "").ToArray();

      for (var j = 0; j < lineColumns.Length; j++)
      {
        var item = ulong.Parse(lineColumns[j]);

        if (columns.Count == j)
        {
          columns.Add([item]);
          continue;
        }

        columns[j].Add(item);
      }
    }

    ulong sum = 0L;

    for (var i = 0; i < columns.Count; i++)
    {
      var column = columns[i];
      var operation = operations[i];

      ulong sumSeed = 0L;
      sum += column.Aggregate(
        sumSeed,
        (acc, v) =>
          operation == SUM ? acc + v
          : operation == MULT ? (acc == 0 ? 1 : acc) * v
          : throw new Exception("Invalid Operation")
      );
    }

    return sum;
  }

  public static ulong Part2(string filename = "example")
  {
    var lines = InputParserService.GetInputLines("06", filename);

    var numbersLines = lines[0..^1];
    var operationsLine = lines[^1];

    ulong sum = 0L;

    List<List<ulong>> columns = [];
    char[] operations = [.. lines[^1].Split(" ").Where(l => l != "").Select(char.Parse)];

    var curColumn = -1;
    for (var i = 0; i < operationsLine.Length; i++)
    {
      var character = operationsLine[i];

      if (character == SUM || character == MULT)
      {
        curColumn++;
      }

      List<int> curNumberList = [];
      foreach (var numberLine in numbersLines)
      {
        var curChar = numberLine[i];

        if (curChar != ' ')
        {
          curNumberList.Add(int.Parse(curChar.ToString()));
        }
      }

      if (curNumberList.Count > 0)
      {
        var columnNumber = ulong.Parse(curNumberList.Aggregate("", (acc, v) => acc + v));

        if (columns.Count == curColumn)
        {
          columns.Add([columnNumber]);
          continue;
        }

        columns[curColumn].Add(columnNumber);
      }
    }

    for (var i = 0; i < columns.Count; i++)
    {
      var column = columns[i];
      var operation = operations[i];

      ulong sumSeed = 0L;
      sum += column.Aggregate(
        sumSeed,
        (acc, v) =>
          operation == SUM ? acc + v
          : operation == MULT ? (acc == 0 ? 1 : acc) * v
          : throw new Exception("Invalid Operation")
      );
    }

    return sum;
  }
}
