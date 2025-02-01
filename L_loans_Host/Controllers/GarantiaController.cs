using ClosedXML.Excel;
using L_loans_Class;
using Microsoft.AspNetCore.Mvc;

namespace L_loans_Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarantiaController : ControllerBase
    {
        private readonly PInventoryContext _context;

        public GarantiaController(PInventoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Garantium>> GetGarantia()
        {
            return _context.Garantia.ToList();
        }

        [HttpGet("DescargarExcel")]
        public IActionResult DescargarExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Garantia");

                // Encabezados
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Id del Prestamo";
                worksheet.Cell(1, 3).Value = "Tipo De Garantia";
                worksheet.Cell(1, 4).Value = "Descripcion";
                worksheet.Cell(1, 5).Value = "Valor Estimado";

                // Datos
                var garantias = _context.Garantia.ToList();
                for (int i = 0; i < garantias.Count; i++)
                {
                    var garantia = garantias[i];
                    worksheet.Cell(i + 2, 1).Value = garantia.Id;
                    worksheet.Cell(i + 2, 2).Value = garantia.PId;
                    worksheet.Cell(i + 2, 3).Value = garantia.TipoDeGarantia;
                    worksheet.Cell(i + 2, 4).Value = garantia.Descripcion;
                    worksheet.Cell(i + 2, 5).Value = garantia.ValorEstimado;
                }

                // Enviar archivo al cliente
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Garantia.xlsx");
                }
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Garantium>> PostGarantia([FromBody] Garantium garantium)
        {
            if (garantium.PId == 0 || string.IsNullOrEmpty(garantium.TipoDeGarantia) || string.IsNullOrEmpty(garantium.Descripcion) || garantium.ValorEstimado <= 0)
            {
                return BadRequest("Revise el registro y corriga el error e intentelo de nuevo");
            }
            else
            {
                _context.Garantia.Add(garantium);
                await _context.SaveChangesAsync();
                return Ok("El registro se ha insertado correctamente");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteGarantia(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Ingrese un ID valido");
            }
            else
            {
                var garantia = await _context.Garantia.FindAsync(id);
                if (garantia == null)
                {
                    return NotFound("Garantia no encontrada");
                }
                _context.Garantia.Remove(garantia);
                await _context.SaveChangesAsync();
                return Ok("El registro se ha eliminado correctamente");
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutGarantia(int id, [FromBody] Garantium garantia)
        {
            var existingGarantia = await _context.Garantia.FindAsync(id);
            if (existingGarantia == null)
            {
                return BadRequest("Garantia no encontrada");
            }

            existingGarantia.PId = garantia.PId;
            existingGarantia.TipoDeGarantia = garantia.TipoDeGarantia;
            existingGarantia.Descripcion = garantia.Descripcion;
            existingGarantia.ValorEstimado = garantia.ValorEstimado;

            await _context.SaveChangesAsync();
            return Ok("El registro se ha actualizado correctamente");
        }
    }

}
