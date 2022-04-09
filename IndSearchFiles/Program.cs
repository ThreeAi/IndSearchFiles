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
            Console.WriteLine("Введите имя папки");
            s = Console.ReadLine();          
            List<Name>[] Table = new List<Name>[256];                                 //создания массива списков
            for (int i = 0; i < Table.Length; i++)
                Table[i] = new List<Name>();
            Filling(s, Table);
            Console.WriteLine();
            Console.WriteLine("1: - Поиск файла по имени  2: - Выход");
            while (Convert.ToInt32(Console.ReadLine()) == 1) 
            {
                Console.WriteLine("Введите имя файла для поиска (регистр не учитывается)");
                s = Console.ReadLine();
                Search(s, Table);
            } 
        }
        static void Filling (string s, List<Name>[] Table)
        {
            try
            {
                foreach (string file in Directory.GetFiles(s))
                    Table[Hash(Path.GetFileName(file).ToLower())].Add(new Name(file, Path.GetFileName(file.ToLower()))); //добавление в список 
                foreach (string folder in Directory.GetDirectories(s))
                    Filling(folder, Table);
                Console.Write('.');
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                Console.WriteLine("Указано имя несуществующей или недоступной папки...");
            }
            catch (System.UnauthorizedAccessException)
            {
                Console.Write("нет доступа к папке {0}",s);
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
            int ind = Hash(s.ToLower()), k= 0;
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
