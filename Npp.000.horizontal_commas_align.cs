//css_ref C:\Program Files\Notepad++\plugins\NppScripts\NppScripts.dll
//css_ref C:\Program Files\Notepad++\plugins\NppScripts\NppScripts\NppScripts.asm.dll
using System;
using System.Windows.Forms;
using NppScripts;
using System.Collections.Generic;
using System.Text;

/*
 '//npp_toolbar_image Shell32.dll|3' associates the script with the 16x16 icon #3 from Shell32.dll and places it on the toolbar
 '//npp_shortcut Ctrl+Alt+Shift+F10' associates the script with the shortcut Ctrl+Alt+Shift+F10 (needs N++ restarting)
*/

public class Script : NppScript
{
    private void UpdateLengths(List<int> lengths, string text) {
        string[] fragments = text.Split(new char[] {','});
        for (int i=0; i<fragments.Length; i++) {
            int len = fragments[i].Length;
            if (i >= lengths.Count) {
                lengths.Add(len);
            } else {
                lengths[i] = Math.Max(lengths[i], len);
            }
        }
    }
    
    private List<int> CreateLengthsList(string[] lines) {
        List<int> lengths = new List<int>();
        for (int i=0; i<lines.Length; i++) {
            UpdateLengths(lengths, lines[i]);
        }
        return lengths;
    }
    
    private void CreateResultString(List<int> lengths, string line, StringBuilder result) {
        string[] fragments = line.Split(new char[] {','}, lengths.Count);
        for (int i=0; i<fragments.Length; i++) {
            string fragment = fragments[i];
            result.Append(fragment);
            if (i != fragments.Length-1) {
                result.Append(' ', lengths[i]-fragment.Length);
                result.Append(',');
            }
        }
    }
    
    private void CreateResultString(List<int> lengths, string[] lines, StringBuilder result) {
        for (int i=0; i<lines.Length; i++) {
            if (i != 0) {
                result.AppendLine();
            }
            CreateResultString(lengths, lines[i], result);
        }
    }
    
    public override void Run()
    {
        string text = Npp.Document.GetSelText();
        if (text == null) {
            return;
        }
        string[] lines = text.Split(new [] { "\r\n" }, StringSplitOptions.None);
        List<int> lengths = CreateLengthsList(lines);
        StringBuilder result = new StringBuilder();
        CreateResultString(lengths, lines, result);
        Npp.Document.ReplaceSel(result.ToString());
    }
}