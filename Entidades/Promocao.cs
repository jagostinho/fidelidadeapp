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
    [Table("tb_promocao")]
    public class Promocao
    {

        [Key]
        [Column("id_promocao")]
        public int idPromocao { get; set; }

        [ForeignKey("Parceiro")]
        [Column("id_parceiro")]
        [Display(Name = "Parceiro")]
        public int idParceiro { get; set; }
        public Parceiro Parceiro { get; set; }

        [Column("nome_str")]
        [Display(Name="Nome da Promoção")]
        public string NomeStr { get; set; }
        [Column("imagem_str")]
        [Display(Name = "Imagem")]
        public string ImagemStr   { get; set; }
        [Column("descricao_str")]
        [Display(Name = "Descrição")]
        public string DescricaoStr { get; set; }
        [Column("qtd_utilizacao_dia_int")]
        [Display(Name = "Qtd. Max Utilização/Dia")]
        public int QtdUtilizacaoDiaInt { get; set; }
        [Column("ativa_bool")]
        [Display(Name = "Está Ativa?")]
        public bool AtivaBool { get; set; }

        [Column("data_cadastro_date")]
        [Display(Name = "Data de cadastro")]
        public DateTime DataCadastroDate { get; set; }

        [Display(Name = "Data de Validade")]
        [Column("data_validade_date")]
        public DateTime DataValidadeDate { get; set; }

        [Display(Name = "Data de Validade")]
        public DateTime DataValidadeBr
        {
            get { return AppHelper.GetCurrentTime(this.DataValidadeDate); }
        }

        [Display(Name = "Data de cadastro")]
        public DateTime DataCadastroBr
        {
            get { return AppHelper.GetCurrentTime(this.DataCadastroDate); }
        }

        public virtual ICollection<CartaoPromocao> listaCartao { get; set; }

    }

}