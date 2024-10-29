using UnityEngine;

public class WeaponVisualBase : MonoBehaviour
{
    protected Animator Animator;

    protected Quaternion InitialRotation;
    protected Vector3 InitialPosition;

    protected void InitiateFields()
    {
        InitialRotation = transform.localRotation;
        InitialPosition = transform.localPosition;

        Animator = GetComponent<Animator>();
    }
}
