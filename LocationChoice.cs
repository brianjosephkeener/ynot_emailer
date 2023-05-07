using System;
using System.Collections.Generic;

class CurrentLeadStatus
{
    public List<string> StatusAvailable { get; set; }
    public List<string> StatusSelected { get; set; }
    
    public CurrentLeadStatus()
    {
        StatusAvailable = new List<string>
        {
            "Appt No Show",
            "Appt No Show AC",
            "Cancel",
            "Contacted",
            "Interviewed",
            "Withdrawal",
            "Graduate",
            "Appointment Set",
            "Lead"
        };

        StatusSelected = new List<string>();

        Console.WriteLine("Enter the statuses you wish to select. Press Enter without typing anything to finish.");

        while (true)
        {
            Console.WriteLine("\nAvailable statuses:");
            for (int i = 0; i < StatusAvailable.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {StatusAvailable[i]}");
            }

            Console.Write("\nEnter the number of the statuses you wish to add: ");
            string input = Console.ReadLine();

            // Check for exit command
            if (input == "")
            {
                break;
            }

            // Parse input as an integer
            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            // Check if choice is valid
            if (choice < 1 || choice > StatusAvailable.Count)
            {
                Console.WriteLine("Invalid choice. Please try again.");
                continue;
            }

            // Add selected program to ProgramsSelected list and remove it from ProgramsAvailable list
            string selectedProgram = StatusAvailable[choice - 1];
            StatusSelected.Add(selectedProgram);
            StatusAvailable.Remove(selectedProgram);
        }

        Console.WriteLine("\nSelected statuses:");
        foreach (string program in StatusSelected)
        {
            Console.WriteLine(program);
        }
    }
}