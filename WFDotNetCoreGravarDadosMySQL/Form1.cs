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

namespace WFDotNetCoreGravarDadosMySQL
{
    public partial class Form1 : Form
    {
        private MySqlConnection Conexao;
        private string data_source = "datasource=localhost;username=root;password=;database=db_agenda";


        private int ?id_contato_selecionado = null;
     
        public Form1()
        {
            InitializeComponent();

            lst_contatos.View = View.Details;
            lst_contatos.AllowColumnReorder = true;
            lst_contatos.FullRowSelect = true;
            lst_contatos.GridLines = true;

            lst_contatos.Columns.Add("ID", 30, HorizontalAlignment.Left);
            lst_contatos.Columns.Add("Nome", 150, HorizontalAlignment.Left);
            lst_contatos.Columns.Add("Email", 150, HorizontalAlignment.Left);
            lst_contatos.Columns.Add("Telefone", 150, HorizontalAlignment.Left);

            CarregarContatos();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;
         

                if (id_contato_selecionado == null)
                {
                    cmd.CommandText = "INSERT INTO contato (nome,email,telefone) VALUES (@nome,@email,@telefone) ";
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Contato inserido com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //atualização de contato
                    cmd.CommandText = "UPDATE contato SET nome=@nome, email=@email, telefone=@telefone WHERE id=@id";
                    cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
                    cmd.Parameters.AddWithValue("@id",id_contato_selecionado);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Contato atualizado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                txtNome.Text = string.Empty;
                txtEmail.Text = "";
                txtTelefone.Text = "";

                CarregarContatos();
            }

            catch (MySqlException ex) {
               
                MessageBox.Show("Erro Ocorreu: " + ex.Message,"Erro ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }
       
        }
        private void button2_Click(object sender, EventArgs e)
        {
           try
            {
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = Conexao;

                cmd.CommandText = "SELECT * FROM contato WHERE nome LIKE @q OR email LIKE  @q";

                cmd.Parameters.AddWithValue("@q", "%"+txt_buscar.Text+"%");

                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();

                lst_contatos.Items.Clear();

                while(reader.Read())
                {   
                    string[] row =
                    {
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                    };

                    lst_contatos.Items.Add(new ListViewItem(row));
                }
            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Erro Ocorreu: " + ex.Message, "Erro ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }
        }

        private void CarregarContatos()
        {
            try
            {
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Conexao;
                cmd.CommandText = "SELECT * FROM contato ORDER BY id desc";
                cmd.Prepare();
                MySqlDataReader reader = cmd.ExecuteReader();
                lst_contatos.Items.Clear();

                while (reader.Read())
                {
                    string[] row =
                    {
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                    };

                    lst_contatos.Items.Add(new ListViewItem(row));
                }
            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Erro Ocorreu: " + ex.Message, "Erro ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }
        }

        private void lst_contatos_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListView.SelectedListViewItemCollection itens_selecionados = lst_contatos.SelectedItems;

            foreach(ListViewItem item in itens_selecionados)
            {
                id_contato_selecionado = Convert.ToInt32(item.SubItems[0].Text);

                txtNome.Text = item.SubItems[1].Text;
                txtEmail.Text = item.SubItems[2].Text;
                txtTelefone.Text = item.SubItems[3].Text;

              

            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //vamos por uma mensagem de teste aqui
            MessageBox.Show("Vou excluir");
        }
    }
}
