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
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Logging;
using MySql.Data.MySqlClient;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace GameStore
{
    public partial class GameRefreshRecord : MaterialForm
    {
        public GameRefreshRecord()
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
            string sql2 = "SELECT name FROM genres";
            string sql3 = "SELECT name FROM devs";
            string sql4 = "SELECT name FROM platforms";
            string sql5 = "SELECT name FROM types";

            MySqlCommand cmd1 = connection.CreateCommand();
            MySqlCommand cmd2 = connection.CreateCommand();
            MySqlCommand cmd3 = connection.CreateCommand();
            MySqlCommand cmd4 = connection.CreateCommand();
            MySqlCommand cmd5 = connection.CreateCommand();

            cmd1.CommandText = sql1;
            cmd2.CommandText = sql2;
            cmd3.CommandText = sql3;
            cmd4.CommandText = sql4;
            cmd5.CommandText = sql5;

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
            MySqlDataReader reader3 = cmd3.ExecuteReader();
            while (reader3.Read())
            {
                materialComboBox3.Items.Add(reader3[0]).ToString();
            }
            reader3.Close();

            MySqlDataReader reader4 = cmd4.ExecuteReader();
            while (reader4.Read())
            {
                materialComboBox4.Items.Add(reader4[0]).ToString();
            }
            reader4.Close();
            MySqlDataReader reader5 = cmd5.ExecuteReader();
            while (reader5.Read())
            {
                materialComboBox5.Items.Add(reader5[0]).ToString();
            }
            reader5.Close();
            connection.Close();
            connection.Dispose();
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {

            string gamename = materialComboBox1.SelectedItem.ToString();
            string genre = materialComboBox2.SelectedItem.ToString();
            string dev = materialComboBox3.SelectedItem.ToString();
            string platform = materialComboBox4.SelectedItem.ToString();
            string rating = materialTextBox1.Text;
            string type = materialComboBox5.SelectedItem.ToString();
            string price = materialTextBox2.Text;

            MySqlConnection connection = DataBase.GetDBConnection();
            connection.Open();
            try
            {

                string sql = $"UPDATE games SET genre= '{genre}', dev= '{dev}', platform= '{platform}', rating= '{rating}', type= '{type}', price= '{price}' WHERE name = '{gamename}'";

                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();

                MessageBox.Show("Запись обновлена!");
                ActiveForm.Hide();
            }
            catch(Exception exc)
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
    }
}
