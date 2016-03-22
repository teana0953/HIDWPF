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

using System.Windows.Forms;
using UsbLibrary;
using System.Windows.Interop;

namespace HIDWPF
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        UsbLibrary.UsbHidPort usb = new UsbHidPort();
        public MainWindow()
        {
            InitializeComponent();

            // load usbHid
            usbHid.Child = usb;
            usb.ProductId = 21760;
            usb.VendorId = 1204;

            this.usb.OnSpecifiedDeviceArrived += new System.EventHandler(this.usb_BPA_OnSpecifiedDeviceArrived);
            this.usb.OnSpecifiedDeviceRemoved += new System.EventHandler(this.usb_BPA_OnSpecifiedDeviceRemoved);
            this.usb.OnDeviceArrived += new System.EventHandler(this.usb_BPA_OnDeviceArrived);
            this.usb.OnDeviceRemoved += new System.EventHandler(this.usb_BPA_OnDeviceRemoved);
            this.usb.OnDataRecieved += new UsbLibrary.DataRecievedEventHandler(this.usb_BPA_OnDataRecieved);
            this.usb.OnDataSend += new System.EventHandler(this.usb_BPA_OnDataSend);
        }

        #region USB-related
        private void usb_BPA_OnDataSend(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void usb_BPA_OnDataRecieved(object sender, DataRecievedEventArgs args)
        {
            throw new NotImplementedException();
        }
        
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);

            //USB Connection
            usb.RegisterHandle(source.Handle);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            usb.ParseMessages(msg,wParam);

            return IntPtr.Zero;
        }

        private void usb_BPA_OnDeviceRemoved(object sender, EventArgs e)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new EventHandler(usb_BPA_OnDeviceRemoved), new object[] { sender, e });
            }
        }

        private void usb_BPA_OnDeviceArrived(object sender, EventArgs e)
        {
            
        }

        private void usb_BPA_OnSpecifiedDeviceRemoved(object sender, EventArgs e)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new EventHandler(usb_BPA_OnSpecifiedDeviceRemoved), new object[] { sender, e });
            }
        }

        private void usb_BPA_OnSpecifiedDeviceArrived(object sender, EventArgs e)
        {
           
        }

        #endregion
    }
}
