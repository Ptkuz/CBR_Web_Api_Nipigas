using CBR_Web_Api.Models;
using CBR_Web_Api.Services.Intrerfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace CBR_Web_Api.ViewModels
{
    internal class MainWindowViewMiodel : ViewModel
    {
        private IGetXML xmlDocument;
        private CollectionViewSource MainViewSource;
        public ICollectionView ElementsView { get { return MainViewSource.View; } }

        private string tittle = "Курсы валют";
        public string Title { get => tittle; set => Set(ref tittle, value); }

        private XmlCharCode selectedCode;
        public XmlCharCode SelectedCode
        {
            get => selectedCode;
            set
            {

                Set(ref selectedCode, value);
                if (SelectedCode != null)
                    SelectedCodeStr = SelectedCode.CharCode;
                OnPropertyChanged(nameof(SelectedCode));

            }
        }
        private string selectedCodeStr;
        public string SelectedCodeStr { get => selectedCodeStr; set => Set(ref selectedCodeStr, value); }



        private ObservableCollection<XmlValute>? elementCollections = new ObservableCollection<XmlValute>();
        public ObservableCollection<XmlValute> ElementCollections
        {
            get => elementCollections;
            set
            {
                if (Set(ref elementCollections, value))
                {
                    MainViewSource.Source = value;
                    OnPropertyChanged(nameof(ElementsView));
                }
            }
        }

        private ObservableCollection<XmlCharCode> charCodeCollections = new ObservableCollection<XmlCharCode>();
        public ObservableCollection<XmlCharCode> CharCodeCollections { get => charCodeCollections; set => Set(ref charCodeCollections, value); }





        public MainWindowViewMiodel(IGetXML xmlDocument)
        {
            this.xmlDocument = xmlDocument;
            MainViewSource = new CollectionViewSource();
        }

        private ICommand loadDataCommand;
        public ICommand LoadDataCommand => loadDataCommand
            ??= new LambdaCommandAsync(OnLoadDataCommandExecutedAsync, CanLoadDataCommandExecute);

        private bool CanLoadDataCommandExecute(object? arg)
            => true;


        private async Task OnLoadDataCommandExecutedAsync(object? obj)
        {            
            CharCodeCollections = (await xmlDocument.ReadAllCharCodeAsync().ToArrayAsync()).ToObservableCollection();
        }

        private ICommand loadValutaCommand;
        public ICommand LoadValutaCommand => loadValutaCommand
            ??= new LambdaCommandAsync(OnLoadValutaCommandExecutedAsync, CanLoadValutaCommandExecute);

        private bool CanLoadValutaCommandExecute(object? arg)
            => true;

        private async Task OnLoadValutaCommandExecutedAsync(object? obj) 
        {
            var document = await xmlDocument.ReadyXMLAsync(SelectedCodeStr);
            ElementCollections = xmlDocument.ReadXmlValutes(document).ToArray().ToObservableCollection();
        }
    }
}
