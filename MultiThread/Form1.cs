using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace MultiThread
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 간단한 T Parallel 비동기 함수를 형지정하여 호출
        /// </summary>
        private async void test()
        {
            // 입력값 생성
            Random rand = new Random();
            int[] inputs = Enumerable.Range(0, 20)
                                .Select(_ => rand.Next(1, 11))
                                .ToArray();

            // 비동기 병렬 함수 호출
            int[] results = await MultiThreadHelper.ParallelForAsync<int, int>(
                inputs,
                (result, input, index) =>
                { 
                    // 간단한 계산 후 결과값에 반영
                    result[index] = input;
                    Thread.Sleep(1000);
                });

            // 병렬 작업 결과 프린트
            for (int i = 0; i < results.Length; i++)
            {
                Console.WriteLine($"{i}번째, 값 : {results[i]}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 간단한 Parallel 작업 호출
            SimpleTest.SameSimpleWorksAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 간단한 Task 작업 호출
            SimpleTest.DifferentWorksAsync();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            test();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SimpleTest.SameSimpleWorks();
        }
    }
}
