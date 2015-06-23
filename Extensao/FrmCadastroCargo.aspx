<%@ Page Title="" Language="C#" MasterPageFile="~/GenrecAdmin.master" AutoEventWireup="true" CodeBehind="FrmCadastroCargo.aspx.cs" Inherits="Genrec.FrmCadastroCargo" %>
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
                <h1>Cargo</h1>
                <div class="ntid-box">
                    <asp:HiddenField ID="idRegistro" runat="server" Value="0"/>
                    <asp:Panel ID="panelDetalhes" runat="server"  Visible="false" CssClass="form-horizontal" role="form">
                        <fieldset>
                            <legend class="sem-borda">Detalhes de Cargo</legend>
                            <asp:DetailsView ID="viewDetalhes" runat="server"  CssClass="table" GridLines="None" AutoGenerateRows="false">
                                <RowStyle  ForeColor="#333333" BorderColor="#e9e9e9" />
                                <FieldHeaderStyle Width="150px" Font-Bold="True" HorizontalAlign="Right" />
                                <AlternatingRowStyle BackColor="White" />
                                <Fields>
                                    <asp:BoundField DataField="Nome" HeaderText="Nome:" />
                                    <asp:BoundField DataField="PrioridadePadrao" HeaderText="Prioridade Padrão:" />
                                </Fields>
                            </asp:DetailsView>
                        </fieldset>
                        <asp:Button ID="btnEditarView" CssClass="btn btn-primary" runat="server" Text="Editar" onclick="btnEditar_Click" CausesValidation="False"/>
                        <asp:Button ID="btnCancelarView" CssClass="btn btn-primary" runat="server" Text="Cancelar/Lista" onclick="btnCancelar_Click" CausesValidation="False"/>
                    </asp:Panel>
                    <asp:Panel ID="panelFormulario" runat="server"  Visible="false" CssClass="form-horizontal" role="form">
                        <fieldset>
                            <legend>Cadastro de Cargo</legend>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="txtNome">Nome:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtNome" placeholder="Informe o Nome" runat="server" CssClass="form-control" Width="280px" MaxLength="40"/><asp:RequiredFieldValidator ID="requeriedFieldNome" runat="server" ErrorMessage="Campo Obrigatório" ControlToValidate="txtNome" SetFocusOnError="True" ToolTip="Campo Obrigatório" CssClass="text-warning" ><i class="glyphicon  glyphicon-info-sign"></i> Campo Obrigatório</asp:RequiredFieldValidator><span class="help-block"></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="dropListPrioridadePadrao">Prioridade Padrão:</label>
                                <div class="col-md-10">
                                    <asp:DropDownList ID="dropListPrioridadePadrao"  runat="server" CssClass="form-control" Width="154px">
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="1" Value="1" />
                                        <asp:ListItem Text="2" Value="2" />
                                        <asp:ListItem Text="3" Value="3" />
                                        <asp:ListItem Text="4" Value="4" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="requeriedFieldPrioridadePadrao" runat="server" ErrorMessage="Campo Obrigatório" ControlToValidate="dropListPrioridadePadrao" SetFocusOnError="True" ToolTip="Campo Obrigatório" CssClass="text-warning" ><i class="glyphicon  glyphicon-info-sign"></i> Campo Obrigatório</asp:RequiredFieldValidator>
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
                            <legend>Pesquisar Cargo</legend>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="txtParan1">Nome:</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtParanNome" runat="server" CssClass="form-control" MaxLength="100" type="search" results="20"/>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnPesquisar" CssClass="btn btn-default" runat="server" Text="Pesquisar" onclick="btnPesquisar_Click"/>
                                </div>
                            </div>
                        </fieldset>
                        <asp:GridView ID="gridConsulta" runat="server" Width="100%" HeaderStyle-BackColor="#F2EEE5" AutoGenerateColumns="False" onrowcommand="gridConsulta_RowCommand" GridLines="None" AllowPaging="False" AllowSorting="True" CssClass="table table-hover">
                            <HeaderStyle BackColor="#F2EEE5" ForeColor="#4F4F4F" />
                            <Columns>
                                 <asp:BoundField DataField="Nome" HeaderText="Nome" />
                                 <asp:BoundField DataField="PrioridadePadrao" HeaderText="Prioridade Padrão"  HeaderStyle-Width="150px"/>
                                 <asp:TemplateField HeaderText="A&ccedil&otilde;es" HeaderStyle-Width="75px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnSelecionar" runat="server" ToolTip="Visualizar em Detalhes" CommandName="Selecionar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdCargo")%>' ><i class="glyphicon glyphicon-folder-open"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnEditar" runat="server" ToolTip="Editar Registro" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdCargo")%>' ><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnExcluir" runat="server" ToolTip="Remover Registro" CommandName="Excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdCargo")%>' ><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
                                        <asp:ConfirmButtonExtender ID="btnExcluir_ConfirmButtonExtender" runat="server" ConfirmText="Deseja realmente excluir este registro?" TargetControlID="btnExcluir" />
                                    </ItemTemplate>
                                 </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="idPagina" runat="server" Value="10" />
                        <asp:Button ID="btnAnterior" Enabled="false" runat="server" CssClass="btn btn-primary" CommandName="Paginar" CommandArgument="Anterior" Text="Anterior" OnCommand="btnPaginar_Command"/>&nbsp;
                        <asp:Button ID="btnProximo" runat="server" CssClass="btn btn-primary" CommandName="Paginar" CommandArgument="Proximo" Text="Pr&oacute;ximo" OnCommand="btnPaginar_Command"/>&nbsp;
                        <asp:Button ID="btnNovo" CssClass="btn btn-success" runat="server" Text="Cadastrar Novo" onclick="btnNovo_Click" />
                   </asp:Panel>
               </div>
           </div>
       </div>
   </div>
</asp:Content>
