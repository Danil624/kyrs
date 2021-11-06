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
    public partial class film : Form
    {
        string connStr = "server = osp74.ru;user= st_1_7;database = st_1_7;password = 27770934;port = 33333";
        MySqlConnection conn;
        MySqlCommand comm;
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        private BindingSource bSource = new BindingSource();
        string index_selected_rows;
        private DataSet ds = new DataSet();
        private DataTable table = new DataTable();
        string id_selected_rows;
        string Query;
        DataTable dataTable;
        MySqlDataAdapter sqladapter;
        public film()
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

        private void film_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection(connStr);
         
            GetListUsers();
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;
            dataGridView1.Columns[3].Visible = true;
            dataGridView1.Columns[4].Visible = true;
            dataGridView1.Columns[5].Visible = true;
            dataGridView1.Columns[6].Visible = true;
            dataGridView1.Columns[7].Visible = true;

            //dataGidview1.Columns[0].fillweight = 10;
            //dataGridview1.columns[1].fillweight = 50;
            //datagridview1.columns[2].fillweight = 20;
            //datagridview1.columns[3].fillweight = 50;
            //datagridview1.columns[4].fillweight = 20;
            //datagridview1.columns[5].fillweight = 50;
            //datagridview1.columns[6].fillweight = 20;
            //datagridview1.columns[7].fillweight = 20;

            //Режим для полей "Только для чтения"
            //dataGridView1.Columns[0].ReadOnly = true;
            //dataGridView1.Columns[1].ReadOnly = true;
            //dataGridView1.Columns[2].ReadOnly = true;
            //dataGridView1.Columns[3].ReadOnly = true;
            //Растягивание полей грида

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;
            //Показываем заголовки столбцов
            conn = new MySqlConnection(connStr);
            //Показываем заголовки столбцов
            dataGridView1.ColumnHeadersVisible = true;


            Query = "select * from film";
            comm = new MySqlCommand(Query, conn);
            sqladapter = new MySqlDataAdapter();
            dataTable = new DataTable();
            sqladapter.SelectCommand = comm;
            sqladapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            textBox1.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox6.Visible = false;
        }

        //Метод наполнения DataGreed
        public void GetListUsers()
        {


            string commandStr = "SELECT imya AS 'название',data_time AS 'время',dlina AS 'длительность', reintinf AS 'рейтинг',opisanie AS'описание',akter AS 'актеры',years AS 'год' FROM film";
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
            dob_fil gg = new dob_fil();
            gg.ShowDialog();
        }

        public void delete_user(int id)
        {
            string del = "DELETE FROM film WHERE id_film = " + id;
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

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            izmen iz = new izmen();
            iz.ShowDialog();
        }

        

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (checkBox1.Checked || checkBox3.Checked || checkBox4.Checked || checkBox6.Checked) // если хотя бы один вид выбрано
            {
                if (checkBox1.Checked)
                {
                    if (textBox1.Text != "")
                    {

                        DataView DV = new DataView(dataTable);

                        DV.RowFilter = string.Format("imya LIKE '%{0}%'", textBox1.Text);

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


                if (checkBox3.Checked)
                {
                    if (textBox3.Text != "")
                    {

                        DataView DV = new DataView(dataTable);

                        DV.RowFilter = string.Format("data_time LIKE '%{0}%'", textBox3.Text);

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

                


                if (checkBox4.Checked)
                {
                    if (textBox4.Text != "")
                    {

                        DataView DV = new DataView(dataTable);

                        DV.RowFilter = string.Format("reintinf LIKE '%{0}%'", textBox4.Text);

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





                if (checkBox6.Checked)
                {
                    if (textBox6.Text != "")
                    {

                        DataView DV = new DataView(dataTable);

                        DV.RowFilter = string.Format("years LIKE '%{0}%'", textBox6.Text);

                        dataGridView1.DataSource = DV;
                    }
                    else
                    {
                        error = true;
                    }

                    if (error)
                    {
                        MessageBox.Show("Заполните поле поиска для выбранного критерия!");
                        return;
                    }
                }
                              
            } else
                    MessageBox.Show("Выберите хотя бы один критерий поиска!");
        }

      
           
        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                textBox3.Visible = true;
                textBox3.Text = String.Empty;
            }
            else
                textBox3.Visible = false;
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.Visible = true;
                textBox1.Text = String.Empty;
            }
            else
                textBox1.Visible = false;
        }

       

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                textBox4.Visible = true;
                textBox4.Text = String.Empty;
            }
            else
                textBox4.Visible = false;
        }

      
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                textBox6.Visible = true;
                textBox6.Text = String.Empty;
            }
            else
                textBox6.Visible = false;
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            reload_list();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            GetSelectedIDString();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}
