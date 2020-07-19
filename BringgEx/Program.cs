using System;

namespace BringgEx
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start gen searcher server... Default URL is http://localhost:8080/genes/find/{gen}");
            try
            {
                var listener = new Listener();
                listener.CreateAndStartListener();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred during the run of gen searcher server. The error is: {ex}");
            }
        }
    }
}
