using Microsoft.AspNetCore.Routing.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRoulette.Models
{
    [Serializable]
    public class Roulette
    {
        public string Id { get; set; }
        public bool IsOpen { get; set; }
        public DateTime? OpendAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public IDictionary<string, double>[] board { get; set; } = new IDictionary<string, double>[39];
        public Roulette()
        {
            this.Init();
        }

        private void Init()
        {
            for(int i =0; i < board.Length; i++)
            {
                board[i] = new Dictionary<string, double>();
            }
        }
    }
}
