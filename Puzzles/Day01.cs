#:sdk Microsoft.NET.Sdk

namespace Day01;

public static class InputParser
{
  public static string[] GetInputLines(string day, string filename)
  {
    var projectRootPath = Directory.GetCurrentDirectory();
    var inputFilePath = Path.Combine(projectRootPath, $"Inputs/day{day}", $"{filename}.txt");

    return File.ReadAllLines(inputFilePath);
  }
}

public class Day01
{
  private static int Part1(string filename = "example")
  {
    var lines = InputParser.GetInputLines("01", filename);

    var dial = Enumerable.Range(0, 100).ToArray();
    var dialPosition = 50;
    var password = 0;

    foreach (var line in lines)
    {
      try
      {
        var (rotationDirection, rotationAmount) = (line[0], int.Parse(line[1..]));

        dialPosition = dial[
          (
            (
              rotationDirection == 'L' ? dialPosition - rotationAmount
              : rotationDirection == 'R' ? dialPosition + rotationAmount
              : throw new FormatException("Invalid direction")
            ) % dial.Length
            + dial.Length
          ) % dial.Length
        ];

        if (dialPosition == 0)
          password++;
      }
      catch (Exception e)
      {
        if (e is ArgumentNullException or FormatException or OverflowException)
        {
          Console.WriteLine($"Failed to parse line: {line}");
          Console.WriteLine("Check your input file");
          break;
        }
        else
          throw;
      }
    }

    return password;
  }

  private static int Part2(string filename = "example")
  {
    var lines = InputParser.GetInputLines("01", filename);

    var dial = Enumerable.Range(0, 100).ToArray();
    var dialPosition = 50;
    var password = 0;

    foreach (var line in lines)
    {
      try
      {
        var (rotationDirection, rotationAmount) = (line[0], int.Parse(line[1..]));

        if (rotationDirection == 'R')
          password += (dialPosition + rotationAmount) / dial.Length;
        else if (rotationDirection == 'L')
        {
          if (dialPosition == 0)
            password += rotationAmount / dial.Length;
          else if (rotationAmount >= dialPosition)
            password += 1 + (rotationAmount - dialPosition) / dial.Length;
        }
        else
          throw new FormatException("Invalid direction");

        dialPosition = dial[
          (
            (
              rotationDirection == 'L' ? dialPosition - rotationAmount
              : rotationDirection == 'R' ? dialPosition + rotationAmount
              : throw new FormatException("Invalid direction")
            ) % dial.Length
            + dial.Length
          ) % dial.Length
        ];
      }
      catch (Exception e)
      {
        if (e is ArgumentNullException or FormatException or OverflowException)
        {
          Console.WriteLine($"Failed to parse line: {line}");
          Console.WriteLine("Check your input file");
          break;
        }
        else
          throw;
      }
    }

    return password;
  }

  public static void Main()
  {
    Console.WriteLine("[[Part 1]]");
    var example1 = Part1();
    Console.WriteLine($"Example Answer: {example1}. Expected: 3");
    var solution1 = Part1("input");
    Console.WriteLine($"Problem Answer: {solution1}"); //=> 1123

    Console.WriteLine("\n...---...\n");

    Console.WriteLine("[[Part 2]]");
    var example2 = Part2();
    Console.WriteLine($"Example Answer: {example2}. Expected: 6");
    var solution2 = Part2("input");
    Console.WriteLine($"Problem Answer: {solution2}"); //=> 6695
  }
}
