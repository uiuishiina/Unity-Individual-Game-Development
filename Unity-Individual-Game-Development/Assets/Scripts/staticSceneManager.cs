using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class staticSceneManager : MonoBehaviour
{
    public static staticSceneManager Instance_;

    private void Awake() {
        if (Instance_ != null) {
            Destroy(gameObject);
            return;
        }
        Instance_ = this;
        DontDestroyOnLoad(gameObject);
    }

    public void MoveScene(string name) {
        SceneManager.LoadScene(name);
    }

    public void AddOnSceneLoaded(UnityAction<Scene,LoadSceneMode> func) {
        SceneManager.sceneLoaded += func;
    }

    public void RemoveOnSceneLoaded(UnityAction<Scene, LoadSceneMode> func) {
        SceneManager.sceneLoaded -= func;
    }
}
