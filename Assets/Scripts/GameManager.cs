using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("設定")]
    [SerializeField] private GameObject Tira;
    [SerializeField] private Transform insTransform;

    private void Awake()
    {
        if (!Tira) {
            Debug.LogError("Tira Not Set");
        }
        if (!insTransform) {
            Debug.LogError("insTransform Not Set");
        }
    }

    private void Start() {
        Sporn();
    }

    public void Sporn() {
        Instantiate(Tira, insTransform);
    }
}