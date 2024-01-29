using UnityEngine;

namespace Assets._FlappyBird
{
    [CreateAssetMenu(fileName = "ActivationPointConfig", menuName = "Configs/SettingObstaclesConfig")]
    public class SettingObstaclesConfig : ScriptableObject
    {
        [field: SerializeField, Range(1, 50)] public float HorizontalDistance { get; private set; }
    }
}
