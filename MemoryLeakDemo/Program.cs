using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MemoryLeakDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Classifier classifier = new Classifier();
            Console.WriteLine("Starting initial operation.");
            Console.WriteLine("Run 1");

            for (int i = 0; i < 100; i++)
            {
                classifier.ClassifyImage("Image.png");
            }

            Console.WriteLine("Create snapshot and press enter to run again.");
            Console.ReadLine();
            Console.WriteLine("Run 2");

            for (int i = 0; i < 100; i++)
            {
                classifier.ClassifyImage("Image.png");
            }

            Console.WriteLine("Create second snapshot and press enter to run GC.Collect().");
            GC.Collect();
            Console.ReadLine();

            Console.WriteLine("GC Collected. Create snapshot and press enter to exit.");
            Console.ReadLine();
        }
    }
}
