using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Serializace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string cesta = "Nastavení.xml";
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += (s, e) => Serializuj();

            Loaded += (s, e) => Deserializuj();

        }

        void Serializuj()
        {
            Settings set = new()
            {
                Jedna = (bool)prvni.IsChecked,
                Dva = (bool)druhy.IsChecked,
                Tri = (bool)treti.IsChecked,
            };

            XmlSerializer writer = new XmlSerializer(typeof(Settings));
            
            using(StreamWriter sw = new StreamWriter(cesta))
            {
                writer.Serialize(sw, set);
                sw.Close();

            }
        }

        void Deserializuj()
        {
            XmlSerializer reader = new XmlSerializer(typeof (Settings));
            Settings nastaveni = new Settings();
            if(File.Exists(cesta))
            {
                using(StreamReader sr = new StreamReader(cesta)) 
                {
                   nastaveni = (Settings)reader.Deserialize(sr);
                }
            }

            prvni.IsChecked = nastaveni.Jedna;
            druhy.IsChecked = nastaveni.Dva;
            treti.IsChecked = nastaveni.Tri;
        }
    }
}