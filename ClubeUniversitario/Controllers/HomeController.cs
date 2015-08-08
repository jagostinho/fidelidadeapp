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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClubeUniversitario.Controllers
{
    public class HomeController : Controller
    {
        DBConnection db = new DBConnection();

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole("admin"))
                    return Redirect("~/Parceiros");
                else
                    return Redirect("~/Validar/Parceiro");
            }
            
            return View();
        }

        public ActionResult Sobre()
        {
            return View();
        }

        public ActionResult Contato()
        {
            return View();
        }
    }
}