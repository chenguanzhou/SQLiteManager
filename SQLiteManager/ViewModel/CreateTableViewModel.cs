using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections;
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
    public class CreateTableViewModel : ViewModelBase
    {

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public CreateTableViewModel()
        {

        }

        private string name = "新建表";
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

        private DataTable fields = new DataTable();
        public DataTable Fields
        {
            get { return fields; }
            set
            {
                if (fields == value)
                    return;
                fields = value;
                RaisePropertyChanged("Fields");
            }
        }

        /// <summary>
        /// Show the Dialog of Craete Table
        /// </summary>
        public ICommand CreateCommand
        {
            get
            {
                return new RelayCommand<DataView>(
                    TableView =>
                    {
                        System.Windows.MessageBox.Show(TableView.GetType().ToString());
                    }
                );
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new RelayCommand(
                    () =>
                    {
                        MainViewModel.This.CurrentControl = null;
                    }
                );
            }
        }
    }
}