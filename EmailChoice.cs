using System;
using System.Text.RegularExpressions;

class EmailChoice
{
    public string selection { get; set; }
  public EmailChoice() {
      Console.WriteLine("\nPick one email template to send");
      string[] availableChoices = {
        // "I'm here to help!", // fix "I'm here to help!"
        "FTC Intro English",
        "Fact Sheet - Information Technology",
        "Fact Sheet - FTC-Computer-Support-Technician-Diploma",
        "Fact Sheet - Bachelors Business Administration",
        "Fact Sheet - Business Office Specialist Diploma",
        "Fact Sheet - Medical Assistant Technician Diploma",
        "Fact Sheet Medical Billing and Coding Specialist Diploma",
        "Fact Sheet - Allied Health"
      };
      for (int i = 0; i < availableChoices.Length; i++)
      {
        Console.WriteLine($"({i+1}): " + availableChoices[i]);
      }
      selection = availableChoices[Int32.Parse(Console.ReadLine())-1];
    }
}
