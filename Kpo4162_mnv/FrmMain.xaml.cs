using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;
using Kpo4162_mnv.Main;
using Kpo4162_nmv.Lib;
using Kpo4162_nmv.Lib.source.Log;
using SubdDevelopers.Common;
using SubdDevelopers.source;
using SubdDevelopers.source.Common;
using SubdDevelopers.source.Exceptions;

namespace Kpo4162_mnv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Developer> _developerList = null;

        public MainWindow()
        {
            InitializeComponent();
            AppGlobalSettings.Initialize();
        }

        private void MnExit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MnOpen_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ILoadDevelopersList developersLoader = IoС.Container.Resolve<ILoadDevelopersList>("prodLoader");
                developersLoader.SetProgressReporter(reportProgress);
                developersLoader.Execute();
                var developers = developersLoader.DeveloperList;

                _developerList = developers;
                dvgMyClasses.ItemsSource = _developerList;
            }
            // обработка исключения "Метод не реализован"
            catch (NotImplementedException exception)
            {
                LogUtility.ErrorLog(exception);
                MessageBox.Show("Ошибка №1: " + exception.Message);
            }
            // обработка исключения с пустым файлом
            catch (EmptyFileException exception)
            {
                ErrorLogger.LogError(exception.Message);
                MessageBox.Show("Ошибка №3: " + exception.Message);
            }
            // обработка остальных исключений
            catch (Exception exception)
            {
                LogUtility.ErrorLog(exception);
                MessageBox.Show("Ошибка №2: " + exception.Message);
            }
        }

        private void MnOpenDeveloper_OnClick(object sender, RoutedEventArgs e)
        {
            if (dvgMyClasses.SelectedItem == null)
                return;

            var developerWindow = new FrmDevelopers();

            var currentDeveloper = dvgMyClasses.SelectedItem as Developer;
            developerWindow.SetDeveloper(currentDeveloper);

            developerWindow.ShowDialog();
        }

        private void MnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (dvgMyClasses.Items.Count <= 0)
                return;

            var developerList = 
                (from object item 
                    in dvgMyClasses.Items
                    where item is Developer
                    select item as Developer)
                .ToList();

            ISaveDevelopersList developersSaver = IoС.Container.Resolve<ISaveDevelopersList>("prodSaver");
            developersSaver.DeveloperList = developerList;
            developersSaver.SetProgressReporter(reportProgress);
            developersSaver.Execute();
        }

        /// <summary>
        /// Изменение значения в ProgressBar
        /// </summary>
        /// <param name="progressValue"></param>
        private void reportProgress(int progressValue)
        {
            ProcessProgress.Value = progressValue;
        }

        /// <summary>
        /// Обработчик "Новое чтение - Обычное"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnNewReadSimple_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ILoadDevelopersList developersLoader = IoС.Container.Resolve<ILoadDevelopersList>("prodLoaderNew");
                developersLoader.SetProgressReporter(reportProgress);
                developersLoader.Execute();
                var developers = developersLoader.DeveloperList;

                _developerList = developers;
                dvgMyClasses.ItemsSource = _developerList;
            }
            // обработка исключения если файл не найден
            catch (FileNotFoundException exception)
            {
                ErrorLogger.LogError(exception.Message);
                MessageBox.Show("Ошибка №4: " + exception.Message);
            }
            // обработка исключения связанного с XML-файлом
            catch (XmlException exception)
            {
                ErrorLogger.LogError(exception.Message);
                MessageBox.Show("Ошибка №5: " + exception.Message);
            }
            // обработка остальных исключений
            catch (Exception exception)
            {
                LogUtility.ErrorLog(exception);
                MessageBox.Show("Ошибка №2: " + exception.Message);
            }
        }

        /// <summary>
        /// Обработчик "Новое чтение - Модифицированное"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnNewReadModified_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ILoadDevelopersList developersLoader = IoС.Container.Resolve<ILoadDevelopersList>("prodLoaderModified");
                developersLoader.SetProgressReporter(reportProgress);
                developersLoader.Execute();
                var developers = developersLoader.DeveloperList;

                _developerList = developers;
                dvgMyClasses.ItemsSource = _developerList;
            }
            // обработка исключения если файл не найден
            catch (FileNotFoundException exception)
            {
                ErrorLogger.LogError(exception.Message);
                MessageBox.Show("Ошибка №4: " + exception.Message);
            }
            // обработка исключения связанного с XML-файлом
            catch (XmlException exception)
            {
                ErrorLogger.LogError(exception.Message);
                MessageBox.Show("Ошибка №5: " + exception.Message);
            }
            // обработка остальных исключений
            catch (Exception exception)
            {
                LogUtility.ErrorLog(exception);
                MessageBox.Show("Ошибка №2: " + exception.Message);
            }
        }
    }
}
