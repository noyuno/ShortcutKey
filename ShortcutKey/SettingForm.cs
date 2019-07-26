using ShortcutKey.Properties;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortcutKey
{
    public partial class SettingForm : Form
    {
        bool CloseByMenu = false;
        Keyboard screenshot;
        Keyboard topMost;
        const string none = "(none)";

        public SettingForm()
        {
            InitializeComponent();
            this.Text = Application.ProductName;
            this.Visible = false;
            var notify = new NotifyIcon();
            notify.Icon = Resources.Notify;
            notify.Visible = true;
            notify.Text = "ShortcutKey";
            notify.DoubleClick += (object sender, EventArgs e) =>
              {
                  this.WindowState = FormWindowState.Normal;
                  this.Show();
                  this.Activate();
              };
            var menu = new ContextMenuStrip();
            var settingMenuItem = new ToolStripMenuItem();
            settingMenuItem.Text = "&Setting";
            settingMenuItem.Click += (object sender, EventArgs e) =>
            {
                this.WindowState = FormWindowState.Normal;
                this.Show();
                this.Activate();
            };
            menu.Items.Add(settingMenuItem);
            var exitMenuItem = new ToolStripMenuItem();
            exitMenuItem.Text = "&Exit";
            exitMenuItem.Click += (object sender, EventArgs e) => {
                this.CloseByMenu = true;
                Application.Exit();
            };
            menu.Items.Add(exitMenuItem);
            notify.ContextMenuStrip = menu;

            this.screenshot = new Keyboard(async () =>
              {
                  var title = "Take screenshot";
                  this.Text = title;
                  Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                  Graphics g = Graphics.FromImage(bmp);
                  g.CopyFromScreen(new Point(0, 0), new Point(0, 0), bmp.Size);
                  g.Dispose();
                  try
                  {
                      if (!Directory.Exists(ScreenshotSavePathTextBox.Text))
                          throw new DirectoryNotFoundException();
                      bmp.Save(Path.Combine(ScreenshotSavePathTextBox.Text, DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".png"), ImageFormat.Png);
                  }
                  catch (Exception ex)
                  {
                      MessageBox.Show(ex.Message);
                  }
                  finally
                  {
                      bmp.Dispose();
                      await Task.Delay(1000);
                      ResetTitle(title);
                  }
              });
            this.screenshot.Key = Settings.Default.ScreenshotKey;

            this.topMost = new Keyboard(async () =>
              {
                  string title;
                  if (WindowManagement.GetTopMostActiveWindow())
                  {
                      title = "Release TopMost";
                      this.Text = title;
                      WindowManagement.ReleaseTopMostActiveWindow();
                  }
                  else
                  {
                      title = "TopMost";
                      this.Text = title;
                      WindowManagement.TopMostActiveWindow();
                  }
                  await Task.Delay(1000);
                  ResetTitle(title);
                  this.Text = Application.ProductName;
              });
            this.topMost.Key = Settings.Default.TopMostKey;

            this.ScreenshotCheckBox.Checked = Settings.Default.Screenshot;
            this.ScreenshotKeyButton.Text = Settings.Default.ScreenshotKey.ToString();
            this.ScreenshotSavePathTextBox.Text = Settings.Default.ScreenshotSaveAs;
            this.TopMostCheckBox.Checked = Settings.Default.TopMost;
            this.TopMostKeyButton.Text = Settings.Default.TopMostKey.ToString();
            this.AutoStartCheckBox.Checked = Settings.GetRegistory(@"Software\Microsoft\Windows\CurrentVersion\Run", Application.ProductName, "foo") != "foo";
        }

        void ResetTitle(string expected)
        {
            if (this.Text == expected)
                this.Text = Application.ProductName;
        }

        private void ScreenshotCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReflectScreenshotCheckBox();
        }

        void ReflectScreenshotCheckBox()
        {
            if (this.ScreenshotCheckBox.Checked)
            {
                this.screenshot.Start();
            }
            else
            {
                this.screenshot.Stop();
            }
            Settings.Default.Screenshot = this.ScreenshotCheckBox.Checked;
            Settings.Default.Save();
        }

        private void TopMostKeyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ReflectTopMostKeyCheckBox();
        }

        private void ReflectTopMostKeyCheckBox()
        {
            if (this.TopMostCheckBox.Checked)
            {
                this.topMost.Start();
            }
            else
            {
                this.topMost.Stop();
            }
            Settings.Default.TopMost = this.TopMostCheckBox.Checked;
            Settings.Default.Save();
        }

        private void AutoStartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (AutoStartCheckBox.Checked)
                    Settings.SetRegistory(@"Software\Microsoft\Windows\CurrentVersion\Run", Application.ProductName, Application.ExecutablePath);
                else
                    Settings.RemoveRegistory(@"Software\Microsoft\Windows\CurrentVersion\Run", Application.ProductName);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ScreenshotSaveToButton_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            fbd.SelectedPath = ScreenshotSavePathTextBox.Text;
            fbd.ShowNewFolderButton = true;
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                ScreenshotSavePathTextBox.Text = fbd.SelectedPath;
            }
        }

        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.CloseByMenu)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }


        private void PrintScreenSavePathTextBox_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ScreenshotSaveAs = this.ScreenshotSavePathTextBox.Text;
            Settings.Default.Save();
        }

        private bool GetKey(string target, Keys current, out Keys key)
        {
            key = Keys.None;
            var kf = new KeyForm(target, current);
            var dr = kf.ShowDialog();
            if (dr == DialogResult.Yes)
                key = kf.Key;
            else if (dr == DialogResult.No)
                key = Keys.None;
            else
                return false;
            return true;
        }

        private async void ScreenshotKeyButton_Click(object sender, EventArgs e)
        {
            Keys key;
            screenshot.Stop();
            topMost.Stop();
            if (GetKey("Screenshot Key", Settings.Default.ScreenshotKey, out key))
            {
                ScreenshotKeyButton.Text = key.ToString();
                Settings.Default.ScreenshotKey = key;
                Settings.Default.Save();
                screenshot.Key = key;
            }
            ReflectScreenshotCheckBox();
            ReflectTopMostKeyCheckBox();

        }


        private async void TopMostKeyButton_Click(object sender, EventArgs e)
        {
            Keys key;
            screenshot.Stop();
            topMost.Stop();
            if (GetKey("TopMost Key", Settings.Default.TopMostKey, out key))
            {
                TopMostKeyButton.Text = key.ToString();
                Settings.Default.TopMostKey = key;
                Settings.Default.Save();
                topMost.Key = key;
            }
            ReflectScreenshotCheckBox();
            ReflectTopMostKeyCheckBox();
        }
    }
}
