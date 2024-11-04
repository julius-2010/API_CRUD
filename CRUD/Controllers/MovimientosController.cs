using CRUD.BusinessLogic;
using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace CRUD.Controllers
{
    [RoutePrefix("api/movimientos")]
    public class MovimientosController : ApiController
    {
        private readonly MovimientoRN _movimientoRN = new MovimientoRN();

        [HttpGet]
        [Route("consultar")]
        public IHttpActionResult Consultar(string fechaInicio, string fechaFin, string tipoMovimiento, string nroDocumento)
        {
            try
            {
                var param = new ParametroFiltro
                {
                    FechaInicio = fechaInicio,
                    FechaFin = fechaFin,
                    TipoMovimiento = tipoMovimiento,
                    NroDocumento = nroDocumento
                };

                List<Movimiento> movimiento = new List<Movimiento>();
                movimiento = _movimientoRN.ConsultarInventario(param);
                return Ok(movimiento);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Error en el controlador: " + ex.Message));
            }
        }


        [HttpPost]
        [Route("nuevo")]
        public IHttpActionResult Nuevo([FromBody] ParametroNuevo param)
        {
            try
            {
                bool exito = _movimientoRN.NuevoMovimiento(param);
                if (exito)
                {
                    return Ok("Movmiento registrado exitosamente.");
                }
                else
                {
                    return BadRequest("No se pudo crear el movimiento.");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("Error en el controlador: " + ex.Message));
            }
        }


    }
}