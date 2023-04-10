using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic.Logging;

namespace GameStore
{
    public partial class SellGame : MaterialForm
    {
        public SellGame()
        {
            InitializeComponent();

            FillListBoxes();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Purple800, Primary.Purple700, Primary.Purple800, Accent.DeepPurple100, TextShade.WHITE);
        }

        private void FillListBoxes()
        {
            MySqlConnection connection = DataBase.GetDBConnection();
            connection.Open();

            string sql1 = "SELECT name FROM games";
            string sql2 = "SELECT fio FROM users";
            

            MySqlCommand cmd1 = connection.CreateCommand();
            MySqlCommand cmd2 = connection.CreateCommand();
         

            cmd1.CommandText = sql1;
            cmd2.CommandText = sql2;

            MySqlDataReader reader1 = cmd1.ExecuteReader();
            while (reader1.Read())
            {
                materialComboBox1.Items.Add(reader1[0]).ToString();
            }
            reader1.Close();

            MySqlDataReader reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                materialComboBox2.Items.Add(reader2[0]).ToString();
            }
            reader2.Close();
            connection.Close();
            connection.Dispose();
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            string gamename = materialComboBox1.SelectedItem.ToString();
            string username = materialComboBox2.SelectedItem.ToString();
            string price = materialComboBox3.SelectedItem.ToString();


            MySqlConnection connection = DataBase.GetDBConnection();
            connection.Open();
            try
            {
                // Команда Insert.
                string sql = "INSERT into sold (date, game, user, price) values ('" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH-mm-ss") + "','" + gamename + "','" + username + "','" + price + "')";

                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                MessageBox.Show("Продажа подтверждена!");
                ActiveForm.Hide();
            }

            catch (Exception exc)
            {
                Console.WriteLine("Error: " + exc);
                Console.WriteLine(exc.StackTrace);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }
        }

        private void materialComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            materialComboBox3.Items.Clear();
            MySqlConnection connection = DataBase.GetDBConnection();
            connection.Open();

            string sql3 = $"SELECT price FROM games WHERE name = '{materialComboBox1.Text}' ";
            MySqlCommand cmd3 = connection.CreateCommand();
            cmd3.CommandText = sql3;

            MySqlDataReader reader3 = cmd3.ExecuteReader();
            while (reader3.Read())
            {
                materialComboBox3.Items.Add(reader3[0]).ToString();
            }
            reader3.Close();
        }
    }
}
