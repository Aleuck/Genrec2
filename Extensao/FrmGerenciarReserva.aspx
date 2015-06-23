<%@ Page Title="" Language="C#" MasterPageFile="~/GenrecAlmoxarifado.master" AutoEventWireup="true" CodeBehind="FrmGerenciarReserva.aspx.cs" Inherits="Genrec.FrmGerenciarReserva" %>
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
                <h1>Gerenciar Reservas</h1>
                <div class="ntid-box">
                    <asp:HiddenField ID="idRegistro" runat="server" Value="0"/>
                    <asp:Panel ID="panelDetalhes" runat="server"  Visible="false" CssClass="form-horizontal" role="form">
                        <fieldset>
                            <legend class="sem-borda">Detalhes da Reserva</legend>
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
                                    <asp:Button ID="btnCancelarView" CssClass="btn btn-primary" runat="server" Text="Voltar Gerenciar" onclick="btnCancelar_Click" CausesValidation="False"/>
                                </div>
                                <div class="tab-pane" id="divViewItens" runat="server">
                                    <br />
                                    <asp:UpdatePanel ID="upPanelItens" runat="server">                                                                      
                                        <ContentTemplate>
                                            <asp:Panel ID="panelMensagemItens" runat="server">
                                                <asp:Label ID="labelMensagemAlertaItens" runat="server" />
                                                <asp:Timer ID="timerMensagemAlertaItens" runat="server" Interval="10000" Enabled="false" OnTick="TimerMensagemItens_Tick"/>
                                            </asp:Panel>
                                            <br />
                                            <asp:GridView ID="gridItens" runat="server" Width="100%" HeaderStyle-BackColor="#F2EEE5" AutoGenerateColumns="False" onrowcommand="gridItens_RowCommand" GridLines="None" AllowPaging="False" AllowSorting="True" CssClass="table">
                                                <HeaderStyle BackColor="#F2EEE5" ForeColor="#4F4F4F" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Recurso">
                                                        <ItemTemplate>
                                                             <%# DataBinder.Eval(Container.DataItem, "Recurso.Descricao")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Data da Retirada">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "DataRetirada").ToString().Replace("01/01/0001 00:00:00", "")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Data Devolução">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "DataDevolucao").ToString().Replace("01/01/0001 00:00:00", "")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Opções" HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnLiberar" runat="server" ToolTip="Liberar recurso" CommandName="Liberar"  Visible='<%# (Eval("Situacao").ToString() == "A") ? true:false %>' Enabled='<%# (Eval("Situacao").ToString() == "A")? true:false %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdItemReserva")%>' ><i class="glyphicon glyphicon-ok"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="btnRecusar" runat="server" ToolTip="Cancelar recurso" CommandName="Cancelar" Visible='<%# (Eval("Situacao").ToString() == "A") ? true:false %>' Enabled='<%# (Eval("Situacao").ToString() == "A")? true:false %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdItemReserva")%>' ><i class="glyphicon glyphicon-thumbs-down"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="btnConfirmaRecebimento" runat="server" ToolTip="Notificar devolução" CommandName="Receber" Visible='<%# (Eval("Situacao").ToString() == "L") ? true:false %>' Enabled='<%# (Eval("Situacao").ToString() == "L") ? true:false %>'  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdItemReserva")%>' ><i class="glyphicon glyphicon-thumbs-up"></i></asp:LinkButton>
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
                                 <asp:TemplateField HeaderText="A&ccedil&otilde;es" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnSelecionar" runat="server" ToolTip="Visualizar a reserva" CommandName="Selecionar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdReserva")%>' ><i class="glyphicon glyphicon-list"></i> Verificar</asp:LinkButton>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="idPagina" runat="server" Value="10" />
                        <asp:Button ID="btnAnterior" Enabled="false" runat="server" CssClass="btn btn-primary" CommandName="Paginar" CommandArgument="Anterior" Text="Anterior" OnCommand="btnPaginar_Command"/>&nbsp;
                        <asp:Button ID="btnProximo" runat="server" CssClass="btn btn-primary" CommandName="Paginar" CommandArgument="Proximo" Text="Pr&oacute;ximo" OnCommand="btnPaginar_Command"/>&nbsp;
                   </asp:Panel>
               </div>
           </div>
       </div>
   </div>
</asp:Content>
