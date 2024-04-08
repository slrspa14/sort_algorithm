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
            //스레드로 돌릴가
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

        private void Swap(int i, int j)
        {
            int temp = rand_data[i];
            rand_data[i] = rand_data[j];
            rand_data[j] = temp;
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
                    Fill = Brushes.Blue,
                    Stroke = Brushes.Black
                };
                //시각화
                Canvas.SetLeft(rect, i * barWidth);
                Canvas.SetBottom(rect, 0);
                bubble.Children.Add(rect);
            }
        }
        private void Draw_sort3()
        {
            quick.Children.Clear();//canvas 초기화

            double barWidth = quick.ActualWidth / rand_data.Length;//막대기 넓이
            double maxVal = rand_data.Max();//배열요소중에 최대값

            for (int i = 0; i < rand_data.Length; i++)
            {
                double barHeight = (rand_data[i] / maxVal) * quick.ActualHeight;//막대기 높이
                Rectangle rect = new Rectangle//막대기 생성 및 색 설정
                {
                    Width = barWidth - 1,
                    Height = barHeight,
                    Fill = Brushes.Blue,
                    Stroke = Brushes.Black
                };
                //시각화
                Canvas.SetLeft(rect, i * barWidth);
                Canvas.SetBottom(rect, 0);
                quick.Children.Add(rect);
            }
        }
        private void Draw_sort4()
        {
            merge.Children.Clear();//canvas 초기화

            double barWidth = merge.ActualWidth / rand_data.Length;//막대기 넓이
            double maxVal = rand_data.Max();//배열요소중에 최대값

            for (int i = 0; i < rand_data.Length; i++)
            {
                double barHeight = (rand_data[i] / maxVal) * merge.ActualHeight;//막대기 높이
                Rectangle rect = new Rectangle//막대기 생성 및 색 설정
                {
                    Width = barWidth - 1,
                    Height = barHeight,
                    Fill = Brushes.Blue,
                    Stroke = Brushes.Black
                };
                //시각화
                Canvas.SetLeft(rect, i * barWidth);
                Canvas.SetBottom(rect, 0);
                merge.Children.Add(rect);
            }
        }
        private void Draw_sort2()
        {
            heap.Children.Clear();//canvas 초기화

            double barWidth = heap.ActualWidth / rand_data.Length;//막대기 넓이
            double maxVal = rand_data.Max();//배열요소중에 최대값

            for (int i = 0; i < rand_data.Length; i++)
            {
                double barHeight = (rand_data[i] / maxVal) * heap.ActualHeight;//막대기 높이
                Rectangle rect = new Rectangle//막대기 생성 및 색 설정
                {
                    Width = barWidth - 1,
                    Height = barHeight,
                    Fill = Brushes.Blue,
                    Stroke = Brushes.Black
                };
                //시각화
                Canvas.SetLeft(rect, i * barWidth);
                Canvas.SetBottom(rect, 0);
                heap.Children.Add(rect);
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            //4개 묶기
            //버블,힙,병합,퀵
            //4개 다 비동기로
            //스레드
            Merge_sort(rand_data, 0, rand_data.Length - 1);
            Quick_sort(rand_data, 0, rand_data.Length - 1);
            Heap_sort();
            Bubble_sort();
        }
        private async void Bubble_sort()
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
        private async void Heap_sort()
        {
            //왼, 오 노드 비교 정렬
            try
            {
                await Task.Delay(1000);
                int heap = rand_data.Length;
                Stopwatch time = new Stopwatch();
                time.Start();
                //힙배열 생성
                for (int i = heap / 2 - 1; i >= 0; i--)
                {
                    Heapify(rand_data, heap, i);
                }
                for(int i = heap -1; i>0; i--)
                {
                    int temp = rand_data[0];
                    rand_data[0] = rand_data[i];
                    rand_data[i] = temp;
                    Heapify(rand_data, heap, i);
                    //Draw_sort2();
                    await Task.Delay(10);
                }
                //끝나면
                time.Stop();
                Time_rank("Heap", time);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"힙 정렬 오류: {ex}");
            }
        }

        private void Heapify(int[] arr, int heap, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            //트리 노드 정렬과정
            if (left < heap && arr[left] > arr[largest])
                largest = left;
            if (right < heap && arr[right] > arr[largest])
                largest = right;
            if (largest != i)
            {
                int temp = arr[i];
                arr[i] = arr[largest];
                arr[largest] = temp;

                Heapify(arr, heap, largest);
            }
        }

        private async void Merge_sort(int[] arr, int left, int right)
        {
            //배열을 반으로 나누고 정렬 한 뒤 합치면서 정렬
            try
            {
                await Task.Delay(1000);
                Stopwatch time = new Stopwatch();
                time.Start();

                if(left<right)
                {
                    int mid = (left + right) / 2;
                    Merge_sort(arr, left, mid);
                    Merge_sort(arr, mid + 1, right);
                    Merge(arr, left, mid, right);
                    //Draw_sort4();
                    await Task.Delay(10);
                }
                //끝나면
                time.Stop();
                Time_rank("Merge", time);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"병합 정렬 오류: {ex}");
            }
        }
        private void Merge(int[] arr, int left, int mid, int right)
        {
            int[] sorted = new int[100];
            int i, j, k = left;
            for (i = left, j = mid + 1; i <= mid && j <= right;)
            {
                sorted[k++] = (arr[i] <= arr[j]) ? arr[i++] : arr[j++];
            }
            if (i > mid) // 왼쪽이 끝나고, 오른쪽 나머지를 복사
            {
                for (int l = j; l <= right; l++)
                    sorted[k++] = arr[l];
            }
            else // 오른쪽이 끝나서, 왼쪽의 나머지를 복사
            {
                for (int l = i; l <= mid; l++)
                    sorted[k++] = arr[l];
            }

            // 정렬된 sorted[]을 rand_data[]로 복사
            for (int l = left; l <= right; l++)
                rand_data[l] = sorted[l];
        }
        private async void Quick_sort(int[] arr, int left, int right)
        {
            //중간지점 정하고 그 값이랑 비교
            try
            {
                await Task.Delay(1000);
                Stopwatch time = new Stopwatch();
                time.Start();
                
                if(left<right)
                {
                    int q = Partition(arr, left, right);
                    Quick_sort(arr, left, q - 1);
                    Quick_sort(arr, q + 1, right);
                }
                time.Stop();
                Time_rank("Quick", time);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"퀵 정렬 오류: {ex}");
            }
        }
        private int Partition(int[] a, int left, int right)
        {
            int low = left;     // 왼쪽부터 작아지는 인덱스
            int high = right + 1;   // 오른쪽부터 작아지는 인덱스, 하나를 빼고 시작하기 때문에 1을 더해줌
            int pivot = a[left];    // 기준값

            do
            {
                do
                {
                    low++;
                } while (low <= right && a[low] < pivot);
                do
                {
                    high--;
                } while (high >= left && a[high] > pivot);

                if (low < high)
                {
                    int t = a[high];
                    a[high] = a[low];
                    a[low] = t;
                }
            } while (low < high);

            // 피봇과 a[hight]를 교환
            a[left] = a[high];
            a[high] = pivot;

            return high;
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
