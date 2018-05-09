using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using Open.WinKeyboardHook;

namespace WinSearch.Source
{
    /**
     * This keyboard hook was writen by: https//github.com/calledude
     *
     * */
    class Hooky
    {
        private KeyboardInterceptor key;
        private MainWindow _window;

        public Hooky(MainWindow window)
        {
            _window = window;
        }

        public void Start()
        {
            key = new KeyboardInterceptor();

            key.KeyDown += key_KeyDown;
            key.KeyUp += key_KeyUp;
            key.KeyPress += key_KeyPress;

            key.StartCapturing();

            Console.WriteLine("[Hooky] Started Hooky.");
            Application.Run();
        }

        private void key_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
        }

        private void key_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Space)
            {
                // Show search window
                //_window.Show();
                _window.ShowWindow();

            }
            else if (e.KeyCode == Keys.Escape)
            {
                _window.HideWindow();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                _window.RunProgram();
            }
            else if (e.KeyCode == Keys.Tab)
            {
                _window.NextSearch();
            }
        }

        private void key_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);
    }
}
