using Imposto.Core.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Imposto.Core.Domain;

namespace TesteImposto
{
    public partial class frmImposto : Form
    {
        private Pedido pedido = new Pedido();
        private string[] estadosBR = {"AC","AL","AP","AM","BA","CE","DF","ES","GO","MA","MT","MS","MG","PA","PB","PR","PE","PI","RJ","RN","RS","RO","RR","SC","SP","SE","TO"};

        public frmImposto()
        {
            InitializeComponent();
            dgvPedidos.AutoGenerateColumns = true;                       
            dgvPedidos.DataSource = GetTablePedidos();
            ResizeColumns();
        }

        private void ResizeColumns()
        {
            double mediaWidth = dgvPedidos.Width / dgvPedidos.Columns.GetColumnCount(DataGridViewElementStates.Visible);

            for (int i = dgvPedidos.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = dgvPedidos.Columns[i];
                coluna.Width = Convert.ToInt32(mediaWidth);
            }   
        }

        private object GetTablePedidos()
        {
            DataTable table = new DataTable("pedidos");
            table.Columns.Add(new DataColumn("Nome do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Codigo do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Valor", typeof(decimal)));
            table.Columns.Add(new DataColumn("Brinde", typeof(bool)));
                     
            return table;
        }

        private void btnGerarNotaFiscal_Click(object sender, EventArgs e)
        {
            if (!ValidaForm())
                return;

            NotaFiscalService service = new NotaFiscalService();
            pedido.EstadoOrigem = txtEstadoOrigem.Text;
            pedido.EstadoDestino = txtEstadoDestino.Text;
            pedido.NomeCliente = txtNomeCliente.Text;

            DataTable table = (DataTable)dgvPedidos.DataSource;

            foreach (DataRow row in table.Rows)
            {
                var item = new PedidoItem();

                item.Brinde = Convert.ToString(row["Brinde"]) != "";
                item.CodigoProduto = row["Codigo do produto"].ToString();
                item.NomeProduto = row["Nome do produto"].ToString();
                item.ValorItemPedido = Convert.ToDouble(row["Valor"].ToString());

                pedido.ItensDoPedido.Add(item);
            }

            service.GerarNotaFiscal(pedido);
            MessageBox.Show("Operação efetuada com sucesso");
        }

        private void LimparCampos()
        {
            dgvPedidos.DataSource = GetTablePedidos();
            ResizeColumns();
            txtEstadoDestino.Text = "";
            txtEstadoOrigem.Text = "";
            txtNomeCliente.Text = "";
        }

        private bool ValidaForm()
        {
            if (txtNomeCliente.Text == "")
            {
                MessageBox.Show("Preencha o nome do Cliente, por favor.");
                return false;
            }

            if (txtEstadoOrigem.Text == "")
            {
                MessageBox.Show("Preencha o estado de origem, por favor.");
                return false;
            }
            else
            {
                if (!estadosBR.Contains(txtEstadoOrigem.Text))
                {
                    MessageBox.Show("Estado de Origem Inválido, verifique por favor.");
                    return false;
                }
            }

            if (txtEstadoDestino.Text == "")
            {
                MessageBox.Show("Preencha o estado de Destino, por favor.");
                return false;
            }
            else
            {
                if (!estadosBR.Contains(txtEstadoOrigem.Text))
                {
                    MessageBox.Show("Estado de Destino Inválido, verifique por favor.");
                    return false;
                }
            }

            if (((DataTable)dgvPedidos.DataSource).Rows.Count == 0)
            {
                MessageBox.Show("Inclua ao menos 1 (Um) item, por favor.");
                return false;
            }

            return true;
        }
    }
}
