using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();
        public CreateTeamForm()
        {
            InitializeComponent();
            //CreateSampleData();
            WireUpList();
        }
        private void WireUpList()
        {
            selectTeamMemberDropDown.DataSource = null;
            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = null;
            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";
        }

        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Adam", LastName = "Gajek" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Micha", LastName = "Galek" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Kasia", LastName = "Marczk" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Basia", LastName = "Merczk" });
        }

        private void teamOneScoreLabel_Click(object sender, EventArgs e)
        {

        }

        private void teamOneScoreValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void CreateTeamForm_Load(object sender, EventArgs e)
        {

        }

        private void tournamentPlayersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private bool EmailValidation()
        {
            bool isAt = false, isDot = false;
            int atCounter = 0;
            int dotCounter = 0;
            if (emailValue.Text[0] == '@' || emailValue.Text[0] == '.') return false;
            for (int i = 1; i < emailValue.Text.Length; i++)
            {
                if (char.IsUpper(emailValue.Text[i])) return false;
                if (emailValue.Text[i] == '@')
                {
                    isAt = true;
                    atCounter++;
                    if (atCounter > 1) return false;
                    if (emailValue.Text[i + 1] == '.' || !(i + 1 < emailValue.Text.Length)) return false;
                }
                if (emailValue.Text[i] == '.' && !isAt) if (emailValue.Text[i+1] == '@') return false;
                if (emailValue.Text[i] == '.' && isAt)
                {
                    isDot = true;
                    dotCounter++;
                    if (!(i + 1 < emailValue.Text.Length)) return false;
                    if (emailValue.Text[i + 1] == '.') return false;
                    if (dotCounter > 1) return false;
                }
            }
            if (isDot && isAt) return true;
            return false;
        }
        private bool ValidateForm()
        {

            if (firstNameValue.Text.Length == 0) return false;
            if (lastNameValue.Text.Length == 0) return false;
            if (emailValue.Text.Length == 0) return false;
            if (cellphoneValue.Text.Length == 0) return false;
            if (!EmailValidation()) return false;
            int parsePhone = 0;
            string phone = cellphoneValue.Text.Remove(0, 4);
            if (!int.TryParse(phone, out parsePhone) || phone.Length != 9) return false;
            return true;
        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel p = new PersonModel();
                p.FirstName = firstNameValue.Text;
                p.LastName = lastNameValue.Text;
                p.EmailAddress = emailValue.Text;
                p.CellPhoneNumber = cellphoneValue.Text;

                p = GlobalConfig.Connection.CreatePerson(p);
                selectedTeamMembers.Add(p);
                WireUpList();

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellphoneValue.Text = "+48-";
            }
            else MessageBox.Show("Your information is incorrect.");
        }

        private void addNewMemberGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void createTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = new TeamModel();
            t.TeamName = teamNameValue.Text;
            t.TeamMembers = selectedTeamMembers;

            t = GlobalConfig.Connection.CreateTeam(t);
        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            if (availableTeamMembers.Count == 0) return;
            PersonModel p = (PersonModel)selectTeamMemberDropDown.SelectedItem;
            availableTeamMembers.Remove(p);
            selectedTeamMembers.Add(p);
            WireUpList();
        }

        private void deleteSelectedMemberButton_Click(object sender, EventArgs e)
        {
            if (teamMembersListBox.SelectedItems.Count == 0) return; 
            PersonModel p = (PersonModel)teamMembersListBox.SelectedItem;
            selectedTeamMembers.Remove(p);
            availableTeamMembers.Add(p);
            WireUpList();
        }
    }
}
