using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Kpo4162_mnv.Main;
using Kpo4162_nmv.Lib;
using SubdDevelopers.Common;
using SubdDevelopers.source;
using SubdDevelopers.source.Common;

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
                ILoadDevelopersList developersLoader = Ioc.Container.Resolve<ILoadDevelopersList>("prodLoader");
                developersLoader.SetProgressReporter(reportProgress);
                developersLoader.Execute();
                var developers = developersLoader.DeveloperList;

                _developerList = developers;
                dvgMyClasses.ItemsSource = _developerList;
            }
            //обработка исключения "Метод не реализован"
            catch (NotImplementedException ex)
            {
                LogUtility.ErrorLog(ex);
                MessageBox.Show("Ошибка №1: " + ex.Message);
            }
            //обработка остальных исключений
            catch (Exception ex)
            {
                LogUtility.ErrorLog(ex);
                MessageBox.Show("Ошибка №2: " + ex.Message);
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

            ISaveDevelopersList developersSaver = Ioc.Container.Resolve<ISaveDevelopersList>("prodSaver");
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
    }
}
