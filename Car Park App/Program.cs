using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using System.Collections;
using static cogs;


namespace Car_Park_App
{
    internal class Program
    {
        static void Reset() //This is just a class to make the rest of the code a little more clear
        {
            Console.Clear();
            Console.WriteLine("####################################################\n\n  W E L C O M E   T O   B O B ' S   C A R   P A R K\n\n####################################################");
        }

        static void Main()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black; // It is inefficient to add this to Reset
            Reset();

            Thread.Sleep(500); //Pausescode execution for 1000 milliseconds
            string reg_plate;
            while (true) // Simple Validation
            {
                Console.WriteLine("\nPlease enter the car registration: ");
                reg_plate = Console.ReadLine().ToUpper();
                Console.WriteLine("Registration entered: " +reg_plate+" \n\nIs this correct? (y/n)");
                try
                {
                    char reg_plate_confimation = char.Parse(Console.ReadLine());
                    if (reg_plate_confimation == 'y')
                    {
                        break;
                    }
                    Thread.Sleep(1000);
                }
                catch
                {
                    Console.WriteLine("\nInvalid data entered.");
                    Thread.Sleep(1000);
                }
            }
            Reset(); //Clears screen ready for the parking options

            (Dictionary<int, int>, string[], Dictionary<int, float>) menu_tuple = cogs.DisplayMenu();
            if (menu_tuple == (null, null, null))
            {
                Console.WriteLine("There was an error with the menu properties file, contact an administrator immediately.");
            }
            Dictionary <int, int> dictionary = menu_tuple.Item1;
            string[] times = menu_tuple.Item2;
            Dictionary<int, float> timesDict = menu_tuple.Item3;

            int choice = 0;

            while (true)
            {
                try
                {
                    choice = int.Parse(Console.ReadLine());
                    if (choice < 1 || choice > dictionary.Count)
                    {
                        Console.WriteLine("Invalid entry, please enter a valid option (1-" + dictionary.Count + ")");
                    }
                    else
                    {
                        Reset();
                        Console.WriteLine("{0} selected. ", times[choice-1]);
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid entry, please enter a valid option (1-" + dictionary.Count + ")");
                }
            }

            float price = dictionary[choice]; float length_of_ticket = timesDict[choice]; float due = price;
            float paid = 0; float total_paid = 0;

            while (true)
            {
                Console.WriteLine("Amount Due: {0:C2}\nPlease enter insert coins below.", due);
                try
                {
                    paid = float.Parse(Console.ReadLine()); //this is for this round of the loop
                    total_paid += paid;

                }
                catch
                {
                    Console.WriteLine("Invalid coin amount entered (symbol not necessary, please try again. (Press any key to retry)");
                    Console.ReadKey();
                    Reset(); continue;
                }

                Console.WriteLine("Amount Entered: {0:C2}", price);
                if (price - total_paid <= 0)
                {
                    Console.WriteLine("Amount inserted in total: {0:C2}", total_paid);
                    Thread.Sleep(2000);
                    Console.WriteLine("Change needed: {0:C2}", (price - total_paid) * -1);
                    Thread.Sleep(2000);
                    cogs.PrintTicket(reg_plate, price, total_paid, length_of_ticket);
                    Main();
                }
                else
                {
                    due -= paid;
                    Reset();
                }
            }




        }
    }
}
