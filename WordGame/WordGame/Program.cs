using System;
using System.IO;


namespace WordGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = "../../../newFile.txt";
            CreateTextFile(path);
        }

        static void CreateTextFile(string location)
        {
            //Need a try catch to handle any possible Exceptions
            try
            {
                //Putting streamwriter in a using block so that any files that are touched/opened get
                // closed at the end of the block
                using (StreamWriter createNewFile = new StreamWriter(location))
                {
                    createNewFile.WriteLine("Im writing in a new file");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
