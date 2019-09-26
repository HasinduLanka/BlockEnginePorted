using Bunifu.Framework.UI;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace BlockEngine
{
    [DesignerGenerated]
    public class FrmSettings : Form
    {

        public int BarValue;

        public Label Label1;

        public Label LblGraphicQuality;

        public Label LblLODAndBuff;

        public BunifuSlider SliderGQ;

        public FrmSettings()
        {
            base.Load += new EventHandler(this.FrmSettings_Load);
            base.Closing += new CancelEventHandler(this.FrmSettings_Closing);
            base.Shown += new EventHandler(this.FrmSettings_Shown);
            this.BarValue = 100;
            this.InitializeComponent();
        }

        private void BunifuSlider1_ValueChanged(object sender, EventArgs e)
        {
            this.CalGraphicQuality(this.SliderGQ.Value);
        }

        public void CalGraphicQuality(int Value)
        {
            double LODBias;
            Main.MapVariablePipeline.GraphicQuality = (double)Value / 100;
            LODBias = (Main.MapVariablePipeline.GraphicQuality <= 1.5 ? -(1.5 - Main.MapVariablePipeline.GraphicQuality) * 0.6 - 0.8 : (Main.MapVariablePipeline.GraphicQuality - 1) * 0.5 - 0.8);
            this.LblGraphicQuality.Text = string.Concat(Conversions.ToString(Value), "%");
            this.LblLODAndBuff.Text = string.Format("LOD Bias {0}  Buffer Ratio {1}", LODBias, Main.MapVariablePipeline.GraphicQuality);
            Main.MapVariablePipeline.LODBias = LODBias;
            this.BarValue = Value;
        }



        private void FrmSettings_Closing(object sender, CancelEventArgs e)
        {
            this.CalGraphicQuality(this.SliderGQ.Value);
            // MySettingsProperty.Settings.GraphicQuality = (float)this.BarValue;
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            this.SliderGQ.Value = this.BarValue;
        }

        private void FrmSettings_Shown(object sender, EventArgs e)
        {
            this.SliderGQ.Value = this.BarValue;
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            this.SliderGQ = new BunifuSlider();
            this.Label1 = new Label();
            this.LblGraphicQuality = new Label();
            this.LblLODAndBuff = new Label();
            base.SuspendLayout();
            this.SliderGQ.BackColor = Color.Transparent;
            this.SliderGQ.BackgroudColor = Color.DarkGray;
            this.SliderGQ.BorderRadius = 0;
            this.SliderGQ.IndicatorColor = Color.SeaGreen;
            this.SliderGQ.Location = new Point(170, 18);
            this.SliderGQ.MaximumValue = 400;
            this.SliderGQ.Name = "SliderGQ";
            this.SliderGQ.Size = new System.Drawing.Size(380, 30);
            this.SliderGQ.TabIndex = 0;
            this.SliderGQ.Value = 100;
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Label1.Location = new Point(8, 20);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(117, 20);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "Graphic Quality";
            this.LblGraphicQuality.AutoSize = true;
            this.LblGraphicQuality.Location = new Point(131, 25);
            this.LblGraphicQuality.Name = "LblGraphicQuality";
            this.LblGraphicQuality.Size = new System.Drawing.Size(33, 13);
            this.LblGraphicQuality.TabIndex = 2;
            this.LblGraphicQuality.Text = "100%";
            this.LblLODAndBuff.AutoSize = true;
            this.LblLODAndBuff.Location = new Point(12, 40);
            this.LblLODAndBuff.Name = "LblLODAndBuff";
            this.LblLODAndBuff.Size = new System.Drawing.Size(144, 13);
            this.LblLODAndBuff.TabIndex = 2;
            this.LblLODAndBuff.Text = "LOD Bias -0.8  Buffer Ratio 1";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(555, 523);
            base.Controls.Add(this.LblLODAndBuff);
            base.Controls.Add(this.LblGraphicQuality);
            base.Controls.Add(this.Label1);
            base.Controls.Add(this.SliderGQ);
            base.Name = "FrmSettings";
            this.Text = "Settings";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}