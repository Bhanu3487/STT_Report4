using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace ConsoleAlarmApp 
{
    class Program
    {
        public static event Action? RaiseAlarm;

        static void Main(string[] args)
        {
            // --- Subscriber ---
            // Subscribe the handler method to the event.
            RaiseAlarm += RingAlarm; // Using PascalCase for method name

            Console.WriteLine("Welcome to the Console Alarm Clock!");
            Console.WriteLine("---------------------------------");
            Console.Write("Enter the alarm time in HH:MM:SS format (24-hour): ");

            string? inputTime = Console.ReadLine(); // Use nullable string

            // Validate input format and handle null input
            if (string.IsNullOrEmpty(inputTime) || !IsValidTimeFormat(inputTime))
            {
                Console.ForegroundColor = ConsoleColor.Red; // Add color for emphasis
                Console.WriteLine("\nError: Invalid time format entered!");
                Console.WriteLine("Please use HH:MM:SS format where:");
                Console.WriteLine("  - HH is hours 00-23");
                Console.WriteLine("  - MM is minutes 00-59");
                Console.WriteLine("  - SS is seconds 00-59");
                Console.WriteLine("Example: 14:30:00");
                Console.ResetColor(); // Reset color
                Console.WriteLine("\nPress any key to exit.");
                Console.ReadKey();
                return; // Exit application
            }

            // Parse validated input to TimeSpan
            TimeSpan alarmTime = TimeSpan.Parse(inputTime);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nAlarm set successfully for: {alarmTime:hh\\:mm\\:ss}"); // Format TimeSpan output
            Console.ResetColor();
            Console.WriteLine("Monitoring system time... (Press Ctrl+C to exit early)");

            // Check time every second
            try 
            {
                Console.CursorVisible = false; // Hide cursor during monitoring loop
                while (true)
                {
                    DateTime now = DateTime.Now;
                    // Truncate milliseconds/ticks for accurate comparison to the second
                    TimeSpan currentTimeOfDay = TimeSpan.FromSeconds(Math.Floor(now.TimeOfDay.TotalSeconds));


                    // Compare truncated current time with the target alarm time
                    if (currentTimeOfDay == alarmTime)
                    {
                        RaiseAlarm?.Invoke(); // Trigger the event (call subscribers)
                        break; // Exit the loop once alarm is raised
                    }

                    // Wait for approximately 1 second before checking again
                    Thread.Sleep(1000);
                }
            }
            finally // This block always executes, even if Ctrl+C is pressed
            {
                Console.CursorVisible = true; // Restore cursor visibility
            }


            Console.WriteLine("\nAlarm finished. Press any key to exit.");
            Console.ReadKey();
        }

        static bool IsValidTimeFormat(string time)
        {
            
            string pattern = @"^(?:[01]\d|2[0-3]):[0-5]\d:[0-5]\d$";
            return Regex.IsMatch(time, pattern);
        }


        static void RingAlarm() // PascalCase naming convention
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n*********************************");
            Console.WriteLine("*      ALARM! Time Reached!     *");
            Console.WriteLine("*********************************");
            Console.ResetColor();
            Console.Beep(1000, 500); // Frequency 1000Hz, Duration 500ms
        }
    }
}