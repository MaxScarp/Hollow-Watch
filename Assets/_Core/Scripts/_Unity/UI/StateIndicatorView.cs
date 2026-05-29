using TMPro;
using UnityEngine;

public class StateIndicatorView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private string textAlertLabel = "!";
    [SerializeField] private string textChaseLabel = "!!";
    [SerializeField] private Color colorAlert = Color.yellow;
    [SerializeField] private Color colorChase = Color.red;

    private void OnEnable()
    {
        GameEventBus.Subscribe<AIStateChangedEvent>(OnAIStateChanged);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe<AIStateChangedEvent>(OnAIStateChanged);
    }

    private void OnAIStateChanged(AIStateChangedEvent e)
    {
        switch (e.NewState)
        {
            case CreatureState.Patrol:
                label.SetText(string.Empty);
                break;
            case CreatureState.Alert:
                label.SetText(textAlertLabel);
                label.color = colorAlert;
                break;
            case CreatureState.Chase:
                label.SetText(textChaseLabel);
                label.color = colorChase;
                break;
        }
    }
}
