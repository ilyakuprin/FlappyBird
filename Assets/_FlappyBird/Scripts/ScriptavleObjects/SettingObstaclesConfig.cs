using UnityEngine;

namespace FlappyBird
{
    [CreateAssetMenu(fileName = "ActivationPointConfig", menuName = "Configs/SettingObstaclesConfig")]
    public class SettingObstaclesConfig : ScriptableObject
    {
        [field: SerializeField, Range(1, 50)] public float HorizontalDistance { get; private set; }
        [field: SerializeField, Range(1, 50)] public float Speed { get; private set; }
        [field: SerializeField] public float UpperPointY { get; private set; }
        [field: SerializeField] public float LowerPointY { get; private set; }

        public void OnValidate()
        {
            if (UpperPointY < LowerPointY)
            {
                UpperPointY = LowerPointY;
            }
        }
    }
}
