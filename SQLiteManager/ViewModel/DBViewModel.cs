using GalaSoft.MvvmLight;
using System;
using System.Linq;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Data;

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
        private SQLiteConnection conn = null;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public DBViewModel(string dbName, string dbPath)
        {
            this.dbName = dbName;
            this.dbPath = dbPath;

            try
            {
                SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
                builder.DataSource = DBPath;
                conn = new SQLiteConnection(builder.ConnectionString);
                conn.Open();
                var schema = conn.GetSchema("Tables");

                foreach (System.Data.DataRow row in schema.Rows)
                {
                    TableViewModel table = new TableViewModel(row.ItemArray[2].ToString(),conn);
                    Tables.Add(table);
                }
                

                IsValid = true;
            }
            catch (Exception ex)
            {
                IsValid = false;
            }
        }



        private string dbPath;
        public string DBPath { get { return dbPath; } }

        private string dbName;
        public string DBName
        {
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

        private ObservableCollection<TableViewModel> tables = new ObservableCollection<TableViewModel>();
        public ObservableCollection<TableViewModel> Tables
        {
            get { return tables; }
            set
            {
                if (tables == value)
                    return;
                tables = value;
                RaisePropertyChanged("Tables");
            }
        }

        private TableViewModel activatedTable = null;
        public TableViewModel ActivatedTable
        {
            get
            {
                return activatedTable;
            }
            set
            {
                if (activatedTable == value)
                    return;
                activatedTable = value;
                RaisePropertyChanged("ActivatedTable");
            }
        }
                
        public ICommand ShowCreateTableCommand
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        UserControlCreateTable control = new UserControlCreateTable();
                        control.DataContext = new CreateTableViewModel();
                        MainViewModel.This.CurrentControl = control;
                    },
                    () => IsValid
                );
            }
        }
    }
}