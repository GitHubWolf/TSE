using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InActionLibrary;

namespace TSE
{
    class Descriptor
    {
        public static Result ParseSingleDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            //Convert to bits.
            descriptorLength = descriptorLength * 8;

            if (result.Fine)
            {
                if ((domain == Scope.MPEG) || (domain == Scope.SI))
                {
                    switch (tag)
                    {
                        case 2:
                            result = ParseVideoStreamDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 3:
                            result = ParseAudioStreamDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 4:
                            result = ParseHierarchyDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 5:
                            result = ParseRegistrationDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 6:
                            result = ParseDataStreamAlignmentDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 7:
                            result = ParseTargetBackgroundGridDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 8:
                            result = ParseVideoWindowDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 9:
                            result = ParseCADescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 10:
                            result = ParseISO639LanguageDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 11:
                            result = ParseSystemClockDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 12:
                            result = ParseMultiplexBufferUtilizationDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 13:
                            result = ParseCopyrightDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 14:
                            result = ParseMaximumBitrateDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 15:
                            result = ParsePrivateDataIndicatorDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 16:
                            result = ParseSmoothingBufferDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 17:
                            result = ParseSTDDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 18:
                            result = ParseIBPDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 27:
                            result = ParseMPEG4VideoDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 28:
                            result = ParseMPEG4AudioDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 29:
                            result = ParseIODDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 30:
                            result = ParseSLDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 31:
                            result = ParseFMCDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 32:
                            result = ParseExternalEsIdDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 33:
                            result = ParseMuxcodeDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 34:
                            result = ParseFmxBufferSizeDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 35:
                            result = ParseMultiplexBufferDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 36:
                            result = ParseContentLabelingDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 37:
                            result = ParseMetadataPointerDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 38:
                            result = ParseMetadataDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 39:
                            result = ParseMetadataStdDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 40:
                            result = ParseAvcVideoDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 41:
                            result = ParseIPMPDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 42:
                            result = ParseAvcTimingAndHrdDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 43:
                            result = ParseMpeg2AacAudioDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 44: 
                            result = ParseFlexMuxTimingDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x40:
                            result = ParseNetworkNameDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x41:
                            result = ParseServiceListDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x42:
                            result = ParseStuffingDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x43:
                            result = ParseSatelliteDeliverySystemDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x44:
                            result = ParseCableDeliverySystemDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x45:
                            result = ParseVbiDataDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x46:
                            result = ParseVbiTeletextDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x47:
                            result = ParseBouquetNameDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x48:
                            result = ParseServiceDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x49:
                            result = ParseCountryAvailabilityDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x4A:
                            result = ParseLinkageDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x4B:
                            result = ParseNvodReferenceDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x4C:
                            result = ParseTimeShiftedServiceDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x4D:
                            result = ParseShortEventDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x4E:
                            result = ParseExtendedEventDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x4F:
                            result = ParseTimeShiftedEventDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x50:
                            result = ParseComponentDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x51:
                            result = ParseMosaicDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x52:
                            result = ParseStreamIdentifierDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x53:
                            result = ParseCaIdentifierDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x54:
                            result = ParseContentDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x55:
                            result = ParseParentalRatingDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x56:
                            result = ParseTeletextDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x57:
                            result = ParseTelephoneDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x58:
                            result = ParseLocalTimeOffsetDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x59:
                            result = ParseSubtitlingDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x5A:
                            result = ParseTerrestrialDeliverySystemDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x5B:
                            result = ParseMultilingualNetworkNameDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x5C:
                            result = ParseMultilingualBouquetNameDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x5D:
                            result = ParseMultilingualServiceNameDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x5E:
                            result = ParseMultilingualComponentDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x5F:
                            result = ParsePrivateDataSpecifierDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x60:
                            result = ParseServiceMoveDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x61:
                            result = ParseShortSmoothingBufferDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x62:
                            result = ParseFrequencyListDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x63:
                            result = ParsePartialTransportStreamDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x64:
                            result = ParseDataBroadcastDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x65:
                            result = ParseScramblingDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x66:
                            result = ParseDataBroadcastIdDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x67:
                            result = ParseTransportStreamDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x68:
                            result = ParseDSNGDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x69:
                            result = ParsePDCDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x6A:
                            result = ParseAC3Descriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x6B:
                            result = ParseAncillaryDataDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x6C:
                            result = ParseCellListDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x6D:
                            result = ParseCellFrequencyLinkDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x6E:
                            result = ParseAnnouncementSupportDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x6F:
                            result = ParseApplicationSignallingDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x70:
                            result = ParseAdaptationFieldDataDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x71:
                            result = ParseServiceIdentifierDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x72:
                            result = ParseServiceAvailbilityDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x73:
                            result = ParseDefaultAuthorityDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        //case 0x74: name = "related_content_descriptor"; break; //No detail content available.
                        case 0x75:
                            result = ParseTvaIdDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x76:
                            result = ParseContentIdentifierDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x77:
                            result = ParseTimeSliceFecIdentifierDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x78:
                            result = ParseEcmRepetitionRateDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x79:
                            result = ParseS2SatelliteDeliverySystemDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x7A:
                            result = ParseEnhancedAc3Descriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x7B:
                            result = ParseDtsDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x7C:
                            result = ParseAacDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        //case 0x7D: name = "XAIT_location_descriptor"; break; //No detail info available.
                        case 0x7E:
                            result = ParseFtaContentManagementDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        case 0x7F:
                            result = ParseExtensionDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                            break;
                        default: 
                            result = Utility.AddNodeData(Position.CHILD, parentNode, out newNode, "unknown_descriptor_payload", ItemType.ITEM, section, ref bitOffset, descriptorLength);
                            if (result.Fine)
                            {
                                descriptorLength = 0;
                            }
                            break;
                    }
                }

            }

            if (0 != descriptorLength)
            {
                result = Utility.AddNodeData(Position.CHILD, parentNode, out newNode, "unknown_descriptor_payload", ItemType.ERROR, section, ref bitOffset, descriptorLength);
                if (result.Fine)
                {
                    descriptorLength = 0;
                }
            }
            return result;

        }

        public static String GetDescriptorName(byte tag, Scope domain)
        {
            String name = null;
            if( (domain == Scope.SI) || (domain == Scope.MPEG))
            {
                switch (tag)
                {
                    //0 Reserved
                    //1 Reserved
                    case 2 : name = "video_stream_descriptor"; break;
                    case 3 : name = "audio_stream_descriptor"; break;
                    case 4 : name = "hierarchy_descriptor"; break;
                    case 5 : name = "registration_descriptor"; break;
                    case 6 : name = "data_stream_alignment_descriptor"; break;
                    case 7 : name = "target_background_grid_descriptor"; break;
                    case 8 : name = "video_window_descriptor"; break;
                    case 9 : name = "CA_descriptor"; break;
                    case 10: name = "ISO_639_language_descriptor"; break;
                    case 11: name = "system_clock_descriptor"; break;
                    case 12: name = "multiplex_buffer_utilization_descriptor"; break;
                    case 13: name = "copyright_descriptor"; break;
                    case 14: name = "maximum_bitrate_descriptor"; break;
                    case 15: name = "private_data_indicator_descriptor"; break;
                    case 16: name = "smoothing_buffer_descriptor"; break;
                    case 17: name = "STD_descriptor"; break;
                    case 18: name = "IBP_descriptor"; break;
                    //19-26 X Defined in ISO/IEC 13818-6
                    case 27: name = "MPEG-4_video_descriptor"; break;
                    case 28: name = "MPEG-4_audio_descriptor"; break;
                    case 29: name = "IOD_descriptor"; break;
                    case 30: name = "SL_descriptor"; break;
                    case 31: name = "FMC_descriptor"; break;
                    case 32: name = "external_ES_ID_descriptor"; break;
                    case 33: name = "MuxCode_descriptor"; break;
                    case 34: name = "FmxBufferSize_descriptor"; break;
                    case 35: name = "multiplexbuffer_descriptor"; break;
                    case 36: name = "content_labeling_descriptor"; break;
                    case 37: name = "metadata_pointer_descriptor"; break;
                    case 38: name = "metadata_descriptor"; break;
                    case 39: name = "metadata_STD_descriptor"; break;
                    case 40: name = "AVC video descriptor"; break;
                    case 41: name = "IPMP_descriptor"; break;
                    case 42: name = "AVC timing and HRD descriptor"; break;
                    case 43: name = "MPEG-2_AAC_audio_descriptor"; break;
                    case 44: name = "FlexMuxTiming_descriptor"; break;

                    case 0x40: name = "network_name_descriptor"; break;
                    case 0x41: name = "service_list_descriptor"; break;
                    case 0x42: name = "stuffing_descriptor"; break;
                    case 0x43: name = "satellite_delivery_system_descriptor"; break;
                    case 0x44: name = "cable_delivery_system_descriptor"; break;
                    case 0x45: name = "VBI_data_descriptor"; break;
                    case 0x46: name = "VBI_teletext_descriptor"; break;
                    case 0x47: name = "bouquet_name_descriptor"; break;
                    case 0x48: name = "service_descriptor"; break;
                    case 0x49: name = "country_availability_descriptor"; break;
                    case 0x4A: name = "linkage_descriptor"; break;
                    case 0x4B: name = "NVOD_reference_descriptor"; break;
                    case 0x4C: name = "time_shifted_service_descriptor"; break;
                    case 0x4D: name = "short_event_descriptor"; break;
                    case 0x4E: name = "extended_event_descriptor"; break;
                    case 0x4F: name = "time_shifted_event_descriptor"; break;
                    case 0x50: name = "component_descriptor"; break;
                    case 0x51: name = "mosaic_descriptor"; break;
                    case 0x52: name = "stream_identifier_descriptor"; break;
                    case 0x53: name = "CA_identifier_descriptor"; break;
                    case 0x54: name = "content_descriptor"; break;
                    case 0x55: name = "parental_rating_descriptor"; break;
                    case 0x56: name = "teletext_descriptor"; break;
                    case 0x57: name = "telephone_descriptor"; break;
                    case 0x58: name = "local_time_offset_descriptor"; break;
                    case 0x59: name = "subtitling_descriptor"; break;
                    case 0x5A: name = "terrestrial_delivery_system_descriptor"; break;
                    case 0x5B: name = "multilingual_network_name_descriptor"; break;
                    case 0x5C: name = "multilingual_bouquet_name_descriptor"; break;
                    case 0x5D: name = "multilingual_service_name_descriptor"; break;
                    case 0x5E: name = "multilingual_component_descriptor"; break;
                    case 0x5F: name = "private_data_specifier_descriptor"; break;
                    case 0x60: name = "service_move_descriptor"; break;
                    case 0x61: name = "short_smoothing_buffer_descriptor"; break;
                    case 0x62: name = "frequency_list_descriptor"; break;
                    case 0x63: name = "partial_transport_stream_descriptor"; break;
                    case 0x64: name = "data_broadcast_descriptor"; break;
                    case 0x65: name = "scrambling_descriptor"; break;
                    case 0x66: name = "data_broadcast_id_descriptor"; break;
                    case 0x67: name = "transport_stream_descriptor"; break;
                    case 0x68: name = "DSNG_descriptor"; break;
                    case 0x69: name = "PDC_descriptor"; break;
                    case 0x6A: name = "AC-3_descriptor"; break;
                    case 0x6B: name = "ancillary_data_descriptor"; break;
                    case 0x6C: name = "cell_list_descriptor"; break;
                    case 0x6D: name = "cell_frequency_link_descriptor"; break;
                    case 0x6E: name = "announcement_support_descriptor"; break;
                    case 0x6F: name = "application_signalling_descriptor"; break;
                    case 0x70: name = "adaptation_field_data_descriptor"; break;
                    case 0x71: name = "service_identifier_descriptor"; break;
                    case 0x72: name = "service_availability_descriptor"; break;
                    case 0x73: name = "default_authority_descriptor"; break;
                    case 0x74: name = "related_content_descriptor"; break;
                    case 0x75: name = "TVA_id_descriptor"; break;
                    case 0x76: name = "content_identifier_descriptor"; break;
                    case 0x77: name = "time_slice_fec_identifier_descriptor"; break;
                    case 0x78: name = "ECM_repetition_rate_descriptor"; break;
                    case 0x79: name = "S2_satellite_delivery_system_descriptor"; break;
                    case 0x7A: name = "enhanced_AC-3_descriptor"; break;
                    case 0x7B: name = "DTS_descriptor"; break;
                    case 0x7C: name = "AAC_descriptor"; break;
                    case 0x7D: name = "XAIT_locationd_escriptor"; break;
                    case 0x7E: name = "FTA_content_management_descriptor"; break;
                    case 0x7F: name = "extension_descriptor"; break;
                    default:
                        {
                            name = "unknown_descriptor";
                            break;
                        }
                }

            }

            return name;
        }

        public static String GetHierarchyTypeName(Int64 hierarchyType)
        {
            String name = null;

            switch (hierarchyType)
            {
                case 0: name = "Reserved"; break;
                case 1: name = "Spatial Scalability"; break;
                case 2: name = "SNR Scalability"; break;
                case 3: name = "Temporal Scalability"; break;
                case 4: name = "Data partitioning"; break;
                case 5: name = "Extension bitstream"; break;
                case 6: name = "Private Stream"; break;
                case 7: name = "Multi-view Profile"; break;
                case 15: name = "Base layer"; break;
                default:
                    {
                        name = "Reserved"; break;
                    }
            }

            return name;
        }

        public static String GetExtendedDescriptorName(byte descriptorTagExtension, Scope domain)
        {
            String name = null;
            if ((domain == Scope.SI) || (domain == Scope.MPEG))
            {
                switch (descriptorTagExtension)
                {
                    case 0: name = "image_icon_descriptor"; break;
                    case 1: name = "cpcm_delivery_signalling_descriptor"; break;
                    case 2: name = "CP_descriptor"; break;
                    case 3: name = "CP_identifier_descriptor"; break;
                    case 4: name = "T2_delivery_system_descriptor"; break;
                    case 5: name = "SH_delivery_system_descriptor"; break;
                    case 6: name = "supplementary_audio_descriptor"; break;
                    case 7: name = "network_change_notify_descriptor"; break;
                    case 8: name = "message_descriptor"; break;
                    case 9: name = "target_region_descriptor"; break;
                    case 10: name = "target_region_name_descriptor"; break;
                    case 11: name = "service_relocated_descriptor"; break;
                    default:
                        {
                            name = "unknown_descriptor_extension";
                            break;
                        }
                }

            }

            return name;
        }

