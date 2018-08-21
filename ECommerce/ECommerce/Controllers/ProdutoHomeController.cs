using ECommerce.DAL;
using ECommerce.Models;
using ECommerce.Utils;
using System;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class ProdutoHomeController : Controller
    {
        ProdutoDAO produtoDAO = new ProdutoDAO();
        CategoriaDAO categoriaDAO = new CategoriaDAO();
        ItemVendaDAO itemVendaDAO = new ItemVendaDAO();
        public ActionResult Index(int? categoriaID)
        {
            ViewBag.Categorias = categoriaDAO.ListarCategorias();
            if (categoriaID != null) return View(produtoDAO.ListarProdutosPorCategoria(categoriaID));   
            return View(produtoDAO.ListarProdutos());
        }
        public ActionResult DetalhesProduto(int? produtoID)
        {
            return View(produtoDAO.BuscarPorID(produtoID));
        }
        public ActionResult AdicionarAoCarrinho(int produtoID)
        {
            Produto produto = produtoDAO.BuscarPorID(produtoID);
            ItemVenda itemVenda = new ItemVenda
            {
                ProdutoVenda = produto,
                Quantidade = 1,
                PrecoVenda = produto.Preco,
                Data = System.DateTime.Now,
                CarrinhoId = Sessao.RetornarCarrinhoId().ToString()
            };

            itemVendaDAO.Adicionar(itemVenda);
            return RedirectToAction("CarrinhoCompras");
        }
        public ActionResult CarrinhoCompras()
        {
            return View(itemVendaDAO.BuscarItensVendaPorCarrinhoId(Sessao.RetornarCarrinhoId().ToString()));
        }
    }
}