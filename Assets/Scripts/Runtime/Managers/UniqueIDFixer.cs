#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class UniqueIDFixer
{
    [MenuItem("Tools/Fix Duplicate UniqueIDs")]
    public static void Fix()
    {
        var all = Object.FindObjectsOfType<UniqueIDGenerator>(true);
        var used = new HashSet<string>();

        foreach (var uid in all)
        {
            if (string.IsNullOrEmpty(uid.ID) || used.Contains(uid.ID))
            {
                typeof(UniqueIDGenerator)
                    .GetField("id", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.SetValue(uid, System.Guid.NewGuid().ToString());

                EditorUtility.SetDirty(uid);
            }   

            used.Add(uid.ID);
        }

        Debug.Log("Duplicate UniqueIDs fixed.");
    }
}
#endif