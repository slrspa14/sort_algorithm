using System.Windows;
using System.Windows.Input;
using System.Windows.Documents;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Diagnostics;//스톱워치용

namespace sort_project
{
    public partial class MainWindow : Window
    {
        private int rank = 1;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //4개 묶기
            //버블,힙,병합,퀵
            //4개 다 비동기로
            Merge_sort();
            Quick_sort();
            Heap_sort();
            Bubble_sort();
        }
        async void Bubble_sort()// 끝나면 종료시간 표시하기//랜덤값으로 데이터 생성해줘야되나 배열이나 리스트에
        {//데이터 100개 기준으로
            try
            {
                await Task.Delay(1000);
                Stopwatch time = new Stopwatch();
                time.Start();
                await Task.Delay(4000);
                //끝나면
                time.Stop();
                Time_rank("Bubble", time);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"버블정렬 오류: {ex}");
            }
            

        }
        async void Heap_sort()// 끝나면 종료시간 표시하기
        {
            try
            {
                await Task.Delay(1000);
                Stopwatch time = new Stopwatch();
                time.Start();
                await Task.Delay(5000);
                //끝나면
                time.Stop();
                Time_rank("Heap", time);

            }
            catch(Exception ex)
            {
                MessageBox.Show($"힙 정렬 오류: {ex}");
            }
        }
        async void Merge_sort()// 끝나면 종료시간 표시하기
        {
            try
            {
                await Task.Delay(1000);
                Stopwatch time = new Stopwatch();
                time.Start();
                await Task.Delay(7000);
                //끝나면
                time.Stop();
                Time_rank("Merge", time);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"병합 정렬 오류: {ex}");
            }
        }
        async void Quick_sort()// 끝나면 종료시간 표시하기
        {
            try
            {
                await Task.Delay(1000);
                Stopwatch time = new Stopwatch();
                time.Start();
                await Task.Delay(10000);
                //끝나면
                time.Stop();
                Time_rank("Quick", time);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"퀵 정렬 오류: {ex}");
            }
        }
        void Time_rank(string algorithm, Stopwatch time)
        {
            Dispatcher.Invoke(() =>
            {
                record.AppendText($"[{rank}] {algorithm} sort: {time.Elapsed.ToString(@"m\:ss\.ff")}\n");
            });
            rank++;
        }
    }
}
