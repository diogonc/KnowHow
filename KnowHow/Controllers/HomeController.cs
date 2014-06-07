using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using KnowHow.Models;
using KnowHow.ViewModel;

namespace KnowHow.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var categoriaId = HttpContext.Session["CategoriaId"] != null ? Convert.ToInt32(HttpContext.Session["CategoriaId"]) : 0;
            var busca = HttpContext.Session["Busca"] != null ? HttpContext.Session["Busca"].ToString().ToUpper() : null;
            var maisProcurados = HttpContext.Session["MaisProcurados"] != null ? Convert.ToBoolean(Session["MaisProcurados"]) : false;

            var cursos = FiltraCursos(categoriaId, busca, maisProcurados);

            var categorias = db.Categorias.OrderBy(x => x.Nome).ToList();

            var listaDeCursosViewModel = cursos.Select(curso => new CursoViewModel(curso, categorias)).ToList();

            var cursosViewModel = new CursosViewModel() { _categorias = categorias, Cursos = listaDeCursosViewModel, CategoriaId = categoriaId, Busca = busca };

            return View(cursosViewModel);
        }

        private List<Curso> FiltraCursos(int categoriaId, string busca, bool maisProcurados)
        {
            var cursos = db.Cursos.Where(x => x.Data >= DateTime.Today && x.Aprovado).ToList();

            if (categoriaId != 0)
            {
                var categoria = db.Categorias.Find(categoriaId);
                cursos = cursos.Where(x => x.Categoria == categoria).ToList();
            }

            if (busca != null)
                cursos = cursos.Where(x => x.Nome.ToUpper().Contains(busca) || x.Descricao.ToUpper().Contains(busca)).ToList();

            if (maisProcurados)
                return cursos.OrderByDescending(x => x.QuantidadeDeInteressados).ThenBy(x => x.Data).ToList();

            return cursos.OrderBy(x => x.Data).ToList();
        }

        [HttpPost]
        public ActionResult Buscar(BuscaViewModel buscaViewModel)
        {
            HttpContext.Session["CategoriaId"] = buscaViewModel.CategoriaId;
            HttpContext.Session["Busca"] = buscaViewModel.Busca;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult KnowHow()
        {
            HttpContext.Session["CategoriaId"] = 0;
            HttpContext.Session["Busca"] = null;
            HttpContext.Session["MaisProcurados"] = false;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult MaisProcurados()
        {
            HttpContext.Session["CategoriaId"] = 0;
            HttpContext.Session["Busca"] = null;
            HttpContext.Session["MaisProcurados"] = true;

            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}