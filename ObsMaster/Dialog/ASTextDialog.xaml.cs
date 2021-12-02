using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ObsMaster.Dialog
{
    /// <summary>
    /// ASPictureDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ASTextDialog
    {
        private ViewModel.MainVM vm;

        // 字体颜色
        public SolidColorBrush ForegroundColor { get; set; } = new SolidColorBrush(Color.FromRgb(255, 255, 255));

        // 字体渐变颜色
        public SolidColorBrush LinearGradientForegroundColor { get; set; } = new SolidColorBrush(Color.FromRgb(255, 255, 255));

        // 背景颜色
        public SolidColorBrush BackgroundColor { get; set; } = new SolidColorBrush(Color.FromRgb(0, 0, 0));

        // 轮廓颜色
        public SolidColorBrush OutlineColor { get; set; } = new SolidColorBrush(Color.FromRgb(255, 255, 255));

        // 字体名字
        public string sFontFamilyName { get; set; } = "Arial";

        // 字体大小
        public int iFontSize { get; set; } = 16;

        public bool bBold { get; set; } = false;
        public bool bItalic { get; set; } = false;
        public bool bUnderline { get; set; } = false;
        public bool bStrikeout { get; set; } = false;

        public System.Drawing.FontStyle FontStyleCollection { get; set; } = System.Drawing.FontStyle.Regular;

        private Regex reNumber = new Regex("^[\\d]$");

        public int iResult { get; set; } = -1;

        // 预置颜色
        private SolidColorBrush WhiteForeground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        private SolidColorBrush BlackForeground = new SolidColorBrush(Color.FromRgb(44, 44, 44));
        private SolidColorBrush GrayForeground = null;

        public ASTextDialog()
        {
            InitializeComponent();
            vm = ViewModel.MainVM.Instance;
            this.Loaded += ASTextDialog_Loaded;
        }

        private void ASTextDialog_Loaded(object sender, RoutedEventArgs e)
        {
            Grid gr = new Grid();
            gr.Background = new SolidColorBrush(Color.FromRgb(0xe0, 0xe1, 0xea));
            HeaderTitleBar.Children.Add(gr);

            TextBlock tb = new TextBlock();
            tb.Text = "添加文字源";
            tb.Padding = new Thickness(20, 0, 0, 0);
            tb.FontSize = 16;
            tb.VerticalAlignment = VerticalAlignment.Center;
            gr.Children.Add(tb);
        }

        protected override void OnCloseButtonClicked()
        {
            base.OnCloseButtonClicked();
            Close();
        }

        protected override void OnMinimizeButtonClicked()
        {
            WindowState = WindowState.Minimized;
        }

        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            iResult = 0;
            Close();
        }


        #region 字体和字体颜色

        // 选择字体
        private void OnClickSelectFontFamily(object sender, RoutedEventArgs e)
        {
            try
            {
                FontDialog fontDialog = new FontDialog();
                fontDialog.Font = new System.Drawing.Font(sFontFamilyName, iFontSize, FontStyleCollection);
                fontDialog.ShowEffects = true;

                if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var font = fontDialog.Font;
                    sFontFamilyName = font.Name;
                    iFontSize = (int)font.Size;
                    FontTextBlock.Text = font.Name;
                    bBold = font.Bold;
                    bItalic = font.Italic;
                    bUnderline = font.Underline;
                    bStrikeout = font.Strikeout;
                    FontStyleCollection = font.Style;

                    FontTextBlock.FontFamily = new FontFamily(font.Name);
                    FontTextBlock.FontSize = font.Size;
                    PreviewTextBlock.FontFamily = new FontFamily(font.Name);
                    PreviewTextBlock.FontSize = font.Size;
                    FontTextBlock.FontWeight = bBold ? FontWeights.Bold : FontWeights.Normal;
                    FontTextBlock.FontStyle = bItalic ? FontStyles.Italic : FontStyles.Normal;
                    PreviewTextBlock.FontWeight = bBold ? FontWeights.Bold : FontWeights.Normal;
                    PreviewTextBlock.FontStyle = bItalic ? FontStyles.Italic : FontStyles.Normal;

                    FontTextBlock.TextDecorations?.Clear();
                    PreviewTextBlock.TextDecorations?.Clear();
                    if (bUnderline)
                    {
                        FontTextBlock.TextDecorations.Add(TextDecorations.Underline);
                        PreviewTextBlock.TextDecorations.Add(TextDecorations.Underline);
                    }
                    if (bStrikeout)
                    {
                        FontTextBlock.TextDecorations.Add(TextDecorations.Strikethrough);
                        PreviewTextBlock.TextDecorations.Add(TextDecorations.Strikethrough);
                    }
                }
            }
            catch { }
        }

        // 读取文本文件
        private void OnClickReadFromTxt(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder txt = new StringBuilder();
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "文本文件(*.txt)|*.txt|(*.rtf)|*.rtf";
                if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    using (StreamReader sr = new StreamReader(openFile.FileName, Encoding.Default))
                    {
                        while (sr.Peek() > 0)
                        {
                            string temp = sr.ReadLine();
                            if (temp == null)
                                break;
                            txt.Append(temp).Append("\r\n");
                        }
                    }
                }
                DisplayContentTextBox.Text = txt.ToString();
            }
            catch { }
        }

        // 选择字体颜色
        private void OnClickSelectForeground(object sender, RoutedEventArgs e)
        {
            try
            {
                ColorDialog pickColor = new ColorDialog();
                if (pickColor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var background = ConvertColor(pickColor);
                    ForegroundBorder.Background = background;
                    ForegroundTextBlock.Text = background.ToString();
                    ForegroundTextBlock.Foreground = GetForegroundByBackground(background.Color);
                    ForegroundColor = background;

                    if (LinearGradientSwitch?.IsChecked == true)
                    {
                        var fore = GetForegroundWithOpacity();
                        var linear = GetLinearGradientWithOpacity();
                        var dir = GetLinearGradientDirection();
                        PreviewTextBlock.Fill = new LinearGradientBrush(fore.Color, linear.Color, dir);
                    }
                    else
                    {
                        var fore = GetForegroundWithOpacity();
                        PreviewTextBlock.Fill = fore;
                    }
                }
            }
            catch { }
        }

        private void OpacityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (LinearGradientSwitch?.IsChecked == false)
                {
                    var fore = GetForegroundWithOpacity();
                    PreviewTextBlock.Fill = fore;
                }
                else if (LinearGradientSwitch?.IsChecked == true)
                {
                    var fore = GetForegroundWithOpacity();
                    var linear = GetLinearGradientWithOpacity();
                    var dir = GetLinearGradientDirection();
                    PreviewTextBlock.Fill = new LinearGradientBrush(fore.Color, linear.Color, dir);
                }
            }
            catch { }
        }

        private SolidColorBrush GetForegroundWithOpacity()
        {
            try
            {
                if (OpacityTextBox.Text.Length > 3)
                    OpacityTextBox.Text = "100";

                int num = 100;
                if (!int.TryParse(OpacityTextBox.Text, out num))
                {
                    num = 100;
                    OpacityTextBox.Text = "100";
                }
                if (num > 100)
                    num = 100;
                if (num < 0)
                    num = 0;

                var color = ForegroundColor.Color;
                double opacity = num / 100.0;
                color.A = (byte)(opacity >= 1 ? 255 : (int)(opacity * 256));
                var fore = new SolidColorBrush(color);
                return fore;
            }
            catch { }
            return BlackForeground;
        }

        #endregion

        #region 背景颜色

        // 选择背景色
        private void OnClickSelectBackground(object sender, RoutedEventArgs e)
        {
            try
            {
                ColorDialog pickColor = new ColorDialog();
                if (pickColor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var background = ConvertColor(pickColor);
                    BackgroundBorder.Background = background;
                    BackgroundTextBlock.Text = background.ToString();
                    BackgroundTextBlock.Foreground = GetForegroundByBackground(background.Color);
                    BackgroundColor = background;
                    textPreview.Background = BackgroundColor;
                }
            }
            catch { }
        }

        private void BackgroundOpacityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (BackgroundOpacityTextBox.Text.Length > 3)
                    BackgroundOpacityTextBox.Text = "100";

                int num = 100;
                if (!int.TryParse(BackgroundOpacityTextBox.Text, out num))
                {
                    num = 100;
                    BackgroundOpacityTextBox.Text = "100";
                }
                if (num > 100)
                    num = 100;
                if (num < 0)
                    num = 0;

                if (textPreview?.Background != null)
                    textPreview.Background.Opacity = num / 100.0;
            }
            catch { }
        }

        #endregion

        #region 自定义文本区

        private void HandleCustomGridWidthChange()
        {
            try
            {
                if (CustomGridSwitch?.IsChecked == true)
                {
                    int num = 100;
                    if (!int.TryParse(CustomGridWidth.Text, out num))
                    {
                        num = 100;
                        CustomGridWidth.Text = "100";
                    }
                    if (num < 0)
                        num = 0;

                    textPreview.MaxWidth = num;
                }
            }
            catch { }
        }

        private void HandleCustomGridHeightChange()
        {
            try
            {
                if (CustomGridSwitch?.IsChecked == true)
                {
                    int num = 100;
                    if (!int.TryParse(CustomGridHeight.Text, out num))
                    {
                        num = 100;
                        CustomGridHeight.Text = "100";
                    }
                    if (num < 0)
                        num = 0;

                    textPreview.MaxHeight = num;
                }
            }
            catch { }
        }

        private void CustomGridWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            HandleCustomGridWidthChange();
        }

        private void CustomGridHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            HandleCustomGridHeightChange();
        }

        private void CustomGridAutoWrapCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                PreviewTextBlock.TextWrapping = TextWrapping.Wrap;
            }
            catch { }
        }

        private void CustomGridAutoWrapCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                PreviewTextBlock.TextWrapping = TextWrapping.NoWrap;
            }
            catch { }
        }

        private void CustomGridSwitch_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                HandleCustomGridWidthChange();
                HandleCustomGridHeightChange();
            }
            catch { }
        }

        private void CustomGridSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                textPreview.MaxHeight = double.PositiveInfinity;
                textPreview.MaxWidth = double.PositiveInfinity;
                PreviewTextBlock.TextWrapping = TextWrapping.NoWrap;
            }
            catch { }
        }

        #endregion

        #region 渐变

        // 选择渐变颜色
        private void OnClickSelectLinearGradientForeground(object sender, RoutedEventArgs e)
        {
            try
            {
                ColorDialog pickColor = new ColorDialog();
                if (pickColor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var background = ConvertColor(pickColor);
                    LinearGradientForegroundBorder.Background = background;
                    LinearGradientForegroundTextBlock.Text = background.ToString();
                    LinearGradientForegroundTextBlock.Foreground = GetForegroundByBackground(background.Color);
                    LinearGradientForegroundColor = background;

                    UpdateLinearGradient();
                }
            }
            catch { }
        }

        private int GetLinearGradientDirection()
        {
            try
            {
                if (LinearGradientDirectionTextBox.Text.Length > 3)
                    LinearGradientDirectionTextBox.Text = "360";

                int num = 90;
                if (!int.TryParse(LinearGradientDirectionTextBox.Text, out num))
                {
                    num = 90;
                    LinearGradientDirectionTextBox.Text = "90";
                }
                if (num < 0)
                    num = 0;
                if (num > 360)
                    num = 360;

                return num;
            }
            catch { }
            return 90;
        }

        private SolidColorBrush GetLinearGradientWithOpacity()
        {
            try
            {
                if (LinearGradientOpacityTextBox.Text.Length > 3)
                    LinearGradientOpacityTextBox.Text = "100";

                int num = 100;
                if (!int.TryParse(LinearGradientOpacityTextBox.Text, out num))
                {
                    num = 100;
                    LinearGradientOpacityTextBox.Text = "100";
                }
                if (num < 0)
                    num = 0;
                if (num > 100)
                    num = 100;

                var color = LinearGradientForegroundColor.Color;
                double opacity = num / 100.0;
                color.A = (byte)(opacity >= 1 ? 255 : (int)(opacity * 256));
                var fore = new SolidColorBrush(color);
                return fore;
            }
            catch { }
            return WhiteForeground;
        }

        private void LinearGradientSwitch_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateLinearGradient();
            }
            catch { }
        }

        private void LinearGradientSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                PreviewTextBlock.Fill = ForegroundColor;
            }
            catch { }
        }

        private void LinearGradientOpacityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LinearGradientSwitch?.IsChecked == true)
            {
                UpdateLinearGradient();
            }
        }

        private void LinearGradientDirectionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LinearGradientSwitch?.IsChecked == true)
            {
                UpdateLinearGradient();
            }
        }

        private void UpdateLinearGradient()
        {
            try
            {
                if (PreviewTextBlock == null)
                    return;

                var fore = GetForegroundWithOpacity();
                var linear = GetLinearGradientWithOpacity();
                var dir = GetLinearGradientDirection();

                PreviewTextBlock.Fill = new LinearGradientBrush(fore.Color, linear.Color, dir);
            }
            catch { }
        }

        #endregion

        #region 轮廓

        // 选择轮廓颜色
        private void OnClickSelectOutlineColor(object sender, RoutedEventArgs e)
        {
            try
            {
                ColorDialog pickColor = new ColorDialog();
                if (pickColor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var background = ConvertColor(pickColor);
                    OutlineColorBorder.Background = background;
                    OutlineColorTextBlock.Text = background.ToString();
                    OutlineColorTextBlock.Foreground = GetForegroundByBackground(background.Color);
                    OutlineColor = background;

                    UpdateOutline();
                }
            }
            catch { }
        }

        private int GetOutlineStroke()
        {
            try
            {
                if (OutlineSizeTextBox.Text.Length > 2)
                    OutlineSizeTextBox.Text = "20";

                int num = 20;
                if (!int.TryParse(OutlineSizeTextBox.Text, out num))
                {
                    num = 20;
                    OpacityTextBox.Text = "20";
                }
                if (num < 0)
                    num = 0;
                if (num > 20)
                    num = 20;

                return num;
            }
            catch { }
            return 20;
        }

        private SolidColorBrush GetOutlineColorWithOpacity()
        {
            try
            {
                if (OutlineOpacityTextBox.Text.Length > 3)
                    OutlineOpacityTextBox.Text = "100";

                int num = 100;
                if (!int.TryParse(OutlineOpacityTextBox.Text, out num))
                {
                    num = 100;
                    OutlineOpacityTextBox.Text = "100";
                }
                if (num < 0)
                    num = 0;
                if (num > 100)
                    num = 100;

                var color = OutlineColor.Color;
                double opacity = num / 100.0;
                color.A = (byte)(opacity >= 1 ? 255 : (int)(opacity * 256));
                var fore = new SolidColorBrush(color);
                return fore;
            }
            catch { }
            return WhiteForeground;
        }

        private void OutlineSwitch_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateOutline();
            }
            catch { }
        }

        private void OutlineSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                PreviewTextBlock.StrokeThickness = 0;
            }
            catch { }
        }

        private void OutlineSizeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (OutlineSwitch?.IsChecked == true)
            {
                UpdateOutline();
            }
        }

        private void OutlineOpacityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (OutlineSwitch?.IsChecked == true)
            {
                UpdateOutline();
            }
        }

        private void UpdateOutline()
        {
            try
            {
                if (PreviewTextBlock == null)
                    return;

                var size = GetOutlineStroke();
                var color = GetOutlineColorWithOpacity();

                PreviewTextBlock.StrokeThickness = size;
                PreviewTextBlock.Stroke = color;
            }
            catch { }
        }

        #endregion

        #region 垂直排列

        private void VerticalCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                PreviewRotateTransform.Angle = 90;
            }
            catch { }
        }

        private void VerticalCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                PreviewRotateTransform.Angle = 0;
            }
            catch { }
        }

        #endregion

        // 限制输入数字
        private void OnNumPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                e.Handled = !reNumber.IsMatch(e.Text);
            }
            catch { }
        }

        // 从颜色选择窗口返回的颜色转换为SolidColorBrush
        private SolidColorBrush ConvertColor(ColorDialog pickedColor)
        {
            try
            {
                return new SolidColorBrush(Color.FromRgb(
                                           pickedColor.Color.R,
                                           pickedColor.Color.G,
                                           pickedColor.Color.B));
            }
            catch { }
            return new SolidColorBrush(Colors.White);
        }

        // 根据背景色决定前景色文字用黑色还是白色
        private SolidColorBrush GetForegroundByBackground(Color back)
        {
            try
            {
                int r = Convert.ToInt32(((Color)back).R);
                int g = Convert.ToInt32(((Color)back).G);
                int b = Convert.ToInt32(((Color)back).B);

                if ((r + g + b) / 3 > 200)
                    return BlackForeground;
                else
                    return WhiteForeground;
            }
            catch { }

            if (GrayForeground == null)
                GrayForeground = new SolidColorBrush(Color.FromRgb(68, 68, 68));
            return GrayForeground;
        }

    }
}
