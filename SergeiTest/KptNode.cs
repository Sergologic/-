using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

// Проект разработан автором Сергей Лысков специально для ООО «ПРОГРАММНЫЙ ЦЕНТР».

namespace SergeiTest.Models
{
    public class KptNode : INotifyPropertyChanged
    {
        private bool _isChecked;

        public string Id { get; set; }

        public XElement XmlElement { get; set; }

        public ObservableCollection<KptNode> Children { get; set; } = new ObservableCollection<KptNode>();


        // Свойство для отметки выбора узла
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged(); // Сообщаем, что свойство изменилось
                }
            }
        }


        // Отображаемое имя — простое ID
        public string DisplayName => Id;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
