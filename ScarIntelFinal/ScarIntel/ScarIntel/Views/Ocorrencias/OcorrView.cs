using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScarIntel.BrokerService;
using ScarIntel.Views;

namespace SarcIntelService.Ocorrencias
{
    public partial class OcorrView : Form
    {
        private readonly CrimeType[]crimes;
       // private readonly CrimeType crimes;
        private  Regist regist;
        private readonly ServerServiceClient serverClient;
      //  private Person person;
       // private Person[] persons;

        public OcorrView(ServerServiceClient serverClient)
        {
            InitializeComponent();
            this.serverClient = serverClient;
            SampleGenericDelegate2<CrimeType[]> del = new SampleGenericDelegate2<CrimeType[]>(serverClient.GetAllCrimeType);

            IAsyncResult result = del.BeginInvoke(null, null);

            crimes = del.EndInvoke(result);
            foreach (CrimeType c in crimes)
            {
                comboBox1.Items.Add(c.Name);
            }
          
            /*
              comboBox1.SelectedIndex = 0;

            if (regist == null)
            {
               // this.regist = new Regist();
                this.person = person;
                Editable();
            }
            else
            {
                NonEditable();
               // this.person = person;
                this.regist = regist;
                date_time_text.Text = regist.Date.ToShortDateString();
                id_text.Text = regist.Id.ToString();
                Description.Text = regist.Description;
            }
            */
        }
        /*
        private void Editable()
        {
            saveButton.Visible = true;
            Description.Enabled = true;

            date_time_text.Enabled = true;
        }

        private void NonEditable()
        {
            saveButton.Enabled = false;
            Description.Enabled = false;

            date_time_text.Enabled = false;

            comboBox1.Enabled = false;
        }
        */
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Closed += (x, y) => Dispose();
            Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                regist = new Regist();
                regist.Date = dateTimePicker1.Value;
                regist.Description = richTextBox1.Text;
                regist.Code = textBox3.Text;

                SampleGenericDelegate<Regist,int> del = new SampleGenericDelegate<Regist,int>(serverClient.InsertRegist);

                IAsyncResult result = del.BeginInvoke(regist,null, null);

                regist.Id = del.EndInvoke(result);

                 SarcIntelService.PersonView.PersonEditor person = new SarcIntelService.PersonView.PersonEditor(serverClient,regist);
                 person.ShowDialog(this);
               
                person.Close();
                
            }
            catch (Exception exc)
            {
                var info = new InfoForm();
                info.Add(exc.Message);
                info.ShowDialog(this);
                info.Dispose();
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void OcorrView_Load(object sender, EventArgs e)
        {
        }

        private void date_time_text_TextChanged(object sender, EventArgs e)
        {
        }

        private void Description_TextChanged(object sender, EventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }


        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void participanteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}