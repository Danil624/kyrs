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

namespace кино
{
    public partial class dob_fil : Form
    {
        string connStr = "server = osp74.ru;user= st_1_7;database = st_1_7;password = 27770934;port = 33333";

        public dob_fil()
        {
            InitializeComponent();
        }
        private void Insertt()
        {


            //Получение новых параметров пользователя
            string new_fio= textBox1.Text;
            string new_rol = textBox5.Text;
            string new_login = textBox3.Text;
            string new_reintinf = maskedTextBox1.Text;
            string new_opisanie = textBox2.Text;
            string new_akter = textBox4.Text;
            decimal new_years = numericUpDown2.Value;



            //Проверяем на заполненность поля ФИО
            MySqlConnection conn = new MySqlConnection(connStr);
            if (textBox1.Text.Length > 0)
            {
                //Формируем строку запроса на добавление строк
                string sql_insert_user = " INSERT INTO `film` ( `imya`, `data_time`, `dlina`,`reintinf`,`opisanie`,`akter`,`years`)" +
                " VALUES ('" + new_fio + "', '" + new_rol + "', '" + new_login + "','" + new_reintinf + "','"+new_opisanie+"','"+new_akter+"','"+new_years+"')";
              


                //Посылаем запрос на добавление данных
                MySqlCommand insert_user = new MySqlCommand(sql_insert_user, conn);
                try
                {
                    conn.Open();
                    insert_user.ExecuteNonQuery();
                    MessageBox.Show("Добавление пользователя прошло успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка добавления пользователя \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("поля не должны быть пустыми", "Информация");
            }

        }
        private void izm_fil_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Insertt();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            Close();
        }
    }
}
