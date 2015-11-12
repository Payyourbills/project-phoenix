using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPhoenix.Model  
{

   // [Table("Games")]
    class Game
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string NameTeamA { get; set; }
        public string NameTeamB { get; set; }
        public int ScoreA { get; set; }
        public int ScoreB { get; set; }
        public int rounds { get; set; }
        

    }
}
