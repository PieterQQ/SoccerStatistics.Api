﻿using SoccerStatistics.Api.Database.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoccerStatistics.Api.Database.Entities
{
    public class Player
    {
        public uint Id { get; set; }
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [StringLength(50)]
        [Required]
        public string Surname { get; set; }
        [Required]
        public uint Height { get; set; }
        [Required]
        public uint Weight { get; set; }
        public DateTime Birthday { get; set; }
        [StringLength(50)]
        [Required]
        public string Nationality { get; set; }
        [Required]
        public DominantLegType DominantLeg { get; set; }
        public string Nick { get; set; }
        public uint Number { get; set; }
    }
}
