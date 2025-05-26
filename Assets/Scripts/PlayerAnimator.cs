using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;

    private Animator animator;
    private const string IS_WALKING = "IsWalking";

    protected void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
