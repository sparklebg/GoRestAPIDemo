using System;
using System.Collections.Generic;
using System.Text;

namespace TestGoRestAPI
{
    public static class Utilities
    {
        private static List<Tuple<string, string>> antonyms = new List<Tuple<string, string>>();
        
        static Utilities()
        {
            antonyms.Add(new Tuple<string, string>("absent", "present"));
        }

        public static bool ToBoolean(this string value)
        {
            string valueTrimmed = value.Trim();

            foreach (Tuple<string, string> tuple in antonyms)
            {
                if (tuple.Item1.Equals(valueTrimmed))
                {
                    return false;
                }

                if (tuple.Item2.Equals(valueTrimmed))
                {
                    return true;
                }
            }

            throw new ArgumentException($@"Can`t parse the the antonym ""{ valueTrimmed }""");
        }

        public static string ToOppositeBoolean(this string input)
        {
            string valueTrimmed = input.Trim();

            foreach (Tuple<string, string> tuple in antonyms)
            {
                if (tuple.Item1.Equals(valueTrimmed))
                {
                    return tuple.Item2;
                }

                if (tuple.Item2.Equals(valueTrimmed))
                {
                    return tuple.Item1;
                }
            }

            throw new ArgumentException($@"Can`t parse the the antonym ""{ valueTrimmed }""");
        }
    }
}
