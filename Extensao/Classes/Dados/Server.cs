using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;

namespace UFRGS.Genrec.Data
{
    public static class Server
    {
        private static string host;

        public static string Host
        {
            get { return Server.host; }
            set { Server.host = value; }
        }
        private static string usuario;

        public static string Usuario
        {
            get { return Server.usuario; }
            set { Server.usuario = value; }
        }
        private static string senha;

        public static string Senha
        {
            get { return Server.senha; }
            set { Server.senha = value; }
        }
        private static string sid;

        public static string Sid
        {
            get { return Server.sid; }
            set { Server.sid = value; }
        }

        private static OracleConnection conn;

        public static string Conectar()
        {
            if (!string.IsNullOrEmpty(host))
            {
                conn = new OracleConnection("DATA SOURCE=" + host + "/" + sid + ";PASSWORD=" + senha + ";USER ID=" + usuario);
            }
            else
            {
                conn = new OracleConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["conexaoDados"].ConnectionString);
            }
            try
            {
                if(conn.State != ConnectionState.Open) 
				{ 
					conn.Open(); 
					ConfigurarSession(); 
				}
            }
            catch (OracleException e)
            {
                return e.Message;
            }
            return "";
        }
        public static string Desconectar()
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (OracleException e)
            {
                return e.Message;
            }
            return "";
        }

        public static string Executa(string sql)
        {
            try
            {
                if ((conn == null)||(conn.State != ConnectionState.Open)) Conectar();
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
            catch (OracleException oe)
            {
                return oe.Message;
            }
            return "";
        }
        public static DataTable Consulta(string sql)
        {
            DataTable table = new DataTable();
            try
            {
                if ((conn == null) || (conn.State != ConnectionState.Open)) Conectar();
                OracleDataAdapter ad = new OracleDataAdapter(sql, conn);
                ad.Fill(table);
            }
            catch (OracleException oe)
            {
                string error = oe.Message;
            }
            return table;
        }
		private static void ConfigurarSession()
        {
            try
            {
                if ((conn == null)||(conn.State != ConnectionState.Open)) Conectar();
                OracleCommand cmd = new OracleCommand("ALTER SESSION SET NLS_DATE_FORMAT='DD/MM/YYYY HH24:MI:SS'", conn); cmd.ExecuteNonQuery(); //Configura formato padrão da data
                cmd = new OracleCommand("alter session set nls_comp=linguistic", conn); cmd.ExecuteNonQuery();  //Enibe caracteres especias no like como cedilha e acentos
                cmd = new OracleCommand("alter session set nls_sort=binary_ai", conn); cmd.ExecuteNonQuery();   //Configura o modo de classificação ORDER BY padrão
                //cmd = new OracleCommand("ALTER SESSION SET NLS_NUMERIC_CHARACTERS= '.,'", conn); cmd.ExecuteNonQuery();  //Configura o formato de pontuação de numeros
				//cmd = new OracleCommand("ALTER SESSION SET NLS_CURRENCY = 'R$'", conn); cmd.ExecuteNonQuery();  //Define o simbolo monetario  Exemplo: SELECT TO_CHAR( 4963.44, 'L999G999D99') Valor FROM dual;  Saida: R$4.963,44
				
            }
            catch
            {
            }
        }
    }
}