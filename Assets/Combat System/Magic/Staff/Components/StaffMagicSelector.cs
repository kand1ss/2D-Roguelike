using UnityEngine;
using Zenject;

[RequireComponent(typeof(Staff))]
public class StaffMagicSelector : MonoBehaviour
{
    private IInputProvider inputProvider;
    
    private Staff staff;
    
    private Magic currentMagic;
    public Magic CurrentMagic => currentMagic;
    
    [SerializeField] private Magic firstMagicSlot;
    [SerializeField] private Magic secondMagicSlot;

    private int chosenSpellIndex;
    public int ChosenSpellIndex => chosenSpellIndex;

    [Inject]
    private void Construct(IInputProvider input)
    {
        inputProvider = input;
    }

    private void Awake()
    {
        staff = GetComponent<Staff>();
        
        currentMagic = firstMagicSlot;
    }

    public void InitializeComponent()
    {
        inputProvider.ButtonsController.MagicInput.OnSpellSwap += SwapChosenSpell;
        inputProvider.ButtonsController.MagicInput.OnMagicSwap += SwapCurrentMagic;
    }

    public void FinalizeComponent()
    {
        inputProvider.ButtonsController.MagicInput.OnSpellSwap -= SwapChosenSpell;
        inputProvider.ButtonsController.MagicInput.OnMagicSwap -= SwapCurrentMagic;
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