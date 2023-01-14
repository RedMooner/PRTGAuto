using Microsoft.Win32;
using Microsoft.Xaml.Behaviors.Media;
using PrtgAPI;
using PRTGAuto.Views.ObjectViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static PRTGAuto.ViewModels.ObjectsViewModels.DeviceViewModel;
using System.Windows;
using Aspose.Cells;
using PRTGAuto.ViewModels.ObjectsViewModels;
using System.Windows.Controls;
using PrtgAPI.Schedules;

namespace PRTGAuto.Models.ExportQueue
{
    public class QueueController
    {
        public static List<Queue> Queues = new List<Queue>();
        public static void Add(string title, List<DeviceViewModel> devices)
        {
            if (Queues.Count == 3)
            {
                MessageBox.Show("Добавлено максимальное кол-во экспортов в очередь!");
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files(*.xlsx)|*.xlsx|All files(*.*)|*.*";
            saveFileDialog.ShowDialog();
            Queues.Add(new Queue(title, devices) { path = saveFileDialog.FileName });
            UpdateUI();
        }

        public static void UpdateUI()
        {
            MainWindow.Instance.parent.Children.Clear();
            MainWindow.Instance.count.Text = $"{Queues.Count}/3";
            foreach (var device in Queues)
            {
                MainWindow.Instance.parent.Children.Add(new Views.ExportControlView(device));
            }
        }
        public static void UpdateUI(int index)
        {

            for (int i = 0; i < Queues.Count; i++)
            {
                if (Queues[i].IsStarted == false && Queues[i].IsFinished == false)
                {
                    Queues[i].ExportSensors();
                    break;
                }
            }
            MainWindow.Instance.parent.Children.Clear();
            MainWindow.Instance.count.Text = $"{Queues.Count}/3";
            foreach (var device in Queues)
            {
                MainWindow.Instance.parent.Children.Add(new Views.ExportControlView(device));
            }
        }
        public class Queue
        {
            public string Title = "";
            public TextBlock ProgressString;
            public List<DeviceViewModel> Devices = new List<DeviceViewModel>();
            public bool IsStarted = false;
            public bool IsFinished = false;
            public string progress = "";
            public string path = "";

            public Dictionary<string, bool?> Settings = new Dictionary<string, bool?>();

            public Queue(string title, List<DeviceViewModel> devices)
            {
                Title = "Экспорт из группы " + title;
                Devices = devices;
                string[] values = new string[] { "Id", "Name", "Device", "Group", "Probe", "Status", "LowerErrorLimit", "LowerWarningLimit", "LastValue" };
                for (int i = 0; i < values.Length; i++)
                    Settings.Add(values[i], true);
            }
            public Thread t;
            public void ExportSensors()
            {
                //LoadingScreenVisibility = Visibility.Visible;
                IsStarted = true;
                t = new Thread(new ThreadStart(RunThreadExportSensors));
                t.IsBackground = true;
                UpdateUI();
                t.Start();
            }
            private void RunThreadExportSensors()
            {
                List<ExcelSensorData> list = new List<ExcelSensorData>();
                int i = 0;
                int j = 0;
                int c = 0;
                foreach (var device in Devices)
                {
                    string text = $"Прогрес: {i}/{Devices.Count} Сенсоры {j}/{c}";
                    progress = text;
                    Application.Current.Dispatcher.BeginInvoke((Action)(() => ProgressString.Text = text));
                    try
                    {
                        var Sensors = PRTGConnection.Client.GetSensors(Property.Device, device.CurrentDevice);
                        j = 0;
                        c = Sensors.Count;
                        foreach (var item in Sensors)
                        {
                            text = $"Прогрес: {i}/{Devices.Count} Сенсоры {j}/{c}";
                            Application.Current.Dispatcher.BeginInvoke((Action)(() => ProgressString.Text = text));
                            j++;
                            var channel = PRTGConnection.Client.GetChannels(item.Id).First();
                            var schedule = PRTGConnection.Client.GetSchedules().First();
                            list.Add(new ExcelSensorData(item.Id.ToString(), item.Name, item.Device, item.Group, item.Probe, item.Status.ToString(), channel.LowerErrorLimit, channel.LowerWarningLimit, channel.VerticalAxisMax.ToString()));
                        }
                    }
                    catch
                    {

                    }
                    i++;
                }
                //SaveFileDialog saveFileDialog = new SaveFileDialog();
                //saveFileDialog.Filter = "Excel files(*.xlsx)|*.xlsx|All files(*.*)|*.*";
                //saveFileDialog.ShowDialog();
                // Instantiate a new Workbook
                Workbook book = new Workbook();
                int count = 0;
                foreach (var item in Settings)
                    if (item.Value == true)
                        count++;
                string[] settings = new string[count];
                int index = 0;
                foreach (var item in Settings)
                    if (item.Value == true)
                    {
                        settings[index] = item.Key;
                        index++;
                    }
                // Obtaining the reference of the worksheet
                Worksheet sheet = book.Worksheets[0];
                sheet.Cells.ImportCustomObjects((System.Collections.ICollection)list,
    settings, // propertyNames
    true, // isPropertyNameShown
    0, // firstRow
    0, // firstColumn
    list.Count, // Number of objects to be exported
    true, // insertRows
    null, // dateFormatString
    false); // convertStringToNumber

                // Save the Excel file

                book.Save(path);
                IsFinished = true;
                // LoadingScreenVisibility = Visibility.Hidden;
                Application.Current.Dispatcher.BeginInvoke((Action)(() => UpdateUI(0)));
            }
        }
    }
}
