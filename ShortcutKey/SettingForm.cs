using ShortcutKey.Properties;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ShortcutKey
{
    public partial class SettingForm : Form
    {
        bool CloseByMenu = false;
        readonly Keyboard screenshot;
        readonly Keyboard topMost;
        const string registoryStartupPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
        NotifyIcon notifyIcon;

        public SettingForm()
        {
            InitializeComponent();
            this.Text = Application.ProductName;
            this.Visible = false;
            this.notifyIcon = new NotifyIcon
            {
                Icon = Resources.Notify,
                Visible = true,
                Text = "ShortcutKey"
            };
            notifyIcon.DoubleClick += (object sender, EventArgs e) =>
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
            var exitMenuItem = new ToolStripMenuItem
            {
                Text = "&Exit"
            };
            exitMenuItem.Click += (object sender, EventArgs e) => {
                this.CloseByMenu = true;
                Application.Exit();
            };
            menu.Items.Add(exitMenuItem);
            notifyIcon.ContextMenuStrip = menu;

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
                      var filename = DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".png";
                      var dest = Path.Combine(ScreenshotSavePathTextBox.Text, filename);
                      if (File.Exists(Path.Combine(
                          Path.GetDirectoryName(Application.ExecutablePath), "pngquant.exe")))
                      {
                          var file = Path.GetTempFileName() + ".png";
                          bmp.Save(file, ImageFormat.Png);
                          var p = Process.Start(new ProcessStartInfo("pngquant.exe",
                              string.Format("--force --verbose --quality=50-95 -o {0} -- {1}", dest, file))
                          {
                              UseShellExecute = false,
                              CreateNoWindow = true,
                              RedirectStandardOutput = true,
                              RedirectStandardError = true
                          });
                          string stdout, stderr;
                          if ((stdout = p.StandardOutput.ReadToEnd()) != "")
                          {
                              Debug.Write(stdout.Replace("\r\r\n", "\n"));
                          }
                          if(( stderr = p.StandardError.ReadToEnd())!="")
                          {
                              Debug.Write(stderr.Replace("\r\r\n", "\n"));
                          }
                          if (p.ExitCode!=0)
                          {
                              throw new System.ComponentModel.Win32Exception(p.ExitCode, stderr);
                          }

                          var q = Regex.Match(stderr, @"\(Q=[0-9][0-9]\)");
                          string t;
                          if (q.Success)
                          {
                              t = string.Format("file: {0}\npngquant quality: {1}", filename, q);
                          }
                          else
                          {
                              t = string.Format("file: {0}\npngquant quality: ?", filename);
                          }
                          //this.notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                          var defaultIcon = (Icon)this.notifyIcon.Icon.Clone();
                          this.notifyIcon.Icon = Icon.FromHandle(bmp.GetHicon());
                          this.notifyIcon.BalloonTipTitle = "Screenshot";
                          this.notifyIcon.BalloonTipText = t;
                          this.notifyIcon.BalloonTipClicked += (sender, e) =>
                          {
                              Process.Start(dest);
                          };
                          this.notifyIcon.ShowBalloonTip(1000);
                          await Task.Delay(1000);
                          this.notifyIcon.Icon = defaultIcon;
                      }
                      else
                      {
                          bmp.Save(dest, ImageFormat.Png);
                          var defaultIcon = (Icon)this.notifyIcon.Icon.Clone();
                          this.notifyIcon.Icon = Icon.FromHandle(bmp.GetHicon());
                          //this.notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                          this.notifyIcon.BalloonTipTitle = "Screenshot";
                          this.notifyIcon.BalloonTipText = string.Format("file: {0}", filename);
                          this.notifyIcon.BalloonTipClicked += (sender, e) =>
                            {
                                Process.Start(dest);
                            };
                          this.notifyIcon.ShowBalloonTip(1000);
                          await Task.Delay(1000);
                          this.notifyIcon.Icon = defaultIcon;

                      }
                  }
                  catch (System.ComponentModel.Win32Exception ex)
                  {
                      MessageBox.Show(ex.Message, string.Format("pngquant returned code {0}", ex.ErrorCode), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
              })
            {
                Key = Settings.Default.ScreenshotKey
            };

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
              })
            {
                Key = Settings.Default.TopMostKey
            };

            this.ScreenshotCheckBox.Checked = Settings.Default.Screenshot;
            this.ScreenshotKeyButton.Text = Settings.Default.ScreenshotKey.ToString();
            this.ScreenshotSavePathTextBox.Text = Settings.Default.ScreenshotSaveAs;
            this.TopMostCheckBox.Checked = Settings.Default.TopMost;
            this.TopMostKeyButton.Text = Settings.Default.TopMostKey.ToString();
            this.AutoStartCheckBox.Checked = Settings.GetRegistory(registoryStartupPath, Application.ProductName, "foo") != "foo";
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
                    Settings.SetRegistory(registoryStartupPath, Application.ProductName, Application.ExecutablePath);
                else
                    Settings.RemoveRegistory(registoryStartupPath, Application.ProductName);
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
            using (var kf = new KeyForm(target, current))
            {
                var dr = kf.ShowDialog();
                if (dr == DialogResult.Yes)
                    key = kf.Key;
                else if (dr == DialogResult.No)
                    key = Keys.None;
                else
                    return false;
                return true;
            }
        }

        private void ScreenshotKeyButton_Click(object sender, EventArgs e)
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


        private void TopMostKeyButton_Click(object sender, EventArgs e)
        {
            screenshot.Stop();
            topMost.Stop();
            if (GetKey("TopMost Key", Settings.Default.TopMostKey, out Keys key))
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
