using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSoft.Models;
//TSoft.Data.DataBaseHelper();
namespace TSoft.Pages
{
    public partial class RoleEditor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var dataHelper = new TSoft.Data.DataBaseHelper();
            bool permissionChecker = dataHelper.GetWebPagePermissionData(SiteMaster.PersonId, Convert.ToString(HttpContext.Current.Request.Url.AbsolutePath));
            if (permissionChecker is false) Response.Redirect("https://localhost:44308/Pages/HomePage.aspx");
            if (!IsPostBack)
            {
                DropDownListRoles.DataBind();
                GridViewAppObjects.DataBind();
                DropDownListAppObjects.DataBind();
                DropDownListAppObjectType.DataSource = Enum.GetValues(typeof(Types));
                DropDownListAppObjectType.DataBind();
            }
        }
        //AddRole
        protected void ButtonAddRole_Click(object sender, EventArgs e)
        {
            string Name = TextBoxRoleName.Text;
            var dataHelper = new TSoft.Data.DataBaseHelper();
            bool RoleChecker = dataHelper.RoleChecker(Name);
            if (RoleChecker is true) ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('es ukve arsebobs')", true);
            else
            {
                dataHelper.SetRole(Name);
                DropDownListRoles.DataBind();
            }
        }
        //GetRoleNames
        public List<Roles> GetRoleNames()
        {
            var dataHelper = new TSoft.Data.DataBaseHelper();
            return dataHelper.GetRoleNames();

        }
        //GetAppObjectsDataByRoleId
        public List<AppObjects> GetAppObjectsData()
        {
            int RoleId = Convert.ToInt32(DropDownListRoles.SelectedItem.Value);
            var dataHelper = new TSoft.Data.DataBaseHelper();
            return dataHelper.GetAppObjectNamesByRoleId(RoleId);
        }
        //DeleteAppObjectsByRoleIdAndAppObjectId
        protected void ButtonAppObjectDelete_Click(object sender, EventArgs e)
        {
            var dataHelper = new TSoft.Data.DataBaseHelper();
            int RoleId = Convert.ToInt32(DropDownListRoles.SelectedItem.Value);
            int AppObjectId = Convert.ToInt32(((System.Web.UI.WebControls.LinkButton)sender).CommandArgument);
            dataHelper.DeleteAppObjectsGroup(RoleId, AppObjectId);
            GridViewAppObjects.DataBind();
        }
        //GridViewAppObjectsUpdateOnRoleChange
        protected void DropDownListRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewAppObjects.DataBind();
        }
        //GetAppObjectsName
        public List<AppObjects> GetAppObjectNames()
        {
            var dataHelper = new TSoft.Data.DataBaseHelper();
            return dataHelper.GetAppObjectNames();
        }
        //AddAppObjectsByRoleIdAndAppObjectsId
        protected void ButtonAddAppObject_Click(object sender, EventArgs e)
        {
            var dataHelper = new TSoft.Data.DataBaseHelper();
            int RoleId = Convert.ToInt32(DropDownListRoles.SelectedItem.Value);
            int AppObjectId = Convert.ToInt32(DropDownListAppObjects.SelectedItem.Value);
            bool AppObjectsChecker = dataHelper.AppObjectsGroupChecker(RoleId, AppObjectId);
            if (AppObjectsChecker is true) ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('es ukve arsebobs')", true);
            else
            {
                dataHelper.SetAppObjectsGroup(RoleId, AppObjectId);
                GridViewAppObjects.DataBind();
            }
        }
        public enum Types
        {
            Write,
            Read
        }
    }
}