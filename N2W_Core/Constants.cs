using Microsoft.Win32.SafeHandles;

namespace N2W_Core
{
    internal static class Constants
    {
        internal static char CommaSeperator = ',';
        internal static char TensSeperator = '-';
        internal static string DollarStr = " dollar";
        internal static string DollarsStr = " dollars";
        internal static string CentStr = " cent";
        internal static string CentsStr = " cents";
        internal static string JoinerStr = " and ";
        internal static string ErrorStr = "Error Occured: ";

        internal static List<string> Units = new()
        {
            "zero",
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine",
            "ten",
            "eleven",
            "twelve",
            "thirteen",
            "fourteen",
            "fifteen",
            "sixteen",
            "seventeen",
            "eighteen",
            "nineteen",
        };

        internal static List<string> Tens = new()
        {
            String.Empty,
            String.Empty,
            "twenty",
            "thirty",
            "forty",
            "fifty",
            "sixty",
            "seventy",
            "eighty",
            "ninety",
        };

        internal static List<Tuple<long, string>> Magnitudes = new()
        {
            new Tuple<long, string>(10, "-"),
            new Tuple<long, string>(100, " hundred"),
            new Tuple<long, string>(1000, " thousand"),
            new Tuple<long, string>(1000000, " million"),
            new Tuple<long, string>(1000000000, " billion"),
            new Tuple<long, string>(1000000000000, " trillion"),
            new Tuple<long, string>(1000000000000000, " quadrillion"),
            new Tuple<long, string>(1000000000000000000, " quintillion"),
            // Just add more here to support bigger numbers
        };
    }
}