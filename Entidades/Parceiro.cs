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
using Entidades.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Entidades
{
    [Table("tb_parceiro")]
    public class Parceiro
    {
        
        [Key]
        [Column("id_parceiro")]
        public int idParceiro { get; set; }

        [Column("nome_str")]
        [Display(Name="Nome")]
        public string nomeStr { get; set; }
        [Column("imagem_str")]
        [Display(Name="Imagem")]
        public string imagemStr { get; set; }
        [Column("ativo_bool")]
        [Display(Name = "Ativo")]
        public bool AtivoBool { get; set; }
        [Column("data_cadastro_date")]
        [Display(Name = "Data de cadastro")]
        public DateTime DataCadastroDate { get; set; }
        
         /* Not the best way to solve, but the fastest */
        [Column("id_usuario")]
        [Display(Name = "Usuário")]
        public string IdUsuario { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime DataCadastroBr
        {
            get { return AppHelper.GetCurrentTime(this.DataCadastroDate); }
        }

        public virtual ICollection<Promocao> listaPromocao { get; set; }
       
    }
}