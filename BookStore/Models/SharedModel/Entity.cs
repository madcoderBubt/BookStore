using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.SharedModel
{
    public class Entity
    {
        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }
    }
}
