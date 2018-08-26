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

        ItemVendaDAO ItemVendaDAO = new ItemVendaDAO();

        public ActionResult AdicionarAoCarrinho(int? produtoID)
        {

            Produto produto = produtoDAO.BuscarPorID(produtoID);
            ItemVenda itemVenda = itemVendaDAO.ItemCarrinho(produto);

            if (itemVenda == null)
            {
                {
                    itemVenda = new ItemVenda();
                    itemVenda.ProdutoVenda = produto;
                    itemVenda.Quantidade = 1;
                    itemVenda.PrecoVenda = produto.Preco;
                    itemVenda.Data = DateTime.Now;
                    itemVenda.CarrinhoId = Sessao.RetornarCarrinhoId().ToString();
                    ItemVendaDAO.Adicionar(itemVenda);
                };
            }
            else
            {
                itemVenda.Quantidade++;
                itemVendaDAO.Atualizar(itemVenda);
            }
            return RedirectToAction("CarrinhoCompras");
        }

        public ActionResult CarrinhoCompras()
        {
            return View(itemVendaDAO.BuscarItensVendaPorCarrinhoId(Sessao.RetornarCarrinhoId().ToString()));
        }

        public ActionResult Remover(int? id)
        {
            ItemVenda item = itemVendaDAO.BuscarPorID(id);

            if(item != null)
            {
                if(item.Quantidade == 1)
                {
                    itemVendaDAO.Remover(id);
                }
                else
                {
                    item.Quantidade--;
                    itemVendaDAO.Atualizar(item);
                }
                
            }

            return RedirectToAction("CarrinhoCompras");
        }

        public ActionResult Decrementar(int? id)
        {
            ItemVenda item = itemVendaDAO.BuscarPorID(id);

            if (item != null && item.Quantidade > 1)
            {
                item.Quantidade--;
                itemVendaDAO.Atualizar(item);

            }

            return RedirectToAction("CarrinhoCompras");
        }

        public ActionResult Incrementar(int? id)
        {
            ItemVenda item = itemVendaDAO.BuscarPorID(id);

            if(item == null)
            {
                return RedirectToAction("CarrinhoCompras");
            }
            else
            {
                item.Quantidade++;
                itemVendaDAO.Atualizar(item);
            }

            return RedirectToAction("CarrinhoCompras");
        }
    }
}