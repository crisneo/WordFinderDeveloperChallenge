using System;
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
using WordFinder.Logic;

namespace WordFinder.WPFUI
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMatrix.Text) || string.IsNullOrEmpty(txtWordStream.Text))
            {
                MessageBox.Show("Please specify the character matrix and the wordstream","Empty Data");
                return;
            }
            var matrix = txtMatrix.Text.Split(',').ToList().Select(x => x.Trim());
            var wordStream = txtWordStream.Text.Split(",").ToList().Select(x => x.Trim()); ;
            IWordFinder wFinder = new WordFinder.Logic.WordFinder(matrix);
            try
            {   
                var res = wFinder.Find(wordStream);
                var results = new StringBuilder();
                res.ToList().ForEach(x => results.Append(x+Environment.NewLine));
                MessageBox.Show(string.IsNullOrEmpty(results.ToString()) ? "no results" : results.ToString(), "Words found");
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Invalid arguments");
            }
        }
    }
}
