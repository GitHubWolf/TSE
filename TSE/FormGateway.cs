using InActionLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DTAPINET;
using System.Net.NetworkInformation;

namespace TSE
{
    public partial class FormGateway : Form
    {
        //Valid when the input is IP network.
        private int inputNetworkPortNumber = 0;
        private bool isMulticastInput = false;
        private IPAddress inputMulticastAddress = null;
        /*Use Socket to listen for UDP datagram, also to join/leave multicast group.*/
        private Socket udpSocketToReceiveData = null;
        private const int SOCKET_BUFFER_SIZE = 8*1024*1024;

        //Valid when to output to file.
        private bool toOutputToFile = false;
        private string outputFileName = null;
        private FileStream outputFileStream = null;

        //Valid when to output to network.
        private bool toOutputToNetwork = false;
        private int outputNetworkPort = 0;
        private IPAddress outputNetworkAddress = null;
        private Socket udpSocketToSendData = null;
        private IPEndPoint outputNetworkTarget = null;

        //Valid when to output to a device.
        private static int DEVICE_COUNT = 16;
        private bool toOutputToDevice = false;
        private DtHwFuncDesc currentOutputHwFuncDesc;
        private DtHwFuncDesc[] dtOutputHwFuncDesc = new DtHwFuncDesc[DEVICE_COUNT];
        private DtDevice dtOutputDevice = new DtDevice();
        private DtOutpChannel dtOutputChannel = new DtOutpChannel();
        private int deviceFifoSize = 0;


        /*Thread to receive data.*/
        Thread receiverThread = null;
        //Thread to process the received data, i.e. to save the data into the file or re-broadcast it into the network.
        Thread processorThread = null;
        bool continueToRun = true;

        //Save all the message notification from the receiver.
        private Queue<MessageNotification> messageQueue = new Queue<MessageNotification>();
        private Semaphore dataSemaphore = new Semaphore(0, Int32.MaxValue);

        //To be used for Dektec device as an input.
        private DtDevice dtInputDevice = new DtDevice();
        private DtInpChannel dtInputChannel = new DtInpChannel();
        private static int DEVICE_BUFFER_SIZE = 188*7*100;//Every 7 packets will be grouped into a UDP packet.
        private DtHwFuncDesc[] dtInputHwFuncDesc = new DtHwFuncDesc[DEVICE_COUNT];
        private DtHwFuncDesc currentInputHwFuncDesc;

        //Valid when to input from the file.
        private string inputFileName = null;
        private FileStream inputFileStream = null;
        private Int64 playoutBitrate = 0;
        private bool loopback = false;
        private int playingProgress = 0;
        private int tsPacketSize = 0;

        //Current bitrate.
        private Int64 bitrate = 0;
        private Int64 totalSize = 0;
        private TimeSpan startTime;

        public FormGateway()
        {
            InitializeComponent();
        }

        private void checkBoxMulticast_CheckedChanged(object sender, EventArgs e)
        {
            labelMulticastIp.Enabled = ((CheckBox)sender).Checked;
            textBoxMulticastAddress.Enabled = ((CheckBox)sender).Checked;
        }

        private void checkBoxLocalFile_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxOutputFile.Enabled = ((CheckBox)sender).Checked;
        }

        private void checkBoxOutputToNetwork_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxOutputToNetwork.Enabled = ((CheckBox)sender).Checked;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            //Open a bitstream file.
            SaveFileDialog saveStreamFileDialog = new SaveFileDialog();
            saveStreamFileDialog.Filter = "Transport stream files(*.ts;*.mpeg;*.mpg;*.m2t)|*.ts;*.mpeg;*.mpg;*.m2t|All files(*.*)|*.*";
            saveStreamFileDialog.Title = "Set output stream";
            saveStreamFileDialog.CheckPathExists = true;
            if (DialogResult.OK == saveStreamFileDialog.ShowDialog())
            {
                textBoxOutputFile.Text = saveStreamFileDialog.FileName;
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Result result = new Result();

            //Set the flag so that we can run it.
            continueToRun = true;

            if (0 == tabControlInput.SelectedIndex)
            {
                //Input is the device.
                result = CheckDeviceInputConfig();
            }
            else if (1 == tabControlInput.SelectedIndex)
            { 
                //Input is the network.

                result = CheckNetworkInputConfig();
            }
            else if (2 == tabControlInput.SelectedIndex)
            {
                //Input is the local file.
                result = CheckFileInputConfig();
            }

            if (result.Fine)
            {
                result = CheckOutputConfig();
            }

            if (result.Fine)
            {
                //Create the worker threads. One to receive data, the other to save data.

                if (0 == tabControlInput.SelectedIndex)
                {
                    //Create thread to receive data from device.
                    result = StartDeviceReceiver();
                }
                else if (1 == tabControlInput.SelectedIndex)
                {
                    //Create thread to receive data from network.
                    result = StartUdpReceiver();
                }
                else if (2 == tabControlInput.SelectedIndex)
                {
                    //Create thread to read data from local file.
                    result = StartFileReader();
                }
            }

            if (result.Fine)
            {
                //Create the worker thread to save the data into the file or re-broadcast the data into the IP network.
                result = StartDataProcessor();
            }

            if (result.Fine)
            {
                //Reset it every time.
                bitrate = 0;
                totalSize = 0;
                startTime = new TimeSpan(DateTime.Now.Ticks);

                textBoxStartTime.Text = DateTime.Now.ToLocalTime().ToString();


                //Timer to update the UI.
                timerToUpdateUI.Enabled = true;

                buttonStart.Enabled = false;
                buttonStop.Enabled = true;
            }
        }


        private void buttonStop_Click(object sender, EventArgs e)
        {
            Result result = new Result();

            if (result.Fine)
            {
                CleanUp();
            }

            if (result.Fine)
            {
                //Timer to update the UI.
                timerToUpdateUI.Enabled = false;

                buttonStart.Enabled = true;
                buttonStop.Enabled = false;
            }
        }


        private Result StartUdpReceiver()
        {
            Result result = new Result();

            //Try to create the socket and bind to the port.
            try
            {
                /*Create a UDP client and bind it to the port.*/
                udpSocketToReceiveData = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                if (null != udpSocketToReceiveData)
                {
                    KeyValuePair<IPAddress, int> networkIndexIpPair = (KeyValuePair<IPAddress, int>)listBoxInputNetworkInterfaces.SelectedItem;
 
                    udpSocketToReceiveData.Bind(new IPEndPoint(networkIndexIpPair.Key, inputNetworkPortNumber));//Use the selected IP address.
                    udpSocketToReceiveData.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, SOCKET_BUFFER_SIZE);//8M bytes buffer.
                    udpSocketToReceiveData.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 100);//0.1 second as timeout.

                    //Join multicast group.
                    if (isMulticastInput)
                    {
                        //Set multicase input interface, so that multicast data can be received from this interface?Necessary to do so?
                        udpSocketToReceiveData.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastInterface, IPAddress.NetworkToHostOrder(networkIndexIpPair.Value));

                        udpSocketToReceiveData.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(inputMulticastAddress));
                        udpSocketToReceiveData.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 10);//TTL.


                    }
                }

                //Create a thread to listen on this socket.
                receiverThread = new Thread(new ThreadStart(UdpDataReceiverEntry));

                if (null != receiverThread)
                {
                    receiverThread.IsBackground = true;
                    receiverThread.Start();
                }

            }
            catch (Exception ex)
            {
                if (null != receiverThread)
                {
                    receiverThread.Abort();
                    receiverThread = null;
                }
                if (null != udpSocketToReceiveData)
                {
                    udpSocketToReceiveData.Close();
                    udpSocketToReceiveData = null;
                }

                result.SetFailure();
                MessageBox.Show("Failed to start the receiver. The port may have been used by other application.", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex);
            }

            return result;
        }

        private Result StartFileReader()
        {
            Result result = new Result();

            try
            {
                //Create a thread to read data from the file.
                receiverThread = new Thread(new ThreadStart(FileReaderEntry));

                if (null != receiverThread)
                {
                    receiverThread.IsBackground = true;
                    receiverThread.Start();
                }

            }
            catch (Exception ex)
            {
                if (null != receiverThread)
                {
                    receiverThread.Abort();
                    receiverThread = null;
                }

                result.SetFailure();
                MessageBox.Show("Failed to start the file reader.", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex);
            }

            return result;
        }
        private Result CheckOutputConfig()
        {
            Result result = new Result();
            toOutputToFile = checkBoxOutputToFile.Checked;
            toOutputToNetwork = checkBoxOutputToNetwork.Checked;
            toOutputToDevice = checkBoxOutputToDevice.Checked;

            if (result.Fine && toOutputToFile)
            {
                //Check whether the output file name is valid.

                try
                {
                    FileStream outputFileStreamTemp = new FileStream(textBoxOutputFile.Text, FileMode.Create, FileAccess.Write);
                    outputFileStreamTemp.Close();

                    toOutputToFile = true;
                    outputFileName = textBoxOutputFile.Text;
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid output file name. Please enter a valid file name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result.SetResult(ResultCode.FAILED_TO_OPEN_FILE);
                    textBoxOutputFile.Focus();
                }
            }

            if (result.Fine && toOutputToNetwork)
            {
                /*Check whether the IP address is valid.*/
                if (!IPAddress.TryParse(textBoxDestinationIP.Text, out outputNetworkAddress))
                {
                    MessageBox.Show("Please set destination address correctly!", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxDestinationIP.Focus();
                    result.SetFailure();
                }
            }

            if (result.Fine && toOutputToNetwork)
            {
                /*Check whether the port number has been filled in correctly.*/
                if (!Int32.TryParse(textBoxDestinationPort.Text, out outputNetworkPort))
                {
                    MessageBox.Show("Please set destination port number correctly!", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxDestinationPort.Focus();
                    result.SetFailure();
                }
            }

            if (result.Fine && toOutputToNetwork)
            {
                /*Check whether a output network interface is selected.*/
                if (null == listBoxOutputNetworkInterfaces.SelectedItem)
                {
                    MessageBox.Show("Please set a network interface!", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listBoxOutputNetworkInterfaces.Focus();
                    result.SetFailure();
                }
                else
                {

                }
            }

            if (result.Fine && toOutputToDevice)
            {
                result = CheckDeviceOutputConfig();
            }

            return result;
        }

        private Result CheckNetworkInputConfig()
        {
            Result result = new Result();

            /*Check whether a output network interface is selected.*/
            if (null == listBoxInputNetworkInterfaces.SelectedItem)
            {
                MessageBox.Show("Please set a network interface!", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                listBoxInputNetworkInterfaces.Focus();
                result.SetFailure();
            }


            if (result.Fine)
            {
                /*Check whether the port number has been filled in correctly.*/
                if (!Int32.TryParse(textBoxListeningPort.Text, out inputNetworkPortNumber))
                {
                    MessageBox.Show("Please set port number correctly!", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxListeningPort.Focus();
                    result.SetResult(ResultCode.INVALID_DATA);
                }
            }

            if (result.Fine)
            {
                isMulticastInput = false;
                if (checkBoxMulticast.Checked)
                {
                    /*Check whether the IP address is valid.*/
                    if (!IPAddress.TryParse(textBoxMulticastAddress.Text, out inputMulticastAddress))
                    {
                        MessageBox.Show("Please set multicast address correctly!", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxMulticastAddress.Focus();
                        result.SetFailure();
                    }

                    //Check whether it is a valid multicast address.
                    if (result.Fine)
                    {
                        if (inputMulticastAddress.IsIPv6Multicast)
                        {
                        }
                        else
                        {
                            byte[] addressBytes = inputMulticastAddress.GetAddressBytes();

                            //Check the last byte.
                            if ((addressBytes[0] >= 224) && (addressBytes[0] <= 240))
                            {
                                //A valid IPV4 multicast address.
                            }
                            else
                            {
                                //Invalid multicast address.
                                MessageBox.Show("Invalid multicast address!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                textBoxMulticastAddress.Focus();
                                result.SetResult(ResultCode.INVALID_DATA);
                            }
                        }
                    }

                    if (result.Fine)
                    {
                        isMulticastInput = true;
                    }
                }
            }

            return result;
        }

        private Result CheckFileInputConfig()
        {
            Result result = new Result();

            //Check whether the input file name is valid.
            try
            {
                if (null != inputFileStream)
                {
                    inputFileStream.Close();
                    inputFileStream = null;
                }

                inputFileStream = new FileStream(textBoxInputFile.Text, FileMode.Open, FileAccess.Read);

                inputFileName = textBoxInputFile.Text;
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input file name. Please enter a valid file name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result.SetResult(ResultCode.FAILED_TO_OPEN_FILE);
                textBoxInputFile.Focus();
            }

            if (result.Fine)
            {
                if (null == inputFileStream)
                {
                    result.SetResult(ResultCode.FAILED_TO_OPEN_FILE);
                }
            }


            if (result.Fine)
            {
                //Check bitrate.
                string bitrateText = maskedTextBoxBitrate.Text.Trim();
                if (bitrateText == "")
                {
                    MessageBox.Show(null, "Please set a bitrate!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    maskedTextBoxBitrate.Focus();
                    result.SetResult(ResultCode.INVALID_DATA);
                }
                else
                {
                    Int64 bitrateFromUI = Convert.ToInt64(bitrateText);
                    if (bitrateFromUI <= 0)
                    {
                        MessageBox.Show(null, "Invalid bitrate!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        maskedTextBoxBitrate.Focus();
                        result.SetResult(ResultCode.INVALID_DATA);
                    }
                    else
                    {
                        playoutBitrate = bitrateFromUI;
                    }
                }
            }

            loopback = checkBoxLoop.Checked;
            return result;
        }

        private void UdpDataReceiverEntry()
        {
            byte[] buffer = new byte[SOCKET_BUFFER_SIZE];
            TimeSpan previousTime = new TimeSpan(DateTime.Now.Ticks);
            Int64 totalSizeInAbout1Second = 0;

            while (continueToRun)
            {
                try
                {
                    int receivedDataLength = udpSocketToReceiveData.Receive(buffer);

                    if (receivedDataLength > 0)
                    {
                        //Make a copy and post it to the form to display.
                        byte[] dataReceived = new byte[receivedDataLength];
                        Array.Copy(buffer, 0, dataReceived, 0, receivedDataLength);

                        lock (this)
                        { 
                            //Post the data to the processor.
                            //Put the message into the queue.
                            messageQueue.Enqueue(new MessageNotification(MessageId.MESSAGE_UDP_PACKET, dataReceived));
                        }

                        //Signal the semaphore.
                        dataSemaphore.Release();
                    }

                    totalSizeInAbout1Second += receivedDataLength;
                    totalSize += receivedDataLength;

                    TimeSpan currentTime = new TimeSpan(DateTime.Now.Ticks);
                    TimeSpan duration = currentTime.Subtract(previousTime).Duration();
                    if (duration.TotalMilliseconds >= 1000)
                    {
                        //Time to calculate the bitrate. The timer will show the bitrate in the form.
                        bitrate = (Int64)((totalSizeInAbout1Second * 8)*1000 / duration.TotalMilliseconds);//bits per second.
                        
                        //Clean up for the next calculation.
                        previousTime = currentTime;
                        totalSizeInAbout1Second = 0;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }//while (continueToRun)

            //Close the socket.
            udpSocketToReceiveData.Close();
            udpSocketToReceiveData = null;

            //Notify the processor thread to quit.
            lock (this)
            {
                messageQueue.Enqueue(new MessageNotification(MessageId.MESSAGE_QUIT, null));
                //Signal the semaphore.
                dataSemaphore.Release();

            }
            receiverThread = null;

        }//UdpDataReceiverEntry

        private void FileReaderEntry()
        {
            int packetsForEachRead = 7 * 100;//We will read in 700 TS packets each time.

            int bytesInOneRead = packetsForEachRead * tsPacketSize;//How many bytes to read in each time.
            int bitsInOneRead = bytesInOneRead * 8;//How many bits in each read.
            int dataDurationForOneRead = (int)(bitsInOneRead * 1000 / playoutBitrate);//In how many milliseconds we need to play out the data.

            byte[] buffer = new byte[bytesInOneRead];
            TimeSpan previousTime = new TimeSpan(DateTime.Now.Ticks);
            Int64 totalSizeInAbout1Second = 0;

            while (continueToRun)
            {
                TimeSpan time1 = new TimeSpan(DateTime.Now.Ticks);

                int receivedDataLength = inputFileStream.Read(buffer, 0, bytesInOneRead);

                //Make a copy and post it.
                byte[] dataReceived = new byte[receivedDataLength];
                Array.Copy(buffer, 0, dataReceived, 0, receivedDataLength);


                DataInTime dataInTime = new DataInTime(dataReceived, dataDurationForOneRead);//dataDurationForOneRead milliseconds.
                lock (this)
                {
                    //Post the data to the processor.
                    //Put the message into the queue.
                    messageQueue.Enqueue(new MessageNotification(MessageId.MESSAGE_DATA_BLOCK_FILE, dataInTime));
                }

                //Signal the semaphore.
                dataSemaphore.Release();

                totalSizeInAbout1Second += receivedDataLength;
                totalSize += receivedDataLength;

                playingProgress = (int)(inputFileStream.Position *100/ inputFileStream.Length);

                TimeSpan time2 = new TimeSpan(DateTime.Now.Ticks);
                TimeSpan timeUsed = time2.Subtract(time1).Duration();

                if (dataDurationForOneRead > (int)timeUsed.TotalMilliseconds)
                {
                    Thread.Sleep(dataDurationForOneRead - (int)timeUsed.TotalMilliseconds);
                }

                TimeSpan currentTime = new TimeSpan(DateTime.Now.Ticks);
                TimeSpan duration = currentTime.Subtract(previousTime).Duration();
                if (duration.TotalMilliseconds >= 1000)
                {
                    //Time to calculate the bitrate. The timer will show the bitrate in the form.
                    bitrate = (Int64)((totalSizeInAbout1Second * 8)*1000/ duration.TotalMilliseconds);//bits per second.

                    //Clean up for the next calculation.
                    previousTime = currentTime;
                    totalSizeInAbout1Second = 0;
                }

                //If we reach the end of the file.
                if (receivedDataLength < bytesInOneRead)
                {
                    if (loopback)
                    {
                        //Seek to the beginning.
                        inputFileStream.Seek(0, SeekOrigin.Begin);
                    }
                    else
                    {
                        //Time to quit.
                        continueToRun = false;
                    }
                }

            }//while (continueToRun)

            //Close the stream.
            inputFileStream.Close();
            inputFileStream = null;

            //Notify the processor thread to quit.
            lock (this)
            {
                messageQueue.Enqueue(new MessageNotification(MessageId.MESSAGE_QUIT, null));
                //Signal the semaphore.
                dataSemaphore.Release();

            }
            receiverThread = null;

        }//UdpDataReceiverEntry

        private Result StartDataProcessor()
        {
            Result result = new Result();

            if (toOutputToFile)
            {
                try
                {
                    //To save the data into the local file.
                    outputFileStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write);
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid output file name. Please enter a valid file name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result.SetResult(ResultCode.FAILED_TO_OPEN_FILE);
                    textBoxOutputFile.Focus();
                }

            }

            if (result.Fine && toOutputToNetwork)
            {
                //Try to create the socket to be used as the output socket.
                try
                {
                    /*Create a UDP client.*/
                    udpSocketToSendData = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                    if (null != udpSocketToSendData)
                    {
                        udpSocketToSendData.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendBuffer, SOCKET_BUFFER_SIZE);//8M bytes buffer.
                        //udpSocketToSendData.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontRoute, 1);//Send it out directly.
                        udpSocketToSendData.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 10);//TTL.
                        udpSocketToSendData.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastInterface, 0);
                        //Non blocking?

                        //Loopback?
                        KeyValuePair<IPAddress, int> networkIndexIpPair = (KeyValuePair<IPAddress, int>)listBoxOutputNetworkInterfaces.SelectedItem;
                        //Set multicase output interface, so that multicast data can go out from this interface.
                        udpSocketToSendData.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastInterface, IPAddress.NetworkToHostOrder(networkIndexIpPair.Value));
                    }

                }
                catch (Exception ex)
                {
                    if (null != udpSocketToSendData)
                    {
                        udpSocketToSendData.Close();
                        udpSocketToSendData = null;
                    }

                    result.SetFailure();
                    MessageBox.Show("Failed to create the socket.", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex);
                } 
            }

            if (result.Fine && toOutputToDevice)
            {
                result = InitOutputDevice();
            }

            if (result.Fine)
            {
                //Try to create the thread
                try
                {
                    //Create a thread to listen on this socket.
                    processorThread = new Thread(new ThreadStart(DataProcessorEntry));
                    if (null != processorThread)
                    {
                        processorThread.IsBackground = true;
                        processorThread.Start();
                    }

                }
                catch (Exception ex)
                {
                    if (null != processorThread)
                    {
                        processorThread.Abort();
                        processorThread = null;
                    }

                    result.SetFailure();
                    MessageBox.Show("Failed to create a new thread.", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex);
                } 
            }

            return result; 
        }//StartDataProcessor


        //To save the data into the file or re-broadcast the data into the network.
        private void DataProcessorEntry()
        {
            bool waitResult = false;
            MessageNotification notification = null;
            bool running = true;

            //To re-broadcast it into the network.
            if (toOutputToNetwork)
            {
                outputNetworkTarget = new IPEndPoint(outputNetworkAddress, outputNetworkPort);
            }

            while (running)
            {
                waitResult = dataSemaphore.WaitOne(100);
                if (waitResult)
                {
                    //We have received the data successfully.
                    lock (this)
                    {
                        notification = messageQueue.Dequeue();
                    }

                    switch (notification.ID)
                    {
                        case MessageId.MESSAGE_UDP_PACKET:
                        {
                            byte[] dataBytes = (byte[])notification.Payload;

                            //Write it into the local file.
                            if (toOutputToFile)
                            {
                               outputFileStream.Write(dataBytes, 0, dataBytes.Length);
                            }

                            //To re-broadcast it into the network.
                            if (toOutputToNetwork)
                            {
                                udpSocketToSendData.SendTo(dataBytes, 0, dataBytes.Length, SocketFlags.None, outputNetworkTarget);
                            }

                            //To send it to the device.
                            if (toOutputToDevice)
                            {
                                dtOutputChannel.Write(dataBytes, dataBytes.Length);
                            }

                            break;
                        }
                        case MessageId.MESSAGE_DATA_BLOCK_DEVICE:
                        {
                            DataInTime dataInTime = (DataInTime)notification.Payload;
                            //Write it into the local file.
                            if (toOutputToFile)
                            {
                                byte[] dataBytes = dataInTime.dataBytes;

                                outputFileStream.Write(dataBytes, 0, dataBytes.Length);
                            }

                            //Broadcast to the network or to the device
                            if ((toOutputToNetwork) || (toOutputToDevice))
                            {
                                SendBlockData(dataInTime, 188);
                            }

                            break;
                        }
                        case MessageId.MESSAGE_DATA_BLOCK_FILE:
                        {
                            DataInTime dataInTime = (DataInTime)notification.Payload;
                            //Write it into the local file.
                            if (toOutputToFile)
                            {
                                byte[] dataBytes = dataInTime.dataBytes;

                                outputFileStream.Write(dataBytes, 0, dataBytes.Length);
                            }

                            //Broadcast to the network or to the device
                            if ((toOutputToNetwork) || (toOutputToDevice))
                            {
                                SendBlockData(dataInTime, tsPacketSize);
                            }

                            break;
                        }
                        case MessageId.MESSAGE_QUIT:
                        {
                            running = false;
                            break;
                        }
                    }
                }
            }//while (continueToRun)

            if (null != outputFileStream)
            {
                //Close the file stream.
                outputFileStream.Close();
                outputFileStream = null;
            }

            if (null != udpSocketToSendData)
            {
                udpSocketToSendData.Close();
                udpSocketToSendData = null;
            }

            if (toOutputToDevice)
            {
                // Detach from channel and device
                dtOutputChannel.SetTxControl(DTAPI.TXCTRL_IDLE);
                dtOutputChannel.Detach(DTAPI.INSTANT_DETACH);
                dtOutputDevice.Detach(); 
            }

            processorThread = null;
        }//DataProcessorEntry

        private void SendBlockData(DataInTime dataInTime, int packetSize)
        {
            byte[] dataBytes = dataInTime.dataBytes;
            int dataLength = dataBytes.Length;
            Int64 milliSeconds = dataInTime.milliseconds;
            //int loopsBeforeSleep = 20; //We will have a rest after we send out 10 TS packets.

            //7 TS packets with each 188 bytes will be put into a UDP packet as a group.

            //How many times we will need to send out all the UDP packets.
            int loopTimes = dataLength / packetSize / 7;
            int leftData = dataLength % (packetSize * 7);
            //Int64 interval = milliSeconds * loopsBeforeSleep / loopTimes;

            //For device.
            byte[] dataToDevice = new byte[packetSize * 7];


            int i = 0;
            for (i = 0; i < loopTimes; ++i)
            {
                if (toOutputToNetwork)
                {
                    //Console.WriteLine("----1111-----" + DateTime.Now.ToString());
                    udpSocketToSendData.SendTo(dataBytes, i * packetSize * 7, packetSize * 7, SocketFlags.None, outputNetworkTarget);
                   // udpSocketToSendData.IOControl(IOControlCode.
                    //Console.WriteLine("----2222-----" + DateTime.Now.ToString());
                }
                if (toOutputToDevice)
                {
                    Array.Copy(dataBytes, i * packetSize * 7, dataToDevice, 0, packetSize * 7);
                    dtOutputChannel.Write(dataToDevice, packetSize * 7);
                }
                //if (0 == (i % loopsBeforeSleep))
                //{
                //    //Thread.Sleep((int)interval);
                //    //Console.WriteLine("----3333-----" + interval);
                //}


            }

            //Send out anything left.
            if (0 != leftData)
            {
                if (toOutputToNetwork)
                {
                    udpSocketToSendData.SendTo(dataBytes, i * packetSize * 7, leftData, SocketFlags.None, outputNetworkTarget);
                }
                if (toOutputToDevice)
                {
                    Array.Copy(dataBytes, i * packetSize * 7, dataToDevice, 0, leftData);
                    dtOutputChannel.Write(dataToDevice, leftData);
                }
            }
            
        }


        private void CleanUp()
        {
            //Shut down the threads. The threads will clean up the resources occuped by them.
            continueToRun = false;

            if (null != receiverThread)
            {
                Thread.Sleep(500);
            }

        }

        private void timerToUpdateUI_Tick(object sender, EventArgs e)
        {
            String bitrateStr = String.Format("{0:###,###,###,###,###,###,###}", bitrate);
            textBoxBitrate.Text = bitrateStr;

            TimeSpan currentTime = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan duration = currentTime.Subtract(startTime).Duration();

            textBoxDuration.Text = String.Format("{0:###,###,###,###,###,###,###}", duration.TotalMilliseconds);

            textBoxTotalSize.Text = String.Format("{0:###,###,###,###,###,###,###}", totalSize);

            progressBarPlay.Value = playingProgress;
        }

        private void FormRecord_Load(object sender, EventArgs e)
        {
        }

        private void GetAllInputDevices()
        {
            int entryNumbers = 0;
            DTAPI_RESULT dtResult = DtGlobal.DtapiHwFuncScan(DEVICE_COUNT, ref entryNumbers, dtInputHwFuncDesc);
            if (DTAPI_RESULT.OK == dtResult)
            {
                //Show all items in the list.
                for (int i = 0; i < entryNumbers; ++i)
                {
                    //If the device can be used as an input source.
                    if (1 == (dtInputHwFuncDesc[i].m_ChanType & DTAPI.CHAN_INPUT))
                    {
                        listBoxAllInputDevices.Items.Add(new DektecDevice(dtInputHwFuncDesc[i]));
                    }
                }

                if (listBoxAllInputDevices.Items.Count > 0)
                {
                    listBoxAllInputDevices.SelectedIndex = 0;
                }
            } 
        }

        private void GetAllOutputDevices()
        {
            int entryNumbers = 0;
            DTAPI_RESULT dtResult = DtGlobal.DtapiHwFuncScan(DEVICE_COUNT, ref entryNumbers, dtOutputHwFuncDesc);
            if (DTAPI_RESULT.OK == dtResult)
            {
                //Show all items in the list.
                for (int i = 0; i < entryNumbers; ++i)
                {
                    //If the device can be used as an output device.
                    if ((0 == (dtOutputHwFuncDesc[i].m_ChanType & DTAPI.CHAN_OUTPUT))
                        && ((dtOutputHwFuncDesc[i].m_Flags & DTAPI.CAP_OUTPUT) == 0)
                        )
                    {
                        //The device doesn't support output.
                    }
                    else
                    {
                        listBoxAllOutputDevices.Items.Add(new DektecDevice(dtOutputHwFuncDesc[i]));
                    }
                }

                if (listBoxAllOutputDevices.Items.Count > 0)
                {
                    listBoxAllOutputDevices.SelectedIndex = 0;
                }
            }
        }


        private Result CheckDeviceInputConfig()
        {
            Result result = new Result();

            if(result.Fine)
            {
                DektecDevice selectedDevice = (DektecDevice)listBoxAllInputDevices.SelectedItem;

                if (null == selectedDevice)
                {
                    result.SetResult(ResultCode.INVALID_DATA);
                    MessageBox.Show("No valid input device has been selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listBoxAllInputDevices.Focus();
                }
                else
                {
                    currentInputHwFuncDesc = selectedDevice.dtHwFuncDesc;
                }
            }

            return result;
        }

        private Result CheckDeviceOutputConfig()
        {
            Result result = new Result();

            if (result.Fine)
            {
                DektecDevice selectedDevice = (DektecDevice)listBoxAllOutputDevices.SelectedItem;

                if (null == selectedDevice)
                {
                    result.SetResult(ResultCode.INVALID_DATA);
                    MessageBox.Show("No valid output device has been selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listBoxAllOutputDevices.Focus();
                }
                else
                {
                    currentOutputHwFuncDesc = selectedDevice.dtHwFuncDesc;
                }
            }

            return result;
        }

        private Result StartDeviceReceiver()
        {
            Result result = new Result();

            if(result.Fine)
            {
                //Create a thread to receive the data.
                receiverThread = new Thread(new ThreadStart(DeviceDataReceiverEntry));
                receiverThread.IsBackground = true;

                //receiverThread.Priority = ThreadPriority.Lowest;
                receiverThread.Start();
            }

            return result;
        }

        private void DeviceDataReceiverEntry()
        {
            //Attach to device with serial number.
            DTAPI_RESULT dtResult = dtInputDevice.AttachToSerial(currentInputHwFuncDesc.m_DvcDesc.m_Serial);
            if (DTAPI_RESULT.OK == dtResult)
            {
                //Attach to the port number of the device to get the input channel.
                dtResult = dtInputChannel.AttachToPort(dtInputDevice, currentInputHwFuncDesc.m_Port);
            }

            if (DTAPI_RESULT.OK == dtResult)
            {
                // Init channel to initial 'safe' state
                dtResult = dtInputChannel.SetRxControl(DTAPI.RXCTRL_IDLE);   // Start in IDLE mode
                dtResult = dtInputChannel.ClearFifo();           // Clear FIFO (i.e. start with zero load)
                unchecked
                {
                    dtResult = dtInputChannel.ClearFlags((int)0xFFFFFFFF);// Clear all flags
                }
                dtResult = dtInputChannel.SetRxMode(DTAPI.RXMODE_ST188);// Set the receive mode   
            }

            int rateOk = 0;
            if (DTAPI_RESULT.OK == dtResult)
            {
                int packetSize = 0;
                int numInv = 0;
                int clkDet = 0;
                int aAsiLock = 0;
                int asiInv = 0;

                dtResult = dtInputChannel.GetStatus(ref packetSize, ref numInv, ref clkDet, ref aAsiLock, ref rateOk, ref asiInv);
            }

            if (DTAPI_RESULT.OK == dtResult)
            {
                if (DTAPI.INPRATE_OK != rateOk)
                {
                    Console.WriteLine("Warning:Possibly no input!");
                }
            }

            if (DTAPI_RESULT.OK == dtResult)
            {
                //Start to receive.
                dtResult = dtInputChannel.SetRxControl(DTAPI.RXCTRL_RCV);
            }

            //Read aligned number of data from input channel.
            int numberToRead = DEVICE_BUFFER_SIZE / 188 * 188;

            if (DTAPI_RESULT.OK == dtResult)
            {
                int fifoLoad = 0;
                byte[] dataBuffer = new byte[DEVICE_BUFFER_SIZE];

                //Used to calculate bitrate.
                TimeSpan previousTime = new TimeSpan(DateTime.Now.Ticks);
                Int64 totalSizeInAbout1Second = 0;

                //Used to get the duration.
                TimeSpan firstTime = new TimeSpan(DateTime.Now.Ticks);

                while (continueToRun)
                {
                    dtResult = dtInputChannel.GetFifoLoad(ref fifoLoad);

                    if (dtResult == DTAPI_RESULT.OK)
                    {
                        //Check whether the data is big enough, if not, we will not retrieve that.
                        if (DEVICE_BUFFER_SIZE > fifoLoad)
                        {
                            //Sleep 1ms, before checking load again
                            Thread.Sleep(1);
                            continue;
                        }
                    }

                    if (dtResult == DTAPI_RESULT.OK)
                    {
                        //Read the data into the buffer.
                        dtResult = dtInputChannel.Read(dataBuffer, numberToRead);
                    }

                    if (dtResult == DTAPI_RESULT.OK)
                    {

                        //Make a copy and post it.
                        byte[] dataReceived = new byte[numberToRead];
                        Array.Copy(dataBuffer, 0, dataReceived, 0, numberToRead);

                        TimeSpan secondTime = new TimeSpan(DateTime.Now.Ticks);
                        //Get the duration in millisecond.
                        Int64 durationInMilliseconds = (Int64)(secondTime.Subtract(firstTime).Duration().TotalMilliseconds);

                        DataInTime dataInTime = new DataInTime(dataReceived, durationInMilliseconds);

                        lock (this)
                        {
                            //Put the message into the queue.
                            messageQueue.Enqueue(new MessageNotification(MessageId.MESSAGE_DATA_BLOCK_DEVICE, dataInTime));
                        }

                        //Signal the semaphore.
                        dataSemaphore.Release();

                        totalSizeInAbout1Second += numberToRead;
                        totalSize += numberToRead;

                        TimeSpan currentTime = new TimeSpan(DateTime.Now.Ticks);
                        TimeSpan duration = currentTime.Subtract(previousTime).Duration();
                        if (duration.TotalMilliseconds >= 1000)
                        {
                            //Time to calculate the bitrate. The timer will show the bitrate in the form.
                            bitrate = (Int64)((totalSizeInAbout1Second * 8 )*1000/ duration.TotalMilliseconds);//bits per second.

                            //Clean up for the next calculation.
                            previousTime = currentTime;
                            totalSizeInAbout1Second = 0;
                        }
                    }
                }//while (continueToRun)

                //Notify the processor thread to quit.
                lock (this)
                {
                    messageQueue.Enqueue(new MessageNotification(MessageId.MESSAGE_QUIT, null));
                    //Signal the semaphore.
                    dataSemaphore.Release();
                }

                if (DTAPI_RESULT.OK == dtResult)
                {
                    dtInputChannel.Detach(DTAPI.INSTANT_DETACH);
                    dtInputDevice.Detach();
                }

                receiverThread = null;
            }

        }

        private void FormRecord_Shown(object sender, EventArgs e)
        {
            GetAllInputDevices();

            GetAllOutputDevices();

            listBoxInputNetworkInterfaces.ListAllNetworkInterfaces();
            listBoxOutputNetworkInterfaces.ListAllNetworkInterfaces();

        }

        private void buttonBrowseInput_Click(object sender, EventArgs e)
        {
            //Open a bitstream file.
            OpenFileDialog openStreamFileDialog = new OpenFileDialog();
            openStreamFileDialog.Filter = "Transport stream files(*.ts;*.mpeg;*.mpg;*.m2t)|*.ts;*.mpeg;*.mpg;*.m2t|All files(*.*)|*.*";
            openStreamFileDialog.Title = "Select input stream";
            openStreamFileDialog.CheckPathExists = true;
            if (DialogResult.OK == openStreamFileDialog.ShowDialog())
            {
                textBoxInputFile.Text = openStreamFileDialog.FileName;
            }
        }

        private void checkBoxOutputToDevice_CheckedChanged(object sender, EventArgs e)
        {
            listBoxAllOutputDevices.Enabled = checkBoxOutputToDevice.Checked;
        }

        private Result InitOutputDevice()
        {
            Result result = new Result();

            DTAPI_RESULT dtResult = DTAPI_RESULT.OK;

            if (DTAPI_RESULT.OK == dtResult)
            {
                if (null == currentOutputHwFuncDesc)
                {
                    dtResult = DTAPI_RESULT.E_NO_DT_OUTPUT;
                }
            }

            //Attach to the device.
            if (DTAPI_RESULT.OK == dtResult)
            {
                dtOutputDevice.AttachToSerial(currentOutputHwFuncDesc.m_DvcDesc.m_Serial);
            }

            if (DTAPI_RESULT.OK == dtResult)
            {
                if (0 == (currentOutputHwFuncDesc.m_ChanType & DTAPI.CHAN_OUTPUT))
                {
                    //Set output config.
                    dtResult = dtOutputDevice.SetIoConfig(currentOutputHwFuncDesc.m_Port, DTAPI.IOCONFIG_IODIR, DTAPI.IOCONFIG_OUTPUT, DTAPI.IOCONFIG_OUTPUT);
                }
            }

            if (DTAPI_RESULT.OK == dtResult)
            {
                // Attach to the output channel
                dtResult = dtOutputChannel.AttachToPort(dtOutputDevice, currentOutputHwFuncDesc.m_Port);
            }

            if (DTAPI_RESULT.OK == dtResult)
            {
                //Init channel to initial 'safe' state.
                dtResult = dtOutputChannel.SetTxControl(DTAPI.TXCTRL_IDLE);
            }

            if (DTAPI_RESULT.OK == dtResult)
            {
                //Set the transmission mode.
                int txMode = DTAPI.TXMODE_188;//To change to 204 if necessary.We will need to get the ts packet size from the stream.

                if ((txMode & DTAPI.TXMODE_TS) != 0)
                {
                    dtResult = dtOutputDevice.SetIoConfig(DTAPI.IOCONFIG_IOSTD, DTAPI.IOCONFIG_ASI, -1);
                }
            }

            if (DTAPI_RESULT.OK == dtResult)
            {
                //Set FIFO size to maximum.
                dtResult = dtOutputChannel.SetFifoSizeMax();
            }

            if (DTAPI_RESULT.OK == dtResult)
            {
                //Clear FIFO (i.e. start with zero load).
                dtResult = dtOutputChannel.ClearFifo();
            }

            if (DTAPI_RESULT.OK == dtResult)
            {
                //Init channel to hold mode.
                dtResult = dtOutputChannel.SetTxControl(DTAPI.TXCTRL_HOLD);
            }

            if (DTAPI_RESULT.OK == dtResult)
            {
                //Get FIFO size.
                dtResult = dtOutputChannel.GetFifoSize(ref deviceFifoSize);
            }

            if (DTAPI_RESULT.OK == dtResult)
            {
                // Start transmission
                dtResult = dtOutputChannel.SetTxControl(DTAPI.TXCTRL_SEND);
            }

            if (DTAPI_RESULT.OK != dtResult)
            {
                result.SetFailure();
            }

            return result;
        }


        public void HideFileInput()
        {
            tabControlInput.TabPages["tabPageInputFile"].Parent = null;
        }

        public void SetFileInput(Int64 bitrate, string inputFileName, int packetSize)
        {
            textBoxInputFile.Text = inputFileName;
            maskedTextBoxBitrate.Text = Convert.ToString(bitrate);
            tsPacketSize = packetSize;
            tabControlInput.SelectedIndex = tabControlInput.TabPages.IndexOfKey("tabPageInputFile");
        }
    }
}
