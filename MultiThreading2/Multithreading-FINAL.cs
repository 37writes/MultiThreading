using System;
using System.IO;
using System.Threading;
using System.Diagnostics;


namespace MultiThreading
{
    public class File
    {
        public static string path1 = @"D:\2022. Visual Studio\folder1\";
        public static string path2 = @"D:\2022. Visual Studio\folder2\";
        public void CreateFile1()
        {
            if (Directory.Exists(path1))
            {
                for (int i = 0; i < 10; i++)
                {
                    string fileName = string.Format("MyFile_" + i + "_{0:HH-mm-ss.fff}" + ".txt", DateTime.Now);
                    string newPath1 = path1 + fileName;
                    if (!System.IO.File.Exists(newPath1))
                    {
                        using (StreamWriter sw = System.IO.File.CreateText(newPath1));
                    }
                }
                //Console.WriteLine("Create files in folder1");
            }
        }
        public void CreateFile2()
        {
            Thread.Sleep(1000);

            if (Directory.Exists(path2))
            {
                for (int i = 0; i < 10; i++)
                {
                    string fileName = string.Format("MyFile_" + i + "_{0:HH-mm-ss.fff}" + ".txt", DateTime.Now);
                    var newPath2 = path2 + fileName;
                    if (!System.IO.File.Exists(newPath2))
                    {
                        using (StreamWriter sw = System.IO.File.CreateText(newPath2));
                    }
                }
                //Console.WriteLine("Create files in folder2");
            }
        }
        public (int, long) CountFile1()
        {
            var watch1 = new System.Diagnostics.Stopwatch();
            watch1.Start();
            int fileCount1 = Directory.GetFiles(path1, "*.*", SearchOption.AllDirectories).Length;
            watch1.Stop();
            var elapse1 = watch1.ElapsedMilliseconds;
            
            return (fileCount1, elapse1);
        }
        public (int, long) CountFile2()
        {
            Thread.Sleep(1000);

            var watch2 = new System.Diagnostics.Stopwatch();
            watch2.Start();
            int fileCount2 = Directory.GetFiles(path2, "*.*", SearchOption.AllDirectories).Length;
            watch2.Stop();
            var elapse2 = watch2.ElapsedMilliseconds;

            return (fileCount2, elapse2);
        }
    }    

    class MainProgram
    {
        public static File file = new File();
        public static string path1 = @"D:\2022. Visual Studio\folder1\";
        public static string path2 = @"D:\2022. Visual Studio\folder2\";

        public static System.Timers.Timer createTimer1;
        public static System.Timers.Timer createTimer2;
        public static System.Timers.Timer countTimer1;
        public static System.Timers.Timer countTimer2;
        public static System.Timers.Timer moniteringTimer;
       
        private static void SetTimer_CreateFile1()
        {
            createTimer1 = new System.Timers.Timer(1000);
            createTimer1.Elapsed += OnTimedEvent1;
            createTimer1.AutoReset = true;
            createTimer1.Enabled = true;
        }
        private static void OnTimedEvent1(Object sender, EventArgs e)
        {
            file.CreateFile1();
        }
        private static void SetTimer_CreateFile2()
        {
            createTimer2 = new System.Timers.Timer(1000);
            createTimer2.Elapsed += OnTimedEvent2;
            createTimer2.AutoReset = true;
            createTimer2.Enabled = true;
        }
        private static void OnTimedEvent2(Object sender, EventArgs e)
        {
            file.CreateFile2();
        }

        private static void SetTimer_CountFile1()
        {
            countTimer1 = new System.Timers.Timer(1000);
            countTimer1.Elapsed += OnTimedEvent3;
            countTimer1.AutoReset = true;
            countTimer1.Enabled = true;
        }
        private static void OnTimedEvent3(Object sender, EventArgs e)
        {
            file.CountFile1();
        }

        private static void SetTimer_CountFile2()
        {
            countTimer2 = new System.Timers.Timer(1000);
            countTimer2.Elapsed += OnTimedEvent3;
            countTimer2.AutoReset = true;
            countTimer2.Enabled = true;
        }
        private static void OnTimedEvent4(Object sender, EventArgs e)
        {
            file.CountFile2();
        }

        public static void Main(string[] args)
        {
            
            Console.WriteLine("Main Thread Started");
            Console.WriteLine("If you want to exit this program, press enter key.");
                        
            //Creating Threads
            Thread thread1 = new Thread(SetTimer_CreateFile1);
            Thread thread2 = new Thread(SetTimer_CreateFile2);
            Thread thread3 = new Thread(SetTimer_CountFile1);
            Thread thread4 = new Thread(SetTimer_CountFile2);

            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();

            moniteringTimer = new System.Timers.Timer(4000);
            moniteringTimer.Elapsed += OnTimedEvent5;
            moniteringTimer.AutoReset = true;
            moniteringTimer.Enabled = true;

            static void OnTimedEvent5(Object sender, EventArgs e)
            {
                (int, long) i = file.CountFile1();
                (int, long) j = file.CountFile2();
                Console.WriteLine("\nThe number of files in {0} ==> {1}", path1, i.Item1);
                Console.WriteLine("Execution Time: {0} ms", i.Item2);
                Console.WriteLine("\nThe number of files in {0} ==> {1}", path2, j.Item1);
                Console.WriteLine("Execution Time: {0} ms",j.Item2);
            }

            Console.WriteLine("Main Thread Ended");
            Console.ReadLine();
            Console.WriteLine("Terminating the application...");
        }
    }
}