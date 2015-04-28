namespace Курсачь
{
    partial class TextForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextForm));
            this.rtbText = new System.Windows.Forms.RichTextBox();
            this.timer100ms = new System.Windows.Forms.Timer(this.components);
            this.lstReplaceMenu = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // rtbText
            // 
            this.rtbText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbText.Location = new System.Drawing.Point(12, 12);
            this.rtbText.Name = "rtbText";
            this.rtbText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbText.Size = new System.Drawing.Size(748, 355);
            this.rtbText.TabIndex = 0;
            this.rtbText.Text = "";
            this.rtbText.Click += new System.EventHandler(this.rtbText_Click);
            this.rtbText.TextChanged += new System.EventHandler(this.rtbText_TextChanged);
            this.rtbText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbText_KeyDown);
            // 
            // timer100ms
            // 
            this.timer100ms.Enabled = true;
            this.timer100ms.Tick += new System.EventHandler(this.timer100ms_Tick);
            // 
            // lstReplaceMenu
            // 
            this.lstReplaceMenu.FormattingEnabled = true;
            this.lstReplaceMenu.Location = new System.Drawing.Point(815, 45);
            this.lstReplaceMenu.Name = "lstReplaceMenu";
            this.lstReplaceMenu.Size = new System.Drawing.Size(120, 134);
            this.lstReplaceMenu.TabIndex = 1;
            this.lstReplaceMenu.Click += new System.EventHandler(this.lstReplaceMenu_Click);
            this.lstReplaceMenu.Leave += new System.EventHandler(this.lstReplaceMenu_Leave);
            // 
            // TextForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 447);
            this.Controls.Add(this.lstReplaceMenu);
            this.Controls.Add(this.rtbText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TextForm";
            this.Text = "Курсачь";
            this.Load += new System.EventHandler(this.TextForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbText;
        private System.Windows.Forms.Timer timer100ms;
        private System.Windows.Forms.ListBox lstReplaceMenu;
    }
}

