using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UFRGS.Genrec.Data;

namespace Genrec
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["USUARIO_ID"] != null && Session["USUARIO_NIVEL"].ToString() == "1") Response.Redirect("~/admin-home");
                if (Session["USUARIO_ID"] != null && Session["USUARIO_NIVEL"].ToString() == "2") Response.Redirect("~/meu-genrec");
                if (Session["USUARIO_ID"] != null && Session["USUARIO_NIVEL"].ToString() == "3") Response.Redirect("~/gerenciar-reserva");
            }
        }
        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            string matricula = this.txtMatricula.Text.Replace("'","");
            string senha = this.txtSenha.Text.Replace("'","");
            if (matricula == "" || senha == "")
            {
                this.labelMensagemAlerta.Text = ICONES_ALERTA.ICONE_CADEADO + "&nbsp; É obrigatório o preenchimento de todos os campos!";
                this.panelMensagem.CssClass = CLASSES_ALERTA.ALERTA_ERRO;
                this.timerMensagemAlerta.Enabled = true;
                return;
            }
            List<Funcionario> usuarios = Funcionario.Carregar(new List<string>() { "MATRICULA = '" + matricula + "'", "SENHA = '" + senha + "'" }, 1);
            if (usuarios.Count == 1)
            {
                Funcionario user = usuarios[0];
                Session["USUARIO_ID"] = user.IdFuncionario.ToString();
                Session["USUARIO_NOME"] = user.Nome;
                Session["USUARIO_MATRICULA"] = user.Matricula;
                Session["USUARIO_NIVEL"] = user.Nivel.ToString();
                Session["USUARIO_PRIORIDADE"] = user.Prioridade.ToString();
                if(user.Nivel == 1 ) Response.Redirect("~/admin-home");
                if (user.Nivel == 2) Response.Redirect("~/meu-genrec");
                if (user.Nivel == 3) Response.Redirect("~/gerenciar-reserva");
            }
            else
            {
                this.labelMensagemAlerta.Text = ICONES_ALERTA.ICONE_ERRO + "&nbsp; Os dados não conferem!";
                this.panelMensagem.CssClass = CLASSES_ALERTA.ALERTA_ERRO;
                this.timerMensagemAlerta.Enabled = true;
            }

        }
        protected void TimerMensagem_Tick(object sender, EventArgs e)
        {
            this.timerMensagemAlerta.Enabled = false;
            this.labelMensagemAlerta.Text = "";
            this.panelMensagem.CssClass = CLASSES_ALERTA.OCULTO;
        }
    }
}