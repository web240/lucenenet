namespace LuceneWinApp
{
    partial class SearchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.rtbMsg = new System.Windows.Forms.RichTextBox();
            this.statusBarLucene = new System.Windows.Forms.StatusBar();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.btnTermQuery = new System.Windows.Forms.Button();
            this.panelPager = new System.Windows.Forms.Panel();
            this.btnRangeQuery = new System.Windows.Forms.Button();
            this.btnBooleanQuery = new System.Windows.Forms.Button();
            this.btnPhraseQuery = new System.Windows.Forms.Button();
            this.btnPrefixQuery = new System.Windows.Forms.Button();
            this.btnWildcardQuery = new System.Windows.Forms.Button();
            this.btnFuzzyQuery = new System.Windows.Forms.Button();
            this.btnSortSearch = new System.Windows.Forms.Button();
            this.chkIsSortById = new System.Windows.Forms.CheckBox();
            this.txtSortKeyword = new System.Windows.Forms.TextBox();
            this.txtPagerKeyword = new System.Windows.Forms.TextBox();
            this.btnPagerSearch = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txtMultiSearchKeyword = new System.Windows.Forms.TextBox();
            this.btnMultSearch = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.btnMultFields = new System.Windows.Forms.Button();
            this.txtMultFields = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(8, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(723, 161);
            this.tabControl1.TabIndex = 11;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // rtbMsg
            // 
            this.rtbMsg.Location = new System.Drawing.Point(8, 175);
            this.rtbMsg.Name = "rtbMsg";
            this.rtbMsg.Size = new System.Drawing.Size(723, 249);
            this.rtbMsg.TabIndex = 12;
            this.rtbMsg.Text = "";
            // 
            // statusBarLucene
            // 
            this.statusBarLucene.Location = new System.Drawing.Point(0, 430);
            this.statusBarLucene.Name = "statusBarLucene";
            this.statusBarLucene.Size = new System.Drawing.Size(735, 22);
            this.statusBarLucene.TabIndex = 13;
            this.statusBarLucene.Text = "准备";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnFuzzyQuery);
            this.tabPage1.Controls.Add(this.btnWildcardQuery);
            this.tabPage1.Controls.Add(this.btnPrefixQuery);
            this.tabPage1.Controls.Add(this.btnPhraseQuery);
            this.tabPage1.Controls.Add(this.btnBooleanQuery);
            this.tabPage1.Controls.Add(this.btnRangeQuery);
            this.tabPage1.Controls.Add(this.btnTermQuery);
            this.tabPage1.Controls.Add(this.txtKeyword);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(715, 135);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "单索引文件搜索";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtSortKeyword);
            this.tabPage2.Controls.Add(this.chkIsSortById);
            this.tabPage2.Controls.Add(this.btnSortSearch);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(715, 135);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "排序";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnPagerSearch);
            this.tabPage3.Controls.Add(this.txtPagerKeyword);
            this.tabPage3.Controls.Add(this.panelPager);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(715, 135);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "分页";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtKeyword
            // 
            this.txtKeyword.Location = new System.Drawing.Point(38, 62);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(150, 21);
            this.txtKeyword.TabIndex = 0;
            // 
            // btnTermQuery
            // 
            this.btnTermQuery.Location = new System.Drawing.Point(243, 18);
            this.btnTermQuery.Name = "btnTermQuery";
            this.btnTermQuery.Size = new System.Drawing.Size(100, 23);
            this.btnTermQuery.TabIndex = 1;
            this.btnTermQuery.Text = "TermQuery";
            this.btnTermQuery.UseVisualStyleBackColor = true;
            this.btnTermQuery.Click += new System.EventHandler(this.btnTermQuery_Click);
            // 
            // panelPager
            // 
            this.panelPager.Location = new System.Drawing.Point(34, 90);
            this.panelPager.Name = "panelPager";
            this.panelPager.Size = new System.Drawing.Size(651, 42);
            this.panelPager.TabIndex = 1;
            this.panelPager.Visible = false;
            // 
            // btnRangeQuery
            // 
            this.btnRangeQuery.Location = new System.Drawing.Point(243, 62);
            this.btnRangeQuery.Name = "btnRangeQuery";
            this.btnRangeQuery.Size = new System.Drawing.Size(100, 23);
            this.btnRangeQuery.TabIndex = 2;
            this.btnRangeQuery.Text = "RangeQuery";
            this.btnRangeQuery.UseVisualStyleBackColor = true;
            this.btnRangeQuery.Click += new System.EventHandler(this.btnRangeQuery_Click);
            // 
            // btnBooleanQuery
            // 
            this.btnBooleanQuery.Location = new System.Drawing.Point(243, 106);
            this.btnBooleanQuery.Name = "btnBooleanQuery";
            this.btnBooleanQuery.Size = new System.Drawing.Size(100, 23);
            this.btnBooleanQuery.TabIndex = 3;
            this.btnBooleanQuery.Text = "BooleanQuery";
            this.btnBooleanQuery.UseVisualStyleBackColor = true;
            this.btnBooleanQuery.Click += new System.EventHandler(this.btnBooleanQuery_Click);
            // 
            // btnPhraseQuery
            // 
            this.btnPhraseQuery.Location = new System.Drawing.Point(409, 60);
            this.btnPhraseQuery.Name = "btnPhraseQuery";
            this.btnPhraseQuery.Size = new System.Drawing.Size(100, 23);
            this.btnPhraseQuery.TabIndex = 4;
            this.btnPhraseQuery.Text = "PhraseQuery";
            this.btnPhraseQuery.UseVisualStyleBackColor = true;
            this.btnPhraseQuery.Click += new System.EventHandler(this.btnPhraseQuery_Click);
            // 
            // btnPrefixQuery
            // 
            this.btnPrefixQuery.Location = new System.Drawing.Point(569, 18);
            this.btnPrefixQuery.Name = "btnPrefixQuery";
            this.btnPrefixQuery.Size = new System.Drawing.Size(100, 23);
            this.btnPrefixQuery.TabIndex = 5;
            this.btnPrefixQuery.Text = "PrefixQuery";
            this.btnPrefixQuery.UseVisualStyleBackColor = true;
            this.btnPrefixQuery.Click += new System.EventHandler(this.btnPrefixQuery_Click);
            // 
            // btnWildcardQuery
            // 
            this.btnWildcardQuery.Location = new System.Drawing.Point(569, 106);
            this.btnWildcardQuery.Name = "btnWildcardQuery";
            this.btnWildcardQuery.Size = new System.Drawing.Size(100, 23);
            this.btnWildcardQuery.TabIndex = 6;
            this.btnWildcardQuery.Text = "WildcardQuery";
            this.btnWildcardQuery.UseVisualStyleBackColor = true;
            this.btnWildcardQuery.Click += new System.EventHandler(this.btnWildcardQuery_Click);
            // 
            // btnFuzzyQuery
            // 
            this.btnFuzzyQuery.Location = new System.Drawing.Point(569, 60);
            this.btnFuzzyQuery.Name = "btnFuzzyQuery";
            this.btnFuzzyQuery.Size = new System.Drawing.Size(100, 23);
            this.btnFuzzyQuery.TabIndex = 7;
            this.btnFuzzyQuery.Text = "FuzzyQuery";
            this.btnFuzzyQuery.UseVisualStyleBackColor = true;
            this.btnFuzzyQuery.Click += new System.EventHandler(this.btnFuzzyQuery_Click);
            // 
            // btnSortSearch
            // 
            this.btnSortSearch.Location = new System.Drawing.Point(488, 56);
            this.btnSortSearch.Name = "btnSortSearch";
            this.btnSortSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSortSearch.TabIndex = 0;
            this.btnSortSearch.Text = "排序搜索";
            this.btnSortSearch.UseVisualStyleBackColor = true;
            this.btnSortSearch.Click += new System.EventHandler(this.btnSortSearch_Click);
            // 
            // chkIsSortById
            // 
            this.chkIsSortById.AutoSize = true;
            this.chkIsSortById.Checked = true;
            this.chkIsSortById.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsSortById.Location = new System.Drawing.Point(344, 60);
            this.chkIsSortById.Name = "chkIsSortById";
            this.chkIsSortById.Size = new System.Drawing.Size(120, 16);
            this.chkIsSortById.TabIndex = 1;
            this.chkIsSortById.Text = "是否按ID升序排列";
            this.chkIsSortById.UseVisualStyleBackColor = true;
            // 
            // txtSortKeyword
            // 
            this.txtSortKeyword.Location = new System.Drawing.Point(133, 56);
            this.txtSortKeyword.Name = "txtSortKeyword";
            this.txtSortKeyword.Size = new System.Drawing.Size(187, 21);
            this.txtSortKeyword.TabIndex = 2;
            // 
            // txtPagerKeyword
            // 
            this.txtPagerKeyword.Location = new System.Drawing.Point(152, 43);
            this.txtPagerKeyword.Name = "txtPagerKeyword";
            this.txtPagerKeyword.Size = new System.Drawing.Size(200, 21);
            this.txtPagerKeyword.TabIndex = 2;
            // 
            // btnPagerSearch
            // 
            this.btnPagerSearch.Location = new System.Drawing.Point(397, 41);
            this.btnPagerSearch.Name = "btnPagerSearch";
            this.btnPagerSearch.Size = new System.Drawing.Size(75, 23);
            this.btnPagerSearch.TabIndex = 3;
            this.btnPagerSearch.Text = "分页搜索";
            this.btnPagerSearch.UseVisualStyleBackColor = true;
            this.btnPagerSearch.Click += new System.EventHandler(this.btnPagerSearch_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnMultSearch);
            this.tabPage4.Controls.Add(this.txtMultiSearchKeyword);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(715, 135);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "多索引文件搜索";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // txtMultiSearchKeyword
            // 
            this.txtMultiSearchKeyword.Location = new System.Drawing.Point(185, 63);
            this.txtMultiSearchKeyword.Name = "txtMultiSearchKeyword";
            this.txtMultiSearchKeyword.Size = new System.Drawing.Size(200, 21);
            this.txtMultiSearchKeyword.TabIndex = 0;
            // 
            // btnMultSearch
            // 
            this.btnMultSearch.Location = new System.Drawing.Point(423, 63);
            this.btnMultSearch.Name = "btnMultSearch";
            this.btnMultSearch.Size = new System.Drawing.Size(75, 23);
            this.btnMultSearch.TabIndex = 1;
            this.btnMultSearch.Text = "搜索";
            this.btnMultSearch.UseVisualStyleBackColor = true;
            this.btnMultSearch.Click += new System.EventHandler(this.btnMultSearch_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.txtMultFields);
            this.tabPage5.Controls.Add(this.btnMultFields);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(715, 135);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "多字段搜索";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // btnMultFields
            // 
            this.btnMultFields.Location = new System.Drawing.Point(392, 53);
            this.btnMultFields.Name = "btnMultFields";
            this.btnMultFields.Size = new System.Drawing.Size(75, 23);
            this.btnMultFields.TabIndex = 0;
            this.btnMultFields.Text = "多字段搜索";
            this.btnMultFields.UseVisualStyleBackColor = true;
            this.btnMultFields.Click += new System.EventHandler(this.btnMultFields_Click);
            // 
            // txtMultFields
            // 
            this.txtMultFields.Location = new System.Drawing.Point(158, 53);
            this.txtMultFields.Name = "txtMultFields";
            this.txtMultFields.Size = new System.Drawing.Size(200, 21);
            this.txtMultFields.TabIndex = 1;
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(735, 452);
            this.Controls.Add(this.statusBarLucene);
            this.Controls.Add(this.rtbMsg);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "SearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lucene.Net SEARCH";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchForm_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.RichTextBox rtbMsg;
        private System.Windows.Forms.StatusBar statusBarLucene;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnTermQuery;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnFuzzyQuery;
        private System.Windows.Forms.Button btnWildcardQuery;
        private System.Windows.Forms.Button btnPrefixQuery;
        private System.Windows.Forms.Button btnPhraseQuery;
        private System.Windows.Forms.Button btnBooleanQuery;
        private System.Windows.Forms.Button btnRangeQuery;
        private System.Windows.Forms.Panel panelPager;
        private System.Windows.Forms.TextBox txtSortKeyword;
        private System.Windows.Forms.CheckBox chkIsSortById;
        private System.Windows.Forms.Button btnSortSearch;
        private System.Windows.Forms.Button btnPagerSearch;
        private System.Windows.Forms.TextBox txtPagerKeyword;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnMultSearch;
        private System.Windows.Forms.TextBox txtMultiSearchKeyword;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TextBox txtMultFields;
        private System.Windows.Forms.Button btnMultFields;
    }
}