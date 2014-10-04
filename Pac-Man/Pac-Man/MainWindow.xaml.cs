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

namespace Pac_Man
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


        private void Start_game(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Извини, я не успел написать игру =(");
        }

        private void Best_score(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Никто не получил ни одного очка");
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Выключите, пожалуйста, антивирус");
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Извините, кнопка не работает");
            Close();
            MessageBox.Show("ХАХА НАЕБАЛ ЛООООЛ");
        }
    }
}
