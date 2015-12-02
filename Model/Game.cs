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
        public Team A { get; set; }
        public Team B { get; set; }
        public int rounds { get; set; }

        public Game()
        {
            A = new Team();
            B = new Team();
            rounds = 0;
        }
        public Game(string aName, string bName, int [] aScore, int [] bScore)
        {
            A = new Team(aName, aScore);
            B = new Team(bName, bScore);
            rounds = aScore.Length;
            A.calcTotalScore();
            B.calcTotalScore();
        }
    }
}
