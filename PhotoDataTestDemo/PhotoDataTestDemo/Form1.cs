using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PhotoDataTestDemo.Properties;
using Timer = System.Windows.Forms.Timer;
using System.Collections.Generic;

namespace PhotoDataTestDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private readonly SerialPort _serialPort = new SerialPort();
        private string[] _portNames;
        private readonly string[] _portBauds = { "1200", "2400", "4800", "9600", "14400", "19200", "38400", "43000", "57600", "76800", "115200", "128000", "230400", "256000", "460800", "921600", "1382400" };
        private readonly string _path = Environment.CurrentDirectory + @"\Photo\FrameCache";
        private byte[][] _serialPortReadedDataBuf;
        private const int Bufnumber = 100000;
        private const int Bufsize = 1024;
        private readonly Timer _photoTimer = new Timer();
        private int _frameCount = 1;
        private int _gpsHeight = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            InitSeriPort();
            Init();
            _photoTimer.Interval = 50;
            _photoTimer.Tick += _photoTimer_Tick;
            CheckForIllegalCrossThreadCalls = false;
        }

        private void InitSeriPort()
        {
            RefreshSerialPort();
            cbo_BaudRate.DataSource = _portBauds;
            cbo_BaudRate.SelectedIndex = 10;
            txt_lon.Text = (113.85974).ToString();
            txt_lat.Text = (22.904123).ToString();
        }
        private void RefreshSerialPort()
        {
            _portNames = SerialPort.GetPortNames();
            cbo_Port.DataSource = _portNames;
            cbo_Port.SelectedIndex = 0;
        }

        private void Init()
        {
            str = new string[BUFNUMBER][];
            for (int i = 0; i < BUFNUMBER; i++)
            {
                str[i] = new string[BUFSIZE];
            }
        }

        /// <summary>
        /// crc校验
        /// </summary>
        /// <param name="pBuffer"></param>
        /// <param name="bufferSize"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static ushort CalcCrc(byte[] pBuffer, int bufferSize, ushort offset)
        {
            byte[] pBytesArray = pBuffer;
            ushort poly = 0x8408;
            ushort crc = 0;
            byte carry;
            byte i_bits;
            ushort j;
            ushort rl = 2;
            for (j = offset; j < bufferSize; j++)
            {

                crc ^= Convert.ToUInt16(pBytesArray[j]);
                //               Console.WriteLine("pBytesArray[{0}]={1},crc={2}", j,pBytesArray[j], crc);

                for (i_bits = 0; i_bits < 8; i_bits++)
                {
                    carry = (byte)(crc & 1);
                    crc /= rl;
                    //                    Console.WriteLine("carry={0},i_bits={1},crc={2}", carry, i_bits, crc);
                    if (carry != 0)
                    {
                        crc ^= poly;
                        //                        Console.WriteLine("crc={0},poly={1}", crc, poly);

                    }

                }
            }
            return crc;
        }

        private void _photoTimer_Tick(object sender, EventArgs e)
        {
            if (_serialPortReadedDataBuf[_frameCount][0] != 0xAA || _frameCount >= Bufnumber)
            {
                _photoTimer.Stop();
                _frameCount = 1;
            }
            else
            {
                _serialPort.Write(_serialPortReadedDataBuf[_frameCount], 0, 15 + _serialPortReadedDataBuf[_frameCount][2]);
                lst_DataShow.Items.Add(_frameCount);
                lst_DataShow.SelectedIndex = lst_DataShow.Items.Count - 1;
                _frameCount++;
            }
        }

        /// <summary>
        /// 打开串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SerialPortOperation_Click(object sender, EventArgs e)
        {
            if (btn_SerialPortOperation.Text.Equals(Resources.strOpenSerialPort))
            {
                btn_SerialPortOperation.Text = Resources.strCloseSerialPort;
                if (cbo_Port.Text == null || cbo_BaudRate.Text == null) return;
                _serialPort.PortName = cbo_Port.Text;
                _serialPort.BaudRate = int.Parse(cbo_BaudRate.Text);
                _serialPort.DataReceived += _serialPort_DataReceived;
                _serialPort.Open();
                _frameCount = 1;
            }
            else
            {
                btn_SerialPortOperation.Text = Resources.strOpenSerialPort;
                _serialPort.Close();
            }
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                while (_serialPort.BytesToRead > 0)
                {
                    lst_DataShow.Items.Add(_serialPort.ReadByte().ToString("X"));
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_BaudRateRefresh_Click(object sender, EventArgs e) => RefreshSerialPort();

        /// <summary>
        /// 读取本地的摄影数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ReadPhotoData_Click(object sender, EventArgs e)
        {
            InitBufs();
            ReadPhotoData();
            AddTextToListView("读取完成。");

        }

        private void InitBufs()
        {
            _serialPortReadedDataBuf = new Byte[Bufnumber][];
            for (int i = 0; i < Bufnumber; i++)
            {
                _serialPortReadedDataBuf[i] = new Byte[Bufsize];
            }
        }

        private void ReadPhotoData()
        {
            if (File.Exists(_path))
            {
                try
                {
                    FileStream file = new FileStream(_path, FileMode.Open);
                    FileInfo f = new FileInfo(_path);
                    byte[] byData = new byte[f.Length];
                    file.Read(byData, 0, byData.Length);
                    int count1 = 1;
                    file.Flush();
                    file.Close();
                    for (int i = 0; i < byData.Length;)
                    {
                        if (i + 14 <= byData.Length - 1 && byData[i + 2] == 0)
                        {
                            for (int n = 0; n < 15; n++)
                            {
                                _serialPortReadedDataBuf[count1][n] = byData[i];
                                i++;
                            }
                            count1++;
                        }
                        else
                        {
                            var count2 = i;
                            if (i + byData[count2 + 2] + 14 <= byData.Length - 1)
                            {
                                for (int n = 0; n < byData[count2 + 2] + 15; n++)
                                {
                                    _serialPortReadedDataBuf[count1][n] = byData[i];
                                    i++;
                                }
                                count1++;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show(_path + Resources.strReadPhotoTip);
            }
        }
        /// <summary>
        /// 开始定时发送摄影数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SendPhotoData_Click(object sender, EventArgs e)
        {
            if (btn_SendPhotoData.Text.Equals(Resources.strStartSendPhoto))
            {
                btn_SendPhotoData.Text = Resources.strStopSendPhoto;
                if (_photoTimer.Enabled)
                {
                    _photoTimer.Stop();
                }
                _photoTimer.Start();
            }
            else
            {
                btn_SendPhotoData.Text = Resources.strStartSendPhoto;
                if (_photoTimer.Enabled)
                {
                    _photoTimer.Stop();
                }
            }

        }
        /// <summary>
        /// 更新摄影数据的发送频率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_RefreshTimeStamp_Click(object sender, EventArgs e)
        {
            if (_photoTimer.Enabled)
            {
                _photoTimer.Stop();
                _photoTimer.Interval = int.Parse(txt_TimeStamp.Text.Trim());
                _photoTimer.Start();
            }
            else
            {
                _photoTimer.Interval = int.Parse(txt_TimeStamp.Text.Trim());
            }
        }
        string[][] str;
        private const int BUFSIZE = 100;
        private const int BUFNUMBER = 1000;

        /// <summary>
        /// 心跳
        /// </summary>
        private byte[] buffer = {
        0xAA ,0x55 ,0x3c ,0xDA ,0xD7 ,0x7C ,0x29 ,0x01 ,0x06 ,0x90 ,0x00 ,0x00 ,0x06 ,0x02 ,0x00,
        0x05 ,0xE2 ,0x07 ,0x03 ,0x07 ,0x06 ,0x16 ,0x31 ,0x00 ,0x00 ,0x00 ,0x00 ,0x24 ,0x00 ,0x0B ,
        0x5E ,0x60 ,0xB8 ,0xE3 ,0x42 ,0x60 ,0xB8 ,0xE3 ,0x42 ,0xFA ,0x3B ,0xB7 ,0x41 ,0x60 ,0xB8 ,
        0xE3 ,0x42 ,0x33 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x00 ,0x23 ,0x00 ,0x00 ,
        0x00 ,0x23 ,0x8A ,0x01 ,0x00 ,0x20 ,0x00 ,0x00 ,0x00 ,0x00 ,0x02 ,0x00 ,0xBB ,0xB4 ,0x4E
        };

        /// <summary>
        /// 更新显示面板
        /// </summary>
        /// <param name="str"></param>
        private void AddTextToListView(string str) => lst_DataShow.Items.Add(str + "\r\n");

        /// <summary>
        /// 读取本地bin下的监控设备数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ReadMonitorMessage_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Environment.CurrentDirectory + @"\Monitor\"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\Monitor\");
            }
            if (File.Exists(Environment.CurrentDirectory + @"\Monitor\monitor.csv"))
            {
                try
                {
                    FileStream fileStream = new FileStream(Environment.CurrentDirectory + @"\Monitor\monitor.csv",
                        FileMode.Open, FileAccess.Read, FileShare.ReadWrite); //读取流
                    StreamReader sr = new StreamReader(fileStream, Encoding.UTF8);
                    Init();
                    string line = sr.ReadLine();
                    int count = 0;
                    while (line != null)
                    {
                        string[] stringArray = line.Split(',');
                        if (stringArray.Count() > 1)
                        {
                            for (int j = 0; j < stringArray.Count(); j++)
                            {
                                str[count][j] = stringArray[j];
                            }
                        }
                        line = sr.ReadLine();
                        count++;
                    }
                    AddTextToListView("读取csv完成。");
                }
                catch (Exception e1)
                {
                    AddTextToListView(e1.ToString());
                }
            }
        }

        private void btn_SendMonitor_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i][1] != null)
                {
                    double lon = double.Parse(str[i][12].Substring(2));
                    double lg = double.Parse(str[i][13].Substring(2));
                    byte[] a1 = BitConverter.GetBytes(lon);
                    byte[] a2 = BitConverter.GetBytes(lg);
                    buffer[31] = a1[0];
                    buffer[32] = a1[1];
                    buffer[33] = a1[2];
                    buffer[34] = a1[3];
                    buffer[35] = a1[4];
                    buffer[36] = a1[5];
                    buffer[37] = a1[6];
                    buffer[38] = a1[7];


                    buffer[39] = a2[0];
                    buffer[40] = a2[1];
                    buffer[41] = a2[2];
                    buffer[42] = a2[3];
                    buffer[43] = a2[4];
                    buffer[44] = a2[5];
                    buffer[45] = a2[6];
                    buffer[46] = a2[7];
                    byte[] a3 = BitConverter.GetBytes(int.Parse(str[i][14]));
                    buffer[47] = a3[0];
                    buffer[48] = a3[1];
                    buffer[49] = a3[2];
                    buffer[50] = a3[3];
                    //CRC
                    ushort crc = CalcCrc(buffer, buffer.Length - 3, 2);
                    byte[] b = BitConverter.GetBytes(crc);
                    buffer[buffer.Length - 3] = b[0];
                    buffer[buffer.Length - 2] = b[1];
                    _serialPort.Write(buffer, 0, buffer.Length);
                }
                else
                {
                    break;
                }
                Thread.Sleep(500);
            }

        }

        /// <summary>
        /// 根据输入的经纬度发送心跳数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SendGPS_Click(object sender, EventArgs e)
        {
            _gpsHeight = _gpsHeight + 10;
            double lon = double.Parse(txt_lon.Text.Trim().Replace(" ", ""));
            double lat = double.Parse(txt_lat.Text.Trim().Replace(" ", ""));
            byte[] a1 = BitConverter.GetBytes(lon);
            byte[] a2 = BitConverter.GetBytes(lat);
            buffer[31] = a1[0];
            buffer[32] = a1[1];
            buffer[33] = a1[2];
            buffer[34] = a1[3];
            buffer[35] = a1[4];
            buffer[36] = a1[5];
            buffer[37] = a1[6];
            buffer[38] = a1[7];
            buffer[39] = a2[0];
            buffer[40] = a2[1];
            buffer[41] = a2[2];
            buffer[42] = a2[3];
            buffer[43] = a2[4];
            buffer[44] = a2[5];
            buffer[45] = a2[6];
            buffer[46] = a2[7];
            byte[] a3 = BitConverter.GetBytes(_gpsHeight);
            buffer[47] = a3[0];
            buffer[48] = a3[1];
            buffer[49] = a3[2];
            buffer[50] = a3[3];
            //CRC
            ushort crc = CalcCrc(buffer, buffer.Length - 3, 2);
            byte[] b = BitConverter.GetBytes(crc);
            buffer[buffer.Length - 3] = b[0];
            buffer[buffer.Length - 2] = b[1];
            _serialPort.Write(buffer, 0, buffer.Length);
        }

        private byte[] enerybuffer = {
            0xAA, 0x55, 0xB9, 0xE7, 0x4B, 0x24, 0x3A, 0x01, 0x03, 0xA0, 0x00, 0x03, 0xBC, 0x00, 0x20, 0x4E , 0x20 , 0x4E , 0xBE , 0xA6 , 0xBE , 0xA6 , 0xBE , 0xA6 , 0xBE ,
            0xA6, 0x20, 0x4E, 0xBE, 0xA6 , 0x23, 0x4E, 0x23 , 0x4E , 0x47 , 0x4E , 0x20 , 0x4E , 0x20 , 0x4E , 0x44 , 0x4E , 0x23 , 0x4E , 0x44 , 0x4E , 0x1B , 0x03 , 0x0D ,
            0x03 , 0x29 , 0x03 , 0x0F , 0x02 , 0x85 , 0x01 , 0x85 , 0x01 , 0x85 , 0x01 , 0xBE , 0xA6 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 ,
            0x00 , 0xC0 , 0xEF , 0xEC , 0x5C , 0x40 , 0x00 , 0x00 , 0x00 , 0xC0 , 0xDB , 0xDA , 0x36 , 0x40 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0xCF , 0xDA , 0x40 , 0x00 ,
            0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 ,
            0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 ,
            0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x01 , 0xBE , 0xA6 , 0x8B , 0xD4 , 0x23 , 0x4E , 0x23 ,
            0x4E , 0xE4 , 0xA3 , 0xF0 , 0xA3 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0x00 , 0xA4 , 0x5F , 0x00 , 0x00 , 0xCB , 0x05 , 0x00 , 0x00 , 0x01 , 0x02 ,
            0x01 , 0x99 , 0x20 , 0x76 , 0x5A , 0x01 , 0x00 , 0x00 , 0x00 , 0x00 , 0xE7 , 0xE2 , 0x4E };

        private void readlocalenerydata_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "D://";
            openFileDialog.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fName = openFileDialog.FileName;
                if (File.Exists(fName))
                {
                    try
                    {
                        FileStream fileStream = new FileStream(fName,
                            FileMode.Open, FileAccess.Read, FileShare.ReadWrite); //读取流
                        StreamReader sr = new StreamReader(fileStream, Encoding.UTF8);
                        Init();
                        string line = sr.ReadLine();
                        int count = 0;
                        while (line != null)
                        {
                            string[] stringArray = line.Split(',');
                            if (stringArray.Count() > 1)
                            {
                                for (int j = 0; j < stringArray.Count(); j++)
                                {
                                    str[count][j] = stringArray[j];
                                }
                            }
                            line = sr.ReadLine();
                            count++;
                        }
                        AddTextToListView("读取csv完成。");
                        readFinish = true;
                    }
                    catch (Exception e1)
                    {
                        AddTextToListView(e1.ToString());
                    }
                }

            }
        }
        private int lonCount = 0;
        private int latCount = 1;
        private int heightCount = 2;
        private void btn_SendEnery_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i][1] != null)
                {
                    double lon = 0;
                    double lat = 0;
                    if (str[i][lonCount].Contains("东经") || str[i][lonCount].Contains("西经"))
                    {
                        lon = double.Parse(str[i][lonCount].Substring(2));
                    }
                    else
                    {
                        lon = double.Parse(str[i][lonCount]);
                    }
                    if (str[i][latCount].Contains("南纬") || str[i][latCount].Contains("北纬"))
                    {
                        lat = double.Parse(str[i][latCount].Substring(2));
                    }
                    else
                    {
                        lat = double.Parse(str[i][latCount]);
                    }
                    double gpsheight = double.Parse(str[i][heightCount]);
                    byte[] lonbyte = BitConverter.GetBytes(lon);
                    byte[] latbyte = BitConverter.GetBytes(lat);
                    byte[] heightbyte = BitConverter.GetBytes(gpsheight);
                    //lon
                    enerybuffer[70] = lonbyte[0];
                    enerybuffer[71] = lonbyte[1];
                    enerybuffer[72] = lonbyte[2];
                    enerybuffer[73] = lonbyte[3];
                    enerybuffer[74] = lonbyte[4];
                    enerybuffer[75] = lonbyte[5];
                    enerybuffer[76] = lonbyte[6];
                    enerybuffer[77] = lonbyte[7];
                    //lat
                    enerybuffer[78] = latbyte[0];
                    enerybuffer[79] = latbyte[1];
                    enerybuffer[80] = latbyte[2];
                    enerybuffer[81] = latbyte[3];
                    enerybuffer[82] = latbyte[4];
                    enerybuffer[83] = latbyte[5];
                    enerybuffer[84] = latbyte[6];
                    enerybuffer[85] = latbyte[7];
                    //height
                    enerybuffer[86] = heightbyte[0];
                    enerybuffer[87] = heightbyte[1];
                    enerybuffer[88] = heightbyte[2];
                    enerybuffer[89] = heightbyte[3];
                    enerybuffer[90] = heightbyte[4];
                    enerybuffer[91] = heightbyte[5];
                    enerybuffer[92] = heightbyte[6];
                    enerybuffer[93] = heightbyte[7];
                    //CRC
                    ushort crc = CalcCrc(enerybuffer, enerybuffer.Length - 3, 2);
                    byte[] b = BitConverter.GetBytes(crc);
                    enerybuffer[enerybuffer.Length - 3] = b[0];
                    enerybuffer[enerybuffer.Length - 2] = b[1];
                    _serialPort.Write(enerybuffer, 0, enerybuffer.Length);
                }
                else
                {
                    break;
                }
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// 转换时区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timechange_Click(object sender, EventArgs e)
        {
            data = new List<string>();
            for (int i = 1; i < str.Length; i++)
            {
                if (str[i][1] != null)
                {
                    changetime(i);
                }
                else
                {
                    break;
                }
            }
            savetocsv(data);
        }
        System.Collections.Generic.List<string> data = new List<string>();
        private void changetime(int i)
        {
            //将北京时间转换成utc时间
            DateTime beijingtime = DateTime.Parse(str[i][4]);
            DateTime utctime = beijingtime.AddHours(-8);
            //经度
            double lon;
            if (str[i][lonCount].Contains("东经") || str[i][lonCount].Contains("西经"))
            {
                lon = double.Parse(str[i][lonCount].Substring(2));
            }
            else
            {
                lon = double.Parse(str[i][lonCount]);
            }
            //经度所在时间
            DateTime localtime;
            int longtitudeby = 0;
            //每个时区为15度,遵循东加西减原则
            //余数若是大于7.5，则+1个时区，否则，不算一个时区
            //东经
            if (lon > 0)
            {
                localtime = utctime.AddHours(((int)lon) / 15);
                longtitudeby = ((int)lon) % 15;
                if (longtitudeby > 7.5)
                {
                    localtime = localtime.AddHours(1);
                }
            }
            //西经
            else
            {
                lon = Math.Abs(lon);
                localtime = utctime.AddHours(0 - (((int)lon) / 15));
                longtitudeby = ((int)lon) % 15;
                if (longtitudeby > 7.5)
                {
                    localtime = localtime.AddHours(-1);
                }
            }
            //AddTextToListView(localtime.ToString("yyyy-MM-dd HH-mm-ss"));
            data.Add(localtime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        string path=Environment.CurrentDirectory + @"\time\";
        string fileName = "time"+ DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
        /// <summary>
        /// 保持成csv文件
        /// </summary>
        private void savetocsv(List<string> data)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                FileStream fs1;
                if (!File.Exists(path + fileName))
                {
                    fs1 = new FileStream(path + fileName, FileMode.Create, FileAccess.Write);//创建写入文件 
                }
                else
                {
                    fs1 = new FileStream(path + fileName, FileMode.Append, FileAccess.Write);//创建写入文件 
                }
                StreamWriter sw = new StreamWriter(fs1, Encoding.UTF8);
                for(int i = 0; i < data.Count; i++)
                {
                    if (data[i] != null && !data[i].Equals(""))
                    {
                        sw.WriteLine(data[i]);
                    }
                }
                //开始写入 
                sw.Flush();
                fs1.Flush();
                sw.Close();
                fs1.Close();
            }
            catch (Exception e)
            {
                
            }
        }


        bool readFinish = false;
        /// <summary>
        /// 检查速度是否符合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (readFinish)
            {
                int count = 0;
                for (int i = 1; i < str.Length; i++)
                {
                    if (str[i] != null && str[i][0] != null)
                    {
                        if (i == str.Length - 1)
                            break;
                        double speed = CalculateSpeed(double.Parse(str[i][0]), double.Parse(str[i][1]),
                            DateTime.Parse(str[i][4]), double.Parse(str[i + 1][0]), double.Parse(str[i + 1][1]),
                            DateTime.Parse(str[i + 1][4]));
                        if (speed > 100)
                        {
                            AddTextToListView("不符合条件项:" + (i + 1));
                            count++;
                        }
                    }
                }
                if (count == 0)
                    AddTextToListView("数据均符合条件!");
            }
            else
            {
                MessageBox.Show("请读取本地.csv文件!");
            }
        }
        private double CalculateSpeed(double preLon, double preLat, DateTime preTime, double lon, double lat, DateTime time)
        {
            double distance = GetDistance(preLon, preLat, lon, lat);
            double timeDiff = (time - preTime).TotalSeconds / 3600;

            return distance / timeDiff;
        }

        private const double EARTH_RADIUS = 6378.137;
        private double rad(double d)
        {
            return d * Math.PI / 180.0;
        }
        private double GetDistance(double long1, double lat1, double long2, double lat2)
        {
            double a, b, d, sa2, sb2;
            lat1 = rad(lat1);
            lat2 = rad(lat2);
            a = lat1 - lat2;
            b = rad(long1 - long2);

            sa2 = Math.Sin(a / 2.0);
            sb2 = Math.Sin(b / 2.0);
            d = 2 * EARTH_RADIUS
                    * Math.Asin(Math.Sqrt(sa2 * sa2 + Math.Cos(lat1)
                    * Math.Cos(lat2) * sb2 * sb2));
            return d;
        }

    }
}