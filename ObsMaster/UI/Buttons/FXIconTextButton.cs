using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HuyaFX.UI.Controls.Buttons
{
    /// <summary>
    /// 图标在文字下层的按钮
    /// </summary>
    public class FXIconTextButton : FXIconTextButtonBase
    {
        static FXIconTextButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FXIconTextButton), new FrameworkPropertyMetadata(typeof(FXIconTextButton)));
        }

    }
}
