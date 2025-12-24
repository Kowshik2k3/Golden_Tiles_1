using UnityEngine;

public class StarResetter : MonoBehaviour
{
    [Header("TESTING ONLY")]
    public SongData[] allSongs;

    [ContextMenu("RESET ALL SONG STARS")]
    public void ResetAllSongStars()
    {
        foreach (SongData song in allSongs)
        {
            if (song == null || string.IsNullOrEmpty(song.songId))
                continue;

            string key = "SONG_" + song.songId + "_STARS";
            PlayerPrefs.DeleteKey(key);
        }

        PlayerPrefs.Save();
        Debug.Log("✅ All song stars reset successfully");
    }
}
