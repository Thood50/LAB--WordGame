using System;
using System.IO;


namespace WordGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            string[] gameWords = { "cat", "dog", "kittie", "puppy", "turtle" };
            string path = "../../../wordFile.txt";
            CreateWordFile(path, gameWords);
            Interface(path);



        }

        static void Play(string path)
        {
            string[] words = File.ReadAllLines(path);
            char[] guessedLetters = new char[0];
            int guesses = 0;
            string randomWord = RandomWord(words);
            
            while(guesses < 16 && CheckForWinner(guessedLetters, randomWord) == false)
            {
                DisplayWord(randomWord, guessedLetters);
                Console.WriteLine($"You have {15 - guesses} guesses left");
                Console.WriteLine($"You have guesses: ");
                DisplayGuessed(guessedLetters);
                Console.WriteLine($"Your next guess is?");
                string guess = Console.ReadLine();
                guesses++;
            }

            if (CheckForWinner(guessedLetters, randomWord) == true)
            {
                Console.WriteLine("Congrats you did it");
                Interface(path);
            }
            if (CheckForWinner(guessedLetters, randomWord) == false)
            {
                Console.WriteLine("Sadly you didnt quite do it, try again!");
                Interface(path);
            }

        }

        static void Interface(string path)
        {
            Console.WriteLine("Welcome to word game");
            Console.WriteLine("Please enter a commad");
            Console.WriteLine("1. Play Game");
            Console.WriteLine("2. View Words");
            Console.WriteLine("3. Add word");
            Console.WriteLine("4. Delete word");
            Console.WriteLine("5. Exit");
            string input = Console.ReadLine();

            switch (Convert.ToInt32(input))
            {
                case 1:
                    Play(path);
                    break;

                case 2:
                    ReadWordFile(path);
                    break;

                case 3:
                    Console.WriteLine("What word do you wish to add?");
                    string newWord = Console.ReadLine();
                    AddWordToGame(path, newWord);
                    break;

                case 4:
                    Console.WriteLine("What word do you wish to delete?");
                    string deleteWord = Console.ReadLine();
                    DeleteWord(path, deleteWord);
                    break;

                case 5:
                    Environment.Exit(1);
                    break;

                default:
                    Interface(path);
                    break;
            }
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
        static string[] ReadWordFile(string location)
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

                Interface(location);
                return words;
            }
            //Console log Exception during development so I can decide if a specific catch is needed
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                string[] error = { "Error reading File" };
                Interface(location);
                return error;
            }
        }

        //Add Word to end of file
        static void AddWordToGame(string location, string newWord)
        {            
            try
            {                
                using (StreamWriter addWord = File.AppendText(location))
                {                    
                    try
                    {
                        addWord.WriteLine(newWord);
                        Interface(location);
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

        //Goal is to Delete A specific word from the gameFile if it excists, if not return gameFile to current state
        static void DeleteWord(string location, string word)
        {
            //Lots of logic so have a generic try catch to catch anything so i can decide if it warrants a specific exception handler
            try
            {
                using (StreamWriter deleteWord = File.AppendText(location))
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
                            catch (IndexOutOfRangeException)
                            {
                                newWords[i - 1] = words[i];
                            }
                            //just incase there is any Exceptions Im not anticapting will throw it to outer catch to be consoled
                            catch (Exception)
                            {
                                throw;
                            }

                        }

                        Interface(location);

                    }
                    //returns intial gameFile in string[] form cause word was not found
                }





            }
            //catches any and all exceptions and alerts developer in console to be further explorered
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                string[] response = { "Error Occured" };
                
            }
        }

        //Generates random word from File
        static string RandomWord(string[] arr)
        {
            Random number = new Random();
            int i = number.Next(0, arr.Length);
            string newWord = arr[i];
            return newWord;
        }

        //Displays the random word
        static void DisplayWord(string word, char[] guessed)
        {
            string theWord = word;
            char[] wordArray = theWord.ToCharArray();

            for (int i = 0; i < wordArray.Length; i++)
            {
                for (int k = 0; k < guessed.Length; k++)
                {
                    if ( wordArray[i] == guessed[k])
                    {
                        Console.Write(wordArray[i]);
                    }
                }

                Console.Write(" _");
            }
        }

        //takes previous guess and newguess and stores into array if incorrect
        static char[] GuessedLetters(char[] oldguess, char newguess)
        {
            oldguess[oldguess.Length] = newguess;

            return oldguess;
        }

        //Displays Guesses
        static void DisplayGuessed(char[] guesses)
        {
            for (int i = 0; i < guesses.Length; i++)
            {
                Console.Write("Your guesses: ");
                if(i == (guesses.Length - 1))
                {
                    Console.WriteLine($"{guesses[i]}.");
                };
                Console.Write($" {guesses[i]},");

            }
        }

        //Check for winner
        static bool CheckForWinner(char[] guessed, string word)
        {
            string theWord = word;
            char[] wordArray = theWord.ToCharArray();
            int totalLetters = 0;

            for (int i = 0; i < wordArray.Length; i++)
            {
                for (int k = 0; k < guessed.Length; k++)
                {
                    if (wordArray[i] == guessed[k])
                    {
                        if (totalLetters == wordArray.Length)
                        {
                            return true;
                        }
                        else
                        {
                            totalLetters++;
                        }
                    }
                }

                
            }
            return false;
        }
    }
}
