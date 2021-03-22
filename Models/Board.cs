using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TowerOfAgile.Models
{
    public class Board
    {
        public int BoardId { get; set; }

        public string Sharecode { get; set; } //something like xxxx-xxxx-xxxx will be encrypted in db 
       
        [Required(ErrorMessage ="Please enter a name for your board")]
        public string Name { get; set; }

        public string createShareCode()
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                //Guid g = Guid.NewGuid(); 

                //return g.ToString();
                string shareCode = RandomCodeGenerator.GetKeyAlphaNumeric(KeyLength.l16);
                return shareCode;
            }
            else
            {
                return "";
            }
        }
    }
}
