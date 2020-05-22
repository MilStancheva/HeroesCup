using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeroesCup.Data.Models
{
    public class Hero
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Guid ClubId { get; set; }
        public Club Club { get; set; }

        public bool IsCoordinator { get; set; }

        public ICollection<HeroMission> HeroMissions { get; set; }
    }
}