using System.Reflection;
using Harmony;
using UnityEngine;

namespace QuieterCrafting
{
	public class QuieterCrafting : FortressCraftMod
    {
        private static bool modInitialised = false;
        private static float savedVolume = 1;

        public override ModRegistrationData Register()
        {
            Debug.Log("QuieterCrafting loading...");

            if (!modInitialised)
            {
				saveVolume();
				applyHarmonyPatches();
            }
            else
            {
                Debug.Log("...Weird double init in QuieterCrafting");
            }

			Debug.Log("...QuieterCrafting loading finished.");

            return new ModRegistrationData();
        }

		private void applyHarmonyPatches()
		{
			Debug.Log("...Applying Harmony Patches.");
            var harmony = HarmonyInstance.Create("au.id.pjf.quietercrafting");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            modInitialised = true;
            Debug.Log("...Harmony patches applied.");
		}

		private void saveVolume()
		{
			Debug.Log("...saving volume");
			savedVolume = AudioListener.volume;
		}

        // Silence audio when we're not focused.
        void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus && AudioListener.volume < 0.01)
            {
                // If we've regained focus, and we had killed audio.
                AudioListener.volume = savedVolume;
            }
            else if (!hasFocus)
            {
                // If we've lost focus, then save the volume and mute audio.
                savedVolume = AudioListener.volume;
                AudioListener.volume = 0;
            }
        }
    }   
}
