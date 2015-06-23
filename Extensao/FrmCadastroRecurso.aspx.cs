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
    public partial class FrmCadastroRecurso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USUARIO_ID"] == null) Response.Redirect("~/login");
            if (!IsPostBack)
            {
                Session["parametros"] = null;
                this.gridConsulta.DataSource = Recurso.Carregar(new List<string>(), 1, 10);
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
            if (!string.IsNullOrEmpty(this.txtParanCodigo.Text)) parametros.Add("CODIGO = '" + this.txtParanCodigo.Text + "'");
            if (!string.IsNullOrEmpty(this.txtParanDescricao.Text)) parametros.Add("UPPER(DESCRICAO) LIKE '%" + this.txtParanDescricao.Text.ToUpper() + "%'");
            this.gridConsulta.DataSource = Recurso.Carregar(parametros, 1, 10);
            this.gridConsulta.DataBind();
            this.idPagina.Value = "10";
            Session["parametros"] = parametros;
            this.btnAnterior.Enabled = false;
            this.btnProximo.Enabled = true;
            if (this.gridConsulta.Rows.Count < 10) this.btnProximo.Enabled = false;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/recurso");
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
                Recurso item = Recurso.Carregar(int.Parse(e.CommandArgument.ToString()));
                this.idRegistro.Value = item.ToString();
                this.viewDetalhes.DataSource = new List<Recurso>() { item };
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
                this.gridConsulta.DataSource = Recurso.Carregar((Session["parametros"] != null) ? (List<string>)Session["parametros"] : new List<string>(), pagina + 1, pagina + 10);
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
            Recurso ob = Recurso.Carregar(int.Parse(this.idRegistro.Value));
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
            Recurso ob = (int.Parse(this.idRegistro.Value) > 0) ? Recurso.Carregar(int.Parse(this.idRegistro.Value)) : new Recurso();
            ob.Codigo = this.txtCodigo.Text;
            ob.Descricao = this.txtDescricao.Text;
            ob.Fabricante = this.txtFabricante.Text;
            ob.Observacao = this.txtObservacao.Text;
            ob.Situacao = this.dropListSituacao.SelectedValue;
            ob.DataAquisicao = (this.txtDataAquisicao.Text != "") ? DateTime.Parse(this.txtDataAquisicao.Text, new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
            ob.DataInoperante = (this.txtDataInoperante.Text != "") ? DateTime.Parse(this.txtDataInoperante.Text, new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
            ob.DataEnvioManutencao = (this.txtDataEnvioManutencao.Text != "") ? DateTime.Parse(this.txtDataEnvioManutencao.Text, new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
            ob.DataRetornoManutencao = (this.txtDataRetornoManutencao.Text != "") ? DateTime.Parse(this.txtDataRetornoManutencao.Text, new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
            string result = "";
            if (int.Parse(this.idRegistro.Value) > 0)
            {
                result = ob.Atualizar();
            }
            else
            {
                ob.IdRecurso = Recurso.ID + 1;
                result = ob.Inserir();
                this.idRegistro.Value = Recurso.ID.ToString();
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
                this.labelMensagemAlerta.Text = ICONES_ALERTA.ICONE_ERRO + "O registro <b>N&Atilde;O</b> foi salvo!<br /> <b>ERRO:</b> " + result;
                this.panelMensagem.CssClass = CLASSES_ALERTA.ALERTA_ERRO;
            }
            this.timerMensagemAlerta.Enabled = true;
        }
        public void Carregar(int id)
        {
            Recurso ob = Recurso.Carregar(id);
            this.idRegistro.Value = ob.IdRecurso.ToString();
            this.txtCodigo.Text = ob.Codigo.ToString();
            this.txtDescricao.Text = ob.Descricao.ToString();
            this.txtFabricante.Text = ob.Fabricante.ToString();
            this.txtObservacao.Text = ob.Observacao.ToString();
            this.dropListSituacao.SelectedIndex = this.dropListSituacao.Items.IndexOf(this.dropListSituacao.Items.FindByValue(ob.Situacao));
            this.txtDataAquisicao.Text = ob.DataAquisicao.ToString("dd/MM/yyyy");
            this.txtDataInoperante.Text = ob.DataInoperante.ToString("dd/MM/yyyy");
            this.txtDataEnvioManutencao.Text = ob.DataEnvioManutencao.ToString("dd/MM/yyyy");
            this.txtDataRetornoManutencao.Text = ob.DataRetornoManutencao.ToString("dd/MM/yyyy");
        }
    }
}
