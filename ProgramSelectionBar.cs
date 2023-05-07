using System;
using System.Collections.Generic;

class ProgramSelectionBar
{
    public List<string> ProgramsAvailable { get; set; }
    public List<string> ProgramsSelected { get; set; }
    
    public ProgramSelectionBar()
    {
        ProgramsAvailable = new List<string>
        {
            "Medical Billing and Coding Specialist - Diploma",
            "Medical Assistant Technician",
            "Allied Health Management",
            "Computer Support Technician",
            "Business Office Specialist",
            "Information Technology with Emphasis in Cyber Security",
            "Business Administration"
        };

        ProgramsSelected = new List<string>();

        Console.WriteLine("Enter the programs you wish to select. Press Enter without typing anything to finish.");

        while (true)
        {
            Console.WriteLine("\nAvailable programs:");
            for (int i = 0; i < ProgramsAvailable.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {ProgramsAvailable[i]}");
            }

            Console.Write("\nEnter the number of the program you wish to add: ");
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
            if (choice < 1 || choice > ProgramsAvailable.Count)
            {
                Console.WriteLine("Invalid choice. Please try again.");
                continue;
            }

            // Add selected program to ProgramsSelected list and remove it from ProgramsAvailable list
            string selectedProgram = ProgramsAvailable[choice - 1];
            ProgramsSelected.Add(selectedProgram);
            ProgramsAvailable.Remove(selectedProgram);
        }

        Console.WriteLine("\nSelected programs:");
        foreach (string program in ProgramsSelected)
        {
            Console.WriteLine(program);
        }
    }
}