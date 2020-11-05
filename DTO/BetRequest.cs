using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRoulette.DTO
{
    public class BetRequest
    {
        //<sumary>
        //Position 0-36, and 37=>red, 38=>black
        //</sumary>
        [Range(0,38)]
        public int position { get; set; }

        //<sumary>
        //Money to Bet
        //</sumary>
        [Range(0.5d, maximum: 100000)]
        public double money { get; set; }
    }
}
