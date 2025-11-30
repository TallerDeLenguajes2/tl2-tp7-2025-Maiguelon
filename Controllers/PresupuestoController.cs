using Microsoft.AspNetCore.Mvc;

namespace tl2_tp7_2025_Maiguelon.Controllers;

using presupuestario;

[ApiController]
[Route("[controller]")]
public class PresupuestoController : ControllerBase
{
    private PresupuestoRepository presupuestoRepository;
    public PresupuestoController()
    {
        presupuestoRepository = new PresupuestoRepository();
    }

    // Metodos

    [HttpPost("Api/AltaProducto")]
    public ActionResult<string> AltaPresu(Presupuesto presupuesto)
    {
        int nuevoId = presupuestoRepository.AltaPresupuesto(presupuesto);
        return Ok(nuevoId);
    }

    [HttpGet]
    public ActionResult<List<Presupuesto>> GetPresupuestos()
    {
        // Llama al método del repositorio que lista (devuelve solo encabezados generalmente)
        var lista = presupuestoRepository.ListarPresupuestos();
        return Ok(lista);
    }

    [HttpGet("{id}")]
    public ActionResult<object> GetPresupuesto(int id)
    {
        var presupuesto = presupuestoRepository.ObtenerPresupuestoPorId(id);

        if (presupuesto == null)
        {
            return NotFound("Presupuesto no encontrado");
        }

        // Creamos un objeto anónimo para devolver el presupuesto Y sus totales calculados
        var resultado = new
        {
            DetallePresupuesto = presupuesto,
            CantidadProductos = presupuesto.CantidadProductos(),
            Subtotal = presupuesto.MontoPresupuesto(),
            TotalConIva = presupuesto.MontoPresupuestoConIva()
        };

        return Ok(resultado);
    }

    [HttpPost("{id}/ProductoDetalle")]
    public ActionResult AgregarDetalle(int id, int idProducto, int cantidad)
    {
        // Llamamos al repositorio para agregar el ítem
        presupuestoRepository.AgregarDetalle(id, idProducto, cantidad);

        return Ok("Detalle agregado exitosamente");
    }

    [HttpDelete("{id}")]
    public ActionResult DeletePresupuesto(int id)
    {
        bool eliminado = presupuestoRepository.EliminarPresupuesto(id);

        if (eliminado)
        {
            return NoContent(); // 204 No Content (Éxito borrando)
        }
        else
        {
            return NotFound("No se encontró el presupuesto para eliminar");
        }
    }
}