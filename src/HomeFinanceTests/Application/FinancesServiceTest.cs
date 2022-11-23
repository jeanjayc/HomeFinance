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
        public void Deve_Add_Novas_Dividas()
        {
            //Arrange
            var dividas = new Fixture().Create<Finances>();

            Mock<IFinanceRepository> moqRepo = new Mock<IFinanceRepository>();

            var calcDivida = new FinancesService(moqRepo.Object);

            //Act
            var somaNovaDivida = calcDivida.AdicionarNovasDividas(dividas);

            //Assert
            moqRepo.Verify(f => f.AddNewFinance(dividas), Times.Once);
        }

        [Trait("Category", "VencimentoDividas")]
        [Fact(DisplayName = "Vencimentos proximos")]
        public void Deve_Notificar_Vencimento_Divida_Com1DiaDeAntecedencia()
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
        public void SubtrairGastos_Deve_SomarTodosGastos_E_Abater_DA_Renda_Mensal()
        {
            //Arrange
            var dividas = new List<Finances>
            {
                new Finances{FinancesId = Guid.NewGuid(),
                FinanceName = "Conta exemplo 1",
                DueDate = Convert.ToDateTime("2022-11-23"),
                Price = 500},
                new Finances{FinancesId = Guid.NewGuid(),
                FinanceName = "conta exemplo 2",
                DueDate = Convert.ToDateTime("2022-11-15"),
                Price = 500}
            };

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

        public static IEnumerable<object[]> Data()
        {
            yield return new object[] { new List<decimal> { 100, 1000, 200 } };
        }
        [Trait("Category","Calculos")]
        [Fact(DisplayName ="Somar total financas")]
        public void SomarGastos_Deve_Somar_Total_De_Todas_As_Financas()
        {
            //Arrange
            var dividas = new List<Finances>
            {
                new Finances
                {
                    FinancesId = Guid.NewGuid(),
                    FinanceName = "Conta exemplo 1",
                    DueDate = Convert.ToDateTime("2022-11-23"),
                    Price = 500
                },
                new Finances
                {
                    FinancesId = Guid.NewGuid(),
                    FinanceName = "conta exemplo 2",
                    DueDate = Convert.ToDateTime("2022-11-15"),
                    Price = 500
                },
                new Finances
                {
                    FinancesId = Guid.NewGuid(),
                    FinanceName = "conta exemplo 3",
                    DueDate = Convert.ToDateTime("2022-11-15"),
                    Price = 200.30M
                }
            };
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
