using System.IO;

namespace InformationSystem.ViewModel;

internal class CertificateViewModel : ViewModelBase
{
    private string _certificate;

    public string Certificate
    {
        get { return _certificate; }
        set { _certificate = value; NotifyPropertyChanged(); }
    }

    public CertificateViewModel(string path)
    {
        if(!File.Exists(path))
        {
            Certificate = "Данные не найдены.";
        }
        else
        {
            using (FileStream fs = File.OpenRead(path))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    Certificate = sr.ReadToEnd();
                }
            }
        }

    }
}