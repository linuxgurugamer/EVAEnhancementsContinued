using KSP.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP.UI.Screens;
using ClickThroughFix;
using ToolbarControl_NS;

namespace EVAEnhancementsContinued
{
    internal class SettingsWindow
    {
        internal ToolbarControl toolbarControl;

        internal bool showWindow;
        internal static bool windowRectDefined = false;
        internal static Rect windowRect;

        internal Vector2 scrollPos = new Vector2(0f, 0f);
        internal int windowId;
        internal Settings settings;
        internal ModStyle modStyle;

        private bool settingPitchDown = false;
        private bool settingPitchUp = false;
        private bool settingRollLeft = false;
        private bool settingRollRight = false;

    //    private bool settingsFillFromPod = false;
        private bool settingsEvaNavballFollowsKerbal = false;
        private bool settingsEvaHideNavballMarkers = false;


        internal SettingsWindow()
        {
            settings = SettingsWrapper.Instance.gameSettings;
            modStyle = SettingsWrapper.Instance.modStyle;
            showWindow = false;
            if (!windowRectDefined)
                windowRect = new Rect((Screen.width - 300) / 1, (Screen.height - 300) / 2, 250, 325);
            windowRectDefined = true;
            windowId = GUIUtility.GetControlID(FocusType.Passive);
        }

        internal void draw()
        {
            if (showWindow)
            {
                GUI.skin = modStyle.skin;
                windowRect =ClickThruBlocker.GUILayoutWindow(windowId, windowRect, drawWindow, "");
            }
        }

