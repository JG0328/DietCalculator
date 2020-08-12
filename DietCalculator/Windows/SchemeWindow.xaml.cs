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
    /// Lógica de interacción para SchemeWindow.xaml
    /// </summary>
    public partial class SchemeWindow : Window
    {
        public SchemeWindow()
        {
            InitializeComponent();
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            var weigth = float.Parse(TxtWeight.Text);
            var height = float.Parse(TxtHeight.Text);
            var age = int.Parse(TxtAge.Text);
            MessageBox.Show(CbxGender.SelectedItem.ToString());
            var item = (ComboBoxItem)CbxGender.SelectedItem;
            var gender = item.Content.ToString() == "Masculino" ? 1 : 0;

            var result = MainController.Instance.FirstScheme(weigth, height, age, gender);
            LblResult.Content = result.ToString("0.00");
        }
    }
}
