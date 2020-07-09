namespace ClubsModule.Common
{
    public static class StringExtensions
    {
        public static string TrimInput(this string input)
        {
            if (input != null)
            {
                return input.Trim(new char[] { '\"', '\'', '.', ' ', '“', '”' });
            }

            return null;
        }
    }
}
