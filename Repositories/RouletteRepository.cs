using ApiRoulette.Models;
using System;
using System.Collections.Generic;
using EasyCaching.Core;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRoulette.Repositories
{
    public class RouletteRepository : IRouletteRepository
    {
        private IEasyCachingProviderFactory cachingProviderFactory;
        private IEasyCachingProvider cachingProvider;

        private const string KEYREDIS = "TablerOulete";
        public RouletteRepository(IEasyCachingProviderFactory cachingProviderFactory)
        {
            this.cachingProviderFactory = cachingProviderFactory;
            this.cachingProvider = this.cachingProviderFactory.GetCachingProvider("roulette");
        }
        public List<Roulette> GetAll()
        {
            var roulett = this.cachingProvider.GetByPrefix<Roulette>(KEYREDIS);
            if (roulett.Values.Count == 0)
            {
                return new List<Roulette>();
            }

            return new List<Roulette>(roulett.Select(x => x.Value.Value));
        }

        public Roulette GetById(string Id)
        {
            var item = this.cachingProvider.Get<Roulette>(KEYREDIS + Id);
            if (!item.HasValue)
            {
                return null;
            }

            return item.Value;
        }

        public Roulette Save(Roulette roulette)
        {
            cachingProvider.Set(KEYREDIS + roulette.Id, roulette, TimeSpan.FromDays(365));

            return roulette;
        }

        public Roulette Update(string Id, Roulette roulette)
        {
            roulette.Id = Id;
            return Save(roulette);  
        }
    }
}
