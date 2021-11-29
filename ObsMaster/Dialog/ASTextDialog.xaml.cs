using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        public int iResult { get; set; } = -1;

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
                    FontTextBlock.FontFamily = new FontFamily(font.Name);
                    FontTextBlock.FontSize = font.Size;
                    bBold = font.Bold;
                    bItalic = font.Italic;
                    bUnderline = font.Underline;
                    bStrikeout = font.Strikeout;
                    if (bBold)
                        FontTextBlock.FontWeight = FontWeights.Bold;
                    if (bItalic)
                        FontTextBlock.FontStyle = FontStyles.Italic;
                    if (bUnderline)
                        FontTextBlock.TextDecorations.Add(TextDecorations.Underline);
                    if (bStrikeout)
                        FontTextBlock.TextDecorations.Add(TextDecorations.Strikethrough);
                    FontStyleCollection = font.Style;
                }
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
                }
            }
            catch { }
        }

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
                }
            }
            catch { }
        }

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
                }
            }
            catch { }
        }

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
                }
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
        private SolidColorBrush WhiteForeground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        private SolidColorBrush BlackForeground = new SolidColorBrush(Color.FromRgb(44, 44, 44));
        private SolidColorBrush GrayForeground = null;
        private SolidColorBrush GetForegroundByBackground(Color back)
        {
            try
            {
                int r = Convert.ToInt32(((Color)back).R);
                int g = Convert.ToInt32(((Color)back).G);
                int b = Convert.ToInt32(((Color)back).B);
                if ((r + g + b) / 3 > 200)
                {
                    return BlackForeground;
                }
                else
                {
                    return WhiteForeground;
                }
            }
            catch { }
            if (GrayForeground == null)
                GrayForeground = new SolidColorBrush(Color.FromRgb(68, 68, 68));
            return GrayForeground;
        }
    }
}
