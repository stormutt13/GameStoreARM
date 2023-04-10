using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;

namespace GameStore
{
    public partial class Auth : MaterialForm
    {
        public Auth()
        {
            InitializeComponent();

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

        private static string GetMD5Hash(string text)
        {
            using (var hashAlg = MD5.Create())
            {
                byte[] hash = hashAlg.ComputeHash(Encoding.UTF8.GetBytes(text));
                var builder = new StringBuilder(hash.Length * 2);
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("X2"));
                }
                return builder.ToString();
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            string login = materialTextBox1.Text;
            string passwd = GetMD5Hash(materialTextBox2.Text);



            MySqlConnection connection = DataBase.GetDBConnection();
            connection.Open();
            try
            {
                // Команда Select.
                string sql = $"SELECT * from users WHERE login = '{login}' and password = '{passwd}'";

                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows == true)
                {
                    reader.Read();
                    userInfo.fioUser = reader.GetString(1);
                    userInfo.roleUser = reader.GetString(4);

                    MessageBox.Show("Вход произведен успешно!", "Вход");

                    ActiveForm.Hide();
                    GamesForm form = new GamesForm();
                    form.ShowDialog();
                }

                else
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }
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

        private void materialButton2_Click(object sender, EventArgs e)
        {
            ActiveForm.Hide();
            RegForm Form = new RegForm();
            Form.ShowDialog();
        }
    }
}
