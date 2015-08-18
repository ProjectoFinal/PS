using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScarIntel;
using ScarIntel.BrokerService;
using ScarIntel.Views;

namespace SarcIntelService
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void launchMainForm(ServerServiceClient serverClient)
        {
            if (serverClient.State == CommunicationState.Opened)
            {
                // Handling user interfaces 
                RegistView mf = new RegistView(serverClient);
                this.Visible = false;
                
                // Running Main form 
                mf.ShowDialog(this);
                
                // Restoring Login interface
                this.Visible = true;
                loginButton.Enabled = true;

                // Close connection 
                serverClient.Close();
                return; 
            }
            serverClient.Abort();
            var info = new InfoForm();
            info.Add("Erro a conectar ao servidor");
            info.ShowDialog(this);
            loginButton.Enabled = true;
            
        }


        private void Login_Click(object sender, EventArgs e)
        {

            var serverClient = new ServerServiceClient();

            String user = userTextBox.Text;
            String pass = passTextBox.Text;

            serverClient.ClientCredentials.UserName.UserName = user;
            serverClient.ClientCredentials.UserName.Password = pass;

            loginButton.Enabled = false;
            passTextBox.Text = "";


            serverClient.Open();
            launchMainForm(serverClient);
            try
            {
                

            
            }
            catch ( Exception exception )
            {
                serverClient.Abort();
                var info = new InfoForm();
                info.Add(exception.Message);
                info.ShowDialog(this);
                
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}