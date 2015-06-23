using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UFRGS.Genrec.Data;

namespace Genrec
{
    public partial class GenrecAdmin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                if (Session["USUARIO_ID"] == null) Response.Redirect("~/login");
                if (Session["USUARIO_NIVEL"].ToString() == "2") Response.Redirect("~/meu-genrec");
                if (Session["USUARIO_NIVEL"].ToString() == "3") Response.Redirect("~/gerenciar-reserva");

                this.labelNomeParticipante.Text = Session["USUARIO_NOME"].ToString();
            }
        }
        protected void lnkEfetuarLogoff_Click(Object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/login");
        }
        protected void lnkMenu_Command(Object sender, CommandEventArgs e)
        {

        }
    }
}