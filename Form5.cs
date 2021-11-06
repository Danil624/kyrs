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
    public partial class Form5 : Form
    {
        string connStr = "server = osp74.ru;user= st_1_7;database = st_1_7;password = 27770934;port = 33333";
        //Переменная соединения
        MySqlConnection conn;
        //DataAdapter представляет собой объект Command , получающий данные из источника данных.
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        //Объявление BindingSource, основная его задача, это обеспечить унифицированный доступ к источнику данных.
        private BindingSource bSource = new BindingSource();
        //DataSet - расположенное в оперативной памяти представление данных, обеспечивающее согласованную реляционную программную 
        //модель независимо от источника данных.DataSet представляет полный набор данных, включая таблицы, содержащие, упорядочивающие 
        //и ограничивающие данные, а также связи между таблицами.
        private DataSet ds = new DataSet();
        //Представляет одну таблицу данных в памяти.
        private DataTable table = new DataTable();
        //Переменная для ID записи в БД, выбранной в гриде
        string id_selected_rows;
        string index_selected_rows;
        public Form5()
        {
            InitializeComponent();
        }
        public void GetSelectedIDString()
        {
            //Переменная для индекс выбранной строки в гриде
            string index_selected_rows;
            //Индекс выбранной строки
            index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
            //ID конкретной записи в Базе данных, на основании индекса строки
            id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
            //Указываем ID выделенной строки в метке
            toolStripLabel4.Text = id_selected_rows;
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            //Инициализируем соединение с БД
            conn = new MySqlConnection(connStr);
            //Вызываем метод для заполнение дата Грида
            GetListUsers();
            //Видимость полей в гриде
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;

            dataGridView1.Columns[0].FillWeight = 10;
            dataGridView1.Columns[1].FillWeight = 10;
            dataGridView1.Columns[2].FillWeight = 20;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;
            //Показываем заголовки столбцов
            dataGridView1.ColumnHeadersVisible = true;
        }
        public void GetListUsers()
        {
            //Запрос для вывода строк в БД
            string commandStr = "SELECT id AS 'Код',zal AS 'zal',dlinelnost AS 'dl' FROM seans";
            //Открываем соединение
            conn.Open();
            //Объявляем команду, которая выполнить запрос в соединении conn
            MyDA.SelectCommand = new MySqlCommand(commandStr, conn);
            //Заполняем таблицу записями из БД
            MyDA.Fill(table);
            //Указываем, что источником данных в bindingsource является заполненная выше таблица
            bSource.DataSource = table;
            //Указываем, что источником данных ДатаГрида является bindingsource 
            dataGridView1.DataSource = bSource;
            //Закрываем соединение
            conn.Close();
            //Отражаем количество записей в ДатаГриде
            toolStripLabel2.Text = (dataGridView1.RowCount - 1).ToString();
        }
        public void reload_list()
        {
            //Чистим виртуальную таблицу
            table.Clear();
            //Вызываем метод получения записей, который вновь заполнит таблицу
            GetListUsers();
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
            //dataGridView1.CurrentRow.Selected = true;

            //Метод получения ID выделенной строки в глобальную переменную
            GetSelectedIDString();
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Метод получения ID выделенной строки в глобальную переменную
            GetSelectedIDString();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //Объявляем и инициализируем построитель команд и привязываем его к Адаптеру
            MySqlCommandBuilder cb = new MySqlCommandBuilder(MyDA);
            //Обновляем таблицу (виртуальную)
            MyDA.Update(table);
            //Вызываем метод обновления грида
            reload_list();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6();
            f.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            reload_list();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
        private void button2_Click(object sender, EventArgs e)
        {
            index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
            MessageBox.Show("Индекс выбранной строки" + index_selected_rows);
            id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
            MessageBox.Show("Содержимое поля Код, в выбранной строке" + id_selected_rows);
            delete_user(Convert.ToInt32(id_selected_rows));
            reload_list();
        }
    }
}
