using Microsoft.EntityFrameworkCore;
using StackAlchemy_Back.Models;

public class StackContext : DbContext
{
    public StackContext(DbContextOptions<StackContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Score> Scores { get; set; }


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

        modelBuilder.Entity<Score>()
        .HasOne(us => us.User)
        .WithMany(u => u.Scores)
        .HasForeignKey(us => us.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Score>()
            .HasOne(us => us.Answer)
            .WithMany(a => a.Scores)
            .HasForeignKey(us => us.AnswerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Score>()
            .HasOne(us => us.Question)
            .WithMany(q => q.Scores)
            .HasForeignKey(us => us.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

    }

}
