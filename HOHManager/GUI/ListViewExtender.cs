using System;
using System.Windows.Forms;
using Microsoft.WindowsCE.Forms;
using System.Runtime.InteropServices;

namespace HOHManager
{

        public static class ListViewExtender
        {
            private const int LVM_SETEXTENDEDLISTVIEWSTYLE = 0x1000 + 54;
            private const int LVM_GETEXTENDEDLISTVIEWSTYLE = 0x1000 + 55;
            private const int LVM_SETCOLUMNORDERARRAY = 0x1000 + 58;
            private const int LVM_GETCOLUMNORDERARRAY = 0x1000 + 59;

            private const int LVS_EX_HEADERDRAGDROP = 0x00000010;

            public static void SetAllowDraggableColumns(this ListView lv, bool enabled)
            {
                // Add or remove the LVS_EX_HEADERDRAGDROP extended style
                // from the ListView control depending upon the 'enabled'
                // argument.
                Message msg = new Message();
                msg.HWnd = lv.Handle;
                msg.Msg = LVM_SETEXTENDEDLISTVIEWSTYLE;
                msg.WParam = (IntPtr)LVS_EX_HEADERDRAGDROP;
                msg.LParam = enabled ? (IntPtr)LVS_EX_HEADERDRAGDROP : IntPtr.Zero;
                MessageWindow.SendMessage(ref msg);
            }

            public static bool GetAllowDraggableColumns(this ListView lv)
            {
                Message msg = new Message();
                msg.HWnd = lv.Handle;
                msg.Msg = LVM_GETEXTENDEDLISTVIEWSTYLE;
                MessageWindow.SendMessage(ref msg);

                // Check if the LVS_EX_HEADERDRAGDROP bit is set
                return ((int)msg.Result & LVS_EX_HEADERDRAGDROP) == LVS_EX_HEADERDRAGDROP;
            }

            public static void SetColumnOrder(this ListView lv, int[] columnOrder)
            {
                // Ensure that the columnOrder array contains the
                // correct number of elements
                IntPtr ptr = IntPtr.Zero;
                if (columnOrder.Length != lv.Columns.Count)
                    throw new ArgumentException("Array length does not match the number of columns", "columnOrder");

                try
                {
                    // Copy the managed int[] array into unmanaged memory.
                    // Alternatively we could use an unsafe code block and
                    // lock the columnOrder array to obtain its address...
                    ptr = Marshal.AllocHGlobal(columnOrder.Length * 4);
                    for (int i = 0; i < columnOrder.Length; i++)
                        Marshal.WriteInt32((IntPtr)((int)ptr + (i * 4)), columnOrder[i]);

                    // Send the window message which informs the
                    // list view to re-order the columns.
                    Message msg = new Message();
                    msg.HWnd = lv.Handle;
                    msg.Msg = LVM_SETCOLUMNORDERARRAY;
                    msg.LParam = ptr;
                    msg.WParam = (IntPtr)columnOrder.Length;
                    MessageWindow.SendMessage(ref msg);
                }
                finally
                {
                    // Free the unmanaged memory allocated above.
                    if (ptr != IntPtr.Zero)
                        Marshal.FreeHGlobal(ptr);
                }
            }

            public static int[] GetColumnOrder(this ListView lv)
            {
                Int32[] order = new Int32[lv.Columns.Count];
                IntPtr ptr = IntPtr.Zero;

                try
                {
                    // Allocate enough native memory for the native
                    // ListView control to store its ordering information
                    ptr = Marshal.AllocHGlobal(lv.Columns.Count * 4);

                    // Send the LVM_GETCOLUMNORDERARRAY message to
                    // the listview control, passing it the address
                    // of our buffer allocated above
                    Message msg = new Message();
                    msg.HWnd = lv.Handle;
                    msg.Msg = LVM_GETCOLUMNORDERARRAY;
                    msg.LParam = ptr;
                    msg.WParam = (IntPtr)order.Length;
                    MessageWindow.SendMessage(ref msg);

                    // Read the indexes stored in our native
                    // buffer and place in our managed array
                    for (int i = 0; i < lv.Columns.Count; i++)
                        order[i] = Marshal.ReadInt32((IntPtr)((int)ptr + (i * 4)));
                }
                finally
                {
                    // Free the unmanaged memory allocated above.
                    if (ptr != IntPtr.Zero)
                        Marshal.FreeHGlobal(ptr);
                }

                return order;
            }

        }

}
