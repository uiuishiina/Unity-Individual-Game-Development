using System.Collections.Generic;
using UnityEngine;

public class staticObjectFactory : MonoBehaviour
{
    [SerializeField] private staticSceneManager SceneManagerPrefab_;
    [SerializeField] private List<staticObjectBase> ObjectList_ = new();

    private void Awake() {
        if (DebugUtility.IsNull(staticSceneManager.Instance_)) {
            Instantiate(SceneManagerPrefab_);
        }
        foreach(var obj in ObjectList_) {
            if (DebugUtility.IsNull(FindFirstObjectByType(obj.GetType()))) {
                Instantiate(obj);
            }
        }
    }
}
