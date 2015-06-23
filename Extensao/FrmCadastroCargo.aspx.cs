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
    public partial class FrmCadastroCargo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USUARIO_ID"] == null) Response.Redirect("~/login");
            if (!IsPostBack)
            {
                Session["parametros"] = null;
                this.gridConsulta.DataSource = Cargo.Carregar(new List<string>(), 1, 10);
                this.gridConsulta.DataBind();
                if (this.gridConsulta.Rows.Count < 10) this.btnProximo.Enabled = false;
            }
        }
        private void PopulaDropList()
        {
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
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
            if (!string.IsNullOrEmpty(this.txtParanNome.Text)) parametros.Add("UPPER(NOME) LIKE '%" + this.txtParanNome.Text.ToUpper() + "%'");
            this.gridConsulta.DataSource = Cargo.Carregar(parametros, 1, 10);
            this.gridConsulta.DataBind();
            this.idPagina.Value = "10";
            Session["parametros"] = parametros;
            this.btnAnterior.Enabled = false;
            this.btnProximo.Enabled = true;
            if (this.gridConsulta.Rows.Count < 10) this.btnProximo.Enabled = false;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/cargo");
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            this.panelFormulario.Visible = true;
            PopulaDropList();
            Carregar(int.Parse(this.idRegistro.Value));
            this.panelDetalhes.Visible = false;
        }
        protected void gridConsulta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                this.panelFormulario.Visible = true;
                PopulaDropList();
                Carregar(int.Parse(e.CommandArgument.ToString()));
                this.panelPesquisa.Visible = false;
            }
            if (e.CommandName == "Selecionar")
            {
                Cargo item = Cargo.Carregar(int.Parse(e.CommandArgument.ToString()));
                this.idRegistro.Value = item.ToString();
                this.viewDetalhes.DataSource = new List<Cargo>() { item };
                this.viewDetalhes.DataBind();
                this.panelDetalhes.Visible = true;
                this.panelPesquisa.Visible = false;
            }
            if (e.CommandName == "Excluir")
            {
                this.idRegistro.Value = e.CommandArgument.ToString();
                Excluir();
                this.idPagina.Value = (int.Parse(this.idPagina.Value) - 10).ToString();
                this.btnPaginar_Command(sender, new CommandEventArgs("Paginar", "Proximo"));
            }
        }
        protected void btnPaginar_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Paginar")
            {
                int pagina = int.Parse(this.idPagina.Value);
                if (e.CommandArgument.ToString() == "Anterior") pagina = pagina - 20;
                this.idPagina.Value = (pagina + 10).ToString();
                this.gridConsulta.DataSource = Cargo.Carregar((Session["parametros"] != null) ? (List<string>)Session["parametros"] : new List<string>(), pagina + 1, pagina + 10);
                this.gridConsulta.DataBind();
                this.btnAnterior.Enabled = true;
                this.btnProximo.Enabled = true;
                if (this.gridConsulta.Rows.Count < 10) this.btnProximo.Enabled = false;
                if (int.Parse(this.idPagina.Value) == 10) this.btnAnterior.Enabled = false;
            }
        }
        protected void btnNovo_Click(object sender, EventArgs e)
        {
            PopulaDropList();
            this.idRegistro.Value = "0";
            this.panelFormulario.Visible = true;
            this.panelPesquisa.Visible = false;
            this.panelDetalhes.Visible = false;
        }
        public void Excluir()
        {
            Cargo ob = Cargo.Carregar(int.Parse(this.idRegistro.Value));
            string result = ob.Excluir();
            if (string.IsNullOrEmpty(result))
            {
                this.labelMensagemAlerta.Text = ICONES_ALERTA.ICONE_OK + "&nbsp; O registro foi excluido com sucesso!";
                this.panelMensagem.CssClass = CLASSES_ALERTA.ALERTA_SUCESSO;
            }
            else
            {
                this.labelMensagemAlerta.Text = ICONES_ALERTA.ICONE_ERRO + "O registro <b>N&Atilde;O</b> foi excluido!<br /> <b>ERRO:</b> " + result;
                this.panelMensagem.CssClass = CLASSES_ALERTA.ALERTA_ERRO;
            }
            this.timerMensagemAlerta.Enabled = true;
        }
        public void Salvar()
        {
            Cargo ob = (int.Parse(this.idRegistro.Value) > 0) ? Cargo.Carregar(int.Parse(this.idRegistro.Value)) : new Cargo();
            ob.Nome = this.txtNome.Text;
            ob.PrioridadePadrao = (this.dropListPrioridadePadrao.SelectedValue != "") ? int.Parse(this.dropListPrioridadePadrao.SelectedValue) : 0;
            string result = "";
            if (int.Parse(this.idRegistro.Value) > 0)
            {
                result = ob.Atualizar();
            }
            else
            {
                ob.IdCargo = Cargo.ID + 1;
                result = ob.Inserir();
                this.idRegistro.Value = Cargo.ID.ToString();
            }
            if (string.IsNullOrEmpty(result))
            {
                this.labelMensagemAlerta.Text = ICONES_ALERTA.ICONE_OK + "&nbsp; O registro foi salvo com sucesso!";
                this.panelMensagem.CssClass = CLASSES_ALERTA.ALERTA_SUCESSO;
                this.gridConsulta_RowCommand(null, new GridViewCommandEventArgs(null, new CommandEventArgs("Selecionar", this.idRegistro.Value)));
                this.panelFormulario.Visible = false;
            }
            else
            {
                this.labelMensagemAlerta.Text = ICONES_ALERTA.ICONE_ERRO + "&nbsp; O registro <b>N&Atilde;O</b> foi salvo!<br /> <b>ERRO:</b> " + result;
                this.panelMensagem.CssClass = CLASSES_ALERTA.ALERTA_ERRO;
            }
            this.timerMensagemAlerta.Enabled = true;
        }
        public void Carregar(int id)
        {
            Cargo ob = Cargo.Carregar(id);
            this.idRegistro.Value = ob.IdCargo.ToString();
            this.txtNome.Text = ob.Nome.ToString();
            this.dropListPrioridadePadrao.SelectedIndex = this.dropListPrioridadePadrao.Items.IndexOf(this.dropListPrioridadePadrao.Items.FindByValue(ob.PrioridadePadrao.ToString()));
        }
    }
}
