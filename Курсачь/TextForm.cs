using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Курсачь
{
    public partial class TextForm : Form
    {
        private int _tick = 0;
        public TextForm()
        {
            InitializeComponent();
            hideReplaceMenu();
            rtbText.WordWrap = true;
            rtbText.AutoWordSelection = true;
        }

        private void timer100ms_Tick(object sender, EventArgs e)
        {
            if (++_tick > 3)
            {
                timer100ms.Enabled = false;

                if (!rtbText.Focused) return;
                int start, end;
                string word = getCurrentWord(out start, out end);
                if (string.IsNullOrEmpty(word)) return;

                fillReplaceMenu(word);
            }
        }
        private void rtbText_TextChanged(object sender, EventArgs e)
        { 
            hideReplaceMenu();
            timer100ms.Enabled = true;
            _tick = 0;
        }

        private void rtbText_KeyDown(object sender, KeyEventArgs e)
        {
            if (!lstReplaceMenu.Visible) { _tick = 0;  return; }

            switch (e.KeyCode)
            {
                case Keys.Up:
                    lstReplaceMenu.SelectedIndex = (lstReplaceMenu.SelectedIndex + lstReplaceMenu.Items.Count - 1) % lstReplaceMenu.Items.Count;
                    e.Handled = true;
                    break;
                case Keys.Down:
                    lstReplaceMenu.SelectedIndex = (lstReplaceMenu.SelectedIndex + 1) % lstReplaceMenu.Items.Count;
                    e.Handled = true;
                    break;
                case Keys.Tab:
                case Keys.Enter:
                    lstReplaceMenu_Click(null, null);
                    e.Handled = true;
                    break;
            }
        }

        private void lstReplaceMenu_Click(object sender, EventArgs e)
        {
            int start, end;
            if(string.IsNullOrEmpty(getCurrentWord(out start, out end))) return;
            rtbText.SelectionStart = start;
            rtbText.SelectionLength = end - start;

            string newText = lstReplaceMenu.Items[lstReplaceMenu.SelectedIndex].ToString();
            Groot.addWord(newText);  // Увеличиваем пользовательский рейтинг слова
            rtbText.SelectedText = newText;
            rtbText.SelectionStart += newText.Length;
            rtbText.SelectionLength = 0;
            rtbText.Focus();

            timer100ms.Enabled = false;
        }

        private void lstReplaceMenu_Leave(object sender, EventArgs e)
        {
            hideReplaceMenu();
        }

        private void rtbText_Click(object sender, EventArgs e)
        {
            hideReplaceMenu();
        }

        void hideReplaceMenu()
        {
            lstReplaceMenu.Visible = false;  
        }

        Point currentMenuPos()
        {
            int indent = 8;
            Point pos;
            Win32.GetCaretPos(out pos);
            pos.Y += (int)rtbText.Font.GetHeight() + indent + rtbText.Location.Y;
            pos.X += indent + rtbText.Location.X;
            return pos;
        }

        string getCurrentWord(out int wordStart, out int wordEnd)
        {
            wordStart = wordEnd = rtbText.SelectionStart - 1;
            int textLenght = rtbText.TextLength;

            int i; //Поиск границ слова
            for (i = wordStart; i >= 0 && i < textLenght && isText(rtbText.Text[i]); i--) wordStart = i;
            for (i = wordEnd; i >= 0 && i < textLenght && isText(rtbText.Text[i]); i++) ; wordEnd = i;
            if (wordStart < 0 || wordEnd == wordStart) return null;

            return rtbText.Text.Substring(wordStart, wordEnd - wordStart);
        }

        bool isText(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || 
                   (c >= 'а' && c <= 'я') || (c >= 'А' && c <= 'Я') ||
                   (c == '\'' || c == 'ё' || c == 'Ё');
        }

        private void fillReplaceMenu(string word)
        {
            if (word.Length < 2) return;
            Groot.searchWord(word, word.Length);

            List<string> wordList = Groot.getFoundWord(8 + word.Length);
            if (!wordList.Any()) return;

            lstReplaceMenu.Items.Clear();
            foreach (string item in wordList)
            {
                lstReplaceMenu.Items.Add(item);
            }

            if (timer100ms.Enabled) return;

            lstReplaceMenu.SelectedIndex = 0;
            lstReplaceMenu.Location = currentMenuPos();
            lstReplaceMenu.Visible = true;
        }

        TreeLayer Groot = new TreeLayer();

        private void TextForm_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            long count = 0;
            stopWatch.Start();
            foreach (string word in File.ReadAllLines("aspell_dump-ru-yo.txt", Encoding.UTF8))
            {
                Groot.addWord(word);
                count++;
            }
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            rtbText.Text = string.Format("Словарь из {0} слов загрузился за {1} ms", count,
                stopWatch.ElapsedMilliseconds);
            rtbText.SelectAll();
        }
       
    }

    public class Win32
    {
        [DllImport("User32.dll")]
        public static extern bool GetCaretPos(out System.Drawing.Point point);
    }
}
