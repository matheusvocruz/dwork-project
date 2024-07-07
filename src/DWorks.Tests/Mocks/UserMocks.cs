using DWorks.Domain.Entities;

namespace DWorks.Tests.Mocks
{
    public static class UserMocks
    {
        public static User UserMock()
           => new User { Id = 1, Name = "Name", Type = Domain.Enums.User.UserTypeEnum.User };

        public static User ManagerUserMock()
           => new User { Id = 1, Name = "Name", Type = Domain.Enums.User.UserTypeEnum.Manager };

        public static User UserMockWithId(long id)
          => new User { Id = id, Name = "Name", Type = Domain.Enums.User.UserTypeEnum.User };

        public static User UserMockToCreate()
           => new User { Name = "Name", Type = Domain.Enums.User.UserTypeEnum.User };
    }
}
