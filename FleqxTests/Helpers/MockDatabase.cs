using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		// Mocked a database for the announcement tests.
		public static DatabaseContext MockAnnouncementDatabaseCreator()
		{
			var mockedDb = new Mock<DatabaseContext>();

			var data = new List<Announcement> {
				new Announcement{
				AnnouncementID = 1,
				AnnouncementContent = "Test Content",
				AnnouncementImportance = 3,
				AnnouncementTitle = "Test Title",
				Created = DateTime.Now,
				UserId = "1"
			}}.AsQueryable();

			var mockedDbSet = new Mock<DbSet<Announcement>>();
			mockedDbSet.As<IQueryable<Announcement>>().Setup(m => m.Provider).Returns(data.Provider);
			mockedDbSet.As<IQueryable<Announcement>>().Setup(m => m.Expression).Returns(data.Expression);
			mockedDbSet.As<IQueryable<Announcement>>().Setup(m => m.ElementType).Returns(data.ElementType);
			mockedDbSet.As<IQueryable<Announcement>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			mockedDb.Setup(db => db.Announcements).Returns(mockedDbSet.Object);
			return mockedDb.Object;
		}
	}
}
