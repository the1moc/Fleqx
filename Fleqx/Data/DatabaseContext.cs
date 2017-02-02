using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Fleqx.Data.DatabaseModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Fleqx.Data
{
	public class DatabaseContext : IdentityDbContext<User>
	{
		public DatabaseContext()
			: base("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|FleqxDatabase.mdf;Integrated Security=True")
		{
			Database.SetInitializer(new DatabaseInitializer());
		}

		public DbSet<Announcement> Announcements { get; set; }
		public DbSet<ChatMessage> ChatMessages { get; set; }
		public DbSet<Task> Tasks { get; set; }
		public DbSet<TaskState> TaskStates { get; set; }
		public DbSet<Team> Teams { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			// Setup relationships for creating and assigning user for tasks.
			modelBuilder.Entity<Task>().HasRequired(t => t.CreatedUser)
						.WithMany(c => c.CreatedTasks)
						.HasForeignKey(t => t.CreatedUserId)
						.WillCascadeOnDelete(false);

			modelBuilder.Entity<Task>().HasRequired(t => t.AssignedUser)
						.WithMany(c => c.AssignedTasks)
						.HasForeignKey(t => t.AssignedUserId)
						.WillCascadeOnDelete(false);

			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>().ToTable("Users");
			modelBuilder.Entity<IdentityRole>().ToTable("Roles");
			modelBuilder.Entity<IdentityUserRole>().ToTable("LkUserRoles");

			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}