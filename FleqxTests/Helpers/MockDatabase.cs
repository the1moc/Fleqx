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

            var data = new List<Task> {
                new Task
                {
                    TaskID               = 1,
                    TaskTitle            = "Test Title",
                    TaskDescription      = "Test Description",
                    TaskPriority         = 1,
                    TaskState            = new TaskState(){ TaskStateID = 1 },
                    ActualDaysTaken      = 1,
                    ActualFinishDate     = DateTime.Now,
                    AssignedUserId       = "2",
                    CreatedUserId        = "1",
                    CriticalFinishDate   = DateTime.Now,
                    LastRenewalDate      = DateTime.Now,
                    EstimatedDaysTaken   = 7,
                    OriginalCreationDate = DateTime.Now,
                    TaskStateId          = 1
                },
                new Task
                {
                    TaskID               = 1,
                    TaskTitle            = "Test Title",
                    TaskDescription      = "Test Description",
                    TaskPriority         = 1,
                    TaskState            = new TaskState(){ TaskStateID = 1 },
                    ActualDaysTaken      = 1,
                    ActualFinishDate     = DateTime.Now,
                    AssignedUserId       = "2",
                    CreatedUserId        = "1",
                    CriticalFinishDate   = DateTime.Now,
                    LastRenewalDate      = DateTime.Now,
                    EstimatedDaysTaken   = 7,
                    OriginalCreationDate = DateTime.Now,
                    TaskStateId          = 1
                }}.AsQueryable();

            var mockedDbSet = new Mock<DbSet<Task>>();
            mockedDbSet.As<IQueryable<Task>>().Setup(m => m.Provider).Returns(data.Provider);
            mockedDbSet.As<IQueryable<Task>>().Setup(m => m.Expression).Returns(data.Expression);
            mockedDbSet.As<IQueryable<Task>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockedDbSet.As<IQueryable<Task>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockedDb.Setup(db => db.Tasks).Returns(mockedDbSet.Object);
            return mockedDb.Object;
        }
    }
}
