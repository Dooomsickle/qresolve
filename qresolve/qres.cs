using VRC.SDK3.Video.Components;
using VRC.SDKBase;
using HarmonyLib;
using MelonLoader;
using qresolve;
using System.Reflection;

[assembly: MelonInfo(typeof(qres), "qResolve", "1.0.0.0", "Doomsickle")]
[assembly: MelonGame("VRChat")]

namespace qresolve;

public class qres : MelonMod
{
    public override void OnInitializeMelon()
    {
        try
        {
            HarmonyInstance.Patch(typeof(VRCUnityVideoPlayer).GetMethod("LoadURL"),
                new HarmonyMethod(typeof(qres).GetMethod(nameof(repl_url), BindingFlags.NonPublic | BindingFlags.Static)));
            MelonLogger.Msg($"successfully patched method {nameof(VRCUnityVideoPlayer.LoadURL)} of class {typeof(VRCUnityVideoPlayer)}");
        }
        catch(Exception e)
        {
            MelonLogger.Msg($"failed to patch method {nameof(VRCUnityVideoPlayer.LoadURL)} of class {typeof(VRCUnityVideoPlayer)}: {e}");
        }

        try
        {
            HarmonyInstance.Patch(typeof(VRC.SDK3.Video.Components.AVPro.VRCAVProVideoPlayer).GetMethod("LoadURL"),
                new HarmonyMethod(typeof(qres).GetMethod(nameof(repl_url), BindingFlags.NonPublic | BindingFlags.Static)));
            MelonLogger.Msg($"successfully patched method {nameof(VRC.SDK3.Video.Components.AVPro.VRCAVProVideoPlayer.LoadURL)} of class {typeof(VRC.SDK3.Video.Components.AVPro.VRCAVProVideoPlayer)}");
        }
        catch (Exception e)
        {
            MelonLogger.Msg($"failed to patch method {nameof(VRC.SDK3.Video.Components.AVPro.VRCAVProVideoPlayer.LoadURL)} of class {typeof(VRC.SDK3.Video.Components.AVPro.VRCAVProVideoPlayer)}: {e}");
        }
        
    }

    private static void repl_url(ref VRCUrl url)
    {
        MelonLogger.Msg("load event fired");
        
        if(url.url.Contains("https://youtu.be"))
        {
            url = new("https://shay.loan/" + url.url.Split('/').Last());
        }
        else if(url.url.Contains("https://youtube.com/watch"))
        {
            url = new("https://shay.loan/" + url.url.Split('=').Last());
        }
        else
        {
            MelonLogger.Msg("video was not a youtube link, not replacing");
        }
    }
}