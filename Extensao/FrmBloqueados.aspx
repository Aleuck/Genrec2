<%@ Page Title="" Language="C#" MasterPageFile="~/GenrecUsuario.master" AutoEventWireup="true" CodeBehind="FrmBloqueados.aspx.cs" Inherits="Genrec.FrmBloqueados" %>
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
                <h1>BLOQUEADOS</h1>
                <div class="ntid-box">
                    <br />
                    <asp:GridView ID="gridConsulta" runat="server" Width="100%" HeaderStyle-BackColor="#F2EEE5" AutoGenerateColumns="False" onrowcommand="gridConsulta_RowCommand" GridLines="None" AllowPaging="False" AllowSorting="True" CssClass="table table-hover">
                        <HeaderStyle BackColor="#F2EEE5" ForeColor="#4F4F4F" />
                        <Columns>
                                <asp:BoundField DataField="Matricula" HeaderText="Matricula" />
                                <asp:BoundField DataField="Nome" HeaderText="Nome" />
                                <asp:BoundField DataField="Email" HeaderText="E-mail" />
                                <asp:TemplateField HeaderText="Cargo">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Cargo.Nome")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="A&ccedil&otilde;es" HeaderStyle-Width="110px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDesbloquear" CssClass="btn btn-link" runat="server" ToolTip="Visualizar em Detalhes" CommandName="Desbloquear" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdFuncionario")%>' ><i class="glyphicon glyphicon-heart-empty"></i> Desbloquear</asp:LinkButton>
                                    <asp:ConfirmButtonExtender ID="btnbtnDesbloquear_ConfirmButtonExtender" runat="server" ConfirmText="Deseja realmente desbloquear este usuario?" TargetControlID="btnDesbloquear" />
                                </ItemTemplate>
                                </asp:TemplateField>                                 
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="alert alert-info"> Não existem usuários para você desbloquear!</div>
                        </EmptyDataTemplate>
                    </asp:GridView>
               </div>
           </div>
       </div>
   </div>
</asp:Content>
