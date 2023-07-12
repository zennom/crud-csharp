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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                Conexao = new MySqlConnection(data_source);

                string sql = "INSERT INTO contato (nome,email,telefone)" +
                   "VALUES "+
                    "(' "+txtNome.Text+"','"+txtEmail.Text+"' , '"+txtTelefone.Text+" ')";

                MySqlCommand comando = new MySqlCommand(sql, Conexao);
                Conexao.Open();
                comando.ExecuteReader();
                MessageBox.Show("Cadastro inserido com sucesso!");

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
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

                string q = " '%" + txt_buscar.Text + "%' ";
                //SELECT * FROM CONTATO WHERE NOME %VIVIANE%

                Conexao = new MySqlConnection(data_source);
                
                
                string sql = "SELECT * " + "FROM contato " +
                    "WHERE nome LIKE "+q+"OR email"+q;

                MySqlCommand comando = new MySqlCommand(sql, Conexao);
                Conexao.Open();
                comando.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                Conexao.Close();
            }
        }
    }
}
