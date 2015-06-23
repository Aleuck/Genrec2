using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;

namespace UFRGS.Genrec.Data{
  public class Recurso{
      private int idRecurso;
      public int IdRecurso{
          get { return this.idRecurso;}
          set { this.idRecurso = value;}
      }
      private string codigo;
      public string Codigo{
          get { return this.codigo;}
          set { this.codigo = value;}
      }
      private string descricao;
      public string Descricao{
          get { return this.descricao;}
          set { this.descricao = value;}
      }
      private string fabricante;
      public string Fabricante{
          get { return this.fabricante;}
          set { this.fabricante = value;}
      }
      private string observacao;
      public string Observacao{
          get { return this.observacao;}
          set { this.observacao = value;}
      }
      private string situacao;
      public string Situacao{
          get { return this.situacao;}
          set { this.situacao = value;}
      }
      private DateTime dataAquisicao;
      public DateTime DataAquisicao{
          get { return this.dataAquisicao;}
          set { this.dataAquisicao = value;}
      }
      private DateTime dataInoperante;
      public DateTime DataInoperante{
          get { return this.dataInoperante;}
          set { this.dataInoperante = value;}
      }
      private DateTime dataEnvioManutencao;
      public DateTime DataEnvioManutencao{
          get { return this.dataEnvioManutencao;}
          set { this.dataEnvioManutencao = value;}
      }
      private DateTime dataRetornoManutencao;
      public DateTime DataRetornoManutencao{
          get { return this.dataRetornoManutencao;}
          set { this.dataRetornoManutencao = value;}
      }
      public override string ToString(){ return this.idRecurso.ToString(); }
      public Recurso(){}
      public Recurso(int idRecurso, string codigo, string descricao, string fabricante, string observacao, string situacao, DateTime dataAquisicao, DateTime dataInoperante, DateTime dataEnvioManutencao, DateTime dataRetornoManutencao){
          this.idRecurso = idRecurso;
          this.codigo = codigo;
          this.descricao = descricao;
          this.fabricante = fabricante;
          this.observacao = observacao;
          this.situacao = situacao;
          this.dataAquisicao = dataAquisicao;
          this.dataInoperante = dataInoperante;
          this.dataEnvioManutencao = dataEnvioManutencao;
          this.dataRetornoManutencao = dataRetornoManutencao;
      }
      public static int ID{
          get{
              try{
                  DataTable tabela = Server.Consulta("SELECT NVL(MAX(ID_RECURSO),0) ID FROM RECURSOS");
                  foreach (DataRow linha in tabela.Rows) return int.Parse(linha["ID"].ToString());
              }
              catch { }
              return 0;
          }
      }
      public static Recurso Carregar(int id){
          Recurso obj = new Recurso();
          DataTable tabela = Server.Consulta("SELECT ID_RECURSO, CODIGO, DESCRICAO, FABRICANTE, OBSERVACAO, SITUACAO, DATA_AQUISICAO, DATA_INOPERANTE, DATA_ENVIO_MANUTENCAO, DATA_RETORNO_MANUTENCAO FROM RECURSOS WHERE ID_RECURSO = "+id.ToString());
          foreach (DataRow linha in tabela.Rows){
              obj.IdRecurso = (linha["ID_RECURSO"].ToString() != "") ? int.Parse(linha["ID_RECURSO"].ToString()) : 0;
              obj.Codigo = linha["CODIGO"].ToString();
              obj.Descricao = linha["DESCRICAO"].ToString();
              obj.Fabricante = linha["FABRICANTE"].ToString();
              obj.Observacao = linha["OBSERVACAO"].ToString();
              obj.Situacao = linha["SITUACAO"].ToString();
              obj.DataAquisicao = (linha["DATA_AQUISICAO"].ToString() != "") ? DateTime.Parse(linha["DATA_AQUISICAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataInoperante = (linha["DATA_INOPERANTE"].ToString() != "") ? DateTime.Parse(linha["DATA_INOPERANTE"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataEnvioManutencao = (linha["DATA_ENVIO_MANUTENCAO"].ToString() != "") ? DateTime.Parse(linha["DATA_ENVIO_MANUTENCAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataRetornoManutencao = (linha["DATA_RETORNO_MANUTENCAO"].ToString() != "") ? DateTime.Parse(linha["DATA_RETORNO_MANUTENCAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
          }
          return obj;
      }
      public static Recurso Carregar(int id, int nivel){
          if (nivel < 1) return null;
          nivel--;
          Recurso obj = new Recurso();
          DataTable tabela = Server.Consulta("SELECT ID_RECURSO, CODIGO, DESCRICAO, FABRICANTE, OBSERVACAO, SITUACAO, DATA_AQUISICAO, DATA_INOPERANTE, DATA_ENVIO_MANUTENCAO, DATA_RETORNO_MANUTENCAO FROM RECURSOS WHERE ID_RECURSO = "+id.ToString());
          foreach (DataRow linha in tabela.Rows){
              obj.IdRecurso = (linha["ID_RECURSO"].ToString() != "") ? int.Parse(linha["ID_RECURSO"].ToString()) : 0;
              obj.Codigo = linha["CODIGO"].ToString();
              obj.Descricao = linha["DESCRICAO"].ToString();
              obj.Fabricante = linha["FABRICANTE"].ToString();
              obj.Observacao = linha["OBSERVACAO"].ToString();
              obj.Situacao = linha["SITUACAO"].ToString();
              obj.DataAquisicao = (linha["DATA_AQUISICAO"].ToString() != "") ? DateTime.Parse(linha["DATA_AQUISICAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataInoperante = (linha["DATA_INOPERANTE"].ToString() != "") ? DateTime.Parse(linha["DATA_INOPERANTE"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataEnvioManutencao = (linha["DATA_ENVIO_MANUTENCAO"].ToString() != "") ? DateTime.Parse(linha["DATA_ENVIO_MANUTENCAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataRetornoManutencao = (linha["DATA_RETORNO_MANUTENCAO"].ToString() != "") ? DateTime.Parse(linha["DATA_RETORNO_MANUTENCAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
          }
          return obj;
      }
      public static List<Recurso> Carregar(List<string> restricoes){
          string restricao = "";
          foreach (string rest in restricoes) restricao += rest + " AND ";
          restricao += "1 = 1";
          List<Recurso> objtos = new List<Recurso>();
          DataTable tabela = Server.Consulta("SELECT ID_RECURSO, CODIGO, DESCRICAO, FABRICANTE, OBSERVACAO, SITUACAO, DATA_AQUISICAO, DATA_INOPERANTE, DATA_ENVIO_MANUTENCAO, DATA_RETORNO_MANUTENCAO FROM RECURSOS WHERE " + restricao);
          foreach (DataRow linha in tabela.Rows){
              Recurso obj = new Recurso();
              obj.IdRecurso = (linha["ID_RECURSO"].ToString() != "") ? int.Parse(linha["ID_RECURSO"].ToString()) : 0;
              obj.Codigo = linha["CODIGO"].ToString();
              obj.Descricao = linha["DESCRICAO"].ToString();
              obj.Fabricante = linha["FABRICANTE"].ToString();
              obj.Observacao = linha["OBSERVACAO"].ToString();
              obj.Situacao = linha["SITUACAO"].ToString();
              obj.DataAquisicao = (linha["DATA_AQUISICAO"].ToString() != "") ? DateTime.Parse(linha["DATA_AQUISICAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataInoperante = (linha["DATA_INOPERANTE"].ToString() != "") ? DateTime.Parse(linha["DATA_INOPERANTE"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataEnvioManutencao = (linha["DATA_ENVIO_MANUTENCAO"].ToString() != "") ? DateTime.Parse(linha["DATA_ENVIO_MANUTENCAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataRetornoManutencao = (linha["DATA_RETORNO_MANUTENCAO"].ToString() != "") ? DateTime.Parse(linha["DATA_RETORNO_MANUTENCAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              objtos.Add(obj);
          }
          return objtos;
      }
      public static List<Recurso> Carregar(List<string> restricoes, int nlMinimo, int nlMaximo){
          string restricao = "";
          foreach (string rest in restricoes) restricao += rest + " AND ";
          restricao += "1 = 1";
          List<Recurso> objtos = new List<Recurso>();
          DataTable tabela = Server.Consulta("SELECT * FROM (SELECT ID_RECURSO, CODIGO, DESCRICAO, FABRICANTE, OBSERVACAO, SITUACAO, DATA_AQUISICAO, DATA_INOPERANTE, DATA_ENVIO_MANUTENCAO, DATA_RETORNO_MANUTENCAO, ROWNUM NLINHA FROM RECURSOS WHERE " + restricao + ") WHERE NLINHA BETWEEN " + nlMinimo + " AND " + nlMaximo);
          foreach (DataRow linha in tabela.Rows){
              Recurso obj = new Recurso();
              obj.IdRecurso = (linha["ID_RECURSO"].ToString() != "") ? int.Parse(linha["ID_RECURSO"].ToString()) : 0;
              obj.Codigo = linha["CODIGO"].ToString();
              obj.Descricao = linha["DESCRICAO"].ToString();
              obj.Fabricante = linha["FABRICANTE"].ToString();
              obj.Observacao = linha["OBSERVACAO"].ToString();
              obj.Situacao = linha["SITUACAO"].ToString();
              obj.DataAquisicao = (linha["DATA_AQUISICAO"].ToString() != "") ? DateTime.Parse(linha["DATA_AQUISICAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataInoperante = (linha["DATA_INOPERANTE"].ToString() != "") ? DateTime.Parse(linha["DATA_INOPERANTE"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataEnvioManutencao = (linha["DATA_ENVIO_MANUTENCAO"].ToString() != "") ? DateTime.Parse(linha["DATA_ENVIO_MANUTENCAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataRetornoManutencao = (linha["DATA_RETORNO_MANUTENCAO"].ToString() != "") ? DateTime.Parse(linha["DATA_RETORNO_MANUTENCAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              objtos.Add(obj);
          }
          return objtos;
      }
      public static List<Recurso> Carregar(List<string> restricoes, int nivel){
          if (nivel < 1) return null;
          nivel--;
          string restricao = "";
          foreach (string rest in restricoes) restricao += rest + " AND ";
          restricao += "1 = 1";
          List<Recurso> objtos = new List<Recurso>();
          DataTable tabela = Server.Consulta("SELECT ID_RECURSO, CODIGO, DESCRICAO, FABRICANTE, OBSERVACAO, SITUACAO, DATA_AQUISICAO, DATA_INOPERANTE, DATA_ENVIO_MANUTENCAO, DATA_RETORNO_MANUTENCAO FROM RECURSOS WHERE " + restricao);
          foreach (DataRow linha in tabela.Rows){
              Recurso obj = new Recurso();
              obj.IdRecurso = (linha["ID_RECURSO"].ToString() != "") ? int.Parse(linha["ID_RECURSO"].ToString()) : 0;
              obj.Codigo = linha["CODIGO"].ToString();
              obj.Descricao = linha["DESCRICAO"].ToString();
              obj.Fabricante = linha["FABRICANTE"].ToString();
              obj.Observacao = linha["OBSERVACAO"].ToString();
              obj.Situacao = linha["SITUACAO"].ToString();
              obj.DataAquisicao = (linha["DATA_AQUISICAO"].ToString() != "") ? DateTime.Parse(linha["DATA_AQUISICAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataInoperante = (linha["DATA_INOPERANTE"].ToString() != "") ? DateTime.Parse(linha["DATA_INOPERANTE"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataEnvioManutencao = (linha["DATA_ENVIO_MANUTENCAO"].ToString() != "") ? DateTime.Parse(linha["DATA_ENVIO_MANUTENCAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataRetornoManutencao = (linha["DATA_RETORNO_MANUTENCAO"].ToString() != "") ? DateTime.Parse(linha["DATA_RETORNO_MANUTENCAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              objtos.Add(obj);
          }
          return objtos;
      }
      public static List<Recurso> Carregar(){
          List<Recurso> objtos = new List<Recurso>();
          DataTable tabela = Server.Consulta("SELECT ID_RECURSO, CODIGO, DESCRICAO, FABRICANTE, OBSERVACAO, SITUACAO, DATA_AQUISICAO, DATA_INOPERANTE, DATA_ENVIO_MANUTENCAO, DATA_RETORNO_MANUTENCAO FROM RECURSOS");
          foreach (DataRow linha in tabela.Rows){
              Recurso obj = new Recurso();
              obj.IdRecurso = (linha["ID_RECURSO"].ToString() != "") ? int.Parse(linha["ID_RECURSO"].ToString()) : 0;
              obj.Codigo = linha["CODIGO"].ToString();
              obj.Descricao = linha["DESCRICAO"].ToString();
              obj.Fabricante = linha["FABRICANTE"].ToString();
              obj.Observacao = linha["OBSERVACAO"].ToString();
              obj.Situacao = linha["SITUACAO"].ToString();
              obj.DataAquisicao = (linha["DATA_AQUISICAO"].ToString() != "") ? DateTime.Parse(linha["DATA_AQUISICAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataInoperante = (linha["DATA_INOPERANTE"].ToString() != "") ? DateTime.Parse(linha["DATA_INOPERANTE"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataEnvioManutencao = (linha["DATA_ENVIO_MANUTENCAO"].ToString() != "") ? DateTime.Parse(linha["DATA_ENVIO_MANUTENCAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataRetornoManutencao = (linha["DATA_RETORNO_MANUTENCAO"].ToString() != "") ? DateTime.Parse(linha["DATA_RETORNO_MANUTENCAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              objtos.Add(obj);
          }
          return objtos;
      }
      public string Atualizar(){
          string ret = "";
          if(this.idRecurso != 0){
              ret = Server.Executa(@"UPDATE RECURSOS SET 
                      ID_RECURSO = " + this.idRecurso.ToString() + @",
                      CODIGO = '" + this.codigo + @"' ,
                      DESCRICAO = '" + this.descricao + @"' ,
                      FABRICANTE = '" + this.fabricante + @"' ,
                      OBSERVACAO = '" + this.observacao + @"' ,
                      SITUACAO = '" + this.situacao + @"' ,
                      DATA_AQUISICAO = TO_DATE('" + GetDataValida(this.dataAquisicao.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ,
                      DATA_INOPERANTE = TO_DATE('" + GetDataValida(this.dataInoperante.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ,
                      DATA_ENVIO_MANUTENCAO = TO_DATE('" + GetDataValida(this.dataEnvioManutencao.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ,
                      DATA_RETORNO_MANUTENCAO = TO_DATE('" + GetDataValida(this.dataRetornoManutencao.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') 
                      WHERE ID_RECURSO = " + this.idRecurso.ToString());
          }
          return ret;
      }
      public string Inserir(){
          string ret = Server.Executa(@"INSERT INTO RECURSOS(ID_RECURSO, CODIGO, DESCRICAO, FABRICANTE, OBSERVACAO, SITUACAO, DATA_AQUISICAO, DATA_INOPERANTE, DATA_ENVIO_MANUTENCAO, DATA_RETORNO_MANUTENCAO)
                      VALUES(" + this.idRecurso.ToString() + @",'" + this.codigo + @"' ,'" + this.descricao + @"' ,'" + this.fabricante + @"' ,'" + this.observacao + @"' ,'" + this.situacao + @"' ,TO_DATE('" + GetDataValida(this.dataAquisicao.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ,TO_DATE('" + GetDataValida(this.dataInoperante.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ,TO_DATE('" + GetDataValida(this.dataEnvioManutencao.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ,TO_DATE('" + GetDataValida(this.dataRetornoManutencao.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') " + ")");
          return ret;
      }
      public string Excluir(){
          string ret = "";
          if(this.idRecurso != 0){
              ret = Server.Executa(@"DELETE FROM RECURSOS WHERE ID_RECURSO = " + this.idRecurso.ToString());
          }
          return ret;
      }
      private string GetDataValida(string data){
          return ((data == "01/01/0001 00:00:00") ? "" : data);
      }
  }
}
