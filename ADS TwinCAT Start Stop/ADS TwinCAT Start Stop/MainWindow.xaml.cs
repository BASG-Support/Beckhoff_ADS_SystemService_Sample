﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Xml.Linq;

using TwinCAT.Ads;

namespace ADS_TwinCAT_Start_Stop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TcAdsClient TcClient = new TcAdsClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Connect to client on start up. Connect to the system service port "10000", where you will use ADS_WriteControl to control the system state.

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                XDocument app_config = XDocument.Load("app_config.xml");
                TcClient.Connect(app_config.Root.Attribute("address").Value, (int)AmsPort.SystemService);
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region Run Mode button press triggers an ADS_WriteControl command. Sends the AdsState "Reset" to the server. The device state is not in use as far as I can tell.

        private void TwinCAT_Run_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TcClient.IsConnected)
                {
                    if (TcClient.ReadState().AdsState == AdsState.Run)
                    {
                        ShowDialog();
                    }
                    StateInfo TcRunMode = new StateInfo(AdsState.Reset, 1);
                    TcClient.WriteControl(TcRunMode);
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region Run Mode button press triggers an ADS_WriteControl command. Sends the AdsState "Reconfig" to the server. The device state is not in use as far as I can tell.

        private void TwinCAT_Config_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TcClient.IsConnected)
                {
                    if (TcClient.ReadState().AdsState == AdsState.Run)
                    {
                        Dialog _Popup = new Dialog();
                        if ((bool)_Popup.ShowDialog())
                        {
                            if (_Popup.OptionSelected == 1)
                            {
                                StateInfo TcConfigMode = new StateInfo(AdsState.Reconfig, 1);
                                TcClient.WriteControl(TcConfigMode);
                            }
                        }
                    }
                    else
                    {
                        StateInfo TcConfigMode = new StateInfo(AdsState.Reconfig, 1);
                        TcClient.WriteControl(TcConfigMode);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region Close connection to client on exit

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (TcClient.IsConnected)
            {
                TcClient.Dispose();
            }
        }

        #endregion

        #region Reconnect to the PLC.

        private void Reconnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool reconnect = true;
                if (TcClient.IsConnected)
                {
                    Dialog _Popup = new Dialog();
                    if ((bool)_Popup.ShowDialog())
                    {
                        if (_Popup.OptionSelected == 0) reconnect = false;
                        else TcClient.Dispose();
                    }
                }
                if (reconnect)
                {
                    XDocument app_config = XDocument.Load("app_config.xml");
                    TcClient.Connect(app_config.Root.Attribute("address").Value, (int)AmsPort.SystemService);
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

    }
}
