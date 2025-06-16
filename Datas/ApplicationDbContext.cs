using ForumWebsite.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumWebsite.Datas
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options){}
        public DbSet<User> Users { get; set; }
        public DbSet<ForumThread> ForumThreads { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ForumThreadVote> ForumThreadVotes { get; set; }
        public DbSet<CommentVote> CommentVotes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
              .HasOne(c => c.ParentComment)
              .WithMany(c => c.Replies)
              .HasForeignKey(c => c.ParentCommentId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ForumThread)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.ThreadId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c=>c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CommentVote>()
                .HasKey(cv => new { cv.UserId, cv.CommentId }); 

            modelBuilder.Entity<CommentVote>()
                .HasOne(cv => cv.User)
                .WithMany(u => u.CommentVotes)
                .HasForeignKey(cv => cv.UserId);

            modelBuilder.Entity<CommentVote>()
                .HasOne(cv => cv.Comment)
                .WithMany(c => c.Votes)
                .HasForeignKey(cv => cv.CommentId);

            modelBuilder.Entity<ForumThread>()
                .HasOne(ft => ft.User)
                .WithMany(u => u.ForumThreads)
                .HasForeignKey(ft => ft.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ForumThread>()
                .HasOne(ft => ft.Topic)
                .WithMany(t => t.ForumThreads)
                .HasForeignKey(ft => ft.TopicId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ForumThreadVote>()
                .HasKey(fv => new { fv.UserId , fv.ForumThreadId});

            modelBuilder.Entity<ForumThreadVote>()
                .HasOne(fv => fv.User)
                .WithMany(u => u.ForumThreadVotes)
                .HasForeignKey(fv => fv.UserId);

            modelBuilder.Entity<ForumThreadVote>()
                .HasOne(fv => fv.ForumThread)
                .WithMany(c => c.Votes)
                .HasForeignKey(fv => fv.ForumThreadId);
        }
    }
}
