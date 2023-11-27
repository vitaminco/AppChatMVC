using Microsoft.EntityFrameworkCore;
namespace AppChatMVC.Entities
{
    public class AppChatDbContext : DbContext
    {
        public DbSet<AppAddFriendTicket> AppAddFriendTickets { get; set; }
        public DbSet<AppAttachment> AppAttachments { get; set; }
        public DbSet<AppConversation> AppConversations { get; set; }
        public DbSet<AppConversationUserSeen> AppConversationUserSeens { get; set; }
        public DbSet<AppFriend> AppFriends { get; set; }
        public DbSet<AppMessage> AppMessages { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserConversation> AppUserConversations { get; set; }

        public AppChatDbContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //cấu hình bảng Appuser 
            modelBuilder.Entity<AppUser>()
                .Property(m => m.Password)
                .HasMaxLength(200);
            modelBuilder.Entity<AppUser>()
                .Property(m=>m.Username)
                .HasMaxLength(200);
            modelBuilder.Entity<AppUser>()
                .Property(m=>m.Avatar)
                .HasMaxLength(300);
            modelBuilder.Entity<AppUser>()
                .Property(m=>m.DisplayName) 
                .IsRequired(true)
                .HasMaxLength(100);
            //ràng buộc unique
            modelBuilder.Entity<AppUser>()
                .HasIndex(m => m.Username)
                .IsUnique(true);
        }
    }
}
