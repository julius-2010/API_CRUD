using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CRUD.DataAccess
{
    public class MovimientoAD
    {
        private readonly string _connection = ConfigurationManager.ConnectionStrings["cnCRUD"].ConnectionString;

        public List<Movimiento> ConsultarInventario(ParametroFiltro param)
        {
            List<Movimiento> movimientos = new List<Movimiento>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_ConsultarInventario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@pFechaInicio", param.FechaInicio);
                        command.Parameters.AddWithValue("@pFechaFin", param.FechaFin);
                        command.Parameters.AddWithValue("@pTipoMovimiento", param.TipoMovimiento);
                        command.Parameters.AddWithValue("@pNroDocumento", param.NroDocumento);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Movimiento movimiento = new Movimiento
                                {
                                    COMPANIA_VENTA_3 = reader["COMPANIA_VENTA_3"].ToString(),
                                    ALMACEN_VENTA = reader["ALMACEN_VENTA"].ToString(),
                                    TIPO_MOVIMIENTO = reader["TIPO_MOVIMIENTO"].ToString(),
                                    TIPO_DOCUMENTO = reader["TIPO_DOCUMENTO"].ToString(),
                                    NRO_DOCUMENTO = reader["NRO_DOCUMENTO"].ToString(),
                                    COD_ITEM_2 = reader["COD_ITEM_2"].ToString(),
                                    CANTIDAD = reader["CANTIDAD"].ToString(),
                                    COMPANIA_DESTINO = reader["COMPANIA_DESTINO"].ToString(),
                                    FECHA_TRANSACCION = reader["FECHA_TRANSACCION"].ToString(),
                                };
                                movimientos.Add(movimiento);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la capa de Acceso a Datos", ex);
            }
            return movimientos;
        }


        public bool NuevoMovimiento(ParametroNuevo param)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_InsertarMovimiento", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@COD_CIA", param.COD_CIA);
                        command.Parameters.AddWithValue("@COMPANIA_VENTA_3", param.COMPANIA_VENTA_3);
                        command.Parameters.AddWithValue("@ALMACEN_VENTA", param.ALMACEN_VENTA);
                        command.Parameters.AddWithValue("@TIPO_MOVIMIENTO", param.TIPO_MOVIMIENTO);
                        command.Parameters.AddWithValue("@TIPO_DOCUMENTO", param.TIPO_DOCUMENTO);
                        command.Parameters.AddWithValue("@NRO_DOCUMENTO", param.NRO_DOCUMENTO);
                        command.Parameters.AddWithValue("@COD_ITEM_2", param.COD_ITEM_2);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la capa de Acceso a Datos", ex);
            }
        }


    }
}