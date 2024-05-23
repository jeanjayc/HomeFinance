using AutoFixture;
using HomeFinance.Application.Services;
using HomeFinance.Domain.Models;
using HomeFinance.Infra.DTOs.Response.Financas;
using HomeFinance.Infra.Interfaces;
using HomeFinance.Infra.Interfaces.DAO;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HomeFinanceTests.Application
{
    public class FinancesServiceTest
    {
        private readonly IFixture _fixture;

        public FinancesServiceTest()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Trait("Category", "CRUD")]
        [Fact(DisplayName = "Adicionar novas Dividas")]
        public async void Finances_AddNovasFinancas_DeveAdicionarNovasFinancas()
        {
            //Arrange
            var dividas = new Fixture().Create<Finances>();

            var moqDAOObj = new Mock<IFinancaDAO>();
            var moqRepoObj = new Mock<IFinanceRepository>();

            var service = new FinancesService(moqRepoObj.Object, moqDAOObj.Object);

            //Act
            await service.AdicionarNovasDividas(dividas);

            //Assert
            moqRepoObj.Verify(f => f.AdicionarNovaDivida(dividas), Times.Once);
        }

        [Trait("Category", "CRUD")]
        [Fact(DisplayName = "Buscar todas as financas")]
        public async void Finaces_BuscarFinancas_DeveRetornarTodasAsFinancas()
        {
            //Arrange
            var dividas = _fixture.Create<List<Finances>>();
            var moqDAOObj = new Mock<IFinancaDAO>();
            var moqRepoObj = new Mock<IFinanceRepository>();
            moqRepoObj.Setup(fin => fin.ListarTodasDividas().Result).Returns(dividas);

            //Act
            var service = new FinancesService(moqRepoObj.Object, moqDAOObj.Object);
            var result = await service.BuscarTodasFinancas();

            //Assert
            Assert.IsType<List<Finances>>(result);
            Assert.NotNull(result);
        }

        [Trait("Category", "CRUD")]
        [Fact(DisplayName = "Buscar financa por Id")]
        public void Finances_BuscarFinancaPorId_DeveBuscarFinancaPeloId()
        {
            //Arrange
            var dividas = _fixture.Create<List<Finances>>();
            var moqDAOObj = new Mock<IFinancaDAO>();
            var moqRepoObj = new Mock<IFinanceRepository>();
            var id = dividas.FirstOrDefault().FinancaId;

            moqRepoObj.Setup(fin => fin.ObterFinancaPorId(id).Result).Returns(dividas.FirstOrDefault(x => x.FinancaId == id));

            //Act
            var service = new FinancesService(moqRepoObj.Object, moqDAOObj.Object);
            var result = service.BuscarFinancaPorId(id).Result;

            //Assert
            Assert.NotNull(result);
            moqRepoObj.Verify(fin => fin.ObterFinancaPorId(id), Times.Once);
        }

        [Trait("Category", "CRUD")]
        [Fact(DisplayName = "Buscar financas por nome")]
        public async void Finances_BuscarFinancaPorNome_DeveBuscarFinancaPeloNome()
        {
            //Arrange
            var dividas = _fixture.Create<IEnumerable<FinancaDTO>>();
            var moqDAOObj = new Mock<IFinancaDAO>();
            var moqRepoObj = new Mock<IFinanceRepository>();
            var nome = dividas.FirstOrDefault().DescricaoFinanca;

            moqDAOObj.Setup(fin => fin.ObterFinancaPorDescricao(nome).Result).Returns(dividas.FirstOrDefault(div => div.DescricaoFinanca == nome));

            //Act
            var service = new FinancesService(moqRepoObj.Object,moqDAOObj.Object);
            var result = await service.BuscarFinancaPorNome(nome);

            //Assert
            Assert.NotNull(result);
        }

        [Trait("Category", "VencimentoDividas")]
        [Fact(DisplayName = "Vencimentos proximos")]
        public void Finances_BuscarVencimentoProximo_VerificarVencimentosCom1DiaDeAntecedencia()
        {
            //Arrange

            var dividas = _fixture.Create<List<Finances>>();


            var moqDAOObj = new Mock<IFinancaDAO>();
            var moqRepoObj = new Mock<IFinanceRepository>();
            moqRepoObj.Setup(fin => fin.ListarTodasDividas().Result).Returns(dividas);


            var service = new FinancesService(moqRepoObj.Object, moqDAOObj.Object);
            //Act
            var vencimentoProx = service.BuscarVencimentoProximo().Result;

            //Assert
            Assert.Contains("Contas", vencimentoProx, StringComparison.OrdinalIgnoreCase);
        }

        [Trait("Category", "Calculos")]
        [Fact(DisplayName = "Subtrair Gastos")]
        public void Finances_CalcularGastos_DeveAbaterValorFinancasDoValorDaRenda()
        {
            //Arrange
            var dividas = _fixture.Create<List<Finances>>();

            var renda = 4400;

            var moqDAOObj = new Mock<IFinancaDAO>();
            var moqRepoObj = new Mock<IFinanceRepository>();
            moqRepoObj.Setup(fin => fin.ListarTodasDividas().Result).Returns(dividas);

            var service = new FinancesService(moqRepoObj.Object, moqDAOObj.Object);

            var valorEsperado = renda; // - dividas.Sum(div => div.Installments.Sum(fin => fin.Price));

            //Act
            var result = service.CalcularGastos(renda).Result;

            //Assert
            Assert.Equal(valorEsperado.ToString(), result.ToString());
        }

        [Trait("Category", "Calculos")]
        [Fact(DisplayName = "Somar total financas")]
        public void Finances_SomarTotalFinancas_DeveSomarValorTotal()
        {
            //Arrange

            var dividas = _fixture.Create<List<Finances>>();

            var moqDAOObj = new Mock<IFinancaDAO>();
            var moqRepoObj = new Mock<IFinanceRepository>();
            moqRepoObj.Setup(fin => fin.ListarTodasDividas().Result).Returns(dividas);


            //Act
            var service = new FinancesService(moqRepoObj.Object, moqDAOObj.Object);
            var result = service.SomarTotalFinancas().Result;

            ////Assert
            //Assert.Equal(result, result);

        }

        [Trait("Category", "Buscas")]
        [Fact(DisplayName = "Atualizar Financas")]
        public void Finances_AtualizarDadosFinancas_DeveAtualizarDadosFinancas()
        {
            //Arrange

            var dividaExistente = _fixture.Create<Finances>();
            var moqDAOObj = new Mock<IFinancaDAO>();
            var moqRepoObj = new Mock<IFinanceRepository>();
            moqRepoObj.Setup(fin => fin.AtualizarFinanca(dividaExistente).Result).Returns(dividaExistente);

            //Act
            var service = new FinancesService(moqRepoObj.Object, moqDAOObj.Object);
            var result = service.AtualizarDadosFinancas(dividaExistente.FinancaId,dividaExistente);

            //Assert
            Assert.NotNull(result);
            moqRepoObj.Verify(fin => fin.AtualizarFinanca(dividaExistente).Result, Times.Once);
        }

        [Trait("Category", "Buscas")]
        [Fact(DisplayName = "Deletar financa")]
        public void Finances_DeletarFinancas_DeveDeletarFinancaPeloId()
        {
            //Arrange
            var divida = _fixture.Create<Finances>();
            var moqDAOObj = new Mock<IFinancaDAO>();
            var moqRepoObj = new Mock<IFinanceRepository>();
            var idDivida = divida.FinancaId;

            moqRepoObj.Setup(fin => fin.DeletarFinanca(idDivida).Result).Returns(divida);
            //Act
            var service = new FinancesService(moqRepoObj.Object, moqDAOObj.Object);

            //Arrange
            Assert.NotNull(service);
            moqRepoObj.Verify(fin => fin.DeletarFinanca(idDivida).Result, Times.Once);
        }

    }
}
