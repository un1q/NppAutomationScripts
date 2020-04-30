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
    public override void Run()
    {
        string text = Npp.Document.GetSelText();
        if (text == null) {
            return;
        }
        string[] lines = text.Split(new [] { "\r\n" }, StringSplitOptions.None);
        StringBuilder result = new StringBuilder();
        for (int i=lines.Length-1; i>=0; i--) {
            result.Append(lines[i]);
            if (i>0) {
                result.AppendLine();
            }
        }
        Npp.Document.ReplaceSel(result.ToString());
    }
}