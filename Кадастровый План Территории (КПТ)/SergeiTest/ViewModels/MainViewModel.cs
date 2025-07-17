using Microsoft.Win32;
using SergeiTest.Helpers;
using SergeiTest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

// Проект разработан автором Сергей Лысков специально для ООО «ПРОГРАММНЫЙ ЦЕНТР».

namespace SergeiTest.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // Коллекция узлов дерева для отображения в UI
        public ObservableCollection<KptNode> TreeItems { get; set; }

        // Команды для кнопок в интерфейсе
        public ICommand LoadXmlCommand { get; }
        public ICommand ExportSelectedCommand { get; }
        public ICommand UncheckAllCommand { get; }
        public ICommand HelpCommand { get; }

        private KptNode _selectedNode;
        public KptNode SelectedNode
        {
            get => _selectedNode;
            set
            {
                _selectedNode = value;
                OnPropertyChanged();

                // Обновляем и текст выбранного элемента
                OnPropertyChanged(nameof(SelectedXmlText));
            }
        }


        // Строковое представление XML выбранного узла
        public string SelectedXmlText => SelectedNode?.XmlElement?.ToString();

        public MainViewModel()
        {
            TreeItems = new ObservableCollection<KptNode>();

            // Инициализация команд с методами
            LoadXmlCommand = new RelayCommand(_ => LoadXml());
            ExportSelectedCommand = new RelayCommand(_ => ExportSelected());
            UncheckAllCommand = new RelayCommand(_ => UncheckAll());
            HelpCommand = new RelayCommand(_ => ShowHelp());
        }

        public void LoadXml()
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "XML files (*.xml)|*.xml",
                Title = "Выберите файл XML"
            };

            if (dlg.ShowDialog() != true) return;

            try
            {
                XDocument doc = XDocument.Load(dlg.FileName);
                TreeItems.Clear();


                AddSection("Parcels", doc.Descendants("land_record"), e => e.Element("cad_number")?.Value);


                // Собираем раздел с недвижимостью
                var realtyRoot = new KptNode { Id = "ObjectRealty" };
                AddChildren(realtyRoot, doc.Descendants("build_record"), e => e.Element("cad_number")?.Value);
                AddChildren(realtyRoot, doc.Descendants("construction_record"), e => e.Element("cad_number")?.Value);
                if (realtyRoot.Children.Count > 0)
                    TreeItems.Add(realtyRoot);
                
                // Другие разделы
                AddSection("SpatialData", doc.Descendants("entity_spatial"), e => e.Element("sk_id")?.Value);
                AddSection("Bounds", doc.Descendants("municipal_boundary_record"), e => e.Element("reg_numb_border")?.Value);
                AddSection("Zones", doc.Descendants("zones_and_territories_record"), e => e.Element("reg_numb_border")?.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки XML:\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Добавлякаем раздел с детьми в дерево
        private void AddSection(string sectionName, IEnumerable<XElement> elements, Func<XElement, string> idSelector)
        {
            var section = new KptNode { Id = sectionName };

            foreach (var el in elements)
            {
                string id = idSelector(el);
                if (!string.IsNullOrWhiteSpace(id))
                {
                    section.Children.Add(new KptNode
                    {
                        Id = id,
                        XmlElement = el
                    });
                }
            }

            if (section.Children.Count > 0)
                TreeItems.Add(section);
        }

        //Дочерние элементы к узлу
        private void AddChildren(KptNode parent, IEnumerable<XElement> elements, Func<XElement, string> idSelector)
        {
            foreach (var el in elements)
            {
                string id = idSelector(el);
                if (!string.IsNullOrWhiteSpace(id))
                {
                    parent.Children.Add(new KptNode
                    {
                        Id = id,
                        XmlElement = el
                    });
                }
            }
        }


        // Экспорт отмеченных элементов в файл
        public void ExportSelected()
        {
            List<XElement> selected = GetCheckedElements();
            if (selected.Count == 0)
            {
                MessageBox.Show("Ничего не выбрано для экспорта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "XML files (*.xml)|*.xml",
                FileName = "export.xml"
            };

            if (dlg.ShowDialog() == true)
            {
                XElement root = new XElement("export");
                foreach (XElement el in selected)
                    root.Add(new XElement(el));  // создаем копии элементов

                root.Save(dlg.FileName);
                MessageBox.Show("Экспорт завершён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Вся работа с отметками
        public void UncheckAll()
        {
            foreach (var node in TreeItems)
                UncheckRecursive(node);
        }

        private void UncheckRecursive(KptNode node)
        {
            node.IsChecked = false;
            foreach (var child in node.Children)
                UncheckRecursive(child);
        }

        private List<XElement> GetCheckedElements()
        {
            List<XElement> result = new List<XElement>();
            foreach (var node in TreeItems)
                CollectChecked(node, result);

            return result;
        }

        private void CollectChecked(KptNode node, List<XElement> result)
        {
            if (node.IsChecked && node.XmlElement != null)
                result.Add(node.XmlElement);

            foreach (KptNode child in node.Children)
                CollectChecked(child, result);
        }


        //Справки
        private void ShowHelp()
        {
            MessageBox.Show(
                "Это программа для работы с XML-файлами кадастрового плана территории (КПТ).\n\n" +
                "Что можно делать:\n" +
                "1. Выбрать XML-файл для загрузки.\n" +
                "2. Посмотреть структуру файла в виде дерева с корневыми узлами и их уникальными идентификаторами.\n" +
                "3. Кликнуть на корневой узел, чтобы увидеть все вложенные элементы.\n" +
                "4. Кликнуть на узел с ID, чтобы увидеть его полное содержимое.\n" +
                "5. Отметить нужные узлы и сохранить их в новый XML-файл с вложенной структурой.\n" +
                "6. При необходимости нажать кнопку «Помощь», чтобы увидеть это сообщение.\n\n" +
                "Автор: Сергей Лысков\n" +
                "Дата: 17.07.2025",
                "Справка", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        //Оповещения об изменениях в свойствах!
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}
