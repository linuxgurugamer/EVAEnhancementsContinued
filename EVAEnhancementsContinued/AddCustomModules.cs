#if false
#region  NO_LOCALIZATION
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EVAEnhancementsContinued
{

    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class AddCustomModules : MonoBehaviour
    {
        private static bool initialized = false;

        public void Update()
        {
            if (!initialized)
            {
                initialized = true;
                addEVAEnhancementsModule("kerbalEVA"); // NO_LOCALIZATION
                addEVAEnhancementsModule("kerbalEVAfemale"); // NO_LOCALIZATION
            }

        }

        private void addEVAEnhancementsModule(string partName)
        {
            try
            {
                ConfigNode node = new ConfigNode("MODULE");  // NO_LOCALIZATION
                node.AddValue("name", "EVAEnhancements"); // NO_LOCALIZATION

                var partInfo = PartLoader.getPartInfoByName(partName);
                var prefab = partInfo.partPrefab;
                var module = prefab.AddModule(node);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Object reference not set"))
                {
                    print("[EVAEnhancements] addEVAEnhancementsModule to " + partName + " succeeded.");
                }
                else
                {
                    print("[EVAEnhancements] addEVAEnhancementsModule [" + Time.time + "]: Failed to add the part module to " + partName + " " + ex.Message + "\n" + ex.StackTrace);
                }
            }
        }
    }
}
#endregion
#endif