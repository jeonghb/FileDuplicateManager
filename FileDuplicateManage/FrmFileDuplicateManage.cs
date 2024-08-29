using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.CheckedListBox;

namespace FileDuplicateManage
{
    public partial class FrmFileDuplicateManage : Form
    {
        #region Property
        private enum SearchType
        {
            FileName,
            Hash,
            FileNameAndHash,
        }
        #endregion

        #region 전역변수
        private ConcurrentDictionary<string, TreeNode> _dic = new ConcurrentDictionary<string, TreeNode>(); // 조회한 모든 파일
        private ConcurrentDictionary<string, string> _dicFilter = new ConcurrentDictionary<string, string>(); // 검색조건 필터링
        private int _duplicateCount = 0; // 중복 파일 수 표시
        private bool _stopFlag = false; // 중지여부
        private SearchType _searchType = SearchType.FileName; // 중복체크 방식
        private object lockObject = new object();
        private bool _conditionType = true; // 검색조건 포함검색: true, 제외검색 : false
        private int _activeThreadCount = 0; // 현재 작동중인 Thread 수
        #endregion

        #region 폼 초기화
        public FrmFileDuplicateManage()
        {
            InitializeComponent();
        }
        #endregion

        #region 폼 로드
        private void FrmFileDuplicateManage_Load(object sender, EventArgs e)
        {
            UserConditionBinding();
        }
        #endregion

        #region 유저 검색 조건 바인딩
        private void UserConditionBinding()
        {
            string docName = Application.StartupPath + "\\ConditionList.xml";

            if (File.Exists(docName))
            {
                CklConditionList.Items.Clear();

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(docName);

                foreach (string addValue in xmlDocument.InnerText.Split(','))
                {
                    CklConditionList.Items.Add(addValue);
                }
            }
        }
        #endregion

        #region 검색폴더 추가
        private void TsbDirectoryAdd_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog directory = new FolderBrowserDialog();
            directory.ShowNewFolderButton = false;

            if (directory.ShowDialog() != DialogResult.OK) return;

            foreach (TreeNode node in TvDirectoryList.Nodes)
            {
                if (node.Text == directory.SelectedPath) return;
            }

            TreeNode addNode = new TreeNode(directory.SelectedPath);
            TvDirectoryList.Nodes.Add(addNode);

            ThreadPool.QueueUserWorkItem(GetDirectoryList, addNode);
        }
        #endregion

        #region 하위 디렉토리 Search
        private void GetDirectoryList(object topNode)
        {
            TreeNode node = (TreeNode)topNode;

            DirectoryInfo di = new DirectoryInfo(node.Text);

            if (di.Attributes != FileAttributes.Directory) return;

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                TreeNode addNode = new TreeNode(dir.FullName);

                Invoke(((MethodInvoker)delegate () { node.Nodes.Add(addNode); }));

                ThreadPool.QueueUserWorkItem(GetDirectoryList, addNode);
            }

            if (ChkFileIncluding.Checked)
            {
                foreach (FileInfo file in di.GetFiles())
                {
                    TreeNode addNode = new TreeNode(file.Name);

                    Invoke(((MethodInvoker)delegate () { node.Nodes.Add(addNode); }));
                }
            }
        }
        #endregion

        #region 검색폴더 확장 시 하위폴더 조회
        private void TvDirectoryList_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            foreach (TreeNode node in e.Node.Nodes)
            {
                if (node.Nodes.Count > 0) continue;

                ThreadPool.QueueUserWorkItem(GetDirectoryList, node);
            }
        }
        #endregion

        #region 선택한 검색폴더 재조회
        private void TvDirectoryList_KeyDown(object sender, KeyEventArgs e)
        {
            if (TvDirectoryList.SelectedNode == null) return;

            if (e.KeyCode != Keys.F5) return; // F5 키만 진입 가능한지 체크 -> 원래 로직 없던건데 아무키나 누르면 새로고침?

            TvDirectoryList.SelectedNode.Nodes.Clear();

            GetDirectoryList(TvDirectoryList.SelectedNode);
        }
        #endregion

        #region 검색폴더 제외
        private void TsbDirectoryRemove_Click(object sender, EventArgs e)
        {
            if (TvDirectoryList.SelectedNode == null) return;

            TvDirectoryList.Nodes.Remove(TvDirectoryList.SelectedNode);
        }
        #endregion

        #region 중복 검색 버튼
        private void BtnDuplicateSearchStart_Click(object sender, EventArgs e)
        {
            DuplicateSearchStart();
        }
        #endregion

        #region 중복 검색 시작
        private void DuplicateSearchStart()
        {
            if (TvDirectoryList.Nodes.Count == 0) return;

            BtnDuplicateSearchStart.Enabled = false;
            BtnDuplicateSearchStart.Text = "검색 중";

            _searchType = (RbDuplicateFileName.Checked ? SearchType.FileName : (RbDuplicateHash.Checked ? SearchType.Hash : SearchType.FileNameAndHash));
            _stopFlag = false;
            _dic.Clear();
            TvDuplicateFileList.Nodes.Clear();
            _duplicateCount = 0;
            LbDuplicateCount.Text = "중복 파일 수 : " + _duplicateCount;
            _conditionType = RbConditionIncluding.Checked;
            _activeThreadCount = 0;

            foreach (TreeNode treeNode in TvDirectoryList.Nodes)
            {
                if (_stopFlag) return;

                if (!treeNode.Checked) continue;

                ThreadPool.QueueUserWorkItem(DirFileSearch, treeNode);
            }
        }
        #endregion

        #region 필터 조건 추가
        /// <summary>
        /// 필터 조건 추가
        /// </summary>
        /// <param name="addValue">사용자가 입력한 필터값</param>
        private void SearchConditionAdd(string addValue)
        {
            _dicFilter.TryAdd(addValue, addValue);

            bool duplicate = false;
            int index = 0;

            foreach (string item in CklConditionList.Items)
            {
                if (addValue == item)
                {
                    duplicate = true;
                    CklConditionList.SetItemChecked(index, true);
                    break;
                }

                index++;
            }

            if (!duplicate)
            {
                CklConditionList.Items.Add(addValue);
                CklConditionList.SetItemChecked(CklConditionList.Items.Count - 1, true);
            }
        }
        #endregion

        #region 중복파일 찾기
        private void DirFileSearch(object v_node)
        {
            lock (lockObject) _activeThreadCount++;

            TreeNode node = (TreeNode)v_node;

            DirectoryInfo di = new DirectoryInfo(node.Text);

            if (di.Attributes != FileAttributes.Directory)
            {
                lock (lockObject) _activeThreadCount--;
                return;
            }

            try
            {
                foreach (FileInfo file in di.GetFiles())
                {
                    if (_stopFlag) return;

                    if (!ConditionCheck(file.Name)) continue; // 검색조건 처리

                    if (_searchType == SearchType.FileName)
                    {
                        #region 파일명 검색
                        TreeNode newNode = new TreeNode(file.FullName)
                        {
                            Name = file.Name
                        };

                        if (_stopFlag) return;

                        lock (lockObject)
                        {
                            if (_dic.ContainsKey(file.Name)) // 파일명이 중복되면
                            {
                                if (TvDuplicateFileList.Nodes.Find(_dic[file.Name].Name, false).Length != 0) // 이미 중복이 돼서 Node가 추가되어 있는 경우 자신만 추가
                                {
                                    TvDuplicateFileList.Invoke((MethodInvoker)delegate () { AddNode(file.Name, newNode); });
                                }
                                else
                                {
                                    _dic[file.Name].Nodes.Add(newNode);
                                    _dic[file.Name].Nodes[0].Name = file.Name;

                                    Invoke((MethodInvoker)delegate () { NodeSet(file.Name); });
                                }
                            }
                            else
                            {
                                newNode.Nodes.Add(file.FullName);

                                _dic.TryAdd(file.Name, newNode);
                            }
                        }
                        #endregion
                    }
                    else if (_searchType == SearchType.Hash)
                    {
                        #region 해시코드 검색
                        string hashCode = "";

                        try
                        {
                            // 파일 해시 추출 '-' 제거
                            using (FileStream stream = File.OpenRead(file.FullName))
                            {
                                SHA256Managed sha = new SHA256Managed();
                                byte[] hash = sha.ComputeHash(stream);
                                stream.Close();

                                hashCode = BitConverter.ToString(hash).Replace("-", string.Empty);
                            }
                        }
                        catch
                        {

                        }

                        TreeNode newNode = new TreeNode(file.FullName)
                        {
                            Name = file.Name,
                            Tag = hashCode
                        };

                        if (_stopFlag) return;

                        lock (lockObject)
                        {
                            if (_dic.ContainsKey(hashCode)) // Hash가 중복되면
                            {
                                if (TvDuplicateFileList.Nodes.Find(_dic[hashCode].Name, false).Length != 0) // 이미 중복이 돼서 Node가 추가되어 있는 경우 자신만 추가
                                {
                                    TvDuplicateFileList.Invoke((MethodInvoker)delegate () { AddNode(hashCode, newNode); });
                                }
                                else
                                {
                                    _dic[hashCode].Nodes.Add(newNode);
                                    _dic[hashCode].Nodes[0].Name = file.Name;

                                    Invoke((MethodInvoker)delegate () { NodeSet(hashCode); });
                                }
                            }
                            else
                            {
                                newNode.Nodes.Add(file.FullName);

                                _dic.TryAdd(hashCode, newNode);
                            }
                        }
                        #endregion
                    }
                    else if (_searchType == SearchType.FileNameAndHash)
                    {
                        #region 파일명 + 해시코드 검색
                        string hashCode = "";

                        try
                        {
                            // 파일 해시 추출 '-' 제거
                            using (FileStream stream = File.OpenRead(file.FullName))
                            {
                                SHA256Managed sha = new SHA256Managed();
                                byte[] hash = sha.ComputeHash(stream);
                                stream.Close();

                                hashCode = BitConverter.ToString(hash).Replace("-", string.Empty);
                            }
                        }
                        catch
                        {

                        }

                        TreeNode newNode = new TreeNode(file.FullName)
                        {
                            Name = file.Name,
                            Tag = file.Name + hashCode
                        };

                        if (_stopFlag) return;

                        lock (lockObject)
                        {
                            if (_dic.ContainsKey(file.Name + hashCode)) // 파일명 + 해시코드가 중복되면
                            {
                                if (TvDuplicateFileList.Nodes.Find(_dic[file.Name + hashCode].Name, false).Length != 0) // 이미 중복이 돼서 Node가 추가되어 있는 경우 자신만 추가
                                {
                                    TvDuplicateFileList.Invoke((MethodInvoker)delegate () { AddNode(file.Name + hashCode, newNode); });
                                }
                                else
                                {
                                    _dic[file.Name + hashCode].Nodes.Add(newNode);
                                    _dic[file.Name + hashCode].Nodes[0].Name = file.Name;

                                    Invoke((MethodInvoker)delegate () { NodeSet(file.Name + hashCode); });
                                }
                            }
                            else
                            {
                                newNode.Nodes.Add(file.FullName);

                                _dic.TryAdd(file.Name + hashCode, newNode);
                            }
                        }
                        #endregion
                    }

                    Application.DoEvents();
                }

                foreach (TreeNode underNode in node.Nodes)
                {
                    if (_stopFlag) return;

                    if (!underNode.Checked) continue;

                    ThreadPool.QueueUserWorkItem(DirFileSearch, underNode);
                }

                lock (lockObject)
                {
                    _activeThreadCount--;
                }

                if (_activeThreadCount == 0)
                {
                    Invoke((MethodInvoker)delegate () { BtnDuplicateSearchStart.Enabled = true; BtnDuplicateSearchStart.Text = "중복파일 검색"; });
                }
            }
            catch
            {
                lock (lockObject)
                {
                    _activeThreadCount--;
                    Invoke((MethodInvoker)delegate () { _activeThreadCount.ToString(); BtnDuplicateSearchStart.Enabled = true; BtnDuplicateSearchStart.Text = "중복파일 검색"; });
                }
            }
        }
        #endregion

        #region 검색조건 체크
        /// <summary>
        /// 받은 변수값에 기본값과 사용자가 입력한 값이 포함되어 있으면 false 반환
        /// </summary>
        /// <param name="fileName">파일명</param>
        /// <returns></returns>
        private bool ConditionCheck(string fileName)
        {
            string[] extensions = { ".bat", ".bin", ".ini", ".inf", ".reg", ".tmp", ".sys" }; // 기본 제외 항목

            foreach (string value in extensions)
            {
                if (fileName.Contains(value)) return false;
            }

            if (CklConditionList.CheckedItems.Count == 0) return true; // 사용자가 입력한 제외 항목

            foreach (string item in CklConditionList.CheckedItems)
            {
                if (fileName.Contains(item)) return _conditionType;
            }

            return !_conditionType;
        }
        #endregion

        #region 중복파일 추가
        /// <summary>
        /// 중복된 파일이 처음 발생 시 중복된 파일 2개 모두 화면에 추가
        /// </summary>
        /// <param name="name">파일위치 + 파일명</param>
        private void NodeSet(string name)
        {
            if (_stopFlag) return;

            try
            {
                _dic[name].Text = name;
                TvDuplicateFileList.Nodes.Add(_dic[name]);
                _duplicateCount += 2;

                LbDuplicateCount.Text = "중복 파일 수 : " + _duplicateCount;
            }
            catch
            {

            }
        }

        /// <summary>
        /// 중복된 파일이 추가로 발생 시 해당 파일만 화면에 추가
        /// </summary>
        /// <param name="name">파일명</param>
        /// <param name="node">이미 추가된 상위 노드</param>
        private void AddNode(string name, TreeNode node)
        {
            if (_stopFlag) return;

            try
            {
                _dic[name].Nodes.Add(node);

                _duplicateCount++;

                LbDuplicateCount.Text = "중복 파일 수 : " + _duplicateCount;
            }
            catch
            {

            }
        }
        #endregion

        #region 폴더 열기
        private void TsmFileDerictoryOpen_Click(object sender, EventArgs e)
        {
            if (TvDuplicateFileList.SelectedNode.Nodes != null && TvDuplicateFileList.SelectedNode.Nodes.Count > 0) return;

            try
            {
                Process.Start((TvDuplicateFileList.SelectedNode.Text.Substring(0, TvDuplicateFileList.SelectedNode.Text.Length - TvDuplicateFileList.SelectedNode.Name.Length)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 파일 열기
        private void TvDuplicateFileList_DoubleClick(object sender, EventArgs e)
        {
            if (((TreeView)sender).SelectedNode.Nodes.Count > 0) return; // 파일을 선택한 경우만 통과

            try
            {
                Process.Start(TvDuplicateFileList.SelectedNode.FullPath.Replace(TvDuplicateFileList.SelectedNode.Parent.Text + "\\", ""));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TsmFileOpen_Click(object sender, EventArgs e)
        {
            if (TvDuplicateFileList.SelectedNode.Nodes != null && TvDuplicateFileList.SelectedNode.Nodes.Count > 0) return;

            try
            {
                Process.Start(TvDuplicateFileList.SelectedNode.FullPath.Replace(TvDuplicateFileList.SelectedNode.Parent.Text + "\\", ""));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 파일 삭제
        private void TsmFileDelete_Click(object sender, EventArgs e)
        {
            if (TvDuplicateFileList.SelectedNode == null || TvDuplicateFileList.SelectedNode.Nodes.Count > 0) return;

            // 더이상 묻지 않음? 아니면 그냥 메세지 출력 후 삭제
            File.Delete(TvDuplicateFileList.SelectedNode.FullPath.Replace(TvDuplicateFileList.SelectedNode.Parent.Text + "\\", ""));

            TvDuplicateFileList.Nodes.Remove(TvDuplicateFileList.SelectedNode);
        }
        #endregion

        #region 파일명 찾기
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            int i = 0;

            if (TvDuplicateFileList.SelectedNode != null)
            {
                i = TvDuplicateFileList.SelectedNode.Index + 1;
            }

            if (i == TvDuplicateFileList.Nodes.Count)
            {
                i = 0;
            }

            for (; i < TvDuplicateFileList.Nodes.Count; i++)
            {
                if (TvDuplicateFileList.Nodes[i].Name.ToUpper().Contains(TxtDuplicateFileFilter.Text.ToUpper()))
                {
                    TvDuplicateFileList.SelectedNode = TvDuplicateFileList.Nodes[i];
                    TvDuplicateFileList.Focus();
                    return;
                }
            }
        }
        #endregion

        #region 중복자료 엑셀로 다운로드
        private void BtnDuplicateListDownload_Click(object sender, EventArgs e)
        {
            if (TvDuplicateFileList.Nodes.Count == 0) return;

            panel3.Visible = true;

            Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook wb = xla.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xla.ActiveSheet;
            Microsoft.Office.Interop.Excel.Range range = null;

            try
            {
                progressBar1.Maximum = _duplicateCount;
                progressBar1.Value = 0;

                Microsoft.Office.Interop.Excel.Range c1 = ws.Cells[1, 1];
                Microsoft.Office.Interop.Excel.Range c2 = ws.Cells[DrawExcel(ws, TvDuplicateFileList.Nodes, 1, 1), 2];

                panel3.Visible = false;

                range = ws.get_Range(c1, c2);
                range.Columns.AutoFit();
                xla.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            finally
            {
                // Clean up
                ReleaseExcelObject(range);
                ReleaseExcelObject(ws);
                //wb.Close(true);
                ReleaseExcelObject(wb);
                //xla.Quit();
                ReleaseExcelObject(xla);
                xla = null;
            }
        }

        protected static void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// 해당 트리뷰를 엑셀형태로 다운로드
        /// </summary>
        /// <param name="ws">데이터를 담을 Worksheet</param>
        /// <param name="v_nodeCollection">엑셀로 다운로드할 TreeView의 Nodes</param>
        /// <param name="v_row">시작할 Row(1부터 시작)</param>
        /// <param name="level">시작할 Column(1부터 시작)</param>
        private int DrawExcel(Microsoft.Office.Interop.Excel.Worksheet ws, TreeNodeCollection v_nodeCollection, int v_row, int level)
        {
            try
            {
                foreach (TreeNode node in v_nodeCollection)
                {
                    ws.Cells[v_row, level] = (node.Parent == null ? node.Text : node.Text.Replace(node.Parent.Text + "\\", ""));
                    ws.Cells[v_row, level].Interior.Color = Color.Bisque; // 재귀함수일 경우 색깔 수정해야함

                    v_row++;
                    progressBar1.Value++;

                    if (node.Nodes == null) continue;

                    if (!BtnExcelDownloadStop.Enabled) break;

                    Application.DoEvents();

                    v_row = DrawExcel(ws, node.Nodes, v_row, level + 1);

                    if (!BtnExcelDownloadStop.Enabled) break;

                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233086)
                {
                    MessageBox.Show("디렉토리 조회가 아직 완료되지 않아 현재까지 조회된 디렉토리까지 다운로드됩니다.");
                    return v_row;
                }

                MessageBox.Show(ex.ToString());
                return v_row;
            }

            return v_row;
        }
        #endregion

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void TxtDuplicateFileFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter)) BtnSearch_Click(null, null);
        }

        private void TvDuplicateFileList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) BtnSearch_Click(null, null);

            if (e.KeyChar >= 32 && e.KeyChar <= 122)
            {
                TxtDuplicateFileFilter.Text += e.KeyChar;
                TxtDuplicateFileFilter.Focus();
                TxtDuplicateFileFilter.SelectionStart = TxtDuplicateFileFilter.Text.Length;
            }
        }

        private void TvDuplicateFileList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TvDuplicateFileList.SelectedNode = e.Node;
        }

        private void BtnDuplicateSearchStop_Click(object sender, EventArgs e)
        {
            _stopFlag = true;

            BtnDuplicateSearchStart.Enabled = true;
            BtnDuplicateSearchStart.Text = "중복파일 검색";
        }

        private void BtnConditionAdd_Click(object sender, EventArgs e)
        {
            string[] split = TxtCondition.Text.Split(',');

            if (split[split.Count() - 1].Length == 0) return;

            SearchConditionAdd(split[split.Count() - 1]);
        }

        private void RbConditionException_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(RbConditionException, "확장자 bat, bin, ini, inf, reg, tmp, sys는 기본적으로 제외됩니다.");
        }

        private void BtnExcelDownloadStop_Click(object sender, EventArgs e)
        {
            BtnExcelDownloadStop.Enabled = false;
        }

        private void TsbDirectoryRemoveAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("탐색폴더 항목이 모두 초기화됩니다.\n초기화하시겠습니까?", "탐색폴더 항목 초기화", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            TvDirectoryList.Nodes.Clear();
        }

        private void TvDirectoryList_AfterCheck(object sender, TreeViewEventArgs e)
        {
            CheckChangeTreeNode(e.Node, e.Node.Checked);
        }

        private void CheckChangeTreeNode(TreeNode v_node, bool newStatus)
        {
            foreach (TreeNode node in v_node.Nodes)
            {
                node.Checked = newStatus;

                foreach (TreeNode underNode in node.Nodes)
                {
                    CheckChangeTreeNode(underNode, newStatus);
                }
            }
        }

        private void BtnDirectoryInfoDownload_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("폴더 정보를 다운로드하시겠습니까?", "탐색폴더 정보 다운로드", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            if (TvDirectoryList.Nodes.Count == 0) return;

            panel3.Visible = true;

            Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook wb = xla.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xla.ActiveSheet;
            Microsoft.Office.Interop.Excel.Range range = null;

            try
            {
                int count = 0;

                foreach (TreeNode node in TvDirectoryList.Nodes)
                {
                    count += NodeCount(node);
                }

                progressBar1.Maximum = count;
                progressBar1.Value = 0;

                Microsoft.Office.Interop.Excel.Range c1 = ws.Cells[1, 1];
                Microsoft.Office.Interop.Excel.Range c2 = ws.Cells[DrawExcel(ws, TvDirectoryList.Nodes, 1, 1), 2];

                panel3.Visible = false;

                range = ws.get_Range(c1, c2);
                range.Columns.AutoFit();
                xla.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            finally
            {
                // Clean up
                ReleaseExcelObject(range);
                ReleaseExcelObject(ws);
                //wb.Close(true);
                ReleaseExcelObject(wb);
                //xla.Quit();
                ReleaseExcelObject(xla);
                xla = null;
            }
        }

        private int NodeCount(TreeNode v_node)
        {
            int count = 0;

            foreach (TreeNode node in v_node.Nodes)
            {
                count++;

                count += NodeCount(node);
            }

            return count;
        }

        #region 조회된 중복 파일 중 하나만 남기고 제거
        private void BtnDuplicateDeleteAll_Click(object sender, EventArgs e)
        {
            if (BtnDuplicateSearchStart.Enabled == false)
            {
                MessageBox.Show("아직 중복파일 검색이 끝나지 않았습니다.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                foreach (TreeNode parentNode in TvDuplicateFileList.Nodes)
                {
                    while (parentNode.Nodes.Count > 1)
                    {
                        File.Delete(parentNode.FirstNode.Text);
                        parentNode.Nodes.RemoveAt(0);
                    }
                }

                MessageBox.Show("삭제 완료", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("삭제 처리 중 오류가 발생했습니다.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
