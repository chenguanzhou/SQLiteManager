using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SQLiteManager.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}                     
        }

        private ObservableCollection<DBViewModel> dbs = new ObservableCollection<DBViewModel>();
        public ObservableCollection<DBViewModel> DBs
        {
            get
            {
                return dbs;
            }
            set
            {
                if (dbs == value)
                    return;
                dbs = value;
                RaisePropertyChanged("DBs");
            }
        }

        private void CreateDBCommandExcute()
        {

        }
        public ICommand CreateDBCommand { get { return new RelayCommand(CreateDBCommandExcute); } }

        private void RegisterDBCommandExcute()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "打开数据库";
            dlg.Filter = "SQLite 数据库文件(*.db)|*.db|所有文件(*.*)|*.*";
            if (dlg.ShowDialog() != true)
                return;

            string[] paths = dlg.FileNames;
            foreach (string path in paths)
            {
                DBViewModel db = new DBViewModel(System.IO.Path.GetFileNameWithoutExtension(path), path);
                DBs.Add(db);
            }
            
        }
        public ICommand RegisterDBCommand { get { return new RelayCommand(RegisterDBCommandExcute); } }

        private void RemoveDBCommandExcute()
        {

        }
        public ICommand RemoveDBCommand { get { return new RelayCommand(RemoveDBCommandExcute); } }
    }
}