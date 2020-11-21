using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lexicalPhase
{
    class Program
    {
       
        void currentCharacter()
        {

        }

        public static bool NextCharacter(char ch, char chPlusOne)
        {
            if (Char.Equals(ch, '<') && (Char.Equals(chPlusOne, '>'))) 
            {
                return true;
            }

            else if (Char.Equals(ch, '<') && (Char.Equals(chPlusOne, '=')))
            {
                return true;
            }

            else if (Char.Equals(ch, '>') && (Char.Equals(chPlusOne, '=') ) )
            {
                return true;
            }

            else if (Char.Equals(ch, '&') && Equals(chPlusOne, '&'))
            {
                return true;
            }

            else if (Char.Equals(ch, '|') && Char.Equals(chPlusOne, '|'))
            {
                return true;
            }

            else if (Char.Equals(ch, '=') && Char.Equals(chPlusOne, '='))
            {
                return true;
            }

            else
            {
                return false;
            }
        }


        static void Main(string[] args)
        {
            string[] wordList = new string[200];
            List<string> listOfWords = new List<string>();
            List<char> words = new List<char>();
          
            
            char ch;
            int Tchar = 0;
            StreamReader reader;
            reader = new StreamReader(@"C:\Users\Sajjad\Desktop\toRead3.txt");
            int i = 0;

            do
            {
                ch = (char)reader.Read();
                Console.Write(ch);
                
                
                    words.Add(ch);

                
              
                Tchar++;
            } while (!reader.EndOfStream);
            
            foreach(char element in words)
            {
                Console.WriteLine(element);
            }

            int initial = 0;
            bool isPreviousQuote = false;
            bool opCheck;

            for (int j = 0; j<words.Count; j++)
            {
                
                

                if (Char.Equals(words.ElementAt(j), '"') && isPreviousQuote == false)
                {
                    isPreviousQuote = true;
                    Console.WriteLine("value of previous quote at: " + words.ElementAt(j) + "is: " + isPreviousQuote);
                }
                else if (Char.Equals(words.ElementAt(j), '"') && isPreviousQuote == true)
                {
                    isPreviousQuote = false;
                    Console.WriteLine("value of previous quote at: " + words.ElementAt(j) + "is: " + isPreviousQuote);
                }

              


               if (isPreviousQuote == false)
                {
                    if ((!Char.IsWhiteSpace(words.ElementAt(j))
                     &&
                    !Char.Equals(words.ElementAt(j), '+')
                    &&
                    !Char.Equals(words.ElementAt(j), '-')
                    &&
                    !Char.Equals(words.ElementAt(j), '*')
                    &&
                    !Char.Equals(words.ElementAt(j), '/')
                    &&
                    !Char.Equals(words.ElementAt(j), '<')
                    &&
                    !Char.Equals(words.ElementAt(j), '>')
                    &&
                    !Char.Equals(words.ElementAt(j), '(')
                    &&
                    !Char.Equals(words.ElementAt(j), ')')
                    &&
                    !Char.Equals(words.ElementAt(j), '{')
                    &&
                    !Char.Equals(words.ElementAt(j), '}')
                    &&
                    !Char.Equals(words.ElementAt(j), '=')
                    &&
                    !Char.Equals(words.ElementAt(j), '!')
                    &&
                    !Char.Equals(words.ElementAt(j), '"'))
                    )

                    {   
                        wordList[initial] += words.ElementAt(j);
                    }

                    //else if char is any one of the words defined above
                    else
                    {   
                        //if char is whitespace or newline then ignore it. dont create new word for it
                        if (Char.IsWhiteSpace(words.ElementAt(j)) || words.ElementAt(j).Equals('\n'))
                        {   if (wordList[initial] != null)  //if there is something already in word location then increment
                            {
                                initial++;
                            }
                        }

                        else //else if there isnt anything in word location then...
                        {  
                            //checks the closing quotation, appends it to existing word and then increments to new word
                            if (Char.Equals(words.ElementAt(j), '"'))
                            {   
                                wordList[initial] += Char.ToString(words.ElementAt(j));
                                initial++;
                            }

                            //else if char is not a closing quotation...
                            else
                            {
                                if (wordList[initial] != null)
                                {
                                    initial++;

                                    if (NextCharacter(words.ElementAt(j), words.ElementAt(j + 1)) == false)
                                    {
                                        wordList[initial] += words.ElementAt(j);//.ToString();
                                        //wordList[initial] += Char.ToString(words.ElementAt(j));
                                    }

                                    else
                                    {
                                        //wordList[initial] += words.ElementAt(j) + words.ElementAt(j+1);//.ToString();
                                        wordList[initial] += Char.ToString(words.ElementAt(j)) + Char.ToString(words.ElementAt(j + 1));
                                        j++;
                                    }

                                    initial++;  //doesnt break alphabets in new word immediately after op if removed
                                }

                                else
                                {   //have to see if next character is present when reading from txt file warna exception dega
                                    if (NextCharacter(words.ElementAt(j), words.ElementAt(j + 1)) == false)
                                    {
                                        wordList[initial] += words.ElementAt(j);//.ToString();
                                                                                //wordList[initial] += Char.ToString(words.ElementAt(j));
                                    }
                                    else
                                    {
                                        //wordList[initial] += words.ElementAt(j) + words.ElementAt(j + 1);//.ToString();
                                        wordList[initial] += Char.ToString(words.ElementAt(j)) + Char.ToString(words.ElementAt(j + 1));
                                        j++;
                                    }

                                    initial++;
                                }
                            }
                            
                            
                        }

                    }
                }

                else
                {   
                    if (wordList[initial] != null && Char.Equals(words.ElementAt(j), '"'))
                    {
                        initial++;
                    }
                    wordList[initial] += words.ElementAt(j);
                }
                   
                

                
            } //end of for loop---------------------------------------------------------



            for (int k = 0; k<wordList.Length; k++)
            {
                Console.WriteLine(wordList[k]);
            }

            for (int l = 0; l<wordList.Length; l++)
            {
                if (wordList[l] != null)
                {
                    listOfWords.Add(wordList[l]);
                }
            }
            /*
            Console.WriteLine("\n------------printing list of words now-----------\n");
            foreach (var item in listOfWords)
            {
                Console.WriteLine(item);
            }*/

         

            reader.Close();
            reader.Dispose();
            Console.WriteLine(" ");
            Console.WriteLine(Tchar.ToString() + " characters");
            Console.ReadLine();
        }
    }
}
