using KSP.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP.UI.Screens.Flight;

namespace EVAEnhancementsContinued
{
    public class EVAEnhancements : PartModule
    {
        // Action menu fields

        [KSPField(guiActive = true, guiName = "Profession", isPersistant = true)]
        string kerbalProfession = "";

        [KSPField(guiName = "Jetpack Power", guiFormat = "P0", guiActive = true, isPersistant = true), UI_FloatRange(minValue = .1f, maxValue = 1f, stepIncrement = 0.01f)]
        public float jetPackPower = 1f;

        [KSPField(guiName = "Precision Mode Power", guiFormat = "P0", guiActive = true, isPersistant = true), UI_FloatRange(minValue = 0.01f, maxValue = .5f, stepIncrement = 0.01f)]
        public float precisionModePower = 0.25f;

        [KSPField(guiName = "Precision Mode Power", guiActive = true, isPersistant = true)]
        public bool precisionControls = false;

        public bool powerInited = false;

        public float currentPower = 1f;

        bool rotateOnMove = false;


        // Variables to keep track of original values
        float origLinPower = 0f;
        float origRotPower = 0f;
        float origPropConsumption = 0f;

        // Pointers to various objects
        KerbalEVA eva = null;

        Settings settings = SettingsWrapper.Instance.gameSettings;

