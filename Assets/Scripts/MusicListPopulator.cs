
using UnityEngine;

public class MusicListPopulator : MonoBehaviour
{
    public RectTransform contentParent;
    public GameObject songCardPrefab;

    void Start()
    {
        Populate();
    }

    public void Populate()
    {
        if (MusicManager.Instance == null) return;
        if (MusicManager.Instance.songList == null) return;

        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        SongData[] songs = MusicManager.Instance.songList;

        for (int i = 0; i < songs.Length; i++)
        {
            GameObject go = Instantiate(songCardPrefab, contentParent);
            SongCardUI card = go.GetComponent<SongCardUI>();

            if (card != null)
            {
                card.Setup(songs[i], i);
            }
        }
    }
}
