using MaterialSkin;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace GameStore
{
    public partial class RegForm : MaterialForm
    {
        public RegForm()
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

        private void materialButton2_Click(object sender, EventArgs e)
        {
            string fio = materialTextBox1.Text;
            string login = materialTextBox3.Text;
            string passwd = GetMD5Hash(materialTextBox6.Text);
            string checkpasswd = GetMD5Hash(materialTextBox5.Text);
            string adminpass = materialTextBox4.Text;
            string role = materialTextBox2.Text;

            if (fio != "" && role != "" && login != "" && passwd != "" && passwd == checkpasswd && adminpass == "yaneadmin")
            {
                MySqlConnection connection = DataBase.GetDBConnection();
                connection.Open();
                try
                {
                    // Команда Insert.
                    string sql = "INSERT into users (fio, login, password, role) values ('" + fio + "','" + login + "','" + passwd + "','" + role + "')";

                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Регистрация прошла успешно!");
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

                    ActiveForm.Hide();
                    Auth Form = new Auth();
                    Form.ShowDialog();
                }
            }

            else if(adminpass != "yaneadmin")
            {
                MessageBox.Show("Неверный код администратора!");
            }

            else if(passwd != checkpasswd)
            {
                MessageBox.Show("Пароли не совпадают!");
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            ActiveForm.Hide();
            Auth Form = new Auth();
            Form.ShowDialog();
        }
    }
}