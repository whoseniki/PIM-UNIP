using Microsoft.AspNetCore.Mvc;
using DsiVendas.Models;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;

namespace DsiVendas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : Controller
    {
        private readonly ApplicationDbContext context;

        public ApiController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("GetClientes")]
        public List<Cliente> GetClientes()
        {
            var listaClientes = context.Clientes.ToList();
            return listaClientes;
        }

        [HttpPost("CreateCliente")]
        public IActionResult CreateCliente([FromBody] Cliente cliente)
        {
            var clientedb = context.Clientes.Add(cliente);
            context.SaveChanges();
            return CreatedAtAction(nameof(CreateCliente), new { id = clientedb.Entity.Id }, clientedb.Entity);
        }

        [HttpPut("TestePut")]
        public IActionResult TestePut()
        {
            // Apenas retorna uma mensagem de sucesso para teste
            return Ok("Rota PUT funcionando corretamente!");
        }

        [HttpPut("UpdateCliente/{id}")]
        public IActionResult UpdateCliente(int id, [FromBody] Cliente cliente)
        {
            // Verifica se o ID passado na URL é o mesmo do cliente no corpo
            if (id != cliente.Id)
            {
                return BadRequest("ID do cliente na URL e no corpo não coincidem.");
            }

            var clientedb = context.Clientes.Find(id);
            if (clientedb == null)
            {
                return NotFound();
            }

            clientedb.Nome = cliente.Nome;
            clientedb.Email = cliente.Email;
            clientedb.Telefone = cliente.Telefone;

            context.Clientes.Update(clientedb);
            context.SaveChanges();
            return NoContent();
        }
        // GET: api/Fornecedores/GetFornecedores
        [HttpGet("GetFornecedores")]
        public List<Fornecedor> GetFornecedores()
        {
            var listaFornecedores = context.Fornecedores.ToList();
            return listaFornecedores;
        }

        // POST: api/Fornecedores/CreateFornecedor
        [HttpPost("CreateFornecedor")]
        public IActionResult CreateFornecedor([FromBody] Fornecedor fornecedor)
        {
            var fornecedorDb = context.Fornecedores.Add(fornecedor);
            context.SaveChanges();
            return CreatedAtAction(nameof(CreateFornecedor), new { id = fornecedorDb.Entity.Id }, fornecedorDb.Entity);
        }

        // PUT: api/Fornecedores/UpdateFornecedor/{id}
        [HttpPut("UpdateFornecedor/{id}")]
        public IActionResult UpdateFornecedor(int id, [FromBody] Fornecedor fornecedor)
        {
            // Verifica se o ID passado na URL é o mesmo do fornecedor no corpo
            if (id != fornecedor.Id)
            {
                return BadRequest("ID do fornecedor na URL e no corpo não coincidem.");
            }

            var fornecedorDb = context.Fornecedores.Find(id);
            if (fornecedorDb == null)
            {
                return NotFound();
            }

            fornecedorDb.CNPJ = fornecedor.CNPJ;
            fornecedorDb.Cidade = fornecedor.Cidade;
            fornecedorDb.Telefone = fornecedor.Telefone;

            context.Fornecedores.Update(fornecedorDb);
            context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Fornecedores/DeleteFornecedor/{id}
        [HttpDelete("DeleteFornecedor/{id}")]
        public IActionResult DeleteFornecedor(int id)
        {
            var fornecedor = context.Fornecedores.Find(id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            context.Fornecedores.Remove(fornecedor);
            context.SaveChanges();
            return NoContent();
        }
         // GET: api/Vendas
        [HttpGet("GetVendas")]
        public ActionResult<List<Venda>> GetVendas()
        {
            var listaVendas = context.Vendas
                .Include(v => v.Cliente)
                .Include(v => v.ItensVenda)
                .ToList();
            return Ok(listaVendas);
        }

        // GET: api/Vendas/{id}
        [HttpGet("GetVenda/{id}")]
        public ActionResult<Venda> GetVenda(int id)
        {
            var venda = context.Vendas
                .Include(v => v.Cliente)
                .Include(v => v.ItensVenda)
                .FirstOrDefault(v => v.Id == id);

            if (venda == null)
            {
                return NotFound();
            }

            return Ok(venda);
        }

        // POST: api/Vendas/CreateVenda
        [HttpPost("CreateVenda")]
        public IActionResult CreateVenda([FromBody] Venda venda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Vendas.Add(venda);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetVenda), new { id = venda.Id }, venda);
        }

        // PUT: api/Vendas/UpdateVenda/{id}
        [HttpPut("UpdateVenda/{id}")]
        public IActionResult UpdateVenda(int id, [FromBody] Venda venda)
        {
            if (id != venda.Id)
            {
                return BadRequest("ID da venda na URL e no corpo não coincidem.");
            }

            var vendaExistente = context.Vendas
                .Include(v => v.ItensVenda)
                .FirstOrDefault(v => v.Id == id);

            if (vendaExistente == null)
            {
                return NotFound();
            }

            vendaExistente.ClienteId = venda.ClienteId;
            vendaExistente.DataVenda = venda.DataVenda;
            vendaExistente.FormaPagamento = venda.FormaPagamento;
            vendaExistente.ItensVenda = venda.ItensVenda;

            context.Vendas.Update(vendaExistente);
            context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Vendas/DeleteVenda/{id}
        [HttpDelete("DeleteVenda/{id}")]
        public IActionResult DeleteVenda(int id)
        {
            var venda = context.Vendas.Find(id);
            if (venda == null)
            {
                return NotFound();
            }

            context.Vendas.Remove(venda);
            context.SaveChanges();
            return NoContent();
        }
        // GET: api/Fazendas/GetFazendas
        [HttpGet("GetFazendas")]
        public ActionResult<List<Fazenda>> GetFazendas()
        {
            var listaFazendas = context.Fazendas
                .Include(f => f.Plantio)
                .Include(f => f.AreaPlantio)
                .ToList();
            return Ok(listaFazendas);
        }

        // GET: api/Fazendas/GetFazenda/{id}
        [HttpGet("GetFazenda/{id}")]
        public ActionResult<Fazenda> GetFazenda(int id)
        {
            var fazenda = context.Fazendas
                .Include(f => f.Plantio)
                .Include(f => f.AreaPlantio)
                .FirstOrDefault(f => f.Id == id);

            if (fazenda == null)
            {
                return NotFound();
            }

            return Ok(fazenda);
        }

        // POST: api/Fazendas/CreateFazenda
        [HttpPost("CreateFazenda")]
        public IActionResult CreateFazenda([FromBody] Fazenda fazenda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Fazendas.Add(fazenda);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetFazenda), new { id = fazenda.Id }, fazenda);
        }

        // PUT: api/Fazendas/UpdateFazenda/{id}
        [HttpPut("UpdateFazenda/{id}")]
        public IActionResult UpdateFazenda(int id, [FromBody] Fazenda fazenda)
        {
            if (id != fazenda.Id)
            {
                return BadRequest("ID da fazenda na URL e no corpo não coincidem.");
            }

            var fazendaExistente = context.Fazendas
                .Include(f => f.Plantio)
                .Include(f => f.AreaPlantio)
                .FirstOrDefault(f => f.Id == id);

            if (fazendaExistente == null)
            {
                return NotFound();
            }

            fazendaExistente.Nome = fazenda.Nome;
            fazendaExistente.Hectares = fazenda.Hectares;
            fazendaExistente.Endereco = fazenda.Endereco;
            fazendaExistente.Telefone = fazenda.Telefone;
            fazendaExistente.Email = fazenda.Email;
            fazendaExistente.PlantioId = fazenda.PlantioId;
            fazendaExistente.AreaId = fazenda.AreaId;

            context.Fazendas.Update(fazendaExistente);
            context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Fazendas/DeleteFazenda/{id}
        [HttpDelete("DeleteFazenda/{id}")]
        public IActionResult DeleteFazenda(int id)
        {
            var fazenda = context.Fazendas.Find(id);
            if (fazenda == null)
            {
                return NotFound();
            }

            context.Fazendas.Remove(fazenda);
            context.SaveChanges();
            return NoContent();
        }
        // GET: api/Recursos/GetRecursos
        [HttpGet("GetRecursos")]
        public ActionResult<List<Recurso>> GetRecursos()
        {
            var listaRecursos = context.Recursos
                .Include(r => r.Fornecedor)
                .ToList();
            return Ok(listaRecursos);
        }

        // GET: api/Recursos/GetRecurso/{id}
        [HttpGet("GetRecurso/{id}")]
        public ActionResult<Recurso> GetRecurso(int id)
        {
            var recurso = context.Recursos
                .Include(r => r.Fornecedor)
                .FirstOrDefault(r => r.Id == id);

            if (recurso == null)
            {
                return NotFound();
            }

            return Ok(recurso);
        }

        // POST: api/Recursos/CreateRecurso
        [HttpPost("CreateRecurso")]
        public IActionResult CreateRecurso([FromBody] Recurso recurso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Recursos.Add(recurso);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetRecurso), new { id = recurso.Id }, recurso);
        }

        // PUT: api/Recursos/UpdateRecurso/{id}
        [HttpPut("UpdateRecurso/{id}")]
        public IActionResult UpdateRecurso(int id, [FromBody] Recurso recurso)
        {
            if (id != recurso.Id)
            {
                return BadRequest("ID do recurso na URL e no corpo não coincidem.");
            }

            var recursoExistente = context.Recursos
                .Include(r => r.Fornecedor)
                .FirstOrDefault(r => r.Id == id);

            if (recursoExistente == null)
            {
                return NotFound();
            }

            recursoExistente.Nome = recurso.Nome;
            recursoExistente.Tipo = recurso.Tipo;
            recursoExistente.Preco = recurso.Preco;
            recursoExistente.Quantidade = recurso.Quantidade;
            recursoExistente.FornecedorId = recurso.FornecedorId;
            recursoExistente.NumeroSerie = recurso.NumeroSerie;
            recursoExistente.DataAquisicao = recurso.DataAquisicao;

            context.Recursos.Update(recursoExistente);
            context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Recursos/DeleteRecurso/{id}
        [HttpDelete("DeleteRecurso/{id}")]
        public IActionResult DeleteRecurso(int id)
        {
            var recurso = context.Recursos.Find(id);
            if (recurso == null)
            {
                return NotFound();
            }

            context.Recursos.Remove(recurso);
            context.SaveChanges();
            return NoContent();
        }
         // GET: api/AreaPlantio/GetAreasPlantio
        [HttpGet("GetAreasPlantio")]
        public ActionResult<List<AreaPlantio>> GetAreasPlantio()
        {
            var listaAreaPlantio = context.AreaPlantios
                .Include(a => a.Plantio)
                .Include(a => a.Fazenda)
                .ToList();
            return Ok(listaAreaPlantio);
        }

        // GET: api/AreaPlantio/GetAreaPlantio/{id}
        [HttpGet("GetAreaPlantio/{id}")]
        public ActionResult<AreaPlantio> GetAreaPlantio(int id)
        {
            var areaPlantio = context.AreaPlantios
                .Include(a => a.Plantio)
                .Include(a => a.Fazenda)
                .FirstOrDefault(a => a.Id == id);

            if (areaPlantio == null)
            {
                return NotFound();
            }

            return Ok(areaPlantio);
        }

        // POST: api/AreaPlantio/CreateAreaPlantio
        [HttpPost("CreateAreaPlantio")]
        public IActionResult CreateAreaPlantio([FromBody] AreaPlantio areaPlantio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.AreaPlantios.Add(areaPlantio);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetAreaPlantio), new { id = areaPlantio.Id }, areaPlantio);
        }

        // PUT: api/AreaPlantio/UpdateAreaPlantio/{id}
        [HttpPut("UpdateAreaPlantio/{id}")]
        public IActionResult UpdateAreaPlantio(int id, [FromBody] AreaPlantio areaPlantio)
        {
            if (id != areaPlantio.Id)
            {
                return BadRequest("ID da área de plantio na URL e no corpo não coincidem.");
            }

            var areaPlantioExistente = context.AreaPlantios
                .Include(a => a.Plantio)
                .Include(a => a.Fazenda)
                .FirstOrDefault(a => a.Id == id);

            if (areaPlantioExistente == null)
            {
                return NotFound();
            }

            areaPlantioExistente.Nome = areaPlantio.Nome;
            areaPlantioExistente.DataPlantio = areaPlantio.DataPlantio;
            areaPlantioExistente.Hectares = areaPlantio.Hectares;
            areaPlantioExistente.Localizacao = areaPlantio.Localizacao;
            areaPlantioExistente.PlantioId = areaPlantio.PlantioId;
            areaPlantioExistente.FazendaId = areaPlantio.FazendaId;

            context.AreaPlantios.Update(areaPlantioExistente);
            context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/AreaPlantio/DeleteAreaPlantio/{id}
        [HttpDelete("DeleteAreaPlantio/{id}")]
        public IActionResult DeleteAreaPlantio(int id)
        {
            var areaPlantio = context.AreaPlantios.Find(id);
            if (areaPlantio == null)
            {
                return NotFound();
            }

            context.AreaPlantios.Remove(areaPlantio);
            context.SaveChanges();
            return NoContent();
        }
         // GET: api/Plantio/GetPlantios
        [HttpGet("GetPlantios")]
        public ActionResult<List<Plantio>> GetPlantios()
        {
            var listaPlantios = context.Plantios
                .Include(p => p.AreaPlantio)  // Inclui os dados relacionados da Área de Plantio
                .ToList();
            return Ok(listaPlantios);
        }

        // GET: api/Plantio/GetPlantio/{id}
        [HttpGet("GetPlantio/{id}")]
        public ActionResult<Plantio> GetPlantio(int id)
        {
            var plantio = context.Plantios
                .Include(p => p.AreaPlantio)  // Inclui os dados da Área de Plantio
                .FirstOrDefault(p => p.Id == id);

            if (plantio == null)
            {
                return NotFound();
            }

            return Ok(plantio);
        }

        // POST: api/Plantio/CreatePlantio
        [HttpPost("CreatePlantio")]
        public IActionResult CreatePlantio([FromBody] Plantio plantio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Plantios.Add(plantio);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetPlantio), new { id = plantio.Id }, plantio);
        }

        // PUT: api/Plantio/UpdatePlantio/{id}
        [HttpPut("UpdatePlantio/{id}")]
        public IActionResult UpdatePlantio(int id, [FromBody] Plantio plantio)
        {
            if (id != plantio.Id)
            {
                return BadRequest("ID do plantio na URL e no corpo não coincidem.");
            }

            var plantioExistente = context.Plantios
                .Include(p => p.AreaPlantio)  // Inclui os dados da Área de Plantio
                .FirstOrDefault(p => p.Id == id);

            if (plantioExistente == null)
            {
                return NotFound();
            }

            plantioExistente.Nome = plantio.Nome;
            plantioExistente.DataInicio = plantio.DataInicio;
            plantioExistente.AreaId = plantio.AreaId;

            context.Plantios.Update(plantioExistente);
            context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Plantio/DeletePlantio/{id}
        [HttpDelete("DeletePlantio/{id}")]
        public IActionResult DeletePlantio(int id)
        {
            var plantio = context.Plantios.Find(id);
            if (plantio == null)
            {
                return NotFound();
            }

            context.Plantios.Remove(plantio);
            context.SaveChanges();
            return NoContent();
        }
        // GET: api/Compra/GetCompras
        [HttpGet("GetCompras")]
        public ActionResult<List<Compra>> GetCompras()
        {
            var listaCompras = context.Compras
                .Include(c => c.Fornecedor)  // Inclui os dados do Fornecedor
                .Include(c => c.Funcionario) // Inclui os dados do Funcionario
                .Include(c => c.ItensCompra) // Inclui os itens da compra
                .ToList();
            return Ok(listaCompras);
        }

        // GET: api/Compra/GetCompra/{id}
        [HttpGet("GetCompra/{id}")]
        public ActionResult<Compra> GetCompra(int id)
        {
            var compra = context.Compras
                .Include(c => c.Fornecedor)
                .Include(c => c.Funcionario)
                .Include(c => c.ItensCompra)
                .FirstOrDefault(c => c.Id == id);

            if (compra == null)
            {
                return NotFound();
            }

            return Ok(compra);
        }

        // POST: api/Compra/CreateCompra
        [HttpPost("CreateCompra")]
        public IActionResult CreateCompra([FromBody] Compra compra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.Compras.Add(compra);
            context.SaveChanges();
            return CreatedAtAction(nameof(GetCompra), new { id = compra.Id }, compra);
        }

        // PUT: api/Compra/UpdateCompra/{id}
        [HttpPut("UpdateCompra/{id}")]
        public IActionResult UpdateCompra(int id, [FromBody] Compra compra)
        {
            if (id != compra.Id)
            {
                return BadRequest("ID da compra na URL e no corpo não coincidem.");
            }

            var compraExistente = context.Compras
                .Include(c => c.Fornecedor)
                .Include(c => c.Funcionario)
                .FirstOrDefault(c => c.Id == id);

            if (compraExistente == null)
            {
                return NotFound();
            }

            compraExistente.Status = compra.Status;
            compraExistente.FormaPagamento = compra.FormaPagamento;
            compraExistente.FormaEntrega = compra.FormaEntrega;
            compraExistente.DataVenda = compra.DataVenda;

            context.Compras.Update(compraExistente);
            context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/Compra/DeleteCompra/{id}
        [HttpDelete("DeleteCompra/{id}")]
        public IActionResult DeleteCompra(int id)
        {
            var compra = context.Compras.Find(id);
            if (compra == null)
            {
                return NotFound();
            }

            context.Compras.Remove(compra);
            context.SaveChanges();
            return NoContent();
        }
    }
}
