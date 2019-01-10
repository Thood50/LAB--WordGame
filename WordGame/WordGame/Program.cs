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
            //ReadWordFile(path);
            AddWordToGame(path, "mouse");
            ReadWordFile(path);
            Console.ReadLine();
        }

        //Create File, Populate File from array of words
        static void CreateWordFile(string location, string[] words)
        {
            //Used to handle any Exceptions thrown up from the using
            try
            {
                //Putting streamwriter in a using block so that any files that are touched/opened get
                // closed at the end of the block
                using (StreamWriter createNewFile = new StreamWriter(location))
                {
                    //used to throw out any Exceptions to outside of the using block
                    try
                    {
                        //looping through the incoming array to print the words onto the newly created file
                        for (int i = 0; i < words.Length; i++)
                        {
                            createNewFile.WriteLine(words[i]);
                        }
                    }
                    //Does the throwing
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            //Does the catching
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //Read File Lines, populate array from file, console all items in array
        static void ReadWordFile(string location)
        {
            //Catch any Exceptions
            try
            {
                //Create and populate array from contents of file
                string[] words = File.ReadAllLines(location);

                //Console each word that populates words array
                for (int i = 0; i < words.Length; i++)
                {
                    Console.WriteLine(words[i]);
                    
                }

            }
            //Console log Exception during development so I can decide if a specific catch is needed
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //Add Word to end of file
        static void AddWordToGame(string location, string newWord)
        {
            //To handle any Exceptions for the using block
            try
            {
                //Handles the opening and closing of the gameFile
                using (StreamWriter addWord = File.AppendText(location))
                {
                    //Catch any Exceptions to then throw outside of using block
                    try
                    {
                        addWord.WriteLine(newWord);
                    }
                    //does the throwing
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            //Does the catching
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
