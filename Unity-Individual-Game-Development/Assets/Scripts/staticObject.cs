using UnityEngine;

public class staticObjectBase : MonoBehaviour { };
public class staticObject<T> : staticObjectBase where T : MonoBehaviour
{
    public static T Instance_;

    protected virtual void Awake()
    {
        if(Instance_ != null && !ThisInstance()) {
            Destroy(gameObject);
            return;
        }

        Instance_ = this as T;
        DontDestroyOnLoad(gameObject);
    }

    protected bool ThisInstance() {
        return Instance_ == this as T;
    }
};