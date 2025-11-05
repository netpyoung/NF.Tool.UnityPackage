namespace UnityPackageGUI_Winform
{
    partial class UnityPackageGUI_Winform
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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            tabControl1 = new System.Windows.Forms.TabControl();
            tab_Unpack = new System.Windows.Forms.TabPage();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            btn_unpack = new System.Windows.Forms.Button();
            tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            check_is_unpack_meta = new System.Windows.Forms.CheckBox();
            lbl_unpack = new System.Windows.Forms.RichTextBox();
            btn_unitypackage = new System.Windows.Forms.Button();
            tab_Pack = new System.Windows.Forms.TabPage();
            tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            btn_folder = new System.Windows.Forms.Button();
            lbl_pack = new System.Windows.Forms.RichTextBox();
            btn_pack = new System.Windows.Forms.Button();
            tab_Info = new System.Windows.Forms.TabPage();
            lbl_info = new System.Windows.Forms.RichTextBox();
            tableLayoutPanel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tab_Unpack.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tab_Pack.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tab_Info.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(tabControl1, 0, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tab_Unpack);
            tabControl1.Controls.Add(tab_Pack);
            tabControl1.Controls.Add(tab_Info);
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl1.Location = new System.Drawing.Point(3, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(794, 444);
            tabControl1.TabIndex = 0;
            // 
            // tab_Unpack
            // 
            tab_Unpack.Controls.Add(tableLayoutPanel2);
            tab_Unpack.Location = new System.Drawing.Point(4, 24);
            tab_Unpack.Name = "tab_Unpack";
            tab_Unpack.Padding = new System.Windows.Forms.Padding(3);
            tab_Unpack.Size = new System.Drawing.Size(786, 416);
            tab_Unpack.TabIndex = 0;
            tab_Unpack.Text = "Unpack";
            tab_Unpack.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(btn_unpack, 0, 1);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel2.Size = new System.Drawing.Size(780, 410);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // btn_unpack
            // 
            btn_unpack.Dock = System.Windows.Forms.DockStyle.Fill;
            btn_unpack.Location = new System.Drawing.Point(3, 208);
            btn_unpack.Name = "btn_unpack";
            btn_unpack.Size = new System.Drawing.Size(774, 199);
            btn_unpack.TabIndex = 0;
            btn_unpack.Text = "Unpack";
            btn_unpack.UseVisualStyleBackColor = true;
            btn_unpack.Click += btn_unpack_Click;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel2.SetColumnSpan(tableLayoutPanel3, 2);
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel3.Controls.Add(check_is_unpack_meta, 0, 1);
            tableLayoutPanel3.Controls.Add(lbl_unpack, 1, 0);
            tableLayoutPanel3.Controls.Add(btn_unitypackage, 0, 0);
            tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.1256275F));
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74.8743744F));
            tableLayoutPanel3.Size = new System.Drawing.Size(774, 199);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // check_is_unpack_meta
            // 
            check_is_unpack_meta.AutoSize = true;
            tableLayoutPanel3.SetColumnSpan(check_is_unpack_meta, 2);
            check_is_unpack_meta.Dock = System.Windows.Forms.DockStyle.Fill;
            check_is_unpack_meta.Location = new System.Drawing.Point(3, 53);
            check_is_unpack_meta.Name = "check_is_unpack_meta";
            check_is_unpack_meta.Size = new System.Drawing.Size(768, 143);
            check_is_unpack_meta.TabIndex = 4;
            check_is_unpack_meta.Text = "is unpack meta";
            check_is_unpack_meta.UseVisualStyleBackColor = true;
            // 
            // lbl_unpack
            // 
            lbl_unpack.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lbl_unpack.Location = new System.Drawing.Point(112, 3);
            lbl_unpack.Multiline = false;
            lbl_unpack.Name = "lbl_unpack";
            lbl_unpack.Size = new System.Drawing.Size(659, 44);
            lbl_unpack.TabIndex = 3;
            lbl_unpack.Text = "";
            // 
            // btn_unitypackage
            // 
            btn_unitypackage.Dock = System.Windows.Forms.DockStyle.Fill;
            btn_unitypackage.Location = new System.Drawing.Point(5, 5);
            btn_unitypackage.Margin = new System.Windows.Forms.Padding(5);
            btn_unitypackage.Name = "btn_unitypackage";
            btn_unitypackage.Size = new System.Drawing.Size(99, 40);
            btn_unitypackage.TabIndex = 2;
            btn_unitypackage.Text = ".unitypackage";
            btn_unitypackage.UseVisualStyleBackColor = true;
            btn_unitypackage.Click += btn_unitypackage_Click;
            // 
            // tab_Pack
            // 
            tab_Pack.Controls.Add(tableLayoutPanel4);
            tab_Pack.Location = new System.Drawing.Point(4, 24);
            tab_Pack.Name = "tab_Pack";
            tab_Pack.Padding = new System.Windows.Forms.Padding(3);
            tab_Pack.Size = new System.Drawing.Size(786, 416);
            tab_Pack.TabIndex = 1;
            tab_Pack.Text = "Pack";
            tab_Pack.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 0, 0);
            tableLayoutPanel4.Controls.Add(btn_pack, 0, 1);
            tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.9268293F));
            tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.07317F));
            tableLayoutPanel4.Size = new System.Drawing.Size(780, 410);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel5.Controls.Add(btn_folder, 0, 0);
            tableLayoutPanel5.Controls.Add(lbl_pack, 1, 0);
            tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel5.Size = new System.Drawing.Size(774, 47);
            tableLayoutPanel5.TabIndex = 0;
            // 
            // btn_folder
            // 
            btn_folder.Dock = System.Windows.Forms.DockStyle.Fill;
            btn_folder.Location = new System.Drawing.Point(3, 3);
            btn_folder.Name = "btn_folder";
            btn_folder.Size = new System.Drawing.Size(75, 44);
            btn_folder.TabIndex = 0;
            btn_folder.Text = "folder";
            btn_folder.UseVisualStyleBackColor = true;
            btn_folder.Click += btn_folder_Click;
            // 
            // lbl_pack
            // 
            lbl_pack.Dock = System.Windows.Forms.DockStyle.Fill;
            lbl_pack.Location = new System.Drawing.Point(84, 3);
            lbl_pack.Multiline = false;
            lbl_pack.Name = "lbl_pack";
            lbl_pack.Size = new System.Drawing.Size(687, 44);
            lbl_pack.TabIndex = 1;
            lbl_pack.Text = "";
            // 
            // btn_pack
            // 
            btn_pack.Dock = System.Windows.Forms.DockStyle.Fill;
            btn_pack.Location = new System.Drawing.Point(3, 56);
            btn_pack.Name = "btn_pack";
            btn_pack.Size = new System.Drawing.Size(774, 351);
            btn_pack.TabIndex = 1;
            btn_pack.Text = "Pack";
            btn_pack.UseVisualStyleBackColor = true;
            btn_pack.Click += btn_pack_Click;
            // 
            // tab_Info
            // 
            tab_Info.Controls.Add(lbl_info);
            tab_Info.Location = new System.Drawing.Point(4, 24);
            tab_Info.Name = "tab_Info";
            tab_Info.Padding = new System.Windows.Forms.Padding(3);
            tab_Info.Size = new System.Drawing.Size(786, 416);
            tab_Info.TabIndex = 2;
            tab_Info.Text = "Info";
            tab_Info.UseVisualStyleBackColor = true;
            // 
            // lbl_info
            // 
            lbl_info.Dock = System.Windows.Forms.DockStyle.Fill;
            lbl_info.Location = new System.Drawing.Point(3, 3);
            lbl_info.Name = "lbl_info";
            lbl_info.ReadOnly = true;
            lbl_info.Size = new System.Drawing.Size(780, 410);
            lbl_info.TabIndex = 0;
            lbl_info.Text = "";
            lbl_info.LinkClicked += richTextBox1_LinkClicked;
            // 
            // UnityPackageGUI_Winform
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "UnityPackageGUI_Winform";
            Text = "UnityPackageGUI_Winform";
            tableLayoutPanel1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tab_Unpack.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tab_Pack.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tab_Info.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_Unpack;
        private System.Windows.Forms.TabPage tab_Pack;
        private System.Windows.Forms.TabPage tab_Info;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btn_unpack;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.RichTextBox lbl_unpack;
        private System.Windows.Forms.Button btn_unitypackage;
        private System.Windows.Forms.RichTextBox lbl_info;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btn_folder;
        private System.Windows.Forms.RichTextBox lbl_pack;
        private System.Windows.Forms.Button btn_pack;
        private System.Windows.Forms.CheckBox check_is_unpack_meta;
    }
}