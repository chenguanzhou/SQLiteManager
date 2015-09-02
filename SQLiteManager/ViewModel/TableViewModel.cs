using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
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
    public class TableViewModel : ViewModelBase
    {
        private SQLiteConnection conn = null;
        private SQLiteDataAdapter adapter = null;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public TableViewModel(string name,string definition,SQLiteConnection conn)
        {
            Name = name;
            this.definition = definition;
            this.conn = conn;
            adapter = new SQLiteDataAdapter("select * from " + Name, conn);
            adapter.Fill(Table);
            try
            {
                SQLiteCommand cmd = new SQLiteCommand("select * from " + Name, conn);
                using (var reader = cmd.ExecuteReader(CommandBehavior.KeyInfo | CommandBehavior.SchemaOnly))
                {
                    var columnSchemaTable = reader.GetSchemaTable();
                    foreach (DataRow dr in columnSchemaTable.Rows)
                    {
                        FieldViewModel fieldInfo = new FieldViewModel()
                        {
                            Name = dr["ColumnName"].ToString(),
                            Type = dr["DataTypeName"].ToString(),
                            IsPrimaryKey = (bool)dr["IsKey"],
                            IsUnique = (bool)dr["IsUnique"],
                            AllowDBNull = (bool)dr["AllowDBNull"],
                            IsAutoIncrement = (bool)dr["IsAutoIncrement"]
                        };
                        FieldsInfo.Add(fieldInfo);
                    }
                }
            }            
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
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

        private string definition = null;
        public string Definition
        {
            get
            {
                return definition;
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

        private ObservableCollection<FieldViewModel> fieldsInfo = new ObservableCollection<FieldViewModel>();
        public ObservableCollection<FieldViewModel> FieldsInfo
        {
            get
            {
                return fieldsInfo;
            }
            set
            {
                if (fieldsInfo == value)
                    return;
                fieldsInfo = value;
                RaisePropertyChanged("FieldsInfo");
            }
        }


        
    }
}