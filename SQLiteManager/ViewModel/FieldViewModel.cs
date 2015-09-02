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
    public class FieldViewModel : ViewModelBase
    {
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

        private string type = null;
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                if (type == value)
                    return;
                type = value;
                RaisePropertyChanged("Type");
            }
        }

        private bool isPrimaryKey = false;
        public bool IsPrimaryKey 
        { 
            get 
            {
                return isPrimaryKey;                
            }
            set
            {
                if (isPrimaryKey == value)
                    return;
                isPrimaryKey = value;
                RaisePropertyChanged("IsPrimaryKey");
            }
        }

        private bool isUnique = false;
        public bool IsUnique
        {
            get
            {
                return isUnique;
            }
            set
            {
                if (isUnique == value)
                    return;
                isUnique = value;
                RaisePropertyChanged("IsUnique");
            }
        }



        private bool isAutoIncrement = false;
        public bool IsAutoIncrement
        {
            get
            {
                return isAutoIncrement;
            }
            set
            {
                if (isAutoIncrement == value)
                    return;
                isAutoIncrement = value;
                RaisePropertyChanged("IsAutoIncrement");
            }
        }

        private bool allowDBNull = false;
        public bool AllowDBNull
        {
            get
            {
                return allowDBNull;
            }
            set
            {
                if (allowDBNull == value)
                    return;
                allowDBNull = value;
                RaisePropertyChanged("AllowDBNull");
            }
        }
    }
}