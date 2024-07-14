using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CancellableAsyncTasks
{
    /// <summary>
    /// .net 4.8에서 작성하였습니다.
    /// </summary>
    public partial class CancellableTemplateForm : Form
    {
        /// <summary>
        /// 취소 명령을 전달하기 위한 자원
        /// </summary>
        private CancellationTokenSource cancellationTokenSource;

        public CancellableTemplateForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 취소 가능 비동기 함수
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task CancellabeWorkAsync(CancellationToken cancellationToken)
        {
            // 아래와 같이 호출하면 중지되는 시점은
            // 1. 작업 수행 전. (Task.Run에 전달되는 token 옵션)
            // 2. MainWork 내부에서 전달 받은 token의 예외를 감지할 때
            await Task.Run(() => MainWork(cancellationToken), cancellationToken);

            textBox1.AppendText("Working is done\r\n");
        }

        /// <summary>
        /// ★★★주 작업 지점★★★
        /// </summary>
        /// <param name="cancellationToken"></param>
        private void MainWork(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 100; i++)
            {
                // 취소 명령을 감지하고 'OperationCanceledException'을 발생시킨다.
                // 예외는 현재 이곳이나 상위 어딘가에서 try-catch로 처리되어야 한다.
                cancellationToken.ThrowIfCancellationRequested();

                Invoke(new Action(() =>
                {
                    textBox1.AppendText($"Do Something : {i} loop\r\n");
                }));

                Thread.Sleep(500);
            }
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            // 작업 취소 자원 할당(재사용이 불가능한 자원이므로, 신규로 할당해야한다.)
            this.cancellationTokenSource = new CancellationTokenSource();

            buttonStop.Enabled = true;
            buttonStart.Enabled = false;
            progressBar1.Style = ProgressBarStyle.Marquee;

            // 작업 취소로 발생하는 'OperationCanceledException'을 처리하는 지점.
            try
            {
                await CancellabeWorkAsync(this.cancellationTokenSource.Token);
            }
            catch (OperationCanceledException ex) 
            {
                textBox1.AppendText("Working has been canceled\r\n");
            }

            buttonStop.Enabled = false;
            buttonStart.Enabled = true;
            progressBar1.Style = ProgressBarStyle.Blocks;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (this.cancellationTokenSource!= null) 
            { 
                // 작업 취소 수행
                this.cancellationTokenSource.Cancel();

                // 자원 해제(필수)
                this.cancellationTokenSource.Dispose();
                this.cancellationTokenSource= null;
            }
        }
    }
}
