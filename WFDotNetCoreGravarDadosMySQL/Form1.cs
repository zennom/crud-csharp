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
        //aqui vamos criar as informações para conectar nosso banco
        MySqlConnection Conexao;
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
                string data_source = "datasource=localhost;username=root;password=;database=db_agenda";

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
            //finally sempre será executado após o try ou catch
            finally
            {
                Conexao.Close();
            }
       
        }
    }
}
