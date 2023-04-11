using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Controlador
    {
        Conexion conexion = new Conexion();
        public void Guardar(Dictionary<string, List<string>> valoresPorTagTabla, Dictionary<string, List<string>> valoresPorTagColumnas)
        {
            HashSet<string> tables = new HashSet<string>(valoresPorTagTabla.Keys.Union(valoresPorTagColumnas.Keys));

            foreach (string tabla in tables)
            {
                List<string> columnas = valoresPorTagTabla[tabla];
                List<string> valores = valoresPorTagColumnas[tabla];

                // Generate INSERT statement
                string insert = $"INSERT INTO {tabla} ({string.Join(",", columnas)}) VALUES ({string.Join(",", valores)});";

                try
                {
                    OdbcCommand cmd = new OdbcCommand(insert, conexion.conexion());
                    Console.WriteLine(insert);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }

        }
        public void Actualizar(Dictionary<string, List<string>> valoresPorTagTabla, Dictionary<string, List<string>> valoresPorTagColumnas, Dictionary<string, List<string>> condiciones)
        {
            HashSet<string> tables = new HashSet<string>(valoresPorTagTabla.Keys.Union(valoresPorTagColumnas.Keys));
            foreach (string tabla in tables)
            {
                List<string> columnas = valoresPorTagTabla[tabla];
                List<string> valores = valoresPorTagColumnas[tabla];
                string condicion = string.Join(" AND ", condiciones[tabla].Select((v, i) => $"{v}={valoresPorTagColumnas[tabla][i]}"));
                
                List<string> updateValues = columnas.Zip(valores, (col, val) => $"{col}={val}").ToList();
                string update = $"UPDATE {tabla} SET {string.Join(",", updateValues)} WHERE {condicion};";
                try
                {
                    OdbcCommand consulta = new OdbcCommand(update, conexion.conexion());
                    Console.WriteLine(update);
                    consulta.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error en CapaModeloReporteria --> Sentencias" + e);
                }
            }

        }
        public void Eliminar(Dictionary<string, List<string>> valoresPorTagTabla, Dictionary<string, List<string>> valoresPorTagColumnas, Dictionary<string, List<string>> condiciones)
        {
            HashSet<string> tables = new HashSet<string>(valoresPorTagTabla.Keys.Union(valoresPorTagColumnas.Keys));
            foreach (string tabla in tables)
            {
                List<string> columnas = valoresPorTagTabla[tabla];
                List<string> valores = valoresPorTagColumnas[tabla];
                string condicion = string.Join(" AND ", condiciones[tabla].Select((v, i) => $"{v}={valoresPorTagColumnas[tabla][i]}"));
                string delete = $"DELETE FROM {tabla} WHERE {condicion};";
                try
                {
                    OdbcCommand cmd = new OdbcCommand(delete, conexion.conexion());
                    Console.WriteLine(delete);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString() + " \nNo se pudo guardar el registro en la tabla " );
                }
            }

        }
    }
}
