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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_drag = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_drag);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 1;
            // 
            // txt_drag
            // 
            this.txt_drag.AllowDrop = true;
            this.txt_drag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_drag.Location = new System.Drawing.Point(0, 0);
            this.txt_drag.Name = "txt_drag";
            this.txt_drag.Size = new System.Drawing.Size(800, 450);
            this.txt_drag.TabIndex = 0;
            this.txt_drag.Text = "Drag Here(Unpack Same Directory)";
            this.txt_drag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txt_drag.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnTxDrag_DragDrop);
            this.txt_drag.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnTxtDrag_DragEnter);
            // 
            // UnityUnpackGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "UnityUnpackGUI";
            this.Text = "UnityUnpackGUI";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label txt_drag;
}
