﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace University.Models.Data
{
    [Table("titel")]
    public partial class Titel
    {
        [Key]
        [Column("titleID")]
        [StringLength(2)]
        public string TitleId { get; set; }
        [Column("titleName")]
        [StringLength(50)]
        public string TitleName { get; set; }
    }
}