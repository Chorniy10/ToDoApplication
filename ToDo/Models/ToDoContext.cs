using Microsoft.EntityFrameworkCore;

namespace ToDoDem.Models
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options)
        : base(options)
        {
        }
        public DbSet<ToDo> ToDos { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = "work", Name = "Робота" },
                new Category { CategoryId = "personal", Name = "Особисте" },
                new Category { CategoryId = "shopping", Name = "Покупки" },
                new Category { CategoryId = "others", Name = "Інше" }
                );

            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = "open", Name = "В процесі" },
                new Status { StatusId = "completed", Name = "Завершене" }
                );
        }
    }
}
