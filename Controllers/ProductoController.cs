using Microsoft.AspNetCore.Mvc;

namespace tl2_tp7_2025_Maiguelon.Controllers;

using presupuestario;

[ApiController]
[Route("[controller]")]
public class ProductoController : ControllerBase
{
    // Inicializo el repositorio
    private ProductoRepository productoRepository;

    public ProductoController()
    {
        productoRepository = new ProductoRepository();
    }

    // metódicos

    [HttpPost("Api/AltaProducto")]
    public ActionResult<string> AltaProducto(Producto nuevoProducto)
    {
        int nuevoId = productoRepository.Alta(nuevoProducto);
        return Ok("Producto dado de alta exitosamente " + nuevoId.ToString());
    }

    [HttpPut("Api/ModificarProducto{id}")]
    public ActionResult<string> ModificarProducto(int id, Producto producto)
    {
        bool result = productoRepository.Modificar(id, producto);

        if (result == false)
        {
            return NotFound($"No se encontró el producto con ID {id} para modificar.");
        }

        return Ok("Producto modificado exitosamente");
    }

    [HttpGet("Api/Listar")]
    public ActionResult<List<Producto>> GetAll()
    {
        List<Producto> lista = productoRepository.Listar();
        return Ok(lista);
    }

    [HttpGet("Api/ObtenerPorId{id}")]
    public ActionResult<Producto> GetById(int id)
    {
        Producto? prod = productoRepository.obtenerProducto(id);
        return Ok(prod);
    }

    [HttpDelete("Api/BorrarProducto")]
    public ActionResult<string> BorrarPorId(int id)
    {
        bool result = productoRepository.eliminarProducto(id);

        if (result == false)
        {
            return NotFound($"No se pudo borrar el producto de id {id}.");
        }

        return Ok(result);
    }
}
