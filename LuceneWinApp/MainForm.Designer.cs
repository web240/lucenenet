namespace LuceneWinApp
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.rtbMsg = new System.Windows.Forms.RichTextBox();
            this.fbdSelectFile = new System.Windows.Forms.FolderBrowserDialog();
            this.statusBarLucene = new System.Windows.Forms.StatusBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtFileId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDeleteIndex = new System.Windows.Forms.Button();
            this.btnAppendIndex = new System.Windows.Forms.Button();
            this.btnUpdateIndex = new System.Windows.Forms.Button();
            this.btnCreateIndex = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtFileKeyword = new System.Windows.Forms.TextBox();
            this.btnFileIndexSearch = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnMemorySearch = new System.Windows.Forms.Button();
            this.txtMemoryKeyword = new System.Windows.Forms.TextBox();
            this.btnMemoryCreateIndex = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.btnDBSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDBKeyword = new System.Windows.Forms.TextBox();
            this.btnDBCreateIndex = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbMsg
            // 
            this.rtbMsg.Location = new System.Drawing.Point(5, 104);
            this.rtbMsg.Name = "rtbMsg";
            this.rtbMsg.Size = new System.Drawing.Size(723, 319);
            this.rtbMsg.TabIndex = 8;
            this.rtbMsg.Text = "";
            // 
            // statusBarLucene
            // 
            this.statusBarLucene.Location = new System.Drawing.Point(0, 430);
            this.statusBarLucene.Name = "statusBarLucene";
            this.statusBarLucene.Size = new System.Drawing.Size(735, 22);
            this.statusBarLucene.TabIndex = 9;
            this.statusBarLucene.Text = "准备";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(5, -1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(723, 103);
            this.tabControl1.TabIndex = 10;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtFileId);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnDeleteIndex);
            this.tabPage1.Controls.Add(this.btnAppendIndex);
            this.tabPage1.Controls.Add(this.btnUpdateIndex);
            this.tabPage1.Controls.Add(this.btnCreateIndex);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(715, 77);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "索引至文件";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtFileId
            // 
            this.txtFileId.Location = new System.Drawing.Point(371, 25);
            this.txtFileId.Name = "txtFileId";
            this.txtFileId.Size = new System.Drawing.Size(100, 21);
            this.txtFileId.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(252, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "按照文件ID(整数)：";
            // 
            // btnDeleteIndex
            // 
            this.btnDeleteIndex.Location = new System.Drawing.Point(497, 23);
            this.btnDeleteIndex.Name = "btnDeleteIndex";
            this.btnDeleteIndex.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteIndex.TabIndex = 3;
            this.btnDeleteIndex.Text = "删除索引";
            this.btnDeleteIndex.UseVisualStyleBackColor = true;
            this.btnDeleteIndex.Click += new System.EventHandler(this.btnDeleteIndex_Click);
            // 
            // btnAppendIndex
            // 
            this.btnAppendIndex.Location = new System.Drawing.Point(140, 23);
            this.btnAppendIndex.Name = "btnAppendIndex";
            this.btnAppendIndex.Size = new System.Drawing.Size(75, 23);
            this.btnAppendIndex.TabIndex = 2;
            this.btnAppendIndex.Text = "追加索引";
            this.btnAppendIndex.UseVisualStyleBackColor = true;
            this.btnAppendIndex.Click += new System.EventHandler(this.btnAppendIndex_Click);
            // 
            // btnUpdateIndex
            // 
            this.btnUpdateIndex.Location = new System.Drawing.Point(599, 25);
            this.btnUpdateIndex.Name = "btnUpdateIndex";
            this.btnUpdateIndex.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateIndex.TabIndex = 1;
            this.btnUpdateIndex.Text = "更新索引";
            this.btnUpdateIndex.UseVisualStyleBackColor = true;
            this.btnUpdateIndex.Click += new System.EventHandler(this.btnUpdateIndex_Click);
            // 
            // btnCreateIndex
            // 
            this.btnCreateIndex.Location = new System.Drawing.Point(37, 23);
            this.btnCreateIndex.Name = "btnCreateIndex";
            this.btnCreateIndex.Size = new System.Drawing.Size(75, 23);
            this.btnCreateIndex.TabIndex = 0;
            this.btnCreateIndex.Text = "创建索引";
            this.btnCreateIndex.UseVisualStyleBackColor = true;
            this.btnCreateIndex.Click += new System.EventHandler(this.btnCreateIndex_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtFileKeyword);
            this.tabPage2.Controls.Add(this.btnFileIndexSearch);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(715, 77);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "文件索引搜索";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtFileKeyword
            // 
            this.txtFileKeyword.Location = new System.Drawing.Point(171, 28);
            this.txtFileKeyword.Name = "txtFileKeyword";
            this.txtFileKeyword.Size = new System.Drawing.Size(234, 21);
            this.txtFileKeyword.TabIndex = 1;
            // 
            // btnFileIndexSearch
            // 
            this.btnFileIndexSearch.Location = new System.Drawing.Point(446, 28);
            this.btnFileIndexSearch.Name = "btnFileIndexSearch";
            this.btnFileIndexSearch.Size = new System.Drawing.Size(75, 23);
            this.btnFileIndexSearch.TabIndex = 0;
            this.btnFileIndexSearch.Text = "搜索";
            this.btnFileIndexSearch.UseVisualStyleBackColor = true;
            this.btnFileIndexSearch.Click += new System.EventHandler(this.btnFileIndexSearch_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnMemorySearch);
            this.tabPage3.Controls.Add(this.txtMemoryKeyword);
            this.tabPage3.Controls.Add(this.btnMemoryCreateIndex);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(715, 77);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "索引至内存";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnMemorySearch
            // 
            this.btnMemorySearch.Location = new System.Drawing.Point(571, 34);
            this.btnMemorySearch.Name = "btnMemorySearch";
            this.btnMemorySearch.Size = new System.Drawing.Size(75, 23);
            this.btnMemorySearch.TabIndex = 4;
            this.btnMemorySearch.Text = "搜索";
            this.btnMemorySearch.UseVisualStyleBackColor = true;
            this.btnMemorySearch.Click += new System.EventHandler(this.btnMemorySearch_Click);
            // 
            // txtMemoryKeyword
            // 
            this.txtMemoryKeyword.Location = new System.Drawing.Point(320, 34);
            this.txtMemoryKeyword.Name = "txtMemoryKeyword";
            this.txtMemoryKeyword.Size = new System.Drawing.Size(234, 21);
            this.txtMemoryKeyword.TabIndex = 3;
            // 
            // btnMemoryCreateIndex
            // 
            this.btnMemoryCreateIndex.Location = new System.Drawing.Point(62, 32);
            this.btnMemoryCreateIndex.Name = "btnMemoryCreateIndex";
            this.btnMemoryCreateIndex.Size = new System.Drawing.Size(125, 23);
            this.btnMemoryCreateIndex.TabIndex = 0;
            this.btnMemoryCreateIndex.Text = "创建索引至内存";
            this.btnMemoryCreateIndex.UseVisualStyleBackColor = true;
            this.btnMemoryCreateIndex.Click += new System.EventHandler(this.btnMemoryCreateIndex_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.btnDBSearch);
            this.tabPage5.Controls.Add(this.label2);
            this.tabPage5.Controls.Add(this.txtDBKeyword);
            this.tabPage5.Controls.Add(this.btnDBCreateIndex);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(715, 77);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "数据库相关";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // btnDBSearch
            // 
            this.btnDBSearch.Location = new System.Drawing.Point(489, 34);
            this.btnDBSearch.Name = "btnDBSearch";
            this.btnDBSearch.Size = new System.Drawing.Size(75, 23);
            this.btnDBSearch.TabIndex = 3;
            this.btnDBSearch.Text = "搜索";
            this.btnDBSearch.UseVisualStyleBackColor = true;
            this.btnDBSearch.Click += new System.EventHandler(this.btnDBSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(237, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "输入关键字：";
            // 
            // txtDBKeyword
            // 
            this.txtDBKeyword.Location = new System.Drawing.Point(331, 34);
            this.txtDBKeyword.Name = "txtDBKeyword";
            this.txtDBKeyword.Size = new System.Drawing.Size(124, 21);
            this.txtDBKeyword.TabIndex = 1;
            // 
            // btnDBCreateIndex
            // 
            this.btnDBCreateIndex.Location = new System.Drawing.Point(74, 33);
            this.btnDBCreateIndex.Name = "btnDBCreateIndex";
            this.btnDBCreateIndex.Size = new System.Drawing.Size(100, 23);
            this.btnDBCreateIndex.TabIndex = 0;
            this.btnDBCreateIndex.Text = "创建索引";
            this.btnDBCreateIndex.UseVisualStyleBackColor = true;
            this.btnDBCreateIndex.Click += new System.EventHandler(this.btnDBCreateIndex_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(715, 77);
            this.tabPage4.TabIndex = 5;
            this.tabPage4.Text = "打开搜索窗体";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(221, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(233, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "欢迎返回，您大概已经看过搜索窗体了吧！";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(735, 452);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusBarLucene);
            this.Controls.Add(this.rtbMsg);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lucene.Net INDEX";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbMsg;
        private System.Windows.Forms.FolderBrowserDialog fbdSelectFile;
        private System.Windows.Forms.StatusBar statusBarLucene;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDeleteIndex;
        private System.Windows.Forms.Button btnAppendIndex;
        private System.Windows.Forms.Button btnUpdateIndex;
        private System.Windows.Forms.Button btnCreateIndex;
        private System.Windows.Forms.TextBox txtFileKeyword;
        private System.Windows.Forms.Button btnFileIndexSearch;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button btnDBSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDBKeyword;
        private System.Windows.Forms.Button btnDBCreateIndex;
        private System.Windows.Forms.TextBox txtFileId;
        private System.Windows.Forms.Button btnMemoryCreateIndex;
        private System.Windows.Forms.Button btnMemorySearch;
        private System.Windows.Forms.TextBox txtMemoryKeyword;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label3;
    }
}

