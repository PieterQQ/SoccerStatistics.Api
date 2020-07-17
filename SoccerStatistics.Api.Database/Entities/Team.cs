﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Team
    {
        public uint Id { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        [StringLength(50)]
        public string ShortName { get; set; }
        public uint CreatedAt { get; set; }
        [Required]
        [StringLength(50)]
        public string Coach { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [Required]
        public virtual IEnumerable<Player> Players { get; set; }
        [ForeignKey("StadiumId")]
        public virtual Stadium Stadium { get; set; }
        [NotMapped]
        public virtual TeamInMatchStats TeamInMatchStats { get; set; }
        [NotMapped]
        public virtual League League { get; set; }
        [NotMapped]
        [InverseProperty("DestTeam")]
        public virtual IEnumerable<Transfer> Transfer1 { get; set; }
        [NotMapped]
        [InverseProperty("SourceTeam")]
        public virtual IEnumerable<Transfer> Transfer2 { get; set; }

    }
}
