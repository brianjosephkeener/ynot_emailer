using System;
using System.Text.RegularExpressions;

class DateRange
{
    public string date { get; set; }
  public DateRange() {

    string regex = @"^(0[1-9]|1[0-2])\/(0[1-9]|[12][0-9]|3[01])\/\d{4}$";

    while (true)
    {
        Console.WriteLine("\nEnter beginning date range in this format MM/DD/YYYY");
        string? begin = Console.ReadLine();
        if(!Regex.IsMatch(begin, regex))
        {
            continue;
        }
        Console.WriteLine("\nEnter end date range in this format MM/DD/YYYY");
        string? end = Console.ReadLine();        
        if(!Regex.IsMatch(end, regex))
        {
            continue;
        }
        date = $"{begin} to {end}";
        break;
    }
  }
}