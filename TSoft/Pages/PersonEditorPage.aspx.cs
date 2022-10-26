using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using TSoft.Models;
//TSoft.Data.DataBaseHelper();
namespace TSoft.Pages
{
    public partial class PersonEditorPage : System.Web.UI.Page
    {
        public static int personEditChekcer;
        protected void Page_Load(object sender, EventArgs e)
        {
            var dataHelper = new TSoft.Data.DataBaseHelper();
            bool permissionChecker = dataHelper.GetWebPagePermissionData(SiteMaster.PersonId, Convert.ToString(HttpContext.Current.Request.Url.AbsolutePath));
            if (permissionChecker is false) Response.Redirect("https://localhost:44308/Pages/HomePage.aspx");
            if (!IsPostBack)
            {
                DropDownListPerson.DataBind();
                GridViewRole.DataBind();
                //GridViewPersonList.DataBind();
                // DropDownListRole.DataBind();
            }
            if(personEditChekcer != 0)
            {
                Edit();
            }
            
        }
        
        //AddOrEditPersonOnButtonClick
        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            var dataHelper = new TSoft.Data.DataBaseHelper();
            bool Gender = false;
            if (RadioButtonMale.Checked == true)
            {
                Gender = true;
            }
            if (RadioButtonFeMale.Checked == true)
            {
                Gender = false;
            }
            var usernamechecker = dataHelper.UserNameChecker(TextBoxUserName.Text);
            if(usernamechecker == true && personEditChekcer == 0)
            {
                MessageBox.Show("UserName is used", "Some title",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                usernamechecker = false;
            }
            else
            {
                if (personEditChekcer == 0) dataHelper.SetPersonInfo(TextBoxName.Text, TextBoxLastName.Text, TextBoxUserName.Text, TextBoxPassword.Text, TextBoxEmail.Text, TextBoxPhoneNumber.Text, TextBoxIdCardNumber.Text, Gender);
                else dataHelper.UpdatePersonInfo(TextBoxName.Text, TextBoxLastName.Text, TextBoxUserName.Text, TextBoxPassword.Text, TextBoxEmail.Text, TextBoxPhoneNumber.Text, TextBoxIdCardNumber.Text, Gender, personEditChekcer);
                personEditChekcer = 0;
            }
            TextBoxName.Text = null;
            TextBoxLastName.Text = null;
            TextBoxUserName.Text = null;
            TextBoxPassword.Text = null;
            TextBoxRepeatPassword.Text = null;
            TextBoxEmail.Text = null;
            TextBoxPhoneNumber.Text = null; 
            TextBoxIdCardNumber.Text = null;
            RadioButtonMale.Checked = false;
            RadioButtonFeMale.Checked = false;
        }
        //DropDownListPersonData
        public List<Person> GetPersonData()
        {
            var dataHelper = new TSoft.Data.DataBaseHelper();
            return dataHelper.GetPersonData();
        }       
        //GetRoleDataForGridView
        public List<Roles> GetRoleData()
        {
            var dataHelper = new TSoft.Data.DataBaseHelper();
            int PersonId = Convert.ToInt32(DropDownListPerson.SelectedItem.Value);
            return dataHelper.GetRoleNamesByPersonId(PersonId);
        }
        //DeletePerson'sRole
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            var dataHelper = new TSoft.Data.DataBaseHelper();
            var argument = Convert.ToInt16(((LinkButton)sender).CommandArgument);
            int PersonId = Convert.ToInt32(DropDownListPerson.SelectedItem.Value);
            dataHelper.DeleteRole(argument, PersonId);
            GridViewRole.DataBind();
        }
        //UpdateGridViewRoles
        protected void DropDownListPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRole.DataBind();
        }
        //PersonEditButton
        protected void ButtonEdit_Click(object sender, EventArgs e)
        {
            var dataHelper = new TSoft.Data.DataBaseHelper();
            personEditChekcer = Convert.ToInt32(((System.Web.UI.WebControls.Button)sender).CommandArgument);
            var personData = dataHelper.GetPersonDataByPersonId(personEditChekcer);
            TextBoxUserName.Text = personData.UserName;
            TextBoxName.Text = personData.FirstName;
            TextBoxLastName.Text = personData.LastName;
            TextBoxPassword.Text = personData.Password;
            TextBoxRepeatPassword.Text = personData.Password;
            TextBoxIdCardNumber.Text = personData.IdCardNumber;
            TextBoxEmail.Text = personData.Email;
            TextBoxPhoneNumber.Text = personData.PhoneNumber;
            if (personData.Gender == true) { RadioButtonMale.Checked = true; }
            else { RadioButtonFeMale.Checked = true; }
            PanelPersonDataInfo.Visible = true;
            PanelPersonRoleInfo.Visible = false;
            ButtonPersonDataInfo.Font.Bold = true;
            ButtonPersonRoleInfo.Font.Bold = false;
            //PanelPersonGrid.Visible = false;
            //ButtonPersonList.Font.Bold = false;
        }
        public void Edit()
        {
            var dataHelper = new TSoft.Data.DataBaseHelper();
            var personData = dataHelper.GetPersonDataByPersonId(personEditChekcer);
            TextBoxUserName.Text = personData.UserName;
            TextBoxName.Text = personData.FirstName;
            TextBoxLastName.Text = personData.LastName;
            TextBoxPassword.Text = personData.Password;
            TextBoxRepeatPassword.Text = personData.Password;
            TextBoxIdCardNumber.Text = personData.IdCardNumber;
            TextBoxEmail.Text = personData.Email;
            TextBoxPhoneNumber.Text = personData.PhoneNumber;
            if (personData.Gender == true) { RadioButtonMale.Checked = true; }
            else { RadioButtonFeMale.Checked = true; }
            PanelPersonDataInfo.Visible = true;
            PanelPersonRoleInfo.Visible = false;
            ButtonPersonDataInfo.Font.Bold = true;
            ButtonPersonRoleInfo.Font.Bold = false;
        }
        //DeletePersonByPersonId
        protected void ButtonPersonDelete_Click(object sender, EventArgs e)
        {
            var dataHelper = new TSoft.Data.DataBaseHelper();
            var personId = Convert.ToInt32(((System.Web.UI.WebControls.Button)sender).CommandArgument);
            dataHelper.DeletePerson(personId);
            //GridViewPersonList.DataBind();
        }
        //GetPersonDataForPersonGridView
        public List<Person> GetPersonDataGrid()
        {
            var dataHelper = new TSoft.Data.DataBaseHelper();
            return dataHelper.GetPersonData();
        }
        //PersonDataInfoPanelVisible
        protected void ButtonPersonDataInfo_Click(object sender, EventArgs e)
        {
            PanelPersonDataInfo.Visible = true;
            PanelPersonRoleInfo.Visible = false;
            ButtonPersonDataInfo.Font.Bold = true;
            ButtonPersonRoleInfo.Font.Bold = false;
            //PanelPersonGrid.Visible = false;
            //ButtonPersonList.Font.Bold = false;
        }
        //PersonRoleInfoPanelVisible
        protected void ButtonPersonRoleInfo_Click(object sender, EventArgs e)
        {
            PanelPersonDataInfo.Visible = false;
            PanelPersonRoleInfo.Visible = true;
            ButtonPersonDataInfo.Font.Bold = false;
            ButtonPersonRoleInfo.Font.Bold = true;
            //PanelPersonGrid.Visible = false;
            //ButtonPersonList.Font.Bold = false;
        }
        //PersonListPanelVisible
        protected void ButtonPersonList_Click(object sender, EventArgs e)
        {
           // GridViewPersonList.DataBind();
            PanelPersonDataInfo.Visible = false;
            PanelPersonRoleInfo.Visible = false;
            ButtonPersonDataInfo.Font.Bold = false;
            ButtonPersonRoleInfo.Font.Bold = false;
            //PanelPersonGrid.Visible = true;
            //ButtonPersonList.Font.Bold = true;
        }
    }
}