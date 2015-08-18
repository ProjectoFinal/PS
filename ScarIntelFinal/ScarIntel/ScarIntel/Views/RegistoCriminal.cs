using System;
using System.Windows.Forms;
using ScarIntel.BrokerService;
using SarcIntelService;

namespace ScarIntel.Views
{
    public partial class RegistoCriminal : Form
    {
        private  Regist[] registos;
        private ServerServiceClient serverClient;


        public RegistoCriminal(ServerServiceClient serverClient)
        {
            InitializeComponent();
            this.serverClient = serverClient;
             try
            {
                SampleGenericDelegate2<Regist[]> del = new SampleGenericDelegate2<Regist[]>(serverClient.GetAllRegists);

                IAsyncResult result = del.BeginInvoke(null, null);

                registos = del.EndInvoke(result);
                 if(registos!=null)
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
            SampleGenericDelegate<int,CrimeType[]> del = new SampleGenericDelegate<int,CrimeType[]>(serverClient.GetAllCrimeTypeByRegist);

            IAsyncResult result = del.BeginInvoke(r.Id,null, null);

            CrimeType[] crimes = del.EndInvoke(result);
             

           DataGridViewComboBoxColumn comb = (DataGridViewComboBoxColumn)dataGridView1.Columns[1];
           foreach (CrimeType c in crimes)
               comb.Items.Add(c.Name);

           SampleGenericDelegate<int, int> dele = new SampleGenericDelegate<int, int>(serverClient.GetNumberOfParticipants);

           IAsyncResult result2 = dele.BeginInvoke(r.Id, null, null);

           int  count = dele.EndInvoke(result2);

            dataGridView1.Rows.Add(new object[] {r.Id,crimes[0].Name,r.Date.ToShortDateString(),count});
            
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            //quando ocorrer esse evento ir buscar todos os participantes ou pessoas associadas a esse ocorrencia
          
            if (e.RowIndex >= registos.Length) return;

                Regist regist = registos[e.RowIndex];

                //registos.FirstOrDefault(r=> r.Id == (int) dataGridView1.Rows[e.RowIndex].Cells[0].Value);

                if (regist == null) return;

                var person_view = new Participants(serverClient,regist);
                person_view.Visible = true; 
                

            

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}