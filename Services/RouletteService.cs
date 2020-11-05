using ApiRoulette.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRoulette.Services
{
    public class RouletteService : IRouletteService
    {
        public Roulette Bet(string Id, string UserId, int position, double money)
        {
            try
            {
                if (money > 10000 || money < 1)
                {
                    //throw new CashOutRangeException();
                }
            } catch (Exception e)
            {

            }
        }

        public Roulette Close(string Id)
        {
            throw new NotImplementedException();
        }

        public Roulette create()
        {
            throw new NotImplementedException();
        }

        public Roulette Find(string Id)
        {
            throw new NotImplementedException();
        }

        public List<Roulette> GetAll()
        {
            throw new NotImplementedException();
        }

        public Roulette Open(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
