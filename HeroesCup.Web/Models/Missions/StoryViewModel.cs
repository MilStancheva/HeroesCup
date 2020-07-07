using HeroesCup.Data.Models;
using System.Collections.Generic;

namespace HeroesCup.Web.Models
{
    public class StoryViewModel
    {
        public string Content { get; set; }

        public IEnumerable<string> ImageSources { get; set; }
    }
}
