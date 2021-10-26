using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Alien.UI.Core
{
    class ObservableObject : INotifyPropertyChanged /*updates the ui while binding*/
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
