﻿using System;
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
using System.Windows.Shapes;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using Westwind.Utilities;

namespace MarkdownMonster.Windows.ConfigurationEditor
{
    /// <summary>
    /// Interaction logic for ConfigurationEditorWindow.xaml
    /// </summary>
    public partial class ConfigurationEditorWindow 
    {
        public ConfigurationEditorModel Model { get; set; }

        public StatusBarHelper StatusBar { get; set; }
        
        public ConfigurationEditorWindow()
        {
            InitializeComponent();

            Model = new ConfigurationEditorModel();
            DataContext = Model;
            Model.EditorWindow = this;
            mmApp.SetThemeWindowOverride(this);

            StatusBar = new StatusBarHelper(StatusText, StatusIcon);

            Loaded += ConfigurationEditorWindow_Loaded;

            Model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SearchText" || e.PropertyName == "SectionName")
            {
                RefreshPropertyListAsync();
                PropertiesScrollContainer.ScrollToHome();
            }
        }

        private void RefreshPropertyListAsync()
        {
            var t = Model.AddConfigurationsAsync(PropertiesPanel);
        }

        private void ConfigurationEditorWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshPropertyListAsync();
            TextSearch.Focus();
        }


        private void ButtonManualSettings_Click(object sender, RoutedEventArgs e)
        {
            Model.AppModel.Commands.SettingsCommand.Execute(null);
            Close();
        }


        private void Search_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            RefreshPropertyListAsync();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonHelp_Click(object sender, RoutedEventArgs e)
        {
            ShellUtils.GoUrl("https://markdownmonster.west-wind.com/docs/_4nk01yq6q.htm");
        }
    }
}