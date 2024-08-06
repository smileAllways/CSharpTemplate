using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThread
{
    public static class SimpleTest
    {
        /// <summary>
        /// Parallel for 비동기 호출
        /// </summary>
        public static async void SameSimpleWorksAsync()
        {
            await Task.Run(() => SameSimpleWorks());
        }

        /// <summary>
        /// Parallel for 호출
        /// </summary>
        public static void SameSimpleWorks()
        {
            string[] strings = new string[20];

            // 적당한 쓰레드를 할당하여 병렬로 작업을 수행한다.
            // 하지만 모든 쓰레드의 작업 완료전까지 호출 쓰레드를 블럭시킨다.(동기)
            Parallel.For(0, strings.Length, (index) =>
            {
                SimpleWork(strings, index);
            });
        }

        private static void SimpleWork(string[] strings, int index)
        {
            Console.WriteLine($"{index} SimpleWork start.");
            Thread.Sleep(1000);
            strings[index] = $"{index}";
            Console.WriteLine($"{index} SimpleWork end.");
        }

        /// <summary>
        /// Task 방식 비동기 호출
        /// </summary>
        public static async void DifferentWorksAsync()
        {
            List<Task> tasks = new List<Task>();

            tasks.Add(TaskA());
            tasks.Add(TaskB());
            tasks.Add(TaskC());

            await Task.WhenAll(tasks);
        }

        private static async Task TaskA()
        {
            Console.WriteLine("TaskA start.");
            await Task.Delay(2000);
            Console.WriteLine("TaskA end.");
        }

        private static async Task TaskB()
        {
            Console.WriteLine("TaskB start.");
            await Task.Delay(1000);
            Console.WriteLine("TaskB end.");
        }

        private static async Task TaskC()
        {
            Console.WriteLine("TaskC start.");
            await Task.Delay(3000);
            Console.WriteLine("TaskC end.");
        }
    }
}
