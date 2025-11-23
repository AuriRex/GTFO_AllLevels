using System.Reflection;
using AllLevels;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using Globals;
using HarmonyLib;

[assembly: AssemblyVersion(Plugin.VERSION)]
[assembly: AssemblyFileVersion(Plugin.VERSION)]
[assembly: AssemblyInformationalVersion(Plugin.VERSION)]

namespace AllLevels;

[BepInPlugin(GUID, MOD_NAME, VERSION)]
public class Plugin : BasePlugin
{
    public const string GUID = "dev.aurirex.gtfo.alllevels";
    public const string MOD_NAME = ManifestInfo.TSName;
    public const string VERSION = ManifestInfo.TSVersion;

    internal static ManualLogSource L;

    private static readonly Harmony _harmony = new(GUID);

    public override void Load()
    {
        L = Log;

        _harmony.PatchAll(Assembly.GetExecutingAssembly());

        L.LogInfo("Plugin loaded!");
    }

    [HarmonyPatch(typeof(Global), nameof(Global.Setup))]
    internal static class Patch
    {
        public static void Postfix()
        {
            Global.AllowFullRundown = true;
        }
    }
}