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
    public partial class bilet : Form
    {
        public bilet()
        {
            InitializeComponent();
        }
        public string connStr = "server = osp74.ru;user= st_1_7;database = st_1_7;password = 27770934;port = 33333";
        MySqlConnection conn;
        string index_selected_rows;
        string id_selected_rows;
        private MySqlDataAdapter DA = new MySqlDataAdapter();
        private BindingSource bSource = new BindingSource();
        private DataTable table = new DataTable();
        private DataTable dataTable;
        string Query;
        MySqlCommand comm;
       
        private void GetL()
        {
            //Запрос для вывода строк в БД
            string commandStr = "SELECT id AS 'Код', zal AS 'зал',number AS 'номер',seans AS 'сеанс' FROM mesta";
            //Открываем соединение
            conn.Open();
            //Объявляем команду, которая выполнить запрос в соединении conn
            DA.SelectCommand = new MySqlCommand(commandStr, conn);
            //Заполняем таблицу записями из БД
            DA.Fill(table);
            //Указываем, что источником данных в bindingsource является заполненная выше таблица
            bSource.DataSource = table;
            //Указываем, что источником данных ДатаГрида является bindingsource 
            dataGridView1.DataSource = bSource;
            //Закрываем соединение
            conn.Close();
            //Отражаем количество записей в ДатаГриде
            toolStripLabel2.Text = (dataGridView1.RowCount - 1).ToString();

        }
        private void ListUpdate()
        {

            table.Clear();
            GetL();
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            //Формируем строку запроса на добавление строк
            string sql_delete_user = "DELETE FROM mesta WHERE id='" + id_selected_rows + "'";
            //Посылаем запрос на обновление данных
            MySqlCommand delete_user = new MySqlCommand(sql_delete_user, conn);
            try
            {
                conn.Open();
                delete_user.ExecuteNonQuery();
                MessageBox.Show("Удаление прошло успешно", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка удаления строки \n\n" + ex, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            finally
            {
                conn.Close();
                //Вызов метода обновления ДатаГрида
                ListUpdate();
            }
        }
        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
            //dataGridView1.CurrentRow.Selected = true;

            //Метод получения ID выделенной строки в глобальную переменную
            GetSelectedIDString();
        }
        public void GetSelectedIDString()
        {
            //Переменная для индекс выбранной строки в гриде
            string index_selected_rows;
            //Индекс выбранной строки
            index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
            //ID конкретной записи в Базе данных, на основании индекса строки
            id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();

        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!e.RowIndex.Equals(-1) && !e.ColumnIndex.Equals(-1) && e.Button.Equals(MouseButtons.Right))
            {
                dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
                //dataGridView1.CurrentRow.Selected = true;
                dataGridView1.CurrentCell.Selected = true;
                //Метод получения ID выделенной строки в глобальную переменную
                GetSelectedIDString();
            }
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
            //dataGridView1.CurrentRow.Selected = true;

            //Метод получения ID выделенной строки в глобальную переменную
            GetSelectedIDString();
        }

        private void GetSel()
        {
            string index_selected_rows;
            //Индекс выбранной строки
            index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
            //ID конкретной записи в Базе данных, на основании индекса строки
            id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
            toolStripLabel1.Text = (dataGridView1.RowCount - 1).ToString();
        }
        private void bilet_Load(object sender, EventArgs e)
        {
            //Инициализируем соединение с БД
            conn = new MySqlConnection(connStr);
            //Вызываем метод для заполнение дата Грида
            GetL();
            //Видимость полей в гриде
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;
            dataGridView1.Columns[3].Visible = true;
            
            //Ширина полей
            dataGridView1.Columns[0].FillWeight = 10;
            dataGridView1.Columns[1].FillWeight = 5;
            dataGridView1.Columns[2].FillWeight = 10;
            dataGridView1.Columns[3].FillWeight = 15;
            //Режим для полей "Только для чтения"
            //dataGridView1.Columns[0].ReadOnly = true;
            //dataGridView1.Columns[1].ReadOnly = true;
            //dataGridView1.Columns[2].ReadOnly = true;
            //dataGridView1.Columns[3].ReadOnly = true;
            //Растягивание полей грида
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode= DataGridViewAutoSizeColumnMode.Fill;
            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;
            //Показываем заголовки столбцов
            dataGridView1.ColumnHeadersVisible = true;
            MySqlDataAdapter list = new MySqlDataAdapter("SELECT id AS 'Код', zal AS 'зал',number AS 'номер',seans AS 'сеанс' FROM mesta", conn);
            bSource.DataSource = table;
            list.Fill(table);
            dataGridView1.DataSource = bSource;
            conn.Close();
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;

            Query = "select * from ";
            comm = new MySqlCommand(Query, conn);
            DA = new MySqlDataAdapter();
            table = new DataTable();
            DA.SelectCommand = comm;
            GetL();
            dataGridView1.DataSource = table;
        }

        private void AddButton_Click_1(object sender, EventArgs e)
        {
            GetSel();
            class_edit_user.id = id_selected_rows;
            bil edit_user = new bil();
            edit_user.ShowDialog();
        }

        private void DeleteButton_Click_1(object sender, EventArgs e)
        {
            index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
            MessageBox.Show("Индекс выбранной строки" + index_selected_rows);
            id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
            MessageBox.Show("Содержимое поля Код, в выбранной строке" + id_selected_rows);
            delete_user(Convert.ToInt32(id_selected_rows));
            ListUpdate();
        }
        public void delete_user(int id)
        {
            string del = "DELETE FROM mesta WHERE id = " + id;
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ListUpdate();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool error = false;
            if (checkBox1.Checked || checkBox2.Checked || checkBox3.Checked ) // если хотя бы один вид выбрано
            {
                if (checkBox1.Checked)
                {
                    if (textBox1.Text != "")
                    {

                        DataView DV = new DataView(dataTable);

                        DV.RowFilter = string.Format("zal LIKE '%{0}%'", textBox1.Text);

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

                        DV.RowFilter = string.Format("number LIKE '%{0}%'", textBox3.Text);

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
            if (checkBox2.Checked)
            {
                if (textBox2.Text != "")
                {

                    DataView DV = new DataView(dataTable);

                    DV.RowFilter = string.Format("mesto LIKE '%{0}%'", textBox2.Text);

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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox2.Visible = true;
                textBox2.Text = String.Empty;
            }
            else
                textBox2.Visible = false;
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

        private void button1_Click(object sender, EventArgs e)
        {
            index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
            MessageBox.Show("Индекс выбранной строки" + index_selected_rows);
            id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
            MessageBox.Show("Содержимое поля Код, в выбранной строке" + id_selected_rows);
            delete_user(Convert.ToInt32(id_selected_rows));
            ListUpdate();
        }
    }
    
}
