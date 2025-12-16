using AdventOfCode.Services;

namespace AdventOfCode.Puzzles;

public class Day03
{
  public static int Part1(string filename = "example")
  {
    var banks = InputParserService.GetInputLines("03", filename);

    var joltage = 0;

    foreach (var bank in banks)
    {
      var firstBattery = "0";
      var firstBatteryIndex = 0;
      var secondBattery = "0";

      for (var i = 0; i < bank.Length; i++)
      {
        var battery = bank[i].ToString();

        if (i == 0)
        {
          firstBattery = battery;
          continue;
        }

        if (int.Parse(battery) > int.Parse(firstBattery) && i < bank.Length - 1)
        {
          firstBattery = battery;
          firstBatteryIndex = i;
          continue;
        }
      }

      for (var i = firstBatteryIndex + 1; i < bank.Length; i++)
      {
        var battery = bank[i].ToString();

        if (int.Parse(battery) > int.Parse(secondBattery))
          secondBattery = battery;
      }

      joltage += int.Parse(firstBattery + secondBattery);
    }

    return joltage;
  }

  public static ulong Part2(string filename = "example")
  {
    var banks = InputParserService.GetInputLines("03", filename);

    ulong joltage = 0L;

    foreach (var bank in banks)
    {
      var bankJoltage = "";
      var leftMostIndex = 0;

      for (var i = 12; i >= 1; i--)
      {
        string highestJoltageBattery = bank[^i].ToString();
        int highestJoltageBatteryIndex = bank.Length - i;

        for (var j = bank.Length - i; j >= leftMostIndex; j--)
        {
          var battery = bank[j].ToString();

          if (int.Parse(battery.ToString()) >= int.Parse(highestJoltageBattery.ToString()))
          {
            highestJoltageBattery = battery;
            highestJoltageBatteryIndex = j;
          }
        }

        bankJoltage += highestJoltageBattery;
        leftMostIndex = highestJoltageBatteryIndex + 1;
      }

      joltage += ulong.Parse(bankJoltage);
    }

    return joltage;
  }
}
