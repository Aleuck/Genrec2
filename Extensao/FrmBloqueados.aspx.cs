using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UFRGS.Genrec.Data;

namespace Genrec
{
    public partial class FrmBloqueados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USUARIO_ID"] == null) Response.Redirect("~/login");
            if (!IsPostBack)
            {
                this.CarregarGridBloqueados();                
            }
        }
        protected void CarregarGridBloqueados()
        {
            this.gridConsulta.DataSource = Funcionario.Carregar(new List<string>() { "PRIORIDADE = 4" }, 2);
            this.gridConsulta.DataBind();
        }
        protected void TimerMensagem_Tick(object sender, EventArgs e)
        {
            this.timerMensagemAlerta.Enabled = false;
            this.labelMensagemAlerta.Text = "";
            this.panelMensagem.CssClass = CLASSES_ALERTA.OCULTO;
        }

        protected void gridConsulta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Desbloquear")
            {
                Funcionario item = Funcionario.Carregar(int.Parse(e.CommandArgument.ToString()));
                item.Prioridade = item.Cargo.PrioridadePadrao;
                item.IndiceDePenalidade = 0;
                string result =item.Atualizar();
                if (string.IsNullOrEmpty(result))
                {
                    this.labelMensagemAlerta.Text = ICONES_ALERTA.ICONE_OK + "&nbsp; Usuário liberado!";
                    this.panelMensagem.CssClass = CLASSES_ALERTA.ALERTA_SUCESSO;
                    this.CarregarGridBloqueados();
                }
                else
                {
                    this.labelMensagemAlerta.Text = ICONES_ALERTA.ICONE_ERRO + "&nbsp; O usuário não pode ser liberado!";
                    this.panelMensagem.CssClass = CLASSES_ALERTA.ALERTA_ERRO;
                }                
                this.timerMensagemAlerta.Enabled = true;


            }
        }

    }
}
