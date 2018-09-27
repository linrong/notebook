namespace PhotoDataTestDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_Port = new System.Windows.Forms.Label();
            this.lbl_BaudRate = new System.Windows.Forms.Label();
            this.cbo_Port = new System.Windows.Forms.ComboBox();
            this.cbo_BaudRate = new System.Windows.Forms.ComboBox();
            this.btn_SerialPortOperation = new System.Windows.Forms.Button();
            this.btn_BaudRateRefresh = new System.Windows.Forms.Button();
            this.btn_ReadPhotoData = new System.Windows.Forms.Button();
            this.btn_SendPhotoData = new System.Windows.Forms.Button();
            this.txt_TimeStamp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_RefreshTimeStamp = new System.Windows.Forms.Button();
            this.lst_DataShow = new System.Windows.Forms.ListBox();
            this.btn_ReadMonitorMessage = new System.Windows.Forms.Button();
            this.btn_SendMonitor = new System.Windows.Forms.Button();
            this.txt_lon = new System.Windows.Forms.TextBox();
            this.txt_lat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_SendGPS = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.readlocalenerydata = new System.Windows.Forms.Button();
            this.btn_SendEnery = new System.Windows.Forms.Button();
            this.timechange = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Port
            // 
            this.lbl_Port.AutoSize = true;
            this.lbl_Port.Location = new System.Drawing.Point(27, 20);
            this.lbl_Port.Name = "lbl_Port";
            this.lbl_Port.Size = new System.Drawing.Size(35, 12);
            this.lbl_Port.TabIndex = 2;
            this.lbl_Port.Text = "串口:";
            // 
            // lbl_BaudRate
            // 
            this.lbl_BaudRate.AutoSize = true;
            this.lbl_BaudRate.Location = new System.Drawing.Point(15, 48);
            this.lbl_BaudRate.Name = "lbl_BaudRate";
            this.lbl_BaudRate.Size = new System.Drawing.Size(47, 12);
            this.lbl_BaudRate.TabIndex = 3;
            this.lbl_BaudRate.Text = "波特率:";
            // 
            // cbo_Port
            // 
            this.cbo_Port.FormattingEnabled = true;
            this.cbo_Port.Location = new System.Drawing.Point(68, 17);
            this.cbo_Port.Name = "cbo_Port";
            this.cbo_Port.Size = new System.Drawing.Size(103, 20);
            this.cbo_Port.TabIndex = 4;
            // 
            // cbo_BaudRate
            // 
            this.cbo_BaudRate.FormattingEnabled = true;
            this.cbo_BaudRate.Location = new System.Drawing.Point(68, 48);
            this.cbo_BaudRate.Name = "cbo_BaudRate";
            this.cbo_BaudRate.Size = new System.Drawing.Size(103, 20);
            this.cbo_BaudRate.TabIndex = 5;
            // 
            // btn_SerialPortOperation
            // 
            this.btn_SerialPortOperation.Location = new System.Drawing.Point(91, 88);
            this.btn_SerialPortOperation.Name = "btn_SerialPortOperation";
            this.btn_SerialPortOperation.Size = new System.Drawing.Size(80, 36);
            this.btn_SerialPortOperation.TabIndex = 6;
            this.btn_SerialPortOperation.Text = "打开串口";
            this.btn_SerialPortOperation.UseVisualStyleBackColor = true;
            this.btn_SerialPortOperation.Click += new System.EventHandler(this.btn_SerialPortOperation_Click);
            // 
            // btn_BaudRateRefresh
            // 
            this.btn_BaudRateRefresh.Location = new System.Drawing.Point(12, 88);
            this.btn_BaudRateRefresh.Name = "btn_BaudRateRefresh";
            this.btn_BaudRateRefresh.Size = new System.Drawing.Size(63, 36);
            this.btn_BaudRateRefresh.TabIndex = 7;
            this.btn_BaudRateRefresh.Text = "刷新";
            this.btn_BaudRateRefresh.UseVisualStyleBackColor = true;
            this.btn_BaudRateRefresh.Click += new System.EventHandler(this.btn_BaudRateRefresh_Click);
            // 
            // btn_ReadPhotoData
            // 
            this.btn_ReadPhotoData.Location = new System.Drawing.Point(6, 17);
            this.btn_ReadPhotoData.Name = "btn_ReadPhotoData";
            this.btn_ReadPhotoData.Size = new System.Drawing.Size(75, 37);
            this.btn_ReadPhotoData.TabIndex = 9;
            this.btn_ReadPhotoData.Text = "读取本地摄影数据";
            this.btn_ReadPhotoData.UseVisualStyleBackColor = true;
            this.btn_ReadPhotoData.Click += new System.EventHandler(this.btn_ReadPhotoData_Click);
            // 
            // btn_SendPhotoData
            // 
            this.btn_SendPhotoData.Location = new System.Drawing.Point(104, 12);
            this.btn_SendPhotoData.Name = "btn_SendPhotoData";
            this.btn_SendPhotoData.Size = new System.Drawing.Size(75, 37);
            this.btn_SendPhotoData.TabIndex = 10;
            this.btn_SendPhotoData.Text = "开始发送";
            this.btn_SendPhotoData.UseVisualStyleBackColor = true;
            this.btn_SendPhotoData.Click += new System.EventHandler(this.btn_SendPhotoData_Click);
            // 
            // txt_TimeStamp
            // 
            this.txt_TimeStamp.Location = new System.Drawing.Point(47, 67);
            this.txt_TimeStamp.Name = "txt_TimeStamp";
            this.txt_TimeStamp.Size = new System.Drawing.Size(46, 21);
            this.txt_TimeStamp.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "间隔:";
            // 
            // btn_RefreshTimeStamp
            // 
            this.btn_RefreshTimeStamp.Location = new System.Drawing.Point(104, 65);
            this.btn_RefreshTimeStamp.Name = "btn_RefreshTimeStamp";
            this.btn_RefreshTimeStamp.Size = new System.Drawing.Size(56, 23);
            this.btn_RefreshTimeStamp.TabIndex = 13;
            this.btn_RefreshTimeStamp.Text = "刷新";
            this.btn_RefreshTimeStamp.UseVisualStyleBackColor = true;
            this.btn_RefreshTimeStamp.Click += new System.EventHandler(this.btn_RefreshTimeStamp_Click);
            // 
            // lst_DataShow
            // 
            this.lst_DataShow.FormattingEnabled = true;
            this.lst_DataShow.ItemHeight = 12;
            this.lst_DataShow.Location = new System.Drawing.Point(655, 2);
            this.lst_DataShow.Name = "lst_DataShow";
            this.lst_DataShow.Size = new System.Drawing.Size(278, 532);
            this.lst_DataShow.TabIndex = 14;
            // 
            // btn_ReadMonitorMessage
            // 
            this.btn_ReadMonitorMessage.Location = new System.Drawing.Point(27, 42);
            this.btn_ReadMonitorMessage.Name = "btn_ReadMonitorMessage";
            this.btn_ReadMonitorMessage.Size = new System.Drawing.Size(75, 30);
            this.btn_ReadMonitorMessage.TabIndex = 15;
            this.btn_ReadMonitorMessage.Text = "读取CSV";
            this.btn_ReadMonitorMessage.UseVisualStyleBackColor = true;
            this.btn_ReadMonitorMessage.Click += new System.EventHandler(this.btn_ReadMonitorMessage_Click);
            // 
            // btn_SendMonitor
            // 
            this.btn_SendMonitor.Location = new System.Drawing.Point(108, 42);
            this.btn_SendMonitor.Name = "btn_SendMonitor";
            this.btn_SendMonitor.Size = new System.Drawing.Size(75, 30);
            this.btn_SendMonitor.TabIndex = 16;
            this.btn_SendMonitor.Text = "5s循环发送监控设备";
            this.btn_SendMonitor.UseVisualStyleBackColor = true;
            this.btn_SendMonitor.Click += new System.EventHandler(this.btn_SendMonitor_Click);
            // 
            // txt_lon
            // 
            this.txt_lon.Location = new System.Drawing.Point(72, 20);
            this.txt_lon.Name = "txt_lon";
            this.txt_lon.Size = new System.Drawing.Size(100, 21);
            this.txt_lon.TabIndex = 17;
            // 
            // txt_lat
            // 
            this.txt_lat.Location = new System.Drawing.Point(72, 47);
            this.txt_lat.Name = "txt_lat";
            this.txt_lat.Size = new System.Drawing.Size(100, 21);
            this.txt_lat.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "经度:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "纬度:";
            // 
            // btn_SendGPS
            // 
            this.btn_SendGPS.Location = new System.Drawing.Point(59, 74);
            this.btn_SendGPS.Name = "btn_SendGPS";
            this.btn_SendGPS.Size = new System.Drawing.Size(75, 23);
            this.btn_SendGPS.TabIndex = 21;
            this.btn_SendGPS.Text = "发送";
            this.btn_SendGPS.UseVisualStyleBackColor = true;
            this.btn_SendGPS.Click += new System.EventHandler(this.btn_SendGPS_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_SendPhotoData);
            this.groupBox1.Controls.Add(this.btn_ReadPhotoData);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_TimeStamp);
            this.groupBox1.Controls.Add(this.btn_RefreshTimeStamp);
            this.groupBox1.Location = new System.Drawing.Point(12, 130);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(201, 109);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "树莓派";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_SendMonitor);
            this.groupBox2.Controls.Add(this.btn_ReadMonitorMessage);
            this.groupBox2.Location = new System.Drawing.Point(13, 245);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "读取监控设备的经纬度并且按照心跳发送心跳(/bin/Monitor/monitor.csv)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txt_lon);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txt_lat);
            this.groupBox3.Controls.Add(this.btn_SendGPS);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(13, 351);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 100);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "根据输入的经纬度数据发送心跳";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.timechange);
            this.groupBox4.Controls.Add(this.readlocalenerydata);
            this.groupBox4.Controls.Add(this.btn_SendEnery);
            this.groupBox4.Location = new System.Drawing.Point(227, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 176);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "根据输入数据发送能源数据";
            // 
            // readlocalenerydata
            // 
            this.readlocalenerydata.Location = new System.Drawing.Point(17, 92);
            this.readlocalenerydata.Name = "readlocalenerydata";
            this.readlocalenerydata.Size = new System.Drawing.Size(85, 30);
            this.readlocalenerydata.TabIndex = 2;
            this.readlocalenerydata.Text = "读取本地";
            this.readlocalenerydata.UseVisualStyleBackColor = true;
            this.readlocalenerydata.Click += new System.EventHandler(this.readlocalenerydata_Click);
            // 
            // btn_SendEnery
            // 
            this.btn_SendEnery.Location = new System.Drawing.Point(108, 92);
            this.btn_SendEnery.Name = "btn_SendEnery";
            this.btn_SendEnery.Size = new System.Drawing.Size(86, 30);
            this.btn_SendEnery.TabIndex = 1;
            this.btn_SendEnery.Text = "发送能源数据";
            this.btn_SendEnery.UseVisualStyleBackColor = true;
            this.btn_SendEnery.Click += new System.EventHandler(this.btn_SendEnery_Click);
            // 
            // timechange
            // 
            this.timechange.Location = new System.Drawing.Point(17, 134);
            this.timechange.Name = "timechange";
            this.timechange.Size = new System.Drawing.Size(85, 30);
            this.timechange.TabIndex = 3;
            this.timechange.Text = "时间转换";
            this.timechange.UseVisualStyleBackColor = true;
            this.timechange.Click += new System.EventHandler(this.timechange_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(108, 134);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "检查速度";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 538);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lst_DataShow);
            this.Controls.Add(this.btn_BaudRateRefresh);
            this.Controls.Add(this.btn_SerialPortOperation);
            this.Controls.Add(this.cbo_BaudRate);
            this.Controls.Add(this.cbo_Port);
            this.Controls.Add(this.lbl_BaudRate);
            this.Controls.Add(this.lbl_Port);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbl_Port;
        private System.Windows.Forms.Label lbl_BaudRate;
        private System.Windows.Forms.ComboBox cbo_Port;
        private System.Windows.Forms.ComboBox cbo_BaudRate;
        private System.Windows.Forms.Button btn_SerialPortOperation;
        private System.Windows.Forms.Button btn_BaudRateRefresh;
        private System.Windows.Forms.Button btn_ReadPhotoData;
        private System.Windows.Forms.Button btn_SendPhotoData;
        private System.Windows.Forms.TextBox txt_TimeStamp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_RefreshTimeStamp;
        private System.Windows.Forms.ListBox lst_DataShow;
        private System.Windows.Forms.Button btn_ReadMonitorMessage;
        private System.Windows.Forms.Button btn_SendMonitor;
        private System.Windows.Forms.TextBox txt_lon;
        private System.Windows.Forms.TextBox txt_lat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_SendGPS;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btn_SendEnery;
        private System.Windows.Forms.Button readlocalenerydata;
        private System.Windows.Forms.Button timechange;
        private System.Windows.Forms.Button button1;
    }
}

