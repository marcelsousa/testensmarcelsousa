using Microsoft.VisualStudio.TestTools.UnitTesting;
using Imposto.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imposto.Core.Domain;
using System.Xml.Serialization;

namespace Imposto.Core.Service.Tests
{
    [TestClass()]
    public class NotaFiscalServiceTests
    {
        private static string[] estadosBR = { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO" };

        [TestMethod()]
        public void GerarNotaFiscalTest()
        {
            try
            {
                NotaFiscalService service = new NotaFiscalService();

                var pedidos = gerarPedidosTest(10);

                foreach (var pedido in pedidos)
                {
                    try
                    {
                        service.GerarNotaFiscal(pedido);
                    }
                    catch (Exception ex)
                    {
                        Console.Write("ERRO --------------- \n");
                        Console.Write(XMLLog(pedido));
                        Console.Write("ERRO --------------- \n");

                        Assert.Fail("Erro na Nota", ex);
                        throw ex;
                    }
                }

                bool success = pedidos.Count > 0;
                Assert.IsTrue(success, "Todos os testes executados com sucesso.");
                if (success)
                {
                    Console.Write(String.Format("{0} testes executados", pedidos.Count));
                    Console.Write("TESTES --------------- \n");
                    Console.Write(XMLLog(pedidos));
                    Console.Write("TESTES --------------- \n");
                }

            }
            catch (Exception ex)
            {
                Assert.Fail("Erro Gerar Notas Fiscais", ex);
                throw;
            }           
        }

        public List<Pedido> gerarPedidosTest(int numPedidos)
        {
            if (numPedidos == 0)
                numPedidos = 1;

            List<Pedido> lstPedido = new List<Pedido>();

            for (int p = 0; p < numPedidos; p++)
            {
                Pedido pedido = new Pedido();

                pedido.EstadoOrigem = GetRandomEstado();
                pedido.EstadoDestino = GetRandomEstado();
                pedido.NomeCliente = "TesteNome_" + p;

                int numItems = RandomNumber(1, 10);

                bool brinde = false;

                for (int i = 0; i <= numItems; i++)
                {
                    pedido.ItensDoPedido.Add(
                        new PedidoItem()
                        {
                            Brinde = brinde,
                            CodigoProduto = i.ToString(),
                            NomeProduto = "TesteItem_Num_" + i,
                            ValorItemPedido = GetRandomNumber(0.00, 9999.99)
                        });

                    brinde = !brinde;
                }

                lstPedido.Add(pedido);
            }

            return lstPedido;
        }

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }

        public static double GetRandomNumber(double minimum, double maximum)
        {
            lock (syncLock)
            { // synchronize
                return random.NextDouble() * (maximum - minimum) + minimum;
            }
        }

        public static string GetRandomEstado()
        {
            lock (syncLock)
            { // synchronize
                return estadosBR[RandomNumber(0, estadosBR.Length)];
            }
        }

        public string XMLLog(object obj)
        {
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                XmlSerializer xs = new XmlSerializer(obj.GetType());
                xs.Serialize(sw, obj);
                return sw.ToString();
            }
        }
    }
}