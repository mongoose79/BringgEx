using System;
using System.Collections.Concurrent;
using System.IO;

namespace BringgEx
{
    public class GenSearcher
    {
        private readonly string _filePath;
        private readonly ConcurrentBag<long> _genIndexes = new ConcurrentBag<long>();

        const short PrefixCount = 11;

        public GenSearcher()
        {
            try
            {
                _filePath = @"C:\Projects\BringgEx\BringgEx\gens.txt";
                IndexDnaSourceFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to index DNA source file: {ex.Message}");
            }
        }

        private void IndexDnaSourceFile()
        {
            short count = 0;
            long position = 0;
            using (var sr = File.OpenText(_filePath))
            {
                while (!sr.EndOfStream)
                {
                    var ch = sr.Read();
                    if (Convert.ToChar(ch) == 'A')
                    {
                        count++;
                    }
                    else
                    {
                        count = 0;
                    }

                    if (count == PrefixCount)
                    {
                        _genIndexes.Add(position + 1);
                        count = 0;
                    }

                    position++;
                }
            }
        }

        public bool IsGenExist(string gen)
        {
            try
            {
                foreach (long index in _genIndexes)
                {
                    using (var sr = File.OpenText(_filePath))
                    {
                        sr.BaseStream.Seek(index, SeekOrigin.Begin);
                        char[] buffer = new char[gen.Length];
                        sr.Read(buffer, 0, gen.Length);
                        string str = new string(buffer);
                        if (gen == str)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to search the gen '{gen}': {ex.Message}");
            }

            return false;
        }

        public bool IsGenVaild(string gen)
        {
            string prefix = "AAAAAAAAAAA";
            if (gen.StartsWith(prefix) && gen.Length > prefix.Length)
            {
                return true;
            }
            return false;
        }
    }
}
