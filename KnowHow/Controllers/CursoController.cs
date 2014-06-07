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
            return View(db.Cursos.ToList());
        }

        public ActionResult Create()
        {
            var categorias = db.Categorias.ToList();

            var cursoViewModel = new CursoViewModel(null, categorias );

            return View(cursoViewModel);
        }

        [HttpPost]
        public ActionResult Create(CursoViewModel cursoViewModel, HttpPostedFileBase arquivo)
        {
            cursoViewModel._categorias = db.Categorias.ToList();

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
            var curso = new Curso(cursoViewModel.Id, cursoViewModel.Nome, cursoViewModel.Data, cursoViewModel.Local, categoria, cursoViewModel.Preco, cursoViewModel.Organizador, cursoViewModel.QuantidadeDeInteressados, cursoViewModel.HoraDeInicio, cursoViewModel.Duracao, cursoViewModel.Descricao, cursoViewModel.Aprovado, urlDaImagem);

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
            var categorias = db.Categorias.ToList();

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
            var categorias = db.Categorias.ToList();

            curso.AdicionarInteressado();
            db.Entry(curso).State = EntityState.Modified;
            db.SaveChanges();

            var cursoViewModel = new CursoViewModel(curso, categorias);

            return View(cursoViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CursoViewModel cursoViewModel)
        {
            if (ModelState.IsValid)
            {
                var categoria = db.Categorias.Find(cursoViewModel.CategoriaId);
                var curso = new Curso(cursoViewModel.Id, cursoViewModel.Nome, cursoViewModel.Data, cursoViewModel.Local, categoria, cursoViewModel.Preco, cursoViewModel.Organizador, cursoViewModel.QuantidadeDeInteressados,cursoViewModel.HoraDeInicio,cursoViewModel.Duracao,cursoViewModel.Descricao,cursoViewModel.Aprovado);

                db.Entry(curso).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(cursoViewModel);
        }

        public ActionResult Delete(int id)
        {
            Curso curso = db.Cursos.Find(id);
            db.Cursos.Remove(curso);
            db.SaveChanges();
            return RedirectToAction("Index");
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
