using ClosedXML.Excel;
using L_loans_Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L_loans_Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly PInventoryContext _context;

        public ClientesController(PInventoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<IEnumerable<Cliete>> GetClientes()
        {
            return _context.Clietes.ToList();
        }

        [HttpGet("DescargarExcel")]
        public IActionResult DescargarExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Clientes");

                // Encabezados
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "Nombre";
                worksheet.Cell(1, 3).Value = "Apellidos";
                worksheet.Cell(1, 4).Value = "Edad";
                worksheet.Cell(1, 5).Value = "Cédula";
                worksheet.Cell(1, 6).Value = "Teléfono";
                worksheet.Cell(1, 7).Value = "Fecha Registro";
                worksheet.Cell(1, 8).Value = "Descripción";

                // Datos
                var clientes = _context.Clietes.ToList();
                for (int i = 0; i < clientes.Count; i++)
                {
                    var cliente = clientes[i];
                    worksheet.Cell(i + 2, 1).Value = cliente.Id;
                    worksheet.Cell(i + 2, 2).Value = cliente.CNombre;
                    worksheet.Cell(i + 2, 3).Value = cliente.CApellidos;
                    worksheet.Cell(i + 2, 4).Value = cliente.CEdad;
                    worksheet.Cell(i + 2, 5).Value = cliente.CCedula;
                    worksheet.Cell(i + 2, 6).Value = cliente.CNumeroDeTelefono;
                    worksheet.Cell(i + 2, 7).Value = cliente.CFechaDeRegistro.ToString();
                    worksheet.Cell(i + 2, 8).Value = cliente.C_Descripcion;
                }

                // Enviar archivo al cliente
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Clientes.xlsx");
                }
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Cliete>> PostClientes([FromBody] Cliete clientes)
        {

            if (clientes == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(clientes.CNombre) ||
                string.IsNullOrWhiteSpace(clientes.CApellidos) ||
                clientes.CEdad == null ||
                string.IsNullOrWhiteSpace(clientes.CCedula) ||
                string.IsNullOrWhiteSpace(clientes.CNumeroDeTelefono) ||
                clientes.CFechaDeRegistro == null ||
                string.IsNullOrWhiteSpace(clientes.C_Descripcion))
            {
                return BadRequest("Todos los campos son obligatorios. Por favor, revise e intente nuevamente.");
            }
            else
            {
                _context.Clietes.Add(clientes);
                await _context.SaveChangesAsync();
                return Ok("El registro se ah insertado Correctamente");
            }


        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Cliete>> DeleteClientes(int iD)
        {

            if (iD <= 0)
            {
                return BadRequest("Ingrese un ID valido eh intentelo de nuevo");
            }
            else
            {
                var datos = _context.Clietes.FirstOrDefaultAsync(x => x.Id == iD);
                _context.Clietes.Remove(await datos);
                await _context.SaveChangesAsync();

                return Ok("El registro se ah Eliminado Correctamente");
            }

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<Cliete>> PutClientes(int iD, [FromBody] Cliete clientes)
        {
            if (clientes.Id == null || clientes.Id <= 0)
            {
                return BadRequest("Ingrese un ID valido eh intentelo de nuevo");
            }
            if (clientes.CNombre == "" || clientes.CApellidos == "" || clientes.CEdad <= 0 || clientes.CCedula == "" || clientes.CNumeroDeTelefono == "" ||
                     clientes.CFechaDeRegistro == null || clientes.C_Descripcion == "")
            {
                return BadRequest("Revise el registro y corriga el error eh intentelo de nuevo");
            }
            else
            {
                var datos = _context.Clietes.FirstOrDefault(x => x.Id == iD);

                datos.CNombre = clientes.CNombre;
                datos.CApellidos = clientes.CApellidos;
                datos.CEdad = clientes.CEdad;
                datos.CCedula = clientes.CCedula;
                datos.CNumeroDeTelefono = clientes.CNumeroDeTelefono;
                datos.CFechaDeRegistro = clientes.CFechaDeRegistro;
                datos.C_Descripcion = clientes.C_Descripcion;
                await _context.SaveChangesAsync();

                return Ok("El registro se ah Actualizado Correctamente");
            }
        }
    }
}
