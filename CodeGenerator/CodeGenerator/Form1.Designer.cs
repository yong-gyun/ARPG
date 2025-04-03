namespace CodeGenerator
{
    partial class Form1
    {
        private Panel panel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private CheckedListBox excelListViewer;
        private CheckBox serverJsonCheckBox;
        private CheckBox clientJsonCheckBox;
        private Button showListButton;
        private Button buildButton;
        private Button generatePacketButton;
        private TextBox excelInputPathBox;
        private TextBox clientJsonOutputPathBox;
        private TextBox serverJsonOutputPathBox;
        private TextBox clientPacketOutputPathBox;
        private TextBox clientSourceOutputPathBox;
        private TextBox serverPacketOutputPathBox;
        private TextBox serverSourceOutputPathBox;

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
            panel1 = new Panel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            excelInputPathBox = new TextBox();
            clientJsonOutputPathBox = new TextBox();
            clientPacketOutputPathBox = new TextBox();
            clientSourceOutputPathBox = new TextBox();
            clientJsonCheckBox = new CheckBox();
            serverPacketOutputPathBox = new TextBox();
            serverSourceOutputPathBox = new TextBox();
            serverJsonCheckBox = new CheckBox();
            serverJsonOutputPathBox = new TextBox();
            excelListViewer = new CheckedListBox();
            showListButton = new Button();
            buildButton = new Button();
            generatePacketButton = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(excelInputPathBox);
            panel1.Controls.Add(clientJsonOutputPathBox);
            panel1.Controls.Add(clientPacketOutputPathBox);
            panel1.Controls.Add(clientSourceOutputPathBox);
            panel1.Controls.Add(clientJsonCheckBox);
            panel1.Controls.Add(serverPacketOutputPathBox);
            panel1.Controls.Add(serverSourceOutputPathBox);
            panel1.Controls.Add(serverJsonCheckBox);
            panel1.Controls.Add(serverJsonOutputPathBox);
            panel1.Location = new Point(12, 8);
            panel1.Name = "panel1";
            panel1.Size = new Size(776, 478);
            panel1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 20);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 1;
            label1.Text = "엑셀 경로";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 80);
            label2.Name = "label2";
            label2.Size = new Size(93, 15);
            label2.TabIndex = 3;
            label2.Text = "클라이언트 json";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 138);
            label3.Name = "label3";
            label3.Size = new Size(135, 15);
            label3.TabIndex = 6;
            label3.Text = "클라이언트 데이터 소스";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 200);
            label4.Name = "label4";
            label4.Size = new Size(95, 15);
            label4.TabIndex = 9;
            label4.Text = "클라이언트 패킷";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(16, 267);
            label5.Name = "label5";
            label5.Size = new Size(57, 15);
            label5.TabIndex = 12;
            label5.Text = "서버 json";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(16, 338);
            label6.Name = "label6";
            label6.Size = new Size(99, 15);
            label6.TabIndex = 15;
            label6.Text = "서버 데이터 소스";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(16, 404);
            label7.Name = "label7";
            label7.Size = new Size(59, 15);
            label7.TabIndex = 18;
            label7.Text = "서버 패킷";
            // 
            // excelInputPathBox
            // 
            excelInputPathBox.Location = new Point(16, 40);
            excelInputPathBox.Name = "excelInputPathBox";
            excelInputPathBox.Size = new Size(708, 23);
            excelInputPathBox.TabIndex = 0;
            excelInputPathBox.TextChanged += OnExcelTextBoxEvent;
            // 
            // clientJsonOutputPathBox
            // 
            clientJsonOutputPathBox.Location = new Point(16, 100);
            clientJsonOutputPathBox.Name = "clientJsonOutputPathBox";
            clientJsonOutputPathBox.Size = new Size(708, 23);
            clientJsonOutputPathBox.TabIndex = 2;
            clientJsonOutputPathBox.TextChanged += OnClientJsonOutputPathBoxEvent;
            // 
            // clientPacketOutputPathBox
            // 
            clientPacketOutputPathBox.Location = new Point(16, 220);
            clientPacketOutputPathBox.Name = "clientPacketOutputPathBox";
            clientPacketOutputPathBox.Size = new Size(708, 23);
            clientPacketOutputPathBox.TabIndex = 8;
            clientPacketOutputPathBox.TextChanged += OnClientPacketOutputPathBoxEvent;
            // 
            // clientSourceOutputPathBox
            // 
            clientSourceOutputPathBox.Location = new Point(16, 158);
            clientSourceOutputPathBox.Name = "clientSourceOutputPathBox";
            clientSourceOutputPathBox.Size = new Size(708, 23);
            clientSourceOutputPathBox.TabIndex = 5;
            clientSourceOutputPathBox.TextChanged += OnClientSourceOutputPathBoxEvent;
            // 
            // clientJsonCheckBox
            // 
            clientJsonCheckBox.AutoSize = true;
            clientJsonCheckBox.Location = new Point(730, 106);
            clientJsonCheckBox.Name = "clientJsonCheckBox";
            clientJsonCheckBox.Size = new Size(15, 14);
            clientJsonCheckBox.TabIndex = 4;
            clientJsonCheckBox.UseVisualStyleBackColor = true;
            clientJsonCheckBox.CheckedChanged += OnClientJsonCheckBoxEvent;
            // 
            // serverPacketOutputPathBox
            // 
            serverPacketOutputPathBox.Location = new Point(16, 424);
            serverPacketOutputPathBox.Name = "serverPacketOutputPathBox";
            serverPacketOutputPathBox.Size = new Size(708, 23);
            serverPacketOutputPathBox.TabIndex = 17;
            serverPacketOutputPathBox.TextChanged += OnServerPacketOutputPathBoxEvent;
            // 
            // serverSourceOutputPathBox
            // 
            serverSourceOutputPathBox.Location = new Point(16, 358);
            serverSourceOutputPathBox.Name = "serverSourceOutputPathBox";
            serverSourceOutputPathBox.Size = new Size(708, 23);
            serverSourceOutputPathBox.TabIndex = 14;
            serverSourceOutputPathBox.TextChanged += OnServerSourceOutputPathBoxEvent;
            // 
            // serverJsonCheckBox
            // 
            serverJsonCheckBox.AutoSize = true;
            serverJsonCheckBox.Location = new Point(730, 304);
            serverJsonCheckBox.Name = "serverJsonCheckBox";
            serverJsonCheckBox.Size = new Size(15, 14);
            serverJsonCheckBox.TabIndex = 13;
            serverJsonCheckBox.UseVisualStyleBackColor = true;
            serverJsonCheckBox.CheckedChanged += OnServerJsonCheckBoxEvent;
            // 
            // serverJsonOutputPathBox
            // 
            serverJsonOutputPathBox.Location = new Point(16, 287);
            serverJsonOutputPathBox.Name = "serverJsonOutputPathBox";
            serverJsonOutputPathBox.Size = new Size(708, 23);
            serverJsonOutputPathBox.TabIndex = 11;
            serverJsonOutputPathBox.TextChanged += OnServerJsonOutputPathBoxEvent;
            // 
            // excelListViewer
            // 
            excelListViewer.FormattingEnabled = true;
            excelListViewer.Location = new Point(794, 8);
            excelListViewer.Margin = new Padding(10, 3, 3, 3);
            excelListViewer.Name = "excelListViewer";
            excelListViewer.Size = new Size(192, 382);
            excelListViewer.TabIndex = 3;
            excelListViewer.ItemCheck += OnClickExcelListViewerSelectedIndexChanged;
            // 
            // showListButton
            // 
            showListButton.Location = new Point(794, 396);
            showListButton.Name = "showListButton";
            showListButton.Size = new Size(192, 31);
            showListButton.TabIndex = 5;
            showListButton.Text = "Show list";
            showListButton.UseVisualStyleBackColor = true;
            showListButton.Click += OnClickShowExcelListButton;
            // 
            // buildButton
            // 
            buildButton.Location = new Point(794, 440);
            buildButton.Name = "buildButton";
            buildButton.Size = new Size(95, 46);
            buildButton.TabIndex = 1;
            buildButton.Text = "Build";
            buildButton.UseVisualStyleBackColor = true;
            buildButton.Click += OnClickBuildButton;
            // 
            // generatePacketButton
            // 
            generatePacketButton.Location = new Point(891, 440);
            generatePacketButton.Name = "generatePacketButton";
            generatePacketButton.Size = new Size(95, 46);
            generatePacketButton.TabIndex = 4;
            generatePacketButton.Text = "패킷 생성";
            generatePacketButton.UseVisualStyleBackColor = true;
            generatePacketButton.Click += OnClickGeneratePacketButton;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1006, 503);
            Controls.Add(showListButton);
            Controls.Add(generatePacketButton);
            Controls.Add(excelListViewer);
            Controls.Add(panel1);
            Controls.Add(buildButton);
            Name = "Form1";
            Text = "Code Generator";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
