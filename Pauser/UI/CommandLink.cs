using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Pauser.UI {
    public class CommandLink : Button {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(HandleRef hWnd, int Msg, IntPtr wParam, StringBuilder lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(HandleRef hWnd, int Msg, ref int wParam, StringBuilder lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(HandleRef hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(HandleRef hWnd, int Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(HandleRef hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(HandleRef hWnd, int Msg, int wParam, ref IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(HandleRef hWnd, int Msg, int wParam, IntPtr lParam);

        private const int BS_COMMANDLINK = 0x0000000E;
        private const int BCM_FIRST = 0x1600;
        private const int BCM_GETIDEALSIZE = BCM_FIRST + 0x1;
        private const int BCM_GETIMAGELIST = BCM_FIRST + 0x3;
        private const int BCM_GETNOTE = BCM_FIRST + 0xa;
        private const int BCM_GETNOTELENGTH = BCM_FIRST + 0xb;
        private const int BCM_SETIMAGELIST = BCM_FIRST + 0x2;
        private const int BCM_SETNOTE = BCM_FIRST + 0x9;
        private const int BM_SETIMAGE = 0x000000f7;

        public CommandLink() {
            this.FlatStyle = FlatStyle.System;
        }


        protected override CreateParams CreateParams {
            get {
                var cParams = base.CreateParams;
                cParams.Style |= BS_COMMANDLINK;
                return cParams;
            }
        }

        public string Description {
            get {
                var length = Convert.ToInt32(SendMessage(new HandleRef(this, this.Handle),
                                 BCM_GETNOTELENGTH,
                                 IntPtr.Zero, IntPtr.Zero)) + 1;

                var sb = new StringBuilder(length);

                SendMessage(new HandleRef(this, this.Handle),
                    BCM_GETNOTE,
                    ref length, sb);

                return sb.ToString();
            }
            set {
                SendMessage(new HandleRef(this, this.Handle),
                    BCM_SETNOTE,
                    IntPtr.Zero, value);
            }
        }

        public void SetImage(Bitmap bitmap) {
            if (bitmap != null) {
                var iconHandle = bitmap.GetHicon();
                SendMessage(new HandleRef(this, this.Handle),
                    BM_SETIMAGE,
                    (IntPtr)1,
                    iconHandle);
            } else {
                SendMessage(new HandleRef(this, this.Handle),
                    BM_SETIMAGE,
                    (IntPtr)1,
                    IntPtr.Zero);
            }
        }
    }
}
