using System;
using System.Timers;
using System.IO;

public class Example
{
    public static System.Timers.Timer aTimer;
    public static string path = @"D:\2022. Visual Studio\folder1\";

    public static void Main()
    {
        SetTimer();

        Console.WriteLine("\nPress the Enter key to exit the application...\n");
        Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
        Console.ReadLine();
        aTimer.Stop();

        CountFile();

        aTimer.Dispose();

        Console.WriteLine("Terminating the application...");
    }

    private static void SetTimer()
    {
        aTimer = new System.Timers.Timer(1000);
        aTimer.Elapsed += OnTimedEvent;
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
    }

    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        CreateFile();
        Console.WriteLine("Create file at {0:HH:mm:ss.fff}",
                          e.SignalTime);
    }


    private static void CreateFile()
    {
        if(Directory.Exists(path))
        {
            for (int i = 0; i < 100; i++)
            {
                string fileName = string.Format("MyFile_" + i + "_{0:HH-mm-ss.fff}" + ".txt", DateTime.Now);
                var newPath = path + fileName;

                if (!File.Exists(newPath))
                {
                    using (StreamWriter sw = File.CreateText(newPath));
                }
            }
        }
    }

    public static void CountFile()
    {
        int fileCount = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Length;
        Console.WriteLine("The number of files in {0} : {1}", path, fileCount);
    }
}
