using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        public static MainViewModel This { get; set; }
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

            try
            {
                var root = Registry.CurrentUser.CreateSubKey("SOFTWARE").CreateSubKey("SQLiteManager");
                var key = root.CreateSubKey("RegistryInformation");
                string[] paths = key.GetValueNames();
                foreach (string path in paths)
                {
                    if (path == null || path.Count() == 0)
                        continue;
                    string name = key.GetValue(path).ToString();
                    AddDB(name, path);
                }
                if (DBs.Count > 0)
                    ActiveDB = DBs[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "读取注册表失败", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SaveInfo()
        {
            try
            {
                var root = Registry.CurrentUser.CreateSubKey("SOFTWARE").CreateSubKey("SQLiteManager");
                root.DeleteSubKeyTree("RegistryInformation",false);
                var key = root.CreateSubKey("RegistryInformation");
                foreach (DBViewModel db in DBs)
                {
                    key.SetValue(db.DBPath, db.DBName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存至注册表失败", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateDB(string path)
        {
            if (path == null)
                return;
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            if (name == null)
                return;

            AddDB(name, path);
        }

        private void AddDB(string name,string path)
        {
            if (name == null || path == null)
                return;
            int count = DBs.Where(db => { return db.DBPath == path; }).Count();
            if (count != 0)
                return;    
            DBViewModel newDB = new DBViewModel(name, path);
            DBs.Add(newDB);
            ActiveDB = newDB;
        }

        private void RemoveDB(DBViewModel db)
        {
            DBs.Remove(db);
            if (DBs.Count > 0)
                ActiveDB = DBs[0];
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

        private DBViewModel activeDB = null;
        public DBViewModel ActiveDB
        {
            get
            {
                return activeDB;
            }
            set
            {
                if (activeDB == value)
                    return;

                activeDB = value;
                if (activeDB != null && activeDB.ActivatedTable == null && activeDB.Tables.Count != 0)
                    activeDB.ActivatedTable = activeDB.Tables[0];
                RaisePropertyChanged("ActiveDB");
                RaisePropertyChanged("IsActiveDBEnable");
            }
        }

        public bool IsActiveDBEnable 
        { 
            get { return ActiveDB == null ? false : ActiveDB.IsValid; }            
        }

        private bool isCreateTableVisible = false;
        public bool IsCreateTableVisible 
        {
            get { return isCreateTableVisible; }
            set
            {
                if (isCreateTableVisible == value)
                    return;
                isCreateTableVisible = value;
                RaisePropertyChanged("IsCreateTableVisible");
            }
        }

        private void CreateDBCommandExcute()
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Title = "创建SQLite数据库";
                dlg.Filter = "SQLite 数据库文件(*.db)|*.db|所有文件(*.*)|*.*";
                if (dlg.ShowDialog() != true)
                    return;

                CreateDB(dlg.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "创建SQLite数据库文件失败", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public ICommand CreateDBCommand { get { return new RelayCommand(CreateDBCommandExcute); } }

        private void RegisterDBCommandExcute()
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "打开数据库";
                dlg.Filter = "SQLite 数据库文件(*.db)|*.db|所有文件(*.*)|*.*";
                dlg.Multiselect = true;
                if (dlg.ShowDialog() != true)
                    return;

                string[] paths = dlg.FileNames;
                foreach (string path in paths)
                {
                    AddDB(System.IO.Path.GetFileNameWithoutExtension(path), path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "注册SQLite数据库失败", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        public ICommand RegisterDBCommand { get { return new RelayCommand(RegisterDBCommandExcute); } }

        private void RemoveDBCommandExcute()
        {
            try
            {
                if (ActiveDB == null)
                    return;
                RemoveDB(ActiveDB);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "移除SQLite数据库文件失败", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }

        public ICommand RemoveDBCommand 
        {
            get 
            { 
                return new RelayCommand(RemoveDBCommandExcute,()=>ActiveDB!=null); 
            } 
        }
    }
}