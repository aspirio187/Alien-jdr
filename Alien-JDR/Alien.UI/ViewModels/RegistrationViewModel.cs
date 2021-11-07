using Alien.UI.States;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    public class RegistrationViewModel : BindableBase, IDialogAware
    {
        private readonly IAuthenticator _authenticator;

        public string Title => "Inscription";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            // TODO : Si l'inscription s'est bien passé, retourner en arrière, sinon fermer le programme
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            // TODO : Rien
        }
    }
}
