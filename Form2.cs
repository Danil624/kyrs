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
    public partial class Form2 : Form
    {  string connStr = "server = osp74.ru;user= st_1_7;database = st_1_7;password = 27770934;port = 33333";
            MySqlConnection conn;
        static class class_edit_user
        {
            public static string id { get; set; }
        }
        string dish;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection(connStr);
            //Получаем ID пользователя
            string id = class_edit_user.id;
            //Формируем SQL запрос на выборку определённого доктора
            string sql_current_doctor = "SELECT * FROM sotr WHERE Id=" + id;
            MySqlCommand current_user_command = new MySqlCommand(sql_current_doctor, conn);
            conn.Open();
            MySqlDataReader current_user_reader = current_user_command.ExecuteReader();
            //Получаем текущие значения полей пользователя
            while (current_user_reader.Read())
            {
                textBox1.Text = current_user_reader[1].ToString();
                textBox2.Text = current_user_reader[2].ToString();
               

                //Получение текущего id статуса пользователя

                //Изменение метки формы
                this.Text = "Редактирование пользователя: " + current_user_reader[1];
            }
            conn.Close();
        }
        private void Main3()
        {
            MySqlCommand comm= new MySqlCommand(dish, conn);
          
            try
            {
                conn.Open();
                //update_user.ExecuteNonQuery();
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка изменения пользователя \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            //Новые параметры из полей формы
            string new_fio = textBox1.Text;
            string new_rol = textBox2.Text;
          
            
                conn = new MySqlConnection(connStr);

                if (textBox1.Text.Length > 0)
            {
                //Формируем строку запроса на добавление строк
                string sql_update_user = "UPDATE sotr SET " +
                "fio = '" + new_fio + "', " +
                "rol = '" + new_rol + "', " +
             
                "WHERE Id = " + class_edit_user.id;

                //Посылаем запрос на обновление данных
                MySqlCommand update = new MySqlCommand(sql_update_user, conn);
               
                try
                {
                    conn.Open();
                    update.ExecuteNonQuery();
                    MessageBox.Show("Изменение прошло успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка изменения строки \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Main3();
        }
    }
}
