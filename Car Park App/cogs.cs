using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using System.Collections;

public class cogs
{
    internal static (Dictionary<int, int>, string[], Dictionary<int, float>) DisplayMenu()
    {
        Dictionary<int, int> dictionary = new Dictionary<int, int>(); //This upcoming section is making the dictionary from the menu.txt, so that any modifications made in the "management mode" will be saved for when the program loads up again.
        List<string> lines = null;

        try
        {
            lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "menu.txt")).ToList();
        }
        catch
        {
            Console.WriteLine("Could not find menu.txt, please create one.");
            Environment.Exit(400);
        }

        Dictionary<int, float> timesDict = new Dictionary<int, float>();
        List<string> times = new List<string>();    
        int count1 = 0;

        foreach (string line in lines ?? new List<string>())
        {
            count1++;
            string[] parts = line.Split('=');
            try
            {
                times.Add(parts[0]);
                dictionary.Add(count1, int.Parse(parts[1]));
                timesDict.Add(count1, float.Parse(parts[2]));
            }
            catch
            {
                return (null, null, null);
            }

            string menu_before_spaces = string.Concat("{0}      {1}£{2}", count1.ToString(), parts[0], parts[1]);
            int Length = string.Concat("{0}      {1}£{2}", count1.ToString(), parts[0], parts[1]).Length;

            int number_of_spaces = 40 - Length;
            string spaces = "";
            for (int i = 0; i < number_of_spaces; i++)
            {
                spaces += " ";
            }

            Console.WriteLine("{0}      {1}{2}£{3}", count1, parts[0], spaces, parts[1]);
        }


        Console.WriteLine("\nPlease select duration:       (Press number 1-" + dictionary.Count + ")\n");
        return (dictionary, times.ToArray(), timesDict);
    }
    internal static void PrintTicket(string registration, float fee, float change, float time) //needs expiry and date entered etc
    {
        Console.Clear();
        Console.Write("Printing ticket");
        for (int i = 0;i <= 3;i++)
        {
            Thread.Sleep(400);
            Console.Write(". ");
        }
        Console.Write("\n");
        Console.BackgroundColor = ConsoleColor.White;
        string[] ticket_text = TicketTextGen(registration, fee, change, time);
        int longest_line = 0;
        foreach (string line in ticket_text)
        {
            if (line.Length > longest_line)
            {
                longest_line = line.Length;
            }
        }
        foreach (string line in ticket_text)
        {
            Thread.Sleep(200);
            string modifiedLine = line;
            while (modifiedLine.Length < longest_line)
            {
                modifiedLine += " ";
            }
            Console.WriteLine(modifiedLine);
        }
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.WriteLine("Press any key to exit..."); Console.ReadKey();

    }

    internal static string[] TicketTextGen(string registration, float fee, float change, float time)
    {
        DateTime timenow = DateTime.Now;
        DateTime expirytime = timenow.AddHours(time);
        string text = "                             \n  B O B ' S  C A R  P A R K  \n\n\nRegistration: " + registration + "\n\nEntry Time: " + DateTime.Now + "\n\nFee: " + fee.ToString("C2") + "\n\nValid Until: " + expirytime + "\n\nHave a nice day!"; return text.Split('\n');
    }

    internal static void saveTicket(string registration, float fee, float time)
    {
        StreamWriter sw;
        if (!File.Exists("ticket_log.txt"))
        {
            sw = File.CreateText("ticket_log.txt");
        }
        else
        {
            sw = File.AppendText("ticket_log.txt");

        }
        sw.WriteLine("This is the content of the new file.");
        sw.Close()
    }
}
