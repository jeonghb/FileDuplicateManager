namespace FileDuplicateManage
{
    partial class FrmFileDuplicateManage
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFileDuplicateManage));
            this.TvDirectoryList = new System.Windows.Forms.TreeView();
            this.TvDuplicateFileList = new System.Windows.Forms.TreeView();
            this.CmsDuplicateFileList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TsmFileDerictoryOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.TsmFileDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnDuplicateSearchStart = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.LbDuplicateCount = new System.Windows.Forms.Label();
            this.TxtDuplicateFileFilter = new System.Windows.Forms.TextBox();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.RbDuplicateFileName = new System.Windows.Forms.RadioButton();
            this.RbDuplicateHash = new System.Windows.Forms.RadioButton();
            this.BtnDuplicateSearchStop = new System.Windows.Forms.Button();
            this.BtnConditionAdd = new System.Windows.Forms.Button();
            this.RbConditionException = new System.Windows.Forms.RadioButton();
            this.RbConditionIncluding = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtCondition = new System.Windows.Forms.TextBox();
            this.CklConditionList = new System.Windows.Forms.CheckedListBox();
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.TsbDirectoryAdd = new System.Windows.Forms.ToolStripButton();
            this.TsbDirectoryRemove = new System.Windows.Forms.ToolStripButton();
            this.TsbDirectoryRemoveAll = new System.Windows.Forms.ToolStripButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ChkFileIncluding = new System.Windows.Forms.CheckBox();
            this.BtnDirectoryInfoDownload = new System.Windows.Forms.Button();
            this.RbDuplicateFileNameAndHash = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BtnDuplicateDeleteAll = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.BtnExcelDownloadStop = new System.Windows.Forms.Button();
            this.LbProgress = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.BtnDuplicateListDownload = new System.Windows.Forms.Button();
            this.CmsDuplicateFileList.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.ToolStrip.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // TvDirectoryList
            // 
            this.TvDirectoryList.CheckBoxes = true;
            this.TvDirectoryList.Location = new System.Drawing.Point(6, 36);
            this.TvDirectoryList.Name = "TvDirectoryList";
            this.TvDirectoryList.Size = new System.Drawing.Size(976, 139);
            this.TvDirectoryList.TabIndex = 1;
            this.TvDirectoryList.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TvDirectoryList_AfterCheck);
            this.TvDirectoryList.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TvDirectoryList_BeforeExpand);
            this.TvDirectoryList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TvDirectoryList_KeyDown);
            // 
            // TvDuplicateFileList
            // 
            this.TvDuplicateFileList.ContextMenuStrip = this.CmsDuplicateFileList;
            this.TvDuplicateFileList.ItemHeight = 20;
            this.TvDuplicateFileList.Location = new System.Drawing.Point(12, 49);
            this.TvDuplicateFileList.Name = "TvDuplicateFileList";
            this.TvDuplicateFileList.Size = new System.Drawing.Size(1322, 347);
            this.TvDuplicateFileList.TabIndex = 6;
            this.TvDuplicateFileList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TvDuplicateFileList_NodeMouseClick);
            this.TvDuplicateFileList.DoubleClick += new System.EventHandler(this.TvDuplicateFileList_DoubleClick);
            this.TvDuplicateFileList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TvDuplicateFileList_KeyPress);
            // 
            // CmsDuplicateFileList
            // 
            this.CmsDuplicateFileList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsmFileDerictoryOpen,
            this.TsmFileOpen,
            this.TsmFileDelete});
            this.CmsDuplicateFileList.Name = "contextMenuStrip1";
            this.CmsDuplicateFileList.Size = new System.Drawing.Size(127, 70);
            this.CmsDuplicateFileList.Text = "삭제";
            // 
            // TsmFileDerictoryOpen
            // 
            this.TsmFileDerictoryOpen.Name = "TsmFileDerictoryOpen";
            this.TsmFileDerictoryOpen.Size = new System.Drawing.Size(126, 22);
            this.TsmFileDerictoryOpen.Text = "폴더 열기";
            this.TsmFileDerictoryOpen.Click += new System.EventHandler(this.TsmFileDerictoryOpen_Click);
            // 
            // TsmFileOpen
            // 
            this.TsmFileOpen.Name = "TsmFileOpen";
            this.TsmFileOpen.Size = new System.Drawing.Size(126, 22);
            this.TsmFileOpen.Text = "파일 열기";
            this.TsmFileOpen.Click += new System.EventHandler(this.TsmFileOpen_Click);
            // 
            // TsmFileDelete
            // 
            this.TsmFileDelete.Name = "TsmFileDelete";
            this.TsmFileDelete.Size = new System.Drawing.Size(126, 22);
            this.TsmFileDelete.Text = "파일 삭제";
            this.TsmFileDelete.Click += new System.EventHandler(this.TsmFileDelete_Click);
            // 
            // BtnDuplicateSearchStart
            // 
            this.BtnDuplicateSearchStart.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnDuplicateSearchStart.Location = new System.Drawing.Point(1148, 166);
            this.BtnDuplicateSearchStart.Name = "BtnDuplicateSearchStart";
            this.BtnDuplicateSearchStart.Size = new System.Drawing.Size(98, 47);
            this.BtnDuplicateSearchStart.TabIndex = 8;
            this.BtnDuplicateSearchStart.Text = "중복파일 검색";
            this.BtnDuplicateSearchStart.UseVisualStyleBackColor = true;
            this.BtnDuplicateSearchStart.Click += new System.EventHandler(this.BtnDuplicateSearchStart_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnClose.Location = new System.Drawing.Point(1288, 660);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(64, 24);
            this.BtnClose.TabIndex = 9;
            this.BtnClose.Text = "닫기";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // LbDuplicateCount
            // 
            this.LbDuplicateCount.AutoSize = true;
            this.LbDuplicateCount.Location = new System.Drawing.Point(10, 403);
            this.LbDuplicateCount.Name = "LbDuplicateCount";
            this.LbDuplicateCount.Size = new System.Drawing.Size(45, 12);
            this.LbDuplicateCount.TabIndex = 10;
            this.LbDuplicateCount.Text = "중복 수";
            // 
            // TxtDuplicateFileFilter
            // 
            this.TxtDuplicateFileFilter.Location = new System.Drawing.Point(930, 21);
            this.TxtDuplicateFileFilter.Name = "TxtDuplicateFileFilter";
            this.TxtDuplicateFileFilter.Size = new System.Drawing.Size(214, 21);
            this.TxtDuplicateFileFilter.TabIndex = 11;
            this.TxtDuplicateFileFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDuplicateFileFilter_KeyDown);
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(1150, 20);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(75, 23);
            this.BtnSearch.TabIndex = 12;
            this.BtnSearch.Text = "찾기";
            this.BtnSearch.UseVisualStyleBackColor = true;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(831, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "중복파일명 검색";
            // 
            // RbDuplicateFileName
            // 
            this.RbDuplicateFileName.AutoSize = true;
            this.RbDuplicateFileName.Checked = true;
            this.RbDuplicateFileName.Location = new System.Drawing.Point(1015, 159);
            this.RbDuplicateFileName.Name = "RbDuplicateFileName";
            this.RbDuplicateFileName.Size = new System.Drawing.Size(87, 16);
            this.RbDuplicateFileName.TabIndex = 15;
            this.RbDuplicateFileName.TabStop = true;
            this.RbDuplicateFileName.Text = "파일명 일치";
            this.RbDuplicateFileName.UseVisualStyleBackColor = true;
            // 
            // RbDuplicateHash
            // 
            this.RbDuplicateHash.AutoSize = true;
            this.RbDuplicateHash.Location = new System.Drawing.Point(1015, 181);
            this.RbDuplicateHash.Name = "RbDuplicateHash";
            this.RbDuplicateHash.Size = new System.Drawing.Size(114, 16);
            this.RbDuplicateHash.TabIndex = 16;
            this.RbDuplicateHash.TabStop = true;
            this.RbDuplicateHash.Text = "내용(Hash) 일치";
            this.RbDuplicateHash.UseVisualStyleBackColor = true;
            // 
            // BtnDuplicateSearchStop
            // 
            this.BtnDuplicateSearchStop.Location = new System.Drawing.Point(1252, 166);
            this.BtnDuplicateSearchStop.Name = "BtnDuplicateSearchStop";
            this.BtnDuplicateSearchStop.Size = new System.Drawing.Size(88, 47);
            this.BtnDuplicateSearchStop.TabIndex = 17;
            this.BtnDuplicateSearchStop.Text = "중지";
            this.BtnDuplicateSearchStop.UseVisualStyleBackColor = true;
            this.BtnDuplicateSearchStop.Click += new System.EventHandler(this.BtnDuplicateSearchStop_Click);
            // 
            // BtnConditionAdd
            // 
            this.BtnConditionAdd.Location = new System.Drawing.Point(9, 112);
            this.BtnConditionAdd.Name = "BtnConditionAdd";
            this.BtnConditionAdd.Size = new System.Drawing.Size(103, 23);
            this.BtnConditionAdd.TabIndex = 20;
            this.BtnConditionAdd.Text = "조건 추가";
            this.BtnConditionAdd.UseVisualStyleBackColor = true;
            this.BtnConditionAdd.Click += new System.EventHandler(this.BtnConditionAdd_Click);
            // 
            // RbConditionException
            // 
            this.RbConditionException.AutoSize = true;
            this.RbConditionException.Location = new System.Drawing.Point(9, 44);
            this.RbConditionException.Name = "RbConditionException";
            this.RbConditionException.Size = new System.Drawing.Size(103, 16);
            this.RbConditionException.TabIndex = 22;
            this.RbConditionException.TabStop = true;
            this.RbConditionException.Text = "조건 제외 검색";
            this.RbConditionException.UseVisualStyleBackColor = true;
            this.RbConditionException.MouseHover += new System.EventHandler(this.RbConditionException_MouseHover);
            // 
            // RbConditionIncluding
            // 
            this.RbConditionIncluding.AutoSize = true;
            this.RbConditionIncluding.Checked = true;
            this.RbConditionIncluding.Location = new System.Drawing.Point(9, 23);
            this.RbConditionIncluding.Name = "RbConditionIncluding";
            this.RbConditionIncluding.Size = new System.Drawing.Size(103, 16);
            this.RbConditionIncluding.TabIndex = 21;
            this.RbConditionIncluding.TabStop = true;
            this.RbConditionIncluding.Text = "조건 포함 검색";
            this.RbConditionIncluding.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtCondition);
            this.groupBox1.Controls.Add(this.CklConditionList);
            this.groupBox1.Controls.Add(this.RbConditionIncluding);
            this.groupBox1.Controls.Add(this.RbConditionException);
            this.groupBox1.Controls.Add(this.BtnConditionAdd);
            this.groupBox1.Location = new System.Drawing.Point(1006, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 141);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "특정 검색";
            // 
            // TxtCondition
            // 
            this.TxtCondition.Location = new System.Drawing.Point(9, 85);
            this.TxtCondition.Name = "TxtCondition";
            this.TxtCondition.Size = new System.Drawing.Size(103, 21);
            this.TxtCondition.TabIndex = 221;
            // 
            // CklConditionList
            // 
            this.CklConditionList.FormattingEnabled = true;
            this.CklConditionList.Location = new System.Drawing.Point(118, 20);
            this.CklConditionList.Name = "CklConditionList";
            this.CklConditionList.Size = new System.Drawing.Size(216, 116);
            this.CklConditionList.TabIndex = 25;
            // 
            // ToolStrip
            // 
            this.ToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbDirectoryAdd,
            this.TsbDirectoryRemove,
            this.TsbDirectoryRemoveAll});
            this.ToolStrip.Location = new System.Drawing.Point(793, 11);
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.Size = new System.Drawing.Size(189, 25);
            this.ToolStrip.TabIndex = 24;
            this.ToolStrip.Text = "toolStrip1";
            // 
            // TsbDirectoryAdd
            // 
            this.TsbDirectoryAdd.Image = ((System.Drawing.Image)(resources.GetObject("TsbDirectoryAdd.Image")));
            this.TsbDirectoryAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbDirectoryAdd.Name = "TsbDirectoryAdd";
            this.TsbDirectoryAdd.Size = new System.Drawing.Size(51, 22);
            this.TsbDirectoryAdd.Text = "추가";
            this.TsbDirectoryAdd.Click += new System.EventHandler(this.TsbDirectoryAdd_Click);
            // 
            // TsbDirectoryRemove
            // 
            this.TsbDirectoryRemove.Image = ((System.Drawing.Image)(resources.GetObject("TsbDirectoryRemove.Image")));
            this.TsbDirectoryRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbDirectoryRemove.Name = "TsbDirectoryRemove";
            this.TsbDirectoryRemove.Size = new System.Drawing.Size(51, 22);
            this.TsbDirectoryRemove.Text = "삭제";
            this.TsbDirectoryRemove.Click += new System.EventHandler(this.TsbDirectoryRemove_Click);
            // 
            // TsbDirectoryRemoveAll
            // 
            this.TsbDirectoryRemoveAll.Image = ((System.Drawing.Image)(resources.GetObject("TsbDirectoryRemoveAll.Image")));
            this.TsbDirectoryRemoveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbDirectoryRemoveAll.Name = "TsbDirectoryRemoveAll";
            this.TsbDirectoryRemoveAll.Size = new System.Drawing.Size(75, 22);
            this.TsbDirectoryRemoveAll.Text = "전체삭제";
            this.TsbDirectoryRemoveAll.Click += new System.EventHandler(this.TsbDirectoryRemoveAll_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ChkFileIncluding);
            this.groupBox2.Controls.Add(this.BtnDirectoryInfoDownload);
            this.groupBox2.Controls.Add(this.ToolStrip);
            this.groupBox2.Controls.Add(this.TvDirectoryList);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(988, 210);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "검색폴더 지정";
            // 
            // ChkFileIncluding
            // 
            this.ChkFileIncluding.AutoSize = true;
            this.ChkFileIncluding.Location = new System.Drawing.Point(686, 14);
            this.ChkFileIncluding.Name = "ChkFileIncluding";
            this.ChkFileIncluding.Size = new System.Drawing.Size(104, 16);
            this.ChkFileIncluding.TabIndex = 25;
            this.ChkFileIncluding.Text = "파일 포함 추가";
            this.ChkFileIncluding.UseVisualStyleBackColor = true;
            // 
            // BtnDirectoryInfoDownload
            // 
            this.BtnDirectoryInfoDownload.Location = new System.Drawing.Point(868, 181);
            this.BtnDirectoryInfoDownload.Name = "BtnDirectoryInfoDownload";
            this.BtnDirectoryInfoDownload.Size = new System.Drawing.Size(114, 23);
            this.BtnDirectoryInfoDownload.TabIndex = 221;
            this.BtnDirectoryInfoDownload.Text = "폴더정보 다운로드";
            this.BtnDirectoryInfoDownload.UseVisualStyleBackColor = true;
            this.BtnDirectoryInfoDownload.Click += new System.EventHandler(this.BtnDirectoryInfoDownload_Click);
            // 
            // RbDuplicateFileNameAndHash
            // 
            this.RbDuplicateFileNameAndHash.AutoSize = true;
            this.RbDuplicateFileNameAndHash.Location = new System.Drawing.Point(1015, 203);
            this.RbDuplicateFileNameAndHash.Name = "RbDuplicateFileNameAndHash";
            this.RbDuplicateFileNameAndHash.Size = new System.Drawing.Size(127, 16);
            this.RbDuplicateFileNameAndHash.TabIndex = 25;
            this.RbDuplicateFileNameAndHash.TabStop = true;
            this.RbDuplicateFileNameAndHash.Text = "파일명과 내용 일치";
            this.RbDuplicateFileNameAndHash.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BtnDuplicateDeleteAll);
            this.groupBox3.Controls.Add(this.panel3);
            this.groupBox3.Controls.Add(this.BtnDuplicateListDownload);
            this.groupBox3.Controls.Add(this.TvDuplicateFileList);
            this.groupBox3.Controls.Add(this.BtnSearch);
            this.groupBox3.Controls.Add(this.TxtDuplicateFileFilter);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.LbDuplicateCount);
            this.groupBox3.Location = new System.Drawing.Point(12, 228);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1340, 426);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "중복파일 항목";
            // 
            // BtnDuplicateDeleteAll
            // 
            this.BtnDuplicateDeleteAll.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnDuplicateDeleteAll.Location = new System.Drawing.Point(12, 21);
            this.BtnDuplicateDeleteAll.Name = "BtnDuplicateDeleteAll";
            this.BtnDuplicateDeleteAll.Size = new System.Drawing.Size(142, 24);
            this.BtnDuplicateDeleteAll.TabIndex = 26;
            this.BtnDuplicateDeleteAll.Text = "중복파일 삭제";
            this.BtnDuplicateDeleteAll.UseVisualStyleBackColor = true;
            this.BtnDuplicateDeleteAll.Click += new System.EventHandler(this.BtnDuplicateDeleteAll_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.BtnExcelDownloadStop);
            this.panel3.Controls.Add(this.LbProgress);
            this.panel3.Controls.Add(this.progressBar1);
            this.panel3.Location = new System.Drawing.Point(426, 79);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(489, 87);
            this.panel3.TabIndex = 220;
            this.panel3.Visible = false;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.Location = new System.Drawing.Point(10, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(468, 23);
            this.label12.TabIndex = 2;
            this.label12.Text = "조회된 데이터를 엑셀 파일로 작성중입니다. 잠시만 기다려주세요.";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnExcelDownloadStop
            // 
            this.BtnExcelDownloadStop.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnExcelDownloadStop.Location = new System.Drawing.Point(416, 58);
            this.BtnExcelDownloadStop.Name = "BtnExcelDownloadStop";
            this.BtnExcelDownloadStop.Size = new System.Drawing.Size(62, 22);
            this.BtnExcelDownloadStop.TabIndex = 3;
            this.BtnExcelDownloadStop.Text = "중지";
            this.BtnExcelDownloadStop.UseVisualStyleBackColor = true;
            this.BtnExcelDownloadStop.Click += new System.EventHandler(this.BtnExcelDownloadStop_Click);
            // 
            // LbProgress
            // 
            this.LbProgress.Location = new System.Drawing.Point(10, 57);
            this.LbProgress.Name = "LbProgress";
            this.LbProgress.Size = new System.Drawing.Size(468, 23);
            this.LbProgress.TabIndex = 1;
            this.LbProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(10, 31);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(468, 22);
            this.progressBar1.TabIndex = 0;
            // 
            // BtnDuplicateListDownload
            // 
            this.BtnDuplicateListDownload.Location = new System.Drawing.Point(1231, 20);
            this.BtnDuplicateListDownload.Name = "BtnDuplicateListDownload";
            this.BtnDuplicateListDownload.Size = new System.Drawing.Size(103, 23);
            this.BtnDuplicateListDownload.TabIndex = 15;
            this.BtnDuplicateListDownload.Text = "엑셀 다운로드";
            this.BtnDuplicateListDownload.UseVisualStyleBackColor = true;
            this.BtnDuplicateListDownload.Click += new System.EventHandler(this.BtnDuplicateListDownload_Click);
            // 
            // FrmFileDuplicateManage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1364, 696);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.RbDuplicateFileNameAndHash);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnDuplicateSearchStop);
            this.Controls.Add(this.RbDuplicateHash);
            this.Controls.Add(this.RbDuplicateFileName);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.BtnDuplicateSearchStart);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFileDuplicateManage";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.FrmFileDuplicateManage_Load);
            this.CmsDuplicateFileList.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView TvDirectoryList;
        private System.Windows.Forms.TreeView TvDuplicateFileList;
        private System.Windows.Forms.Button BtnDuplicateSearchStart;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.ContextMenuStrip CmsDuplicateFileList;
        private System.Windows.Forms.ToolStripMenuItem TsmFileDerictoryOpen;
        private System.Windows.Forms.ToolStripMenuItem TsmFileOpen;
        private System.Windows.Forms.ToolStripMenuItem TsmFileDelete;
        private System.Windows.Forms.Label LbDuplicateCount;
        private System.Windows.Forms.TextBox TxtDuplicateFileFilter;
        private System.Windows.Forms.Button BtnSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton RbDuplicateFileName;
        private System.Windows.Forms.RadioButton RbDuplicateHash;
        private System.Windows.Forms.Button BtnDuplicateSearchStop;
        private System.Windows.Forms.Button BtnConditionAdd;
        private System.Windows.Forms.RadioButton RbConditionException;
        private System.Windows.Forms.RadioButton RbConditionIncluding;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip ToolStrip;
        private System.Windows.Forms.ToolStripButton TsbDirectoryAdd;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripButton TsbDirectoryRemove;
        private System.Windows.Forms.ToolStripButton TsbDirectoryRemoveAll;
        private System.Windows.Forms.RadioButton RbDuplicateFileNameAndHash;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button BtnDuplicateListDownload;
        protected System.Windows.Forms.Panel panel3;
        protected System.Windows.Forms.Label label12;
        protected System.Windows.Forms.Button BtnExcelDownloadStop;
        protected System.Windows.Forms.Label LbProgress;
        protected System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button BtnDirectoryInfoDownload;
        private System.Windows.Forms.CheckBox ChkFileIncluding;
        private System.Windows.Forms.TextBox TxtCondition;
        private System.Windows.Forms.CheckedListBox CklConditionList;
        private System.Windows.Forms.Button BtnDuplicateDeleteAll;
    }
}

