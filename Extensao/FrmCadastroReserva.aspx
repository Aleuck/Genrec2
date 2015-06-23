<%@ Page Title="" Language="C#" MasterPageFile="~/GenrecUsuario.master" AutoEventWireup="true" CodeBehind="FrmCadastroReserva.aspx.cs" Inherits="Genrec.FrmCadastroReserva" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Extensao_head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Extensao_Conteudo" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div id="content" class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:Panel ID="panelMensagem" runat="server">
                    <asp:Label ID="labelMensagemAlerta" runat="server" />
                    <asp:Timer ID="timerMensagemAlerta" runat="server" Interval="10000" Enabled="false" OnTick="TimerMensagem_Tick"/>
                </asp:Panel>
                <h1>Minhas Reservas</h1>
                <div class="ntid-box">
                    <asp:HiddenField ID="idRegistro" runat="server" Value="0"/>
                    <asp:Panel ID="panelDetalhes" runat="server"  Visible="false" CssClass="form-horizontal" role="form">
                        <fieldset>
                            <legend class="sem-borda ">Detalhes da Reserva</legend>
                            <ul class="nav nav-tabs">
                                <li id="navTabDados" runat="server" class="active"><a href="#<%= divViewDados.ClientID %>" data-toggle="tab">Dados</a></li>
                                <li id="navTabPeriodos" runat="server"><a href="#<%= divViewItens.ClientID %>" data-toggle="tab">Itens da Reserva</a></li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane active" id="divViewDados" runat="server">
                                    <asp:DetailsView ID="viewDetalhes" runat="server"  CssClass="table" GridLines="None" AutoGenerateRows="false">
                                    <RowStyle  ForeColor="#333333" BorderColor="#e9e9e9" />
                                    <FieldHeaderStyle Width="150px" Font-Bold="True" HorizontalAlign="Right" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Fields>
                                        <asp:BoundField DataField="Numero" HeaderText="Número:" />
                                        <asp:TemplateField HeaderText="Reservado Para:">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Funcionario.Nome")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Data:">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Data").ToString().Replace("00:00:00", "")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Observacao" HeaderText="Observacao:" />
                                    </Fields>
                                </asp:DetailsView>
                                <asp:Button ID="btnEditarView" CssClass="btn btn-primary" runat="server" Text="Editar" onclick="btnEditar_Click" CausesValidation="False"/>
                                <asp:Button ID="btnCancelarView" CssClass="btn btn-primary" runat="server" Text="Cancelar/Lista" onclick="btnCancelar_Click" CausesValidation="False"/>
                                </div>
                                <div class="tab-pane" id="divViewItens" runat="server">
                                    <br />
                                    <asp:UpdatePanel ID="upPanelItens" runat="server">                                                                      
                                        <ContentTemplate>
                                            <asp:Panel ID="panelMensagemItens" runat="server">
                                                <asp:Label ID="labelMensagemAlertaItens" runat="server" />
                                                <asp:Timer ID="timerMensagemAlertaItens" runat="server" Interval="10000" Enabled="false" OnTick="TimerMensagemItens_Tick"/>
                                            </asp:Panel>
                                            <div class="form-group form-inline col-md-12">
                                                <label class="col-md-2 control-label" for="dropListViewRecurso">Recurso Disponível:</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList runat="server" Width="710px" ID="dropListViewRecurso" CssClass="form-control" DataTextField="Descricao" DataValueField="IdRecurso" />
                                                </div>
                                                <div class="col-md-2">                                
                                                    <asp:Button ID="btnAdicionarNovoItem" CssClass="btn btn-default" runat="server" Text="Adicionar" onclick="btnAdicionarNovoItem_Click"/>
                                                </div>
                                            </div>
                                            <asp:GridView ID="gridItens" runat="server" Width="100%" HeaderStyle-BackColor="#F2EEE5" AutoGenerateColumns="False" onrowcommand="gridItens_RowCommand" GridLines="None" AllowPaging="False" AllowSorting="True" CssClass="table">
                                                <HeaderStyle BackColor="#F2EEE5" ForeColor="#4F4F4F" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Recurso">
                                                        <ItemTemplate>
                                                            <%# (DataBinder.Eval(Container.DataItem, "Situacao").ToString() == "C")? "<strike>":"" %> 
                                                               <%# DataBinder.Eval(Container.DataItem, "Recurso.Descricao")%>
                                                            <%# (DataBinder.Eval(Container.DataItem, "Situacao").ToString() == "C")? "</strike>":"" %> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Situação" HeaderStyle-Width="230px">
                                                        <ItemTemplate>
                                                            <%# (((DateTime)DataBinder.Eval(Container.DataItem, "Reserva.Data")) < DateTime.Now.Date && DataBinder.Eval(Container.DataItem, "Situacao").ToString() == "A") ? "Prescrito" : ""%> 
                                                            <%# (DataBinder.Eval(Container.DataItem, "Situacao").ToString() == "C")? "Cancelado por Superior":"" %> 
                                                            <%# (((DateTime)DataBinder.Eval(Container.DataItem, "Reserva.Data")) >= DateTime.Now.Date && DataBinder.Eval(Container.DataItem, "Situacao").ToString() == "A") ? "Aguardando Retirada" : ""%> 
                                                            <%# (DataBinder.Eval(Container.DataItem, "Situacao").ToString() == "L")? "Aguardando Devolução":"" %> 
                                                            <%# (DataBinder.Eval(Container.DataItem, "Situacao").ToString() == "R") ? "Devolvido " + DataBinder.Eval(Container.DataItem, "DataDevolucao").ToString().Replace("01/01/0001 00:00:00", "") : ""%> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnExcluirItem" runat="server" ToolTip="Remover Recurso" CommandName="Excluir" Visible='<%# (Eval("Situacao").ToString() == "A" && ((DateTime)Eval("Reserva.Data")) >= DateTime.Now.Date) ? true:false %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdItemReserva")%>' ><i class="glyphicon glyphicon-remove"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div class="alert alert-danger"><i class="glyphicon glyphicon-refresh"></i> Ops!!! Não há nenhum nesta reserva.</div>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </fieldset>                        
                    </asp:Panel>
                    <asp:Panel ID="panelFormulario" runat="server"  Visible="false" CssClass="form-horizontal" role="form">
                        <fieldset>
                            <legend>Cadastro</legend>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="txtNumero">Número:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtNumero" Enabled="false" placeholder="000000" runat="server" CssClass="form-control" Width="154px" MaxLength="10"/>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="dropListFuncionario">Funcionário:</label>
                                <div class="col-md-10">
                                    <asp:DropDownList ID="dropListFuncionario" Width="500px" Enabled="false" runat="server" CssClass="form-control" DataTextField="Nome" DataValueField="IdFuncionario" />
                                </div>
                            </div>
                            <div class="form-group form-inline">
                                <label class="col-md-2 control-label" for="txtData">Data:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtData" runat="server" placeholder="DD/MM/AAAA" CssClass="form-control"/><asp:CalendarExtender ID="calendarData" runat="server" TargetControlID="txtData" PopupButtonID="btnCalendarData" Format="dd/MM/yyyy" />&nbsp;<asp:LinkButton ID="btnCalendarData" runat="server" CssClass="glyphicon glyphicon-calendar form-control-feedback" CausesValidation="False"></asp:LinkButton><span class="help-block"><asp:RegularExpressionValidator ID="regularExpressionData" runat="server" ErrorMessage="Formato invalido" ControlToValidate="txtData" ValidationExpression="\d{2}\/\d{2}\/\d{4}" SetFocusOnError="True" /></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="txtObservacao">Observação:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtObservacao" runat="server" CssClass="form-control" Width="500px" TextMode="MultiLine"/><span class="help-block"><asp:RegularExpressionValidator ID="regularExpressionObservacao" runat="server" ErrorMessage="Limite de caracteres excedido" ControlToValidate="txtObservacao" ValidationExpression="[\s\S]{1,500}" SetFocusOnError="True" /></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label"></label>
                                <div class="col-md-10">
                                    <asp:Button ID="btnSalvar" CssClass="btn btn-default" runat="server" Text="Salvar" onclick="btnSalvar_Click"/>
                                    <asp:Button ID="btnCancelar" CssClass="btn btn-primary" runat="server" Text="Cancelar/Lista" onclick="btnCancelar_Click" CausesValidation="False"/>
                                </div>
                            </div>
                       </fieldset>
                   </asp:Panel>
                   <asp:Panel ID="panelPesquisa" runat="server" role="form" CssClass="form-horizontal">
                        <fieldset>
                            <legend>Pesquisar Reservas</legend>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="txtParanNumero">Número:</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtParanNumero" runat="server" CssClass="form-control" MaxLength="10" type="search" results="20" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnPesquisar" CssClass="btn btn-default" runat="server" Text="Pesquisar" onclick="btnPesquisar_Click"/>
                                </div>
                            </div>
                        </fieldset>
                        <asp:GridView ID="gridConsulta" runat="server" Width="100%" HeaderStyle-BackColor="#F2EEE5" AutoGenerateColumns="False" onrowcommand="gridConsulta_RowCommand" GridLines="None" AllowPaging="False" AllowSorting="True" CssClass="table table-hover">
                            <HeaderStyle BackColor="#F2EEE5" ForeColor="#4F4F4F" />
                            <Columns>
                                 <asp:BoundField DataField="Numero" HeaderText="Número" HeaderStyle-Width="75px"/>
                                 <asp:TemplateField HeaderText="Data" HeaderStyle-Width="150px">
                                    <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "Data").ToString().Replace("00:00:00", "")%>
                                    </ItemTemplate>
                                 </asp:TemplateField>                               
                                 <asp:TemplateField HeaderText="Reservado Para" >
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Funcionario.Nome")%>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="A&ccedil&otilde;es" HeaderStyle-Width="75px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnSelecionar" runat="server" ToolTip="Visualizar em Detalhes" CommandName="Selecionar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdReserva")%>' ><i class="glyphicon glyphicon-folder-open"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnEditar" runat="server" ToolTip="Editar Registro" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdReserva")%>' ><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnExcluir" runat="server" ToolTip="Remover Registro" CommandName="Excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdReserva")%>' ><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                        <asp:ConfirmButtonExtender ID="btnExcluir_ConfirmButtonExtender" runat="server" ConfirmText="Deseja realmente excluir este registro?" TargetControlID="btnExcluir" />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="idPagina" runat="server" Value="10" />
                        <asp:Button ID="btnAnterior" Enabled="false" runat="server" CssClass="btn btn-primary" CommandName="Paginar" CommandArgument="Anterior" Text="Anterior" OnCommand="btnPaginar_Command"/>&nbsp;
                        <asp:Button ID="btnProximo" runat="server" CssClass="btn btn-primary" CommandName="Paginar" CommandArgument="Proximo" Text="Pr&oacute;ximo" OnCommand="btnPaginar_Command"/>&nbsp;
                        <asp:Button ID="btnNovo" CssClass="btn btn-success" runat="server" Text="Solicitar Nova Reserva" onclick="btnNovo_Click" />
                   </asp:Panel>
               </div>
           </div>
       </div>
   </div>
</asp:Content>