        public static Result ParseDescriptorLoop(Scope domain, TreeNode parentNode, DataStore section, ref Int64 bitOffset, ref Int64 descriptorLoopLength)
        {
            Result result = new Result();

            if (result.Fine)
            {
                Int64 tag = 0;
                Int64 length = 0;
                TreeNode itemNode = null;
                TreeNode newNode = null;

                //Parse descriptors.
                while (descriptorLoopLength > 0)
                {
                    Int64 descriptorBitLength1 = section.GetLeftBitLength();

                    //Create a node. We currently don't know the length of this descriptor.
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "descriptor", ItemType.ITEM, section, bitOffset, 2 * 8, descriptorLoopLength);//Bit length will be updated once we know the exact length.

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "descriptor_tag", ItemType.FIELD, section, ref bitOffset, 8, ref tag, ref descriptorLoopLength);
                        if (result.Fine)
                        {
                            Utility.UpdateNode(itemNode, GetDescriptorName((byte)tag, domain), ItemType.ITEM);
                        }
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "length", ItemType.FIELD, section, ref bitOffset, 8, ref length, ref descriptorLoopLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out newNode, "payload", ItemType.ITEM, section, bitOffset, length * 8, descriptorLoopLength);
                        if (result.Fine)
                        {
                            if (result.Fine)
                            {
                                Utility.UpdateNodeLength(itemNode, GetDescriptorName((byte)tag, domain), ItemType.ITEM, (2 + length)*8);
                            }

                            //Decrease the size.
                            descriptorLoopLength -= length * 8;

                            //Parse single descriptor according to the descriptor_tag fieldValue.
                            result = ParseSingleDescriptor(domain, newNode, section, (byte)tag, ref bitOffset, length);
                        }
                    }

                    Int64 descriptorBitLength2 = section.GetLeftBitLength();

                    if (!result.Fine)
                    {
                        Utility.UpdateNodeLength(itemNode, GetDescriptorName((byte)tag, domain) + "(invalid)", ItemType.ERROR, (descriptorBitLength1 - descriptorBitLength2));
                        //Someting is wrong, let's just break!
                        break;
                    }
                }
            }



            return result;
 
        }


        public static Result ParseVideoStreamDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            TreeNode itemNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "multiple_frame_rate_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "frame_rate_code", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
            }

            Int64 mpeg1OnlyFlag = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "MPEG_1_only_flag", ItemType.FIELD, section, ref bitOffset, 1, ref mpeg1OnlyFlag, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "constrained_parameter_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }
            
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "still_picture_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }
            
            if (result.Fine)
            {
                if (0 == mpeg1OnlyFlag)
                {
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (MPEG_1_only_flag = = '0')", ItemType.ITEM, section, bitOffset, 0, descriptorLength);

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "profile_and_level_indication", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "chroma_format", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "frame_rate_extension_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "Reserved", ItemType.FIELD, section, ref bitOffset, 5, ref fieldValue, ref descriptorLength);
                    }
                }
            }
            
            return result;

        }

        public static Result ParseAudioStreamDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "free_format_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "ID", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "layer", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "variable_rate_audio_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
            }

            return result;

        }

        public static Result ParseHierarchyDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
            }

            Int64 hierarchyType = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "hierarchy_type", ItemType.FIELD, section, ref bitOffset, 4, ref hierarchyType, ref descriptorLength);
                //Update hierarchy_type sectionDetail.
                Utility.UpdateNode(newNode, "hierarchy_type: " + GetHierarchyTypeName(hierarchyType), ItemType.FIELD);             
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "hierarchy_layer_index", ItemType.FIELD, section, ref bitOffset, 6, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "hierarchy_embedded_layer_index", ItemType.FIELD, section, ref bitOffset, 6, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "hierarchy_channel", ItemType.FIELD, section, ref bitOffset, 6, ref fieldValue, ref descriptorLength);
            }

            return result;

        }

        public static Result ParseRegistrationDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "format_identifier", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "additional_identification_info", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }

        public static Result ParseDataStreamAlignmentDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "alignment_type", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            return result;
        }

        public static Result ParseTargetBackgroundGridDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "horizontal_size", ItemType.FIELD, section, ref bitOffset, 14, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "vertical_size", ItemType.FIELD, section, ref bitOffset, 14, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "aspect_ratio_information", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
            }

            return result;
        }

        public static Result ParseVideoWindowDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "horizontal_offset", ItemType.FIELD, section, ref bitOffset, 14, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "vertical_offset", ItemType.FIELD, section, ref bitOffset, 14, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "window_priority", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
            }

            return result;
        }
        public static Result ParseCADescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "CA_system_ID", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "CA_PID", ItemType.FIELD, section, ref bitOffset, 13, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "private_data_byte", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }

        public static Result ParseISO639LanguageDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;
            TreeNode itemNode = null;

            TreeNode loopNode = null;
            if (result.Fine)
            {
                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out loopNode, "language_loop", ItemType.LOOP, section, bitOffset, descriptorLength, descriptorLength);
            }

            if (result.Fine)
            {
                Int64 languageCode  = 0;
                while (descriptorLength > 0)
                {
                    result = Utility.AddNodeContainerPlus(Position.CHILD, loopNode, out itemNode, "language", ItemType.ITEM, section, bitOffset, 4 * 8, descriptorLength);

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "ISO_639_language_code", ItemType.FIELD, section, ref bitOffset, 24, ref languageCode, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "audio_type", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        //Show a readable language code.
                        String languageCodeStr = "language_code: " + Utility.GetLanguageCodeStr(languageCode);
                        Utility.UpdateNode(itemNode,  languageCodeStr, ItemType.ITEM);
                    }
                    else
                    {
                        break;
                    }
                }

            }

            return result;
        }
        public static Result ParseSystemClockDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "external_clock_reference_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "clock_accuracy_integer", ItemType.FIELD, section, ref bitOffset, 6, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "clock_accuracy_exponent", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 5, ref fieldValue, ref descriptorLength);
            }

            return result;
        }

        public static Result ParseMultiplexBufferUtilizationDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "bound_valid_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "LTW_offset_lower_bound", ItemType.FIELD, section, ref bitOffset, 15, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "LTW_offset_upper_bound", ItemType.FIELD, section, ref bitOffset, 15, ref fieldValue, ref descriptorLength);
            }

            return result;
        }

        public static Result ParseCopyrightDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "copyright_identifier", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "additional_copyright_info", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }

        public static Result ParseMaximumBitrateDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "maximum_bitrate", ItemType.FIELD, section, ref bitOffset, 22, ref fieldValue, ref descriptorLength);
            }

            return result;
        }

        public static Result ParsePrivateDataIndicatorDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "private_data_indicator", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);
            }

            return result;
        }

        public static Result ParseSmoothingBufferDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "sb_leak_rate", ItemType.FIELD, section, ref bitOffset, 22, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "sb_size", ItemType.FIELD, section, ref bitOffset, 22, ref fieldValue, ref descriptorLength);
            }
            return result;
        }

        public static Result ParseSTDDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 7, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "leak_valid_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }
            return result;
        }

        public static Result ParseIBPDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "closed_gop_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "identical_gop_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "max_gop-length", ItemType.FIELD, section, ref bitOffset, 14, ref fieldValue, ref descriptorLength);
            }
            return result;
        }

        public static Result ParseMPEG4VideoDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "MPEG-4_visual_profile_and_level", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }
            return result;
        }

        public static Result ParseMPEG4AudioDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "MPEG-4_audio_profile_and_level", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }
            return result;
        }

        public static Result ParseIODDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "Scope_of_IOD_label", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "IOD_label", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "InitialObjectDescriptor", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }
            return result;
        }
        public static Result ParseSLDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "ES_ID", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }
            return result;
        }

        public static Result ParseFMCDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;
            TreeNode itemNode = null;

            TreeNode loopNode = null;
            if (result.Fine)
            {
                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out loopNode, "FMC_loop", ItemType.LOOP, section, bitOffset, descriptorLength, descriptorLength);
            }

            if (result.Fine)
            {
                Int64 languageCode = 0;
                while (descriptorLength > 0)
                {
                    result = Utility.AddNodeContainerPlus(Position.CHILD, loopNode, out itemNode, "FMC", ItemType.ITEM, section, bitOffset, 3 * 8, descriptorLength);

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "ES_ID", ItemType.FIELD, section, ref bitOffset, 16, ref languageCode, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "FlexMuxChannel", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                    }

                    if (!result.Fine)
                    {
                        break;
                    }
                }

            }

            return result;
        }
        public static Result ParseExternalEsIdDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "External_ES_ID", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }
            return result;
        }

        public static Result ParseMuxcodeDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "MuxCodeTableEntry", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }
            return result;
        }

        public static Result ParseFmxBufferSizeDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;
            TreeNode itemNode = null;

            TreeNode loopNode = null;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "DefaultFlexMuxBufferDescriptor", ItemType.FIELD, section, ref bitOffset, 24, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out loopNode, "FlexMuxBufferDescriptor_loop", ItemType.LOOP, section, bitOffset, descriptorLength, descriptorLength);
            }

            if (result.Fine)
            {
                while (descriptorLength > 0)
                {
                    result = Utility.AddNodeContainerPlus(Position.CHILD, loopNode, out itemNode, "FlexMuxBufferDescriptor", ItemType.ITEM, section, bitOffset, 4 * 8, descriptorLength);

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "flexMuxChannel", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "FB_BufferSize", ItemType.FIELD, section, ref bitOffset, 24, ref fieldValue, ref descriptorLength);
                    }

                    if (!result.Fine)
                    {
                        break;
                    }
                }

            }

            return result;
        }

        public static Result ParseMultiplexBufferDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "MB_buffer_size", ItemType.FIELD, section, ref bitOffset, 24, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "TB_leak_rate", ItemType.FIELD, section, ref bitOffset, 24, ref fieldValue, ref descriptorLength);
            }

            return result;
        }

        public static Result ParseFlexMuxTimingDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "FCR_ES_ID", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "FCRResolution", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "FCRLength", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "FmxRateLength", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            return result;
        }

        public static Result ParseContentLabelingDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            Int64 metadataApplicationFormat = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "metadata_application_format", ItemType.FIELD, section, ref bitOffset, 16, ref metadataApplicationFormat, ref descriptorLength);
            }

            if (result.Fine)
            {
                if (metadataApplicationFormat == 0xFFFF)
                {
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out newNode, "if (metadata_application_format== 0xFFFF)", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, newNode, out newNode, "metadata_application_format_identifier", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);
                    }
                }
            }

            Int64 contentReferenceIdRecordFlag = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "content_reference_id_record_flag", ItemType.FIELD, section, ref bitOffset, 1, ref contentReferenceIdRecordFlag, ref descriptorLength);
            }

            Int64 contentTimeBaseIndicator = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "content_time_base_indicator", ItemType.FIELD, section, ref bitOffset, 4, ref contentTimeBaseIndicator, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                if (contentReferenceIdRecordFlag == 1)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (content_reference_id_record_flag == ‘1')", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                    if (result.Fine)
                    {
                        Int64 contentReferenceIdRecordLength = 0;
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "content_reference_id_record_length", ItemType.FIELD, section, ref bitOffset, 8, ref contentReferenceIdRecordLength, ref descriptorLength);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeDataPlus(Position.CHILD, itemNode, out newNode, "content_reference_id_byte", ItemType.FIELD, section, ref bitOffset, 8 * contentReferenceIdRecordLength, ref descriptorLength);
                        }
                    }
                }
            }

            if (result.Fine)
            {
                if ((contentTimeBaseIndicator == 1) || (contentTimeBaseIndicator == 2))
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (content_time_base_indicator== 1|2)", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 7, ref fieldValue, ref descriptorLength);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "content_time_base_value", ItemType.FIELD, section, ref bitOffset, 33, ref fieldValue, ref descriptorLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 7, ref fieldValue, ref descriptorLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "metadata_time_base_value", ItemType.FIELD, section, ref bitOffset, 33, ref fieldValue, ref descriptorLength);
                        }
                    } 
                }

                if (contentTimeBaseIndicator == 2)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (content_time_base_indicator== 2)", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "contentId", ItemType.FIELD, section, ref bitOffset, 7, ref fieldValue, ref descriptorLength);
                        }
                    }  
                }

                if (result.Fine)
                {
                    if ((contentTimeBaseIndicator == 3)
                        || (contentTimeBaseIndicator == 4)
                            ||(contentTimeBaseIndicator == 5)
                            ||(contentTimeBaseIndicator == 6)
                            ||(contentTimeBaseIndicator == 7))
                    {
                        TreeNode itemNode = null;
                        result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (content_time_base_indicator==3|4|5|6|7)", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                        if (result.Fine)
                        {
                            Int64 timeBaseAssociationDataLength = 0;
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "time_base_association_data_length", ItemType.FIELD, section, ref bitOffset, 8, ref timeBaseAssociationDataLength, ref descriptorLength);

                            if (result.Fine)
                            {
                                result = Utility.AddNodeDataPlus(Position.CHILD, itemNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 8 * timeBaseAssociationDataLength, ref descriptorLength);
                            }
                        }
                    }
                }

            }

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "private_data_byte", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }

        public static Result ParseMetadataPointerDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            Int64 metadataApplicationFormat = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "metadata_application_format", ItemType.FIELD, section, ref bitOffset, 16, ref metadataApplicationFormat, ref descriptorLength);
            }

            if (result.Fine)
            {
                if (metadataApplicationFormat == 0xFFFF)
                {
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out newNode, "if (metadata_application_format== 0xFFFF)", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, newNode, out newNode, "metadata_application_format_identifier", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);
                    }
                }
            }

            Int64 metadataFormat = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "metadata_format", ItemType.FIELD, section, ref bitOffset, 8, ref metadataFormat, ref descriptorLength);
            }

            if (result.Fine)
            {
                if (metadataFormat == 0xFF)
                {
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out newNode, "if (metadata_format== 0xFF)", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, newNode, out newNode, "metadata_format_identifier", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);
                    }
                }
            }

            Int64 metadataServiceId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "metadata_service_id", ItemType.FIELD, section, ref bitOffset, 8, ref metadataServiceId, ref descriptorLength);
            }

            Int64 metadataLocatorRecordFlag = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "metadata_locator_record_flag", ItemType.FIELD, section, ref bitOffset, 1, ref metadataLocatorRecordFlag, ref descriptorLength);
            }

            Int64 mpegCarriageFlags = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "MPEG_carriage_flags", ItemType.FIELD, section, ref bitOffset, 2, ref mpegCarriageFlags, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 5, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                if (metadataLocatorRecordFlag == 1)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (metadata_locator_record_flag == '1')", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                    if (result.Fine)
                    {
                        Int64 metadataLocatorRecordLength = 0;
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "metadata_locator_record_length", ItemType.FIELD, section, ref bitOffset, 8, ref metadataLocatorRecordLength, ref descriptorLength);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeDataPlus(Position.CHILD, itemNode, out newNode, "metadata_locator_record_byte", ItemType.FIELD, section, ref bitOffset, 8 * metadataLocatorRecordLength, ref descriptorLength);
                        }
                    }
                }
            }

            if (result.Fine)
            {
                if ((mpegCarriageFlags == 0)
                    || (mpegCarriageFlags == 1)
                        || (mpegCarriageFlags == 2))
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (MPEG_carriage_flags == 0|1|2)", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "program_number", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                    }
                }

            }

            if (result.Fine)
            {
                if (mpegCarriageFlags == 1)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (MPEG_carriage_flags == 1)", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "transport_stream_location", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                    }
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "transport_stream_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                    }
                }

            }

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "private_data_byte", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }
		
        public static Result ParseMetadataDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            Int64 metadataApplicationFormat = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "metadata_application_format", ItemType.FIELD, section, ref bitOffset, 16, ref metadataApplicationFormat, ref descriptorLength);
            }

            if (result.Fine)
            {
                if (metadataApplicationFormat == 0xFFFF)
                {
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out newNode, "if (metadata_application_format== 0xFFFF)", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, newNode, out newNode, "metadata_application_format_identifier", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);
                    }
                }
            }

            Int64 metadataFormat = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "metadata_format", ItemType.FIELD, section, ref bitOffset, 8, ref metadataFormat, ref descriptorLength);
            }

            if (result.Fine)
            {
                if (metadataFormat == 0xFF)
                {
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out newNode, "if (metadata_format== 0xFF)", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, newNode, out newNode, "metadata_format_identifier", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);
                    }
                }
            }

            Int64 metadataServiceId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "metadata_service_id", ItemType.FIELD, section, ref bitOffset, 8, ref metadataServiceId, ref descriptorLength);
            }

            Int64 decoderConfigFlags  = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "decoder_config_flags", ItemType.FIELD, section, ref bitOffset, 3, ref decoderConfigFlags, ref descriptorLength);
            }

            Int64 dsmCcFlag = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "DSM-CC_flag", ItemType.FIELD, section, ref bitOffset, 1, ref dsmCcFlag, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                if (dsmCcFlag == 1)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (DSM-CC_flag == '1')", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                    if (result.Fine)
                    {
                        Int64 serviceIdentificationLength = 0;
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "service_identification_length", ItemType.FIELD, section, ref bitOffset, 8, ref serviceIdentificationLength, ref descriptorLength);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeDataPlus(Position.CHILD, itemNode, out newNode, "service_identification_record_byte", ItemType.FIELD, section, ref bitOffset, 8 * serviceIdentificationLength, ref descriptorLength);
                        }
                    }
                }
            }


            if (result.Fine)
            {
                if (decoderConfigFlags == 1)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (decoder_config_flags == '001')", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                    if (result.Fine)
                    {
                        Int64 decoderConfigLength = 0;
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "decoder_config_length", ItemType.FIELD, section, ref bitOffset, 8, ref decoderConfigLength, ref descriptorLength);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeDataPlus(Position.CHILD, itemNode, out newNode, "decoder_config_byte", ItemType.FIELD, section, ref bitOffset, 8 * decoderConfigLength, ref descriptorLength);
                        }
                    }
                }

            }

            if (result.Fine)
            {
                if (decoderConfigFlags == 3)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (decoder_config_flags == '011')", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                    if (result.Fine)
                    {
                        Int64 decConfigIdentificationRecordLength = 0;
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "dec_config_identification_record_length", ItemType.FIELD, section, ref bitOffset, 8, ref decConfigIdentificationRecordLength, ref descriptorLength);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeDataPlus(Position.CHILD, itemNode, out newNode, "dec_config_identification_record_byte", ItemType.FIELD, section, ref bitOffset, 8 * decConfigIdentificationRecordLength, ref descriptorLength);
                        }
                    }
                }

            }
			
            if (result.Fine)
            {
                if (decoderConfigFlags == 4)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (decoder_config_flags == '100')", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "decoder_config_metadata_service_id", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                    }
                }

            }
			
            if (result.Fine)
            {
                if ((decoderConfigFlags == 5) || (decoderConfigFlags == 6))
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (decoder_config_flags == '101'|'110')", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                    if (result.Fine)
                    {
                        Int64 reservedDataLength = 0;
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "reserved_data_length", ItemType.FIELD, section, ref bitOffset, 8, ref reservedDataLength, ref descriptorLength);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeDataPlus(Position.CHILD, itemNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 8 * reservedDataLength, ref descriptorLength);
                        }
                    }
                }

            }
			
            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "private_data_byte", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }

        public static Result ParseMetadataStdDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "metadata_input_leak_rate", ItemType.FIELD, section, ref bitOffset, 22, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "metadata_buffer_size", ItemType.FIELD, section, ref bitOffset, 22, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "metadata_output_leak_rate", ItemType.FIELD, section, ref bitOffset, 22, ref fieldValue, ref descriptorLength);
            }
            return result;
        }

        public static Result ParseAvcVideoDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "profile_idc", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "constraint_set0_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "constraint_set1_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "constraint_set2_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "AVC_compatible_flags", ItemType.FIELD, section, ref bitOffset, 5, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "level_idc", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "AVC_still_present", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "AVC_24_hour_picture_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 6, ref fieldValue, ref descriptorLength);
            }

            return result;
        }

        public static Result ParseIPMPDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "IPMP_Descriptor_ID", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "IPMP_ToolID", ItemType.FIELD, section, ref bitOffset, 128, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "ControlPoint", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "SequenceCode", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            Int64 ipmpDataLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "IPMP_Data_Length", ItemType.FIELD, section, ref bitOffset, 16, ref ipmpDataLength, ref descriptorLength);
            }
            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "IPMP_Data", ItemType.FIELD, section, ref bitOffset, ipmpDataLength * 8, ref descriptorLength);
            }

            Int64 isSigned = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "isSigned", ItemType.FIELD, section, ref bitOffset, 8, ref isSigned, ref descriptorLength);
            }

            if (result.Fine)
            {
                if (isSigned != 0)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "Signature", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
                }

            }

            return result;
        }
        public static Result ParseAvcTimingAndHrdDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "hrd_management_valid_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 6, ref fieldValue, ref descriptorLength);
            }


            if (result.Fine)
            {
                Int64 pictureAndTimingInfoPresent = 0;
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "picture_and_timing_info_present", ItemType.FIELD, section, ref bitOffset, 1, ref pictureAndTimingInfoPresent, ref descriptorLength);

                if (pictureAndTimingInfoPresent == 1)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (picture_and_timing_info_present)", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                    if (result.Fine)
                    {
                        Int64 flag90kHz = 0;
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "90kHz_flag", ItemType.FIELD, section, ref bitOffset, 1, ref flag90kHz, ref descriptorLength);

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 7, ref fieldValue, ref descriptorLength);
                        }

                        if (result.Fine)
                        {
                            if (flag90kHz == 0)
                            {
                                TreeNode innerItemNode = null;
                                result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out innerItemNode, "if (90kHz_flag = = '0')", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, innerItemNode, out newNode, "N", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);
                                }
                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, innerItemNode, out newNode, "K", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);
                                }
                            }
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "num_units_in_tick", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength); 
                        }
                    } 
                }
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "fixed_frame_rate_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "temporal_poc_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "picture_to_display_conversion_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 5, ref fieldValue, ref descriptorLength);
            }

            return result;
        }

        public static Result ParseMpeg2AacAudioDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "MPEG-2_AAC_profile", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "MPEG-2_AAC_channel_configuration", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "MPEG-2_AAC_additional_information", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }
            return result;
        }

        public static Result ParseNetworkNameDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            string networkName = null;
            if (result.Fine)
            {
                result = Utility.GetTextPlus(out networkName, section, bitOffset, descriptorLength, descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "network_name: " + networkName, ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }//ParseNetworkNameDescriptor

        public static Result ParseServiceListDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            while (descriptorLength > 0)
            {
                TreeNode itemNode = null;

                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "service_item", ItemType.FIELD, section, bitOffset, 24, descriptorLength);

                Int64 serviceId = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "service_id", ItemType.FIELD, section, ref bitOffset, 16, ref serviceId, ref descriptorLength);
                }

                Int64 serviceType = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "service_type", ItemType.FIELD, section, ref bitOffset, 8, ref serviceType, ref descriptorLength);
                }

                if (result.Fine)
                {
                    string itemDescription = "service_id: " + Utility.GetValueHexString(serviceId, 16)
                                                + ", service_type: " + Utility.GetValueHexString(serviceType, 16);
                    Utility.UpdateNode(itemNode, itemDescription, ItemType.ITEM);
                }
                else
                {
                    Utility.UpdateNode(itemNode, "service_item(invalid)", ItemType.ERROR);
                    break;
                }

            }//while (descriptorLength > 0)

            return result;
        }//ParseServicelistDescriptor

        public static Result ParseStuffingDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "stuffing_byte", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }//ParseStuffingDescriptor

        public static Result ParseSatelliteDeliverySystemDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "frequency", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "orbital_position", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "west_east_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "polarization", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "roll off", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "modulation_system", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "modulation_type", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "symbol_rate", ItemType.FIELD, section, ref bitOffset, 28, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "FEC_inner", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
            }

            return result;
        }//ParseSatelliteDeliverySystemDescriptor


        public static Result ParseCableDeliverySystemDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "frequency", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "FEC_outer", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "modulation", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "symbol_rate", ItemType.FIELD, section, ref bitOffset, 28, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "FEC_inner", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
            }

            return result;
        }//ParseCableDeliverySystemDescriptor

        public static Result ParseVbiDataDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            Int64 position1LeftLengthInBit = 0;
            Int64 position2LeftLengthInBit = 0;
            while (descriptorLength > 0)
            {
                TreeNode itemNode = null;

                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "VBI_data_info_item", ItemType.ITEM, section, bitOffset, 0, descriptorLength);

                //Make a copy of current length;
                position1LeftLengthInBit = section.GetLeftBitLength();

                Int64 dataServiceId = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "data_service_id", ItemType.FIELD, section, ref bitOffset, 8, ref dataServiceId, ref descriptorLength);
                }

                Int64 dataServiceDescriptorLength = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "data_service_descriptor_length", ItemType.FIELD, section, ref bitOffset, 8, ref dataServiceDescriptorLength, ref descriptorLength);
                }

                if (result.Fine)
                {
                    if ((0x01 <= dataServiceId) && (0x07 >= dataServiceId))//Should I exclude 0x3 according to the spec? Not...currently...
                    {
                        TreeNode ifNode = null;
                        result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out ifNode, "If (data_service_id in [0x01 ~ 0x07] )", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                        if (result.Fine)
                        {
                            Int64 i = dataServiceDescriptorLength;
                            while (i > 0)
                            {
                                TreeNode innerItemNode = null;

                                result = Utility.AddNodeContainerPlus(Position.CHILD, ifNode, out innerItemNode, "field_line_info_item", ItemType.ITEM, section, bitOffset, 8, descriptorLength);

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, innerItemNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
                                }

                                Int64 fieldParity = 0;
                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, innerItemNode, out newNode, "field_parity", ItemType.FIELD, section, ref bitOffset, 1, ref fieldParity, ref descriptorLength);
                                }

                                Int64 lineOffset = 0;
                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, innerItemNode, out newNode, "line_offset", ItemType.FIELD, section, ref bitOffset, 5, ref lineOffset, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    string itemDescription = "field_parity: " + Utility.GetValueHexString(fieldParity, 8)
                                                                + ", line_offset: " + Utility.GetValueHexString(lineOffset, 8);
                                    Utility.UpdateNode(innerItemNode, itemDescription, ItemType.ITEM);
                                }
                                else
                                {
                                    Utility.UpdateNode(itemNode, "field_line_info_item(invalid)", ItemType.ERROR);
                                    break;//Break once something unexpected.
                                }
                                --i;//Increase.
                            }//while( i > 0)
                        }

                    }//if ((0x01 <= dataServiceId) && (0x07 >= dataServiceId))
                    else
                    {
                        if (result.Fine)
                        {
                            result = Utility.AddNodeDataPlus(Position.CHILD, itemNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, dataServiceDescriptorLength * 8, ref descriptorLength);
                        }
                    }
                }

                //Make the second copy of current length;
                position2LeftLengthInBit = section.GetLeftBitLength();
                if (result.Fine)
                {
                    Int64 itemBitLength = (position1LeftLengthInBit - position2LeftLengthInBit);

                    string itemDescription = "data_service_id: " + Utility.GetValueHexString(dataServiceId, 8)
                                                + ", data_service_descriptor_length: " + Utility.GetValueHexString(dataServiceDescriptorLength, 8);
                    Utility.UpdateNodeLength(itemNode, itemDescription, ItemType.ITEM, itemBitLength);
                }
                else
                {
                    Utility.UpdateNode(itemNode, "VBI_data_info_item(invalid)", ItemType.ERROR);
                    break;//Break once something unexpected.
                }
            }//while (descriptorLength > 0)

            return result;
        }//ParseVbiDataDescriptor

        public static Result ParseVbiTeletextDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            while (descriptorLength > 0)
            {
                TreeNode itemNode = null;

                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "teletext_item", ItemType.FIELD, section, bitOffset, 40, descriptorLength);

                Int64 languageCode = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "ISO_639_language_code", ItemType.FIELD, section, ref bitOffset, 24, ref languageCode, ref descriptorLength);
                }

                Int64 teletextType = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "teletext_type", ItemType.FIELD, section, ref bitOffset, 5, ref teletextType, ref descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "teletext_magazine_number", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "teletext_page_number", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                }

                if (result.Fine)
                {
                    string itemDescription = "language_code: " + Utility.GetLanguageCodeStr(languageCode) + ", teletext_type: " + Utility.GetValueHexString(teletextType, 8);
                    Utility.UpdateNode(itemNode, itemDescription, ItemType.ITEM);
                }
                else
                {
                    Utility.UpdateNode(itemNode, "teletext_item(invalid)", ItemType.ERROR);
                    break;
                }

            }//while (descriptorLength > 0)

            return result;
        }//ParseVbiTeletextDescriptor

        public static Result ParseBouquetNameDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            string bouquetName = null;
            if (result.Fine)
            {
                result = Utility.GetTextPlus(out bouquetName, section, bitOffset, descriptorLength, descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "bouquet_name: " + bouquetName, ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }//ParseBouquetNameDescriptor

        public static Result ParseServiceDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "service_type", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            Int64 serviceProviderNameLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "service_provider_name_length", ItemType.FIELD, section, ref bitOffset, 8, ref serviceProviderNameLength, ref descriptorLength);
            }
            if (result.Fine)
            {
                string serviceProviderName = null;
                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out serviceProviderName, section, bitOffset, serviceProviderNameLength * 8, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "service_provider_name: " + serviceProviderName, ItemType.FIELD, section, ref bitOffset, serviceProviderNameLength * 8, ref descriptorLength);
                }
            }

            Int64 serviceNameLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "service_name_length", ItemType.FIELD, section, ref bitOffset, 8, ref serviceNameLength, ref descriptorLength);
            }
            if (result.Fine)
            {
                string serviceName = null;
                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out serviceName, section, bitOffset, serviceNameLength * 8, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "service_name: " + serviceName, ItemType.FIELD, section, ref bitOffset, serviceNameLength * 8, ref descriptorLength);
                }
            }
            return result;
        }//ParseServiceDescriptor

        public static Result ParseCountryAvailabilityDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "country_availability_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 7, ref fieldValue, ref descriptorLength);
            }

            while (descriptorLength > 0)
            {
                TreeNode itemNode = null;

                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "country_code_item", ItemType.FIELD, section, bitOffset, 24, descriptorLength);

                Int64 countryCode = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "country_code", ItemType.FIELD, section, ref bitOffset, 24, ref countryCode, ref descriptorLength);
                }

                if (result.Fine)
                {
                    string itemDescription = "country_code: " + Utility.GetCountryCodeStr(countryCode);
                    Utility.UpdateNode(itemNode, itemDescription, ItemType.ITEM);
                }
                else
                {
                    Utility.UpdateNode(itemNode, "country_code_item(invalid)", ItemType.ERROR);
                    break;
                }

            }//while (descriptorLength > 0)

            return result;
        }//ParseCountryAvailabilityDescriptor

        public static Result ParseLinkageDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "transport_stream_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "original_network_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "service_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }

            Int64 linkageType = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "linkage_type", ItemType.FIELD, section, ref bitOffset, 8, ref linkageType, ref descriptorLength);
            }

            if (linkageType == 0x08)
            {
                TreeNode itemNode = null;
                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (linkage_type ==0x08)", ItemType.ITEM, section, bitOffset, 0, descriptorLength);
                if (result.Fine)
                {
                    Int64 handoverType = 0;
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "hand-over_type", ItemType.FIELD, section, ref bitOffset, 4, ref handoverType, ref descriptorLength);

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
                    }

                    Int64 originType = 0;
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "origin_type", ItemType.FIELD, section, ref bitOffset, 1, ref originType, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        if ((handoverType >= 1) && (handoverType <= 3))
                        {
                            TreeNode innerItemNode = null;
                            result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out innerItemNode, "if hand-over_type in [0x1~0x3]", ItemType.ITEM, section, bitOffset, 16, descriptorLength);
                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, innerItemNode, out newNode, "network_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                            }
                        }
                    }

                    if (result.Fine)
                    {
                        if (0x00 == originType)
                        {
                            TreeNode innerItemNode = null;
                            result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out innerItemNode, "if (origin_type ==0x00)", ItemType.ITEM, section, bitOffset, 16, descriptorLength);
                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, innerItemNode, out newNode, "initial_service_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                            }
                        }
                    }
                }
            }

            if (linkageType == 0x0D)
            {
                TreeNode itemNode = null;
                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (linkage_type ==0x0D)", ItemType.ITEM, section, bitOffset, 24, descriptorLength);
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "target_event_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "target_listed", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "event_simulcast", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 6, ref fieldValue, ref descriptorLength);
                    }
                }
            }

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "private_data_byte", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }

        public static Result ParseNvodReferenceDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            while (descriptorLength > 0)
            {
                TreeNode itemNode = null;

                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "reference_service_item", ItemType.FIELD, section, bitOffset, 48, descriptorLength);

                Int64 transportStreamId = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "transport_stream_id", ItemType.FIELD, section, ref bitOffset, 16, ref transportStreamId, ref descriptorLength);
                }

                Int64 originalNetworkId = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "original_network_id", ItemType.FIELD, section, ref bitOffset, 16, ref originalNetworkId, ref descriptorLength);
                }

                Int64 serviceId = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "service_id", ItemType.FIELD, section, ref bitOffset, 16, ref serviceId, ref descriptorLength);
                }

                if (result.Fine)
                {
                    string itemDescription = "transport_stream_id: " + Utility.GetValueHexString(transportStreamId, 16)
                                            + ", original_network_id: " + Utility.GetValueHexString(originalNetworkId, 16)
                                            + ", service_id: " + Utility.GetValueHexString(serviceId, 16);
                    Utility.UpdateNode(itemNode, itemDescription, ItemType.ITEM);
                }
                else
                {
                    Utility.UpdateNode(itemNode, "reference_service_item(invalid)", ItemType.ERROR);
                    break;
                }

            }//while (descriptorLength > 0)

            return result;
        }//ParseNvodReferenceDescriptor

        public static Result ParseTimeShiftedServiceDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reference_service_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }

            return result;
        }//ParseTimeShiftedServiceDescriptor

        public static Result ParseShortEventDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            //Int64 fieldValue = 0;

            if (result.Fine)
            {
                Int64 languageCode = 0;
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "ISO_639_language_code", ItemType.FIELD, section, ref bitOffset, 24, ref languageCode, ref descriptorLength);

                if (result.Fine)
                {
                    String languageCodeStr = "language_code: " + Utility.GetLanguageCodeStr(languageCode);
                    Utility.UpdateNode(newNode, languageCodeStr, ItemType.FIELD);
                }
            }

            Int64 eventNameLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "event_name_length", ItemType.FIELD, section, ref bitOffset, 8, ref eventNameLength, ref descriptorLength);
            }
            if (result.Fine)
            {
                string eventName = null;
                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out eventName, section, bitOffset, eventNameLength * 8, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "event_name: " + eventName, ItemType.FIELD, section, ref bitOffset, eventNameLength*8, ref descriptorLength);
                }
            }

            Int64 textLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "text_length", ItemType.FIELD, section, ref bitOffset, 8, ref textLength, ref descriptorLength);
            }
            if (result.Fine)
            {
                string eventText = null;
                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out eventText, section, bitOffset, textLength * 8, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "event_text: " + eventText, ItemType.FIELD, section, ref bitOffset, textLength * 8, ref descriptorLength);
                }
            }
            return result;
        }//ParseShortEventDescriptor

        public static Result ParseExtendedEventDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 descriptorNumber = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "descriptor_number", ItemType.FIELD, section, ref bitOffset, 4, ref descriptorNumber, ref descriptorLength);
            }

            Int64 lastDescriptorNumber = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "last_descriptor_number", ItemType.FIELD, section, ref bitOffset, 4, ref lastDescriptorNumber, ref descriptorLength);
            }

            if (result.Fine)
            {
                Int64 languageCode = 0;
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "ISO_639_language_code", ItemType.FIELD, section, ref bitOffset, 24, ref languageCode, ref descriptorLength);

                if (result.Fine)
                {
                    String languageCodeStr = "language_code: " + Utility.GetLanguageCodeStr(languageCode);
                    Utility.UpdateNode(newNode, languageCodeStr, ItemType.FIELD);
                }
            }

            Int64 lengthOfItems = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "length_of_items", ItemType.FIELD, section, ref bitOffset, 8, ref lengthOfItems, ref descriptorLength);
            }

            if (result.Fine)
            {
            
                lengthOfItems = lengthOfItems*8;//Convert to bits.

                TreeNode loopNode = null;
                //Add a container item.
                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out loopNode, "item_loop", ItemType.LOOP, section, bitOffset, lengthOfItems, descriptorLength);

                if (result.Fine)
                {
                    Int64 position1LeftLengthInBit = 0;
                    Int64 position2LeftLengthInBit = 0;
                    while ( lengthOfItems > 0 )
                    {
                        TreeNode itemNode = null;

                        result = Utility.AddNodeContainerPlus(Position.CHILD, loopNode, out itemNode, "item", ItemType.LOOP, section, bitOffset, 0, descriptorLength);

                        //Make a copy of current length;
                        position1LeftLengthInBit = section.GetLeftBitLength();

                        Int64 itemDescriptionLength = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "item_description_length", ItemType.FIELD, section, ref bitOffset, 8, ref itemDescriptionLength, ref descriptorLength);
                        }
                        
                        string itemDescription = null;
                        if (result.Fine)
                        {
                            if (result.Fine)
                            {
                                result = Utility.GetTextPlus(out itemDescription, section, bitOffset, itemDescriptionLength * 8, descriptorLength);
                            }

                            if (result.Fine)
                            {
                                result = Utility.AddNodeDataPlus(Position.CHILD, itemNode, out newNode, "item_description_char: " + itemDescription, ItemType.FIELD, section, ref bitOffset, itemDescriptionLength * 8, ref descriptorLength);
                            }
                        }

                        Int64 itemLength = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "item_length", ItemType.FIELD, section, ref bitOffset, 8, ref itemLength, ref descriptorLength);
                        }

                        if (result.Fine)
                        {
                            string itemChar = null;
                            if (result.Fine)
                            {
                                result = Utility.GetTextPlus(out itemChar, section, bitOffset, itemLength * 8, descriptorLength);
                            }

                            if (result.Fine)
                            {
                                result = Utility.AddNodeDataPlus(Position.CHILD, itemNode, out newNode, "item_char: " + itemChar, ItemType.FIELD, section, ref bitOffset, itemLength * 8, ref descriptorLength);
                            }
                        }

                        //Make the second copy of current length;
                        position2LeftLengthInBit = section.GetLeftBitLength();

                        if (result.Fine)
                        {
                            Int64 itemBitLength = (position1LeftLengthInBit - position2LeftLengthInBit);
                            Utility.UpdateNodeLength(itemNode, "Item: " + itemDescription, ItemType.ITEM, itemBitLength);
                            
                            //Decrease the length.
                            lengthOfItems -= itemBitLength;
                        }
                        else
                        {
                            Utility.UpdateNode(itemNode, "item(invalid)", ItemType.ERROR);
                            break;//Break once something unexpected.
                        }
                    }//while ( lengthOfItems > 0 )

                }//To parse items.
            }

            Int64 textLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "text_length", ItemType.FIELD, section, ref bitOffset, 8, ref textLength, ref descriptorLength);
            }
            if (result.Fine)
            {
                string eventText = null;
                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out eventText, section, bitOffset, textLength * 8, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "event_text: " + eventText, ItemType.FIELD, section, ref bitOffset, textLength * 8, ref descriptorLength);
                }
            }

            return result;
        }//ParseExtendedEventDescriptor

        public static Result ParseTimeShiftedEventDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reference_service_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }


            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reference_event_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }

            return result;
        }//ParseTimeShiftedEventDescriptor

        public static Result ParseComponentDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "stream_content", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "component_type", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "component_tag", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                Int64 languageCode = 0;
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "ISO_639_language_code", ItemType.FIELD, section, ref bitOffset, 24, ref languageCode, ref descriptorLength);

                if (result.Fine)
                {
                    String languageCodeStr = "language_code: " + Utility.GetLanguageCodeStr(languageCode);
                    Utility.UpdateNode(newNode, languageCodeStr, ItemType.FIELD);
                }
            }

            if (result.Fine)
            {
                string textChar = null;
                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out textChar, section, bitOffset, descriptorLength, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "textChar: " + textChar, ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
                }
            }

            return result;
        }//ParseComponentDescriptor

        public static Result ParseMosaicDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "mosaic_entry_point", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "number_of_horizontal_elementary_cells", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "number_of_vertical_elementary_cells", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                TreeNode loopNode = null;
                //Add a container item.
                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out loopNode, "cell_loop", ItemType.LOOP, section, bitOffset, descriptorLength, descriptorLength);

                if (result.Fine)
                {
                    Int64 position1LeftLengthInBit = 0;
                    Int64 position2LeftLengthInBit = 0;
                    while (descriptorLength > 0)
                    {
                        TreeNode itemNode = null;

                        result = Utility.AddNodeContainerPlus(Position.CHILD, loopNode, out itemNode, "logical_cell", ItemType.LOOP, section, bitOffset, 0, descriptorLength);

                        //Make a copy of current length;
                        position1LeftLengthInBit = section.GetLeftBitLength();

                        Int64 logicalCellId = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "logical_cell_id", ItemType.FIELD, section, ref bitOffset, 6, ref logicalCellId, ref descriptorLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 7, ref fieldValue, ref descriptorLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "logical_cell_presentation_info", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
                        }

                        Int64 elementaryCellFieldLength = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "elementary_cell_field_length", ItemType.FIELD, section, ref bitOffset, 8, ref elementaryCellFieldLength, ref descriptorLength);
                        }

                        if (result.Fine)
                        {
                            TreeNode elementaryCellIdLoopNode = null;

                            result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out elementaryCellIdLoopNode, "elementary_cell_id_loop", ItemType.LOOP, section, bitOffset, elementaryCellFieldLength*8, descriptorLength);

                            if (result.Fine)
                            {
                                for (Int64 i = 0; i < elementaryCellFieldLength; i++)
                                {
                                    TreeNode elementaryCellIdItemNode = null;

                                    result = Utility.AddNodeContainerPlus(Position.CHILD, elementaryCellIdLoopNode, out elementaryCellIdItemNode, "elementary_cell_id_item", ItemType.LOOP, section, bitOffset, 8, descriptorLength);

                                    if (result.Fine)
                                    {
                                        result = Utility.AddNodeFieldPlus(Position.CHILD, elementaryCellIdItemNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength); 
                                    }

                                    Int64 elementaryCellId = 0;
                                    if (result.Fine)
                                    {
                                        result = Utility.AddNodeFieldPlus(Position.CHILD, elementaryCellIdItemNode, out newNode, "elementary_cell_id", ItemType.FIELD, section, ref bitOffset, 6, ref elementaryCellId, ref descriptorLength);
                                    }

                                    if (result.Fine)
                                    {
                                        string itemDescription = String.Format("elementary_cell_id: 0x{0, 2:X2}", elementaryCellId);
                                        Utility.UpdateNode(elementaryCellIdItemNode, itemDescription, ItemType.ITEM);
                                    }
                                    else
                                    {
                                        Utility.UpdateNode(elementaryCellIdItemNode, "elementary_cell_id_item(invalid)", ItemType.ERROR);
                                        break;
                                    }

                                }//for (Int64 i = 0; i < elementaryCellFieldLength; i++)
                            }
                        }

                        Int64 cellLinkageInfo = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "cell_linkage_info", ItemType.FIELD, section, ref bitOffset, 8, ref cellLinkageInfo, ref descriptorLength);
                        }

                        if (result.Fine)
                        {
                            if (0x01 == cellLinkageInfo)
                            {
                                TreeNode ifNode = null;
                                result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out ifNode, "If (cell_linkage_info ==0x01)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "bouquet_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                                }
                            }//if (0x01 == cellLinkageInfo)
                        }

                        if (result.Fine)
                        {
                            if (0x02 == cellLinkageInfo)
                            {
                                TreeNode ifNode = null;
                                result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out ifNode, "If (cell_linkage_info ==0x02)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "original_network_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "transport_stream_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "service_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                                }
                            }//if (0x02 == cellLinkageInfo)
                        }

                        if (result.Fine)
                        {
                            if (0x03 == cellLinkageInfo)
                            {
                                TreeNode ifNode = null;
                                result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out ifNode, "If (cell_linkage_info ==0x03)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "original_network_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "transport_stream_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "service_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                                }
                            }//if (0x03 == cellLinkageInfo)
                        }

                        if (result.Fine)
                        {
                            if (0x04 == cellLinkageInfo)
                            {
                                TreeNode ifNode = null;
                                result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out ifNode, "If (cell_linkage_info ==0x04)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "original_network_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "transport_stream_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "service_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "event_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                                }
                            }//if (0x04 == cellLinkageInfo)
                        }

                        //Make the second copy of current length;
                        position2LeftLengthInBit = section.GetLeftBitLength();
                        if (result.Fine)
                        {
                            Int64 itemBitLength = (position1LeftLengthInBit - position2LeftLengthInBit);

                            string itemDescription = String.Format("logical_cell_id: 0x{0, 2:X2}", logicalCellId);
                            Utility.UpdateNodeLength(itemNode, itemDescription, ItemType.ITEM, itemBitLength);
                        }
                        else
                        {
                            Utility.UpdateNode(itemNode, "logical_cell(invalid)", ItemType.ERROR);
                            break;//Break once something unexpected.
                        }
                    }//while (descriptorLength > 0)

                }//To parse items.
            }

            return result;
        }//ParseMosaicDescriptor

        public static Result ParseStreamIdentifierDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "component_tag", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            return result;
        }//ParseStreamIdentifierDescriptor

        public static Result ParseCaIdentifierDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            while (descriptorLength > 0)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "CA_system_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);

                if (!result.Fine)
                {
                    break;
                }
            }//while (descriptorLength > 0)

            return result;
        }//ParseCaIdentifierDescriptor

        public static Result ParseContentDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            while (descriptorLength > 0)
            {
                TreeNode itemNode = null;

                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "content_item", ItemType.FIELD, section, bitOffset, 16, descriptorLength);

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "content_nibble_level_1", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "content_nibble_level_2", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
                }

                Int64 userByte = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "user_byte", ItemType.FIELD, section, ref bitOffset, 8, ref userByte, ref descriptorLength);
                }

                if (result.Fine)
                {
                    string itemDescription = String.Format("user_byte: 0x{0,2:X2}", userByte);
                    Utility.UpdateNode(itemNode, itemDescription, ItemType.ITEM);
                }
                else
                {
                    Utility.UpdateNode(itemNode, "content_item(invalid)", ItemType.ERROR);
                    break;
                }

            }//while (descriptorLength > 0)

            return result;
        }//ParseContentDescriptor

        public static Result ParseParentalRatingDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            //Int64 fieldValue = 0;

            while (descriptorLength > 0)
            {
                TreeNode itemNode = null;

                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "rating_item", ItemType.FIELD, section, bitOffset, 32, descriptorLength);

                Int64 countryCode = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "country_code", ItemType.FIELD, section, ref bitOffset, 24, ref countryCode, ref descriptorLength);
                }

                Int64 rating = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "rating", ItemType.FIELD, section, ref bitOffset, 8, ref rating, ref descriptorLength);
                }

                if (result.Fine)
                {
                    string itemDescription = "country_code: " + Utility.GetCountryCodeStr(countryCode) + ", rating: " + Utility.GetValueHexString(rating, 8);
                    Utility.UpdateNode(itemNode, itemDescription, ItemType.ITEM);
                }
                else
                {
                    Utility.UpdateNode(itemNode, "rating_item(invalid)", ItemType.ERROR);
                    break;
                }

            }//while (descriptorLength > 0)

            return result;
        }//ParseParentalRatingDescriptor

        public static Result ParseTeletextDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            while (descriptorLength > 0)
            {
                TreeNode itemNode = null;

                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "teletext_item", ItemType.FIELD, section, bitOffset, 40, descriptorLength);

                Int64 languageCode = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "ISO_639_language_code", ItemType.FIELD, section, ref bitOffset, 24, ref languageCode, ref descriptorLength);
                }

                Int64 teletextType = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "teletext_type", ItemType.FIELD, section, ref bitOffset, 5, ref teletextType, ref descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "teletext_magazine_number", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "teletext_page_number", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                }

                if (result.Fine)
                {
                    string itemDescription = "language_code: " + Utility.GetLanguageCodeStr(languageCode) + ", teletext_type: " + Utility.GetValueHexString(teletextType, 8);
                    Utility.UpdateNode(itemNode, itemDescription, ItemType.ITEM);
                }
                else
                {
                    Utility.UpdateNode(itemNode, "teletext_item(invalid)", ItemType.ERROR);
                    break;
                }

            }//while (descriptorLength > 0)

            return result;
        }//ParseTeletextDescriptor

        public static Result ParseTelephoneDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "foreign_availability", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "connection_type", ItemType.FIELD, section, ref bitOffset, 5, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            Int64 countryPrefixLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "country_prefix_length", ItemType.FIELD, section, ref bitOffset, 2, ref countryPrefixLength, ref descriptorLength);
            }

            Int64 internationalAreaCodeLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "international_area_code_length", ItemType.FIELD, section, ref bitOffset, 3, ref internationalAreaCodeLength, ref descriptorLength);
            }

            Int64 operatorCodeLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "operator_code_length", ItemType.FIELD, section, ref bitOffset, 2, ref operatorCodeLength, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            Int64 nationalAreaCodeLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "national_area_code_length", ItemType.FIELD, section, ref bitOffset, 3, ref nationalAreaCodeLength, ref descriptorLength);
            }

            Int64 coreNumberLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "core_number_length", ItemType.FIELD, section, ref bitOffset, 4, ref coreNumberLength, ref descriptorLength);
            }

            if (result.Fine)
            {
                string text = null;
                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out text, section, bitOffset, countryPrefixLength * 8, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "country_prefix_char: " + text, ItemType.FIELD, section, ref bitOffset, countryPrefixLength * 8, ref descriptorLength);
                }
            }

            if (result.Fine)
            {
                string text = null;
                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out text, section, bitOffset, internationalAreaCodeLength * 8, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "internationalAreaCodeLength: " + text, ItemType.FIELD, section, ref bitOffset, internationalAreaCodeLength * 8, ref descriptorLength);
                }
            }

            if (result.Fine)
            {
                string text = null;
                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out text, section, bitOffset, operatorCodeLength * 8, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "operator_code_char: " + text, ItemType.FIELD, section, ref bitOffset, operatorCodeLength * 8, ref descriptorLength);
                }
            }

            if (result.Fine)
            {
                string text = null;
                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out text, section, bitOffset, nationalAreaCodeLength * 8, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "national_area_code_char: " + text, ItemType.FIELD, section, ref bitOffset, nationalAreaCodeLength * 8, ref descriptorLength);
                }
            }

            if (result.Fine)
            {
                string text = null;
                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out text, section, bitOffset, coreNumberLength * 8, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "core_number_char: " + text, ItemType.FIELD, section, ref bitOffset, coreNumberLength * 8, ref descriptorLength);
                }
            }

            return result;
        }//ParseTelephoneDescriptor

        public static Result ParseLocalTimeOffsetDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            while (descriptorLength > 0)
            {
                TreeNode itemNode = null;

                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "local_time_offset_item", ItemType.FIELD, section, bitOffset, 13*8, descriptorLength);

                Int64 countryCode = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "country_code", ItemType.FIELD, section, ref bitOffset, 24, ref countryCode, ref descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "country_region_id", ItemType.FIELD, section, ref bitOffset, 6, ref fieldValue, ref descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "local_time_offset_polarity", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
                }

                Int64 localTimeOffset = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "local_time_offset", ItemType.FIELD, section, ref bitOffset, 16, ref localTimeOffset, ref descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "time_of_change", ItemType.FIELD, section, ref bitOffset, 40, ref fieldValue, ref descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "next_time_offset", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                }

                if (result.Fine)
                {
                    string itemDescription = "country_code: " + Utility.GetCountryCodeStr(countryCode) + ", local_time_offset: " + Utility.GetValueHexString(localTimeOffset, 16);
                    Utility.UpdateNode(itemNode, itemDescription, ItemType.ITEM);
                }
                else
                {
                    Utility.UpdateNode(itemNode, "local_time_offset_item(invalid)", ItemType.ERROR);
                    break;
                }

            }//while (descriptorLength > 0)

            return result;
        }//ParseLocalTimeOffsetDescriptor

        public static Result ParseSubtitlingDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            while (descriptorLength > 0)
            {
                TreeNode itemNode = null;

                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "subtitling_item", ItemType.FIELD, section, bitOffset, 64, descriptorLength);

                Int64 languageCode = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "ISO_639_language_code", ItemType.FIELD, section, ref bitOffset, 24, ref languageCode, ref descriptorLength);
                }

                Int64 subtitlingtype = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "subtitling_type", ItemType.FIELD, section, ref bitOffset, 8, ref subtitlingtype, ref descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "composition_page_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "teletext_pagancillary_page_ide_number", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                }

                if (result.Fine)
                {
                    string itemDescription = "language_code: " + Utility.GetLanguageCodeStr(languageCode) + ", subtitling_type: " + Utility.GetValueHexString(subtitlingtype, 8);
                    Utility.UpdateNode(itemNode, itemDescription, ItemType.ITEM);
                }
                else
                {
                    Utility.UpdateNode(itemNode, "subtitling_item(invalid)", ItemType.ERROR);
                    break;
                }

            }//while (descriptorLength > 0)

            return result;
        }//ParseSubtitlingDescriptor

        public static Result ParseTerrestrialDeliverySystemDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "centre_frequency", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);
            }


            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "bandwidth", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
            }


            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "priority", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }


            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "Time_Slicing_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }


            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "MPE-FEC_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }


            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "constellation", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }


            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "hierarchy_information", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
            }


            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "code_rate-HP_stream", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
            }


            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "code_rate-LP_stream", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
            }


            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "guard_interval", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "transmission_mode", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "other_frequency_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);
            }

            return result;
        }//ParseTerrestrialDeliverySystemDescriptor

        public static Result ParseMultilingualNetworkNameDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            //Int64 fieldValue = 0;

            Int64 position1LeftLengthInBit = 0;
            Int64 position2LeftLengthInBit = 0;

            while (descriptorLength > 0)
            {
                TreeNode itemNode = null;

                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "network_name_item", ItemType.FIELD, section, bitOffset, 32, descriptorLength);

                //Make a copy of current length;
                position1LeftLengthInBit = section.GetLeftBitLength();

                Int64 languageCode = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "ISO_639_language_code", ItemType.FIELD, section, ref bitOffset, 24, ref languageCode, ref descriptorLength);
                }

                Int64 networkNameLength = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "network_name_length", ItemType.FIELD, section, ref bitOffset, 8, ref networkNameLength, ref descriptorLength);
                }

                string networkName = null;
                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out networkName, section, bitOffset, networkNameLength * 8, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, itemNode, out newNode, "network_name: " + networkName, ItemType.FIELD, section, ref bitOffset, networkNameLength * 8, ref descriptorLength);
                }

                //Make a second copy of current length;
                position2LeftLengthInBit = section.GetLeftBitLength();

                if (result.Fine)
                {
                    Int64 itemBitLength = position1LeftLengthInBit - position2LeftLengthInBit;
                    string itemDescription = "language_code: " + Utility.GetLanguageCodeStr(languageCode) + ", network_name: " + networkName;
                    Utility.UpdateNodeLength(itemNode, itemDescription, ItemType.ITEM, itemBitLength);
                }
                else
                {
                    Utility.UpdateNode(itemNode, "network_name_item(invalid)", ItemType.ERROR);
                    break;
                }

            }//while (descriptorLength > 0)

            return result;
        }//ParseMultilingualNetworkNameDescriptor

        public static Result ParseMultilingualBouquetNameDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            //Int64 fieldValue = 0;

            Int64 position1LeftLengthInBit = 0;
            Int64 position2LeftLengthInBit = 0;

            while (descriptorLength > 0)
            {
                TreeNode itemNode = null;

                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "bouquet_name_item", ItemType.FIELD, section, bitOffset, 32, descriptorLength);

                //Make a copy of current length;
                position1LeftLengthInBit = section.GetLeftBitLength();

                Int64 languageCode = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "ISO_639_language_code", ItemType.FIELD, section, ref bitOffset, 24, ref languageCode, ref descriptorLength);
                }

                Int64 bouquetNameLength = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "bouquet_name_length", ItemType.FIELD, section, ref bitOffset, 8, ref bouquetNameLength, ref descriptorLength);
                }

                string bouquetName = null;
                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out bouquetName, section, bitOffset, bouquetNameLength * 8, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, itemNode, out newNode, "bouquet_name: " + bouquetName, ItemType.FIELD, section, ref bitOffset, bouquetNameLength * 8, ref descriptorLength);
                }

                //Make a second copy of current length;
                position2LeftLengthInBit = section.GetLeftBitLength();

                if (result.Fine)
                {
                    Int64 itemBitLength = position1LeftLengthInBit - position2LeftLengthInBit;
                    string itemDescription = "language_code: " + Utility.GetLanguageCodeStr(languageCode) + ", bouquet_name: " + bouquetName;
                    Utility.UpdateNodeLength(itemNode, itemDescription, ItemType.ITEM, itemBitLength);
                }
                else
                {
                    Utility.UpdateNode(itemNode, "bouquet_name_item(invalid)", ItemType.ERROR);
                    break;
                }

            }//while (descriptorLength > 0)

            return result;
        }//ParseMultilingualBouquetNameDescriptor

        public static Result ParseMultilingualServiceNameDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            //Int64 fieldValue = 0;

            Int64 position1LeftLengthInBit = 0;
            Int64 position2LeftLengthInBit = 0;

            while (descriptorLength > 0)
            {
                TreeNode itemNode = null;

                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "service_name_item", ItemType.FIELD, section, bitOffset, 32, descriptorLength);

                //Make a copy of current length;
                position1LeftLengthInBit = section.GetLeftBitLength();

                Int64 languageCode = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "ISO_639_language_code", ItemType.FIELD, section, ref bitOffset, 24, ref languageCode, ref descriptorLength);
                }

                Int64 serviceProviderNameLength = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "service_provider_name_length", ItemType.FIELD, section, ref bitOffset, 8, ref serviceProviderNameLength, ref descriptorLength);
                }

                string serviceProviderName = null;
                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out serviceProviderName, section, bitOffset, serviceProviderNameLength * 8, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, itemNode, out newNode, "service_provider_name: " + serviceProviderName, ItemType.FIELD, section, ref bitOffset, serviceProviderNameLength * 8, ref descriptorLength);
                }

                Int64 serviceNameLength = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "service_name_length", ItemType.FIELD, section, ref bitOffset, 8, ref serviceNameLength, ref descriptorLength);
                }

                string serviceName = null;
                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out serviceName, section, bitOffset, serviceNameLength * 8, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, itemNode, out newNode, "service_name: " + serviceName, ItemType.FIELD, section, ref bitOffset, serviceNameLength * 8, ref descriptorLength);
                }


                //Make a second copy of current length;
                position2LeftLengthInBit = section.GetLeftBitLength();

                if (result.Fine)
                {
                    Int64 itemBitLength = position1LeftLengthInBit - position2LeftLengthInBit;
                    string itemDescription = "language_code: " + Utility.GetLanguageCodeStr(languageCode) + ", service_name: " + serviceName;
                    Utility.UpdateNodeLength(itemNode, itemDescription, ItemType.ITEM, itemBitLength);
                }
                else
                {
                    Utility.UpdateNode(itemNode, "service_name_item(invalid)", ItemType.ERROR);
                    break;
                }

            }//while (descriptorLength > 0)

            return result;
        }//ParseMultilingualServiceNameDescriptor

        public static Result ParseMultilingualComponentDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "component_tag", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            Int64 position1LeftLengthInBit = 0;
            Int64 position2LeftLengthInBit = 0;

            while (descriptorLength > 0)
            {
                TreeNode itemNode = null;

                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "component_description_item", ItemType.FIELD, section, bitOffset, 32, descriptorLength);

                //Make a copy of current length;
                position1LeftLengthInBit = section.GetLeftBitLength();

                Int64 languageCode = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "ISO_639_language_code", ItemType.FIELD, section, ref bitOffset, 24, ref languageCode, ref descriptorLength);
                }

                Int64 textDescriptionLength = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "text_description_length", ItemType.FIELD, section, ref bitOffset, 8, ref textDescriptionLength, ref descriptorLength);
                }

                string textDescription = null;
                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out textDescription, section, bitOffset, textDescriptionLength * 8, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, itemNode, out newNode, "text_char: " + textDescription, ItemType.FIELD, section, ref bitOffset, textDescriptionLength * 8, ref descriptorLength);
                }

                //Make a second copy of current length;
                position2LeftLengthInBit = section.GetLeftBitLength();

                if (result.Fine)
                {
                    Int64 itemBitLength = position1LeftLengthInBit - position2LeftLengthInBit;
                    string itemDescription = "language_code: " + Utility.GetLanguageCodeStr(languageCode) + ", component_description: " + textDescription;
                    Utility.UpdateNodeLength(itemNode, itemDescription, ItemType.ITEM, itemBitLength);
                }
                else
                {
                    Utility.UpdateNode(itemNode, "component_description_item(invalid)", ItemType.ERROR);
                    break;
                }

            }//while (descriptorLength > 0)

            return result;
        }//ParseMultilingualComponentDescriptor

        public static Result ParsePrivateDataSpecifierDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "private_data_specifier ", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);
            }

            return result;
        }//ParsePrivateDataSpecifierDescriptor

        public static Result ParseServiceMoveDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "new_original_network_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "new_transport_stream_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }
            
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "new_service_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }
            return result;
        }//ParseServiceMoveDescriptor

        public static Result ParseShortSmoothingBufferDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "sb_size", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "sb_leak_rate", ItemType.FIELD, section, ref bitOffset, 6, ref fieldValue, ref descriptorLength);
            }
            
            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "DVB_reserved", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }
            return result;
        }//ParseShortSmoothingBufferDescriptor


        public static Result ParseFrequencyListDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 6, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "coding_type", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            TreeNode loopNode = null;
            if (result.Fine)
            {
                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out loopNode, "frequency_loop", ItemType.LOOP, section, bitOffset, descriptorLength, descriptorLength);
            }

            if (result.Fine)
            {
                while (descriptorLength > 0)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, loopNode, out newNode, "centre_frequency", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);

                    if (!result.Fine)
                    {
                        break;
                    }

                }//while (descriptorLength > 0) 
            }

            return result;
        }//ParseFrequencyListDescriptor


        public static Result ParsePartialTransportStreamDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "programme_identification_label", ItemType.FIELD, section, ref bitOffset, 20, ref fieldValue, ref descriptorLength);
            }

            return result;
        }//ParsePartialTransportStreamDescriptor


        public static Result ParseDataBroadcastDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "data_broadcast_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "component_tag", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            Int64 selectorLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "selector_length", ItemType.FIELD, section, ref bitOffset, 8, ref selectorLength, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "selector_byte", ItemType.FIELD, section, ref bitOffset, selectorLength*8, ref descriptorLength);
            }

            if (result.Fine)
            {
                Int64 languageCode = 0;
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "ISO_639_language_code", ItemType.FIELD, section, ref bitOffset, 24, ref languageCode, ref descriptorLength);

                if (result.Fine)
                {
                    String languageCodeStr = "language_code: " + Utility.GetLanguageCodeStr(languageCode);
                    Utility.UpdateNode(newNode, languageCodeStr, ItemType.FIELD);
                }
            }

            Int64 textLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "text_length", ItemType.FIELD, section, ref bitOffset, 8, ref textLength, ref descriptorLength);
            }

            if (result.Fine)
            {
                textLength *= 8;//To bits.
                string textChar = null;

                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out textChar, section, bitOffset, textLength, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "textChar: " + textChar, ItemType.FIELD, section, ref bitOffset, textLength, ref descriptorLength);
                }
            }

            return result;
        }//ParseDataBroadcastDescriptor

        public static Result ParseScramblingDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "scrambling_mode", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            return result;
        }//ParseScramblingDescriptor

        public static Result ParseDataBroadcastIdDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "data_broadcast_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "id_selector_byte", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }//ParseDataBroadcastIdDescriptor

        public static Result ParseTransportStreamDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            //Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "byte", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }//ParseTransportStreamDescriptor

        public static Result ParseDSNGDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            //Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "byte", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }//ParseDSNGDescriptor

        public static Result ParsePDCDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "programme_identification_label", ItemType.FIELD, section, ref bitOffset, 20, ref fieldValue, ref descriptorLength);
            }

            return result;
        }//ParsePDCDescriptor

        public static Result ParseAC3Descriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            Int64 componentTypeFlag = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "component_type_flag", ItemType.FIELD, section, ref bitOffset, 1, ref componentTypeFlag, ref descriptorLength);
            }

            Int64 bsidFlag = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "bsid_flag", ItemType.FIELD, section, ref bitOffset, 1, ref bsidFlag, ref descriptorLength);
            }

            Int64 mainidFlag = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "mainidFlag", ItemType.FIELD, section, ref bitOffset, 1, ref mainidFlag, ref descriptorLength);
            }

            Int64 asvcFlag = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "asvc_flag", ItemType.FIELD, section, ref bitOffset, 1, ref asvcFlag, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved_flags", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                if (1 == componentTypeFlag)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (component_type_flag == 1)", ItemType.ITEM, section, bitOffset, 8, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "component_type", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                    }
 
                }
            }

            if (result.Fine)
            {
                if (1 == bsidFlag)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (bsidFlag == 1)", ItemType.ITEM, section, bitOffset, 8, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "bsidFlag", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                    }

                }
            }

            if (result.Fine)
            {
                if (1 == mainidFlag)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (mainid_flag == 1)", ItemType.ITEM, section, bitOffset, 8, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "mainid_flag", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                    }

                }
            }

            if (result.Fine)
            {
                if (1 == asvcFlag)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (asvc == 1)", ItemType.ITEM, section, bitOffset, 8, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "asvc", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                    }

                }
            }

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "additional_info_byte", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }//ParseAC3Descriptor

        public static Result ParseAncillaryDataDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "ancillary_data_identifier", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            return result;
        }//ParseAncillaryDataDescriptor

        public static Result ParseCellListDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            Int64 position1LeftLengthInBit = 0;
            Int64 position2LeftLengthInBit = 0;

            while (descriptorLength > 0)
            {
                TreeNode itemNode = null;

                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "cell_item", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                //Make a copy of current length;
                position1LeftLengthInBit = section.GetLeftBitLength();

                Int64 cellId = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "cell_id", ItemType.FIELD, section, ref bitOffset, 16, ref cellId, ref descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "cell_latitude", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "cell_longitude", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "cell_extent_of_latitude", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue, ref descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "cell_extent_of_longitude", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue, ref descriptorLength);
                }

                Int64 subcellInfoLoopLength = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "subcell_info_loop_length", ItemType.FIELD, section, ref bitOffset, 8, ref subcellInfoLoopLength, ref descriptorLength);
                }

                TreeNode subCellInfoLoopNode = null;
                if (result.Fine)
                {
                    result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out subCellInfoLoopNode, "subcell_info_loop", ItemType.FIELD, section, bitOffset, subcellInfoLoopLength * 8, descriptorLength);
                }

                if (result.Fine)
                {
                    while (subcellInfoLoopLength > 0)
                    {
                        TreeNode subcellItemNode = null;

                        result = Utility.AddNodeContainerPlus(Position.CHILD, subCellInfoLoopNode, out subcellItemNode, "subcell_info_item", ItemType.FIELD, section, bitOffset, 8 * 8, descriptorLength);

                        Int64 cellIdExtension = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, subcellItemNode, out newNode, "cell_id_extension", ItemType.FIELD, section, ref bitOffset, 8, ref cellIdExtension, ref descriptorLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, subcellItemNode, out newNode, "subcell_latitude", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, subcellItemNode, out newNode, "subcell_longitude", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, subcellItemNode, out newNode, "subcell_extent_of_latitude", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue, ref descriptorLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, subcellItemNode, out newNode, "subcell_extent_of_longitude", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue, ref descriptorLength);
                        }

                        if (result.Fine)
                        {
                            string itemDescription = "cell_id_extension: " + Utility.GetValueHexString(cellIdExtension, 8);
                            Utility.UpdateNode(subcellItemNode, itemDescription, ItemType.ITEM);

                            //Decrease the length.
                            subcellInfoLoopLength -= 8;
                        }
                        else
                        {
                            Utility.UpdateNode(subcellItemNode, "subcell_info_item(invalid)", ItemType.ERROR);
                            break;
                        }

                    }//while (subcellInfoLoopLength > 0)

                }

                //Make a second copy of current length;
                position2LeftLengthInBit = section.GetLeftBitLength();

                if (result.Fine)
                {
                    Int64 itemBitLength = position1LeftLengthInBit - position2LeftLengthInBit;
                    string itemDescription = "cell_id: " + Utility.GetValueHexString(cellId, 16);
                    Utility.UpdateNodeLength(itemNode, itemDescription, ItemType.ITEM, itemBitLength);
                }
                else
                {
                    Utility.UpdateNode(itemNode, "cell_item(invalid)", ItemType.ERROR);
                    break;
                }

            }//while (descriptorLength > 0)

            return result;
        }//ParseCellListDescriptor

        public static Result ParseCellFrequencyLinkDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            //Int64 fieldValue = 0;

            Int64 position1LeftLengthInBit = 0;
            Int64 position2LeftLengthInBit = 0;

            while (descriptorLength > 0)
            {
                TreeNode itemNode = null;

                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "cell_frequency_item", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                //Make a copy of current length;
                position1LeftLengthInBit = section.GetLeftBitLength();

                Int64 cellId = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "cell_id", ItemType.FIELD, section, ref bitOffset, 16, ref cellId, ref descriptorLength);
                }

                Int64 frequency = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "frequency", ItemType.FIELD, section, ref bitOffset, 32, ref frequency, ref descriptorLength);
                }


                Int64 subcellInfoLoopLength = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "subcell_info_loop_length", ItemType.FIELD, section, ref bitOffset, 8, ref subcellInfoLoopLength, ref descriptorLength);
                }

                TreeNode subCellInfoLoopNode = null;
                if (result.Fine)
                {
                    result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out subCellInfoLoopNode, "subcell_info_loop", ItemType.FIELD, section, bitOffset, subcellInfoLoopLength * 8, descriptorLength);
                }

                if (result.Fine)
                {
                    while (subcellInfoLoopLength > 0)
                    {
                        TreeNode subcellItemNode = null;

                        result = Utility.AddNodeContainerPlus(Position.CHILD, subCellInfoLoopNode, out subcellItemNode, "subcell_info_item", ItemType.FIELD, section, bitOffset, 5 * 8, descriptorLength);

                        Int64 cellIdExtension = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, subcellItemNode, out newNode, "cell_id_extension", ItemType.FIELD, section, ref bitOffset, 8, ref cellIdExtension, ref descriptorLength);
                        }

                        Int64 transposerFrequency = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, subcellItemNode, out newNode, "transposer_frequency", ItemType.FIELD, section, ref bitOffset, 32, ref transposerFrequency, ref descriptorLength);
                        }

                        if (result.Fine)
                        {
                            string itemDescription = "cell_id_extension: " + Utility.GetValueHexString(cellIdExtension, 8) + ", transposer_frequency: " + Utility.GetValueHexString(transposerFrequency, 32);
                            Utility.UpdateNode(subcellItemNode, itemDescription, ItemType.ITEM);

                            //Decrease the length.
                            subcellInfoLoopLength -= 5;
                        }
                        else
                        {
                            Utility.UpdateNode(subcellItemNode, "subcell_info_item(invalid)", ItemType.ERROR);
                            break;
                        }

                    }//while (subcellInfoLoopLength > 0)

                }

                //Make a second copy of current length;
                position2LeftLengthInBit = section.GetLeftBitLength();

                if (result.Fine)
                {
                    Int64 itemBitLength = position1LeftLengthInBit - position2LeftLengthInBit;
                    string itemDescription = "cell_id: " + Utility.GetValueHexString(cellId, 16) + ", frequency: " + Utility.GetValueHexString(frequency, 32);
                    Utility.UpdateNodeLength(itemNode, itemDescription, ItemType.ITEM, itemBitLength);
                }
                else
                {
                    Utility.UpdateNode(itemNode, "cell_frequency_item(invalid)", ItemType.ERROR);
                    break;
                }

            }//while (descriptorLength > 0)

            return result;
        }//ParseCellFrequencyLinkDescriptor

        public static Result ParseAnnouncementSupportDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "announcement_support_indicator", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                TreeNode loopNode = null;
                //Add a container item.
                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out loopNode, "announcement_support_loop", ItemType.LOOP, section, bitOffset, descriptorLength, descriptorLength);

                if (result.Fine)
                {
                    Int64 position1LeftLengthInBit = 0;
                    Int64 position2LeftLengthInBit = 0;
                    while (descriptorLength > 0)
                    {
                        TreeNode itemNode = null;

                        result = Utility.AddNodeContainerPlus(Position.CHILD, loopNode, out itemNode, "announcement_support", ItemType.LOOP, section, bitOffset, 0, descriptorLength);

                        //Make a copy of current length;
                        position1LeftLengthInBit = section.GetLeftBitLength();

                        Int64 announcementType = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "announcement_type", ItemType.FIELD, section, ref bitOffset, 4, ref announcementType, ref descriptorLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
                        }

                        Int64 referenceType = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "reference_type", ItemType.FIELD, section, ref bitOffset, 3, ref referenceType, ref descriptorLength);
                        }

                        if (result.Fine)
                        {
                            if ((0x01 == announcementType) || (0x02 == announcementType) || (0x03 == announcementType))
                            {
                                TreeNode ifNode = null;
                                result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out ifNode, "If (reference_type in [0x01, 0x02, 0x03] )", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "original_network_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "transport_stream_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "service_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "component_tag", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                                }
                            }//if ((0x01 == cridType) || (0x02 == cridType) || (0x03 == cridType))
                        }

                        //Make the second copy of current length;
                        position2LeftLengthInBit = section.GetLeftBitLength();
                        if (result.Fine)
                        {
                            Int64 itemBitLength = (position1LeftLengthInBit - position2LeftLengthInBit);

                            string itemDescription = "announcement_type: " + Utility.GetValueHexString(announcementType, 8)
                                                        + ", reference_type: " + Utility.GetValueHexString(referenceType, 8);
                            Utility.UpdateNodeLength(itemNode, itemDescription, ItemType.ITEM, itemBitLength);
                        }
                        else
                        {
                            Utility.UpdateNode(itemNode, "announcement_support(invalid)", ItemType.ERROR);
                            break;//Break once something unexpected.
                        }
                    }//while (descriptorLength > 0)

                }//To parse items.
            }

            return result;
        }//ParseAnnouncementSupportDescriptor

        public static Result ParseApplicationSignallingDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            Int64 position1LeftLengthInBit = 0;
            Int64 position2LeftLengthInBit = 0;

            while (descriptorLength > 0)
            {
                TreeNode itemNode = null;

                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "applicaion", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                //Make a copy of current length;
                position1LeftLengthInBit = section.GetLeftBitLength();

                Int64 applicationType = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "application_type", ItemType.FIELD, section, ref bitOffset, 16, ref applicationType, ref descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
                }

                Int64 aitVersionNumber = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "AIT_version_number", ItemType.FIELD, section, ref bitOffset, 5, ref aitVersionNumber, ref descriptorLength);
                }

                //Make a second copy of current length;
                position2LeftLengthInBit = section.GetLeftBitLength();

                if (result.Fine)
                {
                    Int64 itemBitLength = position1LeftLengthInBit - position2LeftLengthInBit;
                    string itemDescription = "application_type: " + Utility.GetValueHexString(applicationType, 16) + ", AIT_version_number: " + Utility.GetValueHexString(aitVersionNumber, 8);
                    Utility.UpdateNodeLength(itemNode, itemDescription, ItemType.ITEM, itemBitLength);
                }
                else
                {
                    Utility.UpdateNode(itemNode, "application(invalid)", ItemType.ERROR);
                    break;
                }

            }//while (descriptorLength > 0)

            return result;
        }//ParseApplicationSignallingDescriptor

        public static Result ParseAdaptationFieldDataDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "adaptation_field_data_identifier", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            return result;
        }//ParseAdaptationFieldDataDescriptor
        
        public static Result ParseServiceIdentifierDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            
            if (result.Fine)
            {
                string text = null;
                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out text, section, bitOffset, descriptorLength, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "textual_service_identifier_bytes: " + text, ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
                }
            }

            return result;
        }//ParseServiceIdentifierDescriptor

        public static Result ParseServiceAvailbilityDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "availability_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 7, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                TreeNode loopNode = null;
                //Add a container item.
                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out loopNode, "cell_id_loop", ItemType.LOOP, section, bitOffset, descriptorLength, descriptorLength);

                if (result.Fine)
                {
                    while (descriptorLength > 0)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, loopNode, out newNode, "cell_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);

                        if (!result.Fine)
                        {
                            break;
                        }
                    }//while (descriptorLength > 0)

                }
            }

            return result;
        }//ParseServiceAvailbilityDescriptor


        public static Result ParseDefaultAuthorityDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            
            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "default authority byte", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }//ParseDefaultAuthorityDescriptor

        public static Result ParseTvaIdDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            Int64 position1LeftLengthInBit = 0;
            Int64 position2LeftLengthInBit = 0;

            while (descriptorLength > 0)
            {
                TreeNode itemNode = null;

                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "TVA", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                //Make a copy of current length;
                position1LeftLengthInBit = section.GetLeftBitLength();

                Int64 tvaId = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "TVA id", ItemType.FIELD, section, ref bitOffset, 16, ref tvaId, ref descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "Reserved", ItemType.FIELD, section, ref bitOffset, 5, ref fieldValue, ref descriptorLength);
                }

                Int64 runningStatus = 0;
                if (result.Fine)
                {
                    result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "running status", ItemType.FIELD, section, ref bitOffset, 3, ref runningStatus, ref descriptorLength);
                }

                //Make a second copy of current length;
                position2LeftLengthInBit = section.GetLeftBitLength();

                if (result.Fine)
                {
                    Int64 itemBitLength = position1LeftLengthInBit - position2LeftLengthInBit;
                    string itemDescription = "TVA id: " + Utility.GetValueHexString(tvaId, 16) + ", running status: " + Utility.GetValueHexString(runningStatus, 8);
                    Utility.UpdateNodeLength(itemNode, itemDescription, ItemType.ITEM, itemBitLength);
                }
                else
                {
                    Utility.UpdateNode(itemNode, "TVA(invalid)", ItemType.ERROR);
                    break;
                }

            }//while (descriptorLength > 0)

            return result;
        }//ParseTvaIdDescriptor


        public static Result ParseContentIdentifierDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            if (result.Fine)
            {
                Int64 position1LeftLengthInBit = 0;
                Int64 position2LeftLengthInBit = 0;
                while (descriptorLength > 0)
                {
                    TreeNode itemNode = null;

                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "crid_info", ItemType.LOOP, section, bitOffset, 0, descriptorLength);

                    //Make a copy of current length;
                    position1LeftLengthInBit = section.GetLeftBitLength();

                    Int64 cridType = 0;
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "crid type", ItemType.FIELD, section, ref bitOffset, 6, ref cridType, ref descriptorLength);
                    }

                    Int64 cridLocation = 0;
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "crid location", ItemType.FIELD, section, ref bitOffset, 2, ref cridLocation, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        if (0x00 == cridLocation)
                        {
                            TreeNode ifNode = null;
                            result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out ifNode, "if (crid location == '00' )", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                            Int64 cridLength = 0;
                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "crid length", ItemType.FIELD, section, ref bitOffset, 8, ref cridLength, ref descriptorLength);
                            }

                            if (result.Fine)
                            {
                                result = Utility.AddNodeDataPlus(Position.CHILD, ifNode, out newNode, "crid byte", ItemType.FIELD, section, ref bitOffset, cridLength*8, ref descriptorLength);
                            }
                        }//if (0x00 == cridLocation)
                    }

                    if (result.Fine)
                    {
                        if (0x01 == cridLocation)
                        {
                            TreeNode ifNode = null;
                            result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out ifNode, "if (crid location == '01' )", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "crid ref", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                            }

                        }//if (0x01 == cridLocation)
                    }

                    //Make the second copy of current length;
                    position2LeftLengthInBit = section.GetLeftBitLength();
                    if (result.Fine)
                    {
                        Int64 itemBitLength = (position1LeftLengthInBit - position2LeftLengthInBit);

                        string itemDescription = "crid type: " + Utility.GetValueHexString(cridType, 8)
                                                    + ", crid location: " + Utility.GetValueHexString(cridLocation, 8);
                        Utility.UpdateNodeLength(itemNode, itemDescription, ItemType.ITEM, itemBitLength);
                    }
                    else
                    {
                        Utility.UpdateNode(itemNode, "crid_info(invalid)", ItemType.ERROR);
                        break;//Break once something unexpected.
                    }
                }//while (descriptorLength > 0)

            }//To parse items.

            return result;
        }//ParseContentIdentifierDescriptor

        public static Result ParseTimeSliceFecIdentifierDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "time_slicing", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "mpe_fec", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved_for_future_use", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "frame_size", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "max_burst_duration", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "max_average_rate", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "time_slice_fec_id", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "id_selector_byte", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }//ParseTimeSliceFecIdentifierDescriptor

        public static Result ParseEcmRepetitionRateDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "CA_system_ID", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "ECM repetition rate", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "private_data_byte", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }//ParseEcmRepetitionRateDescriptor

        public static Result ParseS2SatelliteDeliverySystemDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            Int64 scramblingSequenceSelector = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "scrambling_sequence_selector", ItemType.FIELD, section, ref bitOffset, 1, ref scramblingSequenceSelector, ref descriptorLength);
            }

            Int64 multipleInputStreamFlag = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "multiple_input_stream_flag", ItemType.FIELD, section, ref bitOffset, 1, ref multipleInputStreamFlag, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "backwards_compatibility_indicator", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 5, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                if (0x01 == scramblingSequenceSelector)
                {
                    TreeNode ifNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out ifNode, "if (scrambling_sequence_selector == 1)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "Reserved", ItemType.FIELD, section, ref bitOffset, 6, ref fieldValue, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "scrambling_sequence_index", ItemType.FIELD, section, ref bitOffset, 18, ref fieldValue, ref descriptorLength);
                    }

                }//if (0x01 == scramblingSequenceSelector)
            }

            if (result.Fine)
            {
                if (0x01 == multipleInputStreamFlag)
                {
                    TreeNode ifNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out ifNode, "if (multiple_input_stream_flag == 1)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "input_stream_identifier", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                    }

                }//if (0x01 == multipleInputStreamFlag)
            }

            return result;
        }//ParseS2SatelliteDeliverySystemDescriptor

        public static Result ParseEnhancedAc3Descriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            Int64 componentTypeFlag = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "component_type_flag", ItemType.FIELD, section, ref bitOffset, 1, ref componentTypeFlag, ref descriptorLength);
            }

            Int64 bsidFlag = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "bsid_flag", ItemType.FIELD, section, ref bitOffset, 1, ref bsidFlag, ref descriptorLength);
            }

            Int64 mainidFlag = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "mainidFlag", ItemType.FIELD, section, ref bitOffset, 1, ref mainidFlag, ref descriptorLength);
            }

            Int64 asvcFlag = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "asvc_flag", ItemType.FIELD, section, ref bitOffset, 1, ref asvcFlag, ref descriptorLength);
            }

            Int64 mixinfoExists = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "mixinfoexists", ItemType.FIELD, section, ref bitOffset, 1, ref mixinfoExists, ref descriptorLength);
            }

            Int64 substream1Flag = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "substream1_flag", ItemType.FIELD, section, ref bitOffset, 1, ref substream1Flag, ref descriptorLength);
            }


            Int64 substream2Flag = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "substream2_flag", ItemType.FIELD, section, ref bitOffset, 1, ref substream2Flag, ref descriptorLength);
            }

            Int64 substream3Flag = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "substream3_flag", ItemType.FIELD, section, ref bitOffset, 1, ref substream3Flag, ref descriptorLength);
            }

            if (result.Fine)
            {
                if (1 == componentTypeFlag)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (component_type_flag == 1)", ItemType.ITEM, section, bitOffset, 8, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "component_type", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                    }

                }
            }

            if (result.Fine)
            {
                if (1 == bsidFlag)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (bsidFlag == 1)", ItemType.ITEM, section, bitOffset, 8, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "bsidFlag", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                    }

                }
            }

            if (result.Fine)
            {
                if (1 == mainidFlag)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (mainid_flag == 1)", ItemType.ITEM, section, bitOffset, 8, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "mainid_flag", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                    }

                }
            }

            if (result.Fine)
            {
                if (1 == asvcFlag)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (asvc == 1)", ItemType.ITEM, section, bitOffset, 8, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "asvc", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                    }

                }
            }

            if (result.Fine)
            {
                if (1 == substream1Flag)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (substream1_flag == 1)", ItemType.ITEM, section, bitOffset, 8, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "substream1", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                    }

                }
            }

            if (result.Fine)
            {
                if (1 == substream2Flag)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (substream2_flag == 1)", ItemType.ITEM, section, bitOffset, 8, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "substream2", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                    }

                }
            }

            if (result.Fine)
            {
                if (1 == substream3Flag)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out itemNode, "if (substream3_flag == 1)", ItemType.ITEM, section, bitOffset, 8, descriptorLength);
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "substream3", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                    }

                }
            }

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "additional_info_byte", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }//ParseEnhancedAc3Descriptor

        public static Result ParseDtsDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "sample_rate_code", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "bit_rate_code", ItemType.FIELD, section, ref bitOffset, 6, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "nblks", ItemType.FIELD, section, ref bitOffset, 7, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "fsize", ItemType.FIELD, section, ref bitOffset, 14, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "surround_mode", ItemType.FIELD, section, ref bitOffset, 6, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "lfe_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "extended_surround_flag", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }


            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "additional_info_byte", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }//ParseDtsDescriptor

        public static Result ParseAacDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "profile_and_level", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                if (descriptorLength > 0)
                {
                    TreeNode ifNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out ifNode, "if (descriptor_length > 1)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                    Int64 aacTypeFlag = 0;
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "AAC_type_flag", ItemType.FIELD, section, ref bitOffset, 1, ref aacTypeFlag, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 7, ref fieldValue, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        if (aacTypeFlag == 1)
                        {
                            TreeNode ifNode2 = null;
                            result = Utility.AddNodeContainerPlus(Position.CHILD, ifNode, out ifNode2, "if (AAC_type_flag == 1)", ItemType.FIELD, section, bitOffset, descriptorLength, descriptorLength);

                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "AAC_type", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                            }

                            if (result.Fine)
                            {
                                result = Utility.AddNodeDataPlus(Position.CHILD, ifNode2, out newNode, "additional_info_byte", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
                            }
                        }
                    }

                }//if (descriptorLength > 0)
            }

            return result;
        }//ParseAacDescriptor

        public static Result ParseFtaContentManagementDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "do_not_scramble", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "control_remote_access_over_internet", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "do_not_apply_revocation", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            return result;
        }//ParseFtaContentManagementDescriptor

        public static Result ParseExtensionDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 descriptorTagExtension = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "descriptor_tag_extension", ItemType.FIELD, section, ref bitOffset, 8, ref descriptorTagExtension, ref descriptorLength);

                if (result.Fine)
                {
                    Utility.UpdateNode(parentNode.Parent, GetExtendedDescriptorName((byte)descriptorTagExtension, Scope.SI), ItemType.ITEM);
                }
            }

            switch (descriptorTagExtension)
            {
                case 0x00:
                    result = ParseImageIconDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                    break;
                //case 0x01:
                //    result = ParseCpcmDeliverySignallingDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                //    break;
                case 0x02:
                    result = ParseCpDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                    break;
                //case 0x03:
                //    result = ParseCpIdentifierDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                //    break;
                case 0x04:
                    result = ParseT2DeliverySystemDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                    break;
                case 0x05:
                    result = ParseShDeliverySystemDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                    break;
                case 0x06:
                    result = ParseSupplementaryAudioDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                    break;
                case 0x07:
                    result = ParseNetworkChangeNotifyDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                    break;
                case 0x08:
                    result = ParseMessageDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                    break;
                case 0x09:
                    result = ParseTargetRegionDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                    break;
                case 0x0A:
                    result = ParseTargetRegionNameDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                    break;
                case 0x0B:
                    result = ParseServiceRelocatedDescriptor(domain, parentNode, section, tag, ref bitOffset, ref descriptorLength);
                    break;
                default:
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "descriptor_extension", ItemType.FIELD, section, ref bitOffset, 8, ref descriptorTagExtension, ref descriptorLength);
                    }

                    break;
            }

            return result;
        }//ParseExtensionDescriptor


        public static Result ParseImageIconDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            Int64 descriptorNumber = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "descriptor_number", ItemType.FIELD, section, ref bitOffset, 4, ref descriptorNumber, ref descriptorLength);
            }

            Int64 lastDescriptorNumber = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "last_descriptor_number", ItemType.FIELD, section, ref bitOffset, 4, ref lastDescriptorNumber, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 5, ref fieldValue, ref descriptorLength);
            }

            Int64 iconId = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "icon_id", ItemType.FIELD, section, ref bitOffset, 3, ref iconId, ref descriptorLength);
            }

            if (result.Fine)
            {
                if (0x00 == descriptorNumber)
                {
                    TreeNode ifNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out ifNode, "if (descriptor_number == 0x00)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                    Int64 iconTransportMode = 0;
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "icon_transport_mode", ItemType.FIELD, section, ref bitOffset, 2, ref iconTransportMode, ref descriptorLength);
                    }

                    Int64 positionFlag = 0;
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "position_flag", ItemType.FIELD, section, ref bitOffset, 1, ref positionFlag, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        if (0x01 == positionFlag)
                        {
                            TreeNode ifNode2 = null;
                            result = Utility.AddNodeContainerPlus(Position.CHILD, ifNode, out ifNode2, "if (position_flag == 0x01)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "coordinate_system", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
                            }

                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
                            }

                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "icon_horizontal_origin", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue, ref descriptorLength);
                            }

                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "icon_vertical_origin", ItemType.FIELD, section, ref bitOffset, 12, ref fieldValue, ref descriptorLength);
                            }

                        }//if (0x01 == positionFlag)
                        else
                        {
                            TreeNode ifNode2 = null;
                            result = Utility.AddNodeContainerPlus(Position.CHILD, ifNode, out ifNode2, "if (position_flag != 0x01)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 5, ref fieldValue, ref descriptorLength);
                            }
                        }//if (0x01 != positionFlag)
                    }//positionFlag

                    Int64 iconTypeLength = 0;
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "icon_type_length", ItemType.FIELD, section, ref bitOffset, 8, ref iconTypeLength, ref descriptorLength);
                    }
                    if (result.Fine)
                    {
                        string iconTypeChar = null;
                        if (result.Fine)
                        {
                            result = Utility.GetTextPlus(out iconTypeChar, section, bitOffset, iconTypeLength * 8, descriptorLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeDataPlus(Position.CHILD, ifNode, out newNode, "icon_type_char: " + iconTypeChar, ItemType.FIELD, section, ref bitOffset, iconTypeLength * 8, ref descriptorLength);
                        }
                    }

                    if (result.Fine)
                    {
                        if (0x00 == iconTransportMode)
                        {
                            TreeNode ifNode2 = null;
                            result = Utility.AddNodeContainerPlus(Position.CHILD, ifNode, out ifNode2, "if (icon_transport_mode == 0x00 )", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                            Int64 iconDataLength = 0;
                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "icon_data_length", ItemType.FIELD, section, ref bitOffset, 8, ref iconDataLength, ref descriptorLength);
                            }

                            if (result.Fine)
                            {
                                result = Utility.AddNodeDataPlus(Position.CHILD, ifNode2, out newNode, "icon_data_byte", ItemType.FIELD, section, ref bitOffset, iconDataLength*8, ref descriptorLength);
                            }

                        }//if (0x00 == iconTransportMode)
                        else if (0x01 == iconTransportMode)
                        {
                            TreeNode ifNode2 = null;
                            result = Utility.AddNodeContainerPlus(Position.CHILD, ifNode, out ifNode2, "if (icon_transport_mode == 0x01 )", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                            Int64 urlLength = 0;
                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "url_length", ItemType.FIELD, section, ref bitOffset, 8, ref urlLength, ref descriptorLength);
                            }
                            if (result.Fine)
                            {
                                string urlChar = null;
                                if (result.Fine)
                                {
                                    result = Utility.GetTextPlus(out urlChar, section, bitOffset, urlLength * 8, descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeDataPlus(Position.CHILD, ifNode2, out newNode, "url_char: " + urlChar, ItemType.FIELD, section, ref bitOffset, iconTypeLength * 8, ref descriptorLength);
                                }
                            }
                        }//if (0x01 == iconTransportMode)
                    }//iconTransportMode

                }//if (0x00 == descriptorNumber)
                else
                {
                    TreeNode ifNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out ifNode, "if (0x00 != descriptorNumber)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                    Int64 iconDataLength = 0;
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "icon_data_length", ItemType.FIELD, section, ref bitOffset, 8, ref iconDataLength, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeDataPlus(Position.CHILD, ifNode, out newNode, "icon_data_byte", ItemType.FIELD, section, ref bitOffset, iconDataLength * 8, ref descriptorLength);
                    }
                }//if (0x00 != descriptorNumber)
            }

            return result;
        }//ParseImageIconDescriptor

        public static Result ParseCpDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "CA_system_ID", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "CP_PID", ItemType.FIELD, section, ref bitOffset, 13, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "private_data_byte", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }//ParseCpDescriptor


        public static Result ParseT2DeliverySystemDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "plp_id", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "T2_system_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                if (0 < descriptorLength)
                {
                    TreeNode ifNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out ifNode, "if (descriptor_length > 4)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "SISO/MISO", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "bandwidth", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "reserved_future_use", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "guard_interval", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "transmission_mode", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "other_frequency_flag", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
                    }

                    Int64 tfsFlag = 0;
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "tfs_flag", ItemType.FIELD, section, ref bitOffset, 1, ref tfsFlag, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        Int64 bitOffsetPosition1 = 0;
                        Int64 bitOffsetPosition2 = 0;
                        while (descriptorLength > 0)
                        {
                            TreeNode itemNode = null;
                            result = Utility.AddNodeContainerPlus(Position.CHILD, ifNode, out itemNode, "cell_item", ItemType.ITEM, section, bitOffset, 0, descriptorLength);

                            bitOffsetPosition1 = section.GetLeftBitLength();

                            Int64 cellId = 0;
                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "cell_id", ItemType.FIELD, section, ref bitOffset, 16, ref cellId, ref descriptorLength);
                            }

                            if (result.Fine)
                            {
                                if (1 == tfsFlag)
                                {
                                    TreeNode ifNode2 = null;
                                    result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out ifNode2, "if (tfs_flag == 1)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                                    Int64 frequencyLoopLength = 0;
                                    if (result.Fine)
                                    {
                                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "frequency_loop_length", ItemType.FIELD, section, ref bitOffset, 8, ref frequencyLoopLength, ref descriptorLength);
                                    }

                                    TreeNode frequencyLoopNode = null;
                                    if (result.Fine)
                                    {
                                        result = Utility.AddNodeContainerPlus(Position.CHILD, ifNode2, out frequencyLoopNode, "frequency_loop", ItemType.LOOP, section, bitOffset, frequencyLoopLength * 8, descriptorLength); 
                                    }

                                    if (result.Fine)
                                    {
                                        while (frequencyLoopLength > 0)
                                        {
                                            result = Utility.AddNodeFieldPlus(Position.CHILD, frequencyLoopNode, out newNode, "centre_frequency", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);

                                            if (!result.Fine)
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                frequencyLoopLength -= 4;
                                            }

                                        }//while (frequencyLoopLength > 0)
                                    }

                                }//if (1 == tfsFlag)
                                else
                                {
                                    TreeNode ifNode2 = null;
                                    result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out ifNode2, "if (tfs_flag != 1)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                                    if (result.Fine)
                                    {
                                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "centre_frequency", ItemType.FIELD, section, ref bitOffset, 32, ref fieldValue, ref descriptorLength);
                                    }
                                }//if (1 != tfsFlag)

                            }

                            Int64 subcellInfoLoopLength = 0;
                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "subcell_info_loop_length", ItemType.FIELD, section, ref bitOffset, 8, ref subcellInfoLoopLength, ref descriptorLength);
                            }

                            TreeNode subcellInfoLoopNode = null;
                            if (result.Fine)
                            {
                                result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out subcellInfoLoopNode, "subcell_info_loop", ItemType.LOOP, section, bitOffset, subcellInfoLoopLength * 8, descriptorLength);
                            }

                            if (result.Fine)
                            {
                                while (subcellInfoLoopLength > 0)
                                {
                                    TreeNode subcellInfoItem = null;
                                    result = Utility.AddNodeContainerPlus(Position.CHILD, subcellInfoLoopNode, out subcellInfoItem, "subcell_info", ItemType.ITEM, section, bitOffset, 40, descriptorLength);

                                    Int64 cellIdExtension = 0;
                                    if (result.Fine)
                                    {
                                        result = Utility.AddNodeFieldPlus(Position.CHILD, subcellInfoItem, out newNode, "cell_id_extension", ItemType.FIELD, section, ref bitOffset, 8, ref cellIdExtension, ref descriptorLength);
                                    }

                                    Int64 centreFrequency = 0;
                                    if (result.Fine)
                                    {
                                        result = Utility.AddNodeFieldPlus(Position.CHILD, subcellInfoItem, out newNode, "centre_frequency", ItemType.FIELD, section, ref bitOffset, 32, ref centreFrequency, ref descriptorLength);
                                    }

                                    if (result.Fine)
                                    {
                                        String itemDescription = "cell_id_extension: " + Utility.GetValueHexString(cellIdExtension, 8) + ", centre_frequency: " + Utility.GetValueHexString(centreFrequency, 32);
                                        subcellInfoLoopLength -= 5;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }//while (subcellInfoLoopLength > 0)
                            }

                            bitOffsetPosition2 = section.GetLeftBitLength();

                            if (result.Fine)
                            {
                                Int64 itemBitLength = bitOffsetPosition1 - bitOffsetPosition2;
                                String itemDescription = "cell_id: " + Utility.GetValueHexString(cellId, 16);
                                Utility.UpdateNodeLength(itemNode, itemDescription, ItemType.ITEM, itemBitLength);
                            }
                            else
                            {
                                Utility.UpdateNode(itemNode, "cell(invalid)", ItemType.ERROR);
                                break;
                            }
                        }
                    }
                }//if (0 < descriptorLength )
            }

            return result;
        }//ParseT2DeliverySystemDescriptor

        public static Result ParseShDeliverySystemDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "diversity_mode", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                TreeNode loopNode = null;
                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out loopNode, "modulation_loop", ItemType.LOOP, section, bitOffset, descriptorLength, descriptorLength);

                if (result.Fine)
                {
                    Int64 bitOffsetPosition1 = 0;
                    Int64 bitOffsetPosition2 = 0;

                    while (descriptorLength > 0)
                    {
                        TreeNode itemNode = null;
                        result = Utility.AddNodeContainerPlus(Position.CHILD, loopNode, out itemNode, "modulation_item", ItemType.ITEM, section, bitOffset, 0, descriptorLength);

                        bitOffsetPosition1 = section.GetLeftBitLength();

                        Int64 modulationType = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "modulation_type", ItemType.FIELD, section, ref bitOffset, 1, ref modulationType, ref descriptorLength);
                        }

                        Int64 interleaverPresence = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "interleaver_presence", ItemType.FIELD, section, ref bitOffset, 1, ref interleaverPresence, ref descriptorLength);
                        }

                        Int64 interleaverType = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "interleaver_type", ItemType.FIELD, section, ref bitOffset, 1, ref interleaverType, ref descriptorLength);
                        }

                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "Reserved", ItemType.FIELD, section, ref bitOffset, 5, ref interleaverType, ref descriptorLength);
                        }

                        if (result.Fine)
                        {
                            if (0 == modulationType)
                            {
                                TreeNode ifNode2 = null;
                                result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out ifNode2, "if (modulation_type == 0)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "Polarization", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "roll_off", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "modulation_mode", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "code_rate", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "symbol_rate", ItemType.FIELD, section, ref bitOffset, 5, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "Reserved", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
                                }

                            }//if (0 == cellId)
                            else
                            {
                                TreeNode ifNode2 = null;
                                result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out ifNode2, "if (modulation_type != 0)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "bandwidth", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "priority", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "constellation_and_hierarchy", ItemType.FIELD, section, ref bitOffset, 3, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "code_rate", ItemType.FIELD, section, ref bitOffset, 4, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "guard_interval", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "transmission_mode", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
                                }

                                if (result.Fine)
                                {
                                    result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "common_frequency", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
                                }
                            }//if (0 != cellId)
                        }

                        if (result.Fine)
                        {
                            if (1 == interleaverPresence)
                            {
                                TreeNode ifNode2 = null;
                                result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out ifNode2, "if (interleaver_presence == 1)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                                if (result.Fine)
                                {
                                    if (0 == interleaverType)
                                    {
                                        TreeNode ifNode3 = null;
                                        result = Utility.AddNodeContainerPlus(Position.CHILD, ifNode2, out ifNode3, "if (interleaver_type == 0)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                                        if (result.Fine)
                                        {
                                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode3, out newNode, "common_multiplier", ItemType.FIELD, section, ref bitOffset, 6, ref fieldValue, ref descriptorLength);
                                        }

                                        if (result.Fine)
                                        {
                                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode3, out newNode, "nof_late_taps", ItemType.FIELD, section, ref bitOffset, 6, ref fieldValue, ref descriptorLength);
                                        }

                                        if (result.Fine)
                                        {
                                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode3, out newNode, "nof_slices", ItemType.FIELD, section, ref bitOffset, 6, ref fieldValue, ref descriptorLength);
                                        }

                                        if (result.Fine)
                                        {
                                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode3, out newNode, "slice_distance", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
                                        }

                                        if (result.Fine)
                                        {
                                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode3, out newNode, "non_late_increments", ItemType.FIELD, section, ref bitOffset, 6, ref fieldValue, ref descriptorLength);
                                        }
                                    }//if (0 == interleaverType)
                                    else
                                    {
                                        TreeNode ifNode3 = null;
                                        result = Utility.AddNodeContainerPlus(Position.CHILD, ifNode2, out ifNode3, "if (interleaver_type != 0)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                                        if (result.Fine)
                                        {
                                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode3, out newNode, "common_multiplier", ItemType.FIELD, section, ref bitOffset, 6, ref fieldValue, ref descriptorLength);
                                        }

                                        if (result.Fine)
                                        {
                                            result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode3, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 2, ref fieldValue, ref descriptorLength);
                                        }
                                    }//if (0 != interleaverType)
                                }

                            }//if (1 == interleaverPresence)
                        }

                        bitOffsetPosition2 = section.GetLeftBitLength();

                        if (result.Fine)
                        {
                            Int64 itemBitLength = bitOffsetPosition1 - bitOffsetPosition2;
                            String itemDescription = "modulation_type: " + Utility.GetValueHexString(modulationType, 8)
                                                    + ", interleaver_presence: " + Utility.GetValueHexString(interleaverPresence, 8)
                                                    + ", interleaver_type: " + Utility.GetValueHexString(interleaverType, 8);
                            Utility.UpdateNodeLength(itemNode, itemDescription, ItemType.ITEM, itemBitLength);
                        }
                        else
                        {
                            Utility.UpdateNode(itemNode, "modulation_item(invalid)", ItemType.ERROR);
                            break;
                        }
                    }//while (descriptorLength > 0)
                }
            }
            return result;
        }//ParseShDeliverySystemDescriptor

        public static Result ParseSupplementaryAudioDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "mix_type", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "editorial_classification", ItemType.FIELD, section, ref bitOffset, 5, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 1, ref fieldValue, ref descriptorLength);
            }

            Int64 languageCodePresent = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "language_code_present", ItemType.FIELD, section, ref bitOffset, 1, ref languageCodePresent, ref descriptorLength);
            }

            if (result.Fine)
            {
                if (1 == languageCodePresent)
                {
                    TreeNode ifNode = null;

                    result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out ifNode, "if (language_code_present == 1)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                    if (result.Fine)
                    {
                        Int64 languageCode = 0;
                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "ISO_639_language_code", ItemType.FIELD, section, ref bitOffset, 24, ref languageCode, ref descriptorLength);

                        if (result.Fine)
                        {
                            String languageCodeStr = "language_code: " + Utility.GetLanguageCodeStr(languageCode);
                            Utility.UpdateNode(newNode, languageCodeStr, ItemType.FIELD);
                        }
                    }
                }//if (1 == languageCodePresent)
            }

            if (result.Fine)
            {
                result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "private_data_byte", ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
            }

            return result;
        }//ParseSupplementaryAudioDescriptor

        public static Result ParseNetworkChangeNotifyDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            TreeNode loopNode = null;
            result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out loopNode, "cell_loop", ItemType.LOOP, section, bitOffset, descriptorLength, descriptorLength);

            if (result.Fine)
            {
                Int64 bitOffsetPosition1 = 0;
                Int64 bitOffsetPosition2 = 0;

                while (descriptorLength > 0)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, loopNode, out itemNode, "cell_item", ItemType.ITEM, section, bitOffset, 0, descriptorLength);

                    bitOffsetPosition1 = section.GetLeftBitLength();

                    Int64 cellId = 0;
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "cell_id", ItemType.FIELD, section, ref bitOffset, 16, ref cellId, ref descriptorLength);
                    }

                    Int64 loopLength = 0;
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "loop_length", ItemType.FIELD, section, ref bitOffset, 8, ref loopLength, ref descriptorLength);
                    }

                    TreeNode innerLoopNode = null;
                    if (result.Fine)
                    {
                        result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out innerLoopNode, "change_loop", ItemType.LOOP, section, bitOffset, loopLength * 8, descriptorLength);
                    }

                    if (result.Fine)
                    {
                        Int64 bitOffsetPosition3 = 0;
                        Int64 bitOffsetPosition4 = 0;

                        loopLength *= 8;//Conver to bits.

                        while (loopLength > 0)
                        {
                            TreeNode innerItemNode = null;
                            result = Utility.AddNodeContainerPlus(Position.CHILD, innerLoopNode, out innerItemNode, "change_item", ItemType.ITEM, section, bitOffset, 0, descriptorLength);

                            bitOffsetPosition3 = section.GetLeftBitLength();

                            Int64 networkChangeId = 0;
                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, innerItemNode, out newNode, "network_change_id", ItemType.FIELD, section, ref bitOffset, 8, ref networkChangeId, ref descriptorLength);
                            }

                            Int64 networkChangeVersion = 0;
                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, innerItemNode, out newNode, "network_change_version", ItemType.FIELD, section, ref bitOffset, 8, ref networkChangeVersion, ref descriptorLength);
                            }


                            Int64 startTimeOfChange = 0;
                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, innerItemNode, out newNode, "start_time_of_change", ItemType.FIELD, section, ref bitOffset, 40, ref startTimeOfChange, ref descriptorLength);

                                if (result.Fine)
                                {
                                    Int64 year = 0;
                                    Int64 month = 0;
                                    Int64 day = 0;

                                    Utility.MjdToYmd(startTimeOfChange >> 24, ref year, ref month, ref day);

                                    String startTimeOfChangeDescription = String.Format("start_time_of_change: {0, 4:D4}-{1, 2:D2}-{2, 2:D2} {3, 2:X2}:{4, 2:X2}:{5, 2:X2}",
                                                                    year,
                                                                    month,
                                                                    day,
                                                                    (startTimeOfChange >> 16) & 0xFF,
                                                                    (startTimeOfChange >> 8) & 0xFF,
                                                                    (startTimeOfChange) & 0xFF);
                                    Utility.UpdateNode(newNode, startTimeOfChangeDescription, ItemType.FIELD);
                                }
                            }

                            Int64 changeDuration = 0;
                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, innerItemNode, out newNode, "change_duration", ItemType.FIELD, section, ref bitOffset, 24, ref changeDuration, ref descriptorLength);

                                if (result.Fine)
                                {

                                    String changeDurationDescription = String.Format("change_duration: {0, 2:X2}:{1, 2:X2}:{2, 2:X2}",
                                                                    (changeDuration >> 16) & 0xFF,
                                                                    (changeDuration >> 8) & 0xFF,
                                                                    (changeDuration) & 0xFF);
                                    Utility.UpdateNode(newNode, changeDurationDescription, ItemType.FIELD);
                                }
                            }


                            Int64 receiverCategory = 0;
                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, innerItemNode, out newNode, "receiver_category", ItemType.FIELD, section, ref bitOffset, 3, ref receiverCategory, ref descriptorLength);
                            }


                            Int64 invariantTsPresent = 0;
                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, innerItemNode, out newNode, "invariant_ts_present", ItemType.FIELD, section, ref bitOffset, 1, ref invariantTsPresent, ref descriptorLength);
                            }

                            Int64 changeType = 0;
                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, innerItemNode, out newNode, "change_type", ItemType.FIELD, section, ref bitOffset, 4, ref changeType, ref descriptorLength);
                            }

                            Int64 messageId = 0;
                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, innerItemNode, out newNode, "message_id", ItemType.FIELD, section, ref bitOffset, 8, ref messageId, ref descriptorLength);
                            }

                            if (result.Fine)
                            {
                                if (1 == invariantTsPresent)
                                {
                                    TreeNode ifNode = null;
                                    result = Utility.AddNodeContainerPlus(Position.CHILD, innerItemNode, out ifNode, "if (invariant_ts_present == ‘1’)", ItemType.FIELD, section, bitOffset, 0, descriptorLength); 
 
                                    if (result.Fine)
                                    {
                                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "invariant_ts_tsid", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                                    }

                                    if (result.Fine)
                                    {
                                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "invariant_ts_onid", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
                                    }
                                }
                            }

                            bitOffsetPosition4 = section.GetLeftBitLength();

                            Int64 innerItemBitLength = bitOffsetPosition3 - bitOffsetPosition4;
                            loopLength -= (innerItemBitLength); 
                            if (result.Fine)
                            {
                                String itemDescription = "network_change_id: " + Utility.GetValueHexString(networkChangeId, 8)
                                                            + ", network_change_version: " + Utility.GetValueHexString(networkChangeVersion, 8)
                                                            + ", message_id: " + Utility.GetValueHexString(messageId, 8);
                                Utility.UpdateNodeLength(innerItemNode, itemDescription, ItemType.ITEM, innerItemBitLength);
                            }
                            else
                            {
                                Utility.UpdateNodeLength(innerItemNode, "change_item(invalid)", ItemType.ERROR, innerItemBitLength);
                                break;
                            }
                        }
                    }

                    bitOffsetPosition2 = section.GetLeftBitLength();

                    if (result.Fine)
                    {
                        Int64 itemBitLength = bitOffsetPosition1 - bitOffsetPosition2;
                        String itemDescription = "cell_id: " + Utility.GetValueHexString(cellId, 8);
                        Utility.UpdateNodeLength(itemNode, itemDescription, ItemType.ITEM, itemBitLength);
                    }
                    else
                    {
                        Utility.UpdateNode(itemNode, "cell_item(invalid)", ItemType.ERROR);
                        break;
                    }
                }//while (descriptorLength > 0)
            }
            return result;
        }//ParseNetworkChangeNotifyDescriptor

        public static Result ParseMessageDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "message_id", ItemType.FIELD, section, ref bitOffset, 8, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                Int64 languageCode = 0;
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "ISO_639_language_code", ItemType.FIELD, section, ref bitOffset, 24, ref languageCode, ref descriptorLength);

                if (result.Fine)
                {
                    String languageCodeStr = "language_code: " + Utility.GetLanguageCodeStr(languageCode);
                    Utility.UpdateNode(newNode, languageCodeStr, ItemType.FIELD);
                }
            }

            if (result.Fine)
            {
                string textChar = null;
                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out textChar, section, bitOffset, descriptorLength, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "text_char: " + textChar, ItemType.FIELD, section, ref bitOffset, descriptorLength, ref descriptorLength);
                }
            }

            return result;
        }//ParseMessageDescriptor

        public static Result ParseTargetRegionDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            Int64 countryCode = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "country_code", ItemType.FIELD, section, ref bitOffset, 24, ref countryCode, ref descriptorLength);

                if (result.Fine)
                {
                    string itemDescription = "country_code: " + Utility.GetCountryCodeStr(countryCode);
                    Utility.UpdateNode(newNode, itemDescription, ItemType.ITEM);
                }
            }



            TreeNode loopNode = null;
            result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out loopNode, "region_loop", ItemType.LOOP, section, bitOffset, descriptorLength, descriptorLength);


            if (result.Fine)
            {
                Int64 bitOffsetPosition1 = 0;
                Int64 bitOffsetPosition2 = 0;

                while (descriptorLength > 0)
                {
                    TreeNode itemNode = null;
                    result = Utility.AddNodeContainerPlus(Position.CHILD, loopNode, out itemNode, "region_item", ItemType.ITEM, section, bitOffset, 0, descriptorLength);

                    bitOffsetPosition1 = section.GetLeftBitLength();

                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "reserved", ItemType.FIELD, section, ref bitOffset, 5, ref fieldValue, ref descriptorLength);
                    }

                    Int64 countryCodeFlag = 0;
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "country_code_flag", ItemType.FIELD, section, ref bitOffset, 1, ref countryCodeFlag, ref descriptorLength);
                    }

                    Int64 regionDepth = 0;
                    if (result.Fine)
                    {
                        result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "region_depth", ItemType.FIELD, section, ref bitOffset, 2, ref regionDepth, ref descriptorLength);
                    }

                    if (result.Fine)
                    {
                        if (1 == countryCodeFlag)
                        {
                            TreeNode ifNode = null;
                            result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out ifNode, "if (country_code_flag==1)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "country_code", ItemType.FIELD, section, ref bitOffset, 24, ref countryCode, ref descriptorLength);

                                if (result.Fine)
                                {
                                    string itemDescription = "country_code: " + Utility.GetCountryCodeStr(countryCode);
                                    Utility.UpdateNode(newNode, itemDescription, ItemType.ITEM);
                                }
                            }
                        }
                    }

                    if (result.Fine)
                    {
                        if (1 <= regionDepth)
                        {
                            TreeNode ifNode = null;
                            result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out ifNode, "if (region_depth>=1)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                            Int64 primaryRegionCode = 0;
                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode, out newNode, "primary_region_code", ItemType.FIELD, section, ref bitOffset, 8, ref primaryRegionCode, ref descriptorLength);
                            }

                            if (result.Fine)
                            {
                                if (2 <= regionDepth)
                                {
                                    TreeNode ifNode2 = null;
                                    result = Utility.AddNodeContainerPlus(Position.CHILD, ifNode, out ifNode2, "if (region_depth>=2)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                                    Int64 secondaryRegionCode = 0;
                                    if (result.Fine)
                                    {
                                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "secondary_region_code", ItemType.FIELD, section, ref bitOffset, 8, ref secondaryRegionCode, ref descriptorLength);
                                    }

                                    if (result.Fine)
                                    {
                                        if (3 == regionDepth)
                                        {
                                            TreeNode ifNode3 = null;
                                            result = Utility.AddNodeContainerPlus(Position.CHILD, ifNode2, out ifNode3, "if (region_depth==3)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                                            Int64 tertiaryRegionCode = 0;
                                            if (result.Fine)
                                            {
                                                result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode3, out newNode, "tertiary_region_code", ItemType.FIELD, section, ref bitOffset, 16, ref tertiaryRegionCode, ref descriptorLength);
                                            }
                                        }//if (3 == regionDepth) 
                                    }

                                }//if (2 <= regionDepth) 
                            }

                        }//if (1 <= regionDepth)
                    }

                    bitOffsetPosition2 = section.GetLeftBitLength();

                    if (result.Fine)
                    {
                        Int64 itemBitLength = bitOffsetPosition1 - bitOffsetPosition2;
                        String itemDescription = "region_depth: " + Utility.GetValueHexString(regionDepth, 8);
                        Utility.UpdateNodeLength(itemNode, itemDescription, ItemType.ITEM, itemBitLength);
                    }
                    else
                    {
                        Utility.UpdateNode(itemNode, "region_item(invalid)", ItemType.ERROR);
                        break;
                    }
                }//while (descriptorLength > 0)
            }
            return result;
        }//ParseTargetRegionDescriptor

        public static Result ParseTargetRegionNameDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;

            Int64 countryCode = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "country_code", ItemType.FIELD, section, ref bitOffset, 24, ref countryCode, ref descriptorLength);

                if (result.Fine)
                {
                    string itemDescription = "country_code: " + Utility.GetCountryCodeStr(countryCode);
                    Utility.UpdateNode(newNode, itemDescription, ItemType.ITEM);
                }
            }

            if (result.Fine)
            {
                Int64 languageCode = 0;
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "ISO_639_language_code", ItemType.FIELD, section, ref bitOffset, 24, ref languageCode, ref descriptorLength);

                if (result.Fine)
                {
                    String languageCodeStr = "language_code: " + Utility.GetLanguageCodeStr(languageCode);
                    Utility.UpdateNode(newNode, languageCodeStr, ItemType.FIELD);
                }
            }

            if (result.Fine)
            {

                TreeNode loopNode = null;
                //Add a container item.
                result = Utility.AddNodeContainerPlus(Position.CHILD, parentNode, out loopNode, "region_loop", ItemType.LOOP, section, bitOffset, descriptorLength, descriptorLength);

                if (result.Fine)
                {
                    Int64 position1LeftLengthInBit = 0;
                    Int64 position2LeftLengthInBit = 0;
                    while (descriptorLength > 0)
                    {
                        TreeNode itemNode = null;

                        result = Utility.AddNodeContainerPlus(Position.CHILD, loopNode, out itemNode, "region", ItemType.LOOP, section, bitOffset, 0, descriptorLength);

                        //Make a copy of current length;
                        position1LeftLengthInBit = section.GetLeftBitLength();

                        Int64 regionDepth = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "region_depth", ItemType.FIELD, section, ref bitOffset, 2, ref regionDepth, ref descriptorLength);
                        }

                        Int64 regionNameLength = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "region_name_length", ItemType.FIELD, section, ref bitOffset, 6, ref regionNameLength, ref descriptorLength);
                        }

                        if (result.Fine)
                        {
                            string textChar = null;
                            if (result.Fine)
                            {
                                result = Utility.GetTextPlus(out textChar, section, bitOffset, regionNameLength * 8, descriptorLength);
                            }

                            if (result.Fine)
                            {
                                result = Utility.AddNodeDataPlus(Position.CHILD, itemNode, out newNode, "text_char: " + textChar, ItemType.FIELD, section, ref bitOffset, regionNameLength * 8, ref descriptorLength);
                            }
                        }

                        Int64 primaryRegionCode = 0;
                        if (result.Fine)
                        {
                            result = Utility.AddNodeFieldPlus(Position.CHILD, itemNode, out newNode, "primary_region_code", ItemType.FIELD, section, ref bitOffset, 8, ref primaryRegionCode, ref descriptorLength);
                        }

                        if (2 <= regionDepth)
                        {
                            TreeNode ifNode2 = null;
                            result = Utility.AddNodeContainerPlus(Position.CHILD, itemNode, out ifNode2, "if (region_depth>=2)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                            Int64 secondaryRegionCode = 0;
                            if (result.Fine)
                            {
                                result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode2, out newNode, "secondary_region_code", ItemType.FIELD, section, ref bitOffset, 8, ref secondaryRegionCode, ref descriptorLength);
                            }

                            if (result.Fine)
                            {
                                if (3 == regionDepth)
                                {
                                    TreeNode ifNode3 = null;
                                    result = Utility.AddNodeContainerPlus(Position.CHILD, ifNode2, out ifNode3, "if (region_depth==3)", ItemType.FIELD, section, bitOffset, 0, descriptorLength);

                                    Int64 tertiaryRegionCode = 0;
                                    if (result.Fine)
                                    {
                                        result = Utility.AddNodeFieldPlus(Position.CHILD, ifNode3, out newNode, "tertiary_region_code", ItemType.FIELD, section, ref bitOffset, 16, ref tertiaryRegionCode, ref descriptorLength);
                                    }
                                }//if (3 == regionDepth) 
                            }

                        }//if (2 <= regionDepth) 

                        //Make the second copy of current length;
                        position2LeftLengthInBit = section.GetLeftBitLength();

                        if (result.Fine)
                        {
                            String itemDescription = "region_depth: " + Utility.GetValueHexString(regionDepth, 8)
                                                        + ", primary_region_code: " + Utility.GetValueHexString(primaryRegionCode, 8);
                            Int64 itemBitLength = (position1LeftLengthInBit - position2LeftLengthInBit);
                            Utility.UpdateNodeLength(itemNode, itemDescription, ItemType.ITEM, itemBitLength);
                        }
                        else
                        {
                            Utility.UpdateNode(itemNode, "region(invalid)", ItemType.ERROR);
                            break;//Break once something unexpected.
                        }
                    }//while ( lengthOfItems > 0 )

                }//To parse items.
            }

            Int64 textLength = 0;
            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "text_length", ItemType.FIELD, section, ref bitOffset, 8, ref textLength, ref descriptorLength);
            }
            if (result.Fine)
            {
                string eventText = null;
                if (result.Fine)
                {
                    result = Utility.GetTextPlus(out eventText, section, bitOffset, textLength * 8, descriptorLength);
                }

                if (result.Fine)
                {
                    result = Utility.AddNodeDataPlus(Position.CHILD, parentNode, out newNode, "event_text: " + eventText, ItemType.FIELD, section, ref bitOffset, textLength * 8, ref descriptorLength);
                }
            }

            return result;
        }//ParseTargetRegionNameDescriptor

        public static Result ParseServiceRelocatedDescriptor(Scope domain, TreeNode parentNode, DataStore section, byte tag, ref Int64 bitOffset, ref Int64 descriptorLength)
        {
            Result result = new Result();
            TreeNode newNode = null;
            Int64 fieldValue = 0;

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "old_original_network_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "old_transport_stream_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }

            if (result.Fine)
            {
                result = Utility.AddNodeFieldPlus(Position.CHILD, parentNode, out newNode, "old_service_id", ItemType.FIELD, section, ref bitOffset, 16, ref fieldValue, ref descriptorLength);
            }
            return result;
        }//ParseServiceRelocatedDescriptor
    }
}
