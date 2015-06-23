<%@ Page Title="" Language="C#" MasterPageFile="~/Genrec.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Genrec.Login" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Extensao_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Extensao_Conteudo" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div id="content" class="container">
	    <div class="row">
            <div class="col-md-12">
                <asp:Panel ID="panelMensagem" runat="server">
                    <asp:Label ID="labelMensagemAlerta" runat="server" />
                    <asp:Timer ID="timerMensagemAlerta" runat="server" Interval="10000" Enabled="false" OnTick="TimerMensagem_Tick"/>
                </asp:Panel>
                <div class="ntid-box form-horizontal">
                    <fieldset>
                        <legend>Login</legend>
                        <div class="form-group">
                            <label class="col-md-2 control-label" for="txtMatricula">Matrícula:</label>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" MaxLength="10"/>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label" for="txtSenha">Senha:</label>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtSenha" TextMode="Password" runat="server" CssClass="form-control" MaxLength="40"/>
                            </div>                           
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 control-label" ></label>
                            <div class="col-md-2">
                                <asp:Button ID="btnEntrar" CssClass="btn btn-default" runat="server" Text="Entrar" onclick="btnEntrar_Click"/>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
            <br /><br /><br /><br /><br /><br /><br /><br />
	    </div>
    </div>
</asp:Content>

