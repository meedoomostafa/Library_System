using System.Xml.Serialization;

namespace Testing
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var Reader = new StreamReader(Console.OpenStandardInput()))
            using (var Writer = new StreamWriter(Console.OpenStandardOutput()))
            {
                Writer.AutoFlush = true;
                string[] Q = Reader.ReadLine().Split(' ');
                int L = Math.Min(int.Parse(Q[0]), int.Parse(Q[1]));
                int R = Math.Max(int.Parse(Q[0]), int.Parse(Q[1]));
                Console.WriteLine(L);
                
                test(Reader, Writer);
            }
        }
        public static void test (StreamReader Reader, StreamWriter Writer)
        {
            string[] Q = Console.ReadLine().Split(' ');
            int L = Math.Min(int.Parse(Q[0]), int.Parse(Q[1]));
            int R = Math.Max(int.Parse(Q[0]), int.Parse(Q[1]));
            
        }
    }
}
