﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinelAirMilesLibrary.Common.Data.Entities
{
    public class Country : IEntity
    {
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters.")]
        [Required]
        public string Name { get; set; }



        /*************OBJECT PROPERTIES*********************/


        public int Id { get; set; }


        public User CreatedBy { get; set; }


        public DateTime CreateDate { get; set; }


        public DateTime UpdateDate { get; set; }


        public User ModifiedBy { get; set; }


        public bool IsConfirm { get; set; }


        public int Status { get; set; }


    }
}
