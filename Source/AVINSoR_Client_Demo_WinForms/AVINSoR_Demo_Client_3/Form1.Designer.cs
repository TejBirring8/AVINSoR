namespace AVINSoR_Demo_Client_3
{
    partial class Form1
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
            this.variable6 = new AVINSoR_Library.DTControls.Variable();
            this.nxtRobot1 = new AVINSoR_Library.DTControls.NxtRobot();
            this.variable5 = new AVINSoR_Library.DTControls.Variable();
            this.variable4 = new AVINSoR_Library.DTControls.Variable();
            this.variable3 = new AVINSoR_Library.DTControls.Variable();
            this.variable2 = new AVINSoR_Library.DTControls.Variable();
            this.variable1 = new AVINSoR_Library.DTControls.Variable();
            this.SuspendLayout();
            // 
            // variable6
            // 
            this.variable6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.variable6.Location = new System.Drawing.Point(748, 527);
            this.variable6.Name = "variable6";
            this.variable6.Robot = this.nxtRobot1;
            this.variable6.Size = new System.Drawing.Size(357, 132);
            this.variable6.TabIndex = 6;
            this.variable6.VariableIndex = 5;
            // 
            // nxtRobot1
            // 
            this.nxtRobot1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nxtRobot1.Location = new System.Drawing.Point(22, 150);
            this.nxtRobot1.Name = "nxtRobot1";
            this.nxtRobot1.Size = new System.Drawing.Size(787, 370);
            this.nxtRobot1.TabIndex = 0;
            // 
            // variable5
            // 
            this.variable5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.variable5.Location = new System.Drawing.Point(385, 526);
            this.variable5.Name = "variable5";
            this.variable5.Robot = this.nxtRobot1;
            this.variable5.Size = new System.Drawing.Size(357, 132);
            this.variable5.TabIndex = 5;
            this.variable5.VariableIndex = 4;
            // 
            // variable4
            // 
            this.variable4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.variable4.Location = new System.Drawing.Point(22, 527);
            this.variable4.Name = "variable4";
            this.variable4.Robot = this.nxtRobot1;
            this.variable4.Size = new System.Drawing.Size(357, 132);
            this.variable4.TabIndex = 4;
            this.variable4.VariableIndex = 3;
            // 
            // variable3
            // 
            this.variable3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.variable3.Location = new System.Drawing.Point(748, 12);
            this.variable3.Name = "variable3";
            this.variable3.Robot = this.nxtRobot1;
            this.variable3.Size = new System.Drawing.Size(357, 132);
            this.variable3.TabIndex = 3;
            this.variable3.VariableIndex = 2;
            // 
            // variable2
            // 
            this.variable2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.variable2.Location = new System.Drawing.Point(385, 12);
            this.variable2.Name = "variable2";
            this.variable2.Robot = this.nxtRobot1;
            this.variable2.Size = new System.Drawing.Size(357, 132);
            this.variable2.TabIndex = 2;
            this.variable2.VariableIndex = 1;
            // 
            // variable1
            // 
            this.variable1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.variable1.Location = new System.Drawing.Point(22, 12);
            this.variable1.Name = "variable1";
            this.variable1.Robot = this.nxtRobot1;
            this.variable1.Size = new System.Drawing.Size(357, 132);
            this.variable1.TabIndex = 1;
            this.variable1.VariableIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1248, 671);
            this.Controls.Add(this.variable6);
            this.Controls.Add(this.variable5);
            this.Controls.Add(this.variable4);
            this.Controls.Add(this.variable3);
            this.Controls.Add(this.variable2);
            this.Controls.Add(this.variable1);
            this.Controls.Add(this.nxtRobot1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private AVINSoR_Library.DTControls.NxtRobot nxtRobot1;
        private AVINSoR_Library.DTControls.Variable variable1;
        private AVINSoR_Library.DTControls.Variable variable2;
        private AVINSoR_Library.DTControls.Variable variable3;
        private AVINSoR_Library.DTControls.Variable variable4;
        private AVINSoR_Library.DTControls.Variable variable5;
        private AVINSoR_Library.DTControls.Variable variable6;
    }
}

