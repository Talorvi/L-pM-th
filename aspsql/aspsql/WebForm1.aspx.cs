using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aspsql
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var db = new DataClasses1DataContext();
            GridView1.DataSource = db.oddzialies.ToList();
            GridView1.DataBind();
        }
    }
}