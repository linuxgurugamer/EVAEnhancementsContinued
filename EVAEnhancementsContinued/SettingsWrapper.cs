using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAEnhancementsContinued
{
    public sealed class SettingsWrapper
    {
        public static readonly SettingsWrapper instance = new SettingsWrapper();
        public Settings gameSettings { get; private set; }
        public ModStyle modStyle { get;  set; }

        static SettingsWrapper() 
        {
        }

        private SettingsWrapper()
        {
            gameSettings = new Settings("../PluginData/EVAEnhancements.cfg"); // NO_LOCALIZATION
            //modStyle = new ModStyle();
        }

        public static SettingsWrapper Instance {
            get
            {
                return instance;
            }
        }
    }

}
