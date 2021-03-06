﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraMap;

namespace CustomRatios {
    public partial class Form1 : Form {
        const string filepath = @"..\..\Data\Countries.shp";
        const int defaultSideSize = 512;

        class WidthHeightRatio {
            public string Name { get; set; }
            public double Value { get; set; }

            public override string ToString() {
                return Name;
            }
        }

        List<WidthHeightRatio> ratios = new List<WidthHeightRatio>() {
            new WidthHeightRatio() { Name = "Default", Value = 1 },
            new WidthHeightRatio() { Name = "Lambert", Value = 3.14 },
            new WidthHeightRatio() { Name = "Behrmann", Value = 2.36 },
            new WidthHeightRatio() { Name = "Trystan Edwards", Value = 2 },
            new WidthHeightRatio() { Name = "Gall-Peters", Value = 1.57 },
            new WidthHeightRatio() { Name = "Balthasart", Value = 1.3 }
        };

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            cbRatio.Properties.Items.AddRange(ratios);
            cbRatio.SelectedIndex = 0;

            Uri baseUri = new Uri(System.Reflection.Assembly.GetEntryAssembly().Location);
            Uri uri = new Uri(baseUri, filepath);
            mapControl1.Layers.Add(new VectorItemsLayer() {
                Data = new ShapefileDataAdapter() { FileUri = uri }
            });
        }

        private void OnRatioSelectedIndexChanged(object sender, EventArgs e) {
            mapControl1.InitialMapSize = new Size() {
                Width = defaultSideSize,
                Height = (int)(defaultSideSize / ((WidthHeightRatio)cbRatio.SelectedItem).Value)
            };
        }
    }
}
