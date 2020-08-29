using System;
using System.Collections.Generic;

namespace ClubsModule.Models
{
    public class MissionIdeaListModel
    {
        public IEnumerable<MissionIdeaListItem> MissionIdeas { get; set; }
    }

    public class MissionIdeaListItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public bool IsPublished { get; set; }

        public string LastUpdateOn { get; set; }
    }
}
