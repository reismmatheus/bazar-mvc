﻿using Bazar.Class;
using Bazar.Result;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazar.Repository
{
    public class ProdutoVendidoRepository
    {
        private BaseRepository _sqlConn = new BaseRepository();
        public ProdutoVendidoRepository(BaseRepository sqlConn)
        {
            _sqlConn = sqlConn;
        }
        public ProdutoVendidoResult AdicionarProdutoVendido(ProdutoVendido produto)
        {
            ProdutoVendidoResult result = new ProdutoVendidoResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = "INSERT INTO ProdutoVendido(PrecoPago, Status, Quantidade, IdProduto, IdVenda) VALUES(@precoPago, @status, @quantidade, @idProduto, @idVenda)";

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@precoPago", produto.PrecoPago));
                cmd.Parameters.Add(new SqlParameter("@status", produto.Status));
                cmd.Parameters.Add(new SqlParameter("@quantidade", produto.Quantidade));
                cmd.Parameters.Add(new SqlParameter("@idProduto", produto.IdProduto));
                cmd.Parameters.Add(new SqlParameter("@idVenda", produto.IdVenda));
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
        public ProdutoVendidoResult GetProdutoVendido(int id)
        {
            ProdutoVendidoResult result = new ProdutoVendidoResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = @"SELECT * FROM ProdutoVendido WHERE Id=@id";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.ProdutoVendido.Id = int.Parse(reader["Id"].ToString());
                    result.ProdutoVendido.PrecoPago = int.Parse(reader["PrecoPago"].ToString());
                    result.ProdutoVendido.Status = reader["Status"].ToString() == "0" ? false : true;
                    result.ProdutoVendido.Quantidade = int.Parse(reader["Quantidade"].ToString());
                    result.ProdutoVendido.IdProduto = int.Parse(reader["IdProduto"].ToString());
                    result.ProdutoVendido.IdVenda = int.Parse(reader["IdVenda"].ToString());
                }
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
            return result;
        }
        
        /// <summary>
        /// Pega todos produtos vendidos, se houver idVenda, pega todos da venda.
        /// </summary>
        /// <param name="idVenda">IdVenda (Opcional)</param>
        /// <returns>ListaProdutoVendidoResult</returns>
        public ListaProdutoVendidoResult ListarProdutosVendidos(int idVenda = 0)
        {
            ListaProdutoVendidoResult result = new ListaProdutoVendidoResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string idSql = idVenda != 0 ? " WHERE IdVenda=" + idVenda : "";
            string sql = @"SELECT * FROM ProdutoVendido" + idSql;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ProdutoVendido produtoVendido = new ProdutoVendido();
                    produtoVendido.Id = int.Parse(reader["Id"].ToString());
                    produtoVendido.PrecoPago = float.Parse(reader["PrecoPago"].ToString());
                    produtoVendido.Status = reader["Status"].ToString() == "False" ? false : true;
                    produtoVendido.Quantidade = int.Parse(reader["Quantidade"].ToString());
                    produtoVendido.IdProduto = int.Parse(reader["IdProduto"].ToString());
                    produtoVendido.IdVenda = int.Parse(reader["IdVenda"].ToString());
                    result.ListaProdutoVendido.Add(produtoVendido);
                }
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
        public ProdutoVendidoResult AtualizarProdutoVendido(ProdutoVendido produtoVendido)
        {
            ProdutoVendidoResult result = new ProdutoVendidoResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = "UPDATE ProdutoVendido SET PrecoPago = @preco, Status = @status, Quantidade = @quantidade WHERE Id = @id";

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@preco", produtoVendido.PrecoPago));
                cmd.Parameters.Add(new SqlParameter("@status", produtoVendido.Status));
                cmd.Parameters.Add(new SqlParameter("@quantidade", produtoVendido.Quantidade));
                cmd.Parameters.Add(new SqlParameter("@id", produtoVendido.Id));
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
        //public void ExcluirProdutoVendido()
        //{
        //    SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
        //    string sql = "DELETE ProdutoVendido WHERE Id = @id";

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
