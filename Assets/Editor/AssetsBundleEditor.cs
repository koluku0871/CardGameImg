using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using static Codice.Client.Common.DiffMergeToolConfig;
using UnityEngine.Windows;

public class AssetBundleEditor
{
    static string root_path = "AssetBundles";
    static string variant = "assetbundle";

    [MenuItem("AssetBundles/Windows64_ALL", false)]
    static private void BuildAssetBundlesForWindows64()
    {
        (var target_platform, var output_path) = CreateDirectoryForWindows64();
        var asset_bundle_build_list = GetAssetBundleBuild(output_path);
        BuildAssetBundles(target_platform, output_path, asset_bundle_build_list);
    }

    [MenuItem("AssetBundles/Windows64_bs", false)]
    static private void BuildAssetBundlesForWindows64ToBs()
    {
        (var target_platform, var output_path) = CreateDirectoryForWindows64();
        var asset_bundle_build_list = GetAssetBundleBuild(output_path, "bs_");
        BuildAssetBundles(target_platform, output_path, asset_bundle_build_list);
    }

    [MenuItem("AssetBundles/Windows64_digimon", false)]
    static private void BuildAssetBundlesForWindows64ToDigimon()
    {
        (var target_platform, var output_path) = CreateDirectoryForWindows64();
        var asset_bundle_build_list = GetAssetBundleBuild(output_path, "digimon_");
        BuildAssetBundles(target_platform, output_path, asset_bundle_build_list);
    }

    [MenuItem("AssetBundles/Windows64_dm", false)]
    static private void BuildAssetBundlesForWindows64ToDm()
    {
        (var target_platform, var output_path) = CreateDirectoryForWindows64();
        var asset_bundle_build_list = GetAssetBundleBuild(output_path, "dm_");
        BuildAssetBundles(target_platform, output_path, asset_bundle_build_list);
    }

    [MenuItem("AssetBundles/Windows64_hololive", false)]
    static private void BuildAssetBundlesForWindows64ToHololive()
    {
        (var target_platform, var output_path) = CreateDirectoryForWindows64();
        var asset_bundle_build_list = GetAssetBundleBuild(output_path, "hololive_");
        BuildAssetBundles(target_platform, output_path, asset_bundle_build_list);
    }

    private static (UnityEditor.BuildTarget, String) CreateDirectoryForWindows64()
    {
        // 他プラットフォームを対象にする場合はここを変更する(今回は Windows64 向け)
        UnityEditor.BuildTarget target_platform = UnityEditor.BuildTarget.StandaloneWindows64;

        var output_path = System.IO.Path.Combine(root_path, target_platform.ToString());

        if (System.IO.Directory.Exists(output_path) == false)
        {
            System.IO.Directory.CreateDirectory(output_path);
        }

        Debug.Log("output_path : " + output_path);

        return (target_platform, output_path);
    }

    private static List<UnityEditor.AssetBundleBuild> GetAssetBundleBuild(String output_path, string bundleType = null)
    {
        var asset_bundle_build_list = new List<UnityEditor.AssetBundleBuild>();
        foreach (string asset_bundle_name in UnityEditor.AssetDatabase.GetAllAssetBundleNames())
        {
            if (!string.IsNullOrEmpty(bundleType))
            {
                if (!asset_bundle_name.Contains(bundleType))
                {
                    continue;
                }
            }

            if (System.IO.File.Exists(output_path + "/" + asset_bundle_name + ".assetbundle"))
            {
                continue;
            }

            Debug.Log("asset_bundle_name : " + asset_bundle_name);
            var builder = new AssetBundleBuild();
            builder.assetBundleName = asset_bundle_name;
            builder.assetNames = UnityEditor.AssetDatabase.GetAssetPathsFromAssetBundle(builder.assetBundleName);
            builder.assetBundleVariant = variant;
            asset_bundle_build_list.Add(builder);
        }

        return asset_bundle_build_list;
    }

    private static void BuildAssetBundles(
        UnityEditor.BuildTarget target_platform,
        String output_path,
        List<UnityEditor.AssetBundleBuild> asset_bundle_build_list
    ){

        if (asset_bundle_build_list.Count > 0)
        {
            UnityEditor.BuildPipeline.BuildAssetBundles(
                output_path,
                asset_bundle_build_list.ToArray(),
                UnityEditor.BuildAssetBundleOptions.ChunkBasedCompression,
                target_platform
            );
        }
    }
}
