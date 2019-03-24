using System;
using System.Linq;

namespace RDB.DataCreator.Helpers
{
    public static class Randomize
    {
        #region Fields

        private static Random random = new Random((Int32)DateTime.Now.ToFileTime());

        #endregion

        #region Public methods

        public static Int32 Number(Int32 min, Int32 max)
        {
            return random.Next(min, max);
        }

        public static String Numeric(Int32 length)
        {
            String chars = "01234567890123456789012345678901234567890123456789";

            return new String(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static String String(Int32 length, Boolean numeric = false)
        {
            String chars = "abcdefghijklmnopqrstuvwxyZ";
            if (numeric)
                chars += "0123456789";

            String text = new String(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            return Char.ToUpper(text[0]) + text.Substring(1);
        }

        public static String Text()
        {
            return String(Number(3, 12));
        }

        public static Int32 Position(Int32 length)
        {
            return Number(0, length);
        }

        public static String Plate()
        {
            return String(7, true).Insert(3, " ");
        }

        #endregion
    }
}
