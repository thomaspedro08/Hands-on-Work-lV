using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Hands_on_Work_lll
{
    public partial class Cadastro : Form
    {
        public Cadastro()
        {
            InitializeComponent();
        }

        private void buttonCadastrar_Click(object sender, EventArgs e)
        {


            MySqlConnectionStringBuilder conexaoBD = new MySqlConnectionStringBuilder();
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "fila de espera";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "";

            MySqlConnection connectionBD = new MySqlConnection(conexaoBD.ToString());

            try
            {
                connectionBD.Open();


                MySqlCommand comandoMysql = connectionBD.CreateCommand();
                comandoMysql.CommandText = "INSERT INTO pessoa (idNome, idDataNascimento, idEstado, idCidade) " +
                    "VALUES('" + textNome.Text + "', '" + textData.Text + "', '" + textEstado.Text + "', '" + textCidade.Text + "')";
                comandoMysql.ExecuteNonQuery();

                connectionBD.Close();
                MessageBox.Show("Cadastro Realizado");
                atualizarGrid();
                limparCampos();

            }
            catch (Exception ex)

            {
                MessageBox.Show("Não Foi Possível abrir a conexão");
                Console.WriteLine(ex.Message);

            }


        }

        private void Cadastro_Load(object sender, EventArgs e)
        {
            atualizarGrid();
        }

        private void atualizarGrid()
        {
            MySqlConnectionStringBuilder conexaoBD = new MySqlConnectionStringBuilder();
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "fila de espera";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "";

            MySqlConnection connectionBD = new MySqlConnection(conexaoBD.ToString());

            try
            {
                connectionBD.Open();

                MySqlCommand comandoMySql = connectionBD.CreateCommand();
                comandoMySql.CommandText = "SELECT * from pessoa WHERE ativoPessoa = 0";
                MySqlDataReader reader = comandoMySql.ExecuteReader();

                dataGrid.Rows.Clear();

                while (reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dataGrid.Rows[0].Clone();
                    row.Cells[0].Value = reader.GetInt32(0);
                    row.Cells[1].Value = reader.GetString(1);
                    row.Cells[2].Value = reader.GetString(2);
                    row.Cells[3].Value = reader.GetString(3);
                    row.Cells[4].Value = reader.GetString(4);
                    dataGrid.Rows.Add(row);
                }

                connectionBD.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
                Console.WriteLine(ex.Message);
            }



        }

        private void dataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGrid.CurrentRow.Selected = true;
                
                textNome.Text = dataGrid.Rows[e.RowIndex].Cells["ColumnNome"].FormattedValue.ToString();
                textData.Text = dataGrid.Rows[e.RowIndex].Cells["ColumnData"].FormattedValue.ToString();
                textEstado.Text = dataGrid.Rows[e.RowIndex].Cells["ColumnEstado"].FormattedValue.ToString();
                textCidade.Text = dataGrid.Rows[e.RowIndex].Cells["ColumnCidade"].FormattedValue.ToString();
                textID.Text = dataGrid.Rows[e.RowIndex].Cells["ColumnId"].FormattedValue.ToString();
            }
        }

        private void textEstado_TextChanged(object sender, EventArgs e)
        {

        }

        private void Novo_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void limparCampos()

        {
            textID.Clear();
            textNome.Clear();
            textCidade.Clear();
            textEstado.Clear();
            textData.Clear();
        }

        private void Deletar_Click(object sender, EventArgs e)
        {
            {
                MySqlConnectionStringBuilder conexaoBD = new MySqlConnectionStringBuilder();
                conexaoBD.Server = "localhost";
                conexaoBD.Database = "fila de espera";
                conexaoBD.UserID = "root";
                conexaoBD.Password = "";

                MySqlConnection connectionBD = new MySqlConnection(conexaoBD.ToString());
                try
                {
                    connectionBD.Open(); //Abre a conexão com o banco

                    MySqlCommand comandoMySql = connectionBD.CreateCommand(); //Crio um comando SQL
                    comandoMySql.CommandText = "UPDATE pessoa SET ativoPessoa = 1 WHERE idPessoa = " + textID.Text + "";
                    //comandoMySql.CommandText = "DELETE FROM pessoa WHERE idPessoa = " + textID.Text + "";
                    comandoMySql.ExecuteNonQuery();

                    connectionBD.Close(); // Fecho a conexão com o banco
                    MessageBox.Show("Deletado com sucesso"); //Exibo mensagem de aviso
                    atualizarGrid();
                    limparCampos();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Não foi possivel abrir a conexão! ");
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void atualizar_Click(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder conexaoBD = new MySqlConnectionStringBuilder();
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "fila de espera";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "";

            MySqlConnection connectionBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                connectionBD.Open(); //Abre a conexão com o banco

                MySqlCommand comandoMySql = connectionBD.CreateCommand(); //Crio um comando SQL
                comandoMySql.CommandText = "UPDATE pessoa SET idNome = '" + textNome.Text + "', " +
                    "idDataNascimento = '" + textData.Text + "', " +
                    "idEstado = '" + textEstado.Text + "', " +
                    "idCidade = '" + textCidade.Text + "' WHERE idPessoa = " + textID.Text + "";
                comandoMySql.ExecuteNonQuery();

                connectionBD.Close(); // Fecho a conexão com o banco
                MessageBox.Show("Atualizado com sucesso"); //Exibo mensagem de aviso
                atualizarGrid();
                limparCampos();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Não foi possivel abrir a conexão! ");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