        public override void OnStart(PartModule.StartState state)
        {
            base.OnStart(state);

            rotateOnMove = GameSettings.EVA_ROTATE_ON_MOVE;

            // Load settings
            settings.Load();
            settings.Save();
            rotateOnMove = GameSettings.EVA_ROTATE_ON_MOVE;

            // Display profession and level
            ProtoCrewMember myKerbal = this.part.protoModuleCrew.SingleOrDefault();
            this.Fields["kerbalProfession"].guiName = myKerbal.experienceTrait.Title; // NO_LOCALIZATION
            kerbalProfession = "Level " + myKerbal.experienceLevel.ToString(); // NO_LOCALIZATION

            // Set default jet pack power
            if (!powerInited)
            {
                powerInited = true;
                jetPackPower = settings.defaultJetPackPower;
                precisionModePower = settings.defaultPrecisionModePower;
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            // Only run if the EVA'd Kerbal is the active vessel
            if (this.vessel == FlightGlobals.ActiveVessel)
            {
                // Toggle precision node
                if (GameSettings.PRECISION_CTRL.GetKeyDown())
                {
                    precisionControls = !precisionControls;
                    if (precisionControls)
                    {
                        ScreenMessages.PostScreenMessage(Localizer.Format("#LOC_EvaEnhance_Precision_Controls_Enable"), 2f, ScreenMessageStyle.UPPER_CENTER);
                    }
                    else
                    {
                        ScreenMessages.PostScreenMessage(Localizer.Format("#LOC_EvaEnhance_Precision_Controls_Disabl"), 2f, ScreenMessageStyle.UPPER_CENTER);
                    }
                }

                // Toggle Rotate on Move
                if (GameSettings.SAS_TOGGLE.GetKeyDown())
                {
                    rotateOnMove = !rotateOnMove;
                }

                // Set pointer to KerbalEVA
                if (eva == null || this.vessel != FlightGlobals.ActiveVessel)
                {
                    eva = FlightGlobals.ActiveVessel.GetComponent<KerbalEVA>();
                }

                // Only process is this is current vessel and the eva pointer was set previously
                if (this.vessel == FlightGlobals.ActiveVessel && eva != null)
                {

                    if (eva.JetpackDeployed)
                    {
                        if (origLinPower == 0f)
                        {
                            // Grab original values
                            origLinPower = eva.linPower;
                            origRotPower = eva.rotPower;
                            origPropConsumption = eva.PropellantConsumption;
                        }

                        // Make sure this is set properly
                       // GameSettings.EVA_ROTATE_ON_MOVE = rotateOnMove;

                        // Determine current jetpack power
                        if (precisionControls)
                        {
                            // precisionModePower = settings.defaultPrecisionModePower;
                            currentPower = precisionModePower;
                        }
                        else
                        {
                            //jetPackPower = settings.defaultJetPackPower;
                            currentPower = jetPackPower;
                        }

                        // Set the jetpack power and fuel consumption
                        eva.linPower = origLinPower * currentPower;
                        eva.rotPower = origRotPower * currentPower;
                        eva.PropellantConsumption = origPropConsumption * currentPower;

                        // Detect key presses
                        Log.Info("Input.GetKey(settings.pitchDown): " + ExtendedInput.GetKey(settings.pitchDown).ToString() + "  mod: " + ExtendedInput.GetKey(GameSettings.MODIFIER_KEY.primary).ToString());

                        if (ExtendedInput.GetKey(settings.pitchDown) &&
                            ((settings.modKeypitchDown == false && !ExtendedInput.GetKey(GameSettings.MODIFIER_KEY.primary)) ||
                            (settings.modKeypitchDown == true && ExtendedInput.GetKey(GameSettings.MODIFIER_KEY.primary))))
                            EVAController.Instance.UpdateEVAFlightProperties(-1, 0, jetPackPower);

                        if (ExtendedInput.GetKey(settings.pitchUp) &&
                             ((settings.modKeypitchUp == false && !ExtendedInput.GetKey(GameSettings.MODIFIER_KEY.primary)) ||
                            (settings.modKeypitchDown == true && ExtendedInput.GetKey(GameSettings.MODIFIER_KEY.primary))))
                            EVAController.Instance.UpdateEVAFlightProperties(1, 0, jetPackPower);

                        if (ExtendedInput.GetKey(settings.rollLeft) &&
                             ((settings.modKeyrollLeft == false && !ExtendedInput.GetKey(GameSettings.MODIFIER_KEY.primary)) ||
                            (settings.modKeyrollLeft == true && ExtendedInput.GetKey(GameSettings.MODIFIER_KEY.primary))))
                            EVAController.Instance.UpdateEVAFlightProperties(0, -1, jetPackPower);

                        if (ExtendedInput.GetKey(settings.rollRight) &&
                             ((settings.modKeyrollRight == false && !ExtendedInput.GetKey(GameSettings.MODIFIER_KEY.primary)) ||
                            (settings.modKeyrollRight == true && ExtendedInput.GetKey(GameSettings.MODIFIER_KEY.primary))))
                            EVAController.Instance.UpdateEVAFlightProperties(0, 1, jetPackPower);

                    }

                }

            }

        }

        KSP.UI.Screens.Flight.RCSDisplay rcsDisplay;
        KSP.UI.Screens.Flight.ThrottleGauge throttleGauge;
        KSP.UI.Screens.Flight.SASDisplay sasGauge;

        internal void LateUpdate()
        {
            // Only process if this is current vessel and the eva pointer was set previously
            if (this.vessel == FlightGlobals.ActiveVessel && eva != null)
            {

                if (eva.JetpackDeployed)
                {

                    // Set throttle to current jetpack power
                    if (throttleGauge == null)
                        throttleGauge = UnityEngine.Object.FindObjectOfType<KSP.UI.Screens.Flight.ThrottleGauge>();
                    if (throttleGauge != null)
                        throttleGauge.gauge.SetValue(currentPower);
                    // FlightUIController.fetch.thr.setValue(currentPower);

                    // Turn on RCS light for jetpack
                    if (rcsDisplay == null)
                        rcsDisplay = UnityEngine.Object.FindObjectOfType<KSP.UI.Screens.Flight.RCSDisplay>();
                    if (rcsDisplay != null)
                        rcsDisplay.stateImage.SetState(1);
                    //FlightUIController.fetch.rcs.renderer.material.mainTexture = FlightUIController.fetch.rcs.ledColors[1];

                    // Turn on SAS light for "EVA Rotate on Move"
                    if (sasGauge == null)
                        sasGauge = UnityEngine.Object.FindObjectOfType<KSP.UI.Screens.Flight.SASDisplay>();
                    if (sasGauge != null)
                        if (GameSettings.EVA_ROTATE_ON_MOVE)
                        {
                            sasGauge.stateImage.SetState(1);
                            //FlightUIController.fetch.SAS.renderer.material.mainTexture = FlightUIController.fetch.SAS.ledColors[1];
                        }
                        else
                        {
                            sasGauge.stateImage.SetState(0);
                            //FlightUIController.fetch.SAS.renderer.material.mainTexture = FlightUIController.fetch.SAS.ledColors[0];
                        }

                }
                else
                {
                    // Set throttle to 0 and turn off lights
                    if (throttleGauge != null)
                        throttleGauge.gauge.SetValue(0f);
                    if (rcsDisplay != null)
                        rcsDisplay.stateImage.SetState(0);
                    if (sasGauge != null)
                        sasGauge.stateImage.SetState(0);

                    //FlightUIController.fetch.thr.setValue(0f);
                    //FlightUIController.fetch.rcs.renderer.material.mainTexture = FlightUIController.fetch.rcs.ledColors[0];
                    //FlightUIController.fetch.SAS.renderer.material.mainTexture = FlightUIController.fetch.SAS.ledColors[0];
                }


            }

        }

    }
}
