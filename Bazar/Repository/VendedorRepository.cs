using Bazar.Class;
using Bazar.Result;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazar.Repository
{
    public class VendedorRepository
    {
        private BaseRepository _sqlConn = new BaseRepository();
        public VendedorRepository(BaseRepository sqlConn)
        {
            _sqlConn = sqlConn;
        }
        public VendedorResult AdicionarVendedor(Vendedor vendedor)
        {
            VendedorResult result = new VendedorResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = "INSERT INTO Vendedor(Nome) VALUES(@nome)";

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@nome", vendedor.Nome));
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                result.ProccessOk = false;
                result.MsgCatch = ex.ToString();
            }
            finally
            {
                conn.Close();
            }
            result.ProccessOk = true;
            return result;
        }
        public VendedorResult GetVendedor(string id)
        {
            VendedorResult result = new VendedorResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = "SELECT * FROM Vendedor WHERE Id=@id";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Vendedor.Id = int.Parse(reader["Id"].ToString());
                    result.Vendedor.Nome = reader["Nome"].ToString();
                }

            }
            catch (Exception ex)
            {
                result.ProccessOk = false;
                result.MsgCatch = ex.ToString();
                return result;
            }
            finally
            {
                conn.Close();
            }
            result.ProccessOk = true;
            return result;
        }
        public ListaVendedorResult ListarVendedores()
        {
            ListaVendedorResult result = new ListaVendedorResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = "SELECT * FROM Vendedor";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Vendedor vendedor = new Vendedor();
                    vendedor.Id = int.Parse(reader["Id"].ToString());
                    vendedor.Nome = reader["Nome"].ToString();
                    result.ListaVendedor.Add(vendedor);
                }
            }
            catch (Exception ex)
            {
                result.MsgError = ex.ToString();
                result.ProccessOk = false;
                return result;
            }
            finally
            {
                conn.Close();
            }
            result.ProccessOk = true;
            return result;
        }
        public VendedorResult AtualizarVendedor(Vendedor vendedor)
        {
            VendedorResult result = new VendedorResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = "UPDATE Vendedor SET Nome = @nome WHERE Id = @id";

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@nome", vendedor.Nome));
                cmd.Parameters.Add(new SqlParameter("@id", vendedor.Id));
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                result.ProccessOk = false;
                result.MsgCatch = ex.ToString();
                return result;
            }
            finally
            {
                conn.Close();
            }
            result.ProccessOk = true;
            return result;
        }

        #region Excluir

        //public void ExcluirVendedor()
        //{
        //    SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
        //    string sql = "DELETE Vendedor WHERE Id = @id";

        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        cmd.Parameters.Add(new SqlParameter("@id", "3"));
        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //        conn.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        int x = 10;
        //    }
        //}

        #endregion
    }
}
