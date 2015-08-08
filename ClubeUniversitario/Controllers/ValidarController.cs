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
using ClubeUniversitario.Helpers;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ClubeUniversitario.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;



namespace ClubeUniversitario.Controllers
{
    public class ValidarController : Controller
    {
        private DBConnection db = new DBConnection();
        // GET: Validar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cartao()
        {
            return View();
        }

        [Authorize(Roles="parceiro,admin")]
        public ActionResult Parceiro(int? id)
        {
            string userID = User.Identity.GetUserId();
            Parceiro parceiro = null;
            if (User.IsInRole("admin") && id != null && id.HasValue)
                parceiro = db.parceiroList.Find(id);
            else
                parceiro = db.parceiroList.Where(p => p.IdUsuario == userID.ToString()).FirstOrDefault();
            if (parceiro == null) throw new ArgumentNullException("erro","Não foi possível carregar o parceiro selecionado. Verifique a URL digitada");
            var listaPromocoes = db.promocaoList.Where(p => p.idParceiro == parceiro.idParceiro && p.AtivaBool == true).ToList();
            ViewBag.ListaPromocao = listaPromocoes;
            if (parceiro != null) return View(parceiro);
 
            return Redirect("/");
        }

        [Authorize(Roles = "parceiro,admin")]
        public ActionResult Promocao(int? id)
        {
            if (id.HasValue)
            {
                Promocao promocao = null;
                    
                if(User.IsInRole("admin"))
                    promocao = db.promocaoList.Find(id);
                else
                {
                    string userID = User.Identity.GetUserId();
                    Parceiro parceiro = null;
                    parceiro = db.parceiroList.Where(p => p.IdUsuario == userID.ToString()).First();
                    if (parceiro == null) throw new ArgumentNullException("erro", "Não foi possível carregar o parceiro selecionado. Verifique a URL digitada");
                    promocao = db.promocaoList.Where(p => p.idParceiro == parceiro.idParceiro && p.AtivaBool == true && p.idPromocao == id).FirstOrDefault();
                    if (promocao == null) throw new ArgumentNullException("erro", "Não foi possível carregar o parceiro selecionado. Verifique a URL digitada");
                }

                if (promocao != null && !String.IsNullOrEmpty(promocao.ImagemStr))
                    ViewBag.ImagemStr = promocao.ImagemStr;

                var cartaoPromocao = new CartaoPromocao();

                cartaoPromocao.idPromocao = promocao.idPromocao;

                if (cartaoPromocao != null)
                    return View(cartaoPromocao);
                else
                    throw new ArgumentNullException("cartaoPromocao", "Erro ao validar a promoção.");
            }
            else throw new ArgumentNullException("erro", "Não foi possivel carregar a promoção, verifique a url digitada");

            return View();
        }

        [Authorize(Roles = "parceiro,admin")]
        public ActionResult ValidarPromocao(CartaoPromocao cartaoPromocao)
        {

            if (cartaoPromocao != null)
            {
                cartaoPromocao.DataCadastroDate = AppHelper.GetCurrentTime();
                var promocao = db.promocaoList.FirstOrDefault(p => p.idPromocao == cartaoPromocao.idPromocao);
                int qtdUtilizacaoDia = promocao.QtdUtilizacaoDiaInt;
                var listaCartaoUtilizadosHoje = db.CartaoPromocaos
                                                .ToList()
                                                .FindAll(cartao => cartao.NumeroStr == cartaoPromocao.NumeroStr 
                                                    && cartao.idPromocao == promocao.idPromocao
                                                    && cartao.DataCadastroDate.Date == AppHelper.GetCurrentTime(DateTime.Today.Date));

                if (listaCartaoUtilizadosHoje.Count >= qtdUtilizacaoDia)
                {
                    ViewBag.ImagemStr = promocao.ImagemStr;
                    ViewBag.Erro = "Este cartão atingiu o limite de utilizações diárias ";
                    return View("Promocao", cartaoPromocao);
                }
                               
                if (promocao != null)
                {
                    ViewBag.IdParceiro = promocao.idParceiro;
                    ViewBag.IdPromocao = promocao.idPromocao;
                }
                db.CartaoPromocaos.Add(cartaoPromocao);
                db.SaveChanges();
            }
            return View();
        }
    }
}