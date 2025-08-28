using UnityEngine;

[CreateAssetMenu(fileName = "New Collectible", menuName = "Scriptable Objects/Collectible Data")]
public class CollectibleData : ScriptableObject
{   
    public int collectibleId;
    public bool isHealItem;
    public GameObject prefab;
    public AudioClip sound;
    public int scoreValue;
}
