using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Alien.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : UserControl
    {
        public RegistrationView()
        {
            InitializeComponent();
        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window parent = Window.GetWindow(this);
            if (e.ChangedButton == MouseButton.Left)
            {
                parent?.DragMove();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            SmtpClient Client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "alienjdrpac@gmail.com",
                    Password = "qyizyoiddmvoaywp"
                }
            };

            MailAddress FromEmail = new MailAddress("alienjdrpac@gmail.com", "Alien JDR");
            try
            {
                MailAddress ToEmail = new MailAddress(txbEmail.Text, "Utilisateur");
                MailMessage Message = new MailMessage()
                {
                    From = FromEmail,
                    Subject = "Confiramation d'enregistrement",
                    Body = "BIENVENU DANS LE MONDE DE ALIEN JDR"
                };

                Message.To.Add(ToEmail);

                Client.SendCompleted += Client_SendCompleted;
                Client.SendMailAsync(Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void Client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("Quelque chose a mal tourné \n " + e.Error.Message, "Erreur");
                return;
            }

            MessageBox.Show("Enregistrement Confirmée", "Done");
        }
    }
}
