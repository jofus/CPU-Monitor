using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Speech.Synthesis;

namespace CPUMonitor
{
    class Program
    {
        private static SpeechSynthesizer synth = new SpeechSynthesizer();

        /// <summary>
        ///  Where the magic happens
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            List<string> cpuMaxedOutMessages = new List<string>();
            cpuMaxedOutMessages.Add("WARNING! your CPU is about to catch fire!");
            cpuMaxedOutMessages.Add("RED ALERT RED ALERT RED ALERT RED ALERT CPU BURN!");
            cpuMaxedOutMessages.Add("WATER WATER, I NEED WATER!");

            // The dice!
            Random rand = new Random();

            #region My Performance Counters
            // This will pull the current CPU load in percentage
            PerformanceCounter perfCpuCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");

            // This will pull the current available memory in megabytes
            PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available MBytes");


            // This will get the system up time in seconds
            PerformanceCounter perfUptimeCount = new PerformanceCounter("System", "System Up Time");
            #endregion

            TimeSpan uptimeSpan = TimeSpan.FromSeconds(perfUptimeCount.NextValue());
            string systemUptimeMessage = string.Format("The current CPU up time is {0} days {1} hours {2} minutes {3} seconds",
                (int)uptimeSpan.TotalDays,
                (int)uptimeSpan.Hours,
                (int)uptimeSpan.Minutes,
                (int)uptimeSpan.Seconds
            );

            // tell the user what the current system uptime is

            while (true)
            {   
                // Get the current performance counter values
                int currentCpuPercentage = (int)perfCpuCount.NextValue();
                int currentAvailableMemory = (int)perfMemCount.NextValue();

                // every 1second print the load in percentage to the screen
                Console.WriteLine("CPU Load        : {0}%", currentCpuPercentage);
                Console.WriteLine("Available Memory: {0}MB", currentAvailableMemory);
                 
                // only tell us when the cpu usage is above 80%
                if ( currentCpuPercentage > 80 )
                
                    if (currentCpuPercentage == 100)
                    {
                    
                        string cpuLoadVocalMessage = cpuMaxedOutMessages[rand.Next(4)];
                                                                                                                                                                                                      }
                    else
                    {
                        string cpuLoadVocalMessage = String.Format("The current CPU load is {0} percent", currentCpuPercentage);
                    }
                

                // only tell us if memory is below one gigabyte
                if (currentAvailableMemory < 1024)
                {
                    // speak to the user to tell them what the current values are
                    string memAvailableVocalMessage = String.Format("You currently have {0} megabytes of memory available");
                }

               
    
                Thread.Sleep(1000);
                // end of loop
            }
        }

    }
}

