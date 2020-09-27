using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
        
namespace Murdoch.GAD361.SwipeHeroes
{
    public static class ExportPackage
    {
        [MenuItem("Export/Export Assets and Settings")]
        public static void export()
        {
            string[] projectContent = new string[] {"Assets", "ProjectSettings/TagManager.asset","ProjectSettings/InputManager.asset","ProjectSettings/ProjectSettings.asset"};
            AssetDatabase.ExportPackage(projectContent, "Exported.unitypackage",ExportPackageOptions.Interactive | ExportPackageOptions.Recurse |ExportPackageOptions.IncludeDependencies);
            Debug.Log("Project Exported");
        }
    }
}
