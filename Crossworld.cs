using System;
using System.Runtime.InteropServices;
using Dalamud.Logging;

namespace FFLogsLookup
{
    public unsafe class Crossworld
    {
        delegate IntPtr InfoProxyCrossRealm_GetPtr();
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        static InfoProxyCrossRealm_GetPtr InfoProxyCrossRealm_GetPtrDelegate;
        public delegate byte GetCrossRealmPartySize();
        public static GetCrossRealmPartySize getCrossRealmPartySize;
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public static void Init()
        {
            IntPtr ipcr_ptr = Service.SigScanner.ScanText("48 8B 05 ?? ?? ?? ?? C3 CC CC CC CC CC CC CC CC 40 53 41 57");
            InfoProxyCrossRealm_GetPtrDelegate = Marshal.GetDelegateForFunctionPointer<InfoProxyCrossRealm_GetPtr>(ipcr_ptr);
            PluginLog.Information("InfoProxyCrossRealm_GetPtr: 0x" + ipcr_ptr.ToString("X"));

            IntPtr gcrps_ptr = Service.SigScanner.ScanText("48 83 EC 28 E8 ?? ?? ?? ?? 84 C0 74 3C");
            getCrossRealmPartySize = Marshal.GetDelegateForFunctionPointer<GetCrossRealmPartySize>(gcrps_ptr);
            PluginLog.Information("GetCrossRealmPartySize: 0x" + gcrps_ptr.ToString("X"));
        }

        public static (byte HomeWorld, string Name) GetCrossRealmPlayer(int index)
        {
            IntPtr playerPtr = InfoProxyCrossRealm_GetPtrDelegate() + 0x3c2 + 0x50 * index;
            #pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return (*(byte*)playerPtr, Marshal.PtrToStringUTF8(playerPtr + 0x8));
            #pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }
    }
}
