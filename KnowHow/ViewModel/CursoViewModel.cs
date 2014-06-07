using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using KnowHow.Models;

namespace KnowHow.ViewModel
{
    public class CursoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Url { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public string Local { get; set; }
        public List<Categoria>_categorias { get; set; }
        public int CategoriaId { get; set; }
        public string Preco { get; set; }
        public string Organizador { get; set; }
        public string UrlDaImagem { get; set; }
        public int QuantidadeDeInteressados { get; set; }
        public int QuantidadeDeParticipantes { get; set; }
        public string HoraDeInicio { get; set; }
        public string Duracao { get; set; }
        public bool Aprovado { get; set; }
        public IEnumerable<SelectListItem> Categorias
        {
            get { return new SelectList(_categorias, "Id", "Nome"); }
        }

        public CursoViewModel()
        {
            
        }

        public CursoViewModel(Curso curso, List<Categoria> categorias )
        {
            _categorias = categorias;

            if (curso == null)
                curso = new Curso();
            ;
                
            Id = curso.Id;
            Nome = curso.Nome;
            Url = curso.Url;
            Descricao = curso.Descricao;
            CategoriaId = curso.Categoria == null ? 0 : curso.Categoria.Id;
            Data = curso.Data;
            Local = curso.Local;
            Organizador = curso.Organizador;
            Preco = curso.Preco;
            HoraDeInicio = curso.HoraDeInicio;
            Duracao = curso.Duracao;
            UrlDaImagem = curso.UrlDaImagem;
            Aprovado = curso.Aprovado;
            QuantidadeDeInteressados = curso.QuantidadeDeInteressados;
            QuantidadeDeParticipantes = curso.QuantidadeDeParticipantes;
            _categorias = categorias;
            
        }
    }
}