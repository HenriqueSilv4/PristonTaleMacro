using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HenMacro
{
    public partial class Form1 : Form
    {
        
        private static Random random = new Random();

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        private static Timer timer;

        public Form1()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += SimularCliqueDireito;
            timer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void SimularCliqueDireito(object sender, EventArgs e)
        {
            // Obtém todos os processos em execução
            Process[] processos = Process.GetProcesses();

            // Verifica se um processo com o nome "game.exe" está em execução
            bool jogoAberto = false;
            foreach (Process processo in processos)
            {
                if (processo.ProcessName.Equals("game.exe") || processo.ProcessName.Equals("game"))
                {
                    jogoAberto = true;
                    break;
                }
            }

            // Verifica se a janela ativa tem o nome "game.exe"
            IntPtr janelaAtiva = GetForegroundWindow();

            string nomeJanelaAtiva = string.Empty;

            const int tamanhoMaximoNomeJanela = 256;

            StringBuilder sb = new StringBuilder(tamanhoMaximoNomeJanela);

            if (GetWindowText(janelaAtiva, sb, tamanhoMaximoNomeJanela) > 0)
            {
                nomeJanelaAtiva = sb.ToString();
            }

            // Ação a ser executada se a janela "game.exe" estiver selecionada
            if (jogoAberto && nomeJanelaAtiva.Equals("game.exe") || nomeJanelaAtiva.Equals("game"))
            {

                Point posicaoDoMouse = Cursor.Position;

                Cursor.Position = posicaoDoMouse;

                MouseClicker.SimularCliqueDireito(posicaoDoMouse.X, posicaoDoMouse.Y);

                timer.Stop();

                timer.Interval = random.Next(1000, 2000);

                string textoRepeticao = string.Format("Repetição: {0} ms", timer.Interval);
                string textoFeedbackJanela = string.Format("Processo do Priston Tale encontrado.");
                TxtLog.ForeColor = Color.Green;
                TxtLog.Text = textoRepeticao + "\r\n" + textoFeedbackJanela;

                timer.Start();
            }
            else
            {
                string textoRepeticao = string.Format("Repetição: {0} ms", timer.Interval);
                string textoFeedbackJanela = string.Format("Processo do Priston Tale não foi encontrado.");
                TxtLog.ForeColor = Color.Red;
                TxtLog.Text = textoRepeticao + "\r\n" + textoFeedbackJanela;
            }
        }

        public static class MouseClicker
        {
            public static void SimularCliqueDireito(int x, int y)
            {
                Cursor.Position = new Point(x, y);
                MouseClick(MouseEventFlags.RightDown);
                MouseClick(MouseEventFlags.RightUp);
            }

            private static void MouseClick(MouseEventFlags flags)
            {
                mouse_event((int)flags, 0, 0, 0, 0);
            }

            [DllImport("user32.dll")]
            private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

            private enum MouseEventFlags
            {
                LeftDown = 0x00000002,
                LeftUp = 0x00000004,
                MiddleDown = 0x00000020,
                MiddleUp = 0x00000040,
                RightDown = 0x00000008,
                RightUp = 0x00000010,
                Wheel = 0x00000800,
                XDown = 0x00000080,
                XUp = 0x00000100
            }
        }
    }
}
