using UnityEngine;
using Zenject;

public class PlayerVisual : MonoBehaviour
{
    private Player player;
    
    private const string RUN = "Run";
    
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [Inject]
    private void Construct(Player player)
    {
        this.player = player;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckFacingDirection();
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        animator.SetBool(RUN, player.PlayerState == PlayerState.Run);
    }

    private void CheckFacingDirection()
    {
        var cursorPosition = CoordinateManager.GetCursorPositionInWorldPoint();

        spriteRenderer.flipX = cursorPosition.x < transform.position.x;
    }
}