using System;
using System.Linq;

namespace CeasorsCrypt_lab1
{
    class Program
    {
        private const String usageMessage = "\nUsage : </c | /d> <key> \"<message>\"";
        private const String methodMessage = "\nargs must be /c(rypt) or /d(ecrypt";
        private const String keyMessage = "\nKey must be int number in range of alphabet.symbols.count (like 1 to 25 in English)";

        private static char[] alphabet = {'a', 'b', 'c', 'd', 'e',
                                          'f', 'g' ,'h' , 'i', 'j',
                                          'k' , 'l', 'm', 'n', 'o' ,
                                          'p' , 'q', 'r', 's', 't',
                                          'u' , 'v' , 'w', 'x', 'y',
                                          'z' , ' '};//last is space
        private static string mode = "";
        private static int key = 0;
        private static char[] keyPharse = { };//holding by-char text we should crypt or de-crypt
        private static char[] bufferChar = { };//see below

        private static bool checkResult = false;

        static void Main()
        {

            argsCheck();//enter valid values or close it
            if (checkResult == false)
            {

                Environment.Exit(0);
            }
            if (mode == "/c")//crypt
            {
                encrypt(keyPharse , key);
                if (bufferChar != keyPharse)
                    printBuffer();
                else
                    Console.WriteLine("Incomming and outcomming arrays are the same. Report this error.");
            }
            else//decrypt
            {
                decrypt(keyPharse , key);
                if (bufferChar != keyPharse)
                    printBuffer();
                else
                    Console.WriteLine("Incomming and outcomming arrays are the same. Report this error.");
            }
            Console.WriteLine();
            Console.ReadLine();
        }

        private static void decrypt(char[] cryptText , int key)
        {
            for (int i = 0; i < cryptText.Length; i++)
            {
                int buffer = (((int)cryptText[i] - (int)'a' - key) % 26) + (int)'a';
                bufferChar[i] = (char)buffer;
            }
        }

        private static void encrypt(char[] cryptText , int key)
        {//lowercase english alphabet is 97 to 122
            for (int i = 0; i < cryptText.Length; i++)
            {
                int buffer = (((int)cryptText[i] - (int)'a' + key + 2) % 26) + (int)'a';
                bufferChar[i] = (char)buffer;
            }
        }

        private static void printBuffer()
        {
            foreach (char c in bufferChar) Console.Write(c);
        }
        /// <summary>
        /// Check incomming string and make it lowercased. Set checkResult variable to true or false.
        /// </summary>
        private static void argsCheck()
        {
            ///args[1] is crypt/decrypt - /c or /d command
            ///args[2] is keyword for crypting or decrypting
            ///args[3] is keypharse
            String[] args = Environment.GetCommandLineArgs();

            for (int i = 1; i < args.Length; i++)//start from [1] becouse [0] is application's name
            {
                //Console.WriteLine("args[" + i + "] : " + args[i]);
                if (!String.IsNullOrEmpty(args[i]))//first is args valid check
                {
                    continue;
                }
                else
                {
                    Console.WriteLine(usageMessage);
                    checkResult = false;
                    return;
                }
            }

            if (args[1] != "/d" && args[1] != "/c")
            {
                Console.WriteLine(methodMessage);
                checkResult = false;
                return;
            } else
            {
                mode = args[1];
            }

            if(args[2].Length == 1)
            {
                int checkNumber = 0;
                bool keyResult = Int32.TryParse(args[2], out checkNumber);
                if (!keyResult || (checkNumber < 1 && checkNumber > 26))
                {
                    Console.WriteLine(keyMessage);
                    checkResult = false;
                    return;
                } else
                {
                    key = checkNumber;
                }
            }
            
            try//a lot of errors with strings, so we should know about it
            {
                args[3] = args[3].ToLower();//lowercasing it
                bufferChar = args[3].ToCharArray();

                var result = from ch in args[3].ToCharArray()
                             join ch1 in alphabet on ch equals ch1
                             select ch;

                args[3] = new String(result.ToArray());
                keyPharse = args[3].ToCharArray();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Unknown exception : " + ex.ToString());
                checkResult = false;
                return;
            };

            checkResult = true;//all is OK
        }
    }
}
