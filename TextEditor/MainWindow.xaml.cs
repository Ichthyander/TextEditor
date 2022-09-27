using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace TextEditor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FontFamily_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string fontFamily = (sender as ComboBox).SelectedItem as String;
            if (textBox != null)
            {
                textBox.FontFamily = new FontFamily(fontFamily);
            }
        }

        private void FontSize_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double fontSize = Convert.ToDouble((sender as ComboBox).SelectedItem as String);
            if (textBox != null)
            {
                textBox.FontSize = fontSize;
            }
        }

        private void Bold_ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.FontWeight != FontWeights.Bold)
            {
                textBox.FontWeight = FontWeights.Bold;
            }
            else
            {
                textBox.FontWeight = FontWeights.Normal;
            }
        }

        private void Italic_ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.FontStyle != FontStyles.Italic)
            {
                textBox.FontStyle = FontStyles.Italic;
            }
            else
            {
                textBox.FontStyle = FontStyles.Normal;
            }
        }

        private void Underline_ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.TextDecorations.Count == 0)
            {
                textBox.TextDecorations.Add(TextDecorations.Baseline);
            }
            else
            {
                textBox.TextDecorations.Remove(TextDecorations.Baseline[0]);
            }
        }

        private void Black_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (textBox != null)
            {
                textBox.Foreground = Brushes.Black;
            }
        }

        private void Red_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (textBox != null)
            {
                textBox.Foreground = Brushes.Red;
            }
        }

        private void Blue_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (textBox != null)
            {
                textBox.Foreground = Brushes.Blue;
            }
        }

        private void White_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (textBox != null)
            {
                textBox.Foreground = Brushes.White;
            }
        }

        private void Open_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "TextBook files (.txt)|*.txt|All files (.*)|*.*";
            openFileDialog.ShowDialog();

            string path = openFileDialog.FileName;
            if (path != String.Empty)
                textBox.Text = File.ReadAllText(path);
        }

        private void Save_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "TextBook files (.txt)|*.txt";
            saveFileDialog.ShowDialog();

            string path = saveFileDialog.FileName;
            if (path != String.Empty)
                File.WriteAllText(path, textBox.Text);
        }

        private void Close_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Сохранить текст перед выходом из приложения?", Title = "Выход из приложения", MessageBoxButton.YesNo);

            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    Save_MenuItem_Click(sender, e);
                    Application.Current.Shutdown();
                    break;
                case MessageBoxResult.No:
                    Application.Current.Shutdown();
                    break;
            }
        }

        private void themesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            string uriString;
            if (themesComboBox.SelectedIndex == 0)
            {
                uriString = "LightTheme.xaml";
            }
            else if (themesComboBox.SelectedIndex == 1)
            {
                uriString = "DarkTheme.xaml";
            }
            else
            {
                uriString = "ClassicTheme.xaml";
            }

            Uri theme = new Uri(uriString, UriKind.Relative);
            ResourceDictionary themeDictionary = Application.LoadComponent(theme) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Add(themeDictionary);
        }

    }
}
