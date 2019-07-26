using ShortcutKey.Properties;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortcutKey
{
    public partial class KeyForm : Form
    {
        public Keys Key { private set; get; }

        public KeyForm(string target, Keys current)
        {
            InitializeComponent();
            this.Text = target;
            CurrentLabel.Text = "current: " + current.ToString();
        }

        private void KeyForm_KeyUp(object sender, KeyEventArgs e)
        {
            this.Key = e.KeyCode;
            CurrentLabel.Text = e.KeyCode.ToString();
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
