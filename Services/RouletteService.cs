using ApiRoulette.Exceptions;
using ApiRoulette.Models;
using ApiRoulette.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRoulette.Services
{
    public class RouletteService : IRouletteService
    {
        private IRouletteRepository rouletteRepository;

        public RouletteService(IRouletteRepository rouletteRepository)
        {
            this.rouletteRepository = rouletteRepository;
        }

        public Roulette Bet(string Id, string UserId, int position, double money)
        {
            if(money > 10000 || money < 1)
            {
                throw new CashOutRangeException();
            }
            Roulette roulette = rouletteRepository.GetById(Id);
            if(roulette == null)
            {
                throw new RouletteNotFound();
            }
            if (roulette.IsOpen == false)
            {
                throw new RouletteClosesException();
            }

            double value = 0d;
            roulette.board[position].TryGetValue(UserId, out value);
            roulette.board[position].Remove(UserId + "");
            roulette.board[position].TryAdd(UserId + "", value + money);

            return rouletteRepository.Update(roulette.Id, roulette);
        }

        public Roulette Close(string Id)
        {
            Roulette roulette = rouletteRepository.GetById(Id);
            if (roulette == null)
            {
                throw new RouletteNotFound();
            }
            if(roulette.ClosedAt != null)
            {
                throw new NotAllowedClosedException();
            }
            roulette.ClosedAt = DateTime.Now;
            roulette.IsOpen = false;

            return rouletteRepository.Update(Id, roulette);
        }

        public Roulette create()
        {
            Roulette roulette = new Roulette()
            {
                Id = Guid.NewGuid().ToString(),
                IsOpen = false,
                OpendAt = null,
                ClosedAt = null
            };
            rouletteRepository.Save(roulette);

            return roulette;
        }

        public Roulette Find(string Id)
        {
            return rouletteRepository.GetById(Id);
        }

        public List<Roulette> GetAll()
        {
            return rouletteRepository.GetAll();
        }

        public Roulette Open(string Id)
        {
            Roulette roulette = rouletteRepository.GetById(Id);
            if(roulette == null)
            {
                throw new RouletteNotFound();
            }
            if(roulette.OpendAt != null)
            {
                throw new NotAllowedOpenException();
            }
            roulette.OpendAt = DateTime.Now;
            roulette.IsOpen = true;

            return rouletteRepository.Update(Id, roulette);
        }
    }
}
