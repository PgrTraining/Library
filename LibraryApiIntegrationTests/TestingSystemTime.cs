using LibraryApi.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApiIntegrationTests
{
    public class TestingSystemTime : ISystemTime
    {
        public DateTime GetCurrentTime()
        {
            return new DateTime(1969, 4, 20, 23, 59, 59);
        }
    }
}
