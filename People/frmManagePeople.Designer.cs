namespace DVLD_project
{
    partial class frmManagePeople
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvPeopleList = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowDetailsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.AddNewPersonMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditPersonMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeletePersonMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.SendEmailMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PhoneCallMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNumberOfPeople = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeopleList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(487, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Manage People";
            // 
            // dgvPeopleList
            // 
            this.dgvPeopleList.AllowUserToAddRows = false;
            this.dgvPeopleList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPeopleList.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvPeopleList.Location = new System.Drawing.Point(26, 257);
            this.dgvPeopleList.Name = "dgvPeopleList";
            this.dgvPeopleList.RowHeadersWidth = 51;
            this.dgvPeopleList.RowTemplate.Height = 24;
            this.dgvPeopleList.Size = new System.Drawing.Size(1248, 329);
            this.dgvPeopleList.TabIndex = 2;
            this.dgvPeopleList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPeopleList_CellMouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowDetailsMenuItem,
            this.toolStripMenuItem1,
            this.AddNewPersonMenuItem,
            this.EditPersonMenuItem,
            this.DeletePersonMenuItem,
            this.toolStripMenuItem2,
            this.SendEmailMenuItem,
            this.PhoneCallMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(192, 172);
            // 
            // ShowDetailsMenuItem
            // 
            this.ShowDetailsMenuItem.Image = global::DVLD_project.Properties.Resources.view_details;
            this.ShowDetailsMenuItem.Name = "ShowDetailsMenuItem";
            this.ShowDetailsMenuItem.Size = new System.Drawing.Size(191, 26);
            this.ShowDetailsMenuItem.Text = "Show Details";
            this.ShowDetailsMenuItem.Click += new System.EventHandler(this.ShowDetailsMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(188, 6);
            // 
            // AddNewPersonMenuItem
            // 
            this.AddNewPersonMenuItem.Image = global::DVLD_project.Properties.Resources.add;
            this.AddNewPersonMenuItem.Name = "AddNewPersonMenuItem";
            this.AddNewPersonMenuItem.Size = new System.Drawing.Size(191, 26);
            this.AddNewPersonMenuItem.Text = "Add New Person";
            this.AddNewPersonMenuItem.Click += new System.EventHandler(this.AddNewPersonMenuItem_Click);
            // 
            // EditPersonMenuItem
            // 
            this.EditPersonMenuItem.Image = global::DVLD_project.Properties.Resources.edit;
            this.EditPersonMenuItem.Name = "EditPersonMenuItem";
            this.EditPersonMenuItem.Size = new System.Drawing.Size(191, 26);
            this.EditPersonMenuItem.Text = "Edit";
            this.EditPersonMenuItem.Click += new System.EventHandler(this.EditPersonMenuItem_Click);
            // 
            // DeletePersonMenuItem
            // 
            this.DeletePersonMenuItem.Image = global::DVLD_project.Properties.Resources.trash_can;
            this.DeletePersonMenuItem.Name = "DeletePersonMenuItem";
            this.DeletePersonMenuItem.Size = new System.Drawing.Size(191, 26);
            this.DeletePersonMenuItem.Text = "Delete";
            this.DeletePersonMenuItem.Click += new System.EventHandler(this.DeletePersonMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(188, 6);
            // 
            // SendEmailMenuItem
            // 
            this.SendEmailMenuItem.Image = global::DVLD_project.Properties.Resources.gmail_ios1;
            this.SendEmailMenuItem.Name = "SendEmailMenuItem";
            this.SendEmailMenuItem.Size = new System.Drawing.Size(191, 26);
            this.SendEmailMenuItem.Text = "Send Email";
            this.SendEmailMenuItem.Click += new System.EventHandler(this.SendEmailMenuItem_Click);
            // 
            // PhoneCallMenuItem
            // 
            this.PhoneCallMenuItem.Image = global::DVLD_project.Properties.Resources.phone1;
            this.PhoneCallMenuItem.Name = "PhoneCallMenuItem";
            this.PhoneCallMenuItem.Size = new System.Drawing.Size(191, 26);
            this.PhoneCallMenuItem.Text = "Phone Call";
            this.PhoneCallMenuItem.Click += new System.EventHandler(this.PhoneCallMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Filter By :";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "Person ID",
            "National No.",
            "Frist Name",
            "Second Name",
            "Third Name",
            "Last Name",
            "Nationality",
            "Gendor",
            "Phone",
            "Email"});
            this.cbFilterBy.Location = new System.Drawing.Point(111, 218);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(121, 24);
            this.cbFilterBy.TabIndex = 4;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Image = global::DVLD_project.Properties.Resources.close;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(1178, 592);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 42);
            this.button1.TabIndex = 6;
            this.button1.Text = "Close";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.Image = global::DVLD_project.Properties.Resources.Add_User;
            this.btnAddPerson.Location = new System.Drawing.Point(1198, 174);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(75, 64);
            this.btnAddPerson.TabIndex = 5;
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_project.Properties.Resources.group;
            this.pictureBox1.Location = new System.Drawing.Point(526, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(178, 142);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 602);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "# Records :";
            // 
            // lblNumberOfPeople
            // 
            this.lblNumberOfPeople.AutoSize = true;
            this.lblNumberOfPeople.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfPeople.Location = new System.Drawing.Point(135, 602);
            this.lblNumberOfPeople.Name = "lblNumberOfPeople";
            this.lblNumberOfPeople.Size = new System.Drawing.Size(51, 20);
            this.lblNumberOfPeople.TabIndex = 8;
            this.lblNumberOfPeople.Text = "[???]";
            // 
            // frmManagePeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 638);
            this.Controls.Add(this.lblNumberOfPeople);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAddPerson);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvPeopleList);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Name = "frmManagePeople";
            this.Text = "Manage People";
            this.Load += new System.EventHandler(this.frmManagePeople_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeopleList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dgvPeopleList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ShowDetailsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem AddNewPersonMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditPersonMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeletePersonMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem SendEmailMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PhoneCallMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNumberOfPeople;
    }
}