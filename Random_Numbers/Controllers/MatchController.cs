using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Random_Numbers.Data;
using Random_Numbers.Models;
using Random_Numbers.Services;

namespace Random_Numbers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : BaseController
    {
        private readonly QueuePlayerSimulator _queue;
        private readonly RandomNumberDbContext _context;
        public MatchController(QueuePlayerSimulator queue, RandomNumberDbContext context)
        {
            _queue = queue;
            _context = context;
        }        
        [HttpGet]
        public async Task<ActionResult> GetAllMatches()
        {
            var matches = await _context.Matches
                    .Include(m => m.PlayerOne)
                        .ThenInclude(p1 => p1.User)
                    .Include(m => m.PlayerTwo)
                        .ThenInclude(p2 => p2.User)
                    .ToListAsync();

            return Ok(matches);
        }

        [HttpGet]
        [Route("username")]
        public async Task<ActionResult> GetAllMatchesByUsername([FromQuery] string username)
        {
            var matches = await _context.Matches
                                .Where(m => m.PlayerOne.User.Username == username || m.PlayerTwo.User.Username == username)
                                .ToListAsync();

            return Ok(matches);
        }

        [HttpPost]
        public async Task<ActionResult> PlayNow(User user)
        {
            var result = "";

            if (_queue.PlayerIsOnTheQueue(user.Username) 
                && _queue.GetMatchByUsername(user.Username).ExpiresIn >= DateTime.Now)
            {
                var match = _queue.GetMatchByUsername(user.Username);
                AddErrorToList($"You're already is on the queue. Expiration time: {match.ExpiresIn}");
                return CustomResponse();
            }

            if (_queue.HasPlayerOnQueue() && !_queue.PlayerIsOnTheQueue(user.Username))
            {
                var newPlayer = new Player();
                newPlayer.GenerateRandomNumber();

                var match = _queue.GetNextMatch();
                match.AddSecondPlayer(newPlayer);

                result = match.GetWinner();

                match.PlayerOneId = match.PlayerOne.Id;
                match.PlayerTwoId = match.PlayerTwo.Id;

                await _context.Matches.AddAsync(match);
                await _context.SaveChangesAsync();

                _queue.ClearMatches();
                return CustomResponse(result);
            }

            if (_queue.PlayerIsOnTheQueue(user.Username))
                _queue.CancelMatchByUsername(user.Username);

            var player = new Player(user);
            player.GenerateRandomNumber();
            _queue.AddPlayerToList(player);

            result = "You're in the queue, waiting for an opponent!";
            return CustomResponse(result);
        }
    }
}
