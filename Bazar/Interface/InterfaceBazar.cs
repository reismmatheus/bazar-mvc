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
        public VendedorResult EditarVendedor(string id, Vendedor vendedor)
        {
            vendedor.Id = int.Parse(id);
            return new VendedorRepository(_sqlConn).AtualizarVendedor(vendedor);
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
        public BaseResult CalcularVenda(string id)
        {
            BaseResult result = new VendaResult();
            VendaRepository repository = new VendaRepository(_sqlConn);
            result = repository.CalcularVenda(id);
            if (!result.ProccessOk)
            {
                return result;
            }

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
                result.ProccessOk = false;
                result.MsgCatch = "Erro ao Adicionar Venda";
                return result;
            }

            foreach (var produto in venda.ListaProdutoVendido)
            {
                var adicionarProdutos = new ProdutoVendidoRepository(_sqlConn).AdicionarProdutoVendido(produto);
            }

            return result;
        }
        public VendaResult EditarVenda(string id, Venda venda)
        {
            venda.Id = int.Parse(id);
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
        #endregion

        public ListaProdutoVendidoResult GetProdutosVendidos()
        {
            return new ProdutoVendidoRepository(_sqlConn).ListarProdutosVendidos();
        }

    }
}
