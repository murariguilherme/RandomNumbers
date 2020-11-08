using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Random_Numbers.Models
{
    public class Match: Entity
    {
        public Guid PlayerOneId { get; set; }
        public Guid PlayerTwoId { get; set; }
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public DateTime CreatedIn { get; set; }
        public DateTime ExpiresIn { get; set; }
        public Match() { }
        public Match(Player playerOne)
        {
            PlayerOne = playerOne;

            CreatedIn = DateTime.Now;
            ExpiresIn = DateTime.Now.AddHours(1);
        }

        public void AddSecondPlayer(Player player)
        {
            PlayerTwo = player;
        }

        public string GetWinner()
        {
            if (PlayerOne.Number > PlayerTwo.Number)
                return PlayerOne.User.Username + " win!";

            if (PlayerTwo.Number > PlayerOne.Number)
                return PlayerOne.User.Username + " win!";
            
            return "Draw";
        }        
    }
}
