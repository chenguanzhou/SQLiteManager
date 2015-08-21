using GalaSoft.MvvmLight;
using System;
using System.Collections;
using System.Data;
using System.Data.SQLite;

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
    public class TableViewModel : ViewModelBase
    {
        private SQLiteConnection conn = null;
        private SQLiteDataAdapter adapter = null;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public TableViewModel(string name,SQLiteConnection conn)
        {
            Name = name;
            this.conn = conn;
            adapter = new SQLiteDataAdapter("select * from " + Name, conn);
            adapter.Fill(Table);
        }

        private string name = null;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name == value)
                    return;
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        private DataTable table = new DataTable();
        public DataTable Table 
        { 
            get 
            {
                return table;                
            }
            set
            {
                if (table == value)
                    return;
                table = value;
                RaisePropertyChanged("Table");
            }
        }
    }
}