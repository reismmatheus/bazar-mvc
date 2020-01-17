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

        public EstatisticaResult GetEstatisticas()
        {
            return new EstatisticaRepository(_sqlConn).GetEstatisticas();
        }

        #region Comprador
        public ListaCompradorResult GetCompradores()
        {
            return new CompradorRepository(_sqlConn).ListarComprador();
        }
        public CompradorResult GetComprador(string id)
        {
            return new CompradorRepository(_sqlConn).GetComprador(id);
        }
        public CompradorResult AdicionarComprador(Comprador comprador)
        {
            return new CompradorRepository(_sqlConn).AdicionarComprador(comprador);
        }
        public CompradorResult EditarComprador(string id, Comprador comprador)
        {
            comprador.Id = int.Parse(id);
            return new CompradorRepository(_sqlConn).AtualizarComprador(comprador);
        }
        #endregion

        #region Vendedor 
        public ListaVendedorResult GetVendedores()
        {
            return new VendedorRepository(_sqlConn).ListarVendedores();
        }
        public VendedorResult GetVendedor(string id)
        {
            return new VendedorRepository(_sqlConn).GetVendedor(id);
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
        public VendaResult GetVenda(string id)
        {
            return new VendaRepository(_sqlConn).GetVenda(id);
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
        public ListaProdutoResult GetProdutos()
        {
            return new ProdutoRepository(_sqlConn).ListarProdutos();
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

            return result;
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

        #endregion

    }
}
