﻿using MaterialSkin;
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

namespace GameStore
{
    public partial class PlatformAdd : MaterialForm
    {
        public PlatformAdd()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Purple800, Primary.Purple700, Primary.Purple800, Accent.DeepPurple100, TextShade.WHITE);
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            string platname = materialTextBox1.Text;

            MySqlConnection connection = DataBase.GetDBConnection();
            connection.Open();

            if (platname != null)
            {
                try
                {
                    // Команда Insert.
                    string sql = "INSERT into platforms (name) value ('" + platname + "')";

                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Запись добавлена!");
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
            else
            {
                MessageBox.Show("Заполните поле!");
            }
        }
    }
}
