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
    public partial class FrmGerenciarReserva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USUARIO_ID"] == null) Response.Redirect("~/login");
            if (!IsPostBack)
            {
                Session["parametros"] = null;
                this.gridConsulta.DataSource = Reserva.Carregar(new List<string>(), 1, 10);
                this.gridConsulta.DataBind();
                if (this.gridConsulta.Rows.Count < 10) this.btnProximo.Enabled = false;
            }
        }
        protected void TimerMensagem_Tick(object sender, EventArgs e)
        {
            this.timerMensagemAlerta.Enabled = false;
            this.labelMensagemAlerta.Text = "";
            this.panelMensagem.CssClass = CLASSES_ALERTA.OCULTO;
        }
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            List<string> parametros = new List<string>();
            if (!string.IsNullOrEmpty(this.txtParanNumero.Text)) parametros.Add("NUMERO = '" + this.txtParanNumero.Text + "'");
            this.gridConsulta.DataSource = Reserva.Carregar(parametros, 1, 10);
            this.gridConsulta.DataBind();
            this.idPagina.Value = "10";
            Session["parametros"] = parametros;
            this.btnAnterior.Enabled = false;
            this.btnProximo.Enabled = true;
            if (this.gridConsulta.Rows.Count < 10) this.btnProximo.Enabled = false;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/gerenciar-reserva");
        }

        protected void gridConsulta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Selecionar")
            {
                Reserva item = Reserva.Carregar(int.Parse(e.CommandArgument.ToString()));
                this.idRegistro.Value = item.ToString();
                this.viewDetalhes.DataSource = new List<Reserva>() { item };
                this.viewDetalhes.DataBind();
                this.gridItens.DataSource = ItemReerva.Carregar(new List<string>() { "ID_RESERVA = " + item.ToString() }, 2);
                this.gridItens.DataBind();
                this.panelDetalhes.Visible = true;
                this.panelPesquisa.Visible = false;
            }
        }
        protected void btnPaginar_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Paginar")
            {
                int pagina = int.Parse(this.idPagina.Value);
                if (e.CommandArgument.ToString() == "Anterior") pagina = pagina - 20;
                this.idPagina.Value = (pagina + 10).ToString();
                this.gridConsulta.DataSource = Reserva.Carregar((Session["parametros"] != null) ? (List<string>)Session["parametros"] : new List<string>(), pagina + 1, pagina + 10);
                this.gridConsulta.DataBind();
                this.btnAnterior.Enabled = true;
                this.btnProximo.Enabled = true;
                if (this.gridConsulta.Rows.Count < 10) this.btnProximo.Enabled = false;
                if (int.Parse(this.idPagina.Value) == 10) this.btnAnterior.Enabled = false;
            }
        }
        protected void TimerMensagemItens_Tick(object sender, EventArgs e)
        {
            this.timerMensagemAlertaItens.Enabled = false;
            this.labelMensagemAlertaItens.Text = "";
            this.panelMensagemItens.CssClass = CLASSES_ALERTA.OCULTO;
        }
        protected void btnAdicionarNovoItem_Click(object sender, EventArgs e)
        {
            
        }
        protected void gridItens_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ItemReerva item = ItemReerva.Carregar(int.Parse(e.CommandArgument.ToString()));
            
            if (e.CommandName == "Liberar")
            {
                item.Situacao = "L";
                item.DataRetirada = DateTime.Now;
            }
            if (e.CommandName == "Cancelar")
            {
                item.Situacao = "C";
                item.DataCancelamento = DateTime.Now;
            }
            if (e.CommandName == "Receber")
            {
                item.Reserva.Funcionario.IndiceDePenalidade += (DateTime.Now.Date > item.DataRetirada.Date) ? 2 : -1; 
                item.Reserva.Funcionario.Atualizar();
                item.Situacao = "R";
                item.DataDevolucao = DateTime.Now;
            }
            string result = result = item.Atualizar();  
            if (!string.IsNullOrEmpty(result))
            {
                this.labelMensagemAlertaItens.Text = ICONES_ALERTA.ICONE_ERRO + "&nbsp; Não foi possivel efetuar a operação";
                this.panelMensagemItens.CssClass = CLASSES_ALERTA.ALERTA_ERRO;
                this.timerMensagemAlertaItens.Enabled = true;
            }
            this.gridItens.DataSource = ItemReerva.Carregar(new List<string>() { "ID_RESERVA = " + item.Reserva.IdReserva.ToString() }, 2);
            this.gridItens.DataBind();
        }
    }
}
