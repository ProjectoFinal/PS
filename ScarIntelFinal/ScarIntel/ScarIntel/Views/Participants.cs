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

    public delegate R SampleGenericDelegate<A, R>(A a);
    public delegate R SampleGenericDelegate2<R>();
    
    public partial class Participants : Form
    {
        private ServerServiceClient serverClient;
        private Person[] persons;
        private Regist regist;

        public Participants(ServerServiceClient serverClient,Regist regist)
        {
            InitializeComponent();
            this.serverClient = serverClient;

            SampleGenericDelegate<int, Person[]> del = new SampleGenericDelegate<int, Person[]>(serverClient.GetAllPersonByIdRegist);

           IAsyncResult result= del.BeginInvoke(regist.Id, null, null);

           this.persons=del.EndInvoke(result);
           
            this.regist = regist;

            foreach (Person p in persons)
                dataGridView1.Rows.Add(new object[]
                {
                    p.Id, p.Name, p.Address, p.Birthday.ToShortDateString(), p.Birthplace,
                });

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
                if (e.RowIndex >= this.persons.Length) return;

                Person person = this.persons[e.RowIndex];


                if (person == null) return;


                var person_view = new PersonView(serverClient, person);
                person_view.Visible = true;
                
            }

        private void registosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonEditor pe = new PersonEditor(serverClient,regist);
            pe.ShowDialog(this);
        }
        }
    }

