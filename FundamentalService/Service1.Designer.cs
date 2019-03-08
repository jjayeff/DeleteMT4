namespace FundamentalService
{
    partial class Service1
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.timer1 = new System.Timers.Timer();
            this.timer2 = new System.Timers.Timer();
            this.timer3 = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer3)).BeginInit();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000D;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 3600000D;
            this.timer2.Elapsed += new System.Timers.ElapsedEventHandler(this.ResetAccessToken);
            // 
            // timer3
            // 
            this.timer3.Enabled = true;
            this.timer3.Interval = 300000D;
            this.timer3.Elapsed += new System.Timers.ElapsedEventHandler(this.Kaohoon2Database);
            // 
            // Service1
            // 
            this.ServiceName = "Service1";
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer3)).EndInit();

        }

        #endregion

        private System.Timers.Timer timer1;
        private System.Timers.Timer timer2;
        private System.Timers.Timer timer3;
    }
}
