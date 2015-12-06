using System.Collections.Generic;

namespace Services.Statistics
{
    public interface IStatService
    {
        Dictionary<string, int> GetUserBirthdayStats
            ();
        Dictionary<string, int> GetHourlyEventStats
            ();
    }
}
