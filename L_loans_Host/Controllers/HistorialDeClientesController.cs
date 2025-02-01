using ClosedXML.Excel;
using L_loans_Class;
using Microsoft.AspNetCore.Mvc;

namespace L_loans_Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialDeClientesController : ControllerBase
    {
        private readonly PInventoryContext _context;

        public HistorialDeClientesController(PInventoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<IEnumerable<HistorialDeCliente>> GetClientes()
        {
            return _context.HistorialDeClientes.ToList();
        }

        [HttpGet("DescargarExcel")]
        public IActionResult DescargarExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Historial de Clientes");

                // Encabezados
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Id Clientes";
                worksheet.Cell(1, 3).Value = "Fecha De Creacion";
                worksheet.Cell(1, 4).Value = "Descripcion";

                // Datos
                var clientes = _context.HistorialDeClientes.ToList();
                for (int i = 0; i < clientes.Count; i++)
                {
                    var cliente = clientes[i];
                    worksheet.Cell(i + 2, 1).Value = cliente.Id;
                    worksheet.Cell(i + 2, 2).Value = cliente.IdClientes;
                    worksheet.Cell(i + 2, 3).Value = cliente.FechaDeCreacion;
                    worksheet.Cell(i + 2, 4).Value = cliente.Descripcion;
                }

                // Enviar archivo al cliente
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Historial de Clientes.xlsx");
                }
            }
        }
    }
}
