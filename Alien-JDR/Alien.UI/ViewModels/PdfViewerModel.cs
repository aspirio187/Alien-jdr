using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien.UI.ViewModels
{
    class PdfViewerModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        private Stream docStream;

        public Stream DocumentSteam
        {
            get
            {
                return docStream;
            }

            set
            {
                docStream = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DocumentStream"));
            }
        }

        public PdfViewerModel()
        {
            docStream = new FileStream(@"..\..\Alien.JdR.PourPAC.pdf", FileMode.OpenOrCreate);
        }
    }
}
