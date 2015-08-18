using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using SarcIntelService.Ocorrencias;
using ScarIntel;
using ScarIntel.BrokerService;
using ScarIntel.Views;
using ScarIntel.Views.DocumentViews;

namespace SarcIntelService.PersonView
{
    public partial class PersonView : Form
    {
        private readonly Person person;
        private readonly ServerServiceClient serverClient;
        private ScarIntel.BrokerService.Regist[] registos;

        public PersonView(ServerServiceClient serverClient,Person p)
        {
            InitializeComponent();

            person = p;
            nameBoxText.Text = p.Name;
            birthBoxText.Text = p.Birthday.ToShortDateString();
            moradaBoxText.Text = p.Address;

            TimeSpan x = DateTime.Now - p.Birthday;
            idadeBoxText.Text = (x.Days/365).ToString();
           
            nacioBoxText.Text = p.Birthplace;
            this.serverClient = serverClient;

            try
            {
                SampleGenericDelegate<int, Regist[]> del = new SampleGenericDelegate<int, Regist[]>(serverClient.GetRegists);

                IAsyncResult result = del.BeginInvoke(person.Id, null, null);

                registos = del.EndInvoke(result);

                if (registos != null)
                    SetRegists();

            }
            catch (Exception e)
            {
                var info = new InfoForm();
                info.Add(e.Message);
                info.ShowDialog(this);
                info.Dispose();
            }
        }
        private void SetRegists()
        {
            foreach (Regist regist in registos)
            {
                AddRegist(regist);
            }
        }

        private void AddRegist(Regist r)
        {

            SampleGenericDelegate<int, CrimeType[]> del = new SampleGenericDelegate<int, CrimeType[]>(serverClient.GetAllCrimeTypeByRegist);

            IAsyncResult result = del.BeginInvoke(r.Id, null, null);


            var crimes = del.EndInvoke(result);

            DataGridViewComboBoxColumn comb = (DataGridViewComboBoxColumn)dataGridView2.Columns[1];
            foreach (CrimeType c in crimes)
                comb.Items.Add(c.Name);

            dataGridView2.Rows.Add(new object[] { r.Id, crimes[0].Name, r.Date.ToShortDateString() });

        }
        public void SetImage(BiometricType bio)
        {
            if (bio == null)
                return;
            byte[] photo = bio.data;
            if (photo == null || photo.Length == 0)
            {
                return;
            }

            var memStream = new MemoryStream();
            Task t = memStream.WriteAsync(photo, 0, photo.Length);

            t.ContinueWith(result =>
            {
                memStream.Position = 0;
                this.photo.Image = Image.FromStream(memStream, true, true);
                this.photo.SizeMode = PictureBoxSizeMode.StretchImage;
            });
        }


        private void ocorrenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }


        private void documentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var doc = new DocumentEditor(serverClient, person);
            doc.ShowDialog(this);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void documentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var doc = new DocumentView(serverClient, person);
            doc.ShowDialog(this);
        }


        private void biometriaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void registoCriminalToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
           }

        private void visualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PersonView_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}