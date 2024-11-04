using CRUD.DataAccess;
using CRUD.Models;
using System;
using System.Collections.Generic;

namespace CRUD.BusinessLogic
{
    public class MovimientoRN
    {
        private readonly MovimientoAD _movimientoAD = new MovimientoAD();

        public List<Movimiento> ConsultarInventario(ParametroFiltro param)
        {
            try
            {
                return _movimientoAD.ConsultarInventario(param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa lógica de negocio: " + ex.Message, ex);
            }
        }



        public bool NuevoMovimiento(ParametroNuevo param)
        {
            try
            {
                return _movimientoAD.NuevoMovimiento(param);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la capa lógica de negocio", ex);
            }
        }

    }
}