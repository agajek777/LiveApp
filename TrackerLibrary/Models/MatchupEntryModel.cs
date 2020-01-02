using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLibrary.Models
{
    public class MatchupEntryModel
    {
        /// <summary>
        /// Represents one team in a matchup
        /// </summary>
        public int Id { get; set; }
        public TeamModel TeamCompeting { get; set; }
        public double Score { get; set; }
        public MatchupModel ParentMatchup { get; set; }

    }
}
