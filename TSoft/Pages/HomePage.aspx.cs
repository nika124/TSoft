using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSoft.Models;

namespace TSoft.Pages
{
    public partial class HomePage : System.Web.UI.Page
    {
        public static bool status = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (status is false)
            {
                Response.Redirect("https://localhost:44308/Pages/LoginPage.aspx");
            }
            if(!IsPostBack)
            {
                GridViewPersonList.DataBind();
                GridViewRoles.DataBind();
            }
        }
        public List<Person> GetPersonDataGrid()
        {
            var dataHelper = new TSoft.Data.DataBaseHelper();
            return dataHelper.GetPersonData();
        }

        protected void ButtonPersonDelete_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonEdit_Click(object sender, EventArgs e)
        {
            PersonEditorPage.personEditChekcer = Convert.ToInt32(((System.Web.UI.WebControls.Button)sender).CommandArgument);
            Response.Redirect("https://localhost:44308/Pages/PersonEditorPage.aspx");
            

        }
        public List<Roles> GetRolesData()
        {
            var dataHelper = new TSoft.Data.DataBaseHelper();
            return dataHelper.GetRoleNames();
        }
    }
}