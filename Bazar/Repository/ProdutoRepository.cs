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
    public class ProdutoRepository
    {
        private BaseRepository _sqlConn = new BaseRepository();
        public ProdutoRepository(BaseRepository sqlConn)
        {
            _sqlConn = sqlConn;
        }
        public ProdutoResult AdicionarProduto(Produto produto)
        {
            ProdutoResult result = new ProdutoResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = "INSERT INTO Produto(Nome, Preco, IdVendedor, Quantidade) VALUES(@nome, @preco, @idVendedor, @quantidade)";

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@nome", produto.Nome));
                cmd.Parameters.Add(new SqlParameter("@preco", produto.Preco));
                cmd.Parameters.Add(new SqlParameter("@idVendedor", produto.IdVendedor));
                cmd.Parameters.Add(new SqlParameter("@quantidade", produto.Quantidade));
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
        public ProdutoResult GetProduto(int id)
        {
            ProdutoResult result = new ProdutoResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = "SELECT * FROM Produto WHERE id=@id";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Produto.Id = int.Parse(reader["Id"].ToString());
                    result.Produto.Nome = reader["Nome"].ToString();
                    result.Produto.Preco = float.Parse(reader["Preco"].ToString());
                    result.Produto.Quantidade = int.Parse(reader["Quantidade"].ToString());
                    result.Produto.IdVendedor = int.Parse(reader["IdVendedor"].ToString());
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
        public ListaProdutoResult ListarProdutos()
        {
            ListaProdutoResult result = new ListaProdutoResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = "SELECT * FROM Produto";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Produto produto = new Produto();
                    produto.Id = int.Parse(reader["Id"].ToString());
                    produto.Nome = reader["Nome"].ToString();
                    produto.Preco = float.Parse(reader["Preco"].ToString());
                    produto.Quantidade = int.Parse(reader["Quantidade"].ToString());
                    produto.IdVendedor = int.Parse(reader["IdVendedor"].ToString());
                    result.ListaProduto.Add(produto);
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

        public ProdutoResult AtualizarProduto(Produto produto)
        {
            ProdutoResult result = new ProdutoResult();
            SqlConnection conn = new SqlConnection(_sqlConn.SqlConnection);
            string sql = "UPDATE Produto SET Nome = @nome, Preco = @preco, Quantidade = @quantidade, IdVendedor = @idVendedor WHERE Id = @id";

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@nome", produto.Nome));
                cmd.Parameters.Add(new SqlParameter("@preco", produto.Preco));
                cmd.Parameters.Add(new SqlParameter("@quantidade", produto.Quantidade));
                cmd.Parameters.Add(new SqlParameter("@idVendedor", produto.IdVendedor));
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                result.ProccessOk = false;
                result.MsgError = ex.ToString();
                return result;
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