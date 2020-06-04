namespace ClubsModule.Common
{
    public static class StringExtensions
    {
        public static string TrimInput(this string input)
        {
            return input.Trim(new char[] { '\"', '\'', '.', ' ', '“', '”' });
        }
    }
}
