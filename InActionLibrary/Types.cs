using System;
using System.Collections.Generic;
using System.Text;

namespace InActionLibrary
{
    //Callback to notify all kinds of messages(from StreamParser to owner of the StreamParser, like StreamParserForm.)
    public delegate void MessageHandlerDelegate(MessageId messageId, object message);

    //Callback to notify ts packet/PES packet/section after filter.
    public delegate void FilterDataCallback(UInt16 pid, byte[] data, Int64 dataOffset, Int64 dataLength, List<TsPacketMetadata> packetMetadataList);


    public enum TsPacketSize
    {
        SIZE_UNKNOWN = 0,
        SIZE_188 = 188,
        SIZE_204 = 204
    };

    public enum TsPacketSource
    {
        SOURCE_FILE = 1,
        SOURCE_NETWORK = 2
    };

    public enum DataType
    {
        UNKNOWN = 0,
        TS_PACKET = 1,
        PES_PACKET = 2,
        SECTION = 3
    };


    public enum TsPID : ushort
    {
        PAT = 0x0000,
        CAT = 0x0001,
        TSDT = 0x0002,
        /*
        **0x0003 to 0x000F = reserved.
        */
        NIT = 0x0010,
        //ST   = 0x0010,

        //BAT = 0x0011,
        //SDT = 0x0011,
        BAT_SDT = 0x0011,
        //ST   = 0x0011,



        EIT = 0x0012,
        //ST   = 0x0012,

        RST = 0x0013,
        //ST   = 0x0013,

        //TDT = 0x0014,
        //TOT = 0x0014,
        TDT_TOT = 0x0014,
        //ST   = 0x0014,

        NST = 0x0015,
        IST = 0x001C,
        MT = 0x001D,
        /*
        **0x0015 network synchronization
        **0x0016 to 0x001B reserved
        **0x001C inband signalling
        **0x001D measurement
        */

        DIT = 0x001E,
        SIT = 0x001F,
    };

/*
Value Description
0x00 program_association_section
0x01 conditional_access_section
0x02 program_map_section
0x03 transport_stream_description_section
0x04 to 0x3F reserved
0x40 network_information_section - actual_network
0x41 network_information_section - other_network
0x42 service_description_section - actual_transport_stream
0x43 to 0x45 reserved for future use
0x46 service_description_section - other_transport_stream
0x47 to 0x49 reserved for future use
0x4A bouquet_association_section
0x4B to 0x4D reserved for future use
0x4E event_information_section - actual_transport_stream, present/following
0x4F event_information_section - other_transport_stream, present/following
0x50 to 0x5F event_information_section - actual_transport_stream, schedule
0x60 to 0x6F event_information_section - other_transport_stream, schedule
0x70 time_date_section
0x71 running_status_section
0x72 stuffing_section
0x73 time_offset_section
0x74 to 0x7D reserved for future use
0x7E discontinuity_information_section
0x7F selection_information_section
0x80 to 0xFE user defined
0xFF reserved
*/
    public enum TableId : byte
    {
        PAT = 0x00,
        CAT = 0x01,
        PMT = 0x02,
        TSDT = 0x03,
        /*
        **0x04~0x3F Reserved.
        */

        DSI_DII = 0x3B, /*DownloadServerInitiate/DownloadInfoIndication DSMCC Section*/
        DDB = 0x3C, /*DownloadDataBlock DSMCC Section*/


        NIT_ACTUAL = 0x40,
        NIT_OTHER = 0x41,
        SDT_ACTUAL = 0x42,
        /*
        **0x43~0x45 Reserved for future use.
        */
        SDT_OTHER = 0x46,
        /*
        **0x47~0x49 Reserved for future use.
        */
        BAT = 0x4A,
        /*
        **0x4B~0x4D Reserved for future use.
        */
        EIT_PF_ACTUAL = 0x4E,
        EIT_PF_OTHER = 0x4F,

        /*0x50~0x5F all for EIT CURRENT SCHEDULE table.*/
        EIT_SCHEDULE_ACTUAL = 0x50,

        /*0x60~0x6F all for EIT OTHER SCHEDULE table.*/
        EIT_SCHEDULE_OTHER = 0x60,

        TDT = 0x70,
        RST = 0x71,
        ST = 0x72,
        TOT = 0x73,
        /*
        **0x74~0x7D Reserved for future use.
        */
        DIT = 0x7E,
        SIT = 0x7F
        /*
        **0x80~0xFE User defined.
        */

        /*
        **0xFF Reserved.
        */
    }

    public enum MessageId
    {
        MESSAGE_TS_PACKET_SIZE = 0, //Packet size. 188 or 204 by default.
        MESSAGE_STANDARD_SECTION = 1, //Standard SI/PSI section.
        MESSAGE_NEW_PROGRESS = 2,
        MESSAGE_PARSING_DONE = 3,
        MESSAGE_PID_LIST = 4,
        MESSAGE_MUX_BITRATE = 5,//Stream bitrate calculated from PCR.
        MESSAGE_SEARCHED_DATA = 6, //TS packet, section or PES packet that is found in search mode.
        MESSAGE_SEARCH_DONE = 7,
        MESSAGE_PID_BITRATE_DATA = 8, //PID bitrate in one second is done.
        MESSAGE_MEASURE_PID_BITRATE_DONE = 9, //We have finished collected all the bitrate information for each PID.
        MESSAGE_PID_UPDATE_DONE = 10, //PID replacement is done.
        MESSAGE_PID_DUMP_DONE = 11, //PID dump is done.
        MESSAGE_DESCRABLING_DONE = 12, //Descrambling is done.
        MESSAGE_SCRAMBLING_DONE = 13, //Scrambling is done.
        //----------------------------------------------------------------------------the above is used by stream parser etc.
        MESSAGE_UDP_PACKET = 101, //A UDP packet is received.
        MESSAGE_DATA_BLOCK_DEVICE = 102,//Data block from device.
        MESSAGE_DATA_BLOCK_FILE = 103,//Data block from file.
        //------------------------------------The following messages are command messages.
        MESSAGE_QUIT = 1000
    }

    public enum ItemType
    {
        ROOT = 0,
        PSI = 1,
        SI = 2,
        PID = 3,
        PSI_SECTION = 4,
        SI_SECTION = 5,
        FIELD = 6,
        LOOP = 7,
        ITEM = 8,
        WARNING = 9,
        ERROR = 10,
        PID_ITEM = 11,
        SEARCH_REQUEST = 12,
        SEARCH_TS = 13,
        SEARCH_PES = 14,
        SEARCH_SECTION = 15
    }

    public enum Scope
    {
        MPEG = 0,
        SI = 1 
    }

    public enum Position
    {
        CHILD = 0,
        PEER = 1
    }
}
