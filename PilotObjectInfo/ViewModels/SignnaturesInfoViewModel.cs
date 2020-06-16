using Ascon.Pilot.SDK;
using Homebrew.Mvvm.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilotObjectInfo.ViewModels
{
    class SignnaturesInfoViewModel: ObservableObject
    {
        public SignnaturesInfoViewModel(IFile file)
        {
            File = file;
            Signatures = new ObservableCollection<ISignature>(file.Signatures);
        }

        public IFile File { get; set; }
        public ObservableCollection<ISignature> Signatures { get; set; }
    }
}
