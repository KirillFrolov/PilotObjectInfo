using System.Collections.ObjectModel;
using PilotObjectInfo.Models.Core;
using ReactiveUI;

namespace PilotObjectInfo.ViewModels
{
    class SignnaturesInfoViewModel: ReactiveObject
    {
        public SignnaturesInfoViewModel(PilotFile file)
        {
            File = file;
            Signatures = new ObservableCollection<SignatureRequestInfo>(file.SignatureRequests);
        }

        public PilotFile File { get; set; }
        public ObservableCollection<SignatureRequestInfo> Signatures { get; set; }
    }
}
