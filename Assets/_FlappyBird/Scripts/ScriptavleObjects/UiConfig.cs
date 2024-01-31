using UnityEngine;

namespace FlappyBird
{
    [CreateAssetMenu(fileName = "UiConfig", menuName = "Configs/UiConfig")]
    public class UiConfig : ScriptableObject
    {
        [field: SerializeField, Range(0, 5)] public float TimeFadingGetReady { get; private set; }
        [field: SerializeField, Range(0.01f, 5)] public float TimeBlackout { get; private set; }
    }
}
