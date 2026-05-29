using UnityEngine;

public class PlayingScreenView : MonoBehaviour
{
    private void OnEnable()
    {
        GameEventBus.Subscribe<GameStateChangedEvent>(OnGameStateChanged);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe<GameStateChangedEvent>(OnGameStateChanged);
    }

    private void OnGameStateChanged(GameStateChangedEvent e)
    {
        gameObject.SetActive(e.NewState == GameState.Playing);
    }
}
