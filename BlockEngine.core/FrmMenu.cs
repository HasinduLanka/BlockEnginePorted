using Bunifu.Framework.UI;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.IO;

namespace BlockEngine
{
    public class FrmMenu : Form
    {

        public List<string> Maps;

        public List<string> BiomeLst;

        private FrmSettings FrmSettings1;

        public Button BtnCreateNewMap;

        public BunifuThinButton2 BtnGraphicSetts;
        public Button BtnStart;

        public Button Button1;

        public CheckBox ChkSS;
        public ComboBox CmbBiomes;

        public Label Label1;

        public Label Label2;

        public Label Label5;
        public ListBox LstMaps;

        public Panel Panel1;
        public TextBox TxtMapName;

        public TextBox TxtWorldSize;

        public FrmMenu()
        {
            base.Load += new EventHandler(this.FrmMenu_Load);
            this.Maps = new List<string>();
            this.BiomeLst = new List<string>();
            this.FrmSettings1 = new FrmSettings();


            this.InitializeComponent();


            BtnCreateNewMap.Click += BtnCreateNewMap_Click;
            BtnGraphicSetts.Click += BtnGraphicSetts_Click;
            BtnStart.Click += BtnStart_Click;
            Button1.Click += Button1_Click;
            Label1.Click += Label1_Click;
            Label5.MouseEnter += Label4_MouseEnter;
            Label5.MouseLeave += Label4_MouseLeave;
        }

        private void BtnCreateNewMap_Click(object sender, EventArgs e)
        {
            if (this.CmbBiomes.SelectedIndex < 0 | Operators.CompareString(this.TxtMapName.Text, "", false) == 0 | Operators.CompareString(this.TxtWorldSize.Text, "", false) == 0 | !Versioned.IsNumeric(this.TxtWorldSize.Text))
            {
                Interaction.MsgBox("Please recorrect the details.", MsgBoxStyle.OkOnly, null);
            }
            else if (Conversion.Val(this.TxtWorldSize.Text) > 20)
            {
                Main.MapVariablePipeline.NewMap = true;
                Main.MapVariablePipeline.NewMapBiome = this.CmbBiomes.SelectedIndex;
                Main.MapVariablePipeline.NewMapSpeedSave = this.ChkSS.Checked;
                Main.MapVariablePipeline.NewMapSize = checked((int)Math.Round(Conversion.Val(this.TxtWorldSize.Text)));
                Main.CurrentMapName = this.TxtMapName.Text;
                Main.ShowLoger();
                base.Hide();
                Main.StartGame();
            }
            else
            {
                Interaction.MsgBox("World size must be geater than 20", MsgBoxStyle.OkOnly, null);
            }
        }

        private void BtnGraphicSetts_Click(object sender, EventArgs e)
        {
            this.FrmSettings1.ShowDialog();
            // MySettingsProperty.Settings.Save();

        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (this.LstMaps.SelectedIndex <= -1)
            {
                Interaction.MsgBox("Please Create a new map or select one from the list", MsgBoxStyle.OkOnly, null);
            }
            else if (!Loader.CheckIfMapExits(this.LstMaps.SelectedItem.ToString()))
            {
                Interaction.MsgBox("This Map does not exist. Please Create a new map", MsgBoxStyle.OkOnly, null);
            }
            else
            {
                Main.CurrentMapName = this.LstMaps.SelectedItem.ToString();
                Main.ShowLoger();
                base.Hide();
                Main.StartGame();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (this.LstMaps.SelectedIndex > -1)
            {
                try
                {
                    Directory.Delete(string.Concat("Maps/", this.LstMaps.SelectedItem.ToString()), true);
                }
                catch (Exception exception)
                {
                    ProjectData.SetProjectError(exception);
                    Main.Log(exception.Message, true);
                    ProjectData.ClearProjectError();
                }
            }
            this.LstMaps.Items.Clear();

            var enumerator = Directory.EnumerateDirectories("Maps").GetEnumerator();
            while (enumerator.MoveNext())
            {
                string S = enumerator.Current;
                string MapName = S.Substring(checked(S.LastIndexOf('\\') + 1));
                if (Loader.CheckIfMapExits(MapName))
                {
                    this.Maps.Add(MapName);
                    this.LstMaps.Items.Add(MapName);
                    this.LstMaps.SelectedIndex = 0;
                }

            }
        }



        private void FrmMenu_Load(object sender, EventArgs e)
        {

            Directory.CreateDirectory("Maps");

            this.LstMaps.Items.Clear();
            var enumerator = Directory.EnumerateDirectories("Maps").GetEnumerator();
            while (enumerator.MoveNext())
            {
                string S = enumerator.Current;
                string MapName = S.Substring(checked(S.LastIndexOf('\\') + 1));
                if (Loader.CheckIfMapExits(MapName))
                {
                    this.Maps.Add(MapName);
                    this.LstMaps.Items.Add(MapName);
                    this.LstMaps.SelectedIndex = 0;
                }

            }
            this.BiomeLst = Enum.GetNames(typeof(BiomeList.Biomes)).ToList<string>();
            this.CmbBiomes.Items.AddRange(this.BiomeLst.ToArray());
            this.CmbBiomes.SelectedIndex = 0;
            this.FrmSettings1.CalGraphicQuality(100);//(checked((int)Math.Round((double)MySettingsProperty.Settings.GraphicQuality)));
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(FrmMenu));
            this.Label1 = new Label();
            this.Label2 = new Label();
            this.LstMaps = new ListBox();
            this.TxtMapName = new TextBox();
            this.Panel1 = new Panel();
            this.BtnCreateNewMap = new Button();
            this.ChkSS = new CheckBox();
            this.Label5 = new Label();
            this.TxtWorldSize = new TextBox();
            this.CmbBiomes = new ComboBox();
            this.Button1 = new Button();
            this.BtnStart = new Button();
            this.BtnGraphicSetts = new BunifuThinButton2();
            this.Panel1.SuspendLayout();
            base.SuspendLayout();
            this.Label1.AutoSize = true;
            this.Label1.BackColor = Color.Gray;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Label1.ForeColor = Color.DarkRed;
            this.Label1.Location = new Point(599, 2);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(32, 31);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "X";
            this.Label2.AutoSize = true;
            this.Label2.BackColor = SystemColors.ControlDark;
            this.Label2.Font = new System.Drawing.Font("Elephant", 45f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Label2.ForeColor = Color.FromArgb(20, 20, 20);
            this.Label2.Location = new Point(35, 9);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(553, 77);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Block Engine 3D";
            this.LstMaps.BackColor = Color.FromArgb(64, 64, 64);
            this.LstMaps.BorderStyle = BorderStyle.FixedSingle;
            this.LstMaps.Font = new System.Drawing.Font("Microsoft Sans Serif", 20f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.LstMaps.ForeColor = Color.FromArgb(224, 224, 224);
            this.LstMaps.FormattingEnabled = true;
            this.LstMaps.ItemHeight = 31;
            this.LstMaps.Location = new Point(362, 107);
            this.LstMaps.Name = "LstMaps";
            this.LstMaps.Size = new System.Drawing.Size(259, 343);
            this.LstMaps.TabIndex = 3;
            this.TxtMapName.BackColor = Color.Silver;
            this.TxtMapName.BorderStyle = BorderStyle.FixedSingle;
            this.TxtMapName.Font = new System.Drawing.Font("Microsoft Sans Serif", 17f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.TxtMapName.Location = new Point(5, 78);
            this.TxtMapName.Name = "TxtMapName";
            this.TxtMapName.Size = new System.Drawing.Size(100, 33);
            this.TxtMapName.TabIndex = 4;
            this.TxtMapName.Text = "Map1";
            this.Panel1.BackColor = SystemColors.ControlDark;
            this.Panel1.BorderStyle = BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.BtnCreateNewMap);
            this.Panel1.Controls.Add(this.ChkSS);
            this.Panel1.Controls.Add(this.Label5);
            this.Panel1.Controls.Add(this.TxtWorldSize);
            this.Panel1.Controls.Add(this.CmbBiomes);
            this.Panel1.Controls.Add(this.TxtMapName);
            this.Panel1.Location = new Point(7, 227);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(338, 223);
            this.Panel1.TabIndex = 5;
            this.BtnCreateNewMap.BackColor = Color.FromArgb(64, 64, 64);
            this.BtnCreateNewMap.FlatAppearance.MouseDownBackColor = Color.Black;
            this.BtnCreateNewMap.FlatAppearance.MouseOverBackColor = Color.Gray;
            this.BtnCreateNewMap.FlatStyle = FlatStyle.Flat;
            this.BtnCreateNewMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.BtnCreateNewMap.ForeColor = SystemColors.HighlightText;
            this.BtnCreateNewMap.Location = new Point(4, 13);
            this.BtnCreateNewMap.Name = "BtnCreateNewMap";
            this.BtnCreateNewMap.Size = new System.Drawing.Size(329, 47);
            this.BtnCreateNewMap.TabIndex = 7;
            this.BtnCreateNewMap.Text = "■ Create new map";
            this.BtnCreateNewMap.TextAlign = ContentAlignment.MiddleLeft;
            this.BtnCreateNewMap.UseVisualStyleBackColor = false;
            this.ChkSS.AutoSize = true;
            this.ChkSS.Checked = true;
            this.ChkSS.CheckState = CheckState.Checked;
            this.ChkSS.FlatAppearance.MouseDownBackColor = Color.Black;
            this.ChkSS.FlatAppearance.MouseOverBackColor = Color.FromArgb(64, 64, 64);
            this.ChkSS.FlatStyle = FlatStyle.Flat;
            this.ChkSS.Font = new System.Drawing.Font("Microsoft Sans Serif", 15f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.ChkSS.Location = new Point(85, 148);
            this.ChkSS.Name = "ChkSS";
            this.ChkSS.Size = new System.Drawing.Size(152, 29);
            this.ChkSS.TabIndex = 8;
            this.ChkSS.Text = "Speed Saving";
            this.ChkSS.UseVisualStyleBackColor = true;
            this.Label5.AutoSize = true;
            this.Label5.BackColor = SystemColors.ControlDark;
            this.Label5.Location = new Point(13, 134);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(58, 13);
            this.Label5.TabIndex = 7;
            this.Label5.Text = "World Size";
            this.TxtWorldSize.BackColor = Color.Silver;
            this.TxtWorldSize.BorderStyle = BorderStyle.FixedSingle;
            this.TxtWorldSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 17f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.TxtWorldSize.Location = new Point(14, 151);
            this.TxtWorldSize.Name = "TxtWorldSize";
            this.TxtWorldSize.Size = new System.Drawing.Size(57, 33);
            this.TxtWorldSize.TabIndex = 6;
            this.TxtWorldSize.Text = "50";
            this.CmbBiomes.BackColor = Color.Silver;
            this.CmbBiomes.FlatStyle = FlatStyle.Flat;
            this.CmbBiomes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.CmbBiomes.FormattingEnabled = true;
            this.CmbBiomes.Location = new Point(111, 78);
            this.CmbBiomes.Name = "CmbBiomes";
            this.CmbBiomes.Size = new System.Drawing.Size(211, 33);
            this.CmbBiomes.TabIndex = 5;
            this.Button1.BackColor = Color.FromArgb(64, 64, 64);
            this.Button1.FlatAppearance.MouseDownBackColor = Color.Black;
            this.Button1.FlatAppearance.MouseOverBackColor = Color.Gray;
            this.Button1.FlatStyle = FlatStyle.Flat;
            this.Button1.Location = new Point(13, 170);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(332, 23);
            this.Button1.TabIndex = 6;
            this.Button1.Text = "Delete";
            this.Button1.UseVisualStyleBackColor = false;
            this.BtnStart.BackColor = Color.FromArgb(64, 64, 64);
            this.BtnStart.FlatAppearance.MouseDownBackColor = Color.Black;
            this.BtnStart.FlatAppearance.MouseOverBackColor = Color.Gray;
            this.BtnStart.FlatStyle = FlatStyle.Flat;
            this.BtnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.BtnStart.ForeColor = SystemColors.HighlightText;
            this.BtnStart.Location = new Point(13, 117);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(332, 47);
            this.BtnStart.TabIndex = 7;
            this.BtnStart.Text = "■  Start";
            this.BtnStart.TextAlign = ContentAlignment.MiddleLeft;
            this.BtnStart.UseVisualStyleBackColor = false;
            this.BtnGraphicSetts.ActiveBorderThickness = 1;
            this.BtnGraphicSetts.ActiveCornerRadius = 20;
            this.BtnGraphicSetts.ActiveFillColor = Color.Cyan;
            this.BtnGraphicSetts.ActiveForecolor = Color.FromArgb(64, 64, 64);
            this.BtnGraphicSetts.ActiveLineColor = Color.Yellow;
            this.BtnGraphicSetts.BackColor = SystemColors.GrayText;
            this.BtnGraphicSetts.BackgroundImage = (Image)resources.GetObject("BtnGraphicSetts.BackgroundImage");
            this.BtnGraphicSetts.ButtonText = "Settings";
            this.BtnGraphicSetts.Cursor = Cursors.Hand;
            this.BtnGraphicSetts.Font = new System.Drawing.Font("Century Gothic", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.BtnGraphicSetts.ForeColor = Color.White;
            this.BtnGraphicSetts.IdleBorderThickness = 1;
            this.BtnGraphicSetts.IdleCornerRadius = 20;
            this.BtnGraphicSetts.IdleFillColor = Color.FromArgb(64, 64, 64);
            this.BtnGraphicSetts.IdleForecolor = Color.White;
            this.BtnGraphicSetts.IdleLineColor = Color.Cyan;
            this.BtnGraphicSetts.Location = new Point(7, 458);
            this.BtnGraphicSetts.Margin = new System.Windows.Forms.Padding(5);
            this.BtnGraphicSetts.Name = "BtnGraphicSetts";
            this.BtnGraphicSetts.Size = new System.Drawing.Size(142, 50);
            this.BtnGraphicSetts.TabIndex = 8;
            this.BtnGraphicSetts.TextAlign = ContentAlignment.MiddleCenter;
            base.AcceptButton = this.BtnStart;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = SystemColors.GrayText;
            base.ClientSize = new System.Drawing.Size(633, 518);
            base.Controls.Add(this.BtnGraphicSetts);
            base.Controls.Add(this.BtnStart);
            base.Controls.Add(this.Button1);
            base.Controls.Add(this.Panel1);
            base.Controls.Add(this.LstMaps);
            base.Controls.Add(this.Label2);
            base.Controls.Add(this.Label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            base.Name = "FrmMenu";
            this.Text = "FrmMenu";
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            Main.ExitProgram();
        }

        private void Label4_MouseEnter(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.DodgerBlue;
        }

        private void Label4_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).ForeColor = Color.White;
        }

        private void LstMaps_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.LstMaps.SelectedIndex > -1)
            {
                this.BtnStart_Click(null, null);
            }
        }
    }
}