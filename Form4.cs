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
    public partial class Form4 : Form
    {
        string connStr = "server = osp74.ru;user= st_1_7;database = st_1_7;password = 27770934;port = 33333";
      
        public Form4()
        {
            InitializeComponent();
        }
        private void Insertt()
        {


            //Получение новых параметров пользователя
            string new_fio = textBox1.Text;
            string new_rol = textBox2.Text;
            



            //Проверяем на заполненность поля ФИО
            MySqlConnection conn = new MySqlConnection (connStr);
            if (textBox2.Text.Length > 0)
            {
                //Формируем строку запроса на добавление строк
                string sql_insert_user = " INSERT INTO `sotr` ( `fio`, `rol`)" +
                " VALUES ('" + new_fio + "', '" + new_rol + "')";
              


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
                MessageBox.Show("ФИО пользователя не должно быть пустым", "Информация");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Insertt();
        }
        private void Form4_Load(object sender, EventArgs e)
        {
        }
       
    }
}
