using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Utility.Interop.Com
{
    public static class ComHelper
    {
        /*
        public static unsafe void Release(IntPtr comPtr)
        {
            // NOTE : All of these are possible alternatives that do not seem to work well with D3D9, or maybe I'm doing something wrong, idk.
            // I'm just keeping these calls here just in case any of them becomes useful in the future...
            // Marshal.ReleaseComObject(ptr);
            // Marshal.Release(ptr);
            // Marshal.FinalReleaseComObject(ptr);

            if (comPtr == IntPtr.Zero)
                return;

            IntPtr* vtable = *(IntPtr**)comPtr;
            delegate* unmanaged[Stdcall]<IntPtr, uint> release = (delegate* unmanaged[Stdcall]<IntPtr, uint>)vtable[2];
            release(comPtr);
        }
        */
    }
}
