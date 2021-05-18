﻿using MobiFlight.OutputConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobiFlight.UI.Panels.Config
{
    public partial class SimConnectPanel : UserControl
    {
        public SimConnectPanel()
        {
            InitializeComponent();
            transformOptionsGroup1.setMode(true);
            transformOptionsGroup1.ShowSubStringPanel(false);
        }

        internal void syncToConfig(OutputConfigItem config)
        {
            config.SimConnectValue.VarType = SimConnectVarType.CODE;
            switch (config.SimConnectValue.VarType)
            {
                case SimConnectVarType.CODE:
                    config.SimConnectValue.Value = SimVarNameTextBox.Text;
                    break;
            }
            config.SimConnectValue.Value = SimVarNameTextBox.Text;
            transformOptionsGroup1.syncToConfig(config);
        }

        internal void syncFromConfig(OutputConfigItem config)
        {
            SimVarNameTextBox.Text = config.SimConnectValue.Value;
            transformOptionsGroup1.syncFromConfig(config);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://docs.flightsimulator.com/html/Programming_Tools/SimVars/Aircraft_Simulation_Variables.htm");
        }
    }
}