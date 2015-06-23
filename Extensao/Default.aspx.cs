using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UFRGS.Genrec.Data;
using System.Data;

namespace Genrec
{
    public partial class DefaultAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USUARIO_ID"] == null) Response.Redirect("~/login");
        }
        
    }
}