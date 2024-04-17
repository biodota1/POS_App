namespace OOP2_POS
{
    partial class AddItem
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
            this.addItemName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.addItemPrice = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.addItemQuantity = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.addItemCategory = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.addItemToDbButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // addItemName
            // 
            this.addItemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addItemName.Location = new System.Drawing.Point(23, 70);
            this.addItemName.Name = "addItemName";
            this.addItemName.Size = new System.Drawing.Size(227, 23);
            this.addItemName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Price";
            // 
            // addItemPrice
            // 
            this.addItemPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addItemPrice.Location = new System.Drawing.Point(23, 130);
            this.addItemPrice.Name = "addItemPrice";
            this.addItemPrice.Size = new System.Drawing.Size(227, 23);
            this.addItemPrice.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Quantity";
            // 
            // addItemQuantity
            // 
            this.addItemQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addItemQuantity.Location = new System.Drawing.Point(23, 190);
            this.addItemQuantity.Name = "addItemQuantity";
            this.addItemQuantity.Size = new System.Drawing.Size(227, 23);
            this.addItemQuantity.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Category";
            // 
            // addItemCategory
            // 
            this.addItemCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addItemCategory.Location = new System.Drawing.Point(23, 250);
            this.addItemCategory.Name = "addItemCategory";
            this.addItemCategory.Size = new System.Drawing.Size(227, 23);
            this.addItemCategory.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(70, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 25);
            this.label5.TabIndex = 14;
            this.label5.Text = "ADD ITEM";
            // 
            // addItemToDbButton
            // 
            this.addItemToDbButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addItemToDbButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.addItemToDbButton.Location = new System.Drawing.Point(23, 312);
            this.addItemToDbButton.Name = "addItemToDbButton";
            this.addItemToDbButton.Size = new System.Drawing.Size(227, 48);
            this.addItemToDbButton.TabIndex = 15;
            this.addItemToDbButton.Text = "ADD";
            this.addItemToDbButton.UseVisualStyleBackColor = true;
            this.addItemToDbButton.Click += new System.EventHandler(this.addItemToDbButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(20, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 17);
            this.label6.TabIndex = 16;
            this.label6.Text = "Item Name / Description";
            // 
            // AddItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(274, 398);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.addItemToDbButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.addItemCategory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.addItemQuantity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addItemPrice);
            this.Controls.Add(this.addItemName);
            this.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.Name = "AddItem";
            this.Text = "AddItem";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox addItemName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox addItemPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox addItemQuantity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox addItemCategory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button addItemToDbButton;
        private System.Windows.Forms.Label label6;
    }
}