using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;

namespace UFRGS.Genrec.Data{
  public class Funcionario{
      private int idFuncionario;
      public int IdFuncionario{
          get { return this.idFuncionario;}
          set { this.idFuncionario = value;}
      }
      private string matricula;
      public string Matricula{
          get { return this.matricula;}
          set { this.matricula = value;}
      }
      private string nome;
      public string Nome{
          get { return this.nome;}
          set { this.nome = value;}
      }
      private string cpf;
      public string Cpf{
          get { return this.cpf;}
          set { this.cpf = value;}
      }
      private DateTime dataNascimento;
      public DateTime DataNascimento{
          get { return this.dataNascimento;}
          set { this.dataNascimento = value;}
      }
      private string email;
      public string Email{
          get { return this.email;}
          set { this.email = value;}
      }
      private string senha;
      public string Senha{
          get { return this.senha;}
          set { this.senha = value;}
      }
      private Cargo cargo;
      public Cargo Cargo{
          get { return this.cargo;}
          set { this.cargo = value;}
      }
      private DateTime dataAdmissao;
      public DateTime DataAdmissao{
          get { return this.dataAdmissao;}
          set { this.dataAdmissao = value;}
      }
      private DateTime dataDemissao;
      public DateTime DataDemissao{
          get { return this.dataDemissao;}
          set { this.dataDemissao = value;}
      }
      private int nivel;
      public int Nivel{
          get { return this.nivel;}
          set { this.nivel = value;}
      }
      private int prioridade;
      public int Prioridade{
          get { return (this.indiceDePenalidade < 2) ? this.prioridade : 4;}
          set { this.prioridade = (value > 4) ? 4 : ((value < 1) ? 1 : value); }
      }
      private DateTime dataModificacaoPrioridade;
      public DateTime DataModificacaoPrioridade{
          get { return this.dataModificacaoPrioridade;}
          set { this.dataModificacaoPrioridade = value;}
      }
      private int indiceDePenalidade;
      public int IndiceDePenalidade{
          get { return this.indiceDePenalidade;}
          set { this.indiceDePenalidade = (value > 2) ? 2 : ((value < 0)? 0: value); }
      }
      public override string ToString(){ return this.idFuncionario.ToString(); }
      public Funcionario(){}
      public Funcionario(int idFuncionario, string matricula, string nome, string cpf, DateTime dataNascimento, string email, string senha, Cargo cargo, DateTime dataAdmissao, DateTime dataDemissao, int nivel, int prioridade, DateTime dataModificacaoPrioridade, int indiceDePenalidade){
          this.idFuncionario = idFuncionario;
          this.matricula = matricula;
          this.nome = nome;
          this.cpf = cpf;
          this.dataNascimento = dataNascimento;
          this.email = email;
          this.senha = senha;
          this.cargo = cargo;
          this.dataAdmissao = dataAdmissao;
          this.dataDemissao = dataDemissao;
          this.nivel = nivel;
          this.prioridade = prioridade;
          this.dataModificacaoPrioridade = dataModificacaoPrioridade;
          this.indiceDePenalidade = indiceDePenalidade;
      }
      public static int ID{
          get{
              try{
                  DataTable tabela = Server.Consulta("SELECT NVL(MAX(ID_FUNCIONARIO),0) ID FROM FUNCIONARIOS");
                  foreach (DataRow linha in tabela.Rows) return int.Parse(linha["ID"].ToString());
              }
              catch { }
              return 0;
          }
      }
      public static Funcionario Carregar(int id){
          Funcionario obj = new Funcionario();
          DataTable tabela = Server.Consulta("SELECT ID_FUNCIONARIO, MATRICULA, NOME, CPF, DATA_NASCIMENTO, EMAIL, SENHA, ID_CARGO, DATA_ADMISSAO, DATA_DEMISSAO, NIVEL, PRIORIDADE, DATA_MODIFICACAO_PRIORIDADE, INDICE_DE_PENALIDADE FROM FUNCIONARIOS WHERE ID_FUNCIONARIO = "+id.ToString());
          foreach (DataRow linha in tabela.Rows){
              obj.IdFuncionario = (linha["ID_FUNCIONARIO"].ToString() != "") ? int.Parse(linha["ID_FUNCIONARIO"].ToString()) : 0;
              obj.Matricula = linha["MATRICULA"].ToString();
              obj.Nome = linha["NOME"].ToString();
              obj.Cpf = linha["CPF"].ToString();
              obj.DataNascimento = (linha["DATA_NASCIMENTO"].ToString() != "") ? DateTime.Parse(linha["DATA_NASCIMENTO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.Email = linha["EMAIL"].ToString();
              obj.Senha = linha["SENHA"].ToString();
              obj.Cargo = Cargo.Carregar((linha["ID_CARGO"].ToString() != "") ? int.Parse(linha["ID_CARGO"].ToString()) : 0);
              obj.DataAdmissao = (linha["DATA_ADMISSAO"].ToString() != "") ? DateTime.Parse(linha["DATA_ADMISSAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataDemissao = (linha["DATA_DEMISSAO"].ToString() != "") ? DateTime.Parse(linha["DATA_DEMISSAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.Nivel = (linha["NIVEL"].ToString() != "") ? int.Parse(linha["NIVEL"].ToString()) : 0;
              obj.Prioridade = (linha["PRIORIDADE"].ToString() != "") ? int.Parse(linha["PRIORIDADE"].ToString()) : 0;
              obj.DataModificacaoPrioridade = (linha["DATA_MODIFICACAO_PRIORIDADE"].ToString() != "") ? DateTime.Parse(linha["DATA_MODIFICACAO_PRIORIDADE"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.IndiceDePenalidade = (linha["INDICE_DE_PENALIDADE"].ToString() != "") ? int.Parse(linha["INDICE_DE_PENALIDADE"].ToString()) : 0;
          }
          return obj;
      }
      public static Funcionario Carregar(int id, int nivel){
          if (nivel < 1) return null;
          nivel--;
          Funcionario obj = new Funcionario();
          DataTable tabela = Server.Consulta("SELECT ID_FUNCIONARIO, MATRICULA, NOME, CPF, DATA_NASCIMENTO, EMAIL, SENHA, ID_CARGO, DATA_ADMISSAO, DATA_DEMISSAO, NIVEL, PRIORIDADE, DATA_MODIFICACAO_PRIORIDADE, INDICE_DE_PENALIDADE FROM FUNCIONARIOS WHERE ID_FUNCIONARIO = "+id.ToString());
          foreach (DataRow linha in tabela.Rows){
              obj.IdFuncionario = (linha["ID_FUNCIONARIO"].ToString() != "") ? int.Parse(linha["ID_FUNCIONARIO"].ToString()) : 0;
              obj.Matricula = linha["MATRICULA"].ToString();
              obj.Nome = linha["NOME"].ToString();
              obj.Cpf = linha["CPF"].ToString();
              obj.DataNascimento = (linha["DATA_NASCIMENTO"].ToString() != "") ? DateTime.Parse(linha["DATA_NASCIMENTO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.Email = linha["EMAIL"].ToString();
              obj.Senha = linha["SENHA"].ToString();
              obj.Cargo = Cargo.Carregar((linha["ID_CARGO"].ToString() != "") ? int.Parse(linha["ID_CARGO"].ToString()) : 0, nivel);
              obj.DataAdmissao = (linha["DATA_ADMISSAO"].ToString() != "") ? DateTime.Parse(linha["DATA_ADMISSAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataDemissao = (linha["DATA_DEMISSAO"].ToString() != "") ? DateTime.Parse(linha["DATA_DEMISSAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.Nivel = (linha["NIVEL"].ToString() != "") ? int.Parse(linha["NIVEL"].ToString()) : 0;
              obj.Prioridade = (linha["PRIORIDADE"].ToString() != "") ? int.Parse(linha["PRIORIDADE"].ToString()) : 0;
              obj.DataModificacaoPrioridade = (linha["DATA_MODIFICACAO_PRIORIDADE"].ToString() != "") ? DateTime.Parse(linha["DATA_MODIFICACAO_PRIORIDADE"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.IndiceDePenalidade = (linha["INDICE_DE_PENALIDADE"].ToString() != "") ? int.Parse(linha["INDICE_DE_PENALIDADE"].ToString()) : 0;
          }
          return obj;
      }
      public static List<Funcionario> Carregar(List<string> restricoes){
          string restricao = "";
          foreach (string rest in restricoes) restricao += rest + " AND ";
          restricao += "1 = 1";
          List<Funcionario> objtos = new List<Funcionario>();
          DataTable tabela = Server.Consulta("SELECT ID_FUNCIONARIO, MATRICULA, NOME, CPF, DATA_NASCIMENTO, EMAIL, SENHA, ID_CARGO, DATA_ADMISSAO, DATA_DEMISSAO, NIVEL, PRIORIDADE, DATA_MODIFICACAO_PRIORIDADE, INDICE_DE_PENALIDADE FROM FUNCIONARIOS WHERE " + restricao);
          foreach (DataRow linha in tabela.Rows){
              Funcionario obj = new Funcionario();
              obj.IdFuncionario = (linha["ID_FUNCIONARIO"].ToString() != "") ? int.Parse(linha["ID_FUNCIONARIO"].ToString()) : 0;
              obj.Matricula = linha["MATRICULA"].ToString();
              obj.Nome = linha["NOME"].ToString();
              obj.Cpf = linha["CPF"].ToString();
              obj.DataNascimento = (linha["DATA_NASCIMENTO"].ToString() != "") ? DateTime.Parse(linha["DATA_NASCIMENTO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.Email = linha["EMAIL"].ToString();
              obj.Senha = linha["SENHA"].ToString();
              obj.Cargo = Cargo.Carregar((linha["ID_CARGO"].ToString() != "") ? int.Parse(linha["ID_CARGO"].ToString()) : 0);
              obj.DataAdmissao = (linha["DATA_ADMISSAO"].ToString() != "") ? DateTime.Parse(linha["DATA_ADMISSAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataDemissao = (linha["DATA_DEMISSAO"].ToString() != "") ? DateTime.Parse(linha["DATA_DEMISSAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.Nivel = (linha["NIVEL"].ToString() != "") ? int.Parse(linha["NIVEL"].ToString()) : 0;
              obj.Prioridade = (linha["PRIORIDADE"].ToString() != "") ? int.Parse(linha["PRIORIDADE"].ToString()) : 0;
              obj.DataModificacaoPrioridade = (linha["DATA_MODIFICACAO_PRIORIDADE"].ToString() != "") ? DateTime.Parse(linha["DATA_MODIFICACAO_PRIORIDADE"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.IndiceDePenalidade = (linha["INDICE_DE_PENALIDADE"].ToString() != "") ? int.Parse(linha["INDICE_DE_PENALIDADE"].ToString()) : 0;
              objtos.Add(obj);
          }
          return objtos;
      }
      public static List<Funcionario> Carregar(List<string> restricoes, int nlMinimo, int nlMaximo){
          string restricao = "";
          foreach (string rest in restricoes) restricao += rest + " AND ";
          restricao += "1 = 1";
          List<Funcionario> objtos = new List<Funcionario>();
          DataTable tabela = Server.Consulta("SELECT * FROM (SELECT ID_FUNCIONARIO, MATRICULA, NOME, CPF, DATA_NASCIMENTO, EMAIL, SENHA, ID_CARGO, DATA_ADMISSAO, DATA_DEMISSAO, NIVEL, PRIORIDADE, DATA_MODIFICACAO_PRIORIDADE, INDICE_DE_PENALIDADE, ROWNUM NLINHA FROM FUNCIONARIOS WHERE " + restricao + ") WHERE NLINHA BETWEEN " + nlMinimo + " AND " + nlMaximo);
          foreach (DataRow linha in tabela.Rows){
              Funcionario obj = new Funcionario();
              obj.IdFuncionario = (linha["ID_FUNCIONARIO"].ToString() != "") ? int.Parse(linha["ID_FUNCIONARIO"].ToString()) : 0;
              obj.Matricula = linha["MATRICULA"].ToString();
              obj.Nome = linha["NOME"].ToString();
              obj.Cpf = linha["CPF"].ToString();
              obj.DataNascimento = (linha["DATA_NASCIMENTO"].ToString() != "") ? DateTime.Parse(linha["DATA_NASCIMENTO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.Email = linha["EMAIL"].ToString();
              obj.Senha = linha["SENHA"].ToString();
              obj.Cargo = Cargo.Carregar((linha["ID_CARGO"].ToString() != "") ? int.Parse(linha["ID_CARGO"].ToString()) : 0);
              obj.DataAdmissao = (linha["DATA_ADMISSAO"].ToString() != "") ? DateTime.Parse(linha["DATA_ADMISSAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataDemissao = (linha["DATA_DEMISSAO"].ToString() != "") ? DateTime.Parse(linha["DATA_DEMISSAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.Nivel = (linha["NIVEL"].ToString() != "") ? int.Parse(linha["NIVEL"].ToString()) : 0;
              obj.Prioridade = (linha["PRIORIDADE"].ToString() != "") ? int.Parse(linha["PRIORIDADE"].ToString()) : 0;
              obj.DataModificacaoPrioridade = (linha["DATA_MODIFICACAO_PRIORIDADE"].ToString() != "") ? DateTime.Parse(linha["DATA_MODIFICACAO_PRIORIDADE"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.IndiceDePenalidade = (linha["INDICE_DE_PENALIDADE"].ToString() != "") ? int.Parse(linha["INDICE_DE_PENALIDADE"].ToString()) : 0;
              objtos.Add(obj);
          }
          return objtos;
      }
      public static List<Funcionario> Carregar(List<string> restricoes, int nivel){
          if (nivel < 1) return null;
          nivel--;
          string restricao = "";
          foreach (string rest in restricoes) restricao += rest + " AND ";
          restricao += "1 = 1";
          List<Funcionario> objtos = new List<Funcionario>();
          DataTable tabela = Server.Consulta("SELECT ID_FUNCIONARIO, MATRICULA, NOME, CPF, DATA_NASCIMENTO, EMAIL, SENHA, ID_CARGO, DATA_ADMISSAO, DATA_DEMISSAO, NIVEL, PRIORIDADE, DATA_MODIFICACAO_PRIORIDADE, INDICE_DE_PENALIDADE FROM FUNCIONARIOS WHERE " + restricao);
          foreach (DataRow linha in tabela.Rows){
              Funcionario obj = new Funcionario();
              obj.IdFuncionario = (linha["ID_FUNCIONARIO"].ToString() != "") ? int.Parse(linha["ID_FUNCIONARIO"].ToString()) : 0;
              obj.Matricula = linha["MATRICULA"].ToString();
              obj.Nome = linha["NOME"].ToString();
              obj.Cpf = linha["CPF"].ToString();
              obj.DataNascimento = (linha["DATA_NASCIMENTO"].ToString() != "") ? DateTime.Parse(linha["DATA_NASCIMENTO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.Email = linha["EMAIL"].ToString();
              obj.Senha = linha["SENHA"].ToString();
              obj.Cargo = Cargo.Carregar((linha["ID_CARGO"].ToString() != "") ? int.Parse(linha["ID_CARGO"].ToString()) : 0, nivel);
              obj.DataAdmissao = (linha["DATA_ADMISSAO"].ToString() != "") ? DateTime.Parse(linha["DATA_ADMISSAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataDemissao = (linha["DATA_DEMISSAO"].ToString() != "") ? DateTime.Parse(linha["DATA_DEMISSAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.Nivel = (linha["NIVEL"].ToString() != "") ? int.Parse(linha["NIVEL"].ToString()) : 0;
              obj.Prioridade = (linha["PRIORIDADE"].ToString() != "") ? int.Parse(linha["PRIORIDADE"].ToString()) : 0;
              obj.DataModificacaoPrioridade = (linha["DATA_MODIFICACAO_PRIORIDADE"].ToString() != "") ? DateTime.Parse(linha["DATA_MODIFICACAO_PRIORIDADE"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.IndiceDePenalidade = (linha["INDICE_DE_PENALIDADE"].ToString() != "") ? int.Parse(linha["INDICE_DE_PENALIDADE"].ToString()) : 0;
              objtos.Add(obj);
          }
          return objtos;
      }
      public static List<Funcionario> Carregar(){
          List<Funcionario> objtos = new List<Funcionario>();
          DataTable tabela = Server.Consulta("SELECT ID_FUNCIONARIO, MATRICULA, NOME, CPF, DATA_NASCIMENTO, EMAIL, SENHA, ID_CARGO, DATA_ADMISSAO, DATA_DEMISSAO, NIVEL, PRIORIDADE, DATA_MODIFICACAO_PRIORIDADE, INDICE_DE_PENALIDADE FROM FUNCIONARIOS");
          foreach (DataRow linha in tabela.Rows){
              Funcionario obj = new Funcionario();
              obj.IdFuncionario = (linha["ID_FUNCIONARIO"].ToString() != "") ? int.Parse(linha["ID_FUNCIONARIO"].ToString()) : 0;
              obj.Matricula = linha["MATRICULA"].ToString();
              obj.Nome = linha["NOME"].ToString();
              obj.Cpf = linha["CPF"].ToString();
              obj.DataNascimento = (linha["DATA_NASCIMENTO"].ToString() != "") ? DateTime.Parse(linha["DATA_NASCIMENTO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.Email = linha["EMAIL"].ToString();
              obj.Senha = linha["SENHA"].ToString();
              obj.Cargo = Cargo.Carregar((linha["ID_CARGO"].ToString() != "") ? int.Parse(linha["ID_CARGO"].ToString()) : 0);
              obj.DataAdmissao = (linha["DATA_ADMISSAO"].ToString() != "") ? DateTime.Parse(linha["DATA_ADMISSAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.DataDemissao = (linha["DATA_DEMISSAO"].ToString() != "") ? DateTime.Parse(linha["DATA_DEMISSAO"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.Nivel = (linha["NIVEL"].ToString() != "") ? int.Parse(linha["NIVEL"].ToString()) : 0;
              obj.Prioridade = (linha["PRIORIDADE"].ToString() != "") ? int.Parse(linha["PRIORIDADE"].ToString()) : 0;
              obj.DataModificacaoPrioridade = (linha["DATA_MODIFICACAO_PRIORIDADE"].ToString() != "") ? DateTime.Parse(linha["DATA_MODIFICACAO_PRIORIDADE"].ToString(), new System.Globalization.CultureInfo("pt-BR")) : new DateTime();
              obj.IndiceDePenalidade = (linha["INDICE_DE_PENALIDADE"].ToString() != "") ? int.Parse(linha["INDICE_DE_PENALIDADE"].ToString()) : 0;
              objtos.Add(obj);
          }
          return objtos;
      }
      public string Atualizar(){
          string ret = "";
          if(this.idFuncionario != 0){
              ret = Server.Executa(@"UPDATE FUNCIONARIOS SET 
                      ID_FUNCIONARIO = " + this.idFuncionario.ToString() + @",
                      MATRICULA = '" + this.matricula + @"' ,
                      NOME = '" + this.nome + @"' ,
                      CPF = '" + this.cpf + @"' ,
                      DATA_NASCIMENTO = TO_DATE('" + GetDataValida(this.dataNascimento.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ,
                      EMAIL = '" + this.email + @"' ,
                      SENHA = '" + this.senha + @"' ,
                      ID_CARGO = " + this.cargo.ToString() + @",
                      DATA_ADMISSAO = TO_DATE('" + GetDataValida(this.dataAdmissao.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ,
                      DATA_DEMISSAO = TO_DATE('" + GetDataValida(this.dataDemissao.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ,
                      NIVEL = " + this.nivel.ToString() + @",
                      PRIORIDADE = " + this.prioridade.ToString() + @",
                      DATA_MODIFICACAO_PRIORIDADE = TO_DATE('" + GetDataValida(this.dataModificacaoPrioridade.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ,
                      INDICE_DE_PENALIDADE = " + this.indiceDePenalidade.ToString() + @"
                      WHERE ID_FUNCIONARIO = " + this.idFuncionario.ToString());
          }
          return ret;
      }
      public string Inserir(){
          string ret = Server.Executa(@"INSERT INTO FUNCIONARIOS(ID_FUNCIONARIO, MATRICULA, NOME, CPF, DATA_NASCIMENTO, EMAIL, SENHA, ID_CARGO, DATA_ADMISSAO, DATA_DEMISSAO, NIVEL, PRIORIDADE, DATA_MODIFICACAO_PRIORIDADE, INDICE_DE_PENALIDADE)
                      VALUES(" + this.idFuncionario.ToString() + @",'" + this.matricula + @"' ,'" + this.nome + @"' ,'" + this.cpf + @"' ,TO_DATE('" + GetDataValida(this.dataNascimento.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ,'" + this.email + @"' ,'" + this.senha + @"' ," + this.cargo.ToString() + @",TO_DATE('" + GetDataValida(this.dataAdmissao.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ,TO_DATE('" + GetDataValida(this.dataDemissao.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ," + this.nivel.ToString() + @"," + this.prioridade.ToString() + @",TO_DATE('" + GetDataValida(this.dataModificacaoPrioridade.ToString("dd/MM/yyyy HH:mm:ss")) + @"', 'DD/MM/YYYY HH24:MI:SS') ," + this.indiceDePenalidade.ToString() + @"" + ")");
          return ret;
      }
      public string Excluir(){
          string ret = "";
          if(this.idFuncionario != 0){
              ret = Server.Executa(@"DELETE FROM FUNCIONARIOS WHERE ID_FUNCIONARIO = " + this.idFuncionario.ToString());
          }
          return ret;
      }
      private string GetDataValida(string data){
          return ((data == "01/01/0001 00:00:00") ? "" : data);
      }
  }
}
