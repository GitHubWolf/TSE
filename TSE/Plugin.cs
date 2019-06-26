using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InActionLibrary;

namespace TSE
{
    /**
     * The class will be used as the proxy to a plugin DLL.
     * Function call to this class will be translated to the methods inside the plugin using the Reflection technique.
     */
    public class Plugin : IParserPlugin
    {
        //Plugin file name.
        private string pluginFile = null;

        //For reflection.
        private Assembly pluginAssembly = null;
        Type pluginClass = null;
        object pluginInstance = null;

        /**
         * Public methods of the parser.
         */
        MethodInfo methodGetName = null;
        MethodInfo methodGetDescription = null;
        MethodInfo methodGetSupportedType = null;
        MethodInfo methodParse = null;
        MethodInfo methodStart = null;
        MethodInfo methodStop = null;

        public Result LoadPlugin(string pluginDllFile)
        {
            Result result = new Result();

            pluginFile = pluginDllFile;

            try
            {
                //Find the class.
                pluginAssembly = Assembly.LoadFile(pluginFile);

                if (null == pluginAssembly)
                {
                    result.SetResult(ResultCode.INVALID_PLUGIN);
                }

                if (result.Fine)
                {
                    pluginClass = pluginAssembly.GetType("CustomizedParser.ParserPlugin");

                    if (null == pluginClass)
                    {
                        result.SetResult(ResultCode.INVALID_PLUGIN);
                    }
                }

                //Find out all required methods.
                if (result.Fine)
                {
                    //Find GetName method.
                    methodGetName = pluginClass.GetMethod("GetName");

                    if (null == methodGetName)
                    {
                        result.SetResult(ResultCode.INVALID_PLUGIN);
                    }
                }

                if (result.Fine)
                {
                    //Find GetDescription method.
                    methodGetDescription = pluginClass.GetMethod("GetDescription");

                    if (null == methodGetDescription)
                    {
                        result.SetResult(ResultCode.INVALID_PLUGIN);
                    }
                }

                if (result.Fine)
                {
                    //Find GetSupportedType method.
                    methodGetSupportedType = pluginClass.GetMethod("GetSupportedType");

                    if (null == methodGetSupportedType)
                    {
                        result.SetResult(ResultCode.INVALID_PLUGIN);
                    }
                }

                if (result.Fine)
                {
                    //Find Parse method. void Parse(TreeNode parentNode, DataStore dataStore)

                    /* System.Type.GetType is mainly used to get the type of dynamic-loaded DLLs.
                    Type[] parameterTypes = new Type[3];
                    parameterTypes[0] = System.Type.GetType("System.Windows.Forms.TreeNode,System.Windows.Forms");
                    parameterTypes[1] = System.Type.GetType("InActionLibrary.DataStore,InActionLibrary");
                    parameterTypes[2] = System.Type.GetType("System.Int32");*/

                    //Parse method needs 2 parameters.
                    Type[] parameterTypes = new Type[2];
                    parameterTypes[0] = typeof(System.Windows.Forms.TreeNode);
                    parameterTypes[1] = typeof(InActionLibrary.DataStore);


                    methodParse = pluginClass.GetMethod("Parse", parameterTypes);

                    if (null == methodParse)
                    {
                        result.SetResult(ResultCode.INVALID_PLUGIN);
                    }
                }

                if (result.Fine)
                {
                    //Find Start method.
                    methodStart = pluginClass.GetMethod("Start");

                    if (null == methodStart)
                    {
                        result.SetResult(ResultCode.INVALID_PLUGIN);
                    }
                }

                if (result.Fine)
                {
                    //Find Stop method.
                    methodStop = pluginClass.GetMethod("Stop");

                    if (null == methodStop)
                    {
                        result.SetResult(ResultCode.INVALID_PLUGIN);
                    }
                }


                //Everything is fine, let's create the instance.
                if (result.Fine)
                {
                    pluginInstance = pluginAssembly.CreateInstance("CustomizedParser.ParserPlugin");

                    if (null == pluginInstance)
                    {
                        result.SetResult(ResultCode.INVALID_PLUGIN);
                    }
                }
            
            }
            catch (Exception)
            {
                result.SetResult(ResultCode.INVALID_PLUGIN);
            }

            return result;
        }

        /**
         * Get the name of this plugin.
         * 
         * @retval
         *     String to describe the name of this plugin.
         * 
         */
        public string GetName()
        {
            String name = null;

            try
            {
                name = (string)methodGetName.Invoke(pluginInstance, null);
            }
            catch (Exception)
            { 
            }

            return name;
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
            String description = null;

            try
            {
                description = (string)methodGetDescription.Invoke(pluginInstance, null);
            }
            catch (Exception)
            {
            }

            return description;
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
            DataType dataType = DataType.UNKNOWN;

            try
            {
                dataType = (DataType)methodGetSupportedType.Invoke(pluginInstance, null);
            }
            catch (Exception)
            {
            }

            return dataType;
        }

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
        public void Parse(TreeNode parentNode, DataStore dataStore)
        {
            try
            {
                object[] parameters = new object[2];
                parameters[0] = parentNode;
                parameters[1] = dataStore;

                methodParse.Invoke(pluginInstance, parameters);
            }
            catch (Exception)
            {
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
            try
            {
                methodStart.Invoke(pluginInstance, null);
            }
            catch (Exception)
            {
            }
        }

        /**
         * Stop the plugin.
         * 
         * When the main application has notified all data created to the plugin, it will notify the plugin by calling this method. 
         * 
         * @retval
         *     void
         * 
         */
        public void Stop()
        {
            try
            {
                methodStop.Invoke(pluginInstance, null);
            }
            catch (Exception)
            {
            }
        }

        /**
         * Set the name as the description to this instance.
         */
        public override string ToString()
        {
            return pluginAssembly.ManifestModule.Name;
        }

    }
}
