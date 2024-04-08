using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading;

namespace sort_thread_test
{
    public partial class MainWindow : Window
    {
        int rank = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            int sort = select_sort.SelectedIndex;
            switch(sort)
            {
                case 1:
                    Thread bubble = new Thread(Bubble_sort);
                    bubble.Start();
                    break;
                case 2:
                    Thread merge = new Thread(Merge_sort);
                    merge.Start();
                    break;
                case 3:
                    Thread heap = new Thread(Heap_sort);
                    heap.Start();
                    break;
                case 4:
                    Thread quick = new Thread(Quick_sort);
                    quick.Start();
                    break;
            }
        }
        private void Bubble_sort()
        {
            Stopwatch time = new Stopwatch();
            time.Start();


            time.Stop();
            Sort_record("Bubble", time);
        }
        private void Merge_sort()
        {
            Stopwatch time = new Stopwatch();
            time.Start();


            time.Stop();
            Sort_record("Merge", time);
        }
        private void Heap_sort()
        {
            Stopwatch time = new Stopwatch();
            time.Start();


            time.Stop();
            Sort_record("Heap", time);
        }
        private void Quick_sort()
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            time.Stop();
            Sort_record("Quick", time);
        }
        private void Sort_record(string algorithm, Stopwatch time)
        {
            Dispatcher.Invoke(() =>
            {
                record.AppendText($"[{rank}] {algorithm} sort: {time.Elapsed.ToString(@"m\:ss\.ff")}\n");
            });
            rank++;
        }

    }
}
