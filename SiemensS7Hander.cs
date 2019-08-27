using System;
using System.Collections.Generic;
using DotNetSiemensPLCToolBoxLibrary.Communication;

namespace Read_Data_from_SiemensS7
{

    class SiemensS7Hander
    {
        private List<PLCConnection> plcConnection; //SPS-Verbindung(en)
        public List<PLCTag> r_tags_con_0 = new List<PLCTag>();

        public void ReadTagList()
        {
            GenTagList();
            startConnection();
            plcConnection[0].ReadValues(r_tags_con_0); //Auslesen

            foreach(PLCTag tag in r_tags_con_0)
            {
                Console.WriteLine("Tag: " + tag.Tag + " Value: " + tag.Value);
            }

            closeConnections();
        }

        private void startConnection()
        {
            plcConnection = new List<PLCConnection>();
            try
            {
                PLCConnection akConn = new PLCConnection("conf");

                //Erstellen der Verbindungskonfiguration
                akConn.Configuration.ConnectionType = LibNodaveConnectionTypes.ISO_over_TCP;
                akConn.Configuration.CpuIP = "192.168.1.205";
                akConn.Configuration.CpuRack = 0;
                akConn.Configuration.CpuSlot = 1;
                akConn.Configuration.ConfigurationType = LibNodaveConnectionConfigurationType.ObjectSavedConfiguration;
                akConn.Configuration.PLCConnectionType = LibNodaveConnectionResource.OP;

                plcConnection.Add(akConn);
                plcConnection[0].Connect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                closeConnections();
                return;
            }
        }

        private void closeConnections()
        {
            if (plcConnection != null)
                foreach (PLCConnection plcConnectioni in plcConnection)
                {
                    plcConnectioni.Dispose();
                }
            plcConnection = null;
        }

        private void GenTagList()
        {
            //Füllstand von Bütte 030B04
            r_tags_con_0.Add(new PLCTag { Tag = "030B04", S7FormatAddress = "DB2.DBD760", TagDataType = DotNetSiemensPLCToolBoxLibrary.DataTypes.TagDataType.Float });
            //Füllstand von Bütte 030B05
            r_tags_con_0.Add(new PLCTag { Tag = "030B05", S7FormatAddress = "DB2.DBD812", TagDataType = DotNetSiemensPLCToolBoxLibrary.DataTypes.TagDataType.Float });
        }

    }
}
