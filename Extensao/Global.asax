<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        UFRGS.Genrec.Data.Server.Conectar();

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        UFRGS.Genrec.Data.Server.Desconectar();
    }
        
    void Application_Error(object sender, EventArgs e) 
    {

    }

    void Session_Start(object sender, EventArgs e) 
    {

    }

    void Session_End(object sender, EventArgs e) 
    {

    }
       
</script>
