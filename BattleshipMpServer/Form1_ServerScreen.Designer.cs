namespace BattleshipMp
{
    partial class Form1_ServerScreen
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
            this.textBoxIpAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.buttonServerStart = new System.Windows.Forms.Button();
            this.labelServerState = new System.Windows.Forms.Label();
            this.buttonGoToBoard = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // textBoxIpAddress
            // 
            this.textBoxIpAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxIpAddress.Location = new System.Drawing.Point(155, 44);
            this.textBoxIpAddress.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxIpAddress.Name = "textBoxIpAddress";
            this.textBoxIpAddress.Size = new System.Drawing.Size(148, 30);
            this.textBoxIpAddress.TabIndex = 0;
            this.textBoxIpAddress.Leave += new System.EventHandler(this.textBoxIpAddress_Leave);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Indirizzo IP:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Porta:";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPort.Location = new System.Drawing.Point(155, 108);
            this.textBoxPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(148, 30);
            this.textBoxPort.TabIndex = 1;
            this.textBoxPort.Text = "13001";
            // 
            // buttonServerStart
            // 
            this.buttonServerStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonServerStart.Location = new System.Drawing.Point(29, 170);
            this.buttonServerStart.Name = "buttonServerStart";
            this.buttonServerStart.Size = new System.Drawing.Size(274, 34);
            this.buttonServerStart.TabIndex = 2;
            this.buttonServerStart.Text = "Avvia il SERVER";
            this.buttonServerStart.UseVisualStyleBackColor = true;
            this.buttonServerStart.Click += new System.EventHandler(this.buttonServerStart_Click);
            // 
            // labelServerState
            // 
            this.labelServerState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelServerState.AutoSize = true;
            this.labelServerState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelServerState.Location = new System.Drawing.Point(12, 9);
            this.labelServerState.Name = "labelServerState";
            this.labelServerState.Size = new System.Drawing.Size(183, 20);
            this.labelServerState.TabIndex = 2;
            this.labelServerState.Text = "Aspettando il SERVER.";
            // 
            // buttonGoToBoard
            // 
            this.buttonGoToBoard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGoToBoard.Enabled = false;
            this.buttonGoToBoard.Location = new System.Drawing.Point(29, 219);
            this.buttonGoToBoard.Name = "buttonGoToBoard";
            this.buttonGoToBoard.Size = new System.Drawing.Size(274, 34);
            this.buttonGoToBoard.TabIndex = 3;
            this.buttonGoToBoard.Text = "Continua...";
            this.buttonGoToBoard.UseVisualStyleBackColor = true;
            this.buttonGoToBoard.Click += new System.EventHandler(this.buttonGoToBoard_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1_ServerScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 269);
            this.Controls.Add(this.buttonGoToBoard);
            this.Controls.Add(this.buttonServerStart);
            this.Controls.Add(this.labelServerState);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxIpAddress);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1_ServerScreen";
            this.Text = "Impostazione Server";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_ServerScreen_FormClosed);
            this.Load += new System.EventHandler(this.Form1_ServerScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxIpAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Button buttonServerStart;
        private System.Windows.Forms.Label labelServerState;
        private System.Windows.Forms.Button buttonGoToBoard;
        private System.Windows.Forms.Timer timer1;
    }
}

