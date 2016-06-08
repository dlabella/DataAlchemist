using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScriptTokenizer
{
    public partial class Form1 : Form
    {
        int _fragmentIdx;
        List<Sql.SqlFragment> _fragments = new List<Sql.SqlFragment>();
        public Form1()
        {
            InitializeComponent();
        }

        private void cmdProcess_Click(object sender, EventArgs e)
        {
            var tokenizer = new Sql.ScriptTokenizer();
            _fragments = tokenizer.TokenizeScript(scriptTxt.Text);
            _fragmentIdx = 0;
            ShowCurrentFragment();

        }

        private void ShowCurrentFragment()
        {
            var fragment = _fragments[_fragmentIdx];
            scriptTxt.SelectionStart = fragment.StartIndex;
            scriptTxt.SelectionLength = fragment.EndIndex - fragment.StartIndex;
            fragmentTxt.Text = fragment.Sql;
            scriptTxt.Focus();
            scriptTxt.DoCaretVisible();
        }

        private void cmdPrevFragment_Click(object sender, EventArgs e)
        {
            if (_fragmentIdx > 0)
            {
                _fragmentIdx--;
                ShowCurrentFragment();
            }
        }

        private void cmdNextFragment_Click(object sender, EventArgs e)
        {
            if (_fragmentIdx < _fragments.Count)
            {
                _fragmentIdx++;
                ShowCurrentFragment();
            }
        }
    }
}
