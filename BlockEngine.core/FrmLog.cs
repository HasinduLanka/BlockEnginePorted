using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace BlockEngine
{
    [DesignerGenerated]
    public class FrmLog : Form
    {
        private IContainer components;

        public long nLog;

        public long Now_nLog;

        public System.Windows.Forms.Timer TmrLog;

        public TextBox TxtLog;

        public FrmLog()
        {
            base.Load += new EventHandler(this.FrmLog_Load);
            this.nLog = (long)0;
            this.Now_nLog = (long)0;
            this.InitializeComponent();
        }

        [DebuggerNonUserCode]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if ((!disposing ? false : this.components != null))
                {
                    this.components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        private void FrmLog_Load(object sender, EventArgs e)
        {
            this.TmrLog.Start();
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TxtLog = new TextBox();
            this.TmrLog = new System.Windows.Forms.Timer(this.components);
            base.SuspendLayout();
            this.TxtLog.BackColor = Color.FromArgb(50, 50, 50);
            this.TxtLog.BorderStyle = BorderStyle.None;
            this.TxtLog.Dock = DockStyle.Fill;
            this.TxtLog.ForeColor = SystemColors.MenuHighlight;
            this.TxtLog.Location = new Point(0, 0);
            this.TxtLog.Multiline = true;
            this.TxtLog.Name = "TxtLog";
            this.TxtLog.ReadOnly = true;
            this.TxtLog.Size = new System.Drawing.Size(543, 384);
            this.TxtLog.TabIndex = 7;
            this.TmrLog.Interval = 500;
            TmrLog.Tick += TmrLog_Tick;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(543, 384);
            base.Controls.Add(this.TxtLog);
            TxtLog.KeyDown += TxtLog_KeyDown;
            base.Name = "FrmLog";
            this.Text = "Log";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void LogUpdate(long n)
        {
            while (this.nLog != n)
            {
                this.nLog = n;
                Thread.Sleep(10);
            }
        }

        private void TmrLog_Tick(object sender, EventArgs e)
        {
            if (this.nLog != this.Now_nLog)
            {
                this.TxtLog.Text = Main.LogContent;
                this.Now_nLog = this.nLog;
            }
        }

        private void TxtLog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Main.ExitApp();
            }
        }
    }
}