<%@ Page Title="" Language="C#" MasterPageFile="~/GenrecAdmin.master" AutoEventWireup="true" CodeBehind="FrmCadastroFuncionario.aspx.cs" Inherits="Genrec.FrmCadastroFuncionario" %>
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
                <h1>Funcionário/Usuário</h1>
                <div class="ntid-box">
                    <asp:HiddenField ID="idRegistro" runat="server" Value="0"/>
                    <asp:Panel ID="panelDetalhes" runat="server"  Visible="false" CssClass="form-horizontal" role="form">
                        <fieldset>
                            <legend class="sem-borda">Detalhes do Funcionário</legend>
                            <asp:DetailsView ID="viewDetalhes" runat="server"  CssClass="table" GridLines="None" AutoGenerateRows="false">
                                <RowStyle  ForeColor="#333333" BorderColor="#e9e9e9" />
                                <FieldHeaderStyle Width="150px" Font-Bold="True" HorizontalAlign="Right" />
                                <AlternatingRowStyle BackColor="White" />
                                <Fields>
                                    <asp:BoundField DataField="Matricula" HeaderText="Matrícula:" />
                                    <asp:BoundField DataField="Nome" HeaderText="Nome:" />
                                    <asp:BoundField DataField="Cpf" HeaderText="CPF:" />
                                    <asp:BoundField DataField="DataNascimento" HeaderText="Data de Nascimento:" />
                                    <asp:BoundField DataField="Email" HeaderText="E-mail:" />
                                    <asp:BoundField DataField="Senha" HeaderText="Senha:" />
                                    <asp:TemplateField HeaderText="Cargo:">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "Cargo.Nome")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data de Admissao:">
                                        <ItemTemplate>
                                            <%# ((DateTime)DataBinder.Eval(Container.DataItem, "DataAdmissao")).ToString("dd/MM/yyyy")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data de Demissao:">
                                        <ItemTemplate>
                                            <%# ((DateTime)DataBinder.Eval(Container.DataItem, "DataDemissao")).ToString("dd/MM/yyyy")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Nivel" HeaderText="Nível:" />
                                    <asp:TemplateField HeaderText="Prioridade:">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "Prioridade")%> - Modificado em <%# ((DateTime)DataBinder.Eval(Container.DataItem, "DataModificacaoPrioridade")).ToString("dd/MM/yyyy HH:mm:ss")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="IndiceDePenalidade" HeaderText="Indice De Penalidade:" />
                                </Fields>
                            </asp:DetailsView>
                        </fieldset>
                        <asp:Button ID="btnEditarView" CssClass="btn btn-primary" runat="server" Text="Editar" onclick="btnEditar_Click" CausesValidation="False"/>
                        <asp:Button ID="btnCancelarView" CssClass="btn btn-primary" runat="server" Text="Cancelar/Lista" onclick="btnCancelar_Click" CausesValidation="False"/>
                    </asp:Panel>
                    <asp:Panel ID="panelFormulario" runat="server"  Visible="false" CssClass="form-horizontal" role="form">
                        <fieldset>
                            <legend>Cadastro de Funciánario/Usuário</legend>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="txtMatricula">Matricula:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtMatricula" placeholder="000000" runat="server" CssClass="form-control" Width="154px" MaxLength="10"/><asp:RequiredFieldValidator ID="requeriedFieldMatricula" runat="server" ErrorMessage="Campo Obrigatório" ControlToValidate="txtMatricula" SetFocusOnError="True" ToolTip="Campo Obrigatório" CssClass="text-warning" ><i class="glyphicon  glyphicon-info-sign"></i> Campo Obrigatório</asp:RequiredFieldValidator><span class="help-block"></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="txtNome">Nome:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtNome" placeholder="Informe o Nome" runat="server" CssClass="form-control" Width="560px" MaxLength="80"/><asp:RequiredFieldValidator ID="requeriedFieldNome" runat="server" ErrorMessage="Campo Obrigatório" ControlToValidate="txtNome" SetFocusOnError="True" ToolTip="Campo Obrigatório" CssClass="text-warning" ><i class="glyphicon  glyphicon-info-sign"></i> Campo Obrigatório</asp:RequiredFieldValidator><span class="help-block"></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="txtCpf">CPF:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtCpf" placeholder="000.000.000-00" runat="server" CssClass="form-control" Width="154px" MaxLength="14"/><asp:RequiredFieldValidator ID="requeriedFieldCpf" runat="server" ErrorMessage="Campo Obrigatório" ControlToValidate="txtCpf" SetFocusOnError="True" ToolTip="Campo Obrigatório" CssClass="text-warning" ><i class="glyphicon  glyphicon-info-sign"></i> Campo Obrigatório</asp:RequiredFieldValidator><span class="help-block"></span>
                                </div>
                            </div>
                            <div class="form-group form-inline">
                                <label class="col-md-2 control-label" for="txtDataNascimento">Data de Nascimento:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtDataNascimento" Width="154px" runat="server" placeholder="DD/MM/AAAA" CssClass="form-control"/><asp:CalendarExtender ID="calendarDataNascimento" runat="server" TargetControlID="txtDataNascimento" PopupButtonID="btnCalendarDataNascimento" Format="dd/MM/yyyy" />&nbsp;<asp:LinkButton ID="btnCalendarDataNascimento" runat="server" CssClass="glyphicon glyphicon-calendar form-control-feedback" CausesValidation="False"></asp:LinkButton><asp:RequiredFieldValidator ID="requeriedFieldDataNascimento" runat="server" ErrorMessage="Campo Obrigatório" ControlToValidate="txtDataNascimento" SetFocusOnError="True" ToolTip="Campo Obrigatório" CssClass="text-warning" ><i class="glyphicon  glyphicon-info-sign"></i> Campo Obrigatório</asp:RequiredFieldValidator><span class="help-block"><asp:RegularExpressionValidator ID="regularExpressionDataNascimento" runat="server" ErrorMessage="Formato invalido" ControlToValidate="txtDataNascimento" ValidationExpression="\d{2}\/\d{2}\/\d{4}" SetFocusOnError="True" /></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label " for="txtEmail">E-mail:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtEmail" placeholder="Informe o Email" runat="server" CssClass="form-control" Width="560px" MaxLength="80"/><asp:RequiredFieldValidator ID="requeriedFieldEmail" runat="server" ErrorMessage="Campo Obrigatório" ControlToValidate="txtEmail" SetFocusOnError="True" ToolTip="Campo Obrigatório" CssClass="text-warning" ><i class="glyphicon  glyphicon-info-sign"></i> Campo Obrigatório</asp:RequiredFieldValidator><span class="help-block"><asp:RegularExpressionValidator ID="regularExpressionEmail" runat="server" ErrorMessage="Formato invalido" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" SetFocusOnError="True" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="txtSenha">Senha:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtSenha" placeholder="Informe o Senha" TextMode="Password" runat="server" CssClass="form-control" Width="280px" MaxLength="40"/><span class="help-block"></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="dropListCargo">Cargo:</label>
                                <div class="col-md-10">
                                    <asp:DropDownList ID="dropListCargo"  runat="server" CssClass="form-control" DataTextField="Nome" Width="560px"  DataValueField="IdCargo" />
                                </div>
                            </div>
                            <div class="form-group form-inline">
                                <label class="col-md-2 control-label" for="txtDataAdmissao">Data de Admissao:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtDataAdmissao" Width="154px" runat="server" placeholder="DD/MM/AAAA" CssClass="form-control"/><asp:CalendarExtender ID="calendarDataAdmissao" runat="server" TargetControlID="txtDataAdmissao" PopupButtonID="btnCalendarDataAdmissao" Format="dd/MM/yyyy" />&nbsp;<asp:LinkButton ID="btnCalendarDataAdmissao" runat="server" CssClass="glyphicon glyphicon-calendar form-control-feedback" CausesValidation="False"></asp:LinkButton><asp:RequiredFieldValidator ID="requeriedFieldDataAdmissao" runat="server" ErrorMessage="Campo Obrigatório" ControlToValidate="txtDataAdmissao" SetFocusOnError="True" ToolTip="Campo Obrigatório" CssClass="text-warning" ><i class="glyphicon  glyphicon-info-sign"></i> Campo Obrigatório</asp:RequiredFieldValidator><span class="help-block"><asp:RegularExpressionValidator ID="regularExpressionDataAdmissao" runat="server" ErrorMessage="Formato invalido" ControlToValidate="txtDataAdmissao" ValidationExpression="\d{2}\/\d{2}\/\d{4}" SetFocusOnError="True" /></span>
                                </div>
                            </div>
                            <div class="form-group form-inline">
                                <label class="col-md-2 control-label" for="txtDataDemissao">Data de Demissao:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtDataDemissao" Width="154px" runat="server" placeholder="DD/MM/AAAA" CssClass="form-control"/><asp:CalendarExtender ID="calendarDataDemissao" runat="server" TargetControlID="txtDataDemissao" PopupButtonID="btnCalendarDataDemissao" Format="dd/MM/yyyy" />&nbsp;<asp:LinkButton ID="btnCalendarDataDemissao" runat="server" CssClass="glyphicon glyphicon-calendar form-control-feedback" CausesValidation="False"></asp:LinkButton><span class="help-block"><asp:RegularExpressionValidator ID="regularExpressionDataDemissao" runat="server" ErrorMessage="Formato invalido" ControlToValidate="txtDataDemissao" ValidationExpression="\d{2}\/\d{2}\/\d{4}" SetFocusOnError="True" /></span>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="dropListNivel">Nível:</label>
                                <div class="col-md-10">
                                    <asp:DropDownList ID="dropListNivel"  runat="server" CssClass="form-control" Width="154px">
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="Administrador" Value="1" />
                                        <asp:ListItem Text="Usuário" Value="2" />
                                        <asp:ListItem Text="Almoxarifado" Value="3" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="requeriedFieldNivel" runat="server" ErrorMessage="Campo Obrigatório" ControlToValidate="dropListNivel" SetFocusOnError="True" ToolTip="Campo Obrigatório" CssClass="text-warning" ><i class="glyphicon  glyphicon-info-sign"></i> Campo Obrigatório</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="dropListPrioridade">Prioridade:</label>
                                <div class="col-md-10">
                                   <asp:DropDownList ID="dropListPrioridade"  runat="server" CssClass="form-control" Width="154px">
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="1" Value="1" />
                                        <asp:ListItem Text="2" Value="2" />
                                        <asp:ListItem Text="3" Value="3" />
                                        <asp:ListItem Text="4" Value="4" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="requeriedFieldPrioridade" runat="server" ErrorMessage="Campo Obrigatório" ControlToValidate="dropListPrioridade" SetFocusOnError="True" ToolTip="Campo Obrigatório" CssClass="text-warning" ><i class="glyphicon  glyphicon-info-sign"></i> Campo Obrigatório</asp:RequiredFieldValidator>
                                
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="txtIndiceDePenalidade">Indice De Penálidade:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtIndiceDePenalidade" Width="154px" placeholder="0" runat="server" MaxLength="1" CssClass="form-control"/><asp:RequiredFieldValidator ID="requeriedFieldIndiceDePenalidade" runat="server" ErrorMessage="Campo Obrigatório" ControlToValidate="txtIndiceDePenalidade" SetFocusOnError="True" ToolTip="Campo Obrigatório" CssClass="text-warning" ><i class="glyphicon  glyphicon-info-sign"></i> Campo Obrigatório</asp:RequiredFieldValidator><span class="help-block"><asp:RegularExpressionValidator ID="regularExpressionIndiceDePenalidade" runat="server" ErrorMessage="Formato invalido" ControlToValidate="txtIndiceDePenalidade" ValidationExpression="\d+" SetFocusOnError="True" /></span>
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
                            <legend>Gerenciar Funcionário</legend>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="txtParanMatricula">Matricula:</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtParanMatricula" runat="server" CssClass="form-control" MaxLength="10" type="search" results="20" placeholder="0000000"/>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label" for="txtParanNome">Nome:</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtParanNome" runat="server" CssClass="form-control" MaxLength="100" type="search" results="20" placeholder="Informe o nome do funcionário"/>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnPesquisar" CssClass="btn btn-default" runat="server" Text="Pesquisar" onclick="btnPesquisar_Click"/>
                                </div>
                            </div>
                        </fieldset>
                        <asp:GridView ID="gridConsulta" runat="server" Width="100%" HeaderStyle-BackColor="#F2EEE5" AutoGenerateColumns="False" onrowcommand="gridConsulta_RowCommand" GridLines="None" AllowPaging="False" AllowSorting="True" CssClass="table table-hover">
                            <HeaderStyle BackColor="#F2EEE5" ForeColor="#4F4F4F" />
                            <Columns>
                                 <asp:BoundField DataField="Matricula" HeaderText="Matricula" />
                                 <asp:BoundField DataField="Nome" HeaderText="Nome" />
                                 <asp:BoundField DataField="Cpf" HeaderText="CPF" />
                                 <asp:BoundField DataField="Email" HeaderText="E-mail" />
                                 <asp:TemplateField HeaderText="Cargo">
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container.DataItem, "Cargo.Nome")%>
                                        </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="A&ccedil&otilde;es" HeaderStyle-Width="75px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnSelecionar" runat="server" ToolTip="Visualizar em Detalhes" CommandName="Selecionar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdFuncionario")%>' ><i class="glyphicon glyphicon-folder-open"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnEditar" runat="server" ToolTip="Editar Registro" CommandName="Editar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdFuncionario")%>' ><i class="glyphicon glyphicon-edit"></i></asp:LinkButton>
                                        <asp:LinkButton ID="btnExcluir" runat="server" ToolTip="Remover Registro" CommandName="Excluir" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "IdFuncionario")%>' ><i class="glyphicon glyphicon-trash"></i></asp:LinkButton>
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
