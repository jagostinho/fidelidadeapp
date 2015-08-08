/*  
	This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
	Copyright Bruno Assis 2015 <bruno.assis@live.com>
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClubeUniversitario.Helpers;
using Entidades;
using System.IO;
using MySql.AspNet.Identity;

namespace ClubeUniversitario.Controllers
{
    [Authorize(Roles="admin")]
    public class ParceirosController : Controller
    {
        private const string imagePath = "~/Content/images/parceiros";
        private DBConnection db;

        public ParceirosController()
        {
            db = new DBConnection();
        }

        // GET: Parceiros
        public ActionResult Listar()
        {
            IEnumerable<Parceiro> listaParceiros = db.parceiroList.ToList();
            return View("Index", listaParceiros);
        }

        // GET: Parceiros
        public ActionResult Index()
        {
            return this.Listar();
        }

        // GET: Parceiros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parceiro parceiro = db.parceiroList.Find(id);
            if (parceiro == null)
            {
                return HttpNotFound();
            }
            return View(parceiro);
        }

        // GET: Parceiros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Parceiros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idParceiro,nomeStr,imagemStr,AtivoBool,DataCadastroDate")] Parceiro parceiro, HttpPostedFileBase upload)
        {

            if (upload != null)
            {
                if (!AppHelper.isValidExtension(upload))
                {
                    ViewBag.Error = "A extensão do arquivo não é válida, extensões válidas: jpg, jpeg, png e gif";
                    ModelState.AddModelError("ExtensaoInvalida", ViewBag.Error);
                }
                if (ModelState.IsValid)
                {
                    string fileName = String.Empty;
                    //Salva o arquivo 
                    if (upload.ContentLength > 0)
                    {
                        var uploadPath = Server.MapPath(imagePath);
                        fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(upload.FileName);
                        string caminhoArquivo = Path.Combine(@uploadPath, fileName);
                        upload.SaveAs(caminhoArquivo);
                    }
                    parceiro.imagemStr = fileName;
                    db.parceiroList.Add(parceiro);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            
            return View(parceiro);
        }

        // GET: Parceiros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parceiro parceiro = db.parceiroList.Find(id);
            if (parceiro == null)
            {
                return HttpNotFound();
            }
            return View(parceiro);
        }

        // POST: Parceiros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idParceiro,imagemStr,nomeStr,AtivoBool")] Parceiro parceiro, HttpPostedFileBase upload)
        {
            //Ok ele atualizou a imagem, verificamos se a extensão é válida.
            if (upload != null)
            {
                if (!AppHelper.isValidExtension(upload))
                {
                    ViewBag.Error = "A extensão do arquivo não é válida, extensões válidas: jpg, jpeg, png e gif";
                    ModelState.AddModelError("ExtensaoInvalida", ViewBag.Error);
                }
            }
            if (ModelState.IsValid)
            {
                //Ok ele atualizou a imagem, então atualizamos o modelo
                db.Entry(parceiro).State = EntityState.Modified;
                if (upload != null)
                {
                    string fileName = String.Empty;
                    //Salva o arquivo 
                    if (upload.ContentLength > 0)
                    {
                        var uploadPath = Server.MapPath(imagePath);
                        fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(upload.FileName);
                        string caminhoArquivo = Path.Combine(@uploadPath, fileName);
                        upload.SaveAs(caminhoArquivo);
                    }
                    parceiro.imagemStr = fileName;
                }
                else
                {
                    var parceiroAux = db.parceiroList.Find(parceiro.idParceiro);
                    string nomeImagem = (parceiroAux != null && parceiroAux.imagemStr != null) ? parceiroAux.imagemStr : String.Empty;
                    parceiro.imagemStr = nomeImagem;
                }
                db.Entry(parceiro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parceiro);
        }

        // GET: Parceiros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parceiro parceiro = db.parceiroList.Find(id);
            if (parceiro == null)
            {
                return HttpNotFound();
            }
            return View(parceiro);
        }

        // POST: Parceiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Parceiro parceiro = db.parceiroList.Find(id);
            db.parceiroList.Remove(parceiro);
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
