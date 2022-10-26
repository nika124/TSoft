using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSoft.Models;

namespace TSoft.Pages
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //LoginOnButtonClick
        protected void ButtonLogIn_Click(object sender, EventArgs e)
        {
            var allCategory = new Person();
            var dataHelper = new TSoft.Data.DataBaseHelper();
            var userName = TextBoxUserName.Text;
            var password = TextBoxPassword.Text;
            allCategory = dataHelper.GetPersonData(userName, password);
            if (allCategory.Status is true)
            {
                HomePage.status = allCategory.Status;
                SiteMaster.PersonId = allCategory.Id;
                Response.Redirect("https://localhost:44308/Pages/HomePage.aspx");

            }
        }
    }
}