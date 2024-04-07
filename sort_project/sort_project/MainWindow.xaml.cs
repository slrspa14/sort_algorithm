using System.Windows;
using System.Windows.Input;
using System.Windows.Documents;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Diagnostics;//스톱워치용
using System.Text;

namespace sort_project
{
    public partial class MainWindow : Window
    {
        private int[] rand_data;
        private int rank = 1;
        public MainWindow()
        {
            InitializeComponent();
            rand_data = new int[100];
            Random data = new Random();
            for(int i =0; i< 100; i++)
            {
                rand_data[i] = data.Next(0,1000);
            }
            
        }
        private void Print()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.Write(rand_data[i] + ", ");
            }
            Console.WriteLine();
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
        private async void Bubble_sort()// 끝나면 종료시간 표시하기//랜덤값으로 데이터 생성해줘야되나 배열이나 리스트에
        {//데이터 100개 기준으로
            try
            {
                await Task.Delay(1000);
                Stopwatch time = new Stopwatch();
                time.Start();
                //이걸 시각화
                for (int i = 0; i < rand_data.Length; i++)
                {
                    for (int j = 0; j < rand_data.Length -i -1; j++)
                    {
                        if (rand_data[j] > rand_data[j + 1])
                        {
                            int temp = rand_data[j];
                            rand_data[j] = rand_data[j + 1];
                            rand_data[j + 1] = temp;
                        }
                    }
                }
                Print();

                //끝나면
                time.Stop();
                Time_rank("Bubble", time);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"버블정렬 오류: {ex}");
            }
            

        }
        private async void Heap_sort()// 끝나면 종료시간 표시하기
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
        private async void Merge_sort()// 끝나면 종료시간 표시하기
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
        private async void Quick_sort()// 끝나면 종료시간 표시하기
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
        private void Time_rank(string algorithm, Stopwatch time)
        {
            Dispatcher.Invoke(() =>
            {
                record.AppendText($"[{rank}] {algorithm} sort: {time.Elapsed.ToString(@"m\:ss\.ff")}\n");
            });
            rank++;
        }
    }
}
