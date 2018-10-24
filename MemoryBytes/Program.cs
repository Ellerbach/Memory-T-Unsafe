using System;
using System.Diagnostics;

namespace MemoryBytes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Demonstration of Memory<T> usage");
            // Initialize the memory type as null
            Memory<byte> memory = null; // = new Memory<byte>();
            // In normal world, this would throw an error but in fact, the null initialization will initialize the class
            if (memory.IsEmpty)
                Console.WriteLine($"Memory is empty");
            else
            {
                DisplayData(memory);
            }
            // Initialization of 2 bytes
            var twobytes = new byte[2] { 0, 3 };
            // Initialization of the 2 bytes in a Memory type
            memory = new Memory<byte>(twobytes);
            if (memory.IsEmpty)
                Console.WriteLine($"Memory is empty");
            else
            {
                DisplayData(memory);
            }
            memory.Span[0] = 42;
            DisplayData(memory);
            DisplayBytes(twobytes);
            // Setting it to null
            memory = null;
            // The twobytes still exist and has not been destroyed
            DisplayBytes(twobytes);
            // No bytes will be displayed
            DisplayData(memory);
            if (memory.Span.IsEmpty)
                Console.WriteLine($"Memory is empty");
            memory = twobytes;
            DisplayData(memory);
            twobytes[0] = 5;
            DisplayData(memory);
            memory.Span[0] = 9;
            DisplayBytes(twobytes);
            // Now initialize the Memory with a partial part of the bytes
            memory = new Memory<byte>(twobytes, 1, 1);
            DisplayData(memory);
            DisplayBytes(twobytes);
            memory.Span[0] = 12;
            DisplayData(memory);
            DisplayBytes(twobytes);
            // Releasing the byte array
            twobytes = null;
            // Are the data still here?
            if (memory.IsEmpty)
                Console.WriteLine($"Memory is empty");
            else
            {
                DisplayData(memory);
            }
            memory.Span[0] = 142;
            DisplayData(memory);
            if (twobytes == null)
            {
                Console.WriteLine("The byte array is null");
            }
            else
            {
                DisplayBytes(twobytes);
            }
            GC.Collect();
            DisplayData(memory);
            memory = new byte[4] { 0, 2, 4, 6 };
            DisplayData(memory);
            var dt = memory.Pin();

            unsafe
            {
                // Here, we get the pointer
                var pointer = (byte*)dt.Pointer;
                *pointer = 24;
                // Let's see what we have in this memory
                Console.Write("Pointer: ");
                for (int i = 0; i < memory.Length; i++)
                {
                    Console.Write($"{*pointer} ");
                    pointer++;
                }
                Console.WriteLine();
            }
            DisplayData(memory);
            Console.ReadKey();

        }

        static private void DisplayData(Memory<byte> memory)
        {
            Console.Write($"Memory: ");
            for (int i = 0; i < memory.Length; i++)
                Console.Write($"{memory.Span[i]} ");
            Console.WriteLine();
        }

        static private void DisplayBytes(byte[] memory)
        {
            Console.Write($"Bytes: ");
            for (int i = 0; i < memory.Length; i++)
                Console.Write($"{memory[i]} ");
            Console.WriteLine();
        }

    }
}


