<%@ Page Title="" Language="C#" MasterPageFile="~/GenrecAdmin.master" AutoEventWireup="true" CodeBehind="FrmCadastroRecurso.aspx.cs" Inherits="Genrec.FrmCadastroRecurso" %>
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
                <h1>Recurso</h1>
                <div class="ntid-box">
                    <asp:HiddenField ID="idRegistro" runat="server" Value="0"/>
                    <asp:Panel ID="panelDetalhes" runat="server"  Visible="false" CssClass="form-horizontal" role="form">
                        <fieldset>
                            <legend class="sem-borda">Detalhes de Recurso</legend>
                            <asp:DetailsView ID="viewDetalhes" runat="server"  CssClass="table" GridLines="None" AutoGenerateRows="false">
                                <RowStyle  ForeColor="#333333" BorderColor="#e9e9e9" />
                                <FieldHeaderStyle Width="150px" Font-Bold="True" HorizontalAlign="Right" />
                                <AlternatingRowStyle BackColor="White" />
                                <Fields>
                                    <asp:BoundField DataField="Codigo" HeaderText="Código:" />
                                    <asp:BoundField DataField="Descricao" HeaderText="Descrição:" />
                                    <asp:BoundField DataField="Fabricante" HeaderText="Fabricante:" />
                                    <asp:BoundField DataField="Observacao" HeaderText="Observação:" />
                                    <asp:TemplateField HeaderText="Situação:">
                                        <ItemTemplate>
                                            <%# (DataBinder.Eval(Container.DataItem, "Situacao").ToString() == "A") ? "Ativo" : "Inativo"%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data de Aquisição:">
                                        <ItemTemplate>
                                            <%# ((DateTime)DataBinder.Eval(Container.DataItem, "DataAquisicao")).ToString("dd/MM/yyyy")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data da Inoperação:">
                                        <ItemTemplate>
                                            <%# ((DateTime)DataBinder.Eval(Container.DataItem, "DataInoperante")).ToString("dd/MM/yyyy")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data do Envio para Manutencao:">
                                        <ItemTemplate>
                                            <%# ((DateTime)DataBinder.Eval(Container.DataItem, "DataEnvioManutencao")).ToString("dd/MM/yyyy")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data do Retorno da Manutencao:">
                                        <ItemTemplate>
                                            <%# ((DateTime)DataBinder.Eval(Container.DataItem, "DataRetornoManutencao")).ToString("dd/MM/yyyy")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Fields>
                            </asp:DetailsView>
                        </fieldset>
                        <asp:Button ID="btnEditarView" CssClass="btn btn-primary" runat="server" Text="Editar" onclick="btnEditar_Click" CausesValidation="False"/>
                        <asp:Button ID="btnCancelarView" CssClass="btn btn-primary" runat="server" Text="Cancelar/Lista" onclick="btnCancelar_Click" CausesValidation="False"/>
                    </asp:Panel>
                    <asp:Panel ID="panelFormulario" runat="server"  Visible="false" CssClass="form-horizontal" role="form">
                        <fieldset>
                            <legend>Cadastro de Recurso</legend>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="txtCodigo">Código:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtCodigo"  runat="server" CssClass="form-control" Width="154px" MaxLength="8"/><asp:RequiredFieldValidator ID="requeriedFieldCodigo" runat="server" ErrorMessage="Campo Obrigatório" ControlToValidate="txtCodigo" SetFocusOnError="True" ToolTip="Campo Obrigatório" CssClass="text-warning" ><i class="glyphicon  glyphicon-info-sign"></i> Campo Obrigatório</asp:RequiredFieldValidator><span class="help-block"></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="txtDescricao">Descrição:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtDescricao" runat="server" CssClass="form-control" Width="700px" MaxLength="100"/><asp:RequiredFieldValidator ID="requeriedFieldDescricao" runat="server" ErrorMessage="Campo Obrigatório" ControlToValidate="txtDescricao" SetFocusOnError="True" ToolTip="Campo Obrigatório" CssClass="text-warning" ><i class="glyphicon  glyphicon-info-sign"></i> Campo Obrigatório</asp:RequiredFieldValidator><span class="help-block"></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="txtFabricante">Fabricante:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtFabricante" placeholder="Informe o nome do Fabricante" runat="server" CssClass="form-control" Width="700px" MaxLength="100"/><span class="help-block"></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="txtObservacao">Observação:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtObservacao" placeholder="Livre" runat="server" CssClass="form-control" Width="500px" TextMode="MultiLine"/><span class="help-block"><asp:RegularExpressionValidator ID="regularExpressionObservacao" runat="server" ErrorMessage="Limite de caracteres excedido" ControlToValidate="txtObservacao" ValidationExpression="[\s\S]{1,500}" SetFocusOnError="True" /></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="dropListSituacao">Situação:</label>
                                <div class="col-md-10">
                                    <asp:DropDownList runat="server" ID="dropListSituacao" CssClass="form-control" Width="154px">
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="Ativo" Value="A" />
                                        <asp:ListItem Text="Inativo" Value="I" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="requeriedFieldSituacao" runat="server" ErrorMessage="Campo Obrigatório" ControlToValidate="dropListSituacao" SetFocusOnError="True" ToolTip="Campo Obrigatório" CssClass="text-warning" ><i class="glyphicon  glyphicon-info-sign"></i> Campo Obrigatório</asp:RequiredFieldValidator><span class="help-block"></span>
                                </div>
                            </div>
                            <div class="form-group form-inline">
                                <label class="col-md-2 control-label" for="txtDataAquisicao">Data de Aquisição:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtDataAquisicao" Width="154px" runat="server" placeholder="DD/MM/AAAA" CssClass="form-control"/><asp:CalendarExtender ID="calendarDataAquisicao" runat="server" TargetControlID="txtDataAquisicao" PopupButtonID="btnCalendarDataAquisicao" Format="dd/MM/yyyy" />&nbsp;<asp:LinkButton ID="btnCalendarDataAquisicao" runat="server" CssClass="glyphicon glyphicon-calendar form-control-feedback" CausesValidation="False"></asp:LinkButton><asp:RequiredFieldValidator ID="requeriedFieldDataAquisicao" runat="server" ErrorMessage="Campo Obrigatório" ControlToValidate="txtDataAquisicao" SetFocusOnError="True" ToolTip="Campo Obrigatório" CssClass="text-warning" ><i class="glyphicon  glyphicon-info-sign"></i> Campo Obrigatório</asp:RequiredFieldValidator><span class="help-block"><asp:RegularExpressionValidator ID="regularExpressionDataAquisicao" runat="server" ErrorMessage="Formato invalido" ControlToValidate="txtDataAquisicao" ValidationExpression="\d{2}\/\d{2}\/\d{4}" SetFocusOnError="True" /></span>
                                </div>
                            </div>
                            <div class="form-group form-inline">
                                <label class="col-md-2 control-label" for="txtDataInoperante">Data de Inoperação:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtDataInoperante" Width="154px" runat="server" placeholder="DD/MM/AAAA" CssClass="form-control"/><asp:CalendarExtender ID="calendarDataInoperante" runat="server" TargetControlID="txtDataInoperante" PopupButtonID="btnCalendarDataInoperante" Format="dd/MM/yyyy" />&nbsp;<asp:LinkButton ID="btnCalendarDataInoperante" runat="server" CssClass="glyphicon glyphicon-calendar form-control-feedback" CausesValidation="False"></asp:LinkButton><span class="help-block"><asp:RegularExpressionValidator ID="regularExpressionDataInoperante" runat="server" ErrorMessage="Formato invalido" ControlToValidate="txtDataInoperante" ValidationExpression="\d{2}\/\d{2}\/\d{4}" SetFocusOnError="True" /></span>
                                </div>
                            </div>
                            <div class="form-group form-inline">
                                <label class="col-md-2 control-label" for="txtDataEnvioManutencao">Data de Envio de Manutenção:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtDataEnvioManutencao" Width="154px" runat="server" placeholder="DD/MM/AAAA" CssClass="form-control"/><asp:CalendarExtender ID="calendarDataEnvioManutencao" runat="server" TargetControlID="txtDataEnvioManutencao" PopupButtonID="btnCalendarDataEnvioManutencao" Format="dd/MM/yyyy" />&nbsp;<asp:LinkButton ID="btnCalendarDataEnvioManutencao" runat="server" CssClass="glyphicon glyphicon-calendar form-control-feedback" CausesValidation="False"></asp:LinkButton><span class="help-block"><asp:RegularExpressionValidator ID="regularExpressionDataEnvioManutencao" runat="server" ErrorMessage="Formato invalido" ControlToValidate="txtDataEnvioManutencao" ValidationExpression="\d{2}\/\d{2}\/\d{4}" SetFocusOnError="True" /></span>
                                </div>
                            </div>
                            <div class="form-group form-inline">
                                <label class="col-md-2 control-label" for="txtDataRetornoManutencao">Data de Retorno de Manutenção:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtDataRetornoManutencao" Width="154px" runat="server" placeholder="DD/MM/AAAA" CssClass="form-control"/><asp:CalendarExtender ID="calendarDataRetornoManutencao" runat="server" TargetControlID="txtDataRetornoManutencao" PopupButtonID="btnCalendarDataRetornoManutencao" Format="dd/MM/yyyy" />&nbsp;<asp:LinkButton ID="btnCalendarDataRetornoManutencao" runat="server" CssClass="glyphicon glyphicon-calendar form-control-feedback" CausesValidation="False"></asp:LinkButton><span class="help-block"><asp:RegularExpressionValidator ID="regularExpressionDataRetornoManutencao" runat="server" ErrorMessage="Formato invalido" ControlToValidate="txtDataRetornoManutencao" ValidationExpression="\d{2}\/\d{2}\/\d{4}" SetFocusOnError="True" /></span>
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
                            <legend>Gerenciar Recurso</legend>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="txtParan1">Código:</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtParanCodigo" runat="server" CssClass="form-control" MaxLength="10" type="search" results="20" placeholder=""/>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="txtParanDescricao">Descrição:</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtParanDescricao" runat="server" CssClass="form-control" MaxLength="100" type="search" results="20" placeholder="Informe a descrição"/>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnPesquisar" CssClass="btn btn-default" runat="server" Text="Pesquisar" onclick="btnPesquisar_Click"/>
                                </div>
                            </div>
                        </fieldset>
                        <asp:GridView ID="gridConsulta" runat="server" Width="100%" HeaderStyle-BackColor="#F2EEE5" AutoGenerateColumns="False" onrowcommand="gridConsulta_RowCommand" GridLines="None" AllowPaging="False" AllowSorting="True" CssClass="table table-hover">
                            <HeaderStyle BackColor="#F2EEE5" ForeColor="#4F4F4F" />
                            <Columns>
                                 <asp:BoundField DataField="Codigo" HeaderText="Código" HeaderStyle-Width="75px" />
                                 <asp:BoundField DataField="Descricao" HeaderText="Descrição" HeaderStyle-Width="400px" />
                                 <asp:BoundField DataField="Fabricante" HeaderText="Fabricante" />
                                 <asp:TemplateField HeaderText="Situação" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <%# (DataBinder.Eval(Container.DataItem, "Situacao").ToString() == "A") ? "Ativo" : "Inativo"%>
                                        </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="A&ccedil&otilde;es" HeaderStyle-Width="75px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnSelecionar" runat="server" ToolTip="Visualizar em Detalhes" CommandName="Selecionar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdRecurso")%>' ><i class="glyphicon glyphicon-folder-open"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnEditar" runat="server" ToolTip="Editar Registro" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdRecurso")%>' ><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnExcluir" runat="server" ToolTip="Remover Registro" CommandName="Excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdRecurso")%>' ><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
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
