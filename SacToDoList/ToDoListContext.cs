using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheToDoList
{
    public class ToDoListContext: DbContext
    {
        public DbSet<Activity> Activities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Database=ToDoListDB;" +
                                        "Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
