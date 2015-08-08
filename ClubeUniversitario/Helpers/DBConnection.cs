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
using System.Linq;
using System.Web;
using System.Data.Entity;
using Entidades;
using ClubeUniversitario.Models;

namespace ClubeUniversitario.Helpers
{
    public class DBConnection : System.Data.Entity.DbContext
    {
        public DBConnection() : base("DBConnection") { }
        public DbSet<Promocao> promocaoList { get; set; }
        public DbSet<Parceiro> parceiroList { get; set; }
        public DbSet<CartaoPromocao> CartaoPromocaos { get; set; }

    }
}