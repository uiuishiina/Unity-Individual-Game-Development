using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    staticInput staticInput_;

    InputAction MoveAction_;
    InputAction AttackAction_;

    Rigidbody rb_;

    [SerializeField] private float speed_ = 1;
    [SerializeField] private GameObject Racket_;
    [SerializeField] private GameObject Hit_;
    Sequence AttackSequence_;

    private void Awake() {
        rb_ = GetComponent<Rigidbody>();
    }
    private void Start() {
        Hit_.SetActive(false);
        Racket_.SetActive(false);
    }
    private void OnEnable() {
        staticInput_ = staticInput.Instance_;
        if (DebugUtility.IsNull(staticInput_)) {
            DebugUtility.ErrorLog("staticInput NotFound");
        }

        MoveAction_ = staticInput_.GetInputMap(InputMapNames.Player).FindAction("Move");
        AttackAction_ = staticInput_.GetInputMap(InputMapNames.Player).FindAction("Attack");

        AttackAction_.performed += OnAttack;
    }
    private void OnDisable() {
        AttackAction_.performed -= OnAttack;
    }

    private void OnAttack(InputAction.CallbackContext context) {
        AttackSequence_?.Kill(true);
        AttackSequence_ = AttackSequence();
        AttackSequence_.Play();
    }

    private Sequence AttackSequence()
    {
        var seq = DOTween.Sequence();
        seq.AppendCallback(() => { 
            Racket_.SetActive(true);
            Racket_.transform.rotation = Quaternion.Euler(0, 0, 0);
        });
        seq.Join(Racket_.transform.DORotate(new Vector3(0, -180, 0), 0.5f));
        var subTween = DOTween.Sequence().AppendCallback(() =>
        {
            Hit_.SetActive(true);
        }).
        AppendInterval(0.2f).
        OnComplete(() =>
        {
            Hit_.SetActive(false);
        }).
        OnKill(() => 
        {
            Hit_.SetActive(false);
        });

        seq.Join(subTween);
        seq.OnComplete(() => {
            Racket_.SetActive(false);
            Racket_.transform.rotation = Quaternion.Euler(0, 0, 0);
        });
        seq.Pause();

        return seq;
    }

    private void FixedUpdate() {

        var inputVec_ = MoveAction_.ReadValue<Vector2>();

        var MoveVec = new Vector3(inputVec_.x * speed_, rb_.linearVelocity.y, inputVec_.y * speed_);
        rb_.linearVelocity = MoveVec;
    }
}
