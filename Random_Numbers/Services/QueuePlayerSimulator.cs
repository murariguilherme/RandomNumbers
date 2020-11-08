using Random_Numbers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Random_Numbers.Services
{
    public class QueuePlayerSimulator
    {
        private List<Match> matches;
        public QueuePlayerSimulator()
        {
            matches = new List<Match>();
        }
        public void AddPlayerToList(Player player)
        {
            var match = new Match(player);
            matches.Add(match);
        }

        public bool HasPlayerOnQueue()
        {
            return matches.Any();
        }

        public Match GetNextMatch()
        {            
            return matches[0];
        }

        public bool PlayerIsOnTheQueue(string username)
        {
            return matches.Any(p => p.PlayerOne.User.Username == username);
        }

        public Match GetMatchByUsername(string username)
        {
            var match = matches.FirstOrDefault(p => p.PlayerOne.User.Username == username);
            return match;
        }

        public void CancelMatchByUsername(string username)
        {
            var match = GetMatchByUsername(username);
            matches.Remove(match);
        }

        public void ClearMatches()
        {
            matches.Clear();
        }
    }
}
