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
        private readonly int AgeMin=16;
        public Stream fileStream;
        private Regist _regist;
        private Person p;


        public PersonEditor(ServerServiceClient serverClient,Regist regist)
        {
            InitializeComponent();
            this.serverClient = serverClient;
            photoDialog = new OpenFileDialog();
            _regist = regist;
            p = new Person();
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
            
            try
            {    SetPerson();

            serverClient.InsertPersonAsync(p); 
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

        private  void SetPerson()
        {
            p.Name = nameBoxText.Text;
            p.Birthday = birthPicker.Value;
            
            TimeSpan x = DateTime.Now - p.Birthday;
            int aux = (x.Days/365);
            if (aux < AgeMin)
            {
               throw new Exception("Menor que 16 anos nao é permitida a inserçao");
               /*
                InfoForm info = new InfoForm();
                info.Add();
                info.Enabled = false;
                info.ShowDialog(this);
            */
            }
            p.Address = moradaTextBox.Text;
            p.Birthplace = nacioBoxText.Text;
           
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
              }

        private Boolean verifyPerson() { return !p.Address.Equals("") && p.Birthday != null && !p.Birthplace.Equals("") && !p.Name.Equals("Nome Completo"); }


        private void documentoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void documentoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SetPerson();
            if (!verifyPerson())
            {
                InfoForm info = new InfoForm();
                info.Add("Aviso: Tem que prencher todos os campos pessoa primeiro");
                info.Enabled = false;
                info.Visible = true;
            }
            else { 
              DocumentEditor doc = new DocumentEditor(serverClient,this.p);
              doc.Visible = true;
              doc.FormClosed += (o, even) => this.Close();
           }
        }
    }
}