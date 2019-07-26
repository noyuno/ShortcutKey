namespace ShortcutKey
{
    partial class SettingForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.ScreenshotCheckBox = new System.Windows.Forms.CheckBox();
            this.ScreenshotSavePathTextBox = new System.Windows.Forms.TextBox();
            this.PrintScreenSaveToButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TopMostCheckBox = new System.Windows.Forms.CheckBox();
            this.AutoStartCheckBox = new System.Windows.Forms.CheckBox();
            this.ScreenshotKeyButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TopMostKeyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ScreenshotCheckBox
            // 
            this.ScreenshotCheckBox.AutoSize = true;
            this.ScreenshotCheckBox.Location = new System.Drawing.Point(12, 12);
            this.ScreenshotCheckBox.Name = "ScreenshotCheckBox";
            this.ScreenshotCheckBox.Size = new System.Drawing.Size(150, 27);
            this.ScreenshotCheckBox.TabIndex = 0;
            this.ScreenshotCheckBox.Text = "Take screenshot";
            this.ScreenshotCheckBox.UseVisualStyleBackColor = true;
            this.ScreenshotCheckBox.CheckedChanged += new System.EventHandler(this.ScreenshotCheckBox_CheckedChanged);
            // 
            // ScreenshotSavePathTextBox
            // 
            this.ScreenshotSavePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScreenshotSavePathTextBox.Location = new System.Drawing.Point(108, 45);
            this.ScreenshotSavePathTextBox.Name = "ScreenshotSavePathTextBox";
            this.ScreenshotSavePathTextBox.Size = new System.Drawing.Size(338, 30);
            this.ScreenshotSavePathTextBox.TabIndex = 0;
            this.ScreenshotSavePathTextBox.TextChanged += new System.EventHandler(this.PrintScreenSavePathTextBox_TextChanged);
            // 
            // PrintScreenSaveToButton
            // 
            this.PrintScreenSaveToButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PrintScreenSaveToButton.Location = new System.Drawing.Point(452, 42);
            this.PrintScreenSaveToButton.Name = "PrintScreenSaveToButton";
            this.PrintScreenSaveToButton.Size = new System.Drawing.Size(42, 30);
            this.PrintScreenSaveToButton.TabIndex = 1;
            this.PrintScreenSaveToButton.Text = "...";
            this.PrintScreenSaveToButton.UseVisualStyleBackColor = true;
            this.PrintScreenSaveToButton.Click += new System.EventHandler(this.ScreenshotSaveToButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Save to";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TopMostCheckBox
            // 
            this.TopMostCheckBox.AutoSize = true;
            this.TopMostCheckBox.Location = new System.Drawing.Point(12, 84);
            this.TopMostCheckBox.Name = "TopMostCheckBox";
            this.TopMostCheckBox.Size = new System.Drawing.Size(99, 27);
            this.TopMostCheckBox.TabIndex = 2;
            this.TopMostCheckBox.Text = "Top most";
            this.TopMostCheckBox.UseVisualStyleBackColor = true;
            this.TopMostCheckBox.CheckedChanged += new System.EventHandler(this.TopMostKeyCheckBox_CheckedChanged);
            // 
            // AutoStartCheckBox
            // 
            this.AutoStartCheckBox.AutoSize = true;
            this.AutoStartCheckBox.Location = new System.Drawing.Point(12, 117);
            this.AutoStartCheckBox.Name = "AutoStartCheckBox";
            this.AutoStartCheckBox.Size = new System.Drawing.Size(108, 27);
            this.AutoStartCheckBox.TabIndex = 3;
            this.AutoStartCheckBox.Text = "Auto Start";
            this.AutoStartCheckBox.UseVisualStyleBackColor = true;
            this.AutoStartCheckBox.CheckedChanged += new System.EventHandler(this.AutoStartCheckBox_CheckedChanged);
            // 
            // ScreenshotKeyButton
            // 
            this.ScreenshotKeyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScreenshotKeyButton.Location = new System.Drawing.Point(376, 9);
            this.ScreenshotKeyButton.Name = "ScreenshotKeyButton";
            this.ScreenshotKeyButton.Size = new System.Drawing.Size(118, 30);
            this.ScreenshotKeyButton.TabIndex = 4;
            this.ScreenshotKeyButton.Text = "...";
            this.ScreenshotKeyButton.UseVisualStyleBackColor = true;
            this.ScreenshotKeyButton.Click += new System.EventHandler(this.ScreenshotKeyButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(327, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Key:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(327, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "Key:";
            // 
            // TopMostKeyButton
            // 
            this.TopMostKeyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TopMostKeyButton.Location = new System.Drawing.Point(376, 81);
            this.TopMostKeyButton.Name = "TopMostKeyButton";
            this.TopMostKeyButton.Size = new System.Drawing.Size(118, 30);
            this.TopMostKeyButton.TabIndex = 6;
            this.TopMostKeyButton.Text = "...";
            this.TopMostKeyButton.UseVisualStyleBackColor = true;
            this.TopMostKeyButton.Click += new System.EventHandler(this.TopMostKeyButton_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(506, 153);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TopMostKeyButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ScreenshotKeyButton);
            this.Controls.Add(this.AutoStartCheckBox);
            this.Controls.Add(this.PrintScreenSaveToButton);
            this.Controls.Add(this.ScreenshotSavePathTextBox);
            this.Controls.Add(this.TopMostCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ScreenshotCheckBox);
            this.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.ShowInTaskbar = false;
            this.Text = "Shortcut Key";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ScreenshotCheckBox;
        private System.Windows.Forms.TextBox ScreenshotSavePathTextBox;
        private System.Windows.Forms.Button PrintScreenSaveToButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox TopMostCheckBox;
        private System.Windows.Forms.CheckBox AutoStartCheckBox;
        private System.Windows.Forms.Button ScreenshotKeyButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button TopMostKeyButton;
    }
}

