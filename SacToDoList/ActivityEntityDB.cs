using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheToDoList {
    [Table("activity")]
    public class ActivityEntityDB {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public bool Status { get; set; } = false;

    }
}
