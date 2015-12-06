using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DataAccess.DAO;

namespace Services.Statistics
{
    public class StatService: IStatService
    {
        private readonly EventDAO _eventDao;
        private readonly UserDAO _userDao;

        public StatService(EventDAO eventDao, UserDAO userDao)
        {
            _eventDao = eventDao;
            _userDao = userDao;
        }

        public Dictionary<string, int> GetUserBirthdayStats()
        {
            const int monthsInYear = 12;
            Dictionary<string, int> result = new Dictionary<string, int>();
            var users = _userDao.ReadAll();

            for (int i = 1; i <= monthsInYear; i++)
            {
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(i);
                int userBirthdayCountInMonth = users.Count(u => u.Birthday.Month == i);
                result[monthName] = userBirthdayCountInMonth;
            }

            return result;
        }

        public Dictionary<string, int> GetHourlyEventStats()
        {
            const int hoursInDay = 24;
            Dictionary<string, int> result = new Dictionary<string, int>();
            var events = _eventDao.ReadAll();

            for (int i = 0; i < hoursInDay; i++)
            {
                string hourRangeName = String.Format("{0}:00 - {1}:59", i, i + 1);
                int eventCountHeldInHour = events.Count(u => u.Time.Hour == i);
                result[hourRangeName] = eventCountHeldInHour;
            }

            return result;
        }
    }
}