using Ascon.Pilot.SDK;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class SignnaturesInfoViewModel: ReactiveObject
    {
        public SignnaturesInfoViewModel(IFile file)
        {
            File = file;
            Signatures = new ObservableCollection<ISignatureRequest>(file.SignatureRequests);
        }

        public IFile File { get; set; }
        public ObservableCollection<ISignatureRequest> Signatures { get; set; }
    }
}
