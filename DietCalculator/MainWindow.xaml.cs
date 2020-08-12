using DietCalculator.Logic;
using DietCalculator.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Xml;
using System.Xml.Schema;

namespace DietCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnOpenXml_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos XML (*.xml)|*.xml"
            };

            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                try
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.DtdProcessing = DtdProcessing.Parse;
                    settings.ValidationType = ValidationType.DTD;
                    settings.IgnoreWhitespace = true;
                    settings.XmlResolver = new XmlUrlResolver();
                    settings.ValidationEventHandler += new ValidationEventHandler(XmlValidationCallBack);

                    // Create the XmlReader object.
                    XmlReader reader = XmlReader.Create(openFileDialog.FileName, settings);

                    // Parse the file.
                    while (reader.Read()) ;

                    MainController.Instance.GetXmlData(openFileDialog.FileName);
                    BtnContinue.IsEnabled = true;

                    LabelXml.Content = openFileDialog.SafeFileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }
            }
        }

        private void BtnContinue_Click(object sender, RoutedEventArgs e)
        {
            BtnContinue.IsEnabled = false;
            BtnOpenXml.IsEnabled = false;

            MainController.Instance.IsValid = true;

            if (MainController.Instance.IsValid)
            {
                try
                {
                    var infoWindow = new InfoWindow();
                    infoWindow.Show();
                    Close();
                }
                catch
                {
                    MessageBox.Show("Error al obtener los datos del archivo XML");
                    BtnContinue.IsEnabled = true;
                    BtnOpenXml.IsEnabled = true;
                }
            }
        }

        private void XmlValidationCallBack(object sender, ValidationEventArgs e)
        {
            BtnContinue.IsEnabled = false;
            BtnOpenXml.IsEnabled = true;

            MainController.Instance.IsValid = false;

            throw new Exception("XML is not valid by DTD");
        }
    }
}
