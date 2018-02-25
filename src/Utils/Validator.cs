using System;
using System.Text.RegularExpressions;

namespace Multilang.Utils 
{
    class Validator
    {
        // validation regext
        public const string ALPHA = "^[a-zA-Z()]+$";
        public const string ALPHA_SPACE = "^[a-zA-Z() ]+";
        public const string NUMERIC = "^[0-9]+$";
        public const string HEX = "^[a-f0-9]+$";
        public const string BINARY = "^[01]+$";
        public const string ALPHA_NUM = "^([0-9]|[a-z])+([0-9a-z]+)$";
        public const string ALPHA_NUM_STRICT = "(?=.*[0-9])(?=.*([A-Z]|[a-z]))";

        public static bool NullOrEmpty(string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static bool WhiteSpace(string str) 
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        public static bool Present(string str) 
        {
            return NullOrEmpty(str) && WhiteSpace(str);
        }

        public static bool Alpha(string str) 
        {
            return new Regex(ALPHA).IsMatch(str);
        }

        public static bool AlphaSpace(string str)
        {
            return new Regex(ALPHA_SPACE).IsMatch(str) && WhiteSpace(str);
        }

        public  static bool Numeric(string str)
        {
            return new Regex(NUMERIC).IsMatch(str);
        }

        public  static bool Hex(string str)
        {
            return new Regex(HEX).IsMatch(str);
        }

        public  static bool Binary(string str)
        {
            return new Regex(BINARY).IsMatch(str);
        }

        public static bool AlphaNum(string str)
        {
            return new Regex(ALPHA_NUM).IsMatch(str);
        }

        public static bool AlphaNumStrict(string str)
        {
            return new Regex(ALPHA_NUM_STRICT).IsMatch(str);
        }

        public static bool Email(string str)
        {
            return (new EmailVerifier()).IsValidEmail(str);
        }

        /*
        Criteria:
        - Atleast 8 characters long
        - Contains atleast one uppercase character
        - Contains atleast one lower case character
        - Contains atleast one number
        - Contains atleast one special character
            */
        public static bool Password(string str)
        {
            return str.Length >= 8
                    && new Regex("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[/*-+_@&$#%])").IsMatch(str);
        }
    }
}