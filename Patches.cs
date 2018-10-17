using Harmony;

namespace QuieterCrafting
{

	[HarmonyPatch(typeof(AudioSpeechManager))]
	[HarmonyPatch("UpdateStorageHopper")]
	public static class Patch_StorageHopper_Audio
	{
		public static bool Prefix()
		{
			// No talking hoppers, please.
			return false;
		}
	}

	[HarmonyPatch(typeof(AudioHUDManager))]
	[HarmonyPatch("CraftingVocal")]
	public static class Patch_AudioHUDManager_CraftingVocal
	{
		public static bool Prefix()
		{
			// No audio on crafting, please.
			return false;
		}
	}   
}