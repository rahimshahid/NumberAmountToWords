using System.Text.RegularExpressions;

namespace N2W_Core
{
    public class AmountConverter
    {
        public static string Convert(string amount) 
        {
            try
            {
                if (String.IsNullOrEmpty(amount))
                    throw new ArgumentException("No input provided.");

                // Remove all whitespaces & dots
                var amount_clean = Regex.Replace(amount, @"\s+|\.", ""); 

                // Split string to integer and decimal amount
                var split = amount_clean.Split(Constants.CommaSeperator).ToList();
                long integer_part = ParseLong(split[0]);
                long decimal_part = split.Count == 2 ? ParseDecimal(split[1]) : 0;

                // Convert integer (and the decimal part if required)
                string words = LongToWords(integer_part) + DollarPostfix(integer_part); // int part
                words += decimal_part == 0 ? String.Empty : Constants.JoinerStr + LongToWords(decimal_part) + CentPostfix(decimal_part); // decimal part
                return words;
            }
            catch (Exception ex) 
            {
                // We can throw this exception after logging to be handled by the Data Server application,
                // But in case this code is used without the server, we want a clean error string to be returned.
                return Constants.ErrorStr + ex.Message;
            }  
        }

        /// <summary>
        /// Recursive method to convert a positive integer to its word form.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private static string LongToWords(long num) 
        {
            if (num < 0) 
            {
                throw new ArgumentException("Negative amounts not supported");
            }

            if (num < 20) // Special Case -> eleven, twelve,... nineteen
            {
                return Constants.Units[(int)num];
            }
            else if (num < 100) // Special Case -> twenty-two, seventy-seven,...
            {
                var tensPart = num / 10;
                var remainingNumber = num % 10;
                return Constants.Tens[(int)tensPart] + 
                    (remainingNumber == 0 ? String.Empty : Constants.TensSeperator + LongToWords(remainingNumber));
            }
            else // General case
            {
                var magnitude = GetMagnitude(num); // e.g. thousand, million, billion, quadrillion etc.
                var magnitudePart = num / magnitude.Item1; // Item1 = value of magnitude (1000), Item2 = string representation of magnitude (thousand)
                var remainingNumber = num % magnitude.Item1;
                return LongToWords(magnitudePart) + magnitude.Item2 + 
                    (remainingNumber == 0 ? String.Empty : " " + LongToWords(remainingNumber));
            }
        }

        #region Utils
        private static long ParseLong(string str) 
        {
            return !String.IsNullOrEmpty(str) ? Int64.Parse(str) : 0;
        }
        private static long ParseDecimal(string str)
        {
            if(String.IsNullOrEmpty(str))
                return 0;

            if (str.Length == 1)
                return Int64.Parse(str[0] + "") * 10;
            else if (str.Length == 2)
                return Int64.Parse(str);
            else
                throw new ArgumentException("Incorrect decimal part. Decimal part cannot be larger than 99.");
        }
        private static string DollarPostfix(long dollars) 
        {
            return (dollars == 1 ? Constants.DollarStr : Constants.DollarsStr);// Correct postfix for singular and plural
        }
        private static string CentPostfix(long cents)
        {
            return (cents == 1 ? Constants.CentStr : Constants.CentsStr);// Correct postfix for singular and plural
        }
        private static Tuple<long, string> GetMagnitude(long num)
        {
            int i = 1;
            for (; i < Constants.Magnitudes.Count; i++)
            {
                Tuple<long, string>? mag = Constants.Magnitudes[i];
                if (num < mag.Item1)
                    return Constants.Magnitudes[i-1];
            }
            throw new ArgumentException("Amounts larger than a " + Constants.Magnitudes[i].Item2 + " are not supported");
        }
        #endregion
    }
}
