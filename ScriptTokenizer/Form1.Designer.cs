namespace ScriptTokenizer
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mainLayout = new DevExpress.XtraLayout.LayoutControl();
            this.cmdNextFragment = new DevExpress.XtraEditors.SimpleButton();
            this.cmdPrevFragment = new DevExpress.XtraEditors.SimpleButton();
            this.cmdProcess = new DevExpress.XtraEditors.SimpleButton();
            this.fragmentTxt = new FastColoredTextBoxNS.FastColoredTextBox();
            this.scriptTxt = new FastColoredTextBoxNS.FastColoredTextBox();
            this.mainLayoutGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.scriptLci = new DevExpress.XtraLayout.LayoutControlItem();
            this.fragmentLci = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.mainLayout)).BeginInit();
            this.mainLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fragmentTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scriptTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainLayoutGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scriptLci)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fragmentLci)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.Controls.Add(this.cmdNextFragment);
            this.mainLayout.Controls.Add(this.cmdPrevFragment);
            this.mainLayout.Controls.Add(this.cmdProcess);
            this.mainLayout.Controls.Add(this.fragmentTxt);
            this.mainLayout.Controls.Add(this.scriptTxt);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.Root = this.mainLayoutGroup;
            this.mainLayout.Size = new System.Drawing.Size(742, 559);
            this.mainLayout.TabIndex = 0;
            this.mainLayout.Text = "mainLayout";
            // 
            // cmdNextFragment
            // 
            this.cmdNextFragment.Location = new System.Drawing.Point(488, 525);
            this.cmdNextFragment.Name = "cmdNextFragment";
            this.cmdNextFragment.Size = new System.Drawing.Size(242, 22);
            this.cmdNextFragment.StyleController = this.mainLayout;
            this.cmdNextFragment.TabIndex = 8;
            this.cmdNextFragment.Text = "Next";
            this.cmdNextFragment.Click += new System.EventHandler(this.cmdNextFragment_Click);
            // 
            // cmdPrevFragment
            // 
            this.cmdPrevFragment.Location = new System.Drawing.Point(259, 525);
            this.cmdPrevFragment.Name = "cmdPrevFragment";
            this.cmdPrevFragment.Size = new System.Drawing.Size(225, 22);
            this.cmdPrevFragment.StyleController = this.mainLayout;
            this.cmdPrevFragment.TabIndex = 7;
            this.cmdPrevFragment.Text = "Prev";
            this.cmdPrevFragment.Click += new System.EventHandler(this.cmdPrevFragment_Click);
            // 
            // cmdProcess
            // 
            this.cmdProcess.Location = new System.Drawing.Point(12, 525);
            this.cmdProcess.Name = "cmdProcess";
            this.cmdProcess.Size = new System.Drawing.Size(243, 22);
            this.cmdProcess.StyleController = this.mainLayout;
            this.cmdProcess.TabIndex = 6;
            this.cmdProcess.Text = "Process";
            this.cmdProcess.Click += new System.EventHandler(this.cmdProcess_Click);
            // 
            // fragmentTxt
            // 
            this.fragmentTxt.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fragmentTxt.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.fragmentTxt.BackBrush = null;
            this.fragmentTxt.CharHeight = 14;
            this.fragmentTxt.CharWidth = 8;
            this.fragmentTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fragmentTxt.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fragmentTxt.IsReplaceMode = false;
            this.fragmentTxt.Location = new System.Drawing.Point(12, 262);
            this.fragmentTxt.Name = "fragmentTxt";
            this.fragmentTxt.Paddings = new System.Windows.Forms.Padding(0);
            this.fragmentTxt.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fragmentTxt.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fragmentTxt.ServiceColors")));
            this.fragmentTxt.Size = new System.Drawing.Size(718, 259);
            this.fragmentTxt.TabIndex = 5;
            this.fragmentTxt.Zoom = 100;
            // 
            // scriptTxt
            // 
            this.scriptTxt.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.scriptTxt.AutoIndentCharsPatterns = "";
            this.scriptTxt.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.scriptTxt.BackBrush = null;
            this.scriptTxt.CharHeight = 14;
            this.scriptTxt.CharWidth = 8;
            this.scriptTxt.CommentPrefix = "--";
            this.scriptTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.scriptTxt.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.scriptTxt.IsReplaceMode = false;
            this.scriptTxt.Language = FastColoredTextBoxNS.Language.SQL;
            this.scriptTxt.LeftBracket = '(';
            this.scriptTxt.Location = new System.Drawing.Point(12, 28);
            this.scriptTxt.Name = "scriptTxt";
            this.scriptTxt.Paddings = new System.Windows.Forms.Padding(0);
            this.scriptTxt.RightBracket = ')';
            this.scriptTxt.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.scriptTxt.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("scriptTxt.ServiceColors")));
            this.scriptTxt.Size = new System.Drawing.Size(718, 214);
            this.scriptTxt.TabIndex = 4;
            this.scriptTxt.Zoom = 100;
            // 
            // mainLayoutGroup
            // 
            this.mainLayoutGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.mainLayoutGroup.GroupBordersVisible = false;
            this.mainLayoutGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.scriptLci,
            this.fragmentLci,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.mainLayoutGroup.Location = new System.Drawing.Point(0, 0);
            this.mainLayoutGroup.Name = "mainLayoutGroup";
            this.mainLayoutGroup.Size = new System.Drawing.Size(742, 559);
            this.mainLayoutGroup.TextVisible = false;
            // 
            // scriptLci
            // 
            this.scriptLci.Control = this.scriptTxt;
            this.scriptLci.Location = new System.Drawing.Point(0, 0);
            this.scriptLci.Name = "scriptLci";
            this.scriptLci.Size = new System.Drawing.Size(722, 234);
            this.scriptLci.Text = "Script";
            this.scriptLci.TextLocation = DevExpress.Utils.Locations.Top;
            this.scriptLci.TextSize = new System.Drawing.Size(46, 13);
            // 
            // fragmentLci
            // 
            this.fragmentLci.Control = this.fragmentTxt;
            this.fragmentLci.Location = new System.Drawing.Point(0, 234);
            this.fragmentLci.Name = "fragmentLci";
            this.fragmentLci.Size = new System.Drawing.Size(722, 279);
            this.fragmentLci.Text = "Fragment";
            this.fragmentLci.TextLocation = DevExpress.Utils.Locations.Top;
            this.fragmentLci.TextSize = new System.Drawing.Size(46, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmdProcess;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 513);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(247, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmdPrevFragment;
            this.layoutControlItem2.Location = new System.Drawing.Point(247, 513);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(229, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cmdNextFragment;
            this.layoutControlItem3.Location = new System.Drawing.Point(476, 513);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(246, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 559);
            this.Controls.Add(this.mainLayout);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.mainLayout)).EndInit();
            this.mainLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fragmentTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scriptTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainLayoutGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scriptLci)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fragmentLci)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl mainLayout;
        private DevExpress.XtraLayout.LayoutControlGroup mainLayoutGroup;
        private FastColoredTextBoxNS.FastColoredTextBox fragmentTxt;
        private FastColoredTextBoxNS.FastColoredTextBox scriptTxt;
        private DevExpress.XtraLayout.LayoutControlItem scriptLci;
        private DevExpress.XtraLayout.LayoutControlItem fragmentLci;
        private DevExpress.XtraEditors.SimpleButton cmdNextFragment;
        private DevExpress.XtraEditors.SimpleButton cmdPrevFragment;
        private DevExpress.XtraEditors.SimpleButton cmdProcess;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
    }
}

