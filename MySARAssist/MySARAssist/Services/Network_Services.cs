using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySARAssist.Models;
using MySARAssist.ResourceClasses;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections.TCP;
using NetworkCommsDotNet.Tools;

namespace MySARAssist.Services
{
    public static class Network_Services
    {
        
        //Dictionary<ShortGuid, NetworkSendObject> lastPeerSendObjectDict = new Dictionary<ShortGuid, NetworkSendObject>();
        public static bool SendNetworkObject(object objToSend, string comment = null, string ip = null, string port = null)
        {
            bool successful = false;

            if (objToSend != null)
            {
                NetworkSendObject networkSendObject = new NetworkSendObject();
                switch (objToSend)
                {

                  
                    case Guid g:
                        networkSendObject.GuidValue = g;
                        break;
                /*
                    case SignInRecord sir:
                        networkSendObject.signInRecord = sir;
                        break;*/
                    case TeamMember member:
                        networkSendObject.teamMember = member;
                        break;
                   
                    case List<TeamMember> memberList:
                        networkSendObject.memberList = memberList;
                        break;
                  
                }
                networkSendObject.RequestID = Guid.NewGuid();
                networkSendObject.objectType = objToSend.GetType().ToString();
                networkSendObject.SourceName = HostInfo.HostName;
                networkSendObject.SourceIdentifier = NetworkComms.NetworkIdentifier;
                networkSendObject.comment = comment;


                string iptouse = ip;
                int porttouse = int.Parse(port);
                if (ip != null) { iptouse = ip; }
                if (port != null)
                {
                    int.TryParse(port, out porttouse);
                }
                DateTime today = DateTime.Now;

                //We may or may not have entered some server connection information
                ConnectionInfo serverConnectionInfo = null;
                if (!string.IsNullOrEmpty(iptouse))
                {
                    try { serverConnectionInfo = new ConnectionInfo(iptouse, porttouse); }
                    catch (Exception)
                    {
                        //addToNetworkLog(string.Format(Globals.cultureInfo, "{0:HH:mm:ss}", today) + " Error - Failed to parse the server IP and port. Please ensure it is correct and try again\r\n\r\n");
                        //MessageBox.Show("Failed to parse the server IP and port. Please ensure it is correct and try again", "Server IP & Port Parse Error", MessageBoxButtons.OK);
                        return false;
                    }
                }
                // ShortGuid networkIdentifier = ShortGuid.NewGuid();

                //lock (lastPeerSendObjectDict) lastPeerSendObjectDict[networkIdentifier] = networkSendObject;




                if (serverConnectionInfo != null)
                {
                    //We perform the send within a try catch to ensure the application continues to run if there is a problem.
                    try
                    {


                        using (TCPConnection connection = TCPConnection.GetConnection(serverConnectionInfo))
                        {
                            connection.SendObject("NetworkSendObject", networkSendObject);
                        }
                        //errors.Add(string.Format(Globals.cultureInfo, "{0:HH:mm:ss}", today) + " - sent " + networkSendObject.objectType + " - " + networkSendObject.comment + "\r\n");
                    }
                    catch (CommsException ce)
                    {

                        //errors.Add(string.Format(Globals.cultureInfo, "{0:HH:mm:ss}", today) + " Error - " + ce.ToString() + "\r\n\r\n");
                        //MessageBox.Show("A CommsException occurred while trying to send a " + networkSendObject.GetType() + " to " + serverConnectionInfo + "\r\n\r\n" + ce.ToString(), "Network Comms Exception", MessageBoxButtons.OK);
                        //MessageBox.Show("A CommsException occurred while trying to send message to " + serverConnectionInfo, "CommsException", MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        //errors.Add(string.Format(Globals.cultureInfo, "{0:HH:mm:ss}", today) + " Error - " + ex.ToString() + "\r\n\r\n");
                        // MessageBox.Show("A CommsException occurred while trying to send a " + networkSendObject.GetType() + " to " + ex.ToString(), "CommsException", MessageBoxButtons.OK);
                    }

                }


                //If we have any other connections we now send the message to those as well
                //This ensures that if we are the server everyone who is connected to us gets our message
                var otherConnectionInfos = (from current in NetworkComms.AllConnectionInfo() where current != serverConnectionInfo select current).ToArray();
                foreach (ConnectionInfo info in otherConnectionInfos)
                {
                    //We perform the send within a try catch to ensure the application continues to run if there is a problem.
                    try { TCPConnection.GetConnection(info).SendObject("NetworkSendObject", networkSendObject); }
                    catch (CommsException ce)
                    {
                       
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("A CommsException occurred while trying to send message to " + ex.ToString(), "CommsException", MessageBoxButtons.OK);

                    }
                }


                /*

                //If we provided server information we send to the server first
                if (serverConnectionInfo != null)
                {
                    //We perform the send within a try catch to ensure the application continues to run if there is a problem.
                    try
                    {
                        //ProtobufSerializer serializer = new ProtobufSerializer();
                        //SendReceiveOptions sendReceiveOptions = new SendReceiveOptions();

                        //NetworkComms.SendObject("Message", iptouse, porttouse, networkSendObject);

                        using (TCPConnection connection = TCPConnection.GetConnection(serverConnectionInfo))
                        {



                            connection.SendObject("NetworkSendObject", networkSendObject);
                        }
                        successful = true;
                       // addToNetworkLog(string.Format(Globals.cultureInfo, "{0:HH:mm:ss}", today) + " - sent " + networkSendObject.objectType + " - " + networkSendObject.comment + "\r\n");
                    }
                 
                    catch (Exception ex)
                    {
                        return false;
                        //addToNetworkLog(string.Format(Globals.cultureInfo, "{0:HH:mm:ss}", today) + " Error - " + ex.ToString() + "\r\n\r\n");
                        // MessageBox.Show("A CommsException occurred while trying to send a " + networkSendObject.GetType() + " to " + ex.ToString(), "CommsException", MessageBoxButtons.OK);
                    }

                }*/

                return successful;
            } else { return false; }
        }
    }
}
