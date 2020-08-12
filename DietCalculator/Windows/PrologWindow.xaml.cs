using DietCalculator.Logic;
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
    /// Lógica de interacción para PrologWindow.xaml
    /// </summary>
    public partial class PrologWindow : Window
    {
        public PrologWindow()
        {
            InitializeComponent();
        }

        private void BtnFirst_Click(object sender, RoutedEventArgs e)
        {
            var recipes = MainController.Instance.FirstPrologQuery(TxtFirst.Text);
            ListView.ItemsSource = recipes;
        }

        private void BtnSecond_Click(object sender, RoutedEventArgs e)
        {
            var recipes = MainController.Instance.SecondPrologQuery(TxtSecond.Text);
            ListView.ItemsSource = recipes;
        }

        private void BtnThird_Click(object sender, RoutedEventArgs e)
        {
            var recipes = MainController.Instance.ThirdPrologQuery(TxtThird.Text);
            ListView.ItemsSource = recipes;
        }

        private void BtnFourth_Click(object sender, RoutedEventArgs e)
        {
            var recipes = MainController.Instance.FourthPrologQuery(TxtFourth.Text);
            ListView.ItemsSource = recipes;
        }

        private void BtnFifth_Click(object sender, RoutedEventArgs e)
        {
            var recipes = MainController.Instance.FifthPrologQuery(TxtFifth.Text);
            ListView.ItemsSource = recipes;
        }

        private void BtnSixth_Click(object sender, RoutedEventArgs e)
        {
            var recipes = MainController.Instance.SixthPrologQuery(TxtSixthIngredient.Text, TxtSixthTool.Text);
            ListView.ItemsSource = recipes;
        }
    }
}
