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
            Console.WriteLine("----------------");
            CreateWordFile(path, DeleteWord(path, "kangoroo"));
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

        //Goal is to Delete A specific word from the gameFile if it excists, if not return gameFile to current state
        static string[] DeleteWord(string location, string word)
        {
            //Lots of logic so have a generic try catch to catch anything so i can decide if it warrants a specific exception handler
            try
            {
                //grabbing all current words from file
                string[] words = File.ReadAllLines(location);
                //I love booleans when checking for something specific
                bool found = false;

                //this for loops entire purpuse is to check for the word at question against the gameFile
                for (int i = 0; i < words.Length; i++)
                {    
                    //if word is in gameFile toggle boolean to found
                    if (word == words[i])
                    {
                        found = true;
                    }
                }
                
                //this block will only run IF WORD IS FOUND
                if (found == true)
                {
                    //create newArray that is one shorter then current array
                    string[] newWords = new string[words.Length - 1];
                    //this for loop is looping over the gameFile array
                    for (int i = 0; i < words.Length; i++)
                    {
                        //Utilizing a specific Exception for my logic since newArray is one shorter the intial array the last run in the loop will throw a Exception
                        try
                        {
                            //will populate new array only if the word isnt the requested word to delete
                            if (word != words[i])
                            {
                                //populates new array and throws the Exception on last loop if words dont match
                                newWords[i] = words[i];
                            }
                        }
                        //handles the very last run in the loop populating only if last item in intial array wasnt the requested word
                        catch(IndexOutOfRangeException)
                        {
                            newWords[i-1] = words[i];
                        }
                        //just incase there is any Exceptions Im not anticapting will throw it to outer catch to be consoled
                        catch(Exception)
                        {
                            throw;
                        }
                        
                    }

                    return newWords;
                }
                //returns intial gameFile in string[] form cause word was not found
                else
                {
                    return words;
                }




            }
            //catches any and all exceptions and alerts developer in console to be further explorered
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                string[] response = { "Error Occured" };
                return response;
            }
        }
    }
}
