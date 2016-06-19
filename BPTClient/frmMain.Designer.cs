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
            this.toolTopBarStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTipBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.listBoxConnectedPlayers = new System.Windows.Forms.ListBox();
            this.lblConnectedPlayers = new System.Windows.Forms.Label();
            this.listBoxTables = new System.Windows.Forms.ListBox();
            this.lblTables = new System.Windows.Forms.Label();
            this.btnNewTable = new System.Windows.Forms.Button();
            this.rbTwoSeats = new System.Windows.Forms.RadioButton();
            this.rbSixSeats = new System.Windows.Forms.RadioButton();
            this.rbNineSeats = new System.Windows.Forms.RadioButton();
            this.grpBoxNewTable = new System.Windows.Forms.GroupBox();
            this.tbLobbyChat = new System.Windows.Forms.TextBox();
            this.tbLobbyChatInput = new System.Windows.Forms.TextBox();
            this.btnSendChat = new System.Windows.Forms.Button();
            this.lblTableInfo = new System.Windows.Forms.Label();
            this.menuGame.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.grpBoxNewTable.SuspendLayout();
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
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolTopBarStatus,
            this.toolTipBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 352);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(499, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolTopBarStatus
            // 
            this.toolTopBarStatus.Name = "toolTopBarStatus";
            this.toolTopBarStatus.Size = new System.Drawing.Size(42, 17);
            this.toolTopBarStatus.Text = "Status:";
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
            this.listBoxConnectedPlayers.Location = new System.Drawing.Point(21, 56);
            this.listBoxConnectedPlayers.Name = "listBoxConnectedPlayers";
            this.listBoxConnectedPlayers.Size = new System.Drawing.Size(100, 108);
            this.listBoxConnectedPlayers.TabIndex = 3;
            // 
            // lblConnectedPlayers
            // 
            this.lblConnectedPlayers.AutoSize = true;
            this.lblConnectedPlayers.Location = new System.Drawing.Point(21, 37);
            this.lblConnectedPlayers.Name = "lblConnectedPlayers";
            this.lblConnectedPlayers.Size = new System.Drawing.Size(95, 13);
            this.lblConnectedPlayers.TabIndex = 4;
            this.lblConnectedPlayers.Text = "Connected players";
            // 
            // listBoxTables
            // 
            this.listBoxTables.FormattingEnabled = true;
            this.listBoxTables.Location = new System.Drawing.Point(148, 56);
            this.listBoxTables.Name = "listBoxTables";
            this.listBoxTables.Size = new System.Drawing.Size(100, 108);
            this.listBoxTables.TabIndex = 5;
            this.listBoxTables.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBoxTables_MouseClick);
            this.listBoxTables.DoubleClick += new System.EventHandler(this.listBoxLobbys_DoubleClick);
            // 
            // lblTables
            // 
            this.lblTables.AutoSize = true;
            this.lblTables.Location = new System.Drawing.Point(148, 37);
            this.lblTables.Name = "lblTables";
            this.lblTables.Size = new System.Drawing.Size(39, 13);
            this.lblTables.TabIndex = 6;
            this.lblTables.Text = "Tables";
            // 
            // btnNewTable
            // 
            this.btnNewTable.Location = new System.Drawing.Point(87, 64);
            this.btnNewTable.Name = "btnNewTable";
            this.btnNewTable.Size = new System.Drawing.Size(97, 23);
            this.btnNewTable.TabIndex = 7;
            this.btnNewTable.Text = "New table";
            this.btnNewTable.UseVisualStyleBackColor = true;
            this.btnNewTable.Click += new System.EventHandler(this.btnNewLobby_Click);
            // 
            // rbTwoSeats
            // 
            this.rbTwoSeats.AutoSize = true;
            this.rbTwoSeats.Checked = true;
            this.rbTwoSeats.Location = new System.Drawing.Point(6, 19);
            this.rbTwoSeats.Name = "rbTwoSeats";
            this.rbTwoSeats.Size = new System.Drawing.Size(61, 17);
            this.rbTwoSeats.TabIndex = 8;
            this.rbTwoSeats.TabStop = true;
            this.rbTwoSeats.Text = "2 Seats";
            this.rbTwoSeats.UseVisualStyleBackColor = true;
            // 
            // rbSixSeats
            // 
            this.rbSixSeats.AutoSize = true;
            this.rbSixSeats.Location = new System.Drawing.Point(6, 43);
            this.rbSixSeats.Name = "rbSixSeats";
            this.rbSixSeats.Size = new System.Drawing.Size(61, 17);
            this.rbSixSeats.TabIndex = 9;
            this.rbSixSeats.Text = "6 Seats";
            this.rbSixSeats.UseVisualStyleBackColor = true;
            // 
            // rbNineSeats
            // 
            this.rbNineSeats.AutoSize = true;
            this.rbNineSeats.Location = new System.Drawing.Point(6, 67);
            this.rbNineSeats.Name = "rbNineSeats";
            this.rbNineSeats.Size = new System.Drawing.Size(61, 17);
            this.rbNineSeats.TabIndex = 10;
            this.rbNineSeats.Text = "9 Seats";
            this.rbNineSeats.UseVisualStyleBackColor = true;
            // 
            // grpBoxNewTable
            // 
            this.grpBoxNewTable.Controls.Add(this.rbTwoSeats);
            this.grpBoxNewTable.Controls.Add(this.btnNewTable);
            this.grpBoxNewTable.Controls.Add(this.rbNineSeats);
            this.grpBoxNewTable.Controls.Add(this.rbSixSeats);
            this.grpBoxNewTable.Location = new System.Drawing.Point(278, 69);
            this.grpBoxNewTable.Name = "grpBoxNewTable";
            this.grpBoxNewTable.Size = new System.Drawing.Size(190, 100);
            this.grpBoxNewTable.TabIndex = 11;
            this.grpBoxNewTable.TabStop = false;
            this.grpBoxNewTable.Text = "Create Table";
            // 
            // tbLobbyChat
            // 
            this.tbLobbyChat.BackColor = System.Drawing.SystemColors.Window;
            this.tbLobbyChat.Location = new System.Drawing.Point(21, 211);
            this.tbLobbyChat.Multiline = true;
            this.tbLobbyChat.Name = "tbLobbyChat";
            this.tbLobbyChat.ReadOnly = true;
            this.tbLobbyChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLobbyChat.Size = new System.Drawing.Size(227, 111);
            this.tbLobbyChat.TabIndex = 12;
            // 
            // tbLobbyChatInput
            // 
            this.tbLobbyChatInput.Location = new System.Drawing.Point(21, 322);
            this.tbLobbyChatInput.Name = "tbLobbyChatInput";
            this.tbLobbyChatInput.Size = new System.Drawing.Size(227, 20);
            this.tbLobbyChatInput.TabIndex = 13;
            // 
            // btnSendChat
            // 
            this.btnSendChat.Location = new System.Drawing.Point(254, 322);
            this.btnSendChat.Name = "btnSendChat";
            this.btnSendChat.Size = new System.Drawing.Size(75, 23);
            this.btnSendChat.TabIndex = 14;
            this.btnSendChat.Text = "Send";
            this.btnSendChat.UseVisualStyleBackColor = true;
            this.btnSendChat.Click += new System.EventHandler(this.btnSendChat_Click);
            // 
            // lblTableInfo
            // 
            this.lblTableInfo.AutoSize = true;
            this.lblTableInfo.Location = new System.Drawing.Point(148, 181);
            this.lblTableInfo.Name = "lblTableInfo";
            this.lblTableInfo.Size = new System.Drawing.Size(0, 13);
            this.lblTableInfo.TabIndex = 15;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 374);
            this.Controls.Add(this.lblTableInfo);
            this.Controls.Add(this.btnSendChat);
            this.Controls.Add(this.tbLobbyChatInput);
            this.Controls.Add(this.tbLobbyChat);
            this.Controls.Add(this.grpBoxNewTable);
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
            this.grpBoxNewTable.ResumeLayout(false);
            this.grpBoxNewTable.PerformLayout();
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
        private System.Windows.Forms.Button btnNewTable;
        public System.Windows.Forms.ToolStripStatusLabel toolTipBar;
        private System.Windows.Forms.ToolStripStatusLabel toolTopBarStatus;
        private System.Windows.Forms.RadioButton rbTwoSeats;
        private System.Windows.Forms.RadioButton rbSixSeats;
        private System.Windows.Forms.RadioButton rbNineSeats;
        private System.Windows.Forms.GroupBox grpBoxNewTable;
        private System.Windows.Forms.TextBox tbLobbyChat;
        private System.Windows.Forms.TextBox tbLobbyChatInput;
        private System.Windows.Forms.Button btnSendChat;
        private System.Windows.Forms.Label lblTableInfo;
    }
}