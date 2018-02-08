using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfboardQuiverConsoleApp.Models
{
    public class BoardStyle
    {
        public BoardStyle()
        {
            Surfboards = new List<Surfboard>();
        }

        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }

        public ICollection<Surfboard> Surfboards { get; set; }
    }
}
