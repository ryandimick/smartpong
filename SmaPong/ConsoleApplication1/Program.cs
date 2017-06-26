using System;
using System.Collections.Generic;
using SmartPong;
using SmartPong.Models;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = RepositoryManager.Create("server=(local);database=SmartPong;Trusted_Connection=Yes;TransparentNetworkIPResolution=False;MultipleActiveResultSets=True");

            repository.DeleteMatch(2);

            //repository.DeleteMatch(1005);

            var test = repository.ConfirmMatch(2, 9);

            var match = new Match(MatchType.Type.Singles, new DateTime(2016, 6, 14,6, 0, 0));

            var ryan = new User { UserId = 9 }; // really this would be a fetch
            var cordy = new User { UserId = 13 }; // really this would be a fetch

            match.AddTeam(1, new List<User> { ryan });
            match.AddTeam(2, new List<User> { cordy });
            match.SetOutcome(1);

            repository.CreateMatch(match);
        }
    }
}
