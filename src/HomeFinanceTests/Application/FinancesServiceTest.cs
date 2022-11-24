using AutoFixture;
using HomeFinance.Application.Services;
using HomeFinance.Domain.Models;
using HomeFinance.Infra.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HomeFinanceTests.Application
{
    public class FinancesServiceTest
    {
        [Trait("Category", "Calculos")]
        [Fact(DisplayName = "Adicionar novas Dividas")]
        public async void Finances_AddNovasFinancas_DeveAdicionarNovasFinancas()
        {
            //Arrange
            var dividas = new Fixture().Create<Finances>();

            Mock<IFinanceRepository> moqRepo = new Mock<IFinanceRepository>();

            var calcDivida = new FinancesService(moqRepo.Object);

            //Act
            var somaNovaDivida = await calcDivida.AdicionarNovasDividas(dividas);

            //Assert
            moqRepo.Verify(f => f.AddNewFinance(dividas), Times.Once);
        }

        [Trait("Category","Buscas")]
        [Fact(DisplayName ="Buscar financa por Id")]
        public void Finances_DeveBuscar_Financa_Por_Id()
        {
            //Arrange
            var dividas = new Fixture().Create<List<Finances>>();
            var moqObje = new Mock<IFinanceRepository>();
            var id = dividas.FirstOrDefault().FinancesId;

            moqObje.Setup(fin => fin.GetFinanceById(id).Result).Returns(dividas.FirstOrDefault(x => x.FinancesId == id));

            //Act
            var service = new FinancesService(moqObje.Object);
            var result = service.BuscarFinancaPorId(id).Result;

            //Assert
            Assert.NotNull(result);
            moqObje.Verify(fin => fin.GetFinanceById(id), Times.Once);
        }

        [Trait("Category", "VencimentoDividas")]
        [Fact(DisplayName = "Vencimentos proximos")]
        public void Finances_BuscarVencimentoProximo_VerificarVencimentosCom1DiaDeAntecedencia()
        {
            //Arrange

            var dividas = new List<Finances>
            {
                new Finances{FinancesId = Guid.NewGuid(),
                FinanceName = "Conta exemplo 1",
                DueDate = Convert.ToDateTime("2022-11-23"),
                Price = 500},
                new Finances{FinancesId = Guid.NewGuid(),
                FinanceName = "Conta Exemplo 2",
                DueDate = Convert.ToDateTime("2022-11-15"),
                Price = 500}
            };


            var moqObj = new Mock<IFinanceRepository>();
            moqObj.Setup(fin => fin.GetAllFinances().Result).Returns(dividas);


            var calcDivida = new FinancesService(moqObj.Object);
            //Act
            var vencimentoProx = calcDivida.BuscarVencimentoProximo().Result;

            //Assert
            Assert.Contains("Contas", vencimentoProx, StringComparison.OrdinalIgnoreCase);
        }

        [Trait("Category", "Calculos")]
        [Fact(DisplayName = "Subtrair Gastos")]
        public void Finances_CalcularGastos_DeveAbaterValorFinancasDoValorDaRenda()
        {
            //Arrange
            var dividas = new Fixture().Create<List<Finances>>();

            var renda = 4400;

            var moqObj = new Mock<IFinanceRepository>();
            moqObj.Setup(fin => fin.GetAllFinances().Result).Returns(dividas);

            var calcDivida = new FinancesService(moqObj.Object);

            var valorEsperado = renda - dividas.Sum(div => div.Price);

            //Act
            var result = calcDivida.CalcularGastos(renda).Result;

            //Assert
            Assert.Equal(valorEsperado.ToString(), result.ToString());
        }

        [Trait("Category","Calculos")]
        [Fact(DisplayName ="Somar total financas")]
        public void Finances_SomarTotalFinancas_DeveSomarValorTotal()
        {
            //Arrange
            var dividas = new Fixture().Create<List<Finances>>();
            var moqObj = new Mock<IFinanceRepository>();
            moqObj.Setup(fin => fin.GetAllFinances().Result).Returns(dividas);

            var valorEsperado = dividas.Sum(div => div.Price);

            //Act
            var service = new FinancesService(moqObj.Object);
            var result = service.SomarTotalFinancas().Result;

            //Assert
            Assert.Equal(valorEsperado.ToString(),result.ToString());

        }
    }
}
