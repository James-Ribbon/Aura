using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction SoulCollected;
    public static void OnSoulCollected() => SoulCollected?.Invoke();

    public static event UnityAction<int> KeyCollected;
    public static void OnKeyCollected(int keyID) => KeyCollected?.Invoke(keyID);
}
