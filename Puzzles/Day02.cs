#:sdk Microsoft.NET.Sdk

namespace Day02;

public static class InputParser
{
  public static string[] GetInputLines(string day, string filename)
  {
    var projectRootPath = Directory.GetCurrentDirectory();
    var inputFilePath = Path.Combine(projectRootPath, $"Inputs/day{day}", $"{filename}.txt");

    return File.ReadAllLines(inputFilePath);
  }
}

public class Day02
{
  private static ulong Part1(string filename = "example")
  {
    var line = InputParser.GetInputLines("02", filename)[0];

    var ranges = line.Split(",");

    ulong invalidIdsSum = 0;

    foreach (var range in ranges)
    {
      var rangeStartAndEnd = range.Split("-");
      var rangeStart = ulong.Parse(rangeStartAndEnd[0]);
      var rangeEnd = ulong.Parse(rangeStartAndEnd[1]);

      for (var id = rangeStart; id <= rangeEnd; id++)
      {
        var stringId = id.ToString();
        var isOdd = stringId.ToString().Length % 2 != 0;
        if (isOdd)
          continue;

        var firstHalf = stringId[0..(stringId.Length / 2)];
        var secondHalf = stringId[(stringId.Length / 2)..stringId.Length];

        if (firstHalf == secondHalf)
          invalidIdsSum += id;
      }
    }

    return invalidIdsSum;
  }

  private static ulong Part2(string filename = "example")
  {
    var line = InputParser.GetInputLines("02", filename)[0];

    var ranges = line.Split(",");

    ulong invalidIdsSum = 0;

    static bool IsIdInvalid(string id, int length)
    {
      if (length == 1)
        return false;
      if (id.Length % length != 0)
        return IsIdInvalid(id, length - 1);

      var isIdInvalid = false;
      var subString = id[..(id.Length / length)];
      var virtualId = string.Concat(Enumerable.Repeat(subString, length));

      isIdInvalid = virtualId == id;

      if (isIdInvalid)
        return true;

      if (length == id.Length)
        return IsIdInvalid(id, length / 2);

      return IsIdInvalid(id, length - 1);
    }

    foreach (var range in ranges)
    {
      var rangeStartAndEnd = range.Split("-");
      var rangeStart = ulong.Parse(rangeStartAndEnd[0]);
      var rangeEnd = ulong.Parse(rangeStartAndEnd[1]);

      for (var id = rangeStart; id <= rangeEnd; id++)
      {
        var isIdInvalid = IsIdInvalid(id.ToString(), id.ToString().Length);

        if (isIdInvalid)
        {
          invalidIdsSum += id;
        }
      }
    }

    return invalidIdsSum;
  }

  public static void Main()
  {
    Console.WriteLine("[[Part 1]]");
    var example1 = Part1();
    Console.WriteLine($"Example Answer: {example1}");
    var solution1 = Part1("input");
    Console.WriteLine($"Problem Answer: {solution1}");

    Console.WriteLine("\n...---...\n");

    Console.WriteLine("[[Part 2]]");
    var example2 = Part2();
    Console.WriteLine($"Example Answer: {example2}");
    var solution2 = Part2("input");
    Console.WriteLine($"Problem Answer: {solution2}");
  }
}
