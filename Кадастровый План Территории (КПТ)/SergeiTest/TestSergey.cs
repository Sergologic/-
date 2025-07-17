using SergeiTest.Models;
using SergeiTest.ViewModels;
using System.Windows;
using System.Windows.Controls;

// Проект разработан автором Сергей Лысков специально для ООО «ПРОГРАММНЫЙ ЦЕНТР».

namespace SergeiTest
{
    public partial class TestSergey : Window
    {
        public MainViewModel ViewModel { get; }

        public TestSergey()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            DataContext = ViewModel; // Назначаем DataContext для биндингов
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is KptNode node)
            {
                ViewModel.SelectedNode = node; // Обновляем выбранный узел в ViewModel
            }
        }
    }
}
