using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfboardQuiverConsoleApp.Models
{
    public class Surfboard
    {
        public int Id { get; set; }
        public int BuilderId { get; set; }
        public Builder Builder { get; set; }
        [Required, StringLength(50)]
        public string Model { get; set; }
        
        public int BoardStyleId { get; set; }
        public BoardStyle Style { get; set; }

        public float Length { get; set; }
        public float Width { get; set; }
        public int MaxFins { get; set; }
        public float? Rating { get; set; }
        public string Notes { get; set; }

        //public int? Test { get; set; }

        public string DisplayText
        {
            get
            {
                return $"{Builder.Name} - {Model} - {Length}'";
            }
        }
    }

}
