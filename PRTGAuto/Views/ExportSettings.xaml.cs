using PrtgAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PRTGAuto.Views
{
    /// <summary>
    /// Логика взаимодействия для ExportSettings.xaml
    /// </summary>
    public partial class ExportSettings : Window
    {
        Models.ExportQueue.QueueController.Queue Queue;
        public ExportSettings(Models.ExportQueue.QueueController.Queue queue)
        {
            InitializeComponent();
            Queue = queue;
            this.Title = "Настройка для " + queue.Title;
            SensorIdSetting.IsChecked = Queue.Settings["Id"];
            SensorNameSetting.IsChecked = Queue.Settings["Name"];
            DeviceNameSetting.IsChecked = Queue.Settings["Device"];
            GroupNameSetting.IsChecked = Queue.Settings["Group"];
            ProbeNameSetting.IsChecked = Queue.Settings["Probe"];
            DeviceStatusSetting.IsChecked = Queue.Settings["Status"];
            MinErrorSetting.IsChecked = Queue.Settings["LowerErrorLimit"];
            MinWarningSetting.IsChecked = Queue.Settings["LowerWarningLimit"];
            LastValueSetting.IsChecked = Queue.Settings["LastValue"];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //"Id", "Name", "Device", "Group", "Probe", "Status", "LowerErrorLimit", "LowerWarningLimit"
            Queue.Settings["Id"] = SensorIdSetting.IsChecked;
            Queue.Settings["Name"] = SensorNameSetting.IsChecked;
            Queue.Settings["Device"] = DeviceNameSetting.IsChecked;
            Queue.Settings["Group"] = GroupNameSetting.IsChecked;
            Queue.Settings["Probe"] = ProbeNameSetting.IsChecked;
            Queue.Settings["Status"] = DeviceStatusSetting.IsChecked;
            Queue.Settings["LowerErrorLimit"] = MinErrorSetting.IsChecked;
            Queue.Settings["LowerWarningLimit"] = MinWarningSetting.IsChecked;
            Queue.Settings["LastValue"] = LastValueSetting.IsChecked;
            this.Close();
        }
    }
}
