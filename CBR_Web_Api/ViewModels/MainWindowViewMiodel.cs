using CBR_Web_Api.Models;
using CBR_Web_Api.Services.Intrerfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace CBR_Web_Api.ViewModels
{
    internal class MainWindowViewMiodel : ViewModel
    {
        private IGetXML xmlDocument;
        private IUserDialog userDialog;
        private CollectionViewSource MainViewSource;
        private CancellationTokenSource ProcessCancellation;
        public ICollectionView ElementsView { get { return MainViewSource.View; } }


        #region Значение прогресса

        private double progressValue;
        public double ProgressValue
        {
            get { return progressValue; }
            set { Set(ref progressValue, value); }

        }
        #endregion


        private string tittle = "Курсы валют";
        public string Title { get => tittle; set => Set(ref tittle, value); }

        private XmlCharCode? selectedCode;
        public XmlCharCode? SelectedCode
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


        public MainWindowViewMiodel(IGetXML xmlDocument, IUserDialog userDialog)
        {
            this.xmlDocument = xmlDocument;
            this.userDialog = userDialog;
            MainViewSource = new CollectionViewSource()
            {
                SortDescriptions =
                {
                     new SortDescription(nameof(XmlValute.Date), ListSortDirection.Descending)
                }
            };
        }

        #region Загрузка CharCode для отображения в ComboBox при загрузке формы
        private ICommand loadDataCommand;
        public ICommand LoadDataCommand => loadDataCommand
            ??= new LambdaCommandAsync(OnLoadDataCommandExecutedAsync, CanLoadDataCommandExecute);

        private bool CanLoadDataCommandExecute(object? arg)
            => true;


        private async Task OnLoadDataCommandExecutedAsync(object? obj)
        {
            try
            {
                CharCodeCollections = (await xmlDocument.ReadAllCharCodeAsync().ToArrayAsync()).ToObservableCollection();
            }
            catch (HttpRequestException)
            {
                userDialog.ConfirmError("Проблема с подключением!", "Внимание!");
            }
            catch (Exception)
            {
                userDialog.ConfirmError("Непредвиденная ошибка!", "Внимание!");
            }
        }
        #endregion


        #region Загрузка данных о валютах
        private ICommand loadValutaCommand;
        public ICommand LoadValutaCommand => loadValutaCommand
            ??= new LambdaCommandAsync(OnLoadValutaCommandExecutedAsync, CanLoadValutaCommandExecute);

        private bool CanLoadValutaCommandExecute(object? arg)
            => true;

        private async Task OnLoadValutaCommandExecutedAsync(object? obj)
        {
            try
            {
                var progressValue = new Progress<double>(p => ProgressValue = p);
                ProcessCancellation = new CancellationTokenSource();

                var document = await xmlDocument.ReadyXMLAsync(SelectedCodeStr, progress: progressValue, cancel: ProcessCancellation.Token);
                ElementCollections = xmlDocument.ReadXmlValutes(document).ToArray().ToObservableCollection();
            }
            catch (HttpRequestException) 
            {
                userDialog.ConfirmError("Проблема с подключением!", "Внимание!");
            }
            catch (Exception)
            {
                userDialog.ConfirmError("Непредвиденная ошибка!", "Внимание!");
            }
        }
        #endregion
    }
}
