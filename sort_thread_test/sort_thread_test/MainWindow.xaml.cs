﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading;
using System.Collections.Generic;

namespace sort_thread_test
{
    public partial class MainWindow : Window
    {
        int[] sort_data;
        int rank = 1;
        int sleep = 20;
        public MainWindow()
        {
            InitializeComponent();
        }
        void Rand_data()
        {
            Random data = new Random();
            sort_data = new int[100];
            for(int i=0; i< sort_data.Length; i++)
            {
                sort_data[i] = data.Next(0, 10000);
            }
        }

        private void Draw_sort()
        {
            sort.Children.Clear();//canvas 초기화

            double barWidth = sort.ActualWidth / sort_data.Length;//막대기 넓이
            double maxVal = sort_data.Max();//배열요소중에 최대값

            for (int i = 0; i < sort_data.Length; i++)
            {
                double barHeight = (sort_data[i] / maxVal) * sort.ActualHeight;//막대기 높이
                Rectangle rect = new Rectangle//막대기 생성 및 색 설정
                {
                    Width = barWidth - 1,
                    Height = barHeight,
                    Fill = Brushes.SteelBlue,
                    Stroke = Brushes.Black
                };
                //시각화
                Canvas.SetLeft(rect, i * barWidth);
                Canvas.SetBottom(rect, 0);
                sort.Children.Add(rect);
            }
        }
        private void End_sort()
        {
            sort.Children.Clear();//canvas 초기화

            double barWidth = sort.ActualWidth / sort_data.Length;//막대기 넓이
            double maxVal = sort_data.Max();//배열요소중에 최대값

            for (int i = 0 ; i < sort_data.Length; i++)
            {
                double barHeight = (sort_data[i] / maxVal) * sort.ActualHeight;//막대기 높이
                Rectangle rect = new Rectangle//막대기 생성 및 색 설정
                {
                    Width = barWidth - 1,
                    Height = barHeight,
                    Fill = Brushes.Green,
                    Stroke = Brushes.White
                };
                //시각화
                Canvas.SetLeft(rect, i * barWidth);
                Canvas.SetBottom(rect, 0);
                sort.Children.Add(rect);
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Rand_data();
            int sort = select_sort.SelectedIndex;
            switch(sort)
            {
                case 0:
                    Thread bubble = new Thread(Bubble_sort);
                    sort_name.Content = "Bubble Sort";
                    bubble.Start();
                    break;
                case 1:
                    Thread merge = new Thread(Merge_start);
                    sort_name.Content = "Merge Sort";
                    merge.Start();
                    break;
                case 2:
                    Thread heap = new Thread(Heap_sort);
                    sort_name.Content = "Heap Sort";
                    heap.Start();
                    break;
                case 3:
                    Thread quick = new Thread(Quick_start);
                    sort_name.Content = "Quick Sort";
                    quick.Start();
                    break;
                case 4:
                    Thread selection = new Thread(Selection_sort);
                    sort_name.Content = "Selection Sort";
                    selection.Start();
                    break;
                case 5:
                    Thread cocktail = new Thread(Cocktail_sort);
                    sort_name.Content = "Cocktail Sort";
                    cocktail.Start();
                    break;
                case 6:
                    Thread insertion = new Thread(Insertion_sort);
                    sort_name.Content = "Insertion Sort";
                    insertion.Start();
                    break;
                case 7:
                    Thread counting = new Thread(Counting_sort);
                    sort_name.Content = "Counting Sort";
                    counting.Start();
                    break;
            }
        }
        private void Bubble_sort()
        {
            try
            {
                Stopwatch time = new Stopwatch();
                time.Start();
                for (int i = 0; i < sort_data.Length; i++)
                {
                    for (int j = 0; j < sort_data.Length - i - 1; j++)
                    {
                        if (sort_data[j] > sort_data[j + 1])
                        {
                            int temp = sort_data[j];
                            sort_data[j] = sort_data[j + 1];
                            sort_data[j + 1] = temp;
                            Dispatcher.Invoke(new Action(Draw_sort));
                            Thread.Sleep(sleep);
                        }
                    }
                }

                time.Stop();
                Sort_record("Bubble", time);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"버블 정렬 오류: {ex}");
            }
            
        }
        private void Merge_start()
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            Merge_sort(sort_data, 0, sort_data.Length-1);
            time.Stop();
            Sort_record("Merge", time);
        }
        private void Merge_sort(int[] arr, int left, int right)
        {
            try
            {
                if (left < right)
                {
                    int mid = (left + right) / 2;
                    Merge_sort(arr, left, mid);
                    Merge_sort(arr, mid + 1, right);
                    Merge(arr, left, mid, right);//
                    Dispatcher.Invoke(Draw_sort);
                    Thread.Sleep(sleep);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"병합 정렬 오류: {ex}");
            }
        }
        void Merge(int[] arr, int left, int mid, int right)
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
                    sorted[k++] = arr[l];//
            }
            else // 오른쪽이 끝나서, 왼쪽의 나머지를 복사
            {
                for (int l = i; l <= mid; l++)
                    sorted[k++] = arr[l];
            }
            // 정렬된 sorted[]을 rand_data[]로 복사
            for (int l = left; l <= right; l++)
                sort_data[l] = sorted[l];
        }
        private void Heap_sort()
        {
            try
            {
                Stopwatch time = new Stopwatch();
                time.Start();
                // 최대 힙 초기화
                for (int i = sort_data.Length / 2 - 1; i >= 0; i--)
                {
                    heapify(sort_data, sort_data.Length, i);//
                }
                for (int i = sort_data.Length - 1; i > 0; i--)
                {
                    Swap(sort_data, 0, i);
                    heapify(sort_data, i, 0);
                    Dispatcher.Invoke(Draw_sort);
                    Thread.Sleep(sleep);
                }
                time.Stop();
                Sort_record("Heap", time);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"힙 정렬 오류: {ex}");
            }
        }
        private void heapify(int[] array, int arrayLength, int i)
        {
            int parent = i;
            int left = i * 2 + 1;
            int right = i * 2 + 2;
            // 왼쪽 자식노드
            if (left < arrayLength && array[parent] < array[left])
            {
                parent = left;
            }
            // 오른쪽 자식노드
            if (right < arrayLength && array[parent] < array[right])
            {
                parent = right;
            }
            // 부모노드 < 자식노드
            if (i != parent)
            {
                Swap(array,parent, i);
                //Swap(array, parent, i);//
                
                heapify(array, arrayLength, parent);
            }
        }
        private void Swap(int[] ary, int a, int b)
        {
            int temp;
            temp = sort_data[a];
            sort_data[a] = sort_data[b];
            sort_data[b] = temp;
        }
        void Quick_start()
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            Quick_sort(sort_data, 0, sort_data.Length - 1);
            time.Stop();
            Sort_record("Quick", time);
        }
        private void Quick_sort(int[] arr, int left, int right)
        {
            try
            {
                if (left < right)
                {
                    int q = Partition(arr, left, right);
                    Quick_sort(arr, left, q - 1);
                    Quick_sort(arr, q + 1, right);
                    Dispatcher.Invoke(Draw_sort);
                    Thread.Sleep(sleep);
                }
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

        private void Selection_sort()
        {
            try
            {
                Stopwatch time = new Stopwatch();
                time.Start();
                for (int i = 0; i < sort_data.Length; i++)
                {
                    int min = i;
                    for (int j = i + 1; j < sort_data.Length; j++)
                    {
                        if (sort_data[min] > sort_data[j])
                        {
                            min = j;
                        }
                    }
                    //if (i != min)
                    //{
                        int temp = sort_data[i];
                        sort_data[i] = sort_data[min];
                        sort_data[min] = temp;
                    //}
                    Dispatcher.Invoke(Draw_sort);
                    Thread.Sleep(sleep);
                }
                time.Stop();
                Sort_record("Selection", time);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"선택 정렬 오류{ex}");
            }
        }

        private void Cocktail_sort()
        {
            int left = 0;
            int right = sort_data.Length - 1;
            int last = right;
            //버블정렬 변형
            Stopwatch time = new Stopwatch();
            time.Start();
            while(left < right)
            {
                for (int i = left; i < right; i++)
                {
                    if(sort_data[i] > sort_data[i+1])
                    {
                        int temp = sort_data[i];
                        sort_data[i] = sort_data[i + 1];
                        sort_data[i + 1] = temp;
                        Dispatcher.Invoke(Draw_sort);
                        Thread.Sleep(sleep);
                    }
                }
                right--;
                for(int i = right; i> left; i--)
                {
                    if(sort_data[i-1] > sort_data[i])
                    {
                        int temp = sort_data[i - 1];
                        sort_data[i - 1] = sort_data[i];
                        sort_data[i] = temp;
                        Dispatcher.Invoke(Draw_sort);
                        Thread.Sleep(sleep);
                    }
                }
                left++;
            }
            time.Stop();
            Sort_record("Cocktail", time);
        }

        private void Insertion_sort()
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            for(int i = 1; i < sort_data.Length; i++)
            {
                int key = i;
                for(int j = i-1; j>=0; j--)
                {
                    if (sort_data[key] < sort_data[j])
                    {
                        int temp = sort_data[j];
                        sort_data[j] = sort_data[key];
                        sort_data[key] = temp;
                        key = j;
                        Dispatcher.Invoke(Draw_sort);
                        Thread.Sleep(sleep);
                    }
                    else
                        break;
                }
            }
            time.Stop();
            Sort_record("Insertion", time);

        }
        private void Counting_sort()
        {
            int max = sort_data.Max();
            int size = sort_data.Length;
            int[] output = new int[size];
            int[] count = new int[max + 1];
            Stopwatch time = new Stopwatch();
            time.Start();
            //size, 최대값+1,메모리 낭비 심한 정렬
            for (int i = 0; i < max; i++)
                count[i] = 0;
            for (int i = 0; i < size; i++)
                count[sort_data[i]]++;
            for (int i = 1; i <= max; i++)
                count[i] += count[i - 1];
            for(int i=0;i<size;i++)
            {
                output[count[sort_data[i]] - 1] = sort_data[i];
                count[sort_data[i]]--;
            }
            for (int i = 0; i < size; i++)
            {
                sort_data[i] = output[i];
                Dispatcher.Invoke(Draw_sort);
                Thread.Sleep(sleep);
            }
            time.Stop();
            Sort_record("Counting", time);
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
