using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacToDoList
{
    [Table("activity")]
    public class ActivityEntityDB
    { 
            [Key]
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }
            public string Description { get; set; }
            [Required]
            public DateTime Date_start { get; set; }
            public DateTime Date_end { get; set; }
            [Required]
            public string Status { get; set; }
        
    }
}
