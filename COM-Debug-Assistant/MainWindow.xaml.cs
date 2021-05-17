using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
using System.Windows.Threading;

namespace COM_Debug_Assistant
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public SerialPort _serialPort = new SerialPort(); //初始化串口对象
		public DispatcherTimer timer= new DispatcherTimer(); //初始化定时器对象
		public MainWindow()
		{
			InitializeComponent();
			//
			timer.Interval = TimeSpan.FromMilliseconds(500); //计时器刻度之间的时间段：500ms
			timer.Tick += TimerTickHandler; //tick时间处理程序
			timer.Start();
            //
            ReadSerial();	

		}
        private void ReadSerial() //读取串口
        {
            portcb.Items.Clear();
            string[] ports = SerialPort.GetPortNames();//获取当前计算机的串行端口名的数组。

            for (int index = 0; index < ports.Length; index++)
            {
                portcb.Items.Add(ports[index]);//添加item
                portcb.SelectedIndex = index; //设置显示的item索引
            }

            baudcb.SelectedIndex = 1;
        }
        private void TimerTickHandler(object sender, EventArgs e) //tick时间处理程序
        {
            // 定时更新串口信息
            ReadSerial();
        }

        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)//读取下位机的数据，显示在textBlock中
        {
            int len = this._serialPort.BytesToRead;
            byte[] buffer = new byte[len];

            this._serialPort.Read(buffer, 0, len);

            //string strData = BitConverter.ToString(buffer, 0, len);  // 16进制
            string strData = Encoding.Default.GetString(buffer); // 字符串

            Dispatcher.Invoke(() =>
            {
                //this.receivetb.Text += strData;
                //this.receivetb.Text += "-";//字符分隔-

                this.receivetb.AppendText(strData);
            });
        }

        private void portbClick(object sender, RoutedEventArgs s)
        {
            string strContent = this.portb.Content.ToString();
            if (strContent == "打开串口")
            {
                try
                {
                    _serialPort.PortName = portcb.SelectedItem.ToString();//串口号
                    ComboBoxItem seletedItem = (ComboBoxItem)this.baudcb.SelectedItem;
                    _serialPort.BaudRate = Convert.ToInt32(seletedItem.Content.ToString());//波特率
                    _serialPort.DataBits = 8;//数据位
                    _serialPort.StopBits = StopBits.One;//停止位
                    _serialPort.Parity = Parity.None;//校验位

                    _serialPort.Open();
                    _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);//添加数据接收事件
                    _serialPort.DataReceived += DataReceivedHandler;

                    portb.Content = "关闭串口";
                }
                catch
                {
                    MessageBox.Show("打开串口失败", "错误");
                }
            }
            else
            {
                try
                {
                    _serialPort.DataReceived -= DataReceivedHandler;
                    _serialPort.Close();
                    portb.Content = "打开串口";
                }
                catch
                {
                    MessageBox.Show("关闭串口失败", "错误");
                }
            }
        }

        private void clear(object sender, RoutedEventArgs s)
        {
            // 清空数据
            receivetb.Clear();
        }

        private void sendBtnClick(object sender, RoutedEventArgs s)
        {

        }

        
    }	
}
