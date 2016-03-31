﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }        

        private void CreatePatternButton(Type type, int i)
        {
            Button btnPattern = new Button();
            var dnAttr = type.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;
            btnPattern.Text = dnAttr != null ? dnAttr.DisplayName : type.Name;
            btnPattern.Tag = type;
            btnPattern.Size = new Size(100, 100);
            btnPattern.Location = new Point(20 + 50 * i, 20 + 50 * (i / 3));
            btnPattern.Click -= BtnPattern_Click;
            btnPattern.Click += BtnPattern_Click;
            btnPattern.Tag = type;
            CreatePatternToolTip(type, btnPattern);
            Controls.Add(btnPattern);
        }

        private void CreatePatternToolTip(Type type, Button btnPattern)
        {
            var descrAttr = type.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (descrAttr != null)
            {
                ToolTip toolTipPattern = new ToolTip();
                toolTipPattern.ToolTipIcon = ToolTipIcon.Info;
                toolTipPattern.ToolTipTitle = btnPattern.Text;
                toolTipPattern.SetToolTip(btnPattern, GetToolTipWrapText(descrAttr.Description));
            }
        }

        static string GetToolTipWrapText(string text)
        {
            StringBuilder sb = new StringBuilder();
            int curLength = 0, N = 50;
            var parts = text.Split();
            foreach (var part in parts)
            {
                if (curLength + part.Length > N)
                {
                    sb.AppendLine($" {part}");
                    curLength = 0;
                }
                else
                {
                    if (sb.Length > 0)
                        sb.Append(' ');
                    sb.Append(part);
                    curLength += part.Length;
                }
            }
            return sb.ToString();
        }

        private void BtnPattern_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            var patternType = btn.Tag as Type;
            PatternForm patternForm = new PatternForm(patternType);
            patternForm.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var patternTypes = PatternLoader.GetPatternTypes();
            int i = 0;
            foreach (var type in patternTypes)
            {
                CreatePatternButton(type, i);
                i++;
            }
        }
    }
}