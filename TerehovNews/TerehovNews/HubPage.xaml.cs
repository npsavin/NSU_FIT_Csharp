﻿using System;
using Windows.ApplicationModel.Resources;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using TerehovNews.Common;
using TerehovNews.Data;
using TerehovNews.Logic;

//  Документацию по шаблону "Приложение с Hub" см. по адресу http://go.microsoft.com/fwlink/?LinkId=391641

namespace TerehovNews
{
    /// <summary>
    /// Страница, на которой отображается сгруппированная коллекция элементов.
    /// </summary>
    public sealed partial class HubPage : Page
    {
        private readonly NavigationHelper navigationHelper;
        private readonly ObservableDictionary defaultViewModel = new ObservableDictionary();
        private readonly ResourceLoader resourceLoader = ResourceLoader.GetForCurrentView("Resources");

        public HubPage()
        {
            
            RSSParser.UpdateList();
            
            this.InitializeComponent();

            // Элемент управления Hub ("Главный раздел") поддерживается только в книжной ориентации
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Получает объект <see cref="NavigationHelper"/>, связанный с данным объектом <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Получает модель представлений для данного объекта <see cref="Page"/>.
        /// Эту настройку можно изменить на модель строго типизированных представлений.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Заполняет страницу содержимым, передаваемым в процессе навигации.  Также предоставляется любое сохраненное состояние
        /// при повторном создании страницы из предыдущего сеанса.
        /// </summary>
        /// <param name="sender">
        /// Источник события; как правило, <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Данные события, предоставляющие параметр навигации, который передается
        /// <see cref="Frame.Navigate(Type, Object)"/> при первоначальном запросе этой страницы и
        /// словарь состояний, сохраненных этой страницей в ходе предыдущего
        /// сеанса.  Это состояние будет равно NULL при первом посещении страницы.</param>
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            //// TODO: Создание соответствующей модели данных для области проблемы, чтобы заменить пример данных
            var sampleDataGroups = await SampleDataSource.GetGroupsAsync();
            this.DefaultViewModel["Groups"] = sampleDataGroups;
        }

        /// <summary>
        /// Сохраняет состояние, связанное с данной страницей, в случае приостановки приложения или
        /// удаления страницы из кэша навигации.  Значения должны соответствовать требованиям сериализации
        /// <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">Источник события; как правило, <see cref="NavigationHelper"/></param>
        /// <param name="e">Данные события, которые предоставляют пустой словарь для заполнения
        /// сериализуемым состоянием.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            // TODO: Сохраните здесь уникальное состояние страницы.
        }

        /// <summary>
        /// Показывает сведения о группе, которую щелкнул пользователь, в объекте <see cref="SectionPage"/>.
        /// </summary>
        private void GroupSection_ItemClick(object sender, ItemClickEventArgs e)
        {
            var groupId = ((SampleDataGroup)e.ClickedItem).UniqueId;
            if (!Frame.Navigate(typeof(SectionPage), groupId))
            {
                throw new Exception(this.resourceLoader.GetString("NavigationFailedExceptionMessage"));
            }
        }

        #region Регистрация NavigationHelper

        /// <summary>
        /// Методы, предоставленные в этом разделе, используются исключительно для того, чтобы
        /// NavigationHelper для отклика на методы навигации страницы.
        /// <para>
        /// Логика страницы должна быть размещена в обработчиках событий для
        /// <see cref="NavigationHelper.LoadState"/>
        /// и <see cref="NavigationHelper.SaveState"/>.
        /// Параметр навигации доступен в методе LoadState
        /// в дополнение к состоянию страницы, сохраненному в ходе предыдущего сеанса.
        /// </para>
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            RSSParser.UpdateList();
            
            
        }
    }
}
