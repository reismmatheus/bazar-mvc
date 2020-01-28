using Bazar.Result;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazar.Repository
{
    public class EstatisticaRepository : BaseRepository
    {
        private BaseRepository _sqlConn = new BaseRepository();
        public EstatisticaRepository(BaseRepository sqlConn)
        {
            _sqlConn = sqlConn;
        }
        public EstatisticaResult GetEstatisticas()
        {
            EstatisticaResult result = new EstatisticaResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = "SELECT COUNT(*) AS Compradores FROM Comprador";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Compradores = int.Parse(reader["Compradores"].ToString());
                }

            }
            catch (Exception ex)
            {
                result.MsgCatch = "Erro ao contas os Compradores";
                result.ProccessOk = false;
                result.MsgError = ex.ToString();
                return result;
            }
            finally
            {
                conn.Close();
            }
            
            sql = "SELECT COUNT(*) AS Vendedores FROM Vendedor";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Vendedores = int.Parse(reader["Vendedores"].ToString());
                }

            }
            catch (Exception ex)
            {
                result.MsgCatch = "Erro ao contas os Vendedores";
                result.ProccessOk = false;
                result.MsgError = ex.ToString();
                return result;
            }
            finally
            {
                conn.Close();
            }

            sql = "SELECT COUNT(*) AS Produtos FROM Produto";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Produtos = int.Parse(reader["Produtos"].ToString());
                }

            }
            catch (Exception ex)
            {
                result.MsgCatch = "Erro ao contas os Produtos";
                result.ProccessOk = false;
                result.MsgError = ex.ToString();
                return result;
            }
            finally
            {
                conn.Close();
            }

            sql = "SELECT COUNT(*) AS ProdutosVendidos FROM ProdutoVendido";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.ProdutosVendidos = int.Parse(reader["ProdutosVendidos"].ToString());
                }

            }
            catch (Exception ex)
            {
                result.MsgCatch = "Erro ao contas os Produtos";
                result.ProccessOk = false;
                result.MsgError = ex.ToString();
                return result;
            }
            finally
            {
                conn.Close();
            }

            sql = "SELECT COUNT(*) AS Vendas FROM Venda";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Vendas = int.Parse(reader["Vendas"].ToString());
                }

            }
            catch (Exception ex)
            {
                result.MsgCatch = "Erro ao contas os Produtos";
                result.ProccessOk = false;
                result.MsgError = ex.ToString();
                return result;
            }
            finally
            {
                conn.Close();
            }

            sql = "SELECT COUNT(*) AS ProdutoVendido from ProdutoVendido WHERE Status = 1";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.ProdutosPagos = int.Parse(reader["ProdutoVendido"].ToString());
                }

            }
            catch (Exception ex)
            {
                result.MsgCatch = "Erro ao contas os ProdutoPagos";
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
        public EstatisticaResult GetEstatisticasVendedor(int idVendedor)
        {
            EstatisticaResult result = new EstatisticaResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = "SELECT COUNT(*) AS Compradores FROM Comprador";

            sql = "SELECT COUNT(*) AS Produtos FROM Produto WHERE IdVendedor = " + idVendedor;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Produtos = int.Parse(reader["Produtos"].ToString());
                }

            }
            catch (Exception ex)
            {
                result.MsgCatch = "Erro ao contas os Produtos";
                result.ProccessOk = false;
                result.MsgError = ex.ToString();
                return result;
            }
            finally
            {
                conn.Close();
            }

            sql = @"SELECT COUNT(*) AS ProdutosVendidos FROM ProdutoVendido
                    INNER JOIN Produto
                    ON Produto.Id = ProdutoVendido.IdProduto
                    WHERE IdVendedor = " + idVendedor;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.ProdutosVendidos = int.Parse(reader["ProdutosVendidos"].ToString());
                }

            }
            catch (Exception ex)
            {
                result.MsgCatch = "Erro ao contas os Produtos";
                result.ProccessOk = false;
                result.MsgError = ex.ToString();
                return result;
            }
            finally
            {
                conn.Close();
            }

            sql = @"SELECT COUNT(*) AS ProdVendido FROM ProdutoVendido
                    INNER JOIN Produto
                    ON Produto.Id = ProdutoVendido.IdProduto
                    WHERE IdVendedor = " + idVendedor + " AND Status = 1";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.ProdutosPagos = int.Parse(reader["ProdVendido"].ToString());
                }

            }
            catch (Exception ex)
            {
                result.MsgCatch = "Erro ao contas os ProdutoPagos";
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
    }
}
