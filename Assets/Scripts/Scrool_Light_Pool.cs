using UnityEngine;
using UnityEngine.Pool;


public class Scrool_Light_Pool : MonoBehaviour
{
    public ObjectPool<GameObject> pool;
    [SerializeField, Header("プレハブ")] private GameObject Light_prefab;
    [SerializeField,Header("打てる最大数")] int shot_max;

    private void Start()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: OnCreateObject,//第1関数：プールにオブジェクトがない場合オブジェクト生成(Instantiate)する
            actionOnGet: OnGetObject,//第2関数：プールに使用していないオブジェクトがある場合はプールから出す。SetActive(true)する
            actionOnRelease: OnReturnedObject,//第3関数：プールに返却する
            actionOnDestroy: OnDestroyObject,//第4関数：プールの許容量を超えた時にオブジェクトを削除する
            collectionCheck: false,//既にプールにあるオブジェトを追加した場合に例外とするか。エディタでのみ実行される
            defaultCapacity: shot_max,//初期のプールサイズ
            maxSize: shot_max//最大プールサイズ
            );
    }

    GameObject OnCreateObject()
    {
        GameObject obj = Instantiate(Light_prefab);
        obj.GetComponent<Scroll_Light_Move>().SetFunc(() => { pool.Release(obj); });
        return obj;
    }

    void OnGetObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    void OnReturnedObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    void OnDestroyObject(GameObject obj)
    {
        Destroy(obj);
    }
}
