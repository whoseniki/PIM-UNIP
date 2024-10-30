using DsiVendas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DsiVendas.Controllers;

public class VendasController(ApplicationDbContext context) : Controller
    {
         public IActionResult Index()
    {
         var listaVendas = context.Vendas.Include(v => v.Cliente).ToList();
         return View(listaVendas);
    }
        // GET: Criação de Venda
        public IActionResult Criar()
        {
            var ListaFormaPagamento = new List<string>();
            ListaFormaPagamento.Add("Cartão de Débito");
            ListaFormaPagamento.Add("Cartão de Crédito");
            ListaFormaPagamento.Add("Boleto");
            ListaFormaPagamento.Add("PIX");
            ViewBag.Clientes = new SelectList(context.Clientes, "Id", "Nome");
            ViewBag.Produtos = new SelectList(context.Produtos, "Id", "Nome");
            ViewBag.FormaPagamentos = new SelectList(ListaFormaPagamento);
            return View();
        }

        [HttpGet]
        public JsonResult GetPrecoProduto(int idProduto)
        {
            var produto = context.Produtos.FirstOrDefault(p => p.Id == idProduto);
            if (produto != null)
            {
                return Json(produto.Preco);
            }
            return Json(0);
        }

        // POST: Salvar a Venda e seus itens
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Venda venda, List<ItemVenda> itensVenda)
        {
            context.Add(venda);
            await context.SaveChangesAsync();
            foreach (var item in itensVenda)
            {
                item.VendaId = venda.Id;
                item.PrecoUnitario = context.Produtos.Find(item.ProdutoId).Preco;
                context.ItemsVenda.Add(item);
            }
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


            ViewBag.Clientes = new SelectList(context.Clientes, "Id", "Nome", venda.Id);
            ViewBag.Produtos = new SelectList(context.Produtos, "Id", "Nome");
            return View(venda);
        }

        public IActionResult Editar(int id)
        {
         var venda = context.Vendas.Find(id);
         if (venda == null)
         {
           return NotFound();
         }
         return View(venda);
        }

        [HttpPost]
        public IActionResult Editar(Venda venda)
        {
            var vendaExistente = context.Vendas.Find(venda.Id);
            if (vendaExistente == null)
            {
              return NotFound();
            }
            vendaExistente.Cliente = venda.Cliente;
            vendaExistente.DataVenda = venda.DataVenda;
            vendaExistente.FormaPagamento = venda.FormaPagamento;
            context.Update(vendaExistente); 
            context.SaveChanges(); 
            return RedirectToAction("Index");
            
        }

        [HttpPost]
        public async Task<IActionResult> Remover(int id)   
        {
          var venda = await context.Vendas.FindAsync(id);
          if (venda == null)
          {
            return NotFound();
          }

          context.Vendas.Remove(venda);
          await context.SaveChangesAsync();
          return RedirectToAction(nameof(Index));
        }
    }
    
    

    