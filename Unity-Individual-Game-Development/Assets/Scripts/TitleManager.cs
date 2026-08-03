using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    //  Input
    staticInput staticInput_;
    InputAction EscAction_;
    InputAction SpaceAction_;
    InputAction AnyAction_;

    //  DoTween
    Sequence StartSequence_;
    Sequence FadeSceneSequence_;

    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject NaviText_;
    private GameObject copy;

    bool end_start_sequence = false;
    bool is_fade_start = false;

    private void OnEnable() {
        staticInput_ = staticInput.Instance_;
        if (DebugUtility.IsNull(staticInput_)) {
            DebugUtility.ErrorLog("staticInput NotFound");
        }
        EscAction_ = staticInput_.GetInputMap(InputMapNames.Global).FindAction("Esc");
        SpaceAction_ = staticInput_.GetInputMap(InputMapNames.Global).FindAction("Space");
        AnyAction_ = staticInput_.GetInputMap(InputMapNames.Global).FindAction("Any");

        EscAction_.performed += OnEsc;
        SpaceAction_.performed += OnSpace;
        AnyAction_.performed += OnAny;
    }

    private void OnDisable() {
        EscAction_.performed -= OnEsc;
        SpaceAction_.performed -= OnSpace;
        AnyAction_.performed -= OnAny;
    }

    private void OnEsc(InputAction.CallbackContext context) {
        DebugUtility.Log("Esc");
    }
    private void OnSpace(InputAction.CallbackContext context) {
        DebugUtility.Log("Space");
        if (end_start_sequence && !is_fade_start) {
            FadeSceneSequence_.Play();
            is_fade_start = true;
        }
    }

    private void OnAny(InputAction.CallbackContext context) {
        StartSequence_?.Kill(true);
    }

    private void Start() {

        copy = Instantiate(NaviText_, NaviText_.transform.parent);
        copy.SetActive(false);
        NaviText_.GetComponent<Text>().text = "";

        StartSequence_ = DOTween.Sequence();
        StartSequence_.Append(NaviText_.GetComponent<Text>().DOText("Push to Space", 1.0f).SetDelay(1.5f));
        StartSequence_.OnComplete(() => { end_start_sequence = true; });
        StartSequence_.Play();

        FadeSceneSequence_= DOTween.Sequence();
        FadeSceneSequence_.AppendCallback(() => { 
            copy.SetActive(true);
            NaviText_.SetActive(false);
        });
        FadeSceneSequence_.Append(copy.transform.DOScale(new Vector3(2f, 1.5f, 1), 1));
        FadeSceneSequence_.Join(copy.GetComponent<Text>().DOFade(0, 1.2f));
        FadeSceneSequence_.AppendInterval(0.3f);
        FadeSceneSequence_.OnComplete(() => {
            staticSceneManager.Instance_.MoveScene("GameScene");
        });
        FadeSceneSequence_.Pause();
    }
}
