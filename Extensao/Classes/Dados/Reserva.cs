using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;

namespace UFRGS.Genrec.Data{
    public class Reserva
    {
        private int idReserva;
        public int IdReserva
        {
            get { return this.idReserva; }
            set { this.idReserva = value; }
        }
        private string numero;
        public string Numero
        {
            get { return this.numero; }
            set { this.numero = value; }
        }
        private Funcionario funcionario;
        public Funcionario Funcionario
        {
            get { return this.funcionario; }
            set { this.funcionario = value; }
        }
        private DateTime data;
        public DateTime Data
        {
            get { return this.data; }
            set { this.data = value; }
        }
        private string observacao;
        public string Observacao
        {
            get { return this.observacao; }
            set { this.observacao = value; }
        }
        public override string ToString() { return this.idReserva.ToString(); }
        public Reserva() { }
        public Reserva(int idReserva, string numero, Funcionario funcionario, DateTime data, string observacao)
        {
            this.idReserva = idReserva;
            this.numero = numero;
            this.funcionario = funcionario;
            this.data = data;
            this.observacao = observacao;
        }
        public static int ID
        {
            get
            {
                try
                {
                    DataTable tabela = Server.Consulta("SELECT NVL(MAX(ID_RESERVA),0) ID FROM RESERVAS");
                    foreach (DataRow linha in tabela.Rows) return int.Parse(linha["ID"].ToString());
                }
                catch { }
                return 0;
            }
        }
        public static Reserva Carregar(int id)
        {
            Reserva obj = new Reserva();
            DataTable tabela = Server.Consulta("SELECT ID_RESERVA, NUMERO, ID_FUNCIONARIO, DATA, OBSERVACAO FROM RESERVAS WHERE ID_RESERVA = " + id.ToString());
            foreach (DataRow linha in tabela.Rows)
            {
                obj.IdReserva = (linha["ID_RESERVA"].ToString() != "") ? int.Parse(linha["ID_RESERVA"].ToString()) : 0;
                obj.Numero = linha["NUMERO"].ToString();
                obj.Funcionario = Funcionario.Carregar((linha["ID_FUNCIONARIO"].ToString() != "") ? int.Parse(linha["ID_FUNCIONARIO"].ToString()) : 0);
                obj.Data = (linha["DATA"].ToString() != "") ? DateTime.Parse(linha["DATA"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                obj.Observacao = linha["OBSERVACAO"].ToString();
            }
            return obj;
        }
        public static Reserva Carregar(int id, int nivel)
        {
            if (nivel < 1) return null;
            nivel--;
            Reserva obj = new Reserva();
            DataTable tabela = Server.Consulta("SELECT ID_RESERVA, NUMERO, ID_FUNCIONARIO, DATA, OBSERVACAO FROM RESERVAS WHERE ID_RESERVA = " + id.ToString());
            foreach (DataRow linha in tabela.Rows)
            {
                obj.IdReserva = (linha["ID_RESERVA"].ToString() != "") ? int.Parse(linha["ID_RESERVA"].ToString()) : 0;
                obj.Numero = linha["NUMERO"].ToString();
                obj.Funcionario = Funcionario.Carregar((linha["ID_FUNCIONARIO"].ToString() != "") ? int.Parse(linha["ID_FUNCIONARIO"].ToString()) : 0, nivel);
                obj.Data = (linha["DATA"].ToString() != "") ? DateTime.Parse(linha["DATA"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                obj.Observacao = linha["OBSERVACAO"].ToString();
            }
            return obj;
        }
        public static List<Reserva> Carregar(List<string> restricoes)
        {
            string restricao = "";
            foreach (string rest in restricoes) restricao += rest + " AND ";
            restricao += "1 = 1";
            List<Reserva> objtos = new List<Reserva>();
            DataTable tabela = Server.Consulta("SELECT ID_RESERVA, NUMERO, ID_FUNCIONARIO, DATA, OBSERVACAO FROM RESERVAS WHERE " + restricao);
            foreach (DataRow linha in tabela.Rows)
            {
                Reserva obj = new Reserva();
                obj.IdReserva = (linha["ID_RESERVA"].ToString() != "") ? int.Parse(linha["ID_RESERVA"].ToString()) : 0;
                obj.Numero = linha["NUMERO"].ToString();
                obj.Funcionario = Funcionario.Carregar((linha["ID_FUNCIONARIO"].ToString() != "") ? int.Parse(linha["ID_FUNCIONARIO"].ToString()) : 0);
                obj.Data = (linha["DATA"].ToString() != "") ? DateTime.Parse(linha["DATA"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                obj.Observacao = linha["OBSERVACAO"].ToString();
                objtos.Add(obj);
            }
            return objtos;
        }
        public static List<Reserva> Carregar(List<string> restricoes, int nlMinimo, int nlMaximo)
        {
            string restricao = "";
            foreach (string rest in restricoes) restricao += rest + " AND ";
            restricao += "1 = 1";
            List<Reserva> objtos = new List<Reserva>();
            DataTable tabela = Server.Consulta("SELECT * FROM (SELECT ID_RESERVA, NUMERO, ID_FUNCIONARIO, DATA, OBSERVACAO, ROWNUM NLINHA FROM (SELECT * FROM RESERVAS ORDER BY DATA DESC) WHERE " + restricao + ") WHERE NLINHA BETWEEN " + nlMinimo + " AND " + nlMaximo);
            foreach (DataRow linha in tabela.Rows)
            {
                Reserva obj = new Reserva();
                obj.IdReserva = (linha["ID_RESERVA"].ToString() != "") ? int.Parse(linha["ID_RESERVA"].ToString()) : 0;
                obj.Numero = linha["NUMERO"].ToString();
                obj.Funcionario = Funcionario.Carregar((linha["ID_FUNCIONARIO"].ToString() != "") ? int.Parse(linha["ID_FUNCIONARIO"].ToString()) : 0);
                obj.Data = (linha["DATA"].ToString() != "") ? DateTime.Parse(linha["DATA"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                obj.Observacao = linha["OBSERVACAO"].ToString();
                objtos.Add(obj);
            }
            return objtos;
        }
        public static List<Reserva> Carregar(List<string> restricoes, int nivel)
        {
            if (nivel < 1) return null;
            nivel--;
            string restricao = "";
            foreach (string rest in restricoes) restricao += rest + " AND ";
            restricao += "1 = 1";
            List<Reserva> objtos = new List<Reserva>();
            DataTable tabela = Server.Consulta("SELECT ID_RESERVA, NUMERO, ID_FUNCIONARIO, DATA, OBSERVACAO FROM RESERVAS WHERE " + restricao);
            foreach (DataRow linha in tabela.Rows)
            {
                Reserva obj = new Reserva();
                obj.IdReserva = (linha["ID_RESERVA"].ToString() != "") ? int.Parse(linha["ID_RESERVA"].ToString()) : 0;
                obj.Numero = linha["NUMERO"].ToString();
                obj.Funcionario = Funcionario.Carregar((linha["ID_FUNCIONARIO"].ToString() != "") ? int.Parse(linha["ID_FUNCIONARIO"].ToString()) : 0, nivel);
                obj.Data = (linha["DATA"].ToString() != "") ? DateTime.Parse(linha["DATA"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                obj.Observacao = linha["OBSERVACAO"].ToString();
                objtos.Add(obj);
            }
            return objtos;
        }
        public static List<Reserva> Carregar()
        {
            List<Reserva> objtos = new List<Reserva>();
            DataTable tabela = Server.Consulta("SELECT ID_RESERVA, NUMERO, ID_FUNCIONARIO, DATA, OBSERVACAO FROM RESERVAS");
            foreach (DataRow linha in tabela.Rows)
            {
                Reserva obj = new Reserva();
                obj.IdReserva = (linha["ID_RESERVA"].ToString() != "") ? int.Parse(linha["ID_RESERVA"].ToString()) : 0;
                obj.Numero = linha["NUMERO"].ToString();
                obj.Funcionario = Funcionario.Carregar((linha["ID_FUNCIONARIO"].ToString() != "") ? int.Parse(linha["ID_FUNCIONARIO"].ToString()) : 0);
                obj.Data = (linha["DATA"].ToString() != "") ? DateTime.Parse(linha["DATA"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                obj.Observacao = linha["OBSERVACAO"].ToString();
                objtos.Add(obj);
            }
            return objtos;
        }
        public string Atualizar()
        {
            string ret = "";
            if (this.idReserva != 0)
            {
                ret = Server.Executa(@"UPDATE RESERVAS SET 
                      ID_RESERVA = " + this.idReserva.ToString() + @",
                      NUMERO = '" + this.numero + @"' ,
                      ID_FUNCIONARIO = " + this.funcionario.ToString() + @",
                      DATA = TO_DATE('" + GetDataValida(this.data.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ,
                      OBSERVACAO = '" + this.observacao + @"' 
                      WHERE ID_RESERVA = " + this.idReserva.ToString());
            }
            return ret;
        }
        public string Inserir()
        {
            string ret = Server.Executa(@"INSERT INTO RESERVAS(ID_RESERVA, NUMERO, ID_FUNCIONARIO, DATA, OBSERVACAO)
                      VALUES(" + this.idReserva.ToString() + @",'" + this.numero + @"' ," + this.funcionario.ToString() + @",TO_DATE('" + GetDataValida(this.data.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ,'" + this.observacao + @"' " + ")");
            return ret;
        }
        public string Excluir()
        {
            string ret = "";
            if (this.idReserva != 0)
            {
                ret = Server.Executa(@"DELETE FROM RESERVAS WHERE ID_RESERVA = " + this.idReserva.ToString());
            }
            return ret;
        }
        private string GetDataValida(string data)
        {
            return ((data == "01/01/0001 00:00:00") ? "" : data);
        }
    }
}
