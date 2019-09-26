using Bunifu.Framework.UI;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace BlockEngine
{
    [DesignerGenerated]
    public class FrmHUI : Form
    {


        private Form Frm;

        private List<Control> CTRLs;

        public BunifuProgressBar BarHealth;

        public BunifuCustomLabel LblHealth;

        public Label LblMoney;
        public Panel PnlDown;

        public Panel PnlLeft;

        public Panel PnlRight;
        public Panel PnlUp;

        public FrmHUI()
        {
            this.CTRLs = new List<Control>();
            this.InitializeComponent();
        }

        public void Apply(System.Windows.Forms.Form Form)
        {
            if (Form == null)
            {
                Main.Log("HUI : Game Window not found");
                return;
            }
            IEnumerator enumerator = null;
            IEnumerator enumerator1 = null;
            Control.CheckForIllegalCrossThreadCalls = false;
            this.Frm = Form;
            this.CTRLs.Clear();
            base.Size = this.Frm.Size;
            this.Refresh();
            this.BarHealth.Width = checked(this.PnlDown.Width - this.LblHealth.Width);
            this.LblHealth.Left = this.BarHealth.Width;
            try
            {
                enumerator = base.Controls.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Control Pnl = (Control)enumerator.Current;
                    if (Pnl is Panel)
                    {
                        try
                        {
                            enumerator1 = Pnl.Controls.GetEnumerator();
                            while (enumerator1.MoveNext())
                            {
                                Control C = (Control)enumerator1.Current;
                                Control top = C;
                                top.Top = checked(top.Top + Pnl.Top);
                                Control left = C;
                                left.Left = checked(left.Left + Pnl.Left);
                                this.CTRLs.Add(C);
                            }
                        }
                        finally
                        {
                            if (enumerator1 is IDisposable)
                            {
                                (enumerator1 as IDisposable).Dispose();
                            }
                        }
                    }
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }
            this.Frm.Controls.Clear();
            this.Frm.Controls.AddRange(this.CTRLs.ToArray());
        }



        [DebuggerStepThrough]
        private void InitializeComponent()
        {

            this.PnlUp = new Panel();
            this.LblMoney = new Label();
            this.PnlDown = new Panel();
            this.LblHealth = new BunifuCustomLabel();
            this.BarHealth = new BunifuProgressBar();
            this.PnlLeft = new Panel();
            this.PnlRight = new Panel();
            this.PnlUp.SuspendLayout();
            this.PnlDown.SuspendLayout();
            base.SuspendLayout();
            this.PnlUp.Controls.Add(this.LblMoney);
            this.PnlUp.Dock = DockStyle.Top;
            this.PnlUp.Location = new Point(0, 0);
            this.PnlUp.Name = "PnlUp";
            this.PnlUp.Size = new System.Drawing.Size(984, 49);
            this.PnlUp.TabIndex = 0;
            this.LblMoney.AutoSize = true;
            this.LblMoney.BackColor = Color.White;
            this.LblMoney.Font = new System.Drawing.Font("Microsoft Sans Serif", 15f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.LblMoney.Location = new Point(9, 9);
            this.LblMoney.Margin = new System.Windows.Forms.Padding(0);
            this.LblMoney.Name = "LblMoney";
            this.LblMoney.Size = new System.Drawing.Size(74, 25);
            this.LblMoney.TabIndex = 0;
            this.LblMoney.Text = "◉ 1000";
            this.LblMoney.Visible = false;
            this.PnlDown.Controls.Add(this.LblHealth);
            this.PnlDown.Controls.Add(this.BarHealth);
            this.PnlDown.Dock = DockStyle.Bottom;
            this.PnlDown.Location = new Point(0, 453);
            this.PnlDown.Name = "PnlDown";
            this.PnlDown.Size = new System.Drawing.Size(984, 42);
            this.PnlDown.TabIndex = 0;
            this.LblHealth.AutoSize = true;
            this.LblHealth.BackColor = Color.Red;
            this.LblHealth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.LblHealth.Location = new Point(937, 21);
            this.LblHealth.Name = "LblHealth";
            this.LblHealth.Size = new System.Drawing.Size(45, 20);
            this.LblHealth.TabIndex = 1;
            this.LblHealth.Text = "1000";
            this.BarHealth.BackColor = Color.White;
            this.BarHealth.BorderRadius = 0;
            this.BarHealth.Location = new Point(1, 22);
            this.BarHealth.Margin = new System.Windows.Forms.Padding(0);
            this.BarHealth.MaximumValue = 100;
            this.BarHealth.Name = "BarHealth";
            this.BarHealth.ProgressColor = Color.Red;
            this.BarHealth.Size = new System.Drawing.Size(933, 20);
            this.BarHealth.TabIndex = 6;
            this.BarHealth.Value = 40;
            this.PnlLeft.Dock = DockStyle.Left;
            this.PnlLeft.Location = new Point(0, 49);
            this.PnlLeft.Name = "PnlLeft";
            this.PnlLeft.Size = new System.Drawing.Size(200, 404);
            this.PnlLeft.TabIndex = 0;
            this.PnlRight.Dock = DockStyle.Right;
            this.PnlRight.Location = new Point(784, 49);
            this.PnlRight.Name = "PnlRight";
            this.PnlRight.Size = new System.Drawing.Size(200, 404);
            this.PnlRight.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.CornflowerBlue;
            base.ClientSize = new System.Drawing.Size(984, 495);
            base.Controls.Add(this.PnlLeft);
            base.Controls.Add(this.PnlRight);
            base.Controls.Add(this.PnlDown);
            base.Controls.Add(this.PnlUp);
            base.Name = "FrmHUI";
            this.Text = "FrmHUI";
            base.TransparencyKey = Color.RosyBrown;
            this.PnlUp.ResumeLayout(false);
            this.PnlUp.PerformLayout();
            this.PnlDown.ResumeLayout(false);
            this.PnlDown.PerformLayout();
            base.ResumeLayout(false);


        }

        public void UpdateUI()
        {
            this.BarHealth.Value = checked((int)Math.Round(Main.Player1.Health / (double)Main.Player1.MaxHealth * 100));
            this.LblHealth.Text = Conversions.ToString(Main.Player1.Health);
            this.BarHealth.Width = checked(this.PnlDown.Width - this.LblHealth.Width);
            this.LblHealth.Left = this.BarHealth.Width;
            this.LblMoney.Text = string.Concat("◉ ", Main.Player1.Money.ToString());
            this.Refresh();
        }
    }
}