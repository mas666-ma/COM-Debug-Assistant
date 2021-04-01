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
			timer.Interval = TimeSpan.FromMilliseconds(500);
			timer.Tick += Timer_Tick;
			timer.Start();

			//
			

		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
