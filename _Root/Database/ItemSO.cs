
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemSO", order = 1)]
public class ItemSO : ScriptableObject
{
    private string guid;
    public string Name;
    public int BaseSalePrice;
    public bool IsConsumable;
    public bool IsKey;
    public Sprite Sprite;
    public string Comments;

    public void SetGUID(string guid)
    {
        this.guid = guid;
    }
}