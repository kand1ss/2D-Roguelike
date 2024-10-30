using UnityEngine;

[RequireComponent(typeof(Staff))]
public class StaffMagicSelector : MonoBehaviour
{
    private Staff staff;
    
    private Magic currentMagic;
    public Magic CurrentMagic => currentMagic;
    
    [SerializeField] private Magic firstMagicSlot;
    [SerializeField] private Magic secondMagicSlot;

    private int chosenSpellIndex;
    public int ChosenSpellIndex => chosenSpellIndex;

    private void Awake()
    {
        staff = GetComponent<Staff>();
        
        currentMagic = firstMagicSlot;
    }

    public void InitializeComponent()
    {
        InputService.ButtonsController.MagicInput.OnSpellSwap += SwapChosenSpell;
        InputService.ButtonsController.MagicInput.OnMagicSwap += SwapCurrentMagic;
    }

    public void FinalizeComponent()
    {
        InputService.ButtonsController.MagicInput.OnSpellSwap -= SwapChosenSpell;
        InputService.ButtonsController.MagicInput.OnMagicSwap -= SwapCurrentMagic;
    }
    
    private void SwapChosenSpell()
    {
        if (staff.GetCastComponent().IsCharging)
            return;

        if (chosenSpellIndex + 1 < currentMagic.Spells.Count)
            chosenSpellIndex++;
        else
            chosenSpellIndex = 0;

        Debug.Log("Spell Swap");
    }

    private void SwapCurrentMagic()
    {
        if (staff.GetCastComponent().IsCharging)
            return;

        currentMagic = currentMagic == firstMagicSlot ? secondMagicSlot : firstMagicSlot;
        chosenSpellIndex = 0;

        Debug.Log($"Magic Set: {currentMagic.GetType().Name}");
    }
}