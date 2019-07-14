using DiffMatchPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfDiffTool
{
    public class DiffHelper
    {
        public void ShowDiffInRichTextBox(string originalString, string targetString, RichTextBox rtbResult)
        {
            diff_match_patch diffHelper = new diff_match_patch();
            List<Diff> diffs = null;

            //string target_1 = "This is some thing good," + Environment.NewLine + "Do you like it?";
            //string target_2 = "This is something nice," + Environment.NewLine + "Do you want it?";

            diffs = diffHelper.diff_main(originalString, targetString);
            //diffHelper.diff_cleanupSemanticLossless(diffs);
            diffHelper.diff_cleanupSemantic(diffs);
            // diffHelper.diff_cleanupMerge(diffs);

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
    }
}
