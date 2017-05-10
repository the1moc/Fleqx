using System;
using System.Collections.Generic;
using System.Data.Entity;
using Fleqx.Data.DatabaseModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Fleqx.Data
{
    /// <summary>
    /// Initialize the database if it has not been created.
    /// </summary>
    /// <seealso cref="System.Data.Entity.CreateDatabaseIfNotExists{Fleqx.Data.DatabaseContext}" />
    public class DatabaseInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            try
            {
                // Add the team types
                List<Team> teams = new List<Team>
                {
                    new Team {TeamID = 1, TeamTitle = "Group One" },
                    new Team {TeamID = 2, TeamTitle = "Group Two" },
                    new Team {TeamID = 3, TeamTitle = "Group Three" }
                };

                teams.ForEach(team => context.Teams.Add(team));

                context.SaveChanges();

                // Add the admin user.
                PasswordHasher pHasher = new PasswordHasher();
                string hashedPassword = pHasher.HashPassword("password");
                context.Users.Add(new User
                {
                    Id = "1",
                    UserName = "admin",
                    FirstName = "Admin",
                    LastName = "Jones",
                    PasswordHash = hashedPassword,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    TeamId = 1,
                    IsLoggedIn = 0
                });

                context.SaveChanges();

                List<Activity> activities = new List<Activity>
                {
                    new Activity
                    {
                        ActivityContent = "Created an Announcement",
                        User = context.Users.Find("1"),
                        Date = DateTime.Now
                    },
                    new Activity
                    {
                        ActivityContent = "Created a Task",
                        User = context.Users.Find("1"),
                        Date = DateTime.Now
                    },
                     new Activity
                    {
                        ActivityContent = "Added a new user",
                        User = context.Users.Find("1"),
                        Date = DateTime.Now
                    }
                };

                activities.ForEach(activity => context.Activities.Add(activity));
                context.SaveChanges();

                // Add the roles.
                List<IdentityRole> roles = new List<IdentityRole>
                {
                    new IdentityRole {Id = "1", Name = "Admin"},
                    new IdentityRole {Id = "2", Name = "Standard"},
                    new IdentityRole {Id = "3", Name = "Guest"}
                };

                roles.ForEach(role => context.Roles.Add(role));
                context.SaveChanges();

                // Add the first user to the admin role.
                UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));
                userManager.AddToRole("1", "Admin");

                // Add the first announcement.
                List<Announcement> announcements = new List<Announcement>
                {
                    new Announcement
                    {
                        AnnouncementID      = 1,
                        UserId              = context.Users.Find("1").Id,
                        AnnouncementTitle   = "Welcome",
                        AnnouncementContent =
                            "This is the first announcement in the database. Please add further announcements through the add announcement button.",
                        Created                = DateTime.Now,
                        AnnouncementImportance = 5
                    }
                };

                announcements.ForEach(announcement => context.Announcements.Add(announcement));
                context.SaveChanges();

                // Add the first chat message.
                context.ChatMessages.Add(new ChatMessage
                {
                    ChatContent = "This is the first message sent!",
                    Created = DateTime.Now,
                    UserId = "1"
                });

                context.SaveChanges();

                // Add the task states.
                List<TaskState> taskStates = new List<TaskState>
                {
                    new TaskState {TaskStateID = 1, TaskStateCurrent = "Open" },
                    new TaskState {TaskStateID = 2, TaskStateCurrent = "Active" },
                    new TaskState {TaskStateID = 3, TaskStateCurrent = "Closed" }
                };

                taskStates.ForEach(taskState => context.TaskStates.Add(taskState));
                context.SaveChanges();

                // Add the tasks.
                context.Tasks.Add(new Task
                {
                    TaskID = 1,
                    TaskTitle = "Add More Tasks",
                    TaskDescription = "I am open!",
                    TaskPriority = 1,
                    OriginalCreationDate = DateTime.Now,
                    CriticalFinishDate = DateTime.Now.AddDays(7),
                    ActualFinishDate = new DateTime(2050, 1, 1),
                    LastRenewalDate = DateTime.Now,
                    EstimatedDaysTaken = 1,
                    CreatedUserId = "1",
                    AssignedUserId = "1",
                    TaskStateId = 1,
                    TaskStartedDate = new DateTime(2050, 1, 1)
                });

                // Add the tasks.
                context.Tasks.Add(new Task
                {
                    TaskID = 2,
                    TaskTitle = "Add More Tasks",
                    TaskDescription = "I am active!",
                    TaskPriority = 1,
                    OriginalCreationDate = DateTime.Now,
                    CriticalFinishDate = DateTime.Now.AddDays(7),
                    ActualFinishDate = new DateTime(2050, 1, 1),
                    LastRenewalDate = DateTime.Now,
                    EstimatedDaysTaken = 1,
                    CreatedUserId = "1",
                    AssignedUserId = "1",
                    TaskStateId = 2,
                    TaskStartedDate = DateTime.Now
                });

                // Add the tasks.
                context.Tasks.Add(new Task
                {
                    TaskID = 3,
                    TaskTitle = "Add More Tasks",
                    TaskDescription = "I am closed!",
                    TaskPriority = 1,
                    OriginalCreationDate = DateTime.Now,
                    CriticalFinishDate = DateTime.Now.AddDays(7),
                    ActualFinishDate = new DateTime(2050, 1, 1),
                    LastRenewalDate = DateTime.Now,
                    EstimatedDaysTaken = 1,
                    ActualDaysTaken = 1,
                    CreatedUserId = "1",
                    AssignedUserId = "1",
                    TaskStateId = 3,
                    TaskStartedDate = DateTime.Now.AddDays(6)
                });

                // Add the tasks.
                context.Tasks.Add(new Task
                {
                    TaskID = 4,
                    TaskTitle = "Clean the kitchen",
                    TaskDescription = "Participate in the cleaning of the kitchen",
                    TaskPriority = 1,
                    OriginalCreationDate = DateTime.Now,
                    CriticalFinishDate = DateTime.Now.AddDays(4),
                    ActualFinishDate = new DateTime(2050, 1, 1),
                    LastRenewalDate = DateTime.Now,
                    EstimatedDaysTaken = 4,
                    ActualDaysTaken = 1,
                    CreatedUserId = "1",
                    AssignedUserId = "1",
                    TaskStateId = 1,
                    TaskStartedDate = DateTime.Now.AddDays(1)
                });

                // Add the tasks.
                context.Tasks.Add(new Task
                {
                    TaskID = 5,
                    TaskTitle = "Eat some food",
                    TaskDescription = "Go help eat food!",
                    TaskPriority = 4,
                    OriginalCreationDate = DateTime.Now,
                    CriticalFinishDate = DateTime.Now.AddDays(4),
                    ActualFinishDate = DateTime.Now.AddDays(5),
                    LastRenewalDate = DateTime.Now,
                    EstimatedDaysTaken = 19,
                    ActualDaysTaken = 2,
                    CreatedUserId = "1",
                    AssignedUserId = "1",
                    TaskStateId = 2,
                    TaskStartedDate = DateTime.Now.AddDays(1)
                });

                context.SaveChanges();

                context.Files.Add(
                    new DataFile
                    {
                        DataFileId = 1,
                        UserId = "1",
                        FilePath = "Files/Test.txt",
                        FileName = "Test.txt"
                    }
                );
            }
            catch (Exception e)
            {
                throw new Exception("Something has gone wrong when trying to initialise the database.", e.InnerException);
            }
        }
    }
}