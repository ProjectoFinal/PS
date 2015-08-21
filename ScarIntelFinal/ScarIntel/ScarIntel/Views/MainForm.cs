using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScarIntel.BrokerService;
using SarcIntelService;
using SarcIntelService.PersonView;

namespace ScarIntel
{
    public partial class MainForm : Form
    {
        private readonly ServerServiceClient serverClient;
        private List<Person> persons = new List<Person>();

        public MainForm(ServerServiceClient serverClient)
        {
            InitializeComponent();
            this.serverClient = serverClient;
        }


        public void AddPerson(Person p)
        {
            if (p == null) return;
            int row =
                dataGridView1.Rows.Add(new object[]
                {p.Nif, p.Name, p.Birthday.ToShortDateString(), p.Birthplace, p.Address});
        }

        private void perfilToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // var person_editor = new PersonEditor(serverClient);
          //  person_editor.Visible = true;
        }

        private void search_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            try
            {
                string nacionalidade = this.nacio_text.Text;
                string data_nascimento = this.dateTimePicker1.Text;
                string nome = this.nome_text.Text;
                // Argumentos 1º name , 2º birthday , 3º nacionalidade 
                SearchParams sp = new SearchParams();
                sp.filters = new String[] { nome , data_nascimento , nacionalidade } ;
                Person[] list = serverClient.GetAllPerson(sp).result;
                if (list == null)
                    return;
                persons = new List<Person>(list);
                persons.ForEach(AddPerson);
            }

            catch (Exception exception)
            {
                var info = new InfoForm();
                info.Add(exception.Message);
                info.ShowDialog(this);
                info.Dispose();
            }

            /*dataGridView1.Rows.Clear();

            IEnumerable<Person> list = server.GetAllPerson();
            var filter = new PersonFilter(list);


            if (!nome_text.Text.Equals(""))
            {
                filter.Name((nome_text.Text));
            }

            if (!(dateTimePicker1.Value.Date == DateTime.Now.Date))
            {
                filter.Data(dateTimePicker1.Value);
            }

            if (!nacio_text.Text.Equals(""))
            {
                filter.Nacionalidade((nacio_text.Text));
            }
            persons = filter.getFilter().ToList();
            persons.ForEach(AddPerson);*/
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (e.RowIndex >= persons.Count) return;

                Person person =
                    persons.FirstOrDefault(p => p.Nif == (int) dataGridView1.Rows[e.RowIndex].Cells[0].Value);

                if (person == null) return;


                var person_view = new PersonView(serverClient, person);

                try
                {
                    /*
                    serverClient.GetRegistsAsync(person.Id).ContinueWith((r) =>
                    {
                        person_view.SetRegists(r.Result);
                    });
                    */
                    serverClient.GetBiometricTypeAsync(person.Nif, "face").
                        ContinueWith(b => { person_view.SetImage(b.Result); });
                    person_view.Visible = true;
                    Closing += (o, args) => person_view.Close();
                }
                catch (Exception exception)
                {
                    if (person_view != null || !person_view.IsDisposed)
                    {
                        person_view.Close();
                    }

                    var info = new InfoForm();
                    info.Add(exception.Message);
                    info.ShowDialog(this);
                    info.Dispose();
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }
    }
}