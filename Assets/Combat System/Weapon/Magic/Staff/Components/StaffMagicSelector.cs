using UnityEngine;
using Zenject;

[RequireComponent(typeof(Staff))]
public class StaffMagicSelector : MonoBehaviour
{
    private IInputProvider inputProvider;
    private Staff staff;
    
    public Magic CurrentMagic { get; private set; }
    
    [SerializeField] private Magic firstMagicSlot;
    [SerializeField] private Magic secondMagicSlot;

    private int chosenSpellIndex;
    public int ChosenSpellIndex
    {
        get => chosenSpellIndex;
        set
        {
            if (value > CurrentMagic.Spells.Count - 1 || value < 0)
                return;
            
            chosenSpellIndex = value;
        }
    }

    [Inject]
    private void Construct([InjectOptional] IInputProvider input)
    {
        inputProvider = input;
    }

    private void Awake()
    {
        CurrentMagic = firstMagicSlot;
    }

    public void InitializeComponent()
    {
        staff = GetComponent<Staff>();

        if (inputProvider != null)
        {
            inputProvider.ButtonsController.MagicInput.OnSpellSwap += SwapChosenSpell;
            inputProvider.ButtonsController.MagicInput.OnMagicSwap += SwapCurrentMagic;
        }
    }

    public void FinalizeComponent()
    {
        staff = null;

        if (inputProvider != null)
        {
            inputProvider.ButtonsController.MagicInput.OnSpellSwap -= SwapChosenSpell;
            inputProvider.ButtonsController.MagicInput.OnMagicSwap -= SwapCurrentMagic;
        }
    }
    
    private void SwapChosenSpell()
    {
        if (staff.GetCastComponent().IsCharging)
            return;

        if (chosenSpellIndex + 1 < CurrentMagic.Spells.Count)
            chosenSpellIndex++;
        else
            chosenSpellIndex = 0;
    }

    private void SwapCurrentMagic()
    {
        if (staff.GetCastComponent().IsCharging)
            return;

        CurrentMagic = CurrentMagic == firstMagicSlot ? secondMagicSlot : firstMagicSlot;
        chosenSpellIndex = 0;
    }
}