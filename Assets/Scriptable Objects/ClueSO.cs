using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "ClueSO", menuName = "Scriptable Objects/ClueSO")]
public class ClueSO : ScriptableObject
{
    [Tooltip("0 = normal clue, 1 = haiku, 2 = song lyrics, 3 = image")]
    [SerializeField] int clueType;

    [Header("0: Normal Clue")]
    [TextArea(3, 6)]
    [SerializeField] string clueText;

    [Header("1: Haiku Clue")]
    [SerializeField] string[] haikuClueText;

    [Header("2: Song Lyric Clue")]
    [SerializeField] string[] songLyricText;

    [Header("3: Image Clue")]
    [SerializeField] GameObject image;

    [Header("Button Number")]
    [SerializeField] int buttonNumber;

    public int GetClueType() { return clueType; }

    public string GetClueText() { return clueText; }

    public string GetHaikuClueText(int index) { return haikuClueText[index]; }

    public string GetSongLyricText(int index) { return songLyricText[index]; }

    public GameObject GetImage() { return image; }

    public int GetButtonNumber() { return buttonNumber; }
}
