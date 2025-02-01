using L_loans_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CalendarioPagosController : ControllerBase
{
    private readonly PInventoryContext _context;

    public CalendarioPagosController(PInventoryContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CalendarioPagos>>> GetCalendarioPagos()
    {
        var calendarioPagos = await _context.CalendarioPagos.ToListAsync();

        if (calendarioPagos == null || !calendarioPagos.Any())
        {
            return NotFound("No se encontraron pagos programados.");
        }

        return Ok(calendarioPagos);
    }

    [HttpGet("api/PagosCalendario")]
    public async Task<ActionResult<List<CalendarioPagos>>> GetPagosCalendario(int Id)
    {

        var Ids = _context.Prestamos.OrderByDescending(x => x.Id);

        var pagosCalendario = await _context.CalendarioPagos
                                            .Where(p => p.Estado == "Pendiente")
                                            .Where(p => p.p_Id == Id)
                                            .ToListAsync();
        return Ok(pagosCalendario);
    }
}
