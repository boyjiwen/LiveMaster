using System;
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
            Model.MSceneItem data = ((UI.UISceneListboxItem)sender).DataContext as Model.MSceneItem;
            if (data != null)
            {
                int idx = vm.VSceneItems.IndexOf(data);
                if (idx != -1)
                    vm.ObsCore.SelectSceneItem(idx);
            }
        }

        private void UISceneListboxItem_HideButtonClicked(object sender, RoutedEventArgs e)
        {
            var data = ((UI.UISceneListboxItem)sender).DataContext as Model.MSceneItem;
            if (data != null)
            {
                int idx = vm.VSceneItems.IndexOf(data);
                data.Visible = true;
                vm.ObsCore.SetSceneItemVisibile(idx, true);
            }
        }

        private void UISceneListboxItem_DeleteButtonClicked(object sender, RoutedEventArgs e)
        {
            var data = ((UI.UISceneListboxItem)sender).DataContext as Model.MSceneItem;
            if (data != null)
            {
                int idx = vm.VSceneItems.IndexOf(data);
                data.Visible = true;
                vm.ObsCore.RemoveSceneItem(idx);
            }
        }

        private void UISceneListboxItem_ShowButtonClicked(object sender, RoutedEventArgs e)
        {
            var data = ((UI.UISceneListboxItem)sender).DataContext as Model.MSceneItem;
            if (data != null)
            {
                int idx = vm.VSceneItems.IndexOf(data);
                data.Visible = false;
                vm.ObsCore.SetSceneItemVisibile(idx, false);
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
