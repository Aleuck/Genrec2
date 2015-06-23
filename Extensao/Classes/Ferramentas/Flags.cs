using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Genrec
{
    public static class ICONES_ALERTA
    {
        public const string ICONE_OK = "<i class=\"glyphicon glyphicon-ok\" ></i>";
        public const string ICONE_ERRO = "<i class=\"glyphicon glyphicon-warning-sign\" ></i>";
        public const string ICONE_CADEADO = "<i class=\"fa fa-lock\" ></i>";
        public const string ICONE_ASTERISCO = "<i class=\"glyphicon glyphicon-asterisk\" ></i>";
    }
    public static class CLASSES_ALERTA
    {
        public const string ALERTA_SUCESSO = "alert alert-success";
        public const string ALERTA_ERRO = "alert alert-danger";
        public const string ALERTA_ATENCAO = "alert alert-info";
        public const string OCULTO = "hidden";
    }
    public static class USUARIOS
    {
        public const int MODERADOR_MASTER = 1;
        public const int USUARIO_ANONIMO = 0;
    }
    public static class TIPO_PARTICIPANTE
    {
        public const string FACEBOOK = "F";
        public const string TWITTER = "T";
        public const string REITORIA_DIGITAL = "C";
    }
    public static class GENERO
    {
        public const string MASCULINO = "M";
        public const string FEMININO = "F";
        public const string NAO_DECLARADO = "N";
    }
    public static class SITUACAO_MODERACAO
    {
        public const string APROVADO = "A";
        public const string REPROVADO = "R";
        public const string NAO_ANALISADO = "N";
    }
    public static class TIPO_MODULO
    {
        public const string REITORA_CONSULTA = "C";
        public const string PERGUNTAS_PARA_REITORA = "P";
    }
}