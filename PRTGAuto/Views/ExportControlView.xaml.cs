using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PRTGAuto.Views
{
    /// <summary>
    /// Логика взаимодействия для ExportControlView.xaml
    /// </summary>
    public partial class ExportControlView : UserControl
    {
        Models.ExportQueue.QueueController.Queue Queue;
        private bool IsStarted = false;
        private bool IsCompleted = false;
        public ExportControlView(Models.ExportQueue.QueueController.Queue queue)
        {
            InitializeComponent();
            path.Content = queue.path;
            var hasRunned = Models.ExportQueue.QueueController.Queues.Find(x => x.IsStarted == true);
            if (queue.IsFinished)
            {
                Queue = queue;
                title.Text = queue.Title;
                queue.ProgressString = status;
                status.Text = queue.progress;
                btn.Background = Brushes.Red;
                btn.Content = "Остановить и удалить";
                completed.Visibility = Visibility.Visible;
            }
            else
            {
                if (queue.IsStarted)
                {
                    Queue = queue;
                    title.Text = queue.Title;
                    queue.ProgressString = status;
                    status.Text = queue.progress;
                    btn.Background = Brushes.Red;
                    btn.Content = "Остановить и удалить";
                    IsStarted = true;
                }
                else
                {
                    Queue = queue;
                    title.Text = queue.Title;
                    queue.ProgressString = status;
                    status.Text = "Не запущена!";
                }
            }
            if (hasRunned != null && !Queue.IsStarted)
            {
                if (!queue.IsStarted)
                    btn.Visibility = Visibility.Hidden;
                status.Text = "В очереди!";
                queue.ProgressString = status;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!IsStarted)
            {
                Queue.ExportSensors();
                btn.Background = Brushes.Red;
                btn.Content = "Остановить и удалить";
                IsStarted = true;
            }
            else if (IsCompleted)
            {
                Models.ExportQueue.QueueController.Queues.Remove(Queue);
                Models.ExportQueue.QueueController.UpdateUI();
            }
            else
            {
                if (Queue.IsStarted)
                {
                    if (Queue.t != null)
                        Queue.t.Interrupt();
                }
                Models.ExportQueue.QueueController.Queues.Remove(Queue);
                Models.ExportQueue.QueueController.UpdateUI();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Process.Start(Queue.path);
            Models.ExportQueue.QueueController.Queues.Remove(Queue);
            Models.ExportQueue.QueueController.UpdateUI();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var Export = new ExportSettings(Queue);
            Export.ShowDialog();
        }
    }
}
