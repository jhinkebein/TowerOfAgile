using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TowerOfAgile.Models
{
    public class BoardElement //this class will be the columns of the board table
    {
        public int BoardElementId { get; set; }
        public string Element { get; set; }

        //FK
        public Board Board { get; set; }
    }
}
