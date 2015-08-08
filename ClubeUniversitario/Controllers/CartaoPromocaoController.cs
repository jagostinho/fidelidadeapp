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
using System.Web.Security;
using Microsoft.AspNet.Identity;


namespace ClubeUniversitario.Controllers
{
    
    public class CartaoPromocaoController : Controller
    {
        private DBConnection db = new DBConnection();

        // GET: CartaoPromocao
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var cartaoPromocaos = db.CartaoPromocaos.Include(c => c.Promocao).Include("Promocao.Parceiro");
            return View(cartaoPromocaos.ToList());
        }

        // GET: CartaoPromocao
        [Authorize(Roles="parceiro")]
        public ActionResult Listar()
        {
            string userID = User.Identity.GetUserId();
            Parceiro parceiro = null;
            parceiro = db.parceiroList.Where(p => p.IdUsuario == userID.ToString()).FirstOrDefault();

            var cartaoPromocaos = db.CartaoPromocaos.Include(c => c.Promocao).Include("Promocao.Parceiro");
            cartaoPromocaos = cartaoPromocaos.Where(cartao => cartao.Promocao.idParceiro == parceiro.idParceiro);

            return View("Index",cartaoPromocaos.ToList());
        }

        // GET: CartaoPromocao/Details/5
        [Authorize(Roles = "admin, parceiro")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartaoPromocao cartaoPromocao = db.CartaoPromocaos.Find(id);
            if (cartaoPromocao == null)
            {
                return HttpNotFound();
            }
            return View(cartaoPromocao);
        }

        // GET: CartaoPromocao/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.idPromocao = new SelectList(db.promocaoList, "idPromocao", "NomeStr");
            return View();
        }

        // POST: CartaoPromocao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCartao,idPromocao,NumeroStr,DataValidadeDate,DataCadastroDate")] CartaoPromocao cartaoPromocao)
        {
            if (ModelState.IsValid)
            {
                db.CartaoPromocaos.Add(cartaoPromocao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idPromocao = new SelectList(db.promocaoList, "idPromocao", "NomeStr", cartaoPromocao.idPromocao);
            return View(cartaoPromocao);
        }

        // GET: CartaoPromocao/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartaoPromocao cartaoPromocao = db.CartaoPromocaos.Find(id);
            if (cartaoPromocao == null)
            {
                return HttpNotFound();
            }
            ViewBag.idPromocao = new SelectList(db.promocaoList, "idPromocao", "NomeStr", cartaoPromocao.idPromocao);
            return View(cartaoPromocao);
        }

        // POST: CartaoPromocao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCartao,idPromocao,NumeroStr,DataValidadeDate,DataCadastroDate")] CartaoPromocao cartaoPromocao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartaoPromocao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idPromocao = new SelectList(db.promocaoList, "idPromocao", "NomeStr", cartaoPromocao.idPromocao);
            return View(cartaoPromocao);
        }

        // GET: CartaoPromocao/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartaoPromocao cartaoPromocao = db.CartaoPromocaos.Find(id);
            if (cartaoPromocao == null)
            {
                return HttpNotFound();
            }
            return View(cartaoPromocao);
        }

        // POST: CartaoPromocao/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartaoPromocao cartaoPromocao = db.CartaoPromocaos.Find(id);
            db.CartaoPromocaos.Remove(cartaoPromocao);
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
