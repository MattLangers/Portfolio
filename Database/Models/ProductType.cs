﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class ProductType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(250)]
        public string Name { get; set; }

        public DateTime CreationDate { get; set; } =  DateTime.UtcNow;
    }
}
