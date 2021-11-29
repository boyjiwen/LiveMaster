using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ObsMaster
{
    /// <summary>
    /// ToolView.xaml 的交互逻辑
    /// </summary>
    public partial class PluginsView : UserControl
    {
        private ViewModel.MainVM vm;
        public PluginsView()
        {
            InitializeComponent();
            vm = ViewModel.MainVM.Instance;
        }

        private void FXLIconButton_Click(object sender, RoutedEventArgs e)
        {
            //((HuyaFX.UI.Controls.Buttons.FXLIconButton)sender).IsChecked = !((HuyaFX.UI.Controls.Buttons.FXLIconButton)sender).IsChecked;
            ((HuyaFX.UI.Controls.Buttons.FXLIconButton)sender).IsChecked = true;
        }

        private void OnSceneToolClicked(object sender, RoutedEventArgs e)
        {
            ToolPopup.IsOpen = true;
        }

        private void HideToolPopupClicked(object sender, RoutedEventArgs e)
        {
            ToolPopup.IsOpen = false;
        }

        private void UISceneListboxItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.MSceneItem data = ((UI.UISceneListboxItem)sender).DataContext as Model.MSceneItem;
                if (data != null)
                {
                    int idx = vm.VSceneItems.IndexOf(data);
                    if (idx != -1)
                        vm.ObsCore.SelectSceneItem(idx);
                }
            }
            catch { }
        }

        private void UISceneListboxItem_HideButtonClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = ((Control)sender).DataContext as Model.MSceneItem;
                if (data != null)
                {
                    int idx = vm.VSceneItems.IndexOf(data);
                    data.Visible = true;
                    vm.ObsCore.SetSceneItemVisibile(idx, true);
                }
            }
			catch { }
        }

        private void UISceneListboxItem_DeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = ((Control)sender).DataContext as Model.MSceneItem;
                if (data != null)
                {
                    int idx = vm.VSceneItems.IndexOf(data);
                    data.Visible = true;
                    vm.ObsCore.RemoveSceneItem(idx);
                }
            }
			catch { }
        }

        private void UISceneListboxItem_ShowButtonClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = ((Control)sender).DataContext as Model.MSceneItem;
                if (data != null)
                {
                    int idx = vm.VSceneItems.IndexOf(data);
                    data.Visible = false;
                    vm.ObsCore.SetSceneItemVisibile(idx, false);
                }
            }
            catch { }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

		private void ScrollViewer_RightClicked(object sender, MouseButtonEventArgs e)
		{
			try
			{
                if (sender is UI.UISceneListboxItem listItem)
                {
                    listItem.IsChecked = true;
                    var data = listItem.DataContext as Model.MSceneItem;
                    this.MenuItemShow.Visibility = data.Visible ? Visibility.Collapsed : Visibility.Visible;
                    this.MenuItemHide.Visibility = data.Visible ? Visibility.Visible : Visibility.Collapsed;
                    this.contextMenu.DataContext = data;
                    this.contextMenu.IsOpen = true;
                }
            }
			catch { }
		}

		private void UISceneListboxItem_ItemUpClicked(object sender, RoutedEventArgs e)
		{
			try
			{
                vm.SceneItemUpCmd.Execute("");
			}
			catch { }
		}

		private void UISceneListboxItem_ItemDownClicked(object sender, RoutedEventArgs e)
		{
			try
			{
                vm.SceneItemDownCmd.Execute("");
            }
			catch { }
		}

		private void UISceneListboxItem_ItemMoveTopClicked(object sender, RoutedEventArgs e)
		{
            try
            {
				var data = ((Control)sender).DataContext as Model.MSceneItem;
				if (data != null)
				{
					int idx = vm.VSceneItems.IndexOf(data);
					vm.ObsCore.MoveToTop(idx);
				}
			}
            catch { }
		}

		private void UISceneListboxItem_ItemMoveBottomTopClicked(object sender, RoutedEventArgs e)
		{
            try
            {
				var data = ((Control)sender).DataContext as Model.MSceneItem;
				if (data != null)
				{
					int idx = vm.VSceneItems.IndexOf(data);
					vm.ObsCore.MoveToBottom(idx);
				}
			}
            catch { }
		}

		private void UISceneListboxItem_FitToScreenClicked(object sender, RoutedEventArgs e)
		{
			try
			{
				vm.ObsCore.FitToScreen();
			}
			catch { }
		}
	}
}
