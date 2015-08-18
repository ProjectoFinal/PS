using System;
using System.Windows.Forms;

namespace SarcIntelService
{
    public partial class InfoForm : Form
    {
        public InfoForm()
        {
            InitializeComponent();
        }

        public void Add(String s)
        {
            textBox1.Text = s + "\n";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}