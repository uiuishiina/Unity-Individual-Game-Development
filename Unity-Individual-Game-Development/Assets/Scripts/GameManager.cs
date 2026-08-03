using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //  input
    staticInput staticInput_;

    InputAction ApaceAction_;
    InputAction EscAction_;

    bool is_pauce = false;

    //  Boll
    [SerializeField] private GameObject Boll_;

    //  UI
    [SerializeField] private GameObject PaucePanel_;

    private void Start() {
        PaucePanel_.SetActive(false);
    }

    private void OnEnable() {
        staticInput_ = staticInput.Instance_;
        if (DebugUtility.IsNull(staticInput_)) {
            DebugUtility.ErrorLog("staticInput NotFound");
        }

        ApaceAction_ = staticInput_.GetInputMap(InputMapNames.Player).FindAction("Jump");
        EscAction_ = staticInput_.GetInputMap(InputMapNames.Global).FindAction("Esc");

        ApaceAction_.performed += OnSpace;
        EscAction_.performed += OnEsc;
    }

    private void OnDisable() {
        ApaceAction_.performed -= OnSpace;
        EscAction_.performed -= OnEsc;
    }

    private void OnEsc(InputAction.CallbackContext context) {
        is_pauce = !is_pauce;
        Time.timeScale = is_pauce ? 0 : 1;
        PaucePanel_.SetActive(is_pauce);

        var map = is_pauce ? InputMapNames.UI : InputMapNames.Player;
        staticInput_.ChangeInputMap(map);
    }

    private void OnSpace(InputAction.CallbackContext context) {
        Boll_.gameObject.SetActive(true);
        Boll_.transform.position = new Vector3(0, 3, 0);
        var brb = Boll_.GetComponent<Rigidbody>();
        brb.linearVelocity = Vector3.zero;
        brb.angularVelocity = Vector3.zero;
        brb.AddForce(new Vector3(0, 0, 2.5f),ForceMode.Impulse);
    }
}
