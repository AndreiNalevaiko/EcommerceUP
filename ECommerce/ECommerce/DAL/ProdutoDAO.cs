using ECommerce.Controllers;
using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ECommerce.DAL
{

    public class ProdutoDAO
    {
        private static Context context = Singleton.Instance();
        public bool Adicionar(Produto produto)
        {
            if(BuscarPorNome(produto) == null)
            {
                context.Produtos.Add(produto);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Atualizar(Produto produto)
        {
            context.Entry(produto).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public List<Produto> ListarProdutos()
        {
            return context.Produtos.Include("Categoria").ToList();
        }

        public void Remover(int id)
        {
            Produto produto = BuscarPorID(id);
            context.Produtos.Remove(produto);
            context.SaveChanges();
        }
        
        public Produto BuscarPorID(int? id)
        {
            return context.Produtos.Find(id);
        }

        public Produto BuscarPorNome(Produto produto)
        {
            return context.Produtos.FirstOrDefault(p => p.Nome.Equals(produto.Nome));
        }

        public List<Produto> ListarProdutosPorCategoria(int? categoriaid)
        {
            return context.Produtos.Include("Categoria").Where(produto => produto.Categoria.CategoriaID == categoriaid).ToList();
        }
    }
}