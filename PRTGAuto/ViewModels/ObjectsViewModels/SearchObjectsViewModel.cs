using Aspose.Cells;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using PrtgAPI;
using PRTGAuto.Models;
using PRTGAuto.Views.ObjectViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using static PRTGAuto.ViewModels.ObjectsViewModels.DeviceViewModel;

namespace PRTGAuto.ViewModels.ObjectsViewModels
{
    public class SearchObjectsViewModel : BindableBase
    {
        // SubGroups
        private PRTGData _model;
        private Visibility _loadingScreenVisibility = Visibility.Hidden;
        private string _groupname = "";
        private ObservableCollection<Group> _groups;

        private Probe _selectedProbe;
        private Group _selectedGroup;

        private bool _isOnlyDownChecked = false;
        private bool _isExportButtonenabled = false;
        private string _name = "";
        public PRTGData Model
        {
            get
            {
                return _model;
            }
            set { _model = value; }
        }
        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                RaisePropertyChanged("Groups");
            }
        }
        public Visibility LoadingScreenVisibility
        {
            get { return _loadingScreenVisibility; }
            set
            {
                _loadingScreenVisibility = value;
                RaisePropertyChanged("LoadingScreenVisibility");
            }
        }
        public Probe SelectedProbe
        {
            get { return _selectedProbe; }
            set
            {
                _selectedProbe = value;
                LoadGroups();
            }
        }
        Thread t;

        private void LoadGroups()
        {
            LoadingScreenVisibility = Visibility.Visible;
            t = new Thread(new ThreadStart(RunTheradLoadGroups));
            t.IsBackground = true;
            t.Start();
        }
        private void RunTheradLoadGroups()
        {
            Groups = new System.Collections.ObjectModel.ObservableCollection<Group>(_model.client.QueryGroups(x => x.Probe == _selectedProbe.Name));
            LoadingScreenVisibility = Visibility.Hidden;
        }
        public Group SelectedGroup
        {
            get
            {
                return _selectedGroup;
            }
            set
            {
                _selectedGroup = value;
            }
        }
        public bool IsOnlyDownChecked
        {
            get { return _isOnlyDownChecked; }
            set
            {
                _isOnlyDownChecked = value;
                RaisePropertyChanged("IsOnlyDownChecked");
            }
        }
        public bool IsExportButtonEnabled
        {
            get
            {
                return _isExportButtonenabled;
            }
            set
            {
                _isExportButtonenabled = value;
                RaisePropertyChanged("IsExportButtonEnabled");
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value; RaisePropertyChanged("Name");
            }
        }
        public string GroupName
        {
            get
            {
                return _groupname;
            }
            set
            {
                _groupname = value; RaisePropertyChanged("GroupName");
            }
        }
        private ObservableCollection<DeviceViewModel> _devices = new ObservableCollection<DeviceViewModel>();
        public ObservableCollection<DeviceViewModel> Devices
        {
            get
            {
                return _devices;
            }
            set
            {
                _devices = value;
                RaisePropertyChanged("Devices");
            }
        }

        public DelegateCommand<object> SearchCommand { get; }
        public DelegateCommand<object> ExportCommand { get; }
        public DelegateCommand<object> ExportSensorsCommand { get; }
        private void Search()
        {
            LoadingScreenVisibility = Visibility.Visible;
            t = new Thread(new ThreadStart(RunTheradSerach));
            t.IsBackground = true;
            t.Start();
        }
        private void RunTheradSerach()
        {
            try
            {
                int count = 0;
                Application.Current.Dispatcher.BeginInvoke((Action)(() => SearchObjectsView.Instance.status.Text = $"Устройств обработанно: {count}"));
                IQueryable<Device> values = null;
                if (Name.Length < 1 && GroupName.Length < 1)
                    values = Model.client.QueryDevices(x => x.Group == SelectedGroup.Name);
                else if (GroupName.Length > 1)
                    values = Model.client.QueryDevices(x => x.Group == GroupName);
                else
                    values = Model.client.QueryDevices(x => x.Group == SelectedGroup.Name && x.Name.Contains(Name));

                if (GroupName.Length < 1 && SelectedProbe == null && SelectedGroup == null)
                {
                    values = null;
                }
                if (values != null)
                {
                    foreach (var item in values)
                    {
                        if (IsOnlyDownChecked)
                        {
                            if (item.DownSensors > 0)
                            {
                                Application.Current.Dispatcher.BeginInvoke((Action)(() => Devices.Add(new DeviceViewModel(item))));
                                Application.Current.Dispatcher.BeginInvoke((Action)(() => SearchObjectsView.Instance.status.Text = $"Устройств обработанно: {count}"));
                                count++;
                                Thread.Sleep(100);
                            }
                        }
                        else
                        {
                            Application.Current.Dispatcher.BeginInvoke((Action)(() => Devices.Add(new DeviceViewModel(item))));
                            Application.Current.Dispatcher.BeginInvoke((Action)(() => SearchObjectsView.Instance.status.Text = $"Устройств обработанно: {count}"));
                            count++;
                            Thread.Sleep(100);
                        }
                    }

                    if (count > 0)
                    {
                        IsExportButtonEnabled = true;
                    }
                    else
                    {
                        IsExportButtonEnabled = false;
                    }
                    LoadingScreenVisibility = Visibility.Hidden;
                }
                else
                {
                    LoadingScreenVisibility = Visibility.Hidden;
                    MessageBox.Show("Вы не указали критерии поиска!");
                }
            }
            catch
            {
                LoadingScreenVisibility = Visibility.Hidden;
                MessageBox.Show("Вы не указали критерии поиска!");
            }
        }

        public SearchObjectsViewModel()
        {
            Model = new PRTGData();
            SearchCommand = new DelegateCommand<object>(i =>
            {
                Devices.Clear();
                Search();
            });
            ExportCommand = new DelegateCommand<object>(i =>
            {
                LoadingScreenVisibility = Visibility.Visible;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel files(*.xlsx)|*.xlsx|All files(*.*)|*.*";
                saveFileDialog.ShowDialog();
                // Instantiate a new Workbook
                Workbook book = new Workbook();
                List<ExcelDevicesData> data = new List<ExcelDevicesData>();
                foreach (var device in Devices)
                {
                    var value = device.CurrentDevice;
                    data.Add(new ExcelDevicesData(value.Name, value.Id, value.Status.ToString(), value.Host, value.Probe, value.TotalSensors, value.DownSensors));
                }
                // Obtaining the reference of the worksheet
                Worksheet sheet = book.Worksheets[0];
                sheet.Cells.ImportCustomObjects((System.Collections.ICollection)data,
    new string[] { "Name", "Id", "Status", "Host", "Probe", "TotalSensors", "DownSensors" }, // propertyNames
    true, // isPropertyNameShown
    0, // firstRow
    0, // firstColumn
    data.Count, // Number of objects to be exported
    true, // insertRows
    null, // dateFormatString
    false); // convertStringToNumber

                // Save the Excel file
                book.Save(saveFileDialog.FileName);
                LoadingScreenVisibility = Visibility.Hidden;

            });
            ExportSensorsCommand = new DelegateCommand<object>(i =>
            {
                ExportSensors();
            });
        }
        private void ExportSensors()
        {
            //LoadingScreenVisibility = Visibility.Visible;
            //Thread t = new Thread(new ThreadStart(RunThreadExportSensors));
            //t.IsBackground = true;
            //t.Start();
            Models.ExportQueue.QueueController.Add($"тест", Devices.ToList());
        }
        private void RunThreadExportSensors()
        {
            List<ExcelSensorData> list = new List<ExcelSensorData>();
            int i = 0;

            foreach (var device in Devices)
            {
                string text = $"{i}/{Devices.Count}";
                Application.Current.Dispatcher.BeginInvoke((Action)(() => SearchObjectsView.Instance.status.Text = text));
                try
                {
                    var Sensors = PRTGConnection.Client.GetSensors(Property.Device, device.CurrentDevice);
                    foreach (var item in Sensors)
                    {
                        var channel = PRTGConnection.Client.GetChannels(item.Id).First();
                        list.Add(new ExcelSensorData(item.Id.ToString(), item.Name, item.Device, item.Group, item.Probe, item.Status.ToString(), channel.LowerErrorLimit, channel.LowerWarningLimit, channel.LastValue.ToString()));

                    }
                }
                catch
                {

                }
                i++;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files(*.xlsx)|*.xlsx|All files(*.*)|*.*";
            saveFileDialog.ShowDialog();
            // Instantiate a new Workbook
            Workbook book = new Workbook();
            // Obtaining the reference of the worksheet
            Worksheet sheet = book.Worksheets[0];
            sheet.Cells.ImportCustomObjects((System.Collections.ICollection)list,
new string[] { "Id", "Name", "Device", "Group", "Probe", "Status", "LowerErrorLimit", "LowerWarningLimit" }, // propertyNames
true, // isPropertyNameShown
0, // firstRow
0, // firstColumn
list.Count, // Number of objects to be exported
true, // insertRows
null, // dateFormatString
false); // convertStringToNumber

            // Save the Excel file

            book.Save(saveFileDialog.FileName);
            LoadingScreenVisibility = Visibility.Hidden;

        }
        public class ExcelDevicesData
        {
            public string Name { get; set; }
            public int Id { get; set; }
            public string Status { get; set; }
            public string Host { get; set; }
            public string Probe { get; set; }
            public int TotalSensors { get; set; }
            public int DownSensors { get; set; }
            public ExcelDevicesData(string name, int id, string status, string host, string probe, int totalSensors, int downSensors)
            {
                Name = name;
                Id = id;
                Status = status;
                Host = host;
                Probe = probe;
                TotalSensors = totalSensors;
                DownSensors = downSensors;
            }
        }
    }
}
