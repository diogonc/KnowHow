using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnowHow.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Url { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public string Local { get; set; }
        public Categoria Categoria { get; set; }
        public decimal Preco { get; set; }
        public string Organizador { get; set; }
        public int QuantidadeDeInteressados { get; set; }
        public int QuantidadeDeParticipantes { get; set; }
        public string UrlDaImagem { get; set;  }
        public string HoraDeInicio { get; set; }
        public string Duracao { get; set; }
        public bool Aprovado { get; set; }
        
        public Curso()
        {
            Data = DateTime.Today;
            QuantidadeDeInteressados = 0;
            Aprovado = false;
        }

        public Curso(int id, string nome, string url, DateTime data, string local, Categoria categoria, decimal preco, string organizador, int quantidadeDeInteressados,int quantidadeDeParticipantes, string horaDeInicio, string duracao, string descricao, bool aprovado,string urlDaImagem = "")
        {
            Id = id;
            Nome = nome;
            Url = url;
            Descricao = descricao;
            Data = data;
            Local = local;
            Categoria = categoria;
            Preco = preco;
            Organizador = organizador;
            QuantidadeDeInteressados = quantidadeDeInteressados;
            QuantidadeDeParticipantes = quantidadeDeParticipantes;
            UrlDaImagem = urlDaImagem;
            HoraDeInicio = horaDeInicio;
            Duracao = duracao;
            Aprovado = aprovado;
        }

        public void AdicionarInteressado()
        {
            QuantidadeDeInteressados++;
        }

        public void AdicionarParticipante()
        {
            QuantidadeDeParticipantes++;
        }
    
        public void Aprovar()
        {
            Aprovado = true;
        }
    }
}