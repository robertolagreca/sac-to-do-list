using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SacToDoList
{
    public class ToDoListContext: DbContext
    {
        public DbSet<ActivityEntityDB> ActivityEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Database=ToDoListDB;" +
"Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
