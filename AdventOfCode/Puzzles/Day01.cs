using AdventOfCode.Services;

namespace AdventOfCode.Puzzles;

public class Day01
{
  private static int Part1(string filename = "example")
  {
    var lines = InputParserService.GetInputLines("01", filename);

    var dialPosition = 50;
    var password = 0;

    foreach (var line in lines)
    {
      try
      {
        var (rotationDirection, rotationAmount) = (line[0], int.Parse(line[1..]));

        dialPosition =
          (
            (
              rotationDirection == 'L' ? dialPosition - rotationAmount
              : rotationDirection == 'R' ? dialPosition + rotationAmount
              : throw new FormatException("Invalid direction")
            ) % 100
            + 100
          ) % 100;

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
    var lines = InputParserService.GetInputLines("01", filename);

    var dialPosition = 50;
    var password = 0;

    foreach (var line in lines)
    {
      try
      {
        var (rotationDirection, rotationAmount) = (line[0], int.Parse(line[1..]));

        if (rotationDirection == 'R')
          password += (dialPosition + rotationAmount) / 100;
        else if (rotationDirection == 'L')
        {
          if (dialPosition == 0)
            password += rotationAmount / 100;
          else if (rotationAmount >= dialPosition)
            password += 1 + (rotationAmount - dialPosition) / 100;
        }
        else
          throw new FormatException("Invalid direction");

        dialPosition =
          (
            (
              rotationDirection == 'L' ? dialPosition - rotationAmount
              : rotationDirection == 'R' ? dialPosition + rotationAmount
              : throw new FormatException("Invalid direction")
            ) % 100
            + 100
          ) % 100;
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
}