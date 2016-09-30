using Imposto.Core.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Data
{
    public class NotaFiscalRepository
    {
        public void SaveNF(NotaFiscal notaFiscal)
        {
            SqlTransaction trans = null;

            try
            {
                string path = ConfigurationManager.AppSettings["connSQL"];
                var connSQL = new SqlConnection("Server=WINDEV\\SQLEXPRESS;DataBase=Teste;Integrated Security=true;");

                using (connSQL)
                {
                    connSQL.Open();
                    trans = connSQL.BeginTransaction();
                    AddNota(connSQL, notaFiscal, trans);
                    AddItem(connSQL, notaFiscal, trans);
                    trans.Commit();
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
        }

        private void AddNota(SqlConnection conn, NotaFiscal notaFiscal, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand("P_NOTA_FISCAL", conn, trans);

            cmd.CommandType = CommandType.StoredProcedure;
            var pIdNotaFiscal = new SqlParameter("@pId", notaFiscal.Id);
            pIdNotaFiscal.Direction = ParameterDirection.InputOutput;
            cmd.Parameters.Add(pIdNotaFiscal);

            cmd.Parameters.Add(new SqlParameter("@pNumeroNotaFiscal", notaFiscal.NumeroNotaFiscal));
            cmd.Parameters.Add(new SqlParameter("@pSerie", notaFiscal.Serie));
            cmd.Parameters.Add(new SqlParameter("@pNomeCliente", notaFiscal.NomeCliente.ToString()));
            cmd.Parameters.Add(new SqlParameter("@pEstadoDestino", notaFiscal.EstadoDestino.ToString()));
            cmd.Parameters.Add(new SqlParameter("@pEstadoOrigem", notaFiscal.EstadoDestino.ToString()));

            cmd.ExecuteNonQuery();
            notaFiscal.Id = Convert.ToInt32(pIdNotaFiscal.Value);

        }

        private void AddItem(SqlConnection conn, NotaFiscal notaFiscal, SqlTransaction trans)
        {
            for (int i = 0; i < notaFiscal.ItensDaNotaFiscal.Count; i++)
            {
                SqlCommand cmd = new SqlCommand("P_NOTA_FISCAL_ITEM", conn, trans);

                cmd.CommandType = CommandType.StoredProcedure;
                var pIdItem = new SqlParameter("@pId", notaFiscal.Id);
                pIdItem.Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add(pIdItem);

                cmd.Parameters.Add(new SqlParameter("@pIdNotaFiscal", notaFiscal.NumeroNotaFiscal));
                cmd.Parameters.Add(new SqlParameter("@pCfop", notaFiscal.ItensDaNotaFiscal[i].Cfop.ToString()));
                cmd.Parameters.Add(new SqlParameter("@pTipoIcms", notaFiscal.ItensDaNotaFiscal[i].TipoIcms.ToString()));
                cmd.Parameters.Add(new SqlParameter("@pBaseIcms", notaFiscal.ItensDaNotaFiscal[i].BaseIcms));
                cmd.Parameters.Add(new SqlParameter("@pAliquotaIcms", notaFiscal.ItensDaNotaFiscal[i].AliquotaIcms));
                cmd.Parameters.Add(new SqlParameter("@pValorIcms", notaFiscal.ItensDaNotaFiscal[i].ValorIcms));
                cmd.Parameters.Add(new SqlParameter("@pNomeProduto", notaFiscal.ItensDaNotaFiscal[i].NomeProduto.ToString()));
                cmd.Parameters.Add(new SqlParameter("@pCodigoProduto", notaFiscal.ItensDaNotaFiscal[i].CodigoProduto.ToString()));
                cmd.Parameters.Add(new SqlParameter("@pBaseIpi", notaFiscal.ItensDaNotaFiscal[i].BaseIpi));
                cmd.Parameters.Add(new SqlParameter("@pAliquotaIpi", notaFiscal.ItensDaNotaFiscal[i].AliquotaIpi));
                cmd.Parameters.Add(new SqlParameter("@pValorIpi", notaFiscal.ItensDaNotaFiscal[i].ValorIpi));
                cmd.Parameters.Add(new SqlParameter("@pValorDesconto", notaFiscal.ItensDaNotaFiscal[i].ValorDesconto));

                cmd.ExecuteNonQuery();
                notaFiscal.ItensDaNotaFiscal[i].Id = Convert.ToInt32(pIdItem.Value);
            }
        }
    }
}
