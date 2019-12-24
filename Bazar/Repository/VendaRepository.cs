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
    public class VendaRepository
    {
        private BaseRepository _sqlConn = new BaseRepository();
        public VendaRepository(BaseRepository sqlConn)
        {
            _sqlConn = sqlConn;
        }
        public VendaResult AdicionarVenda(Venda venda)
        {
            VendaResult result = new VendaResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = "INSERT INTO Venda(IdComprador, ValorTotal) VALUES(@idComprador, 0)";

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@idComprador", venda.IdComprador));
                conn.Open();
                cmd.ExecuteNonQuery();
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
        public VendaResult GetVenda(string id)
        {
            VendaResult result = new VendaResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = @"SELECT * FROM Venda WHERE Id=@id";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Venda.Id = int.Parse(reader["Id"].ToString());
                    result.Venda.ValorTotal = float.Parse(reader["ValorTotal"].ToString());
                    result.Venda.IdComprador = int.Parse(reader["IdComprador"].ToString());
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
        public ListaVendaResult ListarVendas()
        {
            ListaVendaResult result = new ListaVendaResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = @"SELECT Id FROM Venda";
            List<int> listaIdVenda = new List<int>();

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    listaIdVenda.Add(int.Parse(reader["Id"].ToString()));
                }
            }
            catch (Exception ex)
            {
                result.ProccessOk = false;
                result.MsgCatch = ex.ToString();
                result.MsgError = "Erro ao procurar lista de Ids";
                return result;
            }
            finally
            {
                conn.Close();
            }

            foreach (var item in listaIdVenda)
            {
                List<ProdutoVendido> listaProdutoVendido = new List<ProdutoVendido>();
                try
                {
                    conn.Open();
                    sql = @"SELECT ProdutoVendido.Id AS IdProdutoVendido, Vendedor.Nome AS NomeVendedor, * FROM ProdutoVendido 
                        LEFT JOIN Produto
                        ON Produto.Id = ProdutoVendido.IdProduto
						LEFT JOIN Vendedor
						ON Vendedor.Id = Produto.IdVendedor
                        WHERE IdVenda = @id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@id", item.ToString()));
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ProdutoVendido produtoVendido = new ProdutoVendido();
                        produtoVendido.Id = int.Parse(reader["IdProdutoVendido"].ToString());
                        produtoVendido.IdProduto = int.Parse(reader["IdProduto"].ToString());
                        produtoVendido.IdVenda = int.Parse(reader["IdVenda"].ToString());
                        produtoVendido.PrecoPago = float.Parse(reader["Preco_Pago"].ToString());
                        produtoVendido.Quantidade = int.Parse(reader["Quantidade"].ToString());
                        produtoVendido.Status = int.Parse(reader["Status"].ToString());
                        listaProdutoVendido.Add(produtoVendido);
                    }
                }
                catch (Exception ex)
                {
                    result.MsgCatch = ex.ToString();
                    result.MsgError = "Erro ao inserir Id " + item;
                    result.ProccessOk = false;
                    return result;
                }
                finally
                {
                    conn.Close();
                }

                try
                {
                    conn.Open();
                    sql = @"SELECT * FROM Venda WHERE Id = @id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@id", item.ToString()));
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Venda venda = new Venda();
                        venda.Id = int.Parse(reader["Id"].ToString());
                        venda.ValorTotal = float.Parse(reader["ValorTotal"].ToString());
                        venda.IdComprador = int.Parse(reader["IdComprador"].ToString());
                        venda.ListaProdutoVendido = listaProdutoVendido;
                        result.ListaVenda.Add(venda);
                    }
                }
                catch (Exception ex)
                {
                    result.MsgCatch = ex.ToString();
                    result.MsgError = "Erro ao capturar informações da venda";
                    result.ProccessOk = false;
                    return result;
                }
                finally
                {
                    conn.Close();
                }
            }
            result.ProccessOk = true;
            return result;
        }
        public VendaResult CalcularVenda(string id, string status = "")
        {
            VendaResult result = new VendaResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            status = status == "" ? "" : " AND Status=" + status;
            string sql = @"SELECT SUM(Preco_Pago) AS ValorTotal FROM ProdutoVendido WHERE IdVenda=@id" + status;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Venda.ValorTotal = float.Parse(reader["ValorTotal"].ToString());
                }
            }
            catch (Exception ex)
            {
                result.MsgCatch = "Id da venda não existe";
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
        public VendaResult AtualizarVenda(Venda venda)
        {
            VendaResult result = new VendaResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = "UPDATE Venda SET ValorTotal = @valor WHERE Id = @id";

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@valor", venda.ValorTotal));
                cmd.Parameters.Add(new SqlParameter("@id", venda.Id));
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                result.MsgCatch = ex.ToString();
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
