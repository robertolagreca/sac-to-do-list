namespace TheToDoList {
    public class ToDoListContext : DbContext {
        private const string connectionString = "Data Source=localhost;Database=ToDoListDB;" +
            "Integrated Security=True;TrustServerCertificate=True";

        public DbSet<Activity> Activities { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder
                .UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Activity>()
                .HasMany(a => a.Tags)
                .WithMany(t => t.Activities);
        }
    }
}
