using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;
using static MagickaPUP.Utility.Interop.DirectX.D3DX.D3DXInternal;

namespace MagickaPUP.Utility.Interop.DirectX
{
    public static class D3DX
    {

        

        public static class D3DXInternal
        {
            // Constants
            public const int D3D_SDK_VERSION = 32;
            public const int D3DDEVTYPE_HAL = 1;
            public const int D3DCREATE_SOFTWARE_VERTEXPROCESSING = 0x20;
            public const int D3DSWAPEFFECT_DISCARD = 1;

            // Structs
            [StructLayout(LayoutKind.Sequential)]
            public struct D3DPRESENT_PARAMETERS
            {
                public uint BackBufferWidth;
                public uint BackBufferHeight;
                public uint BackBufferFormat;
                public uint BackBufferCount;
                public uint MultiSampleType;
                public uint MultiSampleQuality;
                public uint SwapEffect;
                public IntPtr hDeviceWindow;
                public bool Windowed;
                public bool EnableAutoDepthStencil;
                public uint AutoDepthStencilFormat;
                public uint Flags;
                public uint FullScreen_RefreshRateInHz;
                public uint PresentationInterval;
            }

            // Functions
            [DllImport("d3d9.dll", CallingConvention = CallingConvention.StdCall)]
            public static extern IntPtr Direct3DCreate9(uint SDKVersion);
        }

        public static unsafe IntPtr D3DCreateDevice(IntPtr windowHandle)
        {
            IntPtr d3d9 = D3DXInternal.Direct3DCreate9(D3DXInternal.D3D_SDK_VERSION);
            if (d3d9 == IntPtr.Zero)
                throw new Exception("Failed to Create D3D9 Interface {PTR = 0}");

            D3DXInternal.D3DPRESENT_PARAMETERS pp = new D3DXInternal.D3DPRESENT_PARAMETERS
            {
                Windowed = true,
                SwapEffect = D3DXInternal.D3DSWAPEFFECT_DISCARD,
                hDeviceWindow = windowHandle
            };

            IntPtr* vtable = *(IntPtr**)d3d9;
            delegate* unmanaged[Stdcall]<IntPtr, uint, int, IntPtr, uint, D3DXInternal.D3DPRESENT_PARAMETERS*, IntPtr*, int> createDevice = (delegate* unmanaged[Stdcall]<IntPtr, uint, int, IntPtr, uint, D3DXInternal.D3DPRESENT_PARAMETERS*, IntPtr*, int>)vtable[16];

            IntPtr devicePtr = IntPtr.Zero;
            int hr = createDevice(d3d9, 0, D3DXInternal.D3DDEVTYPE_HAL, windowHandle, D3DXInternal.D3DCREATE_SOFTWARE_VERTEXPROCESSING, &pp, &devicePtr);
            if (hr < 0)
                throw new Exception($"Failed to Create D3D9 Device (HRESULT = {hr})");

            ComHelper.Release(d3d9);
        }
        


        static void CreateEffectEx()
        { }










        [SuppressUnmanagedCodeSecurity]
        [DllImport("d3dx9_43.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int D3DXDisassembleEffect(
            IntPtr effect,
            [MarshalAs(UnmanagedType.U1)] bool flags,
            out IntPtr output
        );

        [DllImport("d3dcompiler_47.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int D3DDisassemble(IntPtr pSrcData, IntPtr SrcDataSize, uint Flags, string szComments, out IntPtr ppDisassembly);

        [ComImport, Guid("8BA5FB08-5195-40e2-AC58-0D989C3A0102"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ID3DBlob
        {
            IntPtr GetBufferPointer();
            int GetBufferSize();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LocalFree(IntPtr hMem);

        public static string DisassembleEffect2(byte[] code, int start, int length)
        {
            IntPtr assemblyPtr = IntPtr.Zero;
            IntPtr disassemblyPtr = IntPtr.Zero;
            int result;

            assemblyPtr = Marshal.AllocHGlobal(length);
            Marshal.Copy(code, start, assemblyPtr, length);

            result = D3DXDisassembleEffect(assemblyPtr, false, out disassemblyPtr);

            if (result != 0 || disassemblyPtr == IntPtr.Zero)
            {
                throw new Exception($"D3DXDisassembleEffect failed with HRESULT value : {result}");
            }

            string ans = Marshal.PtrToStringAnsi(disassemblyPtr);

            LocalFree(disassemblyPtr);
            Marshal.FreeHGlobal(assemblyPtr);

            return ans;
        }

        public static string DisassembleEffect(byte[] bytecode, int start, int length)
        {
            IntPtr dataPtr = Marshal.AllocHGlobal(length);
            Marshal.Copy(bytecode, start, dataPtr, length);

            IntPtr disassemblyPtr;
            int hr = D3DDisassemble(
                dataPtr,
                new IntPtr(length),
                0,            // or use flags like 0x02 for color-coded output
                null,         // optional comment
                out disassemblyPtr
            );

            Marshal.FreeHGlobal(dataPtr);

            if (hr != 0 || disassemblyPtr == IntPtr.Zero)
                throw new Exception($"D3DDisassemble failed with HRESULT: 0x{hr:X}");

            ID3DBlob blob = (ID3DBlob)Marshal.GetObjectForIUnknown(disassemblyPtr);
            string output = Marshal.PtrToStringAnsi(blob.GetBufferPointer());

            Marshal.Release(disassemblyPtr);
            return output;
        }

        public static string DisassembleEffect(byte[] code)
        {
            // IntPtr p;
            // D3DXDisassembleEffect(IntPtr.Zero, false, out p);

            var form = DummyFormHandler.CreateDummyForm();
            DummyFormHandler.DestroyDummyForm(form);

            return "fsa";
            // return DisassembleEffect(code, 0, code.Length);
        }
    }
}
