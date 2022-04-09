using System;
using System.IO;

namespace IndSearchFiles
{
    class Program
    {
        public struct Name
        {
            public string Full;
            public string Short;
            public Name (string Newfull, string Newshort)
            {
                Full = Newfull;
                Short = Newshort;
            }
        }
        
        static void Main(string[] args)
        {
            Temp();                                                
        }
        static void Temp ()
        {
            string s;
            Console.WriteLine("введите папку где неужно найти файл");
            s = Console.ReadLine();          
            List<Name>[] Table = new List<Name>[256];
            for (int i = 0; i < Table.Length; i++)
                Table[i] = new List<Name>();
            Filling(s, Table);
            Console.WriteLine();
            Console.WriteLine("введите файл, котороый нужно найти(с расширением)");
            s = Console.ReadLine();
            Search(s, Table);
            }
        static void Filling (string s, List<Name>[] Table)
        {
            try
            {
                foreach (string file in Directory.GetFiles(s))
                    Table[Hash(Path.GetFileName(file))].Add(new Name(file, Path.GetFileName(file.ToLower())));
                foreach (string folder in Directory.GetDirectories(s))
                    Filling(folder, Table);
                Console.Write('.');
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                Console.WriteLine("такой папки нет");
            }
            catch (System.UnauthorizedAccessException)
            {
                Console.Write("{0} закрытая папка ",s);
            }
        }
        static int Hash (string name)
        {
            int hash = 0;
            foreach (char c in name)
                hash += (int)c;
            return (hash % 256);
        }
        static void Search(string s, List<Name>[] Table)
        {
            int ind = Hash(s), k= 0;
            foreach (Name name in Table[ind])
                if (s.ToLower() == name.Short)
                    Console.WriteLine(name.Full);
                else
                    k++;
            if (k == Table[ind].Count)
                Console.WriteLine("файл с таким именем не найден");
        }
    }
}
