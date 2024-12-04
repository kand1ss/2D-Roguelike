using System;
using UnityEngine;
using Zenject;

public class PotionPlaceholder : MonoBehaviour
{
    private IPotionUser potionUser;

    [SerializeField] private GameObject pivot;
    
    private SpriteRenderer spriteRenderer;

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
        
        bool isFacingRight = !(cursorPosition.x < transform.position.x);

        var rotation = pivot.transform.rotation;
        rotation.y = isFacingRight ? 0 : 180;
        
        pivot.transform.rotation = rotation;

        spriteRenderer.flipX = isFacingRight;
    }

    private void ShowPotionView(IPotion potion)
    {
        Sprite potionSprite = null;
        if(potion != null)
            potionSprite = potion.PotionSprite;

        spriteRenderer.sprite = potionSprite;
    }
}