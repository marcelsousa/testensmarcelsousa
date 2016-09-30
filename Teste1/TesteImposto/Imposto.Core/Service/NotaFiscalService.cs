using Imposto.Core.Data;
using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Service
{
    public class NotaFiscalService
    {
        string[] estadosSudeste = { "SP", "MG", "ES", "RJ" };
        public void GerarNotaFiscal(Domain.Pedido pedido)
        {
            //Emite nova Nota Fiscal
            NotaFiscal notaFiscal = EmitirNotaFiscal(pedido);

            //Gera XML
            var xmlGerado = GerarXML(notaFiscal);

            //Se XML ok Persiste na base de dados
            if (xmlGerado)
                new NotaFiscalRepository().SaveNF(notaFiscal);
        }

        /// <summary>
        /// Gerar XML da Nota Fiscal 
        /// </summary>
        /// <param name="notaFiscal"> Nota fiscal </param>
        /// <param name="path"> Diretório do arquivo a ser salvo </param>
        public bool GerarXML(NotaFiscal notaFiscal)
        {
            try
            {
                string path = ConfigurationManager.AppSettings["pathXMLNotaFiscal"];
                string fileName = "NF_" + notaFiscal.NumeroNotaFiscal + "_ID_" + notaFiscal.Id + "_SR_" + notaFiscal.Serie + ".xml";
                Common.XmlManager.CriarXML(notaFiscal, path, fileName);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NotaFiscal EmitirNotaFiscal(Pedido pedido)
        {
            NotaFiscal nf = new Domain.NotaFiscal();
            nf.NumeroNotaFiscal = 99999;
            nf.Serie = new Random().Next(Int32.MaxValue);
            nf.NomeCliente = pedido.NomeCliente;

            nf.EstadoDestino = pedido.EstadoOrigem.ToUpper();
            nf.EstadoOrigem = pedido.EstadoDestino.ToUpper();

            var itens = new List<NotaFiscalItem>();

            foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
            {
                NotaFiscalItem notaFiscalItem = new NotaFiscalItem();

                notaFiscalItem.Brinde = itemPedido.Brinde;
                notaFiscalItem.ValorItem = itemPedido.ValorItemPedido;

                if (estadosSudeste.Contains(nf.EstadoDestino))
                {
                    notaFiscalItem.ValorDesconto = notaFiscalItem.ValorItem * 0.10;
                }

                CalcularCfop(nf, ref notaFiscalItem);
                CalcularIpi(ref notaFiscalItem);

                if (nf.EstadoDestino == nf.EstadoOrigem)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                }
                else
                {
                    notaFiscalItem.TipoIcms = "10";
                    notaFiscalItem.AliquotaIcms = 0.17;
                }

                if (notaFiscalItem.Cfop == "6.009")
                {
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido * 0.90; //redução de base
                }
                else
                {
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido;
                }

                notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;

                if (itemPedido.Brinde)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                    notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
                }

                notaFiscalItem.NomeProduto = itemPedido.NomeProduto;
                notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;

                itens.Add(notaFiscalItem);
            }

            nf.ItensDaNotaFiscal = itens;

            return nf;
        }

        private void CalcularIpi(ref NotaFiscalItem notaFiscalItem)
        {
            notaFiscalItem.AliquotaIpi = notaFiscalItem.Brinde ? 0 : 0.10;
            notaFiscalItem.ValorIpi = notaFiscalItem.AliquotaIpi * notaFiscalItem.ValorItem;
        }

        private void CalcularCfop(NotaFiscal nf, ref NotaFiscalItem notaFiscalItem)
        {
            notaFiscalItem.Cfop = "";
            //Eliminei os Ifs em excesso, e a regra para estados destino eram as mesmas para SP e MG e só existiam para eles
            //Reduzi pra um único IF e um Switch entre os estados de destino.
            if (nf.EstadoOrigem == "SP" || nf.EstadoOrigem == "MG")
            {
                switch (nf.EstadoDestino)
                {
                    //"SE" possuia dois IFs pela regra antiga o último seria o setado, sendo assim comentei o primeiro.
                    case "RJ": notaFiscalItem.Cfop = "6.000"; break;
                    case "PE": notaFiscalItem.Cfop = "6.001"; break;
                    case "MG": notaFiscalItem.Cfop = "6.002"; break;
                    case "PB": notaFiscalItem.Cfop = "6.003"; break;
                    case "PR": notaFiscalItem.Cfop = "6.004"; break;
                    case "PI": notaFiscalItem.Cfop = "6.005"; break;
                    case "RO": notaFiscalItem.Cfop = "6.006"; break;
                    //case "SE" : notaFiscalItem.Cfop = "6.007"; break;
                    case "TO": notaFiscalItem.Cfop = "6.008"; break;
                    case "SE": notaFiscalItem.Cfop = "6.009"; break;
                    case "PA": notaFiscalItem.Cfop = "6.010"; break;
                };
            }
        }
    }
}