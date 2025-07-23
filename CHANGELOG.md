ChangeLog

## 1.1.1 - 2015/12/06
- fixes dumb mistake in version file

## 1.1.0 - 2015/12/06
- fixes some KSP 1.05 compatibility issues
- removes some custom EVA navball functionality (since it's now stock)

## 1.0.0 - 2015/08/09
- initial public release

Adoption by LGG

EVAEnhancementsContinued

0.1.0
	Initial release for 1.2

0.1.1
	Fixed missing icons
	Fixed cfg file save location

0.1.2
	Fixed problem with Toolbarwrapper.cs

0.1.3
	Added setting to allow/disallow EVA kerbal from filling EVA propellent from the pod
	Added AssemblyVersion automatic updating
	Added standard log class
	Changed initialization of kerbalProfession from null to an empty string

0.1.4
	Fixed harmless nullref when closing the window with the "x" button and using Blizzy's Toolbarwrapper

0.1.5
	Fixed nullref in the onCrewOnEva when resource didn't exist

0.1.6
	Fixed nullref when switching to a scansat

0.1.7
	Added fix to allow Navball follow kerbal on EVA
	Added navball fix to settings screen
	Updated toggle settings to be consistent with other settings

0.1.8
	Fixed nullref when switching to kerbal (minor, only one time, looks like an uninitialized parameter)
	Added option to disable Navball markers on EVA
	Added ability to use a modifier key with a regular key (but disables the ability to use the mod key by itself).  On Windows, the key is ALT

0.1.9
	Removed the EVA Fuel code, it's now in the EVAFuel mod

0.1.10
	Updated for 1.3
	Added check for nav in lateupdate being null

0.1.11
	updated for 1.3.1

0.1.12
	Added code to save/load the pitch & roll.  Old code wasn't working because the values changed from KeyCode to KeyCodeExtended

0.1.13
	Updated for 1.7.2
	Updated AssemblyVersion.tt to allow for location-independent builds

0.1.12.1
	Removed the overriding of the stock EVA_ROTATE_ON_MOVE when going eva

0.1.13
	Updated for 1.4.1
	Added support for ClickthrougBlocker
	Added support for ToolbarController
	Deleted lots of old disabled code

0.1.13.1
	Updated to use the ToolbarController registration
	Removed setting for blizzy from mod
	Fixed ability to use the primary Modifier key in conjunction with another key

0.1.13.2
	Fixed log spam from debugging statements

0.1.13.3
	Version bump for 1.5 rebuild

0.1.13.4
	Moved code adding a module to the kerbalEVA into a MM script,  This also fixes an issue with the new kerbals from the expansion pack not being able to use this

0.1.13.5
	Fixed error in .version file

0.1.14
	Version bump for 1.7.2

0.1.14.1
	Fixed duplicate entries in PAW when BG DLC is installed
		Updated MM script:
			This ModuleManager code is because the Serenity DLC has duplicate kerbal parts, 
			apparently used to add new modules to an existing kerbal
			This was causing modules to get duplicated.
			By only adding to those kerbalEVA parts which do NOT have the ROCScience experiment,
			we ensure that duplicate entries aren't mad

0.1.15
	Added InstallChecker
	Updated for KSP 1.8

0.1.15.1
	Updated MiniAVC.dll to 1.4.0.2 to fix a nasty CTD

0.1.15.2
	Really did the MiniAVC update

0.1.15.3
	Added AssemblyFileVersion
	Updated version file for 1.12

0.1.16
	Fixed initializion of ModStyle to be called from a Unity Start in the ToolbarRegistration.cs
	Updated version file to use github for new version info
	Localized the mod