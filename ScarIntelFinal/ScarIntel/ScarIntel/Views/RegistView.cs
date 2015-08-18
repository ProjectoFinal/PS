using SarcIntelService.Ocorrencias;
using SarcIntelService.PersonView;
using ScarIntel.BrokerService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScarIntel.Views
{
    public partial class RegistView : Form
    {

        private readonly ServerServiceClient serverClient;

        public RegistView(ServerServiceClient serverClient)
        {
            InitializeComponent();
             this.serverClient = serverClient;
        }

        private void participanteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void perfilToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Description_TextChanged(object sender, EventArgs e)
        {

        }

        private void date_time_box_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void participanteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var person = new RegistoCriminal(serverClient);
            person.ShowDialog(this);
        }

        private void participantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistoCriminal regist = new RegistoCriminal(serverClient);
            regist.ShowDialog(this);
        }

        private void registosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OcorrView rc = new OcorrView(serverClient);
            rc.ShowDialog(this);
        }

        private void RegistView_Load(object sender, EventArgs e)
        {

        }

        private void pessoasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm m = new MainForm(serverClient);
            m.ShowDialog(this);
        }
    }
}
