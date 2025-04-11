using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction SoulCoreCollected;
    public static void OnSoulCoreCollected() => SoulCoreCollected?.Invoke();

    public static event UnityAction<int> KeyCollected;
    public static void OnKeyCollected(int keyID) => KeyCollected?.Invoke(keyID);

    public static event UnityAction RecallOrbCollected;

    public static void OnRecallOrbCollected() => RecallOrbCollected?.Invoke();
}
