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
    public partial class FrmCadastroReserva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USUARIO_ID"] == null) Response.Redirect("~/login");
            if (!IsPostBack)
            {
                Session["parametros"] = null;
                this.gridConsulta.DataSource = Reserva.Carregar(new List<string>() { "ID_FUNCIONARIO = " + Session["USUARIO_ID"].ToString() }, 1, 10);
                this.gridConsulta.DataBind();
                if (this.gridConsulta.Rows.Count < 10) this.btnProximo.Enabled = false;
                if (Funcionario.Carregar(int.Parse(Session["USUARIO_ID"].ToString()),1).IndiceDePenalidade >= 2)
                {
                    this.btnNovo.Enabled = false;
                    this.labelMensagemAlerta.Text = ICONES_ALERTA.ICONE_CADEADO + "&nbsp; ATENÇÃO: Você foi bloqueado por não devolver os recursos retirados no prazo estipulado. Para desbloqueio devolva os recursos.";
                    this.panelMensagem.CssClass = CLASSES_ALERTA.ALERTA_ATENCAO;
                    this.timerMensagemAlerta.Enabled = true;
                }
            }
        }
        private void PopulaDropList()
        {
            this.dropListFuncionario.DataSource = Funcionario.Carregar(new List<string>(), 1);
            this.dropListFuncionario.DataBind();
            this.dropListFuncionario.SelectedIndex = this.dropListFuncionario.Items.IndexOf(this.dropListFuncionario.Items.FindByValue(Session["USUARIO_ID"].ToString()));
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
            List<string> parametros = new List<string>() { "ID_FUNCIONARIO = " + Session["USUARIO_ID"].ToString() };
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
            Response.Redirect("~/meu-genrec");
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            this.panelDetalhes.Visible = false;
            this.gridConsulta_RowCommand(sender, new GridViewCommandEventArgs(null, new CommandEventArgs("Editar", this.idRegistro.Value)));
        }
        protected void gridConsulta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                this.panelFormulario.Visible = true;
                PopulaDropList();
                Carregar(int.Parse(e.CommandArgument.ToString()));
                DateTime data = (this.txtData.Text != "") ? DateTime.Parse(this.txtData.Text, new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                this.txtData.Enabled = (ItemReerva.Carregar(new List<string>() { "ID_RESERVA = " + e.CommandArgument.ToString() }, 1).Count == 0) || (data < DateTime.Now.Date);
                this.btnCalendarData.Enabled = this.txtData.Enabled;
                this.panelPesquisa.Visible = false;
            }
            if (e.CommandName == "Selecionar")
            {
                Reserva item = Reserva.Carregar(int.Parse(e.CommandArgument.ToString()));
                this.idRegistro.Value = item.ToString();
                this.viewDetalhes.DataSource = new List<Reserva>() { item };
                this.viewDetalhes.DataBind();
                this.AtualizarDropListViewRecurso(item);
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
        protected void AtualizarDropListViewRecurso(Reserva item)
        {
            this.dropListViewRecurso.DataSource = Recurso.Carregar(new List<string>() { "SITUACAO = 'A'" }).OrderBy(i => i.Descricao).ToList<Recurso>();
            this.dropListViewRecurso.DataBind();
            this.dropListViewRecurso.Items.Insert(0, "");
            for (int p = 1; p < this.dropListViewRecurso.Items.Count; p++)
            {
                string value = this.dropListViewRecurso.Items[p].Value;
                List<ItemReerva> reservas = ItemReerva.Carregar(new List<string>() { "ID_RECURSO = " + value, "SITUACAO not in ('C','R')" }, 5).Where(r => r.Reserva.Data == item.Data).ToList<ItemReerva>();
                foreach (ItemReerva i in reservas)
                {
                    if (int.Parse(Session["USUARIO_PRIORIDADE"].ToString()) < i.Reserva.Funcionario.Prioridade)
                    {
                        this.dropListViewRecurso.Items[p].Text += "  [Sem prioridade]";
                        this.dropListViewRecurso.Items[p].Value = "R" + value;
                    }
                    else
                    {
                        this.dropListViewRecurso.Items[p].Text += "  [Indisponível]";
                        this.dropListViewRecurso.Items[p].Value = "I" + value;
                    }
                }
                if (reservas.Count == 0)
                {
                    this.dropListViewRecurso.Items[p].Text += "  [Disponível]";
                    this.dropListViewRecurso.Items[p].Value = "D" + value;
                }
            }
            this.gridItens.DataSource = ItemReerva.Carregar(new List<string>() { "ID_RESERVA = " + item.IdReserva.ToString() }, 2);
            this.gridItens.DataBind();
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
        protected void btnNovo_Click(object sender, EventArgs e)
        {
            PopulaDropList();
            this.idRegistro.Value = "0";
            this.txtNumero.Text = new Random().Next(100000, 900000).ToString();
            this.txtData.Enabled = true;
            this.btnCalendarData.Enabled = this.txtData.Enabled;
            this.panelFormulario.Visible = true;
            this.panelPesquisa.Visible = false;
            this.panelDetalhes.Visible = false;
        }
        public void Excluir()
        {
            Reserva ob = Reserva.Carregar(int.Parse(this.idRegistro.Value));
            string result = ob.Excluir();
            if (string.IsNullOrEmpty(result))
            {
                this.labelMensagemAlerta.Text = ICONES_ALERTA.ICONE_OK + "&nbsp; O registro foi excluido com sucesso!";
                this.panelMensagem.CssClass = CLASSES_ALERTA.ALERTA_SUCESSO;
            }
            else
            {
                this.labelMensagemAlerta.Text = ICONES_ALERTA.ICONE_ERRO + "&nbsp; O registro <b>N&Atilde;O</b> foi excluido!<br /> <b>ERRO:</b> " + result;
                this.panelMensagem.CssClass = CLASSES_ALERTA.ALERTA_ERRO;
            }
            this.timerMensagemAlerta.Enabled = true;
        }
        public void Salvar()
        {
            Reserva ob = (int.Parse(this.idRegistro.Value) > 0) ? Reserva.Carregar(int.Parse(this.idRegistro.Value)) : new Reserva();
            ob.Numero = this.txtNumero.Text;
            ob.Funcionario = Funcionario.Carregar((this.dropListFuncionario.SelectedValue != "") ? int.Parse(this.dropListFuncionario.SelectedValue) : 0);
            DateTime data = (this.txtData.Text != "") ? DateTime.Parse(this.txtData.Text, new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
            ob.Observacao = this.txtObservacao.Text;
            string result = "";
            if (int.Parse(this.idRegistro.Value) > 0)
            {
                if (this.txtData.Enabled && data >= DateTime.Now.Date || data == ob.Data)
                {
                    result = ob.Atualizar();
                }
                else
                {
                    result = "A data de reserva é inválida!";
                }
            }
            else
            {
                if (data >= DateTime.Now.Date)
                {
                    ob.Data = data;
                    ob.IdReserva = Reserva.ID + 1;
                    result = ob.Inserir();
                    this.idRegistro.Value = Reserva.ID.ToString();
                }
                else
                {
                    result = "A data de reserva é inválida!";
                }
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
            Reserva ob = Reserva.Carregar(id);
            this.idRegistro.Value = ob.IdReserva.ToString();
            this.txtNumero.Text = ob.Numero.ToString();
            this.dropListFuncionario.SelectedIndex = this.dropListFuncionario.Items.IndexOf(this.dropListFuncionario.Items.FindByValue(ob.Funcionario.ToString()));
            this.txtData.Text = ob.Data.ToString("dd/MM/yyyy");
            this.txtObservacao.Text = ob.Observacao.ToString();
        }
        protected void TimerMensagemItens_Tick(object sender, EventArgs e)
        {
            this.timerMensagemAlertaItens.Enabled = false;
            this.labelMensagemAlertaItens.Text = "";
            this.panelMensagemItens.CssClass = CLASSES_ALERTA.OCULTO;
        }
        protected void btnAdicionarNovoItem_Click(object sender, EventArgs e)
        {
            if (this.dropListViewRecurso.SelectedValue == "") return;
            string valorSelecionado = this.dropListViewRecurso.SelectedValue;
            Recurso rec = Recurso.Carregar(int.Parse(valorSelecionado.Substring(1, valorSelecionado.Length-1)));
            Reserva res = Reserva.Carregar(int.Parse(idRegistro.Value.ToString()));
            switch (valorSelecionado[0])
            {
                case 'D':
                    break;
                case 'I':
                    this.labelMensagemAlertaItens.Text = ICONES_ALERTA.ICONE_ERRO + "&nbsp; O item possui reservas!";
                    this.panelMensagemItens.CssClass = CLASSES_ALERTA.ALERTA_ERRO;
                    this.timerMensagemAlertaItens.Enabled = true;
                    return;
                case 'R':
                    List<ItemReerva> reservas = ItemReerva.Carregar(new List<string>() { "ID_RECURSO = " + rec.IdRecurso.ToString(), "SITUACAO not in ('C','R')" }, 5).Where(r => r.Reserva.Data == res.Data).ToList<ItemReerva>();
                    foreach (ItemReerva i in reservas)
                    {
                        if (int.Parse(Session["USUARIO_PRIORIDADE"].ToString()) < i.Reserva.Funcionario.Prioridade)
                        {
                            i.Situacao = "C";
                            i.DataCancelamento = DateTime.Now;
                            i.Atualizar();
                        }
                        else
                        {
                            this.labelMensagemAlertaItens.Text = ICONES_ALERTA.ICONE_ERRO + "&nbsp; O item possui reservas!";
                            this.panelMensagemItens.CssClass = CLASSES_ALERTA.ALERTA_ERRO;
                            this.timerMensagemAlertaItens.Enabled = true;
                            return;
                        }
                    }
                    break;
                default: return;
            }            
            if (ItemReerva.Carregar(new List<string>() { "ID_RESERVA = " + res.IdReserva.ToString(), "ID_RECURSO = " + rec.IdRecurso.ToString() }, 1).Count > 0)
            {
                this.labelMensagemAlertaItens.Text = ICONES_ALERTA.ICONE_ERRO + "&nbsp;  O recurso já está na reserva!";
                this.panelMensagemItens.CssClass = CLASSES_ALERTA.ALERTA_ERRO;
                this.timerMensagemAlertaItens.Enabled = true;
                return;
            }           

            ItemReerva novoItem = new ItemReerva();
            novoItem.IdItemReserva = ItemReerva.ID + 1;
            novoItem.Reserva = res;
            novoItem.Recurso = rec;
            novoItem.Situacao = "A";
            string result = novoItem.Inserir();
            if (string.IsNullOrEmpty(result))
            {
                this.AtualizarDropListViewRecurso(res);
            }
            else
            {
                this.labelMensagemAlertaItens.Text = ICONES_ALERTA.ICONE_ERRO + "&nbsp; O item não foi adicionado!";
                this.panelMensagemItens.CssClass = CLASSES_ALERTA.ALERTA_ERRO;
            }
            this.timerMensagemAlertaItens.Enabled = true;

        }
        protected void gridItens_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                ItemReerva item = ItemReerva.Carregar(int.Parse(e.CommandArgument.ToString()));
                string result = item.Excluir();
                if (!string.IsNullOrEmpty(result))
                {
                    this.labelMensagemAlertaItens.Text = ICONES_ALERTA.ICONE_ERRO + "&nbsp; Impossivel.";
                    this.panelMensagemItens.CssClass = CLASSES_ALERTA.ALERTA_ERRO;
                    this.timerMensagemAlertaItens.Enabled = true;
                }
                this.AtualizarDropListViewRecurso(item.Reserva);
            }
        }
    }
}
