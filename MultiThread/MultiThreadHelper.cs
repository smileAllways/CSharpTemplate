using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread
{
    public static class MultiThreadHelper
    {
        /// <summary>
        /// Parellel 비동기 호출
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="input">입력값 배열</param>
        /// <param name="action">입력값 배열을 가지고 작업 후 반환값 배열에 적재하는 함수</param>
        /// <returns>계산된 배열</returns>
        public static async Task<T1[]> ParallelForAsync<T1, T2>(T2[] input, Action<T1[], T2, int> action)
        {
            T1[] result = await Task.Run(() => ParallelFunc(input, action));

            return result;
        }

        /// <summary>
        /// Parallel 동기 호출
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="input">입력값 배열</param>
        /// <param name="action">입력값 배열을 가지고 작업 후 반환값 배열에 적재하는 함수</param>
        /// <returns>계산된 배열</returns>
        public static T1[] ParallelFunc<T1, T2>(T2[] input, Action<T1[], T2, int> action)
        {
            T1[] result = new T1[input.Length];
            Parallel.For(0, input.Length,
                (index) => action(result, input[index], index));

            return result;
        }
    }
}
