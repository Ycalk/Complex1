using System;

namespace Complex1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var mas = new[] { 23,45,37,98,67,56,90,56,17,46,78,13,26 };
            var tree = new SegmentTree(mas);

            Console.WriteLine(tree.Contains(1, 3, 37));
            Console.WriteLine(tree.Contains(4, 7, 98));
            Console.WriteLine(tree.Contains(1, 13, 2));
        }
    }
}
