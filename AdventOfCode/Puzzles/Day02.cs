using AdventOfCode.Services;

namespace AdventOfCode.Puzzles;

public class Day02
{
  public static readonly string Part1ExpectedValue = "1227775554";
  public static readonly string Part2ExpectedValue = "4174379265";

  public static ulong Part1(string filename = "example")
  {
    var line = InputParserService.GetInputLines("02", filename)[0];

    var ranges = line.Split(",");

    ulong invalidIdsSum = 0L;

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

  public static ulong Part2(string filename = "example")
  {
    var line = InputParserService.GetInputLines("02", filename)[0];

    var ranges = line.Split(",");

    ulong invalidIdsSum = 0L;

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
}
