using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;

namespace InformationSystem.ViewModel;

internal class CertificateViewModel : ObservableObject
{
    private string _certificate;

    public string Certificate
    {
        get { return _certificate; }
        set { _certificate = value; OnPropertyChanged(); }
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