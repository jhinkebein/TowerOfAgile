using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TowerOfAgile.Models
{
    public class BoardItem
    {
        public int BoardItemId { get; set; }
        public string itemText { get; set; }
        public string itemType { get; set; }

        public int BoardId { get; set; }
        public Board Board { get; set; }
    }
}
