using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KnowHow.Models;
using KnowHow.ViewModel;

namespace KnowHow.Controllers
{
    public class CursoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var logado = HttpContext.Session["logado"] != null ? Convert.ToBoolean(Session["logado"]) : false;

            if(logado)
                return View(db.Cursos.OrderBy(x => x.Aprovado).ThenBy(x => x.Data).ToList());
            else
                return RedirectToAction("Login", "Account");
        }

        public ActionResult Create()
        {
            var categorias = db.Categorias.OrderBy(x => x.Nome).ToList();

            var cursoViewModel = new CursoViewModel(null, categorias);

            return View(cursoViewModel);
        }

        [HttpPost]
        public ActionResult Create(CursoViewModel cursoViewModel, HttpPostedFileBase arquivo)
        {
            cursoViewModel._categorias = db.Categorias.OrderBy(x => x.Nome).ToList();

            if (!ModelState.IsValid) return View(cursoViewModel);

            var urlDaImagem = "";

            if (arquivo != null)
            {
                var pic = Path.GetFileName(arquivo.FileName);
                var path = Path.Combine(Server.MapPath("~/images"), pic);

                // arquivo is uploaded
                arquivo.SaveAs(path);

                urlDaImagem = "/images/" + pic;
            }

            var categoria = db.Categorias.Find(cursoViewModel.CategoriaId);
            var curso = new Curso(cursoViewModel.Id, cursoViewModel.Nome, cursoViewModel.Data, cursoViewModel.Local, categoria, cursoViewModel.Preco, cursoViewModel.Organizador, cursoViewModel.QuantidadeDeInteressados, cursoViewModel.QuantidadeDeParticipantes, cursoViewModel.HoraDeInicio, cursoViewModel.Duracao, cursoViewModel.Descricao, cursoViewModel.Aprovado, urlDaImagem);

            db.Cursos.Add(curso);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Aprovar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var curso = db.Cursos.Find(id);

            curso.Aprovar();

            db.Entry(curso).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var curso = db.Cursos.Find(id);
            var categorias = db.Categorias.OrderBy(x => x.Nome).ToList();

            var cursoViewModel = new CursoViewModel(curso, categorias);

            return View(cursoViewModel);
        }

        public ActionResult Detalhe(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var curso = db.Cursos.Find(id);
            var categorias = db.Categorias.OrderBy(x => x.Nome).ToList();

            curso.AdicionarInteressado();
            db.Entry(curso).State = EntityState.Modified;
            db.SaveChanges();

            var cursoViewModel = new CursoViewModel(curso, categorias);

            return View(cursoViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CursoViewModel cursoViewModel)
        {
            var categorias = db.Categorias.OrderBy(x => x.Nome).ToList();

            cursoViewModel._categorias = categorias;

            if (!ModelState.IsValid) return View(cursoViewModel);

            var categoria = db.Categorias.Find(cursoViewModel.CategoriaId);
            var curso = new Curso(cursoViewModel.Id, cursoViewModel.Nome, cursoViewModel.Data, cursoViewModel.Local, categoria, cursoViewModel.Preco, cursoViewModel.Organizador, cursoViewModel.QuantidadeDeInteressados, cursoViewModel.QuantidadeDeParticipantes, cursoViewModel.HoraDeInicio, cursoViewModel.Duracao, cursoViewModel.Descricao, cursoViewModel.Aprovado, cursoViewModel.UrlDaImagem);

            db.Entry(curso).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Curso curso = db.Cursos.Find(id);
            db.Cursos.Remove(curso);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Participar(int id)
        {
            var curso = db.Cursos.Find(id);
            
            curso.AdicionarParticipante();
            db.Entry(curso).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index","Home");
            //return RedirectToAction("Detalhe", new { id = id });
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
