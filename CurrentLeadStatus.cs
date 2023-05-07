using System;
using System.Collections.Generic;

class LocationChoice
{
    public List<string> LocationAvailable { get; set; }
    public List<string> LocationSelected { get; set; }
    
    public LocationChoice()
    {
        LocationAvailable = new List<string>
        {
            "Deland",
            "Kissimmee",
            "Lakeland",
            "Orlando",
            "Pembroke Pines",
            "South Miami",
            "Online",
            "Tampa"
        };

        LocationSelected = new List<string>();

        Console.WriteLine("Enter the locations you wish to select. Press Enter without typing anything to finish.");

        while (true)
        {
            Console.WriteLine("\nAvailable locations:");
            for (int i = 0; i < LocationAvailable.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {LocationAvailable[i]}");
            }

            Console.Write("\nEnter the number of the locations you wish to add: ");
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
            if (choice < 1 || choice > LocationAvailable.Count)
            {
                Console.WriteLine("Invalid choice. Please try again.");
                continue;
            }

            // Add selected program to ProgramsSelected list and remove it from ProgramsAvailable list
            string selectedProgram = LocationAvailable[choice - 1];
            LocationSelected.Add(selectedProgram);
            LocationAvailable.Remove(selectedProgram);
        }

        Console.WriteLine("\nSelected locations:");
        foreach (string program in LocationSelected)
        {
            Console.WriteLine(program);
        }
    }
}