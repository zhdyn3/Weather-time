using HarmonyLib;

public static class HarmonyPatches
{
    private static Harmony harmony;

    public static void ApplyHarmonyPatches()
    {
        if (harmony == null)
        {
            harmony = new Harmony("com.Zhdyn3.gorillatag.weathertime");
            harmony.PatchAll();
        }
    }

    public static void RemoveHarmonyPatches()
    {
        if (harmony != null)
        {
            harmony.UnpatchSelf(); 
            harmony = null;
        }
    }
}
