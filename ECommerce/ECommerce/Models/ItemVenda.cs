using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    [Table("ItemVenda")]
    public class ItemVenda
    {
        [Key]
        public int ItemVendaID { get; set; }
        public Produto ProdutoVenda { get; set; }
        public int Quantidade { get; set; }
        public double PrecoVenda { get; set; }
        public DateTime Data { get; set; }
        public string CarrinhoId { get; set; }
    }
}