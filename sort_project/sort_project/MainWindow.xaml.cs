using System.Windows;
using System.Windows.Input;
using System.Windows.Documents;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Diagnostics;//스톱워치용
using System.Text;
using System.Linq;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace sort_project
{
    public partial class MainWindow : Window
    {
        private int[] rand_data;
        private int rank = 1;
        public MainWindow()
        {
            InitializeComponent();
            Sort_data();
        }

        private void Sort_data()
        {
            Random data = new Random();
            rand_data = new int[100];
            for (int i = 0; i < 100; i++)
            {
                rand_data[i] = data.Next(0, 1000);
            }
        }

        private void Draw_sort()
        {
            bubble.Children.Clear();//canvas 초기화

            double barWidth = bubble.ActualWidth / rand_data.Length;//막대기 넓이
            double maxVal = rand_data.Max();//배열요소중에 최대값

            for (int i = 0; i < rand_data.Length; i++)
            {
                double barHeight = (rand_data[i] / maxVal) * bubble.ActualHeight;//막대기 높이
                Rectangle rect = new Rectangle//막대기 생성 및 색 설정
                {
                    Width = barWidth - 1,
                    Height = barHeight,
                    Fill = Brushes.CadetBlue,
                    Stroke = Brushes.Black
                };
                //시각화
                Canvas.SetLeft(rect, i * barWidth);
                Canvas.SetBottom(rect, 0);
                bubble.Children.Add(rect);
            }
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

                            // 시각화 업데이트
                            Draw_sort();
                            await Task.Delay(10);//딜레이 없으면 전부 수행 후 멈췄다가 바로 나옴

                        }
                    }
                }
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
