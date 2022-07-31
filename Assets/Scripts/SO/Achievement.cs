using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Achievement")]
public class Achievement<T> : ScriptableObject
{
    public UnityEvent<T> OnAchievementComplete;

    public ChangeableValue<T> Target = null;

    public T Goal;

    public void Init()
    {
        Target.OnValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(T newValue)
    {
        if (Target.Value.Equals(Goal))
        {
            OnAchievementComplete?.Invoke(Target.Value);
        }
    }
}
