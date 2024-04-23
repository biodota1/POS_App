namespace OOP2_POS
{
    partial class EditItem
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
            this.label6 = new System.Windows.Forms.Label();
            this.doneEditButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.editItemQuantity = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.editItemPrice = new System.Windows.Forms.TextBox();
            this.editItemName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(17, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 17);
            this.label6.TabIndex = 25;
            this.label6.Text = "Item Name / Description";
            // 
            // doneEditButton
            // 
            this.doneEditButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doneEditButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.doneEditButton.Location = new System.Drawing.Point(20, 312);
            this.doneEditButton.Name = "doneEditButton";
            this.doneEditButton.Size = new System.Drawing.Size(227, 48);
            this.doneEditButton.TabIndex = 24;
            this.doneEditButton.Text = "DONE";
            this.doneEditButton.UseVisualStyleBackColor = true;
            this.doneEditButton.Click += new System.EventHandler(this.doneEditButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(67, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 25);
            this.label5.TabIndex = 23;
            this.label5.Text = "EDIT  ITEM";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 22;
            this.label3.Text = "Category";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "Quantity";
            // 
            // editItemQuantity
            // 
            this.editItemQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editItemQuantity.Location = new System.Drawing.Point(20, 190);
            this.editItemQuantity.Name = "editItemQuantity";
            this.editItemQuantity.Size = new System.Drawing.Size(227, 23);
            this.editItemQuantity.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 19;
            this.label1.Text = "Price";
            // 
            // editItemPrice
            // 
            this.editItemPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editItemPrice.Location = new System.Drawing.Point(20, 130);
            this.editItemPrice.Name = "editItemPrice";
            this.editItemPrice.Size = new System.Drawing.Size(227, 23);
            this.editItemPrice.TabIndex = 18;
            // 
            // editItemName
            // 
            this.editItemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editItemName.Location = new System.Drawing.Point(20, 70);
            this.editItemName.Name = "editItemName";
            this.editItemName.Size = new System.Drawing.Size(227, 23);
            this.editItemName.TabIndex = 17;
            this.editItemName.TextChanged += new System.EventHandler(this.editItemName_TextChanged);
            // 
            // EditItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(75)))), ((int)(((byte)(200)))));
            this.ClientSize = new System.Drawing.Size(274, 398);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.doneEditButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.editItemQuantity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editItemPrice);
            this.Controls.Add(this.editItemName);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "EditItem";
            this.Text = "EditItem";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button doneEditButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox editItemQuantity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox editItemPrice;
        private System.Windows.Forms.TextBox editItemName;
    }
}