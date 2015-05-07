using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using TerehovNews.Logic;

// Модель данных, определяемая этим файлом, служит типичным примером строго типизированной
// по умолчанию.  Имена свойств совпадают с привязками данных из стандартных шаблонов элементов.
//
// Приложения могут использовать эту модель в качестве начальной точки и добавлять к ней дополнительные элементы или полностью удалить и
// заменить ее другой моделью, соответствующей их потребностям. Использование этой модели позволяет повысить качество приложения 
// скорость реагирования, инициируя задачу загрузки данных в коде программной части для App.xaml, если приложение 
// запускается впервые.

namespace TerehovNews.Data
{
    /// <summary>
    /// Универсальная модель данных элементов.
    /// </summary>
    public class SampleDataItem
    {
        public string UniqueId { get;  set; }
        public string Title { get;  set; }
        public string Subtitle { get; set; }
        public string Description { get;  set; }
        public SampleDataItem(String uniqueId, String title, String description, String subtitle)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Description = description;
            this.Subtitle = subtitle;
        }

        

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Универсальная модель данных групп.
    /// </summary>
    public class SampleDataGroup
    {
        public SampleDataGroup(String uniqueId, String title)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Items = new ObservableCollection<SampleDataItem>();
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public ObservableCollection<SampleDataItem> Items { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Создает коллекцию групп и элементов с содержимым, считываемым из статического JSON-файла.
    /// 
    /// SampleDataSource инициализируется данными, считываемыми из статического JSON-файла, включенного в 
    /// проект.  Предоставляет пример данных как во время разработки, так и во время выполнения.
    /// </summary>
    public sealed class SampleDataSource
    {
        private static SampleDataSource _sampleDataSource = new SampleDataSource();

        private ObservableCollection<SampleDataGroup> _groups = new ObservableCollection<SampleDataGroup>();
        public ObservableCollection<SampleDataGroup> Groups
        {
            get { return this._groups; }
        }

        public static async Task<IEnumerable<SampleDataGroup>> GetGroupsAsync()
        {
            await _sampleDataSource.GetSampleDataAsync();

            return _sampleDataSource.Groups;
        }

        public static async Task<SampleDataGroup> GetGroupAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            var matches = _sampleDataSource.Groups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task<SampleDataItem> GetItemAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Для небольших наборов данных можно использовать простой линейный поиск
            var matches = _sampleDataSource.Groups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        private async Task GetSampleDataAsync()
        {
            if (this._groups.Count != 0)
                return;
            List<SampleDataGroup> ListGroup = new List<SampleDataGroup>
            {
                new SampleDataGroup("Культура", "Культура"),
                new SampleDataGroup("Спорт", "Спорт"),
                new SampleDataGroup("Мир", "Мир"),
                new SampleDataGroup("Россия", "Россия")
            };

            foreach (var groupObject in ListGroup)
            {
                SampleDataGroup group = new SampleDataGroup(groupObject.UniqueId,
                                                            groupObject.Title);

                foreach (var itemValue in RSSParser.GetListNews())
                {
                    
                        group.Items.Add(new SampleDataItem(
                            itemValue.UniqueId,
                            itemValue.Title,
                            itemValue.Description,
                            itemValue.Subtitle));
                }
                this.Groups.Add(group);
            }
        }
    }
}