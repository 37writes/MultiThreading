using System;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace MultiThreading
{
    class Program
    {
        public static System.Timers.Timer createTimer;
        public static System.Timers.Timer countTimer;
        public static System.Timers.Timer moniteringTimer;

        public static string path1 = @"D:\2022. Visual Studio\folder1\";
        public static string path2 = @"D:\2022. Visual Studio\folder2\";

        private static void SetTimer_CreateFile()
        {
            createTimer = new System.Timers.Timer(1000);
            createTimer.Elapsed += OnTimedEvent;
            createTimer.AutoReset = true;
            createTimer.Enabled = true;
        }
        private static void OnTimedEvent(Object sender, EventArgs e)
        {
            CreateFile();
        }
        public static void CreateFile()
        {
            if (Directory.Exists(path1))
            {
                for (int i = 0; i < 10; i++)
                {
                    string fileName = string.Format("MyFile_" + i + "_{0:HH-mm-ss.fff}" + ".txt", DateTime.Now);
                    var newPath1 = path1 + fileName;
                    if (!File.Exists(newPath1))
                    {
                        using (StreamWriter sw = File.CreateText(newPath1));
                    }
                }
                Console.WriteLine("Create files in folder1");
            }
            Thread.Sleep(1000);
            if (Directory.Exists(path2))
            {
                for (int i = 0; i < 10; i++)
                {
                    string fileName = string.Format("MyFile_" + i + "_{0:HH-mm-ss.fff}" + ".txt", DateTime.Now);
                    var newPath2 = path2 + fileName;
                    if (!File.Exists(newPath2))
                    {
                        using (StreamWriter sw = File.CreateText(newPath2));
                    }
                }
                Console.WriteLine("Create files in folder2");
            }

        }
        private static void SetTimer_CountFile()
        {
            countTimer = new System.Timers.Timer(1000);
            countTimer.Elapsed += OnTimedEvent2;
            countTimer.AutoReset = true;
            countTimer.Enabled = true;
        }
        private static void OnTimedEvent2(Object sender, EventArgs e)
        {
            CountFile();
        }
        public static void CountFile()
        {
            var watch1 = new System.Diagnostics.Stopwatch();
            var watch2 = new System.Diagnostics.Stopwatch();
            var elapse1 = watch1.ElapsedMilliseconds;
            var elapse2 = watch2.ElapsedMilliseconds;


            watch1.Start();
            int fileCount1 = Directory.GetFiles(path1, "*.*", SearchOption.AllDirectories).Length;
            watch1.Stop();
            Console.WriteLine("The number of files in {0} : {1}", path1, fileCount1);
            Console.WriteLine($"Execution Time: {elapse1} ms");

            watch2.Start();
            int fileCount2 = Directory.GetFiles(path2, "*.*", SearchOption.AllDirectories).Length;
            watch2.Stop();

            Console.WriteLine("The number of files in {0} : {1}", path2, fileCount2);
            Console.WriteLine($"Execution Time: {elapse2} ms");
        }

        private static void SetTimer_MoniteringFile()
        {
            moniteringTimer = new System.Timers.Timer(10000);
            moniteringTimer.Elapsed += OnTimedEvent3;
            moniteringTimer.AutoReset = true;
            moniteringTimer.Enabled = true;
        }
        private static void OnTimedEvent3(Object sender, EventArgs e)
        {
            MoniteringFile();
        }
        public static void MoniteringFile()
        {

            Console.WriteLine("The TOTAL number of files in {0} : {1}", path1, CountFile.fileCount1);
            Console.WriteLine($"Execution Time: {CountFile.watch1.ElapsedMilliseconds} ms");

            Console.WriteLine("The TOTAL number of files in {0} : {1}", path2, CountFile.fileCount2);
            Console.WriteLine($"Execution Time: {CountFile.elapse1} ms");

        }







        public static void Main(string[] args)
        {
            Console.WriteLine("Main Thread Started");

            //Creating Threads
            Thread thread1 = new Thread(SetTimer_CreateFile)
            {
                Name = "Thread1"
            };

            Thread thread2 = new Thread(SetTimer_CountFile)
            {
                Name = "Thread2"
            };

            Thread thread3 = new Thread(SetTimer_MoniteringFile)
            {
                Name = "Thread3"
            };

            var test = new Countfile();

            Del d = test;

            thread1.Start();
            thread2.Start();
            thread3.Start();

            Console.WriteLine("Main Thread Ended");
            Console.WriteLine("If you want to exit this program, press enter key.");
            Console.ReadLine();

            //thread1();
            //thread2.Stop();

            Console.WriteLine("Terminating the application...");

        }
    }
}