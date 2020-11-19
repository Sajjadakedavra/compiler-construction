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

        void nextCharacter()
        {

        }


        static void Main(string[] args)
        {
            string[] wordList = new string[200];
            List<char> words = new List<char>();
            char[] charArr = new char[500];
            char ch;
            int Tchar = 0;
            StreamReader reader;
            reader = new StreamReader(@"C:\Users\Sajjad\Desktop\toRead1.txt");
            int i = 0;

            do
            {
                ch = (char)reader.Read();
                Console.Write(ch);
                
                //if (Char.IsWhiteSpace(ch) == false)
                //{
                    words.Add(ch);
                //}
                
              
                Tchar++;
            } while (!reader.EndOfStream);
            
            foreach(char element in words)
            {
                Console.WriteLine(element);
            }

            int initial = 0;
            bool isPreviousQuote = false;
       

            for (int j = 0; j<words.Count; j++)
            {
                
                

                if (Char.Equals(words.ElementAt(j), '"') && isPreviousQuote == false)
                {
                    isPreviousQuote = true;
                }
                else
                {
                    isPreviousQuote = false;
                }

              


                if (isPreviousQuote == false)
                {
                    if ((!Char.IsWhiteSpace(words.ElementAt(j))//!String.Equals(words.ElementAt(j), " ")
                    &&
                   !Char.Equals(words.ElementAt(j), "+")
                   &&
                   !Char.Equals(words.ElementAt(j), "-")
                   &&
                   !Char.Equals(words.ElementAt(j), "*")
                   &&
                   !Char.Equals(words.ElementAt(j), "/"))
                   )
                    {
                        wordList[initial] += words.ElementAt(j);
                    }
                    else
                    {
                        if (Char.IsWhiteSpace(words.ElementAt(j)) || words.ElementAt(j).Equals('\n'))
                        {   if (wordList[initial] != null)
                            {
                                initial++;
                            }
                        }
                        else
                        {  
                            if(wordList[initial] != null)
                            {
                                initial++;
                                wordList[initial] = words.ElementAt(j).ToString();
                                //initial++;
                            }
                            else
                            {
                                wordList[initial] = words.ElementAt(j).ToString();
                                initial++;
                            }
                            
                        }

                    }
                }
                else
                {
                    wordList[initial] += words.ElementAt(j);
                }
                   
                

                
            }



            for (int k = 0; k<wordList.Length; k++)
            {
                Console.WriteLine(wordList[k]);
            }

         

            reader.Close();
            reader.Dispose();
            Console.WriteLine(" ");
            Console.WriteLine(Tchar.ToString() + " characters");
            Console.ReadLine();
        }
    }
}
