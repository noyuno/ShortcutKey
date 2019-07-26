using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortcutKey
{
    delegate void KeyboardEventHandler();

    class Keyboard
    {
        public const int Interval = 50;

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        KeyboardEventHandler onKeyDown;
        public Keys Key { set; get; }
        public bool Enabled { private set; get; }
        ulong counter;

        public Keyboard(KeyboardEventHandler onkeydown)
        {
            this.onKeyDown = onkeydown;
        }

        public async void Start()
        {
            this.Enabled = true;
            while (this.Enabled)
            {
                short state = GetAsyncKeyState(this.Key);
                if (counter != 0 && state != 0b00000000)
                {
                    onKeyDown();
                    while (GetAsyncKeyState(this.Key) != 0b00000000)
                        await Task.Delay(Keyboard.Interval);
                }
                counter++;
                await Task.Delay(Keyboard.Interval);
            }
        }

        public void Stop()
        {
            this.Enabled = false;
            this.counter = 0;
        }
    }
}
