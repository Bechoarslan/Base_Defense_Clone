using UnityEngine;

[DisallowMultipleComponent]
public class UniqueIDGenerator : MonoBehaviour
{
    [SerializeField, HideInInspector]
    private string id;

    public string ID => id;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!Application.isPlaying && string.IsNullOrEmpty(id))
        {
            id = System.Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
#endif
}