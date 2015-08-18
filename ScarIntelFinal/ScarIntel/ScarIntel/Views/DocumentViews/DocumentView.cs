using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScarIntel.BrokerService;
using SarcIntelService;

namespace ScarIntel.Views.DocumentViews
{
    public partial class DocumentView : Form
    {
        private readonly Person person;
        private readonly ServerServiceClient serverClient;
        private List<Document> documents;


        public DocumentView(ServerServiceClient serverClient, Person person)
        {
            InitializeComponent();
            this.serverClient = serverClient;
            this.person = person;
            View();
        }

        private void View()
        {
            dataGridView1.Rows.Clear();

            try{

                SampleGenericDelegate<int,Document[]> del = new SampleGenericDelegate<int,Document[]>(serverClient.GetAllDocumentTypebyPerson);

                IAsyncResult result = del.BeginInvoke(person.Id, null, null);

            
                Document[] list = del.EndInvoke(result);

                if (list == null)
                    return;
                documents = new List<Document>(list);
                documents.ForEach(AddDocument);
            }

            catch (Exception exception)
            {
                var info = new InfoForm();
                info.Add(exception.Message);
                info.ShowDialog(this);
                info.Dispose();
            }
        }

        public void AddDocument(Document d)
        {
            if (d == null) return;
            try
            {

                SampleGenericDelegate2<DocumentType[]> del = new SampleGenericDelegate2<DocumentType[]>(serverClient.GetAllDocumentType);

                IAsyncResult result = del.BeginInvoke(null, null);


                DocumentType[] list = del.EndInvoke(result);


                d.Type.Name = list.First(doc => doc.Id == d.Type.Id).Name;
            }
            catch (Exception e)
            {
                var info = new InfoForm();
                info.Add(e.Message);
                info.ShowDialog(this);
                info.Dispose();
            }
            int row =
                dataGridView1.Rows.Add(new object[]
                {
                    d.id, d.code, d.emission_date.ToShortDateString(), d.expiration_date.ToShortDateString(), d.Type.Name,
                    d.emission_local
                });
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void DocumentView_Load(object sender, EventArgs e)
        {
        }
    }
}