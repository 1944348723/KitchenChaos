using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;

    private const string IS_WALKING = "IsWalking";
    private Animator animator;

    private void Awake() {
        this.animator = GetComponent<Animator>();
    }

    private void Update() {
        this.animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
