
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ActorSO", order = 1)]
public class ActorSO : ScriptableObject
{
    [HideInInspector]
    public string GUID;
    public string Name;
    public enum Type { NPC, Playable }
    public Type ActorType;
    public Sprite Sprite;
    public string Comments;

    [HideInInspector]
    public string DateCreated;

}