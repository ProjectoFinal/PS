using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ScarIntel;
using ScarIntel.BrokerService;
using ScarIntel.Views;

namespace SarcIntelService.PersonView
{
    public partial class PersonEditor : Form
    {
        public readonly OpenFileDialog photoDialog;
        private readonly ServerServiceClient serverClient;
        public Stream fileStream;
        private Regist _regist;


        public PersonEditor(ServerServiceClient serverClient,Regist regist)
        {
            InitializeComponent();
            this.serverClient = serverClient;
            photoDialog = new OpenFileDialog();
            _regist = regist;
            
            photoDialog.Filter =
                "Image files (*.jpg, *.jpeg, *.jpe, *.jfif) | *.jpg; *.jpeg; *.jpe; *.jfif;";
            //photoDialog.InitialDirectory = @"C:\";
            photoDialog.Title = "Seleccione uma foto correspondente ";

            Closed += (sender, args) =>
            {
                photoDialog.Dispose();
                if (fileStream != null)
                {
                    fileStream.Dispose();
                }
                Dispose(true);
            };
        }


        private void photoButton_Click(object sender, EventArgs e)
        {
            if (photoDialog.ShowDialog() == DialogResult.OK)
            {
                fileStream = photoDialog.OpenFile();
                photo.Image = Image.FromStream(fileStream, true);
                photo.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }


        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ocorrenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }


        private void SaveButton_Click(object sender, EventArgs e)
        {
            Person p = GetPerson();
            
            try
            {
                 SampleGenericDelegate<Person,int> del = new SampleGenericDelegate<Person,int>(serverClient.InsertPerson);

                IAsyncResult result = del.BeginInvoke(p,null, null);

                p.Id =  del.EndInvoke(result); 
             //   _regist.AddPerson(p);
                SaveButton.Enabled = false;
                Close();
            }
            catch (Exception exc)
            {
                var info = new InfoForm();
                info.Add(exc.Message);
                info.ShowDialog(this);
                info.Dispose();
            }
        }

        private Person GetPerson()
        {
            var p = new Person();
            p.Name = nameBoxText.Text;
            p.Birthday = birthPicker.Value;
            p.Address = moradaTextBox.Text;
            p.Birthplace = nacioBoxText.Text;
            return p;
        }

        private BiometricType GetFaceBiometric()
        {
            var bio = new BiometricType();
            bio.Name = "face";

            if (fileStream == null)
            {
                bio.data = new byte[0];
                return bio;
            }
            using (var ms = new MemoryStream())
            {
                fileStream.Position = 0;
                fileStream.CopyTo(ms);
                bio.data = ms.ToArray();
                ms.Flush();
            }
            return bio;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void PersonEditor_Load(object sender, EventArgs e)
        {
        }

        private void birthPicker_ValueChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var occor = new DocumentEditor(serverClient, GetPerson());
            //occor.Visible = true;
            occor.ShowDialog(this);
        }
    }
}