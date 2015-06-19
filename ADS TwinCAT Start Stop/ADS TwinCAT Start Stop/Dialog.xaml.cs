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
using System.Windows.Shapes;

namespace ADS_TwinCAT_Start_Stop
{
    /// <summary>
    /// Interaction logic for Dialog.xaml
    /// </summary>
    public partial class Dialog : Window
    {
        public Dialog()
        {
            InitializeComponent();
        }

        public string Message
        {
            get { return MessageArea.Content.ToString(); }
            set { MessageArea.Content = value; }
        }

        public int OptionSelected
        {
            get;
            set;
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected = 1;
            DialogResult = true;
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected = 0;
            DialogResult = true;
        }



    }
}
