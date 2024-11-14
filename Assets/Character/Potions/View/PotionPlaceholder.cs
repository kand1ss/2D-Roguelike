using System;
using UnityEngine;
using Zenject;

public class PotionPlaceholder : MonoBehaviour
{
    private IPotionUser potionUser;
    
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject pivot;

    [Inject]
    private void Construct(IPotionUser potionUser)
    {
        this.potionUser = potionUser;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        potionUser.PotionManager.PotionChanged += ShowPotionView;
    }

    private void Update()
    {
        CheckFacingDirection();
    }
    
    private void CheckFacingDirection()
    {
        var cursorPosition = CoordinateManager.GetCursorPositionInWorldPoint();

        var rotation = pivot.transform.rotation;
        rotation.y = cursorPosition.x < transform.position.x ? 180 : 0;
        pivot.transform.rotation = rotation;

        spriteRenderer.flipX = cursorPosition.x < transform.position.x;
    }

    private void ShowPotionView(IPotion potion)
    {
        spriteRenderer.sprite = potion.PotionSprite;
    }
}