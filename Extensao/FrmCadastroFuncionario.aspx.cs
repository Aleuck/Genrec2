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
    public partial class FrmCadastroFuncionario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USUARIO_ID"] == null) Response.Redirect("~/login");
            if (!IsPostBack)
            {

                Session["parametros"] = null;
                this.gridConsulta.DataSource = Funcionario.Carregar(new List<string>(), 1, 10);
                this.gridConsulta.DataBind();
                if (this.gridConsulta.Rows.Count < 10) this.btnProximo.Enabled = false;
            }
        }
        private void PopulaDropList()
        {
            this.dropListCargo.DataSource = Cargo.Carregar(new List<string>(), 1);
            this.dropListCargo.DataBind();
            this.dropListCargo.Items.Insert(0, "");
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
            if (!string.IsNullOrEmpty(this.txtParanNome.Text)) parametros.Add("NOME UPPER(LIKE) '%" + this.txtParanNome.Text.ToUpper() + "%'");
            if (!string.IsNullOrEmpty(this.txtParanMatricula.Text)) parametros.Add("MATRICULA = '" + this.txtParanMatricula.Text + "'");
            this.gridConsulta.DataSource = Funcionario.Carregar(parametros, 1, 10);
            this.gridConsulta.DataBind();
            this.idPagina.Value = "10";
            Session["parametros"] = parametros;
            this.btnAnterior.Enabled = false;
            this.btnProximo.Enabled = true;
            if (this.gridConsulta.Rows.Count < 10) this.btnProximo.Enabled = false;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/funcionario");
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
                Funcionario item = Funcionario.Carregar(int.Parse(e.CommandArgument.ToString()));
                this.idRegistro.Value = item.ToString();
                this.viewDetalhes.DataSource = new List<Funcionario>() { item };
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
                this.gridConsulta.DataSource = Funcionario.Carregar((Session["parametros"] != null) ? (List<string>)Session["parametros"] : new List<string>(), pagina + 1, pagina + 10);
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
            Funcionario ob = Funcionario.Carregar(int.Parse(this.idRegistro.Value));
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
            Funcionario ob = (int.Parse(this.idRegistro.Value) > 0) ? Funcionario.Carregar(int.Parse(this.idRegistro.Value)) : new Funcionario();
            ob.Matricula = this.txtMatricula.Text;
            ob.Nome = this.txtNome.Text;
            ob.Cpf = this.txtCpf.Text;
            ob.DataNascimento = (this.txtDataNascimento.Text != "") ? DateTime.Parse(this.txtDataNascimento.Text, new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
            ob.Email = this.txtEmail.Text;
            ob.Senha = this.txtSenha.Text;
            ob.Cargo = Cargo.Carregar((dropListCargo.SelectedValue != "") ? int.Parse(this.dropListCargo.SelectedValue) : 0);
            ob.DataAdmissao = (this.txtDataAdmissao.Text != "") ? DateTime.Parse(this.txtDataAdmissao.Text, new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
            ob.DataDemissao = (this.txtDataDemissao.Text != "") ? DateTime.Parse(this.txtDataDemissao.Text, new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
            ob.Nivel = (this.dropListNivel.SelectedValue != "") ? int.Parse(this.dropListNivel.SelectedValue) : 0;
            ob.DataModificacaoPrioridade = (ob.Prioridade != int.Parse(this.dropListPrioridade.SelectedValue)) ? DateTime.Now : ob.DataModificacaoPrioridade;
            ob.Prioridade = (this.dropListPrioridade.SelectedValue != "") ? int.Parse(this.dropListPrioridade.SelectedValue) : 0;
            ob.IndiceDePenalidade = (this.txtIndiceDePenalidade.Text != "") ? int.Parse(this.txtIndiceDePenalidade.Text) : 0;
            string result = "";
            if (int.Parse(this.idRegistro.Value) > 0)
            {
                result = ob.Atualizar();
            }
            else
            {
                ob.IdFuncionario = Funcionario.ID + 1;
                result = ob.Inserir();
                this.idRegistro.Value = Funcionario.ID.ToString();
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
            Funcionario ob = Funcionario.Carregar(id);
            this.idRegistro.Value = ob.IdFuncionario.ToString();
            this.txtMatricula.Text = ob.Matricula.ToString();
            this.txtNome.Text = ob.Nome.ToString();
            this.txtCpf.Text = ob.Cpf.ToString();
            this.txtDataNascimento.Text = ob.DataNascimento.ToString("dd/MM/yyyy");
            this.txtEmail.Text = ob.Email.ToString();
            this.txtSenha.Attributes.Add("value", ob.Senha.ToString()); 
            this.dropListCargo.SelectedIndex = this.dropListCargo.Items.IndexOf(this.dropListCargo.Items.FindByValue(ob.Cargo.ToString()));
            this.txtDataAdmissao.Text = ob.DataAdmissao.ToString("dd/MM/yyyy");
            this.txtDataDemissao.Text = ob.DataDemissao.ToString("dd/MM/yyyy");
            this.dropListNivel.SelectedIndex = this.dropListNivel.Items.IndexOf(this.dropListNivel.Items.FindByValue(ob.Nivel.ToString()));
            this.dropListPrioridade.SelectedIndex = this.dropListPrioridade.Items.IndexOf(this.dropListPrioridade.Items.FindByValue(ob.Prioridade.ToString()));
            this.txtIndiceDePenalidade.Text = ob.IndiceDePenalidade.ToString();
        }
    }
}
