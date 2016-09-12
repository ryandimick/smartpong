using System.Linq;
using System.Web.Mvc;

namespace SmartPong.Controllers
{
    public class MatchesController : Controller
    {
        public ActionResult Index()
        {
            var matchStatus = Global.DbContext.MatchStatuses.ToList();
            var matchTypes = Global.DbContext.MatchTypes.ToList();
            var matchRatings = Global.DbContext.MatchRatings.ToList();
            var matchParticipants = Global.DbContext.MatchParticipants.ToList();
            var matches = Global.DbContext.Matches.Where(m => m.Status >= 2).OrderByDescending(m => m.MatchDate);
            return View(matches);
        }
    }
}