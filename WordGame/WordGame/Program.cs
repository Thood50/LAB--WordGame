using System;
using System.IO;


namespace WordGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] gameWords = { "cat", "dog", "kittie", "puppy", "turtle"};
            string path = "../../../wordFile.txt";
            CreateWordFile(path, gameWords);
        }

        static void CreateWordFile(string location, string[] words)
        {
            try
            {
                //Putting streamwriter in a using block so that any files that are touched/opened get
                // closed at the end of the block
                using (StreamWriter createNewFile = new StreamWriter(location))
                {
                    try
                    {
                        for (int i = 0; i < words.Length; i++)
                        {
                            createNewFile.WriteLine(words[i]);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
