using ApiRoulette.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRoulette.Repositories
{
    public interface IRouletteRepository : IRepository
    {
        public Roulette GetById(string Id);
        public List<Roulette> GetAll();
        public Roulette Update(string Id, Roulette roulette);
        public Roulette Save(Roulette roulette);
    }
}
