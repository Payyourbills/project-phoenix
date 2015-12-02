using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPhoenix.Model
{
    class Team
    {
        public string Name { get; set; }
        public int [] Score { get; set; }
        public int TotalScore { get; set; }
        public Team()
        {
            Name = "Team";
            Score = new int[1];
        }
        public Team(string name, int [] score)
        {
            Name = name;
            Score = score;
            calcTotalScore();
        }
        public void calcTotalScore()
        {
            int result = 0;
            for(int i = 0; i< Score.Length; i++)
            {
                result += Score[i];
            }
            TotalScore = result;
        }

       
    }
   
}
