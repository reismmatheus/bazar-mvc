using Bazar.Class;
using Bazar.Repository;
using Bazar.Result;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazar.Interface
{
    public class InterfaceBazar
    {
        BaseRepository _sqlConn = new BaseRepository();
        public InterfaceBazar()
        {
            _sqlConn.SqlConnection = @"Data Source=INDNOTDEV002\SQLEXPRESS;Initial Catalog=Bazar;Integrated Security=True";
        }

        public EstatisticaResult GetEstatisticas(int id = 0)
        {
            EstatisticaResult result = new EstatisticaResult();
            if(id == 0)
            {
                result = new EstatisticaRepository(_sqlConn).GetEstatisticas();
            }
            else
            {
                result = new EstatisticaRepository(_sqlConn).GetEstatisticasVendedor(id);
            }
            return result;
        }

        #region Comprador
        public ListaCompradorResult GetCompradores()
        {
            return new CompradorRepository(_sqlConn).ListarComprador();
        }
        public CompradorResult GetComprador(int id)
        {
            return new CompradorRepository(_sqlConn).GetComprador(id);
        }
        public CompradorResult AdicionarComprador(Comprador comprador)
        {
            return new CompradorRepository(_sqlConn).AdicionarComprador(comprador);
        }
        public CompradorResult EditarComprador(Comprador comprador)
        {
            return new CompradorRepository(_sqlConn).AtualizarComprador(comprador);
        }
        #endregion

        #region Vendedor 
        public ListaVendedorResult GetVendedores()
        {
            return new VendedorRepository(_sqlConn).ListarVendedores();
        }
        public VendedorResult GetVendedor(int id)
        {
            return new VendedorRepository(_sqlConn).GetVendedor(id);
        }

        public VendedorResult GetVendedorByIdUser(string idUser)
        {
            return new VendedorRepository(_sqlConn).GetVendedorByIdUser(idUser);
        }
        public VendedorResult AdicionarVendedor(Vendedor vendedor)
        {
            return new VendedorRepository(_sqlConn).AdicionarVendedor(vendedor);
        }
        #endregion

        #region Venda
        public ListaVendaResult GetVendas()
        {
            return new VendaRepository(_sqlConn).ListarVendas();
        }
        public ListaVendaResult GetVendasDetalhadas()
        {
            return new VendaRepository(_sqlConn).ListarVendas();
        }
        public VendaResult CalcularVenda(Venda venda)
        {
            VendaResult result = new VendaResult();
            VendaRepository repository = new VendaRepository(_sqlConn);
            var atualizarVenda = repository.AtualizarVenda(venda);
            if (!atualizarVenda.ProccessOk)
            {
                result.MsgCatch = "Erro ao atualizar venda";
                result.ProccessOk = false;
                return result;
            }

            result.ProccessOk = true;
            return result;
        }
        public VendaResult GetVenda(int id)
        {
            VendaResult result = new VendaResult();
            var venda = new VendaRepository(_sqlConn).GetVenda(id);
            if (!venda.ProccessOk)
            {
                result.ProccessOk = venda.ProccessOk;
                result.MsgError = venda.MsgError;
                result.MsgCatch = venda.MsgCatch;
                return result;
            }
            result.Venda = venda.Venda;
            var produtosVendidos = new ProdutoVendidoRepository(_sqlConn).ListarProdutosVendidos(id);
            if (!produtosVendidos.ProccessOk)
            {
                result.ProccessOk = produtosVendidos.ProccessOk;
                result.MsgError = produtosVendidos.MsgError;
                result.MsgCatch = produtosVendidos.MsgCatch;
                return result;
            }
            result.Venda.ListaProdutoVendido = produtosVendidos.ListaProdutoVendido;
            result.ProccessOk = true;
            return result;
        }
        public VendaResult AdicionarVenda(Venda venda)
        {
            VendaResult result = new VendaResult();
            var adicionarVenda = new VendaRepository(_sqlConn).AdicionarVenda(venda);
            if (!adicionarVenda.ProccessOk)
            {
                result.MsgError = adicionarVenda.MsgError;
                result.MsgCatch = adicionarVenda.MsgCatch;
                result.ProccessOk = false;
                return result;
            }
            result.Venda.Id = adicionarVenda.Venda.Id;

            foreach (var produto in venda.ListaProdutoVendido)
            {
                produto.IdVenda = adicionarVenda.Venda.Id;
                var adicionarProdutos = new ProdutoVendidoRepository(_sqlConn).AdicionarProdutoVendido(produto);
                if (!adicionarVenda.ProccessOk)
                {
                    result.ProccessOk = false;
                    result.MsgCatch = "Erro ao Adicionar Produto na Venda";
                    return result;
                }
            }

            result.ProccessOk = true;
            return result;
        }
        public VendaResult EditarVenda(Venda venda)
        {
            return new VendaRepository(_sqlConn).AtualizarVenda(venda);
        }
        #endregion

        #region Produto
        public ListaProdutoResult GetProdutos(int idVendedor = 0)
        {
            return new ProdutoRepository(_sqlConn).ListarProdutos(idVendedor);
        }
        public ProdutoResult GetProduto(int id)
        {
            return new ProdutoRepository(_sqlConn).GetProduto(id);
        }
        public ProdutoResult AdicionarProduto(Produto produto)
        {
            return new ProdutoRepository(_sqlConn).AdicionarProduto(produto);
        }
        public ProdutoResult DiminuirQuantidadeProduto(Produto produto, int quantidade)
        {
            ProdutoResult result = new ProdutoResult();
            ProdutoRepository repository = new ProdutoRepository(_sqlConn);

            var getProduto = repository.GetProduto(produto.Id);
            if (!getProduto.ProccessOk)
            {
                return result;
            }
            if(getProduto.Produto.Quantidade - quantidade < 0)
            {
                return result;
            }

            produto.Quantidade -= quantidade;
            var diminuirProduto = repository.AtualizarProduto(produto);
            if (!diminuirProduto.ProccessOk)
            {
                return result;
            }

            result.ProccessOk = true;
            return result;
        }
        
        public ProdutoResult AtualizarProduto(Produto produto)
        {
            return new ProdutoRepository(_sqlConn).AtualizarProduto(produto);
        }
        #endregion

        #region Produtos Vendidos

        public ListaProdutoVendidoResult GetProdutosVendidos(Venda venda = null)
        {
            int idVenda = 0;

            if(venda != null)
            {
                idVenda = venda.Id;
            }

            return new ProdutoVendidoRepository(_sqlConn).ListarProdutosVendidos(idVenda);
        }

        public ProdutoVendidoResult AdicionarProdutoVendido(ProdutoVendido produtoVendido)
        {
            return new ProdutoVendidoRepository(_sqlConn).AdicionarProdutoVendido(produtoVendido);
        }

        #endregion

    }
}
