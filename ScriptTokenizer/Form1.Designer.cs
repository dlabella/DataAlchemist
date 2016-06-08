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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.fragmentTxt = new FastColoredTextBoxNS.FastColoredTextBox();
            this.scriptTxt = new FastColoredTextBoxNS.FastColoredTextBox();
            this.cmdProcess = new System.Windows.Forms.Button();
            this.cmdPrev = new System.Windows.Forms.Button();
            this.cmdNext = new System.Windows.Forms.Button();
            this.mainLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fragmentTxt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scriptTxt)).BeginInit();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 3;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.263F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.737F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.mainLayout.Controls.Add(this.fragmentTxt, 0, 1);
            this.mainLayout.Controls.Add(this.scriptTxt, 0, 0);
            this.mainLayout.Controls.Add(this.cmdProcess, 0, 2);
            this.mainLayout.Controls.Add(this.cmdPrev, 1, 2);
            this.mainLayout.Controls.Add(this.cmdNext, 2, 2);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.RowCount = 3;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.mainLayout.Size = new System.Drawing.Size(742, 559);
            this.mainLayout.TabIndex = 0;
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
            this.mainLayout.SetColumnSpan(this.fragmentTxt, 3);
            this.fragmentTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fragmentTxt.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fragmentTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fragmentTxt.IsReplaceMode = false;
            this.fragmentTxt.Location = new System.Drawing.Point(3, 268);
            this.fragmentTxt.Name = "fragmentTxt";
            this.fragmentTxt.Paddings = new System.Windows.Forms.Padding(0);
            this.fragmentTxt.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fragmentTxt.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fragmentTxt.ServiceColors")));
            this.fragmentTxt.Size = new System.Drawing.Size(736, 259);
            this.fragmentTxt.TabIndex = 7;
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
            this.mainLayout.SetColumnSpan(this.scriptTxt, 3);
            this.scriptTxt.CommentPrefix = "--";
            this.scriptTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.scriptTxt.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.scriptTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scriptTxt.IsReplaceMode = false;
            this.scriptTxt.Language = FastColoredTextBoxNS.Language.SQL;
            this.scriptTxt.LeftBracket = '(';
            this.scriptTxt.Location = new System.Drawing.Point(3, 3);
            this.scriptTxt.Name = "scriptTxt";
            this.scriptTxt.Paddings = new System.Windows.Forms.Padding(0);
            this.scriptTxt.RightBracket = ')';
            this.scriptTxt.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.scriptTxt.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("scriptTxt.ServiceColors")));
            this.scriptTxt.Size = new System.Drawing.Size(736, 259);
            this.scriptTxt.TabIndex = 6;
            this.scriptTxt.Zoom = 100;
            // 
            // cmdProcess
            // 
            this.cmdProcess.Location = new System.Drawing.Point(3, 533);
            this.cmdProcess.Name = "cmdProcess";
            this.cmdProcess.Size = new System.Drawing.Size(75, 23);
            this.cmdProcess.TabIndex = 8;
            this.cmdProcess.Text = "Process";
            this.cmdProcess.UseVisualStyleBackColor = true;
            this.cmdProcess.Click += new System.EventHandler(this.cmdProcess_Click);
            // 
            // cmdPrev
            // 
            this.cmdPrev.Location = new System.Drawing.Point(508, 533);
            this.cmdPrev.Name = "cmdPrev";
            this.cmdPrev.Size = new System.Drawing.Size(99, 23);
            this.cmdPrev.TabIndex = 9;
            this.cmdPrev.Text = "Prev";
            this.cmdPrev.UseVisualStyleBackColor = true;
            this.cmdPrev.Click += new System.EventHandler(this.cmdPrevFragment_Click);
            // 
            // cmdNext
            // 
            this.cmdNext.Location = new System.Drawing.Point(616, 533);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Size = new System.Drawing.Size(85, 23);
            this.cmdNext.TabIndex = 10;
            this.cmdNext.Text = "Next";
            this.cmdNext.UseVisualStyleBackColor = true;
            this.cmdNext.Click += new System.EventHandler(this.cmdNextFragment_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 559);
            this.Controls.Add(this.mainLayout);
            this.Name = "Form1";
            this.Text = "Form1";
            this.mainLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fragmentTxt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scriptTxt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private FastColoredTextBoxNS.FastColoredTextBox fragmentTxt;
        private FastColoredTextBoxNS.FastColoredTextBox scriptTxt;
        private System.Windows.Forms.Button cmdProcess;
        private System.Windows.Forms.Button cmdPrev;
        private System.Windows.Forms.Button cmdNext;
    }
}

