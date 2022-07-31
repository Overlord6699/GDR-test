using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "ScriptableValue")]
public class ChangeableValue<T> : ScriptableObject
{
    [SerializeField]
    private T currentValue;

    public UnityEvent<T> OnValueChanged;

    public virtual T Value
    {
        get => currentValue;
        set
        {
            currentValue = value;
            OnValueChanged?.Invoke(currentValue);
        }
    }
}
