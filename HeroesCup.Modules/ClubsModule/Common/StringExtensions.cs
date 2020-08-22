using System.Text.RegularExpressions;

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

        public static string ToSlug(this string title)
        {
            var slug = title;
            Regex regex = new Regex(@"[^a-zA-Zа-яА-Я0-9-\s]", (RegexOptions)0);
            slug = regex.Replace(slug, "");
            slug = slug.ToLower().Replace(" - ", "-");
            slug = slug.ToLower().Replace("- ", "-");
            slug = slug.ToLower().Replace(" -", "-");
            slug = slug.ToLower().Replace("   ", " ");
            slug = slug.ToLower().Replace("  ", " ");
            slug = slug.ToLower().Replace("  ", " ");
            slug = slug.ToLower().Replace(" ", " ");
            slug = slug.ToLower().Replace(" ", "-");
            return slug;
        }
    }
}
