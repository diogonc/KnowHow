using System.Collections.Generic;
using System.Web.Mvc;
using KnowHow.Models;

namespace KnowHow.ViewModel
{
    public class CursosViewModel
    {
        public int CategoriaId { get; set; }
        public string Busca { get; set; }
        public IList<CursoViewModel> Cursos { get; set; }

        public List<Categoria> _categorias { get; set; }
        public IEnumerable<SelectListItem> Categorias
        {
            get { return new SelectList(_categorias, "Id", "Nome"); }
        }
    }
}