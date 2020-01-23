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
    public class CompradorRepository
    {
        private BaseRepository _sqlConn = new BaseRepository();
        public CompradorRepository(BaseRepository sqlConn)
        {
            _sqlConn = sqlConn;
        }
        public CompradorResult AdicionarComprador(Comprador comprador)
        {
            CompradorResult result = new CompradorResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = "INSERT INTO Comprador(Nome, Sobrenome) VALUES(@nome, @sobrenome)";

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@nome", comprador.Nome));
                cmd.Parameters.Add(new SqlParameter("@sobrenome", comprador.Sobrenome));
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                result.ProccessOk = false;
                result.MsgError = ex.ToString();
                return result;
            }
            finally
            {
                conn.Close();
            }
            result.ProccessOk = true;
            return result;
        }
        public CompradorResult GetComprador(int id)
        {
            CompradorResult result = new CompradorResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = "SELECT * FROM Comprador WHERE id=@id";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Comprador.Id = int.Parse(reader["Id"].ToString());
                    result.Comprador.Nome = reader["Nome"].ToString();
                    result.Comprador.Sobrenome = reader["Sobrenome"].ToString();
                }

            }
            catch (Exception ex)
            {
                result.ProccessOk = false;
                result.MsgError = ex.ToString();
                return result;
            }
            finally
            {
                conn.Close();
            }
            result.ProccessOk = true;
            return result;
        }
        public ListaCompradorResult ListarComprador()
        {
            ListaCompradorResult result = new ListaCompradorResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = "SELECT * FROM Comprador";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Comprador comprador = new Comprador();
                    comprador.Sobrenome = reader["Sobrenome"].ToString();
                    comprador.Nome = reader["Nome"].ToString();
                    comprador.Id = int.Parse(reader["Id"].ToString());
                    result.ListaComprador.Add(comprador);
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
        public CompradorResult AtualizarComprador(Comprador comprador)
        {
            CompradorResult result = new CompradorResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = "UPDATE Comprador SET Nome = @nome, Sobrenome = @sobrenome WHERE Id = @id";

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@sobrenome", comprador.Sobrenome));
                cmd.Parameters.Add(new SqlParameter("@nome", comprador.Nome));
                cmd.Parameters.Add(new SqlParameter("@id", comprador.Id));
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
        //public void ExcluirComprador()
        //{
        //    SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
        //    string sql = "DELETE Comprador WHERE Id = @id";

        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        cmd.Parameters.Add(new SqlParameter("@id", "3"));
        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        int x = 10;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}
        #endregion
    }
}
