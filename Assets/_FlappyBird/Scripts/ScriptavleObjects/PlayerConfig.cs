using UnityEngine;

namespace FlappyBird
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField, Range(0.1f, 30)] public float ForceBounce { get; private set; }
    }
}
