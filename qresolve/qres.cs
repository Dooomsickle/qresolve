using VRC.SDK3.Video.Components;
using VRC.SDKBase;
using HarmonyLib;
using MelonLoader;
using qresolve;

[assembly: MelonInfo(typeof(qres), "qResolve", "1.0.0.0", "Doomsickle")]
[assembly: MelonGame("VRChat")]

namespace qresolve;

[HarmonyPatch(typeof(VRCUnityVideoPlayer), nameof(VRCUnityVideoPlayer.LoadURL))]
[HarmonyPatch(typeof(VRC.SDK3.Video.Components.AVPro.VRCAVProVideoPlayer), nameof(VRC.SDK3.Video.Components.AVPro.VRCAVProVideoPlayer.LoadURL))]
//[HarmonyPatch(typeof(VRC.SDK3.Video.Components.Base.BaseVRCVideoPlayer), nameof(VRC.SDK3.Video.Components.Base.BaseVRCVideoPlayer.LoadURL))]
public class qres : MelonMod
{
    public override void OnInitializeMelon() => MelonLogger.Msg("mod initialized");

    static void Prefix(ref VRCUrl url)
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