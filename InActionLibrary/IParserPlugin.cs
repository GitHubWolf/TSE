using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InActionLibrary
{
    /**
     * Interface for the parser plugins
     * 
     * The plugins must implement all the interface defined in this interface.
     */
    public interface IParserPlugin
    {
        /**
         * Get the name of this plugin.
         * 
         * @retval
         *     String to describe the name of this plugin.
         * 
         */
        string GetName();

        /**
         * Get a string to describle the capability of this parser.
         * 
         * @retval
         *     String to describe the plugin's capabilities.
         * 
         */
        string GetDescription();

        /**
         * Get the data type that this plugin supports.
         * 
         * Only one kind of data type is supported by each plugin, i.e. TS packet, PES packet or section.
         * 
         * @retval
         *     @see DataType
         * 
         */
        DataType GetSupportedType();

        /**
         * Parse the TS packet, PES packet or section.
         * 
         * @param parentNode parent tree node of to display the parsing result.
         * @param dataStore an instance containing the data to be parsed.
         * 
         * @retval
         *     void
         * 
         */
        void Parse(TreeNode parentNode, DataStore dataStore);

        /**
         * Start the plugin.
         * 
         * Before calling other methods in the plugin, this method will be called first. 
         * 
         * @retval
         *     void
         * 
         */
        void Start();

        /**
         * Stop the plugin.
         * 
         * When the main application has notified all data found to the plugin, it will notify the plugin by calling this method. 
         * 
         * @retval
         *     void
         * 
         */
        void Stop();
    }
}
