using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;

namespace UFRGS.Genrec.Data{
  public class Cargo{
      private int idCargo;
      public int IdCargo{
          get { return this.idCargo;}
          set { this.idCargo = value;}
      }
      private string nome;
      public string Nome{
          get { return this.nome;}
          set { this.nome = value;}
      }
      private int prioridadePadrao;
      public int PrioridadePadrao{
          get { return this.prioridadePadrao;}
          set { this.prioridadePadrao = value;}
      }
      public override string ToString(){ return this.idCargo.ToString(); }
      public Cargo(){}
      public Cargo(int idCargo, string nome, int prioridadePadrao){
          this.idCargo = idCargo;
          this.nome = nome;
          this.prioridadePadrao = prioridadePadrao;
      }
      public static int ID{
          get{
              try{
                  DataTable tabela = Server.Consulta("SELECT NVL(MAX(ID_CARGO),0) ID FROM CARGOS");
                  foreach (DataRow linha in tabela.Rows) return int.Parse(linha["ID"].ToString());
              }
              catch { }
              return 0;
          }
      }
      public static Cargo Carregar(int id){
          Cargo obj = new Cargo();
          DataTable tabela = Server.Consulta("SELECT ID_CARGO, NOME, PRIORIDADE_PADRAO FROM CARGOS WHERE ID_CARGO = "+id.ToString());
          foreach (DataRow linha in tabela.Rows){
              obj.IdCargo = (linha["ID_CARGO"].ToString() != "") ? int.Parse(linha["ID_CARGO"].ToString()) : 0;
              obj.Nome = linha["NOME"].ToString();
              obj.PrioridadePadrao = (linha["PRIORIDADE_PADRAO"].ToString() != "") ? int.Parse(linha["PRIORIDADE_PADRAO"].ToString()) : 0;
          }
          return obj;
      }
      public static Cargo Carregar(int id, int nivel){
          if (nivel < 1) return null;
          nivel--;
          Cargo obj = new Cargo();
          DataTable tabela = Server.Consulta("SELECT ID_CARGO, NOME, PRIORIDADE_PADRAO FROM CARGOS WHERE ID_CARGO = "+id.ToString());
          foreach (DataRow linha in tabela.Rows){
              obj.IdCargo = (linha["ID_CARGO"].ToString() != "") ? int.Parse(linha["ID_CARGO"].ToString()) : 0;
              obj.Nome = linha["NOME"].ToString();
              obj.PrioridadePadrao = (linha["PRIORIDADE_PADRAO"].ToString() != "") ? int.Parse(linha["PRIORIDADE_PADRAO"].ToString()) : 0;
          }
          return obj;
      }
      public static List<Cargo> Carregar(List<string> restricoes){
          string restricao = "";
          foreach (string rest in restricoes) restricao += rest + " AND ";
          restricao += "1 = 1";
          List<Cargo> objtos = new List<Cargo>();
          DataTable tabela = Server.Consulta("SELECT ID_CARGO, NOME, PRIORIDADE_PADRAO FROM CARGOS WHERE " + restricao);
          foreach (DataRow linha in tabela.Rows){
              Cargo obj = new Cargo();
              obj.IdCargo = (linha["ID_CARGO"].ToString() != "") ? int.Parse(linha["ID_CARGO"].ToString()) : 0;
              obj.Nome = linha["NOME"].ToString();
              obj.PrioridadePadrao = (linha["PRIORIDADE_PADRAO"].ToString() != "") ? int.Parse(linha["PRIORIDADE_PADRAO"].ToString()) : 0;
              objtos.Add(obj);
          }
          return objtos;
      }
      public static List<Cargo> Carregar(List<string> restricoes, int nlMinimo, int nlMaximo){
          string restricao = "";
          foreach (string rest in restricoes) restricao += rest + " AND ";
          restricao += "1 = 1";
          List<Cargo> objtos = new List<Cargo>();
          DataTable tabela = Server.Consulta("SELECT * FROM (SELECT ID_CARGO, NOME, PRIORIDADE_PADRAO, ROWNUM NLINHA FROM CARGOS WHERE " + restricao + ") WHERE NLINHA BETWEEN " + nlMinimo + " AND " + nlMaximo);
          foreach (DataRow linha in tabela.Rows){
              Cargo obj = new Cargo();
              obj.IdCargo = (linha["ID_CARGO"].ToString() != "") ? int.Parse(linha["ID_CARGO"].ToString()) : 0;
              obj.Nome = linha["NOME"].ToString();
              obj.PrioridadePadrao = (linha["PRIORIDADE_PADRAO"].ToString() != "") ? int.Parse(linha["PRIORIDADE_PADRAO"].ToString()) : 0;
              objtos.Add(obj);
          }
          return objtos;
      }
      public static List<Cargo> Carregar(List<string> restricoes, int nivel){
          if (nivel < 1) return null;
          nivel--;
          string restricao = "";
          foreach (string rest in restricoes) restricao += rest + " AND ";
          restricao += "1 = 1";
          List<Cargo> objtos = new List<Cargo>();
          DataTable tabela = Server.Consulta("SELECT ID_CARGO, NOME, PRIORIDADE_PADRAO FROM CARGOS WHERE " + restricao);
          foreach (DataRow linha in tabela.Rows){
              Cargo obj = new Cargo();
              obj.IdCargo = (linha["ID_CARGO"].ToString() != "") ? int.Parse(linha["ID_CARGO"].ToString()) : 0;
              obj.Nome = linha["NOME"].ToString();
              obj.PrioridadePadrao = (linha["PRIORIDADE_PADRAO"].ToString() != "") ? int.Parse(linha["PRIORIDADE_PADRAO"].ToString()) : 0;
              objtos.Add(obj);
          }
          return objtos;
      }
      public static List<Cargo> Carregar(){
          List<Cargo> objtos = new List<Cargo>();
          DataTable tabela = Server.Consulta("SELECT ID_CARGO, NOME, PRIORIDADE_PADRAO FROM CARGOS");
          foreach (DataRow linha in tabela.Rows){
              Cargo obj = new Cargo();
              obj.IdCargo = (linha["ID_CARGO"].ToString() != "") ? int.Parse(linha["ID_CARGO"].ToString()) : 0;
              obj.Nome = linha["NOME"].ToString();
              obj.PrioridadePadrao = (linha["PRIORIDADE_PADRAO"].ToString() != "") ? int.Parse(linha["PRIORIDADE_PADRAO"].ToString()) : 0;
              objtos.Add(obj);
          }
          return objtos;
      }
      public string Atualizar(){
          string ret = "";
          if(this.idCargo != 0){
              ret = Server.Executa(@"UPDATE CARGOS SET 
                      ID_CARGO = " + this.idCargo.ToString() + @",
                      NOME = '" + this.nome + @"' ,
                      PRIORIDADE_PADRAO = " + this.prioridadePadrao.ToString() + @"
                      WHERE ID_CARGO = " + this.idCargo.ToString());
          }
          return ret;
      }
      public string Inserir(){
          string ret = Server.Executa(@"INSERT INTO CARGOS(ID_CARGO, NOME, PRIORIDADE_PADRAO)
                      VALUES(" + this.idCargo.ToString() + @",'" + this.nome + @"' ," + this.prioridadePadrao.ToString() + @"" + ")");
          return ret;
      }
      public string Excluir(){
          string ret = "";
          if(this.idCargo != 0){
              ret = Server.Executa(@"DELETE FROM CARGOS WHERE ID_CARGO = " + this.idCargo.ToString());
          }
          return ret;
      }
      private string GetDataValida(string data){
          return ((data == "01/01/0001 00:00:00") ? "" : data);
      }
  }
}
