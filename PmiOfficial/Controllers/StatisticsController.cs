using System;
using System.Web.Mvc;
using DataAccess.DAO;
using Services.Statistics;

namespace PmiOfficial.Controllers
{
    //[Authorize] - публічно доступне через проблеми з реєстрацією
    public class StatisticsController : Controller
    {
        private readonly IStatService _statService;

        public StatisticsController()
        {
            _statService = new StatService(new EventDAO(), new UserDAO());
        }

        public ActionResult Index()
        {
            var userStats = _statService.GetUserBirthdayStats();

            ViewBag.UserStatsLabels = String.Join(", ", userStats.Keys);;
            ViewBag.UserStatsData = String.Join(", ", userStats.Values);

            var eventStats = _statService.GetHourlyEventStats();

            ViewBag.EventStatsLabels = String.Join(", ", eventStats.Keys);
            ViewBag.EventStatsData = String.Join(", ", eventStats.Values);

            return View();
        }
    }
}
