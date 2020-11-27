using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace lexicalPhase
{
    class Program
    {
       
        void currentCharacter()
        {

        }

        public static bool IsDigitsOnly(string str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                foreach (char c in str)
                {
                    if (c < '0' || c > '9')
                        return false;
                }
            }
          
            else if (String.IsNullOrEmpty(str))
            {
                return false;
            }

            return true;
            
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
            char[] wordBreaker = new[] { ' ', '\n', '+', '-', '*', '/', '<', '>', '(', ')', '{', '}', '=', '!', '"', '[', ']', ';' };
            
            char ch;
            int Tchar = 0;
            StreamReader reader;
            reader = new StreamReader(@"C:\Users\Sajjad\Desktop\toRead6.txt");
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
                    !Char.Equals(words.ElementAt(j), '"')
                    &&
                    !Char.Equals(words.ElementAt(j), '[')
                    &&
                    !Char.Equals(words.ElementAt(j), ']')
                    &&
                    !Char.Equals(words.ElementAt(j), ';')
                    &&
                    !Char.Equals(words.ElementAt(j), '.'))
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

                        else //else if there is anything except whitespace/newline then...
                        {  
                            //checks the closing quotation, appends it to existing word and then increments to new word
                            if (Char.Equals(words.ElementAt(j), '"'))
                            {   
                                wordList[initial] += Char.ToString(words.ElementAt(j));
                                initial++;
                            }
                            
                            else if (Char.Equals(words.ElementAt(j), '.')) //if char is .
                            {
                                Console.WriteLine("word at inital is: " + wordList[initial]);
                                string[] tempArr = new string[2];

                                if (IsDigitsOnly(wordList[initial]))   //if word previous to . are all digits
                                {
                                    Console.WriteLine("previous word are all digits");
                                    int temp = j + 1;
                                    Console.WriteLine("char in temp is: " + words.ElementAt(temp));
                                    if (!Char.Equals(words.ElementAt(temp), ('+', '-', '*', '/', '<', '>', '(', ')', '{', '}', '=', '!', '[', ']', '"', ';', '.')) && !Char.IsWhiteSpace(words.ElementAt(temp)))
                                    {
                                        Console.WriteLine("char after . is not a breaker");
                                        if (Char.IsDigit(words.ElementAt(temp)))    //if the immediate char after dot is a digit
                                        {
                                            Console.WriteLine("char after . is a digit");
                                            tempArr[0] += Char.ToString(words.ElementAt(j));    //saving . in separate array location
                                            tempArr[1] += Char.ToString(words.ElementAt(temp)); //saving char after .
                                            temp++;
                                            while (temp < words.Count && !Char.Equals(words.ElementAt(temp), ('+', '-', '*', '/', '<', '>', '(', ')', '{', '}', '=', '!', '[', ']', '"', ';', '.')) && !Char.IsWhiteSpace(words.ElementAt(temp)))
                                            {   //appenidn characters in temp until breaker 
                                                Console.WriteLine("chars are not breakers");
                                                tempArr[1] += Char.ToString(words.ElementAt(temp));
                                                Console.WriteLine("tempArrr[1] is: " + tempArr[1]);
                                                temp++;
                                            }


                                            wordList[initial] += tempArr[0] + tempArr[1];
                                            initial++;
                                            j = temp;

                                           
                                        }
                                        else if (!(Char.IsDigit(words.ElementAt(temp))) && !Char.IsWhiteSpace(words.ElementAt(temp)))//else if the first char after . is not a digit
                                        {
                                            Console.WriteLine("first char after . is not a digit and also not a whitespace");
                                            initial ++;
                                            wordList[initial] += words.ElementAt(j);
                                            initial++;
                                        }

                                     
                                    }
                                    else //if char after . is a breaker
                                    {
                                        Console.WriteLine("Char after . is a breaker");
                                        initial++;
                                        wordList[initial] += words.ElementAt(j);    //append dot char with previous word
                                    }
                                }
                                else if (IsDigitsOnly(wordList[initial]) == false)//if previous word in wordlist[inital] is not all digits
                                {   
                                    Console.WriteLine("word before . are not all digits");
                                    Console.WriteLine("current char in j is: " + words.ElementAt(j));
                                    int temp = j + 1;
                                    
                                    if (Char.IsDigit(words.ElementAt(temp)))    //if char after . is digit
                                    {
                                        Console.WriteLine("-----------------------" + words.ElementAt(j));
                                        if (wordList[initial] != null)
                                        {
                                            initial++;
                                            wordList[initial] += Char.ToString(words.ElementAt(j)) + Char.ToString(words.ElementAt(temp));
                                            j = temp;
                                        }
                                        else
                                        {
                                            wordList[initial] += Char.ToString(words.ElementAt(j)) + Char.ToString(words.ElementAt(temp));
                                            j = temp;
                                        }
                                    }
                                    else        //if char after . is not a digit
                                    {   
                                        if (wordList[initial] != null)
                                        {
                                            initial++;
                                            wordList[initial] += words.ElementAt(j);
                                            initial++;
                                        }
                                        else
                                        {
                                            wordList[initial] += words.ElementAt(j);
                                            initial++;
                                        }
                                        
                                    }
                                    
                                }
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
                        wordList[initial] += words.ElementAt(j);
                    }

                    //if char is backslash then accept the very next char whatever it may be and dont check the next char in for loop
                    else if (Char.Equals(words.ElementAt(j), '\\'))
                    {
                        wordList[initial] += Char.ToString(words.ElementAt(j)) + Char.ToString(words.ElementAt(j + 1));
                        j++;
                    }
                    //else if its neither a backslash nor a closing quote then accept it just like any string (i.e wihtout new word)
                    else
                    {
                        wordList[initial] += words.ElementAt(j);
                    }
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
            
            Console.WriteLine("\n------------printing list of words now-----------\n");
            foreach (var item in listOfWords)
            {
                Console.WriteLine(item);
            }

         

            reader.Close();
            reader.Dispose();
            Console.WriteLine(" ");
            Console.WriteLine(Tchar.ToString() + " characters");
            Console.ReadLine();
        }
    }
}
