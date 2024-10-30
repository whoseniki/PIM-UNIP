using Microsoft.AspNetCore.Mvc;
using DsiVendas.Models;

namespace DsiVendas.Controllers;

public class ClientesController(ApplicationDbContext context) : Controller
{
  public IActionResult Index()
  {
    var listaClientes = context.Clientes.ToList();
    return View(listaClientes);
  }

  public List<Cliente> Api()
  {
    var listaClientes = context.Clientes.ToList();
    return listaClientes;
  }

  public IActionResult Criar()
  {
    return View();
  }

  [HttpPost]
  public IActionResult Criar(Cliente cliente)
  {
    context.Clientes.Add(cliente);
    context.SaveChanges();  
    
    return RedirectToAction("Index");
  }

  public IActionResult Editar(int id)
  {
    var cliente = context.Clientes.Find(id);
    if (cliente == null)
    {
      return NotFound();
    }
    return View(cliente);
  }

  [HttpPost]
  public IActionResult Editar(Cliente cliente)
  {
      var clienteExistente = context.Clientes.Find(cliente.Id); 
      if (clienteExistente == null)
      {
        return NotFound();
      }
      clienteExistente.Nome = cliente.Nome;
      clienteExistente.Email = cliente.Email;
      clienteExistente.Telefone = cliente.Telefone;
      context.Clientes.Update(clienteExistente); 
      context.SaveChanges(); 
      return RedirectToAction("Index");

  }

  [HttpPost]
  public async Task<IActionResult> Remover(int id)   
  {
    var cliente = await context.Clientes.FindAsync(id);
    if (cliente == null)
    {
        return NotFound();
    }

    context.Clientes.Remove(cliente);
    await context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
  }
}
