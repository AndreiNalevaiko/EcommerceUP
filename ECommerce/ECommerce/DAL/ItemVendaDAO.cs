using ECommerce.Controllers;
using ECommerce.Models;
using ECommerce.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.DAL
{
    public class ItemVendaDAO
    {
        private static Context context = Singleton.Instance();

        public bool Adicionar(ItemVenda item)
        {
            if(item != null)
            {
                context.ItensVenda.Add(item);
                context.SaveChanges();
                return true;
            }
            return false;
        }
        public void Remover(int? id)
        {
            context.ItensVenda.Remove(BuscarPorID(id));
            context.SaveChanges();
        }

        public ItemVenda BuscarPorID(int? id)
        {
            return context.ItensVenda.Find(id);
        }

        /*public List<ItemVenda> BuscarItensVendaPorCarrinhoId(string carrinhoId)
        {
            return context.ItensVenda.Include("ProdutoVenda").Where(x => x.CarrinhoId.Equals(carrinhoId)).ToList();
        }*/

        public static List<ItemVenda> BuscarItensVendaPorCarrinhoId()
        {
            string carrinhoId = Sessao.RetornarCarrinhoId();
            return context.ItensVenda.Include("ProdutoVenda").Where(x => x.CarrinhoId.Equals(carrinhoId)).ToList();
        }

        public ItemVenda ItemCarrinho(Produto produto)
        {
            string carrinhoId = Sessao.RetornarCarrinhoId().ToString();
            return context.ItensVenda.Include("ProdutoVenda").FirstOrDefault(x => x.ProdutoVenda.ProdutoID == produto.ProdutoID && x.CarrinhoId.Equals(carrinhoId));
        }

        public void Atualizar(ItemVenda item)
        {
            if (item != null)
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void RemoverItem(int id)
        {
            ItemVenda item = context.ItensVenda.Find(id);

            if(item.Quantidade > 1)
            {
                item.Quantidade--;
            } 
            else
            {
                context.ItensVenda.Remove(item);
                context.SaveChanges();
            }

        }

        public static void Incrementar(int id)
        {
            ItemVenda item = context.ItensVenda.Find(id);
            item.Quantidade++;
            context.SaveChanges();
        }

        public static void Decrementar(int id)
        {
            ItemVenda item = context.ItensVenda.Find(id);
            if(item.Quantidade > 1)
            {
                item.Quantidade--;
                context.SaveChanges();
            }
        }

        public static double RetornarTotalDoCarrinho()
        {
            return BuscarItensVendaPorCarrinhoId().Sum(x => x.Quantidade * x.PrecoVenda);
        }

        public static int RetornarQuantidadeDoMenu()
        {
            return BuscarItensVendaPorCarrinhoId().Sum(x => x.Quantidade);
        }


    }
}