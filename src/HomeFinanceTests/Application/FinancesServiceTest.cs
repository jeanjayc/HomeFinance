//using HomeFinance.Application.Services;
//using HomeFinance.Domain.Models;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using Xunit;

//namespace HomeFinanceTests.Application
//{
//    public class FinancesServiceTest
//    {
//        [Trait("Category", "Calculos")]
//        [Fact(DisplayName = "Somar valor Total Dividas")]
//        public void Deve_PegarESomar_Valor_TotalDas_Dividas()
//        {
//            //Arrange
//            var dividas = new List<Finances>();

//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Terreno", DueDate = 15, Price = 500 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Santander", DueDate = 22, Price = 100 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Internet", DueDate = 10, Price = 30 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Emp.Nubank", DueDate = 10, Price = 375 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Jefferson", DueDate = 15, Price = 447 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "MEI", DueDate = 30, Price = 335 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Nubank", DueDate = 15, Price = 124 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "CDB", DueDate = 10, Price = 400 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Agua/Luz", DueDate = 10, Price = 250 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Vitoria", DueDate = 10, Price = 100 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Garagem", DueDate = 10, Price = 80 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Ap", DueDate = 15, Price = 510 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Condominio", DueDate = 15, Price = 20 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Academia", DueDate = 10, Price = 140 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Cartão NU", DueDate = 17, Price = 350 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Inter", DueDate = 22, Price = 51 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Gastos do Mes", DueDate = 10, Price = 300 });


//            var calcDivida = new FinancesService();
//            var valorEsperado = dividas.Sum(x => x.Price);

//            //Act
//            var somaNovaDivida = calcDivida.AdicionarNovaDivida(dividas.Select(x => x.Price).ToList());

//            //Assert
//            Assert.Equal(valorEsperado, somaNovaDivida);
//        }

//        [Trait("Category", "VencimentoDividas")]
//        [Fact(DisplayName = "Vencimentos proximos")]
//        public void Deve_Notificar_Vencimento_Divida_Com1DiaDeAntecedencia()
//        {
//            //Arrange
//            var dividas = new List<Finances>();

//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Terreno", DueDate = 15, Price = 500 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Santander", DueDate = 22, Price = 100 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Internet", DueDate = 10, Price = 30 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Emp.Nubank", DueDate = 10, Price = 375 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Jefferson", DueDate = 15, Price = 447 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "MEI", DueDate = 30, Price = 335 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Nubank", DueDate = 15, Price = 124 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "CDB", DueDate = 10, Price = 400 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Agua/Luz", DueDate = 10, Price = 250 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Vitoria", DueDate = 10, Price = 100 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Garagem", DueDate = 10, Price = 80 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Ap", DueDate = 15, Price = 510 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Condominio", DueDate = 15, Price = 20 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Academia", DueDate = 10, Price = 140 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Cartão NU", DueDate = 17, Price = 350 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Inter", DueDate = 22, Price = 51 });
//            //dividas.Add(new Finances { FinancesId = Guid.NewGuid(), FinanceName = "Gastos do Mes", DueDate = 10, Price = 300 });


//            var calcDivida = new FinancesService();
//            //Act
//            var vencimentoProx = calcDivida.BuscarVencimentoProximo(dividas);

//            //Assert
//            Assert.Contains("Contas a vencer", vencimentoProx);
//        }
//        [Trait("Category", "Calculos")]
//        [Theory(DisplayName = "Subtrair Gastos")]
//        [MemberData(nameof(Data))]
//        public void SubtrairGastos_Deve_SomarTodosGastos_E_Abater_Renda_Mensal(List<decimal> gastos)
//        {
//            //Arrange
//            var calcDivida = new FinancesService();

//            var renda = 4400;

//            var valorEsperado = renda - gastos.Sum();

//            //Act
//            var result = calcDivida.CalcularGastos(renda, gastos);

//            //Assert
//            Assert.Equal(valorEsperado,result);
//        }

//        public static IEnumerable<object[]> Data()
//        {
//            yield return new object[] { new List<decimal> { 100, 1000, 200 } };
//        }
//    }
//}
