namespace UnityUnpackGUI;

partial class UnityUnpackGUI
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.txt_drag = new System.Windows.Forms.Label();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.check_include_meta = new System.Windows.Forms.CheckBox();
            this.tableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_drag
            // 
            this.txt_drag.AllowDrop = true;
            this.txt_drag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_drag.Location = new System.Drawing.Point(3, 0);
            this.txt_drag.Name = "txt_drag";
            this.txt_drag.Size = new System.Drawing.Size(794, 409);
            this.txt_drag.TabIndex = 0;
            this.txt_drag.Text = "Drag Here(Unpack Same Directory)";
            this.txt_drag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txt_drag.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnTxDrag_DragDrop);
            this.txt_drag.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnTxtDrag_DragEnter);
            // 
            // check_include_meta
            // 
            this.tableLayout.ColumnCount = 1;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayout.Controls.Add(this.txt_drag, 0, 0);
            this.tableLayout.Controls.Add(this.check_include_meta, 0, 1);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayout.Name = "check_include_meta";
            this.tableLayout.RowCount = 2;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.88889F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.111111F));
            this.tableLayout.Size = new System.Drawing.Size(800, 450);
            this.tableLayout.TabIndex = 2;
            // 
            // checkBox1
            // 
            this.check_include_meta.Checked = true;
            this.check_include_meta.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_include_meta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.check_include_meta.Location = new System.Drawing.Point(3, 412);
            this.check_include_meta.Name = "checkBox1";
            this.check_include_meta.Size = new System.Drawing.Size(794, 35);
            this.check_include_meta.TabIndex = 1;
            this.check_include_meta.Text = "include .meta";
            this.check_include_meta.UseVisualStyleBackColor = true;
            // 
            // UnityUnpackGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayout);
            this.Name = "UnityUnpackGUI";
            this.Text = "UnityUnpackGUI";
            this.tableLayout.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Label txt_drag;
    private System.Windows.Forms.TableLayoutPanel tableLayout;
    private System.Windows.Forms.CheckBox check_include_meta;
}
