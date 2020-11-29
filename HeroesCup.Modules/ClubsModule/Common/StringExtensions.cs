using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ClubsModule.Common
{
    public static class StringExtensions
    {
        private static IDictionary<string, string> CYRYLLIC_LATIN_LETTERS = new Dictionary<string, string>() {
            {"а", "a" },
            {"б", "b" },
            {"в", "v" },
            {"г", "g" },
            {"д", "d" },
            {"е", "e" },
            {"ж", "zh" },
            {"з", "z" } ,
            {"и", "i"},
            {"й", "i"},
            {"к", "k"},
            {"л", "l"},
            {"м", "m"},
            {"н", "n"},
            {"о", "o"},
            {"п", "p"},
            {"р", "r"},
            {"с", "s"},
            {"т", "t"},
            {"у", "u"},
            {"ф", "f"},
            {"х", "h"},
            {"ц", "c"},
            {"ч", "ch" },
            {"ш", "sh" },
            {"щ", "sht" },
            {"ъ", "u" },
            {"ю", "yu" },
            {"я", "ya" },
            {"А", "A" },
            {"Б", "B"},
            {"В", "V"},
            {"Г", "G"},
            {"Д", "D"},
            {"Е", "E"},
            {"Ж", "Zh" },
            {"З", "Z" },
            {"И", "I"},
            {"Й", "I"},
            {"К", "K"},
            {"Л", "L"},
            {"М", "M"},
            {"Н", "N"},
            {"О", "O"},
            {"П", "P"},
            {"Р", "R"},
            {"С", "S"},
            {"Т", "T"},
            {"У", "U"},
            {"Ф", "F"},
            {"Х", "H"},
            {"Ц", "C" },
            {"Ч", "Ch" },
            {"Ш", "Sh" },
            {"Щ", "Sht" },
            {"Ъ", "U" },
            {"Ю", "Yu"},
            {"Я", "Ya"}
        };

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
            if (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title))
            {
                return title;
            }

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

        public static string Unidecode(this string text)
        {
            if (text == null || text.Length == 0)
            {
                return null;
            }

            var result = text;
            foreach (KeyValuePair<string, string> pair in CYRYLLIC_LATIN_LETTERS)
            {
                result = result.Replace(pair.Key, pair.Value);
            }

            return result;
        }
    }
}
