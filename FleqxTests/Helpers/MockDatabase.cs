using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fleqx.Data;
using Fleqx.Data.DatabaseModels;
using Moq;
using System.Data.Entity;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace FleqxTests.Helpers
{
    public static class MockDatabaseHelper
    {
        public static void SetDbContextInvdalid()
        {

        }

        // Mocked a database for the announcement tests.
        public static DatabaseContext MockAnnouncementDatabaseCreator()
        {
            var mockedDb = new Mock<DatabaseContext>();

            var data = new List<Announcement> {
                new Announcement{
                AnnouncementID         = 1,
                AnnouncementContent    = "Test Content 1",
                AnnouncementImportance = 3,
                AnnouncementTitle      = "Test Title 1",
                Created                = DateTime.Now.AddDays(-1),
                UserId                 = "1"
            },
            new Announcement{
                AnnouncementID         = 2,
                AnnouncementContent    = "Test Content 2",
                AnnouncementImportance = 2,
                AnnouncementTitle      = "Test Title 2",
                Created                = new DateTime().AddDays(90),
                UserId                 = "2"
            },new Announcement{
                AnnouncementID         = 3,
                AnnouncementContent    = "Test Content 3",
                AnnouncementImportance = 1,
                AnnouncementTitle      = "Test Title 3",
                Created                = DateTime.Now,
                UserId                 = "3"
            }}.AsQueryable();

            var mockedDbSet = new Mock<DbSet<Announcement>>();
            mockedDbSet.As<IQueryable<Announcement>>().Setup(m => m.Provider).Returns(data.Provider);
            mockedDbSet.As<IQueryable<Announcement>>().Setup(m => m.Expression).Returns(data.Expression);
            mockedDbSet.As<IQueryable<Announcement>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockedDbSet.As<IQueryable<Announcement>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockedDb.Setup(db => db.Announcements).Returns(mockedDbSet.Object);
            return mockedDb.Object;
        }

        // Mocked a database for the task tests.
        public static DatabaseContext MockTaskDatabaseCreator()
        {
            var mockedDb = new Mock<DatabaseContext>();

            var users = new List<User>
            {
                new User
                {
                    Id = "1"
                },
                new User
                {
                    Id = "2"
                }
            }.AsQueryable();

            var data = new List<Task> {
                new Task
                {
                    TaskID               = 1,
                    TaskTitle            = "Test Title",
                    TaskDescription      = "Test Description",
                    TaskPriority         = 1,
                    TaskState            = new TaskState(){ TaskStateID = 1 },
                    ActualDaysTaken      = 7,
                    ActualFinishDate     = DateTime.Now,
                    AssignedUserId       = "1",
                    CreatedUserId        = "1",
                    CreatedUser          = users.ElementAt(0),
                    CriticalFinishDate   = DateTime.Now,
                    LastRenewalDate      = DateTime.Now,
                    EstimatedDaysTaken   = 7,
                    OriginalCreationDate = DateTime.Now,
                    TaskStateId          = 1
                },
                new Task
                {
                    TaskID               = 2,
                    TaskTitle            = "Custom Title",
                    TaskDescription      = "Test Description",
                    TaskPriority         = 1,
                    TaskState            = new TaskState(){ TaskStateID = 1 },
                    ActualDaysTaken      = 1,
                    ActualFinishDate     = DateTime.Now,
                    AssignedUserId       = "1",
                    CreatedUserId        = "1",
                    CreatedUser          = users.ElementAt(0),
                    AssignedUser         = users.ElementAt(1),
                    CriticalFinishDate   = DateTime.Now,
                    LastRenewalDate      = DateTime.Now,
                    EstimatedDaysTaken   = 7,
                    OriginalCreationDate = DateTime.Now,
                    TaskStateId          = 1
                },
                new Task
                {
                    TaskID               = 3,
                    TaskTitle            = "Test Title Date",
                    TaskDescription      = "Test Description",
                    TaskPriority         = 2,
                    TaskState            = new TaskState(){ TaskStateID = 1 },
                    ActualDaysTaken      = 1,
                    ActualFinishDate     = DateTime.Now,
                    AssignedUserId       = "1",
                    CreatedUserId        = "1",
                    CreatedUser          = users.ElementAt(0),
                    CriticalFinishDate   = DateTime.Now,
                    LastRenewalDate      = DateTime.Now,
                    EstimatedDaysTaken   = 3,
                    OriginalCreationDate = DateTime.Now.AddDays(-30),
                    TaskStateId          = 1
                },
                new Task
                {
                    TaskID               = 4,
                    TaskTitle            = "Test Title Different User",
                    TaskDescription      = "Test Description",
                    TaskPriority         = 2,
                    TaskState            = new TaskState(){ TaskStateID = 1 },
                    ActualDaysTaken      = 1,
                    ActualFinishDate     = DateTime.Now,
                    AssignedUserId       = "2",
                    CreatedUserId        = "2",
                    CreatedUser          = users.ElementAt(1),
                    AssignedUser         = users.ElementAt(1),
                    CriticalFinishDate   = DateTime.Now,
                    LastRenewalDate      = DateTime.Now,
                    EstimatedDaysTaken   = 3,
                    OriginalCreationDate = DateTime.Now,
                    TaskStateId          = 1
                }}.AsQueryable();

            var mockedDbSet = new Mock<DbSet<Task>>();
            mockedDbSet.As<IQueryable<Task>>().Setup(m => m.Provider).Returns(data.Provider);
            mockedDbSet.As<IQueryable<Task>>().Setup(m => m.Expression).Returns(data.Expression);
            mockedDbSet.As<IQueryable<Task>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockedDbSet.As<IQueryable<Task>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockedUserSet = new Mock<DbSet<User>>();
            mockedUserSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            mockedUserSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            mockedUserSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockedUserSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            mockedDb.Setup(db => db.Tasks).Returns(mockedDbSet.Object);
            mockedDb.Setup(db => db.Users).Returns(mockedUserSet.Object);
            return mockedDb.Object;
        }

        // Mocked a database for the chat tests.
        public static DatabaseContext MockChatDatabaseContext()
        {
            var mockedDb = new Mock<DatabaseContext>();

            var data = new List<ChatMessage>
            {
                new ChatMessage
                {
                    ChatMessageID = 1,
                    ChatContent = "Test Content",
                    Created = DateTime.Now,
                    UserId = "1"
                }
            }.AsQueryable();

            var mockedChatSet = new Mock<DbSet<ChatMessage>>();
            mockedChatSet.As<IQueryable<ChatMessage>>().Setup(m => m.Provider).Returns(data.Provider);
            mockedChatSet.As<IQueryable<ChatMessage>>().Setup(m => m.Expression).Returns(data.Expression);
            mockedChatSet.As<IQueryable<ChatMessage>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockedChatSet.As<IQueryable<ChatMessage>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockedDb.Setup(db => db.ChatMessages).Returns(mockedChatSet.Object);
            return mockedDb.Object;
        }
    }
}