        internal void drawWindow(int id)
        {


            GUI.skin = modStyle.skin;
            GUILayout.BeginVertical();
            GUILayout.Label(Localizer.Format("#LOC_EvaEnhance_EVA_Enhancements_Settings"), 
                modStyle.guiStyles["titleLabel"]); // NO_LOCALIZATIONs
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            scrollPos = GUILayout.BeginScrollView(scrollPos);

            GUILayout.Label(Localizer.Format("#LOC_EvaEnhance_Default_Jetpack_Power") + 
                settings.defaultJetPackPower.ToString("P0")); // NO_LOCALIZATIONs
            float newJetPackPower = GUILayout.HorizontalSlider(settings.defaultJetPackPower, 0f, 1f);
            if (newJetPackPower != settings.defaultJetPackPower)
            {
                settings.defaultJetPackPower = newJetPackPower;
                settings.Save();

            }
            GUILayout.Space(10f);

            GUILayout.Label(Localizer.Format("#LOC_EvaEnhance_Default_Precision_Jetpack") + 
                settings.defaultPrecisionModePower.ToString("P0")); // NO_LOCALIZATIONs
            float newPrecisionJetPackPower = GUILayout.HorizontalSlider(settings.defaultPrecisionModePower, 0f, 1f);
            if (newPrecisionJetPackPower != settings.defaultPrecisionModePower)
            {
                settings.defaultPrecisionModePower = newPrecisionJetPackPower;
                settings.Save();

            }
            GUILayout.Space(10f);

            string str;

            GUILayout.BeginHorizontal();
            GUILayout.Label(Localizer.Format("#LOC_EvaEnhance_Pitch_Down"), GUILayout.ExpandWidth(true));
            KeyCodeExtended c = null;
            if (Event.current.isKey)
                c = new KeyCodeExtended(Event.current.keyCode);
            if (settingPitchDown)
            {
                GUILayout.Label(Localizer.Format("#LOC_EvaEnhance_Press_any_key"));
                if (Event.current.isKey && c.ToString() != GameSettings.MODIFIER_KEY.primary.ToString())
                {
                    Log.Info("keyodeExtended: " + c.ToString() + ", primary: " + GameSettings.MODIFIER_KEY.primary.ToString()); // NO_LOCALIZATIONs
                    settings.pitchDown = new KeyCodeExtended(Event.current.keyCode);
                    settings.modKeypitchDown = ExtendedInput.GetKey(GameSettings.MODIFIER_KEY.primary);
                    settings.Save();
                    settingPitchDown = false;
                }
            }
            else
            {
                str = settings.pitchDown.ToString();
                if (settings.modKeypitchDown)
                    str = Localizer.Format("#LOC_EvaEnhance_Alt") + str;
                if (GUILayout.Button(new GUIContent(str), GUILayout.Width(125)))
                {
                    settingPitchDown = true;
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(Localizer.Format("#LOC_EvaEnhance_Pitch_Up"), GUILayout.ExpandWidth(true));

            if (settingPitchUp)
            {
                GUILayout.Label(Localizer.Format("#LOC_EvaEnhance_Press_any_key"));
                if (Event.current.isKey &&  c.ToString() != GameSettings.MODIFIER_KEY.primary.ToString())
                {
                    settings.pitchUp = new KeyCodeExtended(Event.current.keyCode);
                    settings.modKeypitchUp = ExtendedInput.GetKey(GameSettings.MODIFIER_KEY.primary);
                    settings.Save();
                    settingPitchUp = false;
                }
            }
            else
            {
                str = settings.pitchUp.ToString();
                if (settings.modKeypitchUp)
                    str = Localizer.Format("#LOC_EvaEnhance_Alt") + str;
                if (GUILayout.Button(new GUIContent(str), GUILayout.Width(125)))
                {
                    settingPitchUp = true;
                }
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(Localizer.Format("#LOC_EvaEnhance_Roll_Left"), GUILayout.ExpandWidth(true));

            if (settingRollLeft)
            {
                GUILayout.Label(Localizer.Format("#LOC_EvaEnhance_Press_any_key"));
                if (Event.current.isKey &&  c.ToString() != GameSettings.MODIFIER_KEY.primary.ToString())
                {
                    settings.rollLeft = new KeyCodeExtended(Event.current.keyCode);
                    settings.modKeyrollLeft = ExtendedInput.GetKey(GameSettings.MODIFIER_KEY.primary);
                    settings.Save();
                    settingRollLeft = false;
                }
            }
            else
            {
                str = settings.rollLeft.ToString();
                if (settings.modKeyrollLeft)
                    str = Localizer.Format("#LOC_EvaEnhance_Alt") + str;
                if (GUILayout.Button(new GUIContent(str), GUILayout.Width(125)))
                {
                    settingRollLeft = true;
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label(Localizer.Format("#LOC_EvaEnhance_Roll_Right"), GUILayout.ExpandWidth(true));

            if (settingRollRight)
            {
                GUILayout.Label(Localizer.Format("#LOC_EvaEnhance_Press_any_key"));
                if (Event.current.isKey &&  c.ToString() != GameSettings.MODIFIER_KEY.primary.ToString())
                {
                    settings.rollRight = new KeyCodeExtended(Event.current.keyCode);
                    settings.modKeyrollRight = ExtendedInput.GetKey(GameSettings.MODIFIER_KEY.primary);
                    settings.Save();
                    settingRollRight = false;
                }
            }
            else
            {
                str = settings.rollRight.ToString();
                if (settings.modKeyrollRight)
                    str = Localizer.Format("#LOC_EvaEnhance_Alt") + str;
                if (GUILayout.Button(new GUIContent(str), GUILayout.Width(125)))
                {
                    settingRollRight = true;
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(10);

#if false
            
            GUILayout.BeginHorizontal();
            newUseStockToolbar = GUILayout.Toggle(settings.useStockToolbar, Localizer.Format("#LOC_EvaEnhance_Use_Stock_Toolbar"));
            GUILayout.EndHorizontal();

            if (newUseStockToolbar != settings.useStockToolbar)
            {
                settings.useStockToolbar = newUseStockToolbar;
                settings.Save();
            }
#endif

            GUILayout.BeginHorizontal();
            // GUILayout.Label("NavBall follows Kerbal:");
            settingsEvaNavballFollowsKerbal = GUILayout.Toggle(settings.evaNavballFollowsKerbal, Localizer.Format("#LOC_EvaEnhance_NavBall_follows_Kerbal"));
            if (settingsEvaNavballFollowsKerbal != settings.evaNavballFollowsKerbal)
            {
                settings.evaNavballFollowsKerbal = settingsEvaNavballFollowsKerbal;
                settings.Save();
            }
            GUILayout.EndHorizontal();

            
                 GUILayout.BeginHorizontal();
            // GUILayout.Label("NavBall follows Kerbal:");
            settingsEvaHideNavballMarkers = GUILayout.Toggle(settings.evaHideNavballMarkers, Localizer.Format("#LOC_EvaEnhance_Hide_Navball_markers_on_E"));
            if (settingsEvaHideNavballMarkers != settings.evaHideNavballMarkers)
            {
                settings.evaHideNavballMarkers = settingsEvaHideNavballMarkers;
                settings.Save();
            }
            GUILayout.EndHorizontal();

            GUILayout.EndScrollView();
            GUILayout.Space(25f);
            GUILayout.EndVertical();

            if (GUI.Button(new Rect(windowRect.width - 18, 3f, 15f, 15f), new GUIContent("X"))) // NO_LOCALIZATIONs
            {
                showWindow = false;
                toolbarControl.SetFalse(false);
            }

            GUI.DragWindow();

        }

    }
}
