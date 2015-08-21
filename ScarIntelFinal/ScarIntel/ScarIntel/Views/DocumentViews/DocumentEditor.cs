using System;
using System.Linq;
using System.Windows.Forms;
using ScarIntel.BrokerService;
using SarcIntelService;
using ScarIntel.Views;
using System.Threading.Tasks;

namespace ScarIntel
{
    public partial class DocumentEditor : Form
    {
        private static DocumentType[] documents;
        private readonly Person person;
        private readonly ServerServiceClient serverClient;

        public DocumentEditor(ServerServiceClient serverClient, Person person)
        {
            InitializeComponent();
            this.person = person;
            this.serverClient = serverClient;
            if (documents == null)
            {
                SampleGenericDelegate2<DocumentType[]> del = new SampleGenericDelegate2<DocumentType[]>(serverClient.GetAllDocumentType);

                IAsyncResult result = del.BeginInvoke( null, null);

                documents = del.EndInvoke(result); 
            }
            Editable();
        }


        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void Editable()
        {
            foreach (DocumentType c in documents)
            {
                comboBox1.Items.Add(c.Name);
            }
            comboBox1.SelectedIndex = 0 ; 
        }

        public void saveButton_Click(object sender, EventArgs e)
        {
            var document = new Document();

            document.code = textBox1.Text;
            document.emission_local = textBox2.Text;
            document.emission_date = dateTimePicker2.Value;
            document.expiration_date = dateTimePicker1.Value;

            if (document.emission_date > document.expiration_date)
            {
                InfoForm info = new InfoForm();
                info.Add("Aviso: Data de Emissao é maior do que data de Expiraçao !");
                info.Enabled = false;
                info.Visible = true;
            }

            else
            {
                
                DocumentType doctype = documents.FirstOrDefault(d => d.Name.Equals(comboBox1.Text));
                if (doctype == null) return;
                document.Type = doctype;
               serverClient.InsertPersonAsync(person);
               
                document.Person = person;

                try
                {
                    SampleGenericDelegate<Document, int> del = new SampleGenericDelegate<Document, int>(serverClient.InsertDocument);

                    IAsyncResult result = del.BeginInvoke(document, null, null);

                    del.EndInvoke(result);
                    Close();
                }
                catch (Exception exception)
                {
                    var info = new InfoForm();
                    info.Add(exception.Message);
                    info.ShowDialog(this);
                    info.Dispose();
                }
            }
        }

        private void DocumentEditor_Load(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}