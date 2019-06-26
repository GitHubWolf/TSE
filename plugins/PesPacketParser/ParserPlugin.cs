using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InActionLibrary;
using System.Windows.Forms;

namespace CustomizedParser
{
    public class ParserPlugin : IParserPlugin
    {
        /**
         * Get the name of this plugin.
         * 
         * @retval
         *     String to describe the name of this plugin.
         * 
         */
        public string GetName()
        {
            return "PES Packet Parser";
        }

        /**
         * Get a string to describle the capability of this parser.
         * 
         * @retval
         *     String to describe the plugin's capabilities.
         * 
         */
        public string GetDescription()
        {
            return "Plugin to parse individual PES packet. Only PES header is parsed.";
        }

        /**
         * Get the data type that this plugin supports.
         * 
         * Only one kind of data type is supported by each plugin, i.e. TS packet, PES packet or section.
         * 
         * @retval
         *     @see DataType
         * 
         */
        public DataType GetSupportedType()
        {
            return DataType.PES_PACKET;
        }

        /**
         * Parse the TS packet, PES packet or section.
         * 
         * @param parentNode parent tree node to display the parsing result.
         * @param dataStore an instance containing the data to be parsed.
         * 
         * @retval
         *     void
         * 
         */
        public void Parse(TreeNode parentNode, DataStore dataStore)
        {
            TreeNode newNode = null;
            TreeNode pesPacketNode = null;

            //Add one node to indicate this dataStore.
            Result result = Utility.AddNodeContainer(Position.CHILD, parentNode, out pesPacketNode, "PES Paccket", ItemType.SEARCH_TS, dataStore, 0, dataStore.GetLeftBitLength());

            Int64 bitOffset = 0;

            Int64 packetStartCodePrefix = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "packet_start_code_prefix", ItemType.FIELD, dataStore, ref bitOffset, 24, ref packetStartCodePrefix);
            }

            Int64 streamId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "stream_id", ItemType.FIELD, dataStore, ref bitOffset, 8, ref streamId);
            }

            Int64 pesPacketLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeField(Position.CHILD, pesPacketNode, out newNode, "PES_packet_length", ItemType.FIELD, dataStore, ref bitOffset, 16, ref pesPacketLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeData(Position.CHILD, pesPacketNode, out newNode, "PES_packet_data_byte", ItemType.FIELD, dataStore, ref bitOffset, dataStore.GetLeftBitLength());
            }

            if (result.Fine)
            {
                String pesPacketDescription = String.Format("packet_start_code_prefix:0x{0, 6:X6}, stream_id:0x{1, 2:X2}, PES_packet_length:0x{2, 4:X4}",
                                                packetStartCodePrefix,
                                                streamId,
                                                pesPacketLength);
                Utility.UpdateNode(pesPacketNode, pesPacketDescription, ItemType.SEARCH_PES);
            }
            else
            {
                String pesPacketDescription = String.Format("packet_start_code_prefix:0x{0, 6:X6}, stream_id:0x{1, 2:X2}, PES_packet_length:0x{2, 4:X4}",
                                                packetStartCodePrefix,
                                                streamId,
                                                pesPacketLength);
                Utility.UpdateNode(pesPacketNode, pesPacketDescription, ItemType.ERROR);//Something wrong. Highlight it!
            }
        }

        /**
         * Start the plugin.
         * 
         * Before calling other methods in the plugin, this method will be called first. 
         * 
         * @retval
         *     void
         * 
         */
        public void Start()
        {

        }

        /**
         * Stop the plugin.
         * 
         * When the main application has notified all data found to the plugin, it will notify the plugin by calling this method. 
         * 
         * @retval
         *     void
         * 
         */
        public void Stop()
        {

        }
    }
}
