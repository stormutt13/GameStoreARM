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
using Microsoft.VisualBasic.Logging;
using MySql.Data.MySqlClient;

namespace GameStore
{
    public partial class GamesForm : MaterialForm
    {
        public GamesForm()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Purple800, Primary.Purple700, Primary.Purple800, Accent.DeepPurple100, TextShade.WHITE);

            materialLabel1.Text = userInfo.fioUser;
            materialLabel2.Text = userInfo.roleUser;
            GridView();
        }

        private void GridView()
        {
            MySqlConnection connection = DataBase.GetDBConnection();
            connection.Open();
            string sql = $"SELECT * from games";

            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            MySqlDataReader reader = cmd.ExecuteReader();

            List<string[]> data = new();

            while (reader.Read())
            {
                data.Add(new string[8]);

                data[^1][0] = reader[0].ToString();
                data[^1][1] = reader[1].ToString();
                data[^1][2] = reader[2].ToString();
                data[^1][3] = reader[3].ToString();
                data[^1][4] = reader[4].ToString();
                data[^1][5] = reader[5].ToString();
                data[^1][6] = reader[6].ToString();
                data[^1][7] = reader[7].ToString();
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

        private void materialButton2_Click(object sender, EventArgs e)
        {
            GameAddRecord form = new GameAddRecord();
            form.ShowDialog();

            GridView();
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

        private void materialButton3_Click(object sender, EventArgs e)
        {
            GameRefreshRecord form = new GameRefreshRecord();
            form.ShowDialog();

            GridView();
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = DataBase.GetDBConnection();
            connection.Open();
            string sql = $"DELETE FROM games WHERE id= '{dataGridView1.CurrentCell.Value}'";

            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            connection.Close();
            connection.Dispose();
            connection = null;

            GridView();

            MessageBox.Show("Запись удалена!");
        }

        private void materialComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string c = materialComboBox1.Text;
            if (c == "Разработчики")
            {
                ActiveForm.Hide();

                DevsForm form = new DevsForm();
                form.ShowDialog();
            }
            else if(c == "Жанры")
            {
                ActiveForm.Hide();

                GenresForm form = new GenresForm();
                form.ShowDialog();
            }
            else if(c == "Продано")
            {
                ActiveForm.Hide();

                SoldForm form = new SoldForm();
                form.ShowDialog();
            }
            else if(c == "Типы продуктов")
            {
                ActiveForm.Hide();

                TypesForm form = new TypesForm();
                form.ShowDialog();
            }
            else if(c == "Платформы")
            {
                ActiveForm.Hide();

                PlatformsForm form = new PlatformsForm();
                form.ShowDialog();
            }
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            GridView();
        }

        private void materialButton6_Click(object sender, EventArgs e)
        {
            SellGame form = new SellGame();
            form.ShowDialog();

            GridView();
        }
    }
}
