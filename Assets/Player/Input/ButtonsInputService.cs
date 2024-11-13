using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonsInputService : IDisposable
{
    public WeaponInput WeaponInput { get; private set; }
    public MagicInput MagicInput { get; private set; }
    public ActionInput ActionInput { get; private set; }

    public ButtonsInputService(InputSystem playerInput)
    {
        WeaponInput = new WeaponInput(playerInput);
        MagicInput = new MagicInput(playerInput);
        ActionInput = new ActionInput(playerInput);
        
        WeaponInput.InitializeEvents();
        MagicInput.InitializeEvents();
        ActionInput.InitializeEvents();
    }

    public void Dispose()
    {
        WeaponInput.FinalizeEvents();
        MagicInput.FinalizeEvents();
        ActionInput.FinalizeEvents();
    }
}