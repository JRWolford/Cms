using Cms.Desktop.ViewModels;
using MahApps.Metro.Controls;
using System;

namespace Cms.Desktop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BladeEngaged(object sender, EventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;
            viewModel.PublishNavigationBladeEngagementStatus(true);
        }

        private void BladeDisengaged(object sender, EventArgs e)
        {
            var viewModel = DataContext as MainWindowViewModel;
            viewModel.PublishNavigationBladeEngagementStatus(false);
        }
    }
}
