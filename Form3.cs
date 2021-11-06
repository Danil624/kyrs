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
    public partial class Form3 : Form
    {
        string connStr = "server = osp74.ru;user= st_1_7;database = st_1_7;password = 27770934;port = 33333";
        MySqlConnection conn;
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        private BindingSource bSource = new BindingSource();
        private DataTable table = new DataTable();
        string id_selected_rows;
        string Query;
        DataTable dataTable;
        MySqlDataAdapter sqladapter;
        MySqlCommand comm;
        string index_selected_rows;
        public Form3()
        {
            InitializeComponent();
          
        
        }

        public void GetSelectedIDString()
        {
            string index_selected_rows;
            //Индекс выбранной строки
            index_selected_rows = dataGridView11.SelectedCells[0].RowIndex.ToString();
            //ID конкретной записи в Базе данных, на основании индекса строки
            id_selected_rows = dataGridView11.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
            //Указываем ID выделенной строки в метке
            toolStripLabel4.Text = id_selected_rows;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection(connStr);
            GetListUsers();
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;
  
           dataGridView1.Columns[0].FillWeight = 10;
            dataGridView1.Columns[1].FillWeight = 50;
            dataGridView1.Columns[2].FillWeight = 20;
           
            //Режим для полей "Только для чтения"
            //dataGridView1.Columns[0].ReadOnly = true;
            //dataGridView1.Columns[1].ReadOnly = true;
            //dataGridView1.Columns[2].ReadOnly = true;
            //dataGridView1.Columns[3].ReadOnly = true;
            //Растягивание полей грида
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
          
            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;
            //Показываем заголовки столбцов
             conn = new MySqlConnection(connStr);
            //Показываем заголовки столбцов
            dataGridView1.ColumnHeadersVisible = true;
          

            Query = "select * from sotr";
            comm = new MySqlCommand(Query, conn);
            sqladapter = new MySqlDataAdapter();
            dataTable = new DataTable();
            sqladapter.SelectCommand = comm;
            sqladapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            textBox1.Visible = false;
            textBox3.Visible = false;
        }

        //Метод наполнения DataGreed
        public void GetListUsers()
        {
   
            
            string commandStr = "SELECT id AS 'Код', fio AS 'ФИО',rol AS роль FROM sotr";
            conn.Open();
            MyDA.SelectCommand = new MySqlCommand(commandStr, conn);
            MyDA.Fill(table);
            bSource.DataSource = table;
            dataGridView1.DataSource = bSource;
            conn.Close();
            toolStripLabel2.Text = (dataGridView1.RowCount - 1).ToString();
        }

        //Метод обновления DataGreed
        public void reload_list()
        {
            //Чистим виртуальную таблицу
            table.Clear();
            //Вызываем метод получения записей, который вновь заполнит таблицу
            GetListUsers();
        }

        //Сылка на удаление из контекстного меню
        private void delToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        //Выделение всей строки по ЛКМ
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
            //dataGridView1.CurrentRow.Selected = true;

            //Метод получения ID выделенной строки в глобальную переменную
            GetSelectedIDString();
        }

        //Выделение всей строки по ПКМ
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!e.RowIndex.Equals(-1) && !e.ColumnIndex.Equals(-1) && e.Button.Equals(MouseButtons.Right))
            {
                dataGridView11.CurrentCell = dataGridView11[e.ColumnIndex, e.RowIndex];
                //dataGridView1.CurrentRow.Selected = true;
                dataGridView11.CurrentCell.Selected = true;
                //Метод получения ID выделенной строки в глобальную переменную
                GetSelectedIDString();
            }
        }

        //Действие на нажатие кнопки изменить в контекстном меню датагрида
        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //Событие, которое срабатывает на клике по ДатаГриду, что бы не плодить две этих строки на каждой кнопке
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Метод получения ID выделенной строки в глобальную переменную
            GetSelectedIDString();
        }

        //Кнопка обновить, сверху формы
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            reload_list();
        }

        //Кнопка сохранения изменений
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //Объявляем и инициализируем построитель команд и привязываем его к Адаптеру
            MySqlCommandBuilder cb = new MySqlCommandBuilder(MyDA);
            //Обновляем таблицу (виртуальную)
            MyDA.Update(table);
            //Вызываем метод обновления грида
            reload_list();
        }
        //добавление
        private void button5_Click(object sender, EventArgs e)
        {
            Form4 gg = new Form4();
            gg.ShowDialog();
        }

        public void delete_user(int id)
        {
            string del = "DELETE FROM sotr WHERE Id = " + id;
            MySqlCommand del_user = new MySqlCommand(del, conn);

            try
            {
                conn.Open();
                del_user.ExecuteNonQuery();
                this.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления пользователя \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }
       

    //кнопка удаление
        private void button6_Click(object sender, EventArgs e)
        {
            index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
            MessageBox.Show("Индекс выбранной строки" + index_selected_rows);
            id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
            MessageBox.Show("Содержимое поля Код, в выбранной строке" + id_selected_rows);
            delete_user(Convert.ToInt32(id_selected_rows));
            reload_list();
        }
  
      //изменение
        private void button4_Click(object sender, EventArgs e)
        {
            class_edit_user.id = id_selected_rows;
            Form2 xz = new Form2();
            xz.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
                String.Format("Country like '{0}%'", textBox1.Text);
        }
        //поиск
        private void button8_Click(object sender, EventArgs e)
        {

            
            bool error = false;
            if (checkBox1.Checked ||  checkBox3.Checked) // если хотя бы один вид выбрано
            {
                if (checkBox1.Checked)
                {
                    if (textBox1.Text != "")
                    {
                  
                        DataView DV = new DataView(dataTable);

                        DV.RowFilter = string.Format("fio LIKE '%{0}%'", textBox1.Text);

                        dataGridView1.DataSource = DV;

                    }
                    else
                    {
                        error = true;
                    }
                }
                if (checkBox3.Checked)
                {
                    if (textBox3.Text != "")
                    {

                        DataView DV = new DataView(dataTable);

                        DV.RowFilter = string.Format("rol LIKE '%{0}%'", textBox3.Text);

                        dataGridView1.DataSource = DV;
                    }
                    else
                    {
                        error = true;
                    }
                }
               

                if (error)
                {
                    MessageBox.Show("Заполните поле поиска для выбранного критерия!");
                    return;
                }
                

            }
            else
                MessageBox.Show("Выберите хотя бы один критерий поиска!");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.Visible = true;
                textBox1.Text = String.Empty;
            }
            else
                textBox1.Visible = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                textBox3.Visible = true;
                textBox3.Text = String.Empty;
            }
            else
                textBox3.Visible = false;
        }

       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        //обновить в верху
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            reload_list();
        }
    }
    }
    

