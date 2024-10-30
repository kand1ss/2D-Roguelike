using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonsInputService : MonoBehaviour
{
    public WeaponInput WeaponInput { get; private set; }
    public MagicInput MagicInput { get; private set; }

    private void Awake()
    {
        WeaponInput = new WeaponInput(InputService.Instance.PlayerInput);
        MagicInput = new MagicInput(InputService.Instance.PlayerInput);
    }

    private void Start()
    {
        WeaponInput.InitializeEvents();
        MagicInput.InitializeEvents();
    }

    private void OnDestroy()
    {
        WeaponInput.FinalizeEvents();
        MagicInput.FinalizeEvents();
    }

    private void Update()
    {
        WeaponInput.Update();
    }
}