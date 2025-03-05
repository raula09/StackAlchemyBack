using Microsoft.EntityFrameworkCore;
using StackAlchemy_Back.Models;

public class StackContext : DbContext
{
    public StackContext(DbContextOptions<StackContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Answer>()
       .HasOne(a => a.User)
       .WithMany(u => u.Answers)
       .HasForeignKey(a => a.User_Id)
       .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Answer>()
        .HasOne(a => a.Question)
        .WithMany(q => q.Answers)
        .HasForeignKey(a => a.Question_Id)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Question>()
        .HasOne(q => q.User)
        .WithMany(u => u.Questions)
        .HasForeignKey(u => u.User_Id)
        .OnDelete(DeleteBehavior.Restrict);


    }

}
