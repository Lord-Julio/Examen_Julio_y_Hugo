﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Modelos;
namespace Datos
{
    public class DAOGrupo
    {
        public DataTable obtenerTodos() {
            MySqlCommand consulta =
                new MySqlCommand(@"SELECT g.Id, g.ClaveGrupo, m.Nombre Materia, c.Nombre Carrera,g.Cupo
                    FROM Grupos g
                    JOIN Materias m ON g.ClaveMateria=m.Id
                    JOIN Carreras c ON m.ClaveCarrera=c.Clave
                    ORDER BY Carrera, Materia, ClaveGrupo;");
            return Conexion.ejecutarConsulta(consulta);

        }

        public Grupo obtenerUno(int idGrupo)
        {
            MySqlCommand consulta =
                new MySqlCommand(@"SELECT g.Id, g.ClaveGrupo, g.ClaveMateria,g.Cupo, m.ClaveCarrera, g.Horario, g.Dias
                    FROM Grupos g
                    JOIN Materias m ON g.ClaveMateria=m.Id
                    WHERE g.Id=@IdGrupo");
            consulta.Parameters.AddWithValue("@IdGrupo", idGrupo);
            DataTable resultado= Conexion.ejecutarConsulta(consulta);
            if (resultado != null && resultado.Rows.Count > 0)
            {
                DataRow fila = resultado.Rows[0];
                //Llenar los datos en un a alumno
                Grupo grupo = new Grupo() {
                    Id = idGrupo,
                    ClaveGrupo = fila["ClaveGrupo"].ToString(),
                    ClaveMateria = int.Parse(fila["ClaveMateria"].ToString()),
                    ClaveCarrera = int.Parse(fila["ClaveCarrera"].ToString()),
                    Cupo = Byte.Parse(fila["Cupo"].ToString()),
                    Horario = Byte.Parse(fila["Horario"].ToString()),
                    Dias = fila["Dias"].ToString()
                };
                return grupo;
            }
            else {
                return null;
            }

        }
        public bool insertar(Grupo obj) {
            MySqlCommand insert = new MySqlCommand(
                @"INSERT INTO Grupos VALUES(default,
                    @ClaveGrupo,@ClaveMateria,@Cupo,@Dias,@Horario)"
                );
            insert.Parameters.AddWithValue("@ClaveGrupo", obj.ClaveGrupo);
            insert.Parameters.AddWithValue("@ClaveMateria", obj.ClaveMateria);
            insert.Parameters.AddWithValue("@Cupo", obj.Cupo);
            insert.Parameters.AddWithValue("@Dias", obj.Dias);
            insert.Parameters.AddWithValue("@Horario", obj.Horario);
            

            int resultado = Conexion.ejecutarSentencia(insert);
            return (resultado > 0);
        }

        public bool actualizar(Grupo obj)
        {
            MySqlCommand update = new MySqlCommand(
                @"UPDATE Grupos
                SET ClaveGrupo=@ClaveGrupo, 
                    ClaveMateria=@ClaveMateria, 
                    Cupo=@Cupo,
                    Horario=@Horario, 
                    Dias=@Dias
                WHERE Id=@Id"
                );
            update.Parameters.AddWithValue("@ClaveGrupo", obj.ClaveGrupo);
            update.Parameters.AddWithValue("@ClaveMateria", obj.ClaveMateria);
            update.Parameters.AddWithValue("@Cupo", obj.Cupo);
            update.Parameters.AddWithValue("@Dias", obj.Dias);
            update.Parameters.AddWithValue("@Horario", obj.Horario);
            update.Parameters.AddWithValue("@Id", obj.Id);
            
            int resultado = Conexion.ejecutarSentencia(update);
            return (resultado > 0);
        }

        public bool eliminar(int idGrupo)
        {
            MySqlCommand delete = new MySqlCommand(
                @"DELETE FROM grupos
                WHERE Id=@Id"
                );
            delete.Parameters.AddWithValue("@Id", idGrupo);
            
            int resultado = Conexion.ejecutarSentencia(delete);
            return (resultado > 0);
        }
        public DataTable obtener_por_carrera(int clave_carrera)
        {
            MySqlCommand consulta =
               new MySqlCommand(@"SELECT g.id, m.Nombre Materia, m.Nombre Carrera,g.Cupo,
                    g.ClaveGrupo, g.horario, m.creditos
                    FROM Grupos g
                    JOIN Materias m ON g.ClaveMateria=m.Id
                    where m.ClaveCarrera = @clave
                    ORDER BY Carrera, Materia, ClaveGrupo;");
            consulta.Parameters.AddWithValue("@clave", clave_carrera);
            
            return Conexion.ejecutarConsulta(consulta);
        }
        //OBTENEMOS CLAVE DE LA CARRERA
        public int obtener_clave_materia(int id_chido)
        {
            MySqlCommand consulta =
               new MySqlCommand(@"SELECT g.claveMateria
            from grupos g
            where g.id = @idGrupo");
            consulta.Parameters.AddWithValue("@idGrupo", id_chido);
            DataTable resultado  = Conexion.ejecutarConsulta(consulta);
            return int.Parse(resultado.Rows[0][0].ToString());
        }
        //OBTENEMOS LA CARGA ACADEMICA 
        public DataTable obtener_carga(String nocontrol)
        {
            MySqlCommand consulta =
               new MySqlCommand(@"select m.nombre, g.clavegrupo, g.horario
                from materias m join grupos g on m.id = g.clavemateria
                join alumnosgrupos ag on g.id = ag.idgrupo
                where ag.nocontrol = @nocontrol");
            consulta.Parameters.AddWithValue("@nocontrol", nocontrol);
            DataTable resultado = Conexion.ejecutarConsulta(consulta);

            return resultado;
            
        }
        //consultar el cupo de los grupos
        public DataTable obtener_cupo_grupos(int claveCarrera)
        {
            MySqlCommand consulta =
                new MySqlCommand(@"SELECT M.NOMBRE AS NOMBRE_MATERIA, G.CLAVEGRUPO AS CLAVE_GRUPO, G.HORARIO AS HORARIO,
                    G.CUPO AS CUPO
                    FROM GRUPOS G JOIN MATERIAS M ON G.CLAVEMATERIA = M.ID 
                    JOIN CARRERAS C ON M.CLAVECARRERA = C.CLAVE
                    WHERE  C.CLAVE = @claveCarrera");
            consulta.Parameters.AddWithValue("@claveCarrera", claveCarrera);
            DataTable resultado = Conexion.ejecutarConsulta(consulta);
            return resultado;
          
        }

    }
}
