using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TSoft.Models;
using TSoft.Data;


namespace TSoft.Pages
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        public static int PersonId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RepeaterTopNav.DataBind();
                RepeaterSideNav.DataBind();
            }
        }

        public List<AppObjects> AppObjectsDataPicker(string navType)
        {
            
            var dataHelper = new TSoft.Data.DataBaseHelper();
            var allCategory = dataHelper.GetAppObjectsData(PersonId);
            allCategory = allCategory.Where(tnav => tnav.Type == navType).ToList();
            return allCategory;
        }
    }
}