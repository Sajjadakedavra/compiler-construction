using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace lexicalPhase
{

    class lexicalAnalyzer
    {
        public static string temporaryStorage;
        public static List<string> listOfWords = new List<string>();
        public static List<int> listOfLineNumber = new List<int>();

        public static Tuple<int, int> characterChecking(string[] wordList, int initial, int j, List<char> words)
        {
            int i = 0;
            Console.WriteLine("word in character checking is: " + words.ElementAt(j));
            if (wordList[initial] != null)  //if there is already something in word list then goto empty place
            {
                initial++;
            }
            wordList[initial] += words.ElementAt(j);
            i++;
            //j++;    //for the next character from the stream

            if (words.ElementAt(j + 1).Equals('\''))
            {
                j++;
                Console.WriteLine("two direct single quotes wihtout space: " + words.ElementAt(j));
                wordList[initial] += words.ElementAt(j);
            }

            else if (words.ElementAt(j + 1).Equals('\\'))
            {
                Console.WriteLine("backslash after opening quote");
                while (i < 4)
                {
                    j++;
                    wordList[initial] += words.ElementAt(j);
                    i++;
                }
            }

            else
            {
                Console.WriteLine("no backslash after opening quote");
                while (i < 3)
                {
                    j++;
                    wordList[initial] += words.ElementAt(j);
                    i++;
                }
            }
            Console.WriteLine("leaving j-1 with char: " + words.ElementAt(j - 1));
            Console.WriteLine("leaving j with char: " + words.ElementAt(j));
            Console.WriteLine("leaving j+1 with char: " + words.ElementAt(j + 1));
            initial++;
            return new Tuple<int, int>(initial, j);
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

            else if (Char.Equals(ch, '>') && (Char.Equals(chPlusOne, '=')))
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

        public static void WordBreaker()
        {
            string[] wordList = new string[200];
            //List<string> listOfWords = new List<string>();
            //List<int> listOfLineNumber = new List<int>();
            List<char> words = new List<char>();
            char[] wordBreaker = new[] { ' ', '\n', '+', '-', '*', '/', '<', '>', '(', ')', '{', '}', '=', '!', '"', '[', ']', ';', ':' };

            char ch;
            int Tchar = 0;
            StreamReader reader;
            reader = new StreamReader(@"C:\Users\Sajjad\Desktop\lexical_sample_file.txt");
            int i = 0;
            int[] lineNumberArr = new int[wordList.Length];

            do
            {
                ch = (char)reader.Read();
                Console.Write(ch);


                words.Add(ch);



                Tchar++;
            } while (!reader.EndOfStream);

            foreach (char element in words)
            {
                Console.WriteLine(element);
            }

            int initial = 0;
            bool isPreviousQuote = false;
            int lineNumber = 1;

            for (int j = 0; j < words.Count; j++)
            {


                if ((Char.Equals(words.ElementAt(j), '"')) && isPreviousQuote == false) // || Char.Equals(words.ElementAt(j), '\'')) && isPreviousQuote == false)
                {
                    isPreviousQuote = true;
                    Console.WriteLine("value of previous quote at: " + words.ElementAt(j) + "is: " + isPreviousQuote);
                }
                else if ((Char.Equals(words.ElementAt(j), '"')) && isPreviousQuote == true)// || Char.Equals(words.ElementAt(j), '\'')) && isPreviousQuote == true)
                {
                    isPreviousQuote = false;
                    Console.WriteLine("value of previous quote at: " + words.ElementAt(j) + "is: " + isPreviousQuote);
                }

                if (Char.Equals(words.ElementAt(j), '\n') && isPreviousQuote == true)
                {
                    isPreviousQuote = false;
                    Console.WriteLine("falsifying quotation due to new line");
                    Console.WriteLine("value of previous quote is: " + isPreviousQuote);
                }

                if (Char.Equals(words.ElementAt(j), '\n'))
                {
                    lineNumber++;
                }

                if (isPreviousQuote == false && words.ElementAt(j).Equals('\''))
                {
                    var tuple = characterChecking(wordList, initial, j, words);
                    initial = tuple.Item1;
                    j = tuple.Item2;
                }


                else if (isPreviousQuote == false)
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
                    !Char.Equals(words.ElementAt(j), '.')
                    &&
                    !Char.Equals(words.ElementAt(j), ':'))
                    )

                    {
                        wordList[initial] += words.ElementAt(j);
                        lineNumberArr[initial] = lineNumber;
                    }

                    //else if char is any one of the words defined above
                    else
                    {
                        //if char is whitespace or newline then ignore it. dont create new word for it
                        if (Char.IsWhiteSpace(words.ElementAt(j)) || words.ElementAt(j).Equals('\n'))
                        {
                            if (wordList[initial] != null)  //if there is something already in word location then increment
                            {
                                initial++;
                            }
                        }

                        //if consecutive char are beginning of multi line comments
                        else if (Char.Equals(words.ElementAt(j), '/') && Char.Equals(words.ElementAt(j + 1), '*'))
                        {
                            if (wordList[initial] != null)
                            {
                                initial++;
                            }

                            j = j + 2;
                            while (j < words.Count - 1 && !Char.Equals(words.ElementAt(j), '*') && !Char.Equals(words.ElementAt(j + 1), '/'))
                            {
                                j++;
                                Console.WriteLine("the word in while is: " + words.ElementAt(j));
                            }
                            j++;
                        }

                        else //else if there is anything except whitespace/newline then...
                        {
                            //checks the closing quotation, appends it to existing word and then increments to new word
                            if ((Char.Equals(words.ElementAt(j), '"')))// || (Char.Equals(words.ElementAt(j), '\'')))
                            {
                                wordList[initial] += Char.ToString(words.ElementAt(j));
                                lineNumberArr[initial] = lineNumber;
                                initial++;
                            }

                            else if (Char.Equals(words.ElementAt(j), '.')) //if char is .
                            {
                                //changed from here on 21/02/2021 at 1:39 am
                                if (!String.IsNullOrEmpty(temporaryStorage))
                                {
                                    Console.WriteLine("word in temporart storage is: " + temporaryStorage);
                                    if (String.Equals(temporaryStorage, "."))
                                    {
                                        Console.WriteLine("word in temporart storage is: " + temporaryStorage);
                                        if (Char.IsDigit(wordList[initial][0]))
                                        {
                                            Console.WriteLine("char is digit if condiiton true: " + wordList[initial--] + words.ElementAt(j));
                                            Console.WriteLine("bhai -2 pe hai: " + wordList[initial - 2]);
                                            Console.WriteLine("bhai -1 pe hai: " + wordList[initial - 1]);
                                            Console.WriteLine("bhai 0 pe hai: " + wordList[initial]);
                                            Console.WriteLine("bhai 1 pe hai: " + wordList[initial + 1]);
                                            wordList[initial] += wordList[initial + 1]; //agar recurring dot k agar pehla digit he to usky sath jor do
                                            wordList[initial + 1] = "";
                                            temporaryStorage = "";
                                        }
                                        else
                                        {
                                            Console.WriteLine("char is digit if condiiton false: " + wordList[initial]);

                                            temporaryStorage = "";
                                        }
                                    }

                                } //till here...................

                                int lineNumberr = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                Console.WriteLine("executing line number: " + lineNumberr);
                                Console.WriteLine("word at inital is: " + wordList[initial]);
                                string[] tempArr = new string[2];

                                if (IsDigitsOnly(wordList[initial]))   //if word previous to . are all digits
                                {
                                    Console.WriteLine("previous word are all digits");
                                    int temp = j + 1;
                                    Console.WriteLine("char in temp is: " + words.ElementAt(temp));
                                    if (!Char.Equals(words.ElementAt(temp), ('+', '-', '*', '/', '<', '>', '(', ')', '{', '}', '=', '!', '[', ']', '"', ';', '.', ':')) && !Char.IsWhiteSpace(words.ElementAt(temp)))
                                    {
                                        Console.WriteLine("char after . is not a breaker");
                                        if (Char.IsDigit(words.ElementAt(temp)))    //if the immediate char after dot is a digit
                                        {
                                            Console.WriteLine("char after . is a digit");
                                            tempArr[0] += Char.ToString(words.ElementAt(j));    //saving . in separate array location
                                            tempArr[1] += Char.ToString(words.ElementAt(temp)); //saving char after .
                                            temp++;
                                            /*while (temp < words.Count && !Char.Equals(words.ElementAt(temp), ('+', '-', '*', '/', '<', '>', '(', ')', '{', '}', '=', '!', '[', ']', '"', ';', '.', ':')) && !Char.IsWhiteSpace(words.ElementAt(temp)))
                                            {   //appenidn characters in temp until breaker 
                                                Console.WriteLine("chars are not breakers");
                                                Console.WriteLine("processing char: " + words.ElementAt(temp));
                                                tempArr[1] += Char.ToString(words.ElementAt(temp));
                                                Console.WriteLine("tempArrr[1] is: " + tempArr[1]);
                                                temp++;
                                            }*/

                                            //78.98 walay k baad immediate dot nai print horha bas. other than that, working fine
                                            while (temp < words.Count)
                                            {
                                                if (Char.Equals(words.ElementAt(temp), '.'))
                                                {
                                                    break;
                                                }
                                                else if ((Char.Equals(words.ElementAt(temp), '+') || (Char.Equals(words.ElementAt(temp), '-') || (Char.Equals(words.ElementAt(temp), '*') || (Char.Equals(words.ElementAt(temp), '/') || (Char.Equals(words.ElementAt(temp), '<') || (Char.Equals(words.ElementAt(temp), '>') || (Char.Equals(words.ElementAt(temp), '(') || (Char.Equals(words.ElementAt(temp), ')') || (Char.Equals(words.ElementAt(temp), '[') || (Char.Equals(words.ElementAt(temp), ']') || (Char.Equals(words.ElementAt(temp), '=') || (Char.Equals(words.ElementAt(temp), '!') || (Char.Equals(words.ElementAt(temp), ';') || (Char.Equals(words.ElementAt(temp), ':'))))))))))))))))
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    //appenidn characters in temp until breaker 
                                                    Console.WriteLine("chars are not breakers");
                                                    Console.WriteLine("processing char: " + words.ElementAt(temp));
                                                    tempArr[1] += Char.ToString(words.ElementAt(temp));
                                                    Console.WriteLine("tempArrr[1] is: " + tempArr[1]);
                                                }
                                                temp++;
                                            }

                                            wordList[initial] += tempArr[0] + tempArr[1];
                                            lineNumberArr[initial] = lineNumber;
                                            initial++;
                                            j = temp;
                                            Console.WriteLine("hello there: word at j is: " + words.ElementAt(j));
                                            wordList[initial] += words.ElementAt(j);
                                            // if (Char.IsDigit(words.ElementAt(j+1)))   CHECK HERE TO SOLVE DOT PROBLEM
                                            initial++;
                                            temporaryStorage = words.ElementAt(j).ToString();



                                        }
                                        else if (!(Char.IsDigit(words.ElementAt(temp))) && !Char.IsWhiteSpace(words.ElementAt(temp)))//else if the first char after . is not a digit
                                        {
                                            Console.WriteLine("first char after . is not a digit and also not a whitespace");
                                            initial++;
                                            wordList[initial] += words.ElementAt(j);
                                            lineNumberArr[initial] = lineNumber;
                                            initial++;
                                        }


                                    }
                                    else //if char after . is a breaker
                                    {
                                        Console.WriteLine("Char after . is a breaker");
                                        initial++;
                                        wordList[initial] += words.ElementAt(j);    //append dot char with previous word
                                        lineNumberArr[initial] = lineNumber;
                                    }
                                }
                                else if (IsDigitsOnly(wordList[initial]) == false)//if previous word in wordlist[inital] is not all digits
                                {
                                    Console.WriteLine("word before . are not all digits");
                                    Console.WriteLine("current char in j is: " + words.ElementAt(j));
                                    int temp = j + 1;
                                    Console.WriteLine("char in temp is : " + words.ElementAt(temp));
                                    if (Char.IsDigit(words.ElementAt(temp)))    //if char after . is digit
                                    {
                                        Console.WriteLine("-----------------------" + words.ElementAt(j));
                                        if (wordList[initial] != null)
                                        {
                                            initial++;
                                            wordList[initial] += Char.ToString(words.ElementAt(j)) + Char.ToString(words.ElementAt(temp));
                                            lineNumberArr[initial] = lineNumber;
                                            j = temp;
                                        }
                                        else
                                        {
                                            wordList[initial] += Char.ToString(words.ElementAt(j)) + Char.ToString(words.ElementAt(temp));
                                            lineNumberArr[initial] = lineNumber;
                                            j = temp;
                                        }
                                    }
                                    else        //if char after . is not a digit
                                    {
                                        if (wordList[initial] != null)
                                        {
                                            initial++;
                                            wordList[initial] += words.ElementAt(j);
                                            lineNumberArr[initial] = lineNumber;
                                            initial++;
                                        }
                                        else
                                        {
                                            wordList[initial] += words.ElementAt(j);
                                            lineNumberArr[initial] = lineNumber;
                                            initial++;
                                        }

                                    }

                                }
                            }   //end of . case

                            //else if char is not a closing quotation...
                            else
                            {
                                if (wordList[initial] != null)
                                {
                                    initial++;


                                    if (j < words.Count - 1) //only if j + 1 exists
                                    {
                                        if (NextCharacter(words.ElementAt(j), words.ElementAt(j + 1)) == false)
                                        {
                                            wordList[initial] += words.ElementAt(j);//.ToString();
                                            lineNumberArr[initial] = lineNumber;
                                            //wordList[initial] += Char.ToString(words.ElementAt(j));
                                        }

                                        else
                                        {
                                            //wordList[initial] += words.ElementAt(j) + words.ElementAt(j+1);//.ToString();
                                            wordList[initial] += Char.ToString(words.ElementAt(j)) + Char.ToString(words.ElementAt(j + 1));
                                            lineNumberArr[initial] = lineNumber;
                                            j++;
                                        }

                                        initial++;  //doesnt break alphabets in new word immediately after op if removed
                                    }
                                    else //else if j + 1 does not exist, simply put the last word in the array
                                    {
                                        wordList[initial] += words.ElementAt(j);
                                    }

                                }

                                else
                                {
                                    if (j < words.Count - 1) ////only if j + 1 exists
                                    {
                                        //have to see if next character is present when reading from txt file warna exception dega
                                        if (NextCharacter(words.ElementAt(j), words.ElementAt(j + 1)) == false)
                                        {
                                            wordList[initial] += words.ElementAt(j);//.ToString();
                                            lineNumberArr[initial] = lineNumber;
                                            //wordList[initial] += Char.ToString(words.ElementAt(j));
                                        }
                                        else
                                        {
                                            //wordList[initial] += words.ElementAt(j) + words.ElementAt(j + 1);//.ToString();
                                            wordList[initial] += Char.ToString(words.ElementAt(j)) + Char.ToString(words.ElementAt(j + 1));
                                            lineNumberArr[initial] = lineNumber;
                                            j++;
                                        }

                                        initial++;
                                    }
                                    else  //else if j + 1 does not exist, simply put the last word in the array
                                    {
                                        wordList[initial] += words.ElementAt(j);
                                    }

                                }
                            }


                        }

                    }
                }

                else
                {
                    if (wordList[initial] != null && (Char.Equals(words.ElementAt(j), '"')))// || Char.Equals(words.ElementAt(j), '\'')))
                    {
                        initial++;
                        wordList[initial] += words.ElementAt(j);
                        lineNumberArr[initial] = lineNumber;
                    }

                    //if char is backslash then accept the very next char whatever it may be and dont check the next char in for loop
                    else if (Char.Equals(words.ElementAt(j), '\\'))
                    {
                        wordList[initial] += Char.ToString(words.ElementAt(j)) + Char.ToString(words.ElementAt(j + 1)); //accept backslash and next char
                        //wordList[initial] += Char.ToString(words.ElementAt(j + 1)); //accept only next char after backslash
                        lineNumberArr[initial] = lineNumber;
                        j++;
                    }
                    //else if its neither a backslash nor a closing quote then accept it just like any string (i.e wihtout new word)
                    else
                    {
                        wordList[initial] += words.ElementAt(j);
                        lineNumberArr[initial] = lineNumber;
                    }
                }




            } //end of for loop---------------------------------------------------------



            for (int k = 0; k < wordList.Length; k++)
            {
                Console.WriteLine(wordList[k]);
            }
            Console.WriteLine("total lines: " + lineNumber);


            for (int l = 0; l < wordList.Length; l++)
            {
                if (wordList[l] != null)
                {
                    listOfWords.Add(wordList[l]);
                }
            }

            for (int m = 0; m < lineNumberArr.Length; m++)
            {
                if (lineNumberArr[m] != 0)
                {
                    listOfLineNumber.Add(lineNumberArr[m]);
                }
            }


            Console.WriteLine("\n------------printing list of words now-----------\n");
            foreach (var item in listOfWords)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\n------------printing list of line number now-----------\n");
            foreach (var lines in listOfLineNumber)
            {
                Console.WriteLine(lines);
            }
            Console.WriteLine("ttoal words are: " + listOfWords.Count);
            Console.WriteLine("ttoal line numbers are: " + listOfLineNumber.Count);

            Tokens tk = new Tokens();
            tk.classification(listOfWords, listOfLineNumber);
            
            foreach(var token in tk.getTokens())
            {
                Console.WriteLine("(" + token.getCP() + ", " + token.getValue() + ", " + token.getLine() + ")" );
            }
            

            reader.Close();
            reader.Dispose();
            Console.WriteLine(" ");
            Console.WriteLine(Tchar.ToString() + " characters");
            Console.ReadLine();

        }
      
    }

    class Program
    {
        lexicalAnalyzer lex = new lexicalAnalyzer();


        static void Main(string[] args)
        {
            //WordBreaker();

            lexicalAnalyzer.WordBreaker();
        }
    }
         
}



















class Tokens
{
    List<Token> token;// = new List<Token>();
    public string[,] arr2d = new string[200, 3];
    public List<Token> getTokens()
    {
        return this.token;
    }
    string[] isKeywords = new string[] { "word", "number", "bool", "public", "private", "static", "class", "abstract", "final", "for", "if", "elif", "else", "function", "return", "Void", "Main" };
    string[] isPunctuators = new string[] { ";", ",", "(", ")", "[", "]", "{", "}", ":" };
    string[] isOperators = new string[] { "+", "-", "*", "/", "=", "&&", "||", "!", "<", ">", "<=", ">=", "==", "<>" };

    public Regex idRegex = new Regex (@"^[_]*[a-zA-Z]+[\w]*[a-zA-Z0-9]$");
    public Regex wordRegex = new Regex ("(^\"([a-zA-Z0-9]|[\\w]|[\\W])*\"$)|(([a-zA-Z0-9]|[\\w]|[\\W])*)|(^\'([a-zA-Z0-9]|[\\w]|[\\W])*\'$)");
    public Regex numberRegex = new Regex(@"^[-\+][0-9]+$|^[0-9]+$|^[-\+][0-9]*[.][0-9]+$|^[0-9]*[.][0-9]+$");
    public Regex r = new Regex("^[+-] ? ([0 - 9] + ([.][0 - 9] *) ?|[.][0 - 9] +)$");

    public bool isId(string id)
    {
        Match match = idRegex.Match(id);
        if (match.Success)
        {
            Console.WriteLine("is id true");
            return true;
        }
        
        else if (Char.IsLetter(id[0]))
        {
            Console.WriteLine("is id true");
            return true;
        }
        else
        {
            Console.WriteLine("is id false");
            return false;
        }
    }
    /*
    public bool isWord(string str)
    {
        Match match = wordRegex.Match(str);
        if (match.Success)
        {
            Console.WriteLine("true");
            return true;
        }
        else
        {
            Console.WriteLine("false");
            return false;
        }
    }*/

    public bool isWord(string str)
    {
        if (Regex.IsMatch(str, "^\".*\"$") || Regex.IsMatch(str, @"^'[\\]?.'$"))
        {
            Console.WriteLine("true");
            return true;
        }
        else
        {
            Console.WriteLine("false");
            return false;
        }
    }

    public bool isNumber(string number)
    {
        Match match = numberRegex.Match(number);
        if (match.Success)
        {
            Console.WriteLine("is number true");
            return true;
        }
        else
        {
            Console.WriteLine("is number false");
            return false;
        }
    }

 

    public void classification(List<string> listOfWords, List<int> listOfLineNumber)
    {
        this.token = new List<Token>();
        Console.WriteLine("running classification function");
        //string[,] arr2d = new string[200, 3];
        int k = 0;
        int lineNumber = 0;

        for (int j = 0; j < listOfWords.Count; j++)
        {
            Console.WriteLine("processing word: " + listOfWords.ElementAt(j));

            if (j < listOfLineNumber.Count)
            { lineNumber = listOfLineNumber.ElementAt(j); }
            else
            {
                if (listOfLineNumber.Count > 0)
                {
                    var item = listOfLineNumber[listOfLineNumber.Count - 1];
                    lineNumber = item;
                }
            }

            if (Char.IsLetter(listOfWords.ElementAt(j)[0]) || listOfWords.ElementAt(j).StartsWith("_") ||
                Regex.IsMatch(listOfWords.ElementAt(j), @"^[a-zA-Z]+$"))
            {
                if (isId(listOfWords.ElementAt(j)))
                {
                    if (isKeywords.Contains(listOfWords.ElementAt(j)))
                    {
                        arr2d[k, 0] = "Keyword";
                        token.Add(new Token("Keyword", listOfWords.ElementAt(j), lineNumber));
                       
                    }
                    else
                    {
                        arr2d[k, 0] = "Identifier";
                        token.Add(new Token("Identifier", listOfWords.ElementAt(j), lineNumber));
                        
                    }
                    //k++;
                }
                else
                {
                    arr2d[k, 0] = "Invalid Lexeme";
                    token.Add(new Token("Invalid Lexeme", listOfWords.ElementAt(j), lineNumber));
                   
                }
            }

            else if (Char.IsDigit(listOfWords.ElementAt(j)[0]) || Char.Equals(listOfWords.ElementAt(j)[0], '.')) 
            {
                if (isNumber(listOfWords.ElementAt(j)))
                {
                    arr2d[k, 0] = "Number";
                    token.Add(new Token("Number", listOfWords.ElementAt(j), lineNumber));
                }
                else
                {
                    arr2d[k, 0] = "Invalid Lexeme";
                    token.Add(new Token("Invalid Lexeme", listOfWords.ElementAt(j), lineNumber));
                }
               
            }
            
            else if (isWord(listOfWords.ElementAt(j)))
            {
                arr2d[k, 0] = "Word";
                token.Add(new Token("Word", listOfWords.ElementAt(j), lineNumber));
                
            }

            else if (isPunctuators.Contains(listOfWords.ElementAt(j)))
            {
                arr2d[k, 0] = listOfWords.ElementAt(j);
                token.Add(new Token(listOfWords.ElementAt(j), listOfWords.ElementAt(j), lineNumber));

            }

            else if (isOperators.Contains(listOfWords.ElementAt(j)))
            {
                Console.WriteLine("is operator : " + listOfWords.ElementAt(j));
                if (Char.Equals(listOfWords.ElementAt(j), '+') || Char.Equals(listOfWords.ElementAt(j), '-') ||
                    Char.Equals(listOfWords.ElementAt(j), '*') || Char.Equals(listOfWords.ElementAt(j), '/')
                    || listOfWords.ElementAt(j).ToString().Equals("+") || listOfWords.ElementAt(j).ToString().Equals("-")
                    || listOfWords.ElementAt(j).ToString().Equals("*") || listOfWords.ElementAt(j).ToString().Equals("/"))
                {
                    arr2d[k, 0] = "Arithmetic OP";
                    token.Add(new Token("Arithmetic OP", listOfWords.ElementAt(j), lineNumber));
                }

                else if (Char.Equals(listOfWords.ElementAt(j), '=') || listOfWords.ElementAt(j).ToString().Equals("="))
                {
                    arr2d[k, 0] = "Assignment OP";
                    token.Add(new Token("Assignment OP", listOfWords.ElementAt(j), lineNumber));
                }
               
                else if (Char.Equals(listOfWords.ElementAt(j), '<') || Char.Equals(listOfWords.ElementAt(j), '>')
                         || listOfWords.ElementAt(j).ToString().Equals("<") || listOfWords.ElementAt(j).ToString().Equals(">")
                         || listOfWords.ElementAt(j).Equals("<=") || listOfWords.ElementAt(j).Equals(">=") || 
                         listOfWords.ElementAt(j).Equals("==") || listOfWords.ElementAt(j).Equals("<>"))
                {
                    arr2d[k, 0] = "ROP";
                    token.Add(new Token("ROP", listOfWords.ElementAt(j), lineNumber));
                }

                else if (listOfWords.ElementAt(j).Equals("&&") || listOfWords.ElementAt(j).Equals("||") || listOfWords.ElementAt(j).Equals('!')
                        || listOfWords.ElementAt(j).ToString().Equals("&&") || listOfWords.ElementAt(j).ToString().Equals("||") || listOfWords.ElementAt(j).ToString().Equals("!"))
                {
                    arr2d[k, 0] = "Logical OP";
                    token.Add(new Token("Logical OP", listOfWords.ElementAt(j), lineNumber));
                }

               
            }

            else //if nothing matches then its invalid lexeme
            {
                arr2d[k, 0] = "Invalid Lexeme";
                token.Add(new Token("Invalid Lexeme", listOfWords.ElementAt(j), lineNumber));
            }

            arr2d[k, 1] = listOfWords.ElementAt(j);
            if (j < listOfLineNumber.Count)
            { arr2d[k, 2] = listOfLineNumber.ElementAt(j).ToString(); }
            else
            {
                if (listOfLineNumber.Count > 0)
                {
                    var item = listOfLineNumber[listOfLineNumber.Count - 1];
                    arr2d[k, 2] = item.ToString();
                }
            }
            k++;
        }


        for (int m = 0; m < arr2d.GetLength(0); m++)
        {
            for (int l = 0; l < arr2d.GetLength(1); l++)
            {
                Console.WriteLine("print is: " + arr2d[m, l]);
            }
        }

        List<string> finalList = new List<string>();

        for (int m = 0; m < arr2d.GetLength(0); m++)
        {
            for (int l = 0; l < arr2d.GetLength(1); l++)
            {
                if (arr2d [m, l] != null)
                {
                    finalList.Add(arr2d[m, l]);
                }
            }
        }
        Console.WriteLine("\n\n -------------printing final list now------------- \n\n");
        foreach (var item in finalList)
        {
            Console.WriteLine(item);
        }

       


        //Pass the filepath and filename to the StreamWriter Constructor
        StreamWriter sw = new StreamWriter("C:\\Users\\Sajjad\\Desktop\\lexemesList.txt");
        //Write a line of text
        for (int i = 0; i < finalList.Count; i++)
        {
            if (i%3 == 0)
            {
                sw.WriteLine("\n");
            }
            sw.WriteLine(finalList.ElementAt(i));
        }
        //Close the file
        sw.Close();
    }

}



























internal class Token
{
    string classpart;
    string valuepart;
    int line;

    public Token(string classpart, string valuepart, int line)
    {
        this.classpart = classpart;
        this.valuepart = valuepart;
        this.line = line;
    }

    public string getCP()
    {
        return this.classpart;
    }

    public string getValue()
    {
        return this.valuepart;
    }

    public int getLine()
    {
        return this.line;
    }
}










