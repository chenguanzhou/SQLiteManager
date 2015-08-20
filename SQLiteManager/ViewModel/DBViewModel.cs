using GalaSoft.MvvmLight;
using System.Collections;

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
    public class DBViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public DBViewModel(string dbName, string dbPath)
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////} 
            this.dbName = dbName;
            this.dbPath = dbPath;
        }

        private string dbPath;
        public string DBPath { get { return dbPath; } }

        private string dbName;
        public string DBName { 
            get 
            { 
                return dbName; 
            }
            set 
            {
                if (dbName == value)
                    return;
                dbName = value;
                RaisePropertyChanged("DBName");
            }
        }

        private bool isValid;
        public bool IsValid
        {
            get { return isValid; }
            set
            {
                if (isValid == value)
                    return;
                isValid = value;
                RaisePropertyChanged("IsValid");
            }
        }
    }
}