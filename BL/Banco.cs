using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Banco
    {
        public static ML.Result GetAll() { 
        
            ML.Result result = new ML.Result();
            try
            {
                using (var context = new SqlConnection(DL.Conexion.Getconection())) {
                    string cadena = "BancoGetAll";
                    using (SqlCommand command=new SqlCommand())
                    {
                        command.Connection = context;
                        command.CommandText = cadena;
                        command.CommandType = CommandType.StoredProcedure;
                        DataTable tabla=new DataTable();
                        SqlDataAdapter sqlDataAdapter=new SqlDataAdapter(command);
                        sqlDataAdapter.Fill(tabla);
                        if (tabla.Rows.Count>0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tabla.Rows)
                            {
                                ML.Banco banco = new ML.Banco();
                                banco.IdBanco = (int)row[0];
                                banco.Nombre = row[1].ToString();
                                banco.NumeroEmpleados = (int)row[2];
                                banco.NumeroSucursales= (int)row[3];
                                banco.Pais=new ML.Pais();
                                banco.Pais.IdPais= (int)row[4];
                                banco.Pais.Nombre= row[5].ToString();
                                banco.Capital = (decimal)row[6];
                                banco.RazonSocial=new ML.RazonSocial();
                                banco.RazonSocial.IdRazonSocial = (int)row[7];
                                banco.RazonSocial.Nombre = row[8].ToString();
                                banco.NumeroClientes= (int)row[9];
                                result.Objects.Add(banco);
                            }
                            result.Correct = true;
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {

                result.Message ="error: "+ ex.Message;
                result.Correct=false;
            }
            return result;  
        }

        public static ML.Result Add(ML.Banco banco) {
        
            ML.Result result = new ML.Result();
            using (var contex=new SqlConnection(DL.Conexion.Getconection()))
            {
               
                string cadena = "BancoAdd";
                try
                {

                
                using (SqlCommand command=new SqlCommand())
                {
                    command.Connection = contex;
                    command.CommandText = cadena;
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter[] parameters = new SqlParameter[7];
                    parameters[0] = new SqlParameter("Nombre",SqlDbType.VarChar);
                    parameters[0].Value = banco.Nombre;
                    parameters[1] = new SqlParameter("NoEmpleados", SqlDbType.Int);
                    parameters[1].Value = banco.NumeroEmpleados;
                    parameters[2] = new SqlParameter("NoSucursales", SqlDbType.Int);
                    parameters[2].Value = banco.NumeroSucursales;
                    parameters[3] = new SqlParameter("IdPais", SqlDbType.Int);
                    parameters[3].Value = banco.Pais.IdPais;
                    parameters[4] = new SqlParameter("Capital", SqlDbType.Money);
                    parameters[4].Value = banco.Capital;
                    parameters[5] = new SqlParameter("IdRazonSocial", SqlDbType.Int);
                    parameters[5].Value = banco.RazonSocial.IdRazonSocial;
                    parameters[6] = new SqlParameter("NoClientes", SqlDbType.Int);
                    parameters[6].Value = banco.NumeroClientes;

                    command.Parameters.AddRange(parameters);
                    command.Connection.Open();
                    int res=command.ExecuteNonQuery();
                    if (res>0)
                    {
                        result.Message = "Banco agregado Correctamente";
                        result.Correct = true;
                    }
                   

                }
                }
                catch (Exception ex)
                {

                   result.Correct=false;
                   result.Message = "Ocurrio un error al agregar al usuario: "+ex.Message;
                }
            }
            return result;
        }

     
    }
}
