using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;

namespace GameStore
{
    public partial class DevsForm : MaterialForm
    {
        public DevsForm()
        {
            InitializeComponent();

            materialLabel1.Text = userInfo.fioUser;
            materialLabel2.Text = userInfo.roleUser;
            GridView();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Purple800, Primary.Purple700, Primary.Purple800, Accent.DeepPurple100, TextShade.WHITE);
        }

        public static MySqlConnection
                GetDBConnection(string host, int port, string database, string username, string password)
        {
            // Connection String.
            String connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }

        private void GridView()
        {
            MySqlConnection connection = DataBase.GetDBConnection();
            connection.Open();
            string sql = $"SELECT * from devs";

            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            MySqlDataReader reader = cmd.ExecuteReader();

            List<string[]> data = new();

            while (reader.Read())
            {
                data.Add(new string[2]);

                data[^1][0] = reader[0].ToString();
                data[^1][1] = reader[1].ToString();
            }
            reader.Close();
            connection.Close();


            dataGridView1.Rows.Clear();
            dataGridView1.RowsDefaultCellStyle = dataGridView1.DefaultCellStyle;
            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(64, 64, 64);

            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            userInfo.fioUser = null;
            userInfo.roleUser = null;

            MessageBox.Show("Выход успешен!");

            ActiveForm.Hide();
            Auth form = new Auth();
            form.ShowDialog();
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            DevAdd form = new DevAdd();
            form.ShowDialog();

            GridView();
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = DataBase.GetDBConnection();
            connection.Open();
            string sql = $"DELETE FROM devs WHERE id= '{dataGridView1.CurrentCell.Value}'";

            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            connection.Close();
            connection.Dispose();
            connection = null;

            GridView();
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            GridView();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void materialLabel2_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel1_Click(object sender, EventArgs e)
        {

        }

        private void materialComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string c = materialComboBox1.Text;
            if (c == "Игры")
            {
                ActiveForm.Hide();

                GamesForm form = new GamesForm();
                form.ShowDialog();
            }
            else if (c == "Жанры")
            {
                ActiveForm.Hide();

                GenresForm form = new GenresForm();
                form.ShowDialog();
            }
            else if (c == "Продано")
            {
                ActiveForm.Hide();

                SoldForm form = new SoldForm();
                form.ShowDialog();
            }
            else if (c == "Типы продуктов")
            {
                ActiveForm.Hide();

                TypesForm form = new TypesForm();
                form.ShowDialog();
            }
            else if (c == "Платформы")
            {
                ActiveForm.Hide();

                PlatformsForm form = new PlatformsForm();
                form.ShowDialog();
            }
        }
    }
}
