namespace BPTClient
{
    partial class frmMain
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
            this.menuGame = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hostGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joinGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolTipBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.listBoxConnectedPlayers = new System.Windows.Forms.ListBox();
            this.lblConnectedPlayers = new System.Windows.Forms.Label();
            this.listBoxTables = new System.Windows.Forms.ListBox();
            this.lblTables = new System.Windows.Forms.Label();
            this.btnNewLobby = new System.Windows.Forms.Button();
            this.toolTopBarStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuGame.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuGame
            // 
            this.menuGame.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuGame.Location = new System.Drawing.Point(0, 0);
            this.menuGame.Name = "menuGame";
            this.menuGame.Size = new System.Drawing.Size(499, 24);
            this.menuGame.TabIndex = 1;
            this.menuGame.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hostGameToolStripMenuItem,
            this.joinGameToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // hostGameToolStripMenuItem
            // 
            this.hostGameToolStripMenuItem.Name = "hostGameToolStripMenuItem";
            this.hostGameToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.hostGameToolStripMenuItem.Text = "Connect to server";
            this.hostGameToolStripMenuItem.Click += new System.EventHandler(this.hostGameToolStripMenuItem_Click);
            // 
            // joinGameToolStripMenuItem
            // 
            this.joinGameToolStripMenuItem.Name = "joinGameToolStripMenuItem";
            this.joinGameToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.joinGameToolStripMenuItem.Text = "Options";
            this.joinGameToolStripMenuItem.Click += new System.EventHandler(this.joinGameToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.optionsToolStripMenuItem.Text = "Quit";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolTopBarStatus,
            this.toolTipBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 273);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(499, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolTipBar
            // 
            this.toolTipBar.ForeColor = System.Drawing.Color.Maroon;
            this.toolTipBar.Name = "toolTipBar";
            this.toolTipBar.Size = new System.Drawing.Size(77, 17);
            this.toolTipBar.Text = "Not connected";
            this.toolTipBar.TextChanged += new System.EventHandler(this.toolTipBar_TextChanged);
            // 
            // listBoxConnectedPlayers
            // 
            this.listBoxConnectedPlayers.FormattingEnabled = true;
            this.listBoxConnectedPlayers.Location = new System.Drawing.Point(87, 70);
            this.listBoxConnectedPlayers.Name = "listBoxConnectedPlayers";
            this.listBoxConnectedPlayers.Size = new System.Drawing.Size(100, 134);
            this.listBoxConnectedPlayers.TabIndex = 3;
            // 
            // lblConnectedPlayers
            // 
            this.lblConnectedPlayers.AutoSize = true;
            this.lblConnectedPlayers.Location = new System.Drawing.Point(87, 51);
            this.lblConnectedPlayers.Name = "lblConnectedPlayers";
            this.lblConnectedPlayers.Size = new System.Drawing.Size(95, 13);
            this.lblConnectedPlayers.TabIndex = 4;
            this.lblConnectedPlayers.Text = "Connected players";
            // 
            // listBoxTables
            // 
            this.listBoxTables.FormattingEnabled = true;
            this.listBoxTables.Location = new System.Drawing.Point(214, 70);
            this.listBoxTables.Name = "listBoxTables";
            this.listBoxTables.Size = new System.Drawing.Size(100, 134);
            this.listBoxTables.TabIndex = 5;
            this.listBoxTables.DoubleClick += new System.EventHandler(this.listBoxLobbys_DoubleClick);
            // 
            // lblTables
            // 
            this.lblTables.AutoSize = true;
            this.lblTables.Location = new System.Drawing.Point(214, 51);
            this.lblTables.Name = "lblTables";
            this.lblTables.Size = new System.Drawing.Size(39, 13);
            this.lblTables.TabIndex = 6;
            this.lblTables.Text = "Tables";
            // 
            // btnNewLobby
            // 
            this.btnNewLobby.Location = new System.Drawing.Point(217, 210);
            this.btnNewLobby.Name = "btnNewLobby";
            this.btnNewLobby.Size = new System.Drawing.Size(97, 23);
            this.btnNewLobby.TabIndex = 7;
            this.btnNewLobby.Text = "Create new lobby";
            this.btnNewLobby.UseVisualStyleBackColor = true;
            this.btnNewLobby.Click += new System.EventHandler(this.btnNewLobby_Click);
            // 
            // toolTopBarStatus
            // 
            this.toolTopBarStatus.Name = "toolTopBarStatus";
            this.toolTopBarStatus.Size = new System.Drawing.Size(42, 17);
            this.toolTopBarStatus.Text = "Status:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 295);
            this.Controls.Add(this.btnNewLobby);
            this.Controls.Add(this.lblTables);
            this.Controls.Add(this.listBoxTables);
            this.Controls.Add(this.lblConnectedPlayers);
            this.Controls.Add(this.listBoxConnectedPlayers);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuGame);
            this.MainMenuStrip = this.menuGame;
            this.Name = "frmMain";
            this.Text = "Bankpoker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuGame.ResumeLayout(false);
            this.menuGame.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuGame;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hostGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem joinGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ListBox listBoxConnectedPlayers;
        private System.Windows.Forms.Label lblConnectedPlayers;
        private System.Windows.Forms.ListBox listBoxTables;
        private System.Windows.Forms.Label lblTables;
        private System.Windows.Forms.Button btnNewLobby;
        public System.Windows.Forms.ToolStripStatusLabel toolTipBar;
        private System.Windows.Forms.ToolStripStatusLabel toolTopBarStatus;
    }
}