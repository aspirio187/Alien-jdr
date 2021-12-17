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
                    Body = "\t\tALIEN, LE JEU DE ROLE, PLONGEZ DANS L'UNIVERS DE SCIENCE-FICTION HORRIFIQUE DE RIDLEY SCOTTE! " +
                           "\n\n \tL'espace est vaste, sombre, et n'est pas un environnement amical. Les rayons gamma et les neutrinos jaillissent des " +
                           "\n \tétoiles mourantes pour vous brûler vif, les trous noirs vous déchirent, et le vide lui-même fait bouillir votre sang et gèle " +
                           "\n \tvotre cerveau. Essayez de crier et personne ne peut vous entendre - retenez votre respiration et vous vous brisez les" +
                           "\n \tpoumons." +
                           "\n \tMais l'espace n'est pas aussi vide qu'on pourrait le croire. Ses frontières ne cessent de s'élargir. Les gouvernements" +
                           "\n \trivaux mènent une guerre froide tandis que les corporations se disputent des ressources précieuses. Les colons se" +
                           "\n \ttournent vers les étoiles et jouent avec leur vie : chaque nouveau monde apprivoisé offre soit un festin, soit la famine. Et" +
                           "\n \til y a des choses qui se cachent dans l'ombre de chaque astéroïde, des choses étranges, différentes et mortelles." +
                           "\n \tDes choses Aliens...",

                            
                           IsBodyHtml = true,

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
