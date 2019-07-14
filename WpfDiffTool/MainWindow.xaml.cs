using DiffMatchPatch;
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

namespace WpfDiffTool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtDiff1_Click(object sender, RoutedEventArgs e)
        {
            diff_match_patch diffHelper = new diff_match_patch();
            List<Diff> diffs = null;

            string target_1 = "This is some thing good," + Environment.NewLine + "Do you like it?";
            string target_2 = "This is something nice," + Environment.NewLine + "Do you want it?";

            diffs = diffHelper.diff_main(target_1, target_2);
            diffHelper.diff_cleanupSemanticLossless(diffs);

            Paragraph ph = new Paragraph();
            Run run = null; ;
            foreach (var item in diffs)
            {
                run = new Run();
                run.Text = item.text;
                run.FontSize = 16.0;
                if (item.operation == Operation.DELETE)
                {
                    run.Foreground = new SolidColorBrush(Colors.Red);
                    TextDecorationCollection tdc_clone = new TextDecorationCollection();
                    tdc_clone.Add(TextDecorations.Strikethrough);
                    run.TextDecorations = tdc_clone;
                }
                else if (item.operation == Operation.EQUAL)
                    run.Foreground = new SolidColorBrush(Colors.Black);
                else if (item.operation == Operation.INSERT)
                    run.Foreground = new SolidColorBrush(Colors.Green);
                ph.Inlines.Add(run);
            }
            rtbResult.Document.Blocks.Add(ph);
        }

        private void BtDiff2_Click(object sender, RoutedEventArgs e)
        {
            string target_1 = "This is some thing good," + Environment.NewLine + "Do you like it?" +
                Environment.NewLine + "I am not sure abou it." + Environment.NewLine +"Thank you.";
            string target_2 = "This is something nice," + Environment.NewLine + "Do you want it?" +
                Environment.NewLine + "Thank you.";
            DiffHelper dHelper = new DiffHelper();
            dHelper.ShowDiffInRichTextBox(target_1, target_2, rtbResult);
        }
    }
}
