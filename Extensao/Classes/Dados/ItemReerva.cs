using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;

namespace UFRGS.Genrec.Data{
    public class ItemReerva
    {
        private int idItemReserva;
        public int IdItemReserva
        {
            get { return this.idItemReserva; }
            set { this.idItemReserva = value; }
        }
        private Reserva reserva;
        public Reserva Reserva
        {
            get { return this.reserva; }
            set { this.reserva = value; }
        }
        private Recurso recurso;
        public Recurso Recurso
        {
            get { return this.recurso; }
            set { this.recurso = value; }
        }
        private string situacao;
        public string Situacao
        {
            get { return this.situacao; }
            set { this.situacao = value; }
        }
        private DateTime dataCancelamento;
        public DateTime DataCancelamento
        {
            get { return this.dataCancelamento; }
            set { this.dataCancelamento = value; }
        }
        private string observacao;
        public string Observacao
        {
            get { return this.observacao; }
            set { this.observacao = value; }
        }
        private DateTime dataRetirada;
        public DateTime DataRetirada
        {
            get { return this.dataRetirada; }
            set { this.dataRetirada = value; }
        }
        private DateTime dataDevolucao;
        public DateTime DataDevolucao
        {
            get { return this.dataDevolucao; }
            set { this.dataDevolucao = value; }
        }
        public override string ToString() { return this.idItemReserva.ToString(); }
        public ItemReerva() { }
        public ItemReerva(int idItemReserva, Reserva reserva, Recurso recurso, string situacao, DateTime dataCancelamento, string observacao, DateTime dataRetirada, DateTime dataDevolucao)
        {
            this.idItemReserva = idItemReserva;
            this.reserva = reserva;
            this.recurso = recurso;
            this.situacao = situacao;
            this.dataCancelamento = dataCancelamento;
            this.observacao = observacao;
            this.dataRetirada = dataRetirada;
            this.dataDevolucao = dataDevolucao;
        }
        public static int ID
        {
            get
            {
                try
                {
                    DataTable tabela = Server.Consulta("SELECT NVL(MAX(ID_ITEM_RESERVA),0) ID FROM ITENS_RESERVA");
                    foreach (DataRow linha in tabela.Rows) return int.Parse(linha["ID"].ToString());
                }
                catch { }
                return 0;
            }
        }
        public static ItemReerva Carregar(int id)
        {
            ItemReerva obj = new ItemReerva();
            DataTable tabela = Server.Consulta("SELECT ID_ITEM_RESERVA, ID_RESERVA, ID_RECURSO, SITUACAO, DATA_CANCELAMENTO, OBSERVACAO, DATA_RETIRADA, DATA_DEVOLUCAO FROM ITENS_RESERVA WHERE ID_ITEM_RESERVA = " + id.ToString());
            foreach (DataRow linha in tabela.Rows)
            {
                obj.IdItemReserva = (linha["ID_ITEM_RESERVA"].ToString() != "") ? int.Parse(linha["ID_ITEM_RESERVA"].ToString()) : 0;
                obj.Reserva = Reserva.Carregar((linha["ID_RESERVA"].ToString() != "") ? int.Parse(linha["ID_RESERVA"].ToString()) : 0);
                obj.Recurso = Recurso.Carregar((linha["ID_RECURSO"].ToString() != "") ? int.Parse(linha["ID_RECURSO"].ToString()) : 0);
                obj.Situacao = linha["SITUACAO"].ToString();
                obj.DataCancelamento = (linha["DATA_CANCELAMENTO"].ToString() != "") ? DateTime.Parse(linha["DATA_CANCELAMENTO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                obj.Observacao = linha["OBSERVACAO"].ToString();
                obj.DataRetirada = (linha["DATA_RETIRADA"].ToString() != "") ? DateTime.Parse(linha["DATA_RETIRADA"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                obj.DataDevolucao = (linha["DATA_DEVOLUCAO"].ToString() != "") ? DateTime.Parse(linha["DATA_DEVOLUCAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
            }
            return obj;
        }
        public static ItemReerva Carregar(int id, int nivel)
        {
            if (nivel < 1) return null;
            nivel--;
            ItemReerva obj = new ItemReerva();
            DataTable tabela = Server.Consulta("SELECT ID_ITEM_RESERVA, ID_RESERVA, ID_RECURSO, SITUACAO, DATA_CANCELAMENTO, OBSERVACAO, DATA_RETIRADA, DATA_DEVOLUCAO FROM ITENS_RESERVA WHERE ID_ITEM_RESERVA = " + id.ToString());
            foreach (DataRow linha in tabela.Rows)
            {
                obj.IdItemReserva = (linha["ID_ITEM_RESERVA"].ToString() != "") ? int.Parse(linha["ID_ITEM_RESERVA"].ToString()) : 0;
                obj.Reserva = Reserva.Carregar((linha["ID_RESERVA"].ToString() != "") ? int.Parse(linha["ID_RESERVA"].ToString()) : 0, nivel);
                obj.Recurso = Recurso.Carregar((linha["ID_RECURSO"].ToString() != "") ? int.Parse(linha["ID_RECURSO"].ToString()) : 0, nivel);
                obj.Situacao = linha["SITUACAO"].ToString();
                obj.DataCancelamento = (linha["DATA_CANCELAMENTO"].ToString() != "") ? DateTime.Parse(linha["DATA_CANCELAMENTO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                obj.Observacao = linha["OBSERVACAO"].ToString();
                obj.DataRetirada = (linha["DATA_RETIRADA"].ToString() != "") ? DateTime.Parse(linha["DATA_RETIRADA"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                obj.DataDevolucao = (linha["DATA_DEVOLUCAO"].ToString() != "") ? DateTime.Parse(linha["DATA_DEVOLUCAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
            }
            return obj;
        }
        public static List<ItemReerva> Carregar(List<string> restricoes)
        {
            string restricao = "";
            foreach (string rest in restricoes) restricao += rest + " AND ";
            restricao += "1 = 1";
            List<ItemReerva> objtos = new List<ItemReerva>();
            DataTable tabela = Server.Consulta("SELECT ID_ITEM_RESERVA, ID_RESERVA, ID_RECURSO, SITUACAO, DATA_CANCELAMENTO, OBSERVACAO, DATA_RETIRADA, DATA_DEVOLUCAO FROM ITENS_RESERVA WHERE " + restricao);
            foreach (DataRow linha in tabela.Rows)
            {
                ItemReerva obj = new ItemReerva();
                obj.IdItemReserva = (linha["ID_ITEM_RESERVA"].ToString() != "") ? int.Parse(linha["ID_ITEM_RESERVA"].ToString()) : 0;
                obj.Reserva = Reserva.Carregar((linha["ID_RESERVA"].ToString() != "") ? int.Parse(linha["ID_RESERVA"].ToString()) : 0);
                obj.Recurso = Recurso.Carregar((linha["ID_RECURSO"].ToString() != "") ? int.Parse(linha["ID_RECURSO"].ToString()) : 0);
                obj.Situacao = linha["SITUACAO"].ToString();
                obj.DataCancelamento = (linha["DATA_CANCELAMENTO"].ToString() != "") ? DateTime.Parse(linha["DATA_CANCELAMENTO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                obj.Observacao = linha["OBSERVACAO"].ToString();
                obj.DataRetirada = (linha["DATA_RETIRADA"].ToString() != "") ? DateTime.Parse(linha["DATA_RETIRADA"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                obj.DataDevolucao = (linha["DATA_DEVOLUCAO"].ToString() != "") ? DateTime.Parse(linha["DATA_DEVOLUCAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                objtos.Add(obj);
            }
            return objtos;
        }
        public static List<ItemReerva> Carregar(List<string> restricoes, int nlMinimo, int nlMaximo)
        {
            string restricao = "";
            foreach (string rest in restricoes) restricao += rest + " AND ";
            restricao += "1 = 1";
            List<ItemReerva> objtos = new List<ItemReerva>();
            DataTable tabela = Server.Consulta("SELECT * FROM (SELECT ID_ITEM_RESERVA, ID_RESERVA, ID_RECURSO, SITUACAO, DATA_CANCELAMENTO, OBSERVACAO, DATA_RETIRADA, DATA_DEVOLUCAO, ROWNUM NLINHA FROM ITENS_RESERVA WHERE " + restricao + ") WHERE NLINHA BETWEEN " + nlMinimo + " AND " + nlMaximo);
            foreach (DataRow linha in tabela.Rows)
            {
                ItemReerva obj = new ItemReerva();
                obj.IdItemReserva = (linha["ID_ITEM_RESERVA"].ToString() != "") ? int.Parse(linha["ID_ITEM_RESERVA"].ToString()) : 0;
                obj.Reserva = Reserva.Carregar((linha["ID_RESERVA"].ToString() != "") ? int.Parse(linha["ID_RESERVA"].ToString()) : 0);
                obj.Recurso = Recurso.Carregar((linha["ID_RECURSO"].ToString() != "") ? int.Parse(linha["ID_RECURSO"].ToString()) : 0);
                obj.Situacao = linha["SITUACAO"].ToString();
                obj.DataCancelamento = (linha["DATA_CANCELAMENTO"].ToString() != "") ? DateTime.Parse(linha["DATA_CANCELAMENTO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                obj.Observacao = linha["OBSERVACAO"].ToString();
                obj.DataRetirada = (linha["DATA_RETIRADA"].ToString() != "") ? DateTime.Parse(linha["DATA_RETIRADA"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                obj.DataDevolucao = (linha["DATA_DEVOLUCAO"].ToString() != "") ? DateTime.Parse(linha["DATA_DEVOLUCAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                objtos.Add(obj);
            }
            return objtos;
        }
        public static List<ItemReerva> Carregar(List<string> restricoes, int nivel)
        {
            if (nivel < 1) return null;
            nivel--;
            string restricao = "";
            foreach (string rest in restricoes) restricao += rest + " AND ";
            restricao += "1 = 1";
            List<ItemReerva> objtos = new List<ItemReerva>();
            DataTable tabela = Server.Consulta("SELECT ID_ITEM_RESERVA, ID_RESERVA, ID_RECURSO, SITUACAO, DATA_CANCELAMENTO, OBSERVACAO, DATA_RETIRADA, DATA_DEVOLUCAO FROM ITENS_RESERVA WHERE " + restricao);
            foreach (DataRow linha in tabela.Rows)
            {
                ItemReerva obj = new ItemReerva();
                obj.IdItemReserva = (linha["ID_ITEM_RESERVA"].ToString() != "") ? int.Parse(linha["ID_ITEM_RESERVA"].ToString()) : 0;
                obj.Reserva = Reserva.Carregar((linha["ID_RESERVA"].ToString() != "") ? int.Parse(linha["ID_RESERVA"].ToString()) : 0, nivel);
                obj.Recurso = Recurso.Carregar((linha["ID_RECURSO"].ToString() != "") ? int.Parse(linha["ID_RECURSO"].ToString()) : 0, nivel);
                obj.Situacao = linha["SITUACAO"].ToString();
                obj.DataCancelamento = (linha["DATA_CANCELAMENTO"].ToString() != "") ? DateTime.Parse(linha["DATA_CANCELAMENTO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                obj.Observacao = linha["OBSERVACAO"].ToString();
                obj.DataRetirada = (linha["DATA_RETIRADA"].ToString() != "") ? DateTime.Parse(linha["DATA_RETIRADA"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                obj.DataDevolucao = (linha["DATA_DEVOLUCAO"].ToString() != "") ? DateTime.Parse(linha["DATA_DEVOLUCAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                objtos.Add(obj);
            }
            return objtos;
        }
        public static List<ItemReerva> Carregar()
        {
            List<ItemReerva> objtos = new List<ItemReerva>();
            DataTable tabela = Server.Consulta("SELECT ID_ITEM_RESERVA, ID_RESERVA, ID_RECURSO, SITUACAO, DATA_CANCELAMENTO, OBSERVACAO, DATA_RETIRADA, DATA_DEVOLUCAO FROM ITENS_RESERVA");
            foreach (DataRow linha in tabela.Rows)
            {
                ItemReerva obj = new ItemReerva();
                obj.IdItemReserva = (linha["ID_ITEM_RESERVA"].ToString() != "") ? int.Parse(linha["ID_ITEM_RESERVA"].ToString()) : 0;
                obj.Reserva = Reserva.Carregar((linha["ID_RESERVA"].ToString() != "") ? int.Parse(linha["ID_RESERVA"].ToString()) : 0);
                obj.Recurso = Recurso.Carregar((linha["ID_RECURSO"].ToString() != "") ? int.Parse(linha["ID_RECURSO"].ToString()) : 0);
                obj.Situacao = linha["SITUACAO"].ToString();
                obj.DataCancelamento = (linha["DATA_CANCELAMENTO"].ToString() != "") ? DateTime.Parse(linha["DATA_CANCELAMENTO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                obj.Observacao = linha["OBSERVACAO"].ToString();
                obj.DataRetirada = (linha["DATA_RETIRADA"].ToString() != "") ? DateTime.Parse(linha["DATA_RETIRADA"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                obj.DataDevolucao = (linha["DATA_DEVOLUCAO"].ToString() != "") ? DateTime.Parse(linha["DATA_DEVOLUCAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
                objtos.Add(obj);
            }
            return objtos;
        }
        public string Atualizar()
        {
            string ret = "";
            if (this.idItemReserva != 0)
            {
                ret = Server.Executa(@"UPDATE ITENS_RESERVA SET 
                      ID_ITEM_RESERVA = " + this.idItemReserva.ToString() + @",
                      ID_RESERVA = " + this.reserva.ToString() + @",
                      ID_RECURSO = " + this.recurso.ToString() + @",
                      SITUACAO = '" + this.situacao + @"' ,
                      DATA_CANCELAMENTO = TO_DATE('" + GetDataValida(this.dataCancelamento.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ,
                      OBSERVACAO = '" + this.observacao + @"' ,
                      DATA_RETIRADA = TO_DATE('" + GetDataValida(this.dataRetirada.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ,
                      DATA_DEVOLUCAO = TO_DATE('" + GetDataValida(this.dataDevolucao.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') 
                      WHERE ID_ITEM_RESERVA = " + this.idItemReserva.ToString());
            }
            return ret;
        }
        public string Inserir()
        {
            string ret = Server.Executa(@"INSERT INTO ITENS_RESERVA(ID_ITEM_RESERVA, ID_RESERVA, ID_RECURSO, SITUACAO, DATA_CANCELAMENTO, OBSERVACAO, DATA_RETIRADA, DATA_DEVOLUCAO)
                      VALUES(" + this.idItemReserva.ToString() + @"," + this.reserva.ToString() + @"," + this.recurso.ToString() + @",'" + this.situacao + @"' ,TO_DATE('" + GetDataValida(this.dataCancelamento.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ,'" + this.observacao + @"' ,TO_DATE('" + GetDataValida(this.dataRetirada.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ,TO_DATE('" + GetDataValida(this.dataDevolucao.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') " + ")");
            return ret;
        }
        public string Excluir()
        {
            string ret = "";
            if (this.idItemReserva != 0)
            {
                ret = Server.Executa(@"DELETE FROM ITENS_RESERVA WHERE ID_ITEM_RESERVA = " + this.idItemReserva.ToString());
            }
            return ret;
        }
        private string GetDataValida(string data)
        {
            return ((data == "01/01/0001 00:00:00") ? "" : data);
        }
    }
}
