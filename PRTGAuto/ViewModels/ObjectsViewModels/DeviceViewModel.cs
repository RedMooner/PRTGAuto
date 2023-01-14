using Prism.Commands;
using Prism.Mvvm;
using PrtgAPI;
using Microsoft.Win32;
using System;
using PRTGAuto.Models;
using Aspose.Cells;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Windows;
using System.Linq;

namespace PRTGAuto.ViewModels.ObjectsViewModels
{
    public class DeviceViewModel : BindableBase
    {
        public Device CurrentDevice { get; set; }
        public string ShortName
        {
            get
            {
                string rez = "";
                if (CurrentDevice.Name.Length < 30)
                    for (int i = 0; i < CurrentDevice.Name.Length; i++)
                        rez += CurrentDevice.Name[i];
                else
                    for (int i = 0; i < 30; i++)
                        rez += CurrentDevice.Name[i];

                return rez;
            }
        }
        public DelegateCommand<object> ExportDataToExcel { get; }
        public DeviceViewModel(Device currentDevice)
        {
            CurrentDevice = currentDevice;
            ExportDataToExcel = new DelegateCommand<object>(o =>
            {


                var Sensors = PRTGConnection.Client.GetSensors(Property.Device, CurrentDevice);
                List<ExcelSensorData> list = new List<ExcelSensorData>();
                foreach (var item in Sensors)
                {
                    var channel = PRTGConnection.Client.GetChannels(item.Id).First();
                    list.Add(new ExcelSensorData(item.Id.ToString(), item.Name, item.Device, item.Group, item.Probe, item.Status.ToString(), channel.LowerErrorLimit,channel.LowerWarningLimit,channel.LastValue.ToString()));
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
            });
        }
        public class ExcelSensorData
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Device { get; set; }
            public string Group { get; set; }
            public string Probe { get; set; }
            public string Status { get; set; }
            public double? LowerErrorLimit { get; set; }
            public double? LowerWarningLimit { get; set; }
            public string? LastValue { get; set; }
            public ExcelSensorData(string id, string name, string device, string group, string probe, string status,double? s, double? lowerWarningLimit, string? lastValue)
            {
                Id = id;
                Name = name;
                Device = device;
                Group = group;
                Probe = probe;
                Status = status;
                LowerErrorLimit = s;
                LowerWarningLimit = lowerWarningLimit;
                LastValue = lastValue;
            }
        }
    }
}
