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
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Table("tb_cartao_promocao")]
    public class CartaoPromocao
    {
        [Key]
        [Column("id_cartao")]
        public int idCartao { get; set; }

        [ForeignKey("Promocao")]
        [Column("id_promocao")]
        [Display(Name = "Promoção")]
        public int idPromocao { get; set; }
        public Promocao Promocao { get; set; }

        [Column("numero_str")]
        [Display(Name = "Numero do Cartão")]
        [Required]
        public string NumeroStr { get; set; }

        [Display(Name = "Data de Validade")]
        [Required]
        [Column("data_validade_date")]
        public DateTime DataValidadeDate { get; set; }

        [Column("data_ativacao_date")]
        [Display(Name = "Data de utilização")]
        public DateTime DataCadastroDate { get; set; }

        [Display(Name = "Data de Validade")]
        public DateTime DataValidadeBr
        {
            get { return AppHelper.GetCurrentTime(this.DataValidadeDate); }
        }

        [Display(Name = "Data de utilização")]
        public DateTime DataCadastroBr
        {
            get { return AppHelper.GetCurrentTime(this.DataCadastroDate); }
        }

    }
}
