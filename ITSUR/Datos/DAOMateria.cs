﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Modelos;
namespace Datos
{
    public class DAOMateria
    {
        public DataTable obtenerTodas()
        {
            MySqlCommand consulta =
                new MySqlCommand(@"SELECT Id, m.Nombre Materia, ClaveCarrera, 
                    Creditos, c.Nombre Carrera
                    FROM Materias m
                    JOIN Carreras c ON m.ClaveCarrera=c.clave
                    ORDER BY Carrera, Materia;");
            return Conexion.ejecutarConsulta(consulta);

        }

        public DataTable obtenerXCarrera(int idCarrera)
        {
            MySqlCommand consulta =
                new MySqlCommand(@"SELECT Id, m.Nombre Materia, ClaveCarrera, 
                    Creditos
                    FROM Materias m
                    WHERE ClaveCarrera=@ClaveCarrera
                    ORDER BY Materia");
            consulta.Parameters.AddWithValue("@ClaveCarrera",idCarrera);
            return Conexion.ejecutarConsulta(consulta);

        }



        public bool eliminar(int IdMateria)
        {
            MySqlCommand delete = new MySqlCommand(
                @"DELETE FROM Materias
                WHERE Id=@Id"
                );
            delete.Parameters.AddWithValue("@Id", IdMateria);
            
            int resultado = Conexion.ejecutarSentencia(delete);
            return (resultado > 0);
        }
        public int credito_por_materia(String materia)
        {
            MySqlCommand consulta =
               new MySqlCommand(@"SELECT 
                    Creditos
                    FROM Materias m
                    WHERE m.nombre=@materia");
            consulta.Parameters.AddWithValue("@materia", materia);
            DataTable resultado = Conexion.ejecutarConsulta(consulta);

            return int.Parse(resultado.Rows[0][0].ToString());
        }
    }
}
