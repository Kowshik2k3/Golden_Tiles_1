
using UnityEngine;

[CreateAssetMenu(menuName = "Music/Song Data")]
public class SongData : ScriptableObject
{
    [Header("Identity (DO NOT CHANGE ONCE SET)")]
    public string songId;

    [Header("Display")]
    public string songName;
    public Sprite artwork;

    [Header("Audio")]
    public AudioClip demoClip;
    public AudioClip fullClip;
}
