using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveLibrary;

namespace LiveAppUI
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LiveLibrary.GlobalConfig.InitializeConnections(DatabaseType.Sql);
            //Application.Run(new TournamentViewerForm());
            //Application.Run(new TournamentDashboardForm());
            Application.Run(new CreateTournamentForm());
            //Application.Run(new CreateTeamForm());
        }
    }
}
