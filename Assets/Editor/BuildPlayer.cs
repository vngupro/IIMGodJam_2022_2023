using System;
using System.IO;
using System.IO.Compression;
using UnityEditor;
internal class BuildPlayer
{
    private static void StandaloneWindows64(BuildOptions bo = BuildOptions.None)
    {
        string path = "Build_" + DateTime.Now.ToString("dd.MM.yy_HH.mm") + "/";
        Directory.CreateDirectory(path);
        string[] dirs = Directory.GetDirectories(".", "Build_*", SearchOption.TopDirectoryOnly);
        string[] zips = Directory.GetFiles(".", "Build_*", SearchOption.TopDirectoryOnly);
        if (dirs != null)
        {
            foreach (var i in dirs)
            {
                Directory.Delete(i, true);
            }
        }
        else
        {
            Directory.CreateDirectory(path);
        }

        if (zips != null)
        {
            foreach (var i in zips)
            {
                File.Delete(i);
            }
        }
        BuildPipeline.BuildPlayer(
            Directory.GetFiles("Assets/Scenes", "*.unity"), path + "IIMGodJam_2022_2023.exe",
            BuildTarget.StandaloneWindows64,
            BuildOptions.CompressWithLz4HC | bo
        );
        if (bo != BuildOptions.AutoRunPlayer)
        {
            File.Delete(path + "UnityCrashHandler64.exe");
        }
        ZipFile.CreateFromDirectory(path, path.Remove(path.Length - 1) + ".zip");
    }
    [MenuItem("BuildPlayer/Build")]
    private static void Build()
    { StandaloneWindows64(); }
    [MenuItem("BuildPlayer/BuildRun")]
    private static void BuildRun()
    { StandaloneWindows64(BuildOptions.AutoRunPlayer); }
    [MenuItem("BuildPlayer/BuildShow")]
    private static void BuildShow()
    { StandaloneWindows64(BuildOptions.ShowBuiltPlayer); }
}