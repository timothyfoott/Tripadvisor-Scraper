using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Runtime.InteropServices;

namespace internetconnectstatus
{
    class internet_connect_status
    {

        //flag for internet connect 

        InternetConnectionState flags = 0; 
        
        //import windows DLL for internet connectin
        
        [DllImport("WININET", CharSet = CharSet.Auto)]    
        static extern bool InternetGetConnectedState(ref InternetConnectionState lpdwFlags, int dwReserved);
        [Flags]

        //enum  for intrenet status

        enum InternetConnectionState : int
        {
            INTERNET_CONNECTION_MODEM = 0x1,
            INTERNET_CONNECTION_LAN = 0x2,
            INTERNET_CONNECTION_PROXY = 0x4,
            INTERNET_RAS_INSTALLED = 0x10,
            INTERNET_CONNECTION_OFFLINE = 0x20,
            INTERNET_CONNECTION_CONFIGURED = 0x40
        }

        public bool Isinternetisconnected()
        {
            bool isConnected = false;
            isConnected = InternetGetConnectedState(ref flags, 0);
            return isConnected;
        }
    }
}