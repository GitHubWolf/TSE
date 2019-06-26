using InActionLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TSE
{
    public partial class FormUDPHelper : Form
    {
        /*Use Socket to listen for UDP datagram, also to join/leave multicast group.*/
        Socket udpClientSocket = null;

        /*Port number to receive data.*/
        int listeningPort = 0;

        /*Thread to receive data.*/
        Thread udpReceiverThread = null;
        bool continueToRun = true;

        //Udp socket buffer.
        int socketBufferSize = 4 * 1024 * 1024;

        /*Define a delegate function so that the worker thread can access GUI.*/
        delegate void DelegateShowUDPPacket(byte[] datagram);

        public FormUDPHelper()
        {
            InitializeComponent();
        }

        /*To join or to leave multicast group.*/
        private void JoinMulticastGroup(bool toJoin)
        {
            Result result = new Result();
            IPAddress multicastAddress = null;

            if (null == udpClientSocket)
            {
                MessageBox.Show("Please start listening first.", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                result.SetResult(ResultCode.INVALID_DATA);
            }

            if (result.Fine)
            {
                /*Check whether the IP address is valid.*/
                if (!IPAddress.TryParse(textBoxMulticastAddress.Text, out multicastAddress))
                {
                    MessageBox.Show("Please set multicast address correctly!", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxMulticastAddress.Focus();
                    result.SetResult(ResultCode.INVALID_DATA);
                }
            }

            if (result.Fine)
            {
                try
                {
                    if (toJoin)
                    {
                        /*To join a multicast group.*/
                        udpClientSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(multicastAddress));
                        udpClientSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 10);//TTL.

                        MessageBox.Show("You have joined the multicast group successfully!", "Operation Succeeded", MessageBoxButtons.OK);
                    }
                    else
                    {
                        /*To leave a multicast group.*/
                        udpClientSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.DropMembership, new MulticastOption(multicastAddress));
                        MessageBox.Show("You have left the multicast group successfully!", "Operation Succeeded", MessageBoxButtons.OK);
                    }

                }
                catch (SocketException)
                {
                    if (toJoin)
                    {
                        MessageBox.Show("You have joined the multicast group before! ", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("You have NEVER joined the multicast group before! ", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }//JoinMulticastGroup

        private void StartReceiveData()
        {
            byte[] buffer = new byte[socketBufferSize];

            while (continueToRun)
            {
                try
                {
                    int receivedDataLength = udpClientSocket.Receive(buffer);

                    if (receivedDataLength > 0)
                    {
                        //Make a copy and post it to the form to display.
                        byte[] dataToShow = new byte[receivedDataLength];
                        Array.Copy(buffer, 0, dataToShow, 0, receivedDataLength);
                        this.Invoke(new DelegateShowUDPPacket(ShowDataReceived), dataToShow);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }//while (continueToRun)

        }//StartReceiveData

        private void ShowDataReceived(byte[] dataToShow)
        {
            /*String in HEX format.*/
            string hexString = "";

            /*Convert it to a HEX array.*/
            hexString = Utility.GetHexString(dataToShow, 16);

            /*Show the message directly. NOTE! In this way, we may lose some datagrams.*/
            textBoxPacketsReceived.AppendText(hexString);

            textBoxPacketsReceived.AppendText(Environment.NewLine);
        }//ShowDataReceived

        private void buttonStartListening_Click(object sender, EventArgs e)
        {
            int currentPortNumber = 0;
            Result result = new Result();

            if (result.Fine)
            {
                /*Check whether the port number has been filled in correctly.*/
                if (!Int32.TryParse(textBoxListeningPort.Text, out currentPortNumber))
                {
                    MessageBox.Show("Please set port number correctly!", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxListeningPort.Focus();
                    result.SetResult(ResultCode.INVALID_DATA);
                }
            }

            if (result.Fine)
            {
                /*Here is the new port number.*/
                listeningPort = currentPortNumber;

                try
                {
                    /*Create a UDP client and bind it to the port.*/
                    udpClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                    if (null != udpClientSocket)
                    {
                        udpClientSocket.Bind(new IPEndPoint(IPAddress.Any, listeningPort));
                        udpClientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, socketBufferSize);//4M bytes buffer.
                        udpClientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 500);//0.5 second as timeout.
                        udpClientSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 10);//TTL.

                        //Create a thread to listen on this socket.
                        udpReceiverThread = new Thread(new ThreadStart(StartReceiveData));
                    }

                    if(null != udpReceiverThread)
                    {
                        udpReceiverThread.IsBackground = true;
                        udpReceiverThread.Start();
                    }

                }
                catch (Exception ex)
                {
                    if(null != udpReceiverThread)
                    {
                        udpReceiverThread.Abort();
                        udpReceiverThread = null;
                    }
                    if(null != udpClientSocket)
                    {
                        udpClientSocket.Close();
                        udpClientSocket = null;
                    }

                    result.SetFailure();
                    MessageBox.Show("Failed to start the receiver. The port may have been used by other application.", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex);
                }
            }

            if (result.Fine)
            {
                buttonStopListening.Enabled = true;
                buttonStartListening.Enabled = false;
            }
        }//buttonStartListening_Click

        private void buttonStopListening_Click(object sender, EventArgs e)
        {
            ShutDownSocket();

            buttonStopListening.Enabled = false;
            buttonStartListening.Enabled = true;
        }//buttonStopListening_Click

        private void ShutDownSocket()
        {
            /*Try to stop the worker thread.*/
            if (null != udpReceiverThread)
            {
                continueToRun = false;

                if (udpReceiverThread.IsAlive)
                {
                    udpReceiverThread.Join();
                    udpReceiverThread = null;
                }
            }

            if (null != this.udpClientSocket)
            {
                udpClientSocket.Close();
                udpClientSocket = null;
            }
        }

        private void buttonJoinMulticastGroup_Click(object sender, EventArgs e)
        {
            JoinMulticastGroup(true);
        }//buttonJoinMulticastGroup_Click

        private void buttonLeaveMulticastGroup_Click(object sender, EventArgs e)
        {
            JoinMulticastGroup(false);
        }//buttonLeaveMulticastGroup_Click

        private void buttonSendData_Click(object sender, EventArgs e)
        {
            Result result = new Result();
            IPAddress destAddress = null;
            int destPort = 0;

            if (result.Fine)
            {
                /*Check whether the IP address is valid.*/
                if (!IPAddress.TryParse(textBoxDestinationIP.Text, out destAddress))
                {
                    MessageBox.Show("Please set destination address correctly!", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxDestinationIP.Focus();
                    result.SetFailure();
                }
            }

            if (result.Fine)
            {
                /*Check whether the port number has been filled in correctly.*/
                if (!Int32.TryParse(textBoxDestinationPort.Text, out destPort))
                {
                    MessageBox.Show("Please set destination port number correctly!", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxDestinationPort.Focus();
                    result.SetFailure();
                }
            }

            byte[] binaryData = null;
            int binaryLength = 0;
            if (result.Fine)
            {
                /*Convert the input data from HEX string to binary.*/
                if(!textBoxDataToSend.GetByteArray(out binaryData, ref binaryLength))
                {
                    result.SetResult(ResultCode.INVALID_DATA);
                }
            }

            if (result.Fine)
            {
                try
                {
                    Socket tempSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                    if (null != tempSocket)
                    {
                        tempSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 10);//TTL.

                        int sentDataLength = tempSocket.SendTo(binaryData, 0, binaryLength,SocketFlags.None, new IPEndPoint(destAddress, destPort));
                        if (sentDataLength != binaryLength)
                        {
                            MessageBox.Show(binaryLength + " bytes of data have been sent.", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Failed to send data", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }//buttonSendData_Click

        private void UdpForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
