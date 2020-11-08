using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Random_Numbers.Models
{
    public class Player: Entity
    {
        public User User { get; private set; }
        public int Number { get; private set; }
        public Player() { }
        public Player(User user)
        {
            User = user;
        }

        public void GenerateRandomNumber()
        {
            var random = new Random();

            Number = random.Next(0, 101);
        }
    }
}
