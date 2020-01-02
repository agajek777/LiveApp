using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveLibrary;
using LiveLibrary.Models;

namespace LiveAppUI
{
    public partial class CreateTournamentForm : Form, IPrizeRequester, ITeamRequester
    {
        List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeam_All();
        List<TeamModel> selectedTeams = new List<TeamModel>();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();
        public CreateTournamentForm()
        {
            InitializeComponent();
            WireUpList();
        }

        private void WireUpList()
        {
            selectTeamDropDown.DataSource = null;
            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = "TeamName";

            tournamentTeamsListBox.DataSource = null;
            tournamentTeamsListBox.DataSource = selectedTeams;
            tournamentTeamsListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = null;
            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = "PlaceName";
        }

        private void teamOneScoreLabel_Click(object sender, EventArgs e)
        {

        }

        private void teamOneScoreValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tournamentPlayersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CreateTournamentForm_Load(object sender, EventArgs e)
        {

        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            if (availableTeams.Count == 0 || selectTeamDropDown.SelectedItem == null) return;
            TeamModel t = (TeamModel)selectTeamDropDown.SelectedItem;
            availableTeams.Remove(t);
            selectedTeams.Add(t);
            WireUpList();
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            CreatePrizeForm frm = new CreatePrizeForm(this);
            frm.Show();
        }

        public void PrizeComplete(PrizeModel model)
        {
            selectedPrizes.Add(model);
            WireUpList();
        }

        private void createNewTeamLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm frm = new CreateTeamForm(this);
            frm.Show();
        }

        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
            WireUpList();
        }

        private void removeSelectedPlayersButton_Click(object sender, EventArgs e)
        {
            if (tournamentTeamsListBox.SelectedItems.Count == 0) return;
            TeamModel t = (TeamModel)tournamentTeamsListBox.SelectedItem;
            selectedTeams.Remove(t);
            availableTeams.Add(t);
            WireUpList();
        }

        private void removeSelectedPrizeButton_Click(object sender, EventArgs e)
        {
            if (prizesListBox.SelectedItems.Count == 0) return;
            PrizeModel p = (PrizeModel)prizesListBox.SelectedItem;
            selectedPrizes.Remove(p);
            WireUpList();
        }
        private bool ValidateForm()
        {
            decimal fee = 0;
            bool feeAcceptable = decimal.TryParse(entryFeeValue.Text, out fee);
            if (!feeAcceptable)
            {
                MessageBox.Show("You need to enter a valid Entry fee",
                    "Invalid fee",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            else if (tournamentNameValue.Text.Length == 0)
            {
                MessageBox.Show("You need to enter a valid tournament name",
                    "Invalid name",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return false;
            }
            return true;
        }
        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                TournamentModel tm = new TournamentModel();
                tm.TournamentName = tournamentNameValue.Text;
                tm.EntryFee = decimal.Parse(entryFeeValue.Text);
                tm.EnteredTeams = selectedTeams;
                tm.Prizes = selectedPrizes;
                TournamentLogic.CreateRounds(tm);
                GlobalConfig.Connection.CreateTournament(tm);
            }
        }
    }
}
