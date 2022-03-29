using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoloLearn
{

    public class RecursiveFileSearch
    {
        static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();

        static void Main()
        {
            Console.WriteLine("ВВЕДИТЕ ПУТЬ ДИРРЕКТОРИИ:");
            string URL = Console.ReadLine();

            try
            {
                long d = 0;
                long s = 0;
                System.IO.DirectoryInfo root = new System.IO.DirectoryInfo(URL);
                long C = WalkDirectoryTree(root, ref d, ref s);
                Console.WriteLine("Исходный размер папки: " + C + " байт");
                pathDir(URL);
                long r = 0;
                long m = 0;
                System.IO.DirectoryInfo root2 = new System.IO.DirectoryInfo(URL);
                long E = WalkDirectoryTree(root, ref r, ref m);
                long g = s - m;
                long h = d - r;
                Console.WriteLine("удалино файлов: " + g + "Освобождено: "+ h+ " байт");
                Console.WriteLine("Исходный размер папки: " + E + " байт");

            }
            catch (Exception ex)
            { Console.WriteLine("Произошла ошибка: " + ex.Message); }



        }

        static void pathDir(string dirName)
        {
            try
            {
                if (Directory.Exists(dirName));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
            {
                string[] dirs = Directory.GetDirectories(dirName);
                string[] files = Directory.GetFiles(dirName);
                for (int i = 0; i < dirs.Length; i++)
                {
                    if (Directory.Exists(dirs[i]))
                    {
                        DateTime d = Directory.GetCreationTime(dirs[i]) + TimeSpan.FromMinutes(1); // 1 минута за место 30
                        if (d <= DateTime.Now)
                        {
                            Directory.Delete(dirs[i], true);
                        }
                    }
                }
                for (int i = 0; i < files.Length; i++)
                {
                    if (File.Exists(files[i]))
                    {
                        DateTime d = File.GetCreationTime(files[i]) + TimeSpan.FromMinutes(1); // 1 минута за место 30
                        if (d <= DateTime.Now)
                        {
                            File.Delete(files[i]);
                        }
                    }
                }
            }


        }
        static long WalkDirectoryTree(System.IO.DirectoryInfo root, ref long d, ref long s)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;
            try
            {
                files = root.GetFiles(".");
                s = s + files.Length;
            }
            catch (UnauthorizedAccessException e) { log.Add(e.Message); }

            catch (System.IO.DirectoryNotFoundException e) { Console.WriteLine(e.Message); }

            if (files != null)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    d = d + files[i].Length;
                }
            }
            subDirs = root.GetDirectories();

            for (int j = 0; j < subDirs.Length; j++)
            {
                WalkDirectoryTree(subDirs[j], ref d, ref s);
            }
            return d;

        }
    }
}


