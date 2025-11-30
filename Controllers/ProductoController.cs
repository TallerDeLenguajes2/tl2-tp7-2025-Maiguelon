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

    [HttpPost("Api/Producto")]
    public ActionResult<string> AltaProducto(Producto nuevoProducto)
    {
        int nuevoId = productoRepository.Alta(nuevoProducto);
        return Ok("Producto dado de alta exitosamente " + nuevoId.ToString());
    }

    [HttpPut("Api/Producto{id}")]
    public ActionResult<string> ModificarProducto(int id, Producto producto)
    {
        bool result = productoRepository.Modificar(id, producto);

        if (result == false)
        {
            return NotFound($"No se encontró el producto con ID {id} para modificar.");
        }

        return Ok("Producto modificado exitosamente");
    }
}
