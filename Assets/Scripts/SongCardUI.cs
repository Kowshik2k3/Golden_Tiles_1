

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SongCardUI : MonoBehaviour
{
    [Header("UI")]
    public Image artworkImage;
    public TextMeshProUGUI songNameText;

    [Header("Stars (3)")]
    public GameObject[] stars;

    [Header("Demo UI")]
    public Button demoButton;
    public Image playIcon;
    public Image discIcon;
    public float discRotationSpeed = 180f;

    [Header("Play Button")]
    public Button playButton;

    private SongData songData;
    private int songIndex;
    private bool demoPlaying;

    void Update()
    {
        if (demoPlaying && discIcon != null)
        {
            discIcon.transform.Rotate(0f, 0f, -discRotationSpeed * Time.deltaTime);
        }
    }

    public void Setup(SongData data, int index)
    {
        songData = data;
        songIndex = index;

        artworkImage.sprite = data.artwork;
        songNameText.text = data.songName;

        LoadStars();

        playIcon.gameObject.SetActive(true);
        discIcon.gameObject.SetActive(false);

        demoButton.onClick.RemoveAllListeners();
        demoButton.onClick.AddListener(ToggleDemo);

        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(PlayFullSong);
    }

    // ---------------- STARS ----------------
    private void LoadStars()
    {
        foreach (var star in stars)
            star.SetActive(false);

        if (string.IsNullOrEmpty(songData.songId)) return;

        string key = "SONG_" + songData.songId + "_STARS";
        int savedStars = PlayerPrefs.GetInt(key, 0);

        for (int i = 0; i < savedStars && i < stars.Length; i++)
            stars[i].SetActive(true);
    }

    // ---------------- DEMO ----------------
    void ToggleDemo()
    {
        if (demoPlaying) StopDemo();
        else PlayDemo();
    }

    void PlayDemo()
    {
        demoPlaying = true;
        playIcon.gameObject.SetActive(false);
        discIcon.gameObject.SetActive(true);

        MusicManager.Instance.PlayDemo(songData.demoClip, this);
    }

    void StopDemo()
    {
        demoPlaying = false;
        playIcon.gameObject.SetActive(true);
        discIcon.gameObject.SetActive(false);

        MusicManager.Instance.StopDemo();
    }

    public void ForceStopDemo()
    {
        demoPlaying = false;
        playIcon.gameObject.SetActive(true);
        discIcon.gameObject.SetActive(false);
    }

    // ---------------- MAIN PLAY ----------------
    void PlayFullSong()
    {
        MusicManager.Instance.StopDemo(); // 🔴 always stop demo
        MusicManager.Instance.SelectMusic(songIndex);
    }
}
