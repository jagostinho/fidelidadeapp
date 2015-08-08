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

namespace ClubeUniversitario.Controllers
{
    [Authorize(Roles = "admin")]
    public class PromocaoController : Controller
    {
        public const string imagePath = "~/Content/images/promocoes";
        private DBConnection db = new DBConnection();

        // GET: Promocao
        public ActionResult Index()
        {
            var promocaoList = db.promocaoList.Include(p => p.Parceiro);
            return View(promocaoList.ToList());
        }

        // GET: Promocao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promocao promocao = db.promocaoList.Find(id);
            if (promocao == null)
            {
                return HttpNotFound();
            }
            promocao.Parceiro = db.parceiroList.First(model => model.idParceiro == promocao.idParceiro);
            return View(promocao);
        }

        // GET: Promocao/Create
        public ActionResult Create()
        {
            ViewBag.idParceiro = new SelectList(db.parceiroList, "idParceiro", "nomeStr");
            return View();
        }

        // POST: Promocao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "idPromocao,idParceiro,NomeStr,ImagemStr,DescricaoStr,QtdUtilizacaoDiaInt,AtivaBool,DataCadastroDate,DataValidadeDate")] Promocao promocao, HttpPostedFileBase upload)
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
                    promocao.ImagemStr = fileName;
                    db.promocaoList.Add(promocao);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ViewBag.Error = "Por favor, selecione uma imagem para a promoção.";
                ModelState.AddModelError("ExtensaoInvalida", ViewBag.Error);
            }

            ViewBag.idParceiro = new SelectList(db.parceiroList, "idParceiro", "nomeStr", promocao.idParceiro);
            return View(promocao);
        }

        // GET: Promocao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promocao promocao = db.promocaoList.Find(id);
            if (promocao == null)
            {
                return HttpNotFound();
            }
            ViewBag.idParceiro = new SelectList(db.parceiroList, "idParceiro", "nomeStr", promocao.idParceiro);
            return View(promocao);
        }

        // POST: Promocao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idPromocao,idParceiro,NomeStr,ImagemStr,DescricaoStr,QtdUtilizacaoDiaInt,AtivaBool,DataCadastroDate,DataValidadeDate")] Promocao promocao, HttpPostedFileBase upload)
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
                    promocao.ImagemStr = fileName;
                }

                db.Entry(promocao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idParceiro = new SelectList(db.parceiroList, "idParceiro", "nomeStr", promocao.idParceiro);
            return View(promocao);
        }

        // GET: Promocao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promocao promocao = db.promocaoList.Find(id);
            if (promocao == null)
            {
                return HttpNotFound();
            }
            promocao.Parceiro = db.parceiroList.First(model => model.idParceiro == promocao.idParceiro);
            return View(promocao);
        }

        // POST: Promocao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Promocao promocao = db.promocaoList.Find(id);
            db.promocaoList.Remove(promocao);
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
