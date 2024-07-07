using DWorks.Domain.Entities;

namespace DWorks.Tests.Mocks
{
    public static class LogMocks
    {
        public static List<Log> LogListMock()
            => new List<Log> { LogMock() };

        public static Log LogMock()
            => new Log { Entity = "Entity", New = "New", Operation = "Operation" };
    }
}
