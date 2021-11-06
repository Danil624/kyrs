using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace кино
{
    public partial class админ : Form
    {
        public админ()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            film d = new film();
            d.ShowDialog();
        }

        private void админ_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 x = new Form3();
            x.ShowDialog();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 f =new Form5();
            f.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            админ r = new админ();
            r.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 a = new Form5();
            a.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bilet LL = new bilet();
            LL.ShowDialog();
        }
    }
}
