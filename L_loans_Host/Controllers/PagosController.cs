using ClosedXML.Excel;
using L_loans_Class;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace L_loans_Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly PInventoryContext _context;

        public PagosController(PInventoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Pago>> GetPagos()
        {
            return _context.Pagos.ToList();
        }

        [HttpGet("DescargarExcel")]
        public IActionResult DescargarExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Pagos");

                // Encabezados
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Id del Prestamo";
                worksheet.Cell(1, 3).Value = "Monto Pagado";
                worksheet.Cell(1, 4).Value = "Fecha de Pago";
                worksheet.Cell(1, 5).Value = "Metodo de Pago";

                // Datos
                var pagos = _context.Pagos.ToList();
                for (int i = 0; i < pagos.Count; i++)
                {
                    var pago = pagos[i];
                    worksheet.Cell(i + 2, 1).Value = pago.Id;
                    worksheet.Cell(i + 2, 2).Value = pago.PId;
                    worksheet.Cell(i + 2, 3).Value = pago.MontoPagado;
                    worksheet.Cell(i + 2, 4).Value = pago.FechaDePago;
                    worksheet.Cell(i + 2, 5).Value = pago.MetodoDePago;
                }

                // Enviar archivo al cliente
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Pagos.xlsx");
                }
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PostPago([FromBody] Pago pago)
        {
            if (pago.PId == 0 || pago.MontoPagado == 0 || pago.FechaDePago == null || string.IsNullOrEmpty(pago.MetodoDePago))
            {
                return BadRequest("Revise el registro y corriga el error e intente de nuevo.");
            }
            else
            {
                var nuevoPago = _context.Pagos.Add(pago);
                await _context.SaveChangesAsync();
                var pdfContent = GenerarFacturaPDF(nuevoPago.Entity);
                var fileName = $"Factura_{nuevoPago.Entity.Id}.pdf";

                return File(pdfContent, "application/pdf", fileName);
            }
        }

        private byte[] GenerarFacturaPDF(Pago pago)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var document = new PdfDocument())
                {
                    var page = document.AddPage();
                    using (var gfx = XGraphics.FromPdfPage(page))
                    {
                        var font = new XFont("Arial", 12, XFontStyle.Regular);
                        gfx.DrawString("Factura de Pago", new XFont("Arial", 18, XFontStyle.Bold), XBrushes.Black, new XPoint(200, 50));
                        gfx.DrawString($"ID Pago: {pago.Id}", font, XBrushes.Black, new XPoint(50, 100));
                        gfx.DrawString($"ID Préstamo: {pago.PId}", font, XBrushes.Black, new XPoint(50, 130));
                        gfx.DrawString($"Monto Pagado: {pago.MontoPagado:C}", font, XBrushes.Black, new XPoint(50, 160));
                        gfx.DrawString($"Fecha de Pago: {pago.FechaDePago:dd/MM/yyyy}", font, XBrushes.Black, new XPoint(50, 190));
                        gfx.DrawString($"Método de Pago: {pago.MetodoDePago}", font, XBrushes.Black, new XPoint(50, 220));
                    }

                    document.Save(memoryStream);
                }

                return memoryStream.ToArray();
            }
        }


        [HttpDelete]
        public async Task<ActionResult> DeletePago(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Ingrese un ID válido e intente de nuevo.");
            }
            else
            {
                var pago = await _context.Pagos.FindAsync(id);
                if (pago != null)
                {
                    _context.Pagos.Remove(pago);
                    await _context.SaveChangesAsync();
                    return Ok("El pago se ha eliminado correctamente.");
                }

                return NotFound("Pago no encontrado.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Pago>> PutPago(int id, [FromBody] Pago pago)
        {
            if (id <= 0 || pago.PId == 0 || pago.MontoPagado == 0 || pago.FechaDePago == null || string.IsNullOrEmpty(pago.MetodoDePago))
            {
                return BadRequest("Revise el registro y corriga el error e intente de nuevo.");
            }
            else
            {
                var existingPago = await _context.Pagos.FindAsync(id);
                if (existingPago == null)
                {
                    return NotFound("Pago no encontrado.");
                }

                existingPago.PId = pago.PId;
                existingPago.MontoPagado = pago.MontoPagado;
                existingPago.FechaDePago = pago.FechaDePago;
                existingPago.MetodoDePago = pago.MetodoDePago;

                await _context.SaveChangesAsync();
                return Ok("El pago ha sido actualizado correctamente.");
            }
        }
    }
}
