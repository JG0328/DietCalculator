using DietCalculator.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private void BtnKeep_Click(object sender, RoutedEventArgs e)
        {
            var weight = float.Parse(TxtKeep.Text);
            var result = MainController.Instance.SecondScheme(weight);
            var list = new List<string>();

            foreach (var receta in MainController.Instance.recetas)
            {
                var calories = receta.GetCalories();
                if (calories >= result - 10.0f || calories <= result + 10.0f)
                    list.Add(receta.nombre);
            }

            ListView.ItemsSource = list.Distinct().ToList();
        }

        private void BtnReduce_Click(object sender, RoutedEventArgs e)
        {
            var weight = float.Parse(TxtReduce.Text);
            var result = MainController.Instance.ThirdScheme(weight);
            var list = new List<string>();

            foreach (var receta in MainController.Instance.recetas)
            {
                var calories = receta.GetCalories();
                if (calories < result)
                    list.Add(receta.nombre);
            }

            ListView.ItemsSource = list.Distinct().ToList();
        }
    }
}
