using ClosedXML.Excel;
using L_loans_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L_loans_Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly PInventoryContext _context;

        public PrestamosController(PInventoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<IEnumerable<Prestamo>> GetPrestamo()
        {
            return _context.Prestamos.ToList();
        }

        [HttpGet("DescargarExcel")]
        public IActionResult DescargarExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Prestamos");

                // Encabezados
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Id del cliente";
                worksheet.Cell(1, 3).Value = "Monto";
                worksheet.Cell(1, 4).Value = "Interes";
                worksheet.Cell(1, 5).Value = "Fecha De Inicio";
                worksheet.Cell(1, 6).Value = "Duracion Del Prestamo";
                worksheet.Cell(1, 7).Value = "Fecha Registro";
                worksheet.Cell(1, 8).Value = "Estado";

                // Datos
                var clientes = _context.Prestamos.ToList();
                for (int i = 0; i < clientes.Count; i++)
                {
                    var cliente = clientes[i];
                    worksheet.Cell(i + 2, 1).Value = cliente.Id;
                    worksheet.Cell(i + 2, 2).Value = cliente.CId;
                    worksheet.Cell(i + 2, 3).Value = cliente.Monto;
                    worksheet.Cell(i + 2, 4).Value = cliente.Interes;
                    worksheet.Cell(i + 2, 5).Value = cliente.FechaDeInicio;
                    worksheet.Cell(i + 2, 6).Value = cliente.DuracionDelPrestamo;
                    worksheet.Cell(i + 2, 7).Value = cliente.Estado;
                }

                // Enviar archivo al cliente
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Prestamos.xlsx");
                }
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Prestamo>> PostPrestamo([FromBody] Prestamo Prestamo)
        {

            if (Prestamo.CId == 0 || Prestamo.Monto == 0 || Prestamo.FechaDeInicio == null || Prestamo.DuracionDelPrestamo == null || Prestamo.Estado == null)
            {
                return BadRequest("Revise el registro y corriga el error eh intentelo de nuevo");
            }
            else
            {
                var datos = _context.Prestamos.Add(Prestamo);
                await _context.SaveChangesAsync();

                return Ok("El registro se ah insertado Correctamente");
            }

        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Prestamo>> DeletePrestamo(int iD)
        {

            if (iD == null || iD <= 0)
            {
                return BadRequest("Ingrese un ID valido eh intentelo de nuevo");
            }
            else
            {
                var datos = _context.Prestamos.FirstOrDefaultAsync(x => x.Id == iD);
                _context.Prestamos.Remove(await datos);
                await _context.SaveChangesAsync();

                return Ok("El registro se ah Eliminado Correctamente");
            }

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Prestamo>> PutPrestamo(int iD, [FromBody] Prestamo Prestamo)
        {
            if (Prestamo.Id == null || Prestamo.Id <= 0)
            {
                return BadRequest("Ingrese un ID valido eh intentelo de nuevo");
            }
            if (Prestamo.CId == 0 || Prestamo.Monto == 0 || Prestamo.Interes == 0 || Prestamo.FechaDeInicio == null || Prestamo.DuracionDelPrestamo == null ||
                 Prestamo.Estado == null)
            {
                return BadRequest("Revise el registro y corriga el error eh intentelo de nuevo");
            }
            else
            {
                var datos = _context.Prestamos.FirstOrDefault(x => x.Id == iD);

                datos.CId = Prestamo.CId;
                datos.Monto = Prestamo.Monto;
                datos.Interes = Prestamo.Interes;
                datos.FechaDeInicio = Prestamo.FechaDeInicio;
                datos.DuracionDelPrestamo = Prestamo.DuracionDelPrestamo;
                datos.Estado = Prestamo.Estado;
                await _context.SaveChangesAsync();

                return Ok("El registro se ah Actualizado Correctamente");
            }
        }
    }
}
