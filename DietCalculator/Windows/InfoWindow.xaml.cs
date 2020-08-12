﻿using DietCalculator.Logic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DietCalculator.Windows
{
    /// <summary>
    /// Lógica de interacción para InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        public InfoWindow()
        {
            InitializeComponent();

            var info = MainController.Instance.GetInfo();
            LblRecipesCount.Content = info.Item1.ToString();
            LblIngredientsCount.Content = info.Item2.ToString();
            LblToolsCount.Content = info.Item3.ToString();
        }

        private void BtnProlog_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnScheme_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
