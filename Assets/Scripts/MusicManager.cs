
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Song List")]
    public SongData[] songList;

    [Header("Demo Preview")]
    public AudioSource demoSource;

    [Header("Demo Settings")]
    public float demoDuration = 10f;
    public float demoFadeOutTime = 2f;

    public SongData selectedSong;
    public static SongData SelectedSong => Instance != null ? Instance.selectedSong : null;

    private SongCardUI currentDemoCard;
    private Coroutine demoRoutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (demoSource != null)
        {
            demoSource.loop = false;
            demoSource.playOnAwake = false;
        }
    }

    // ---------------- SONG SELECTION ----------------
    public void SelectMusic(int index)
    {
        StopDemo(); // 🔴 IMPORTANT: stop demo before entering game

        if (index < 0 || index >= songList.Length)
        {
            Debug.LogWarning("Invalid song index selected");
            return;
        }

        selectedSong = songList[index];
        SceneManager.LoadScene("SampleScene");
    }

    // ---------------- DEMO SYSTEM ----------------
    public void PlayDemo(AudioClip clip, SongCardUI card)
    {
        StopDemo(); // stop previous demo safely

        currentDemoCard = card;
        demoRoutine = StartCoroutine(DemoRoutine(clip));
    }

    private IEnumerator DemoRoutine(AudioClip clip)
    {
        demoSource.clip = clip;
        demoSource.volume = 1f;
        demoSource.Play();

        float playTime = Mathf.Max(0f, demoDuration - demoFadeOutTime);
        yield return new WaitForSeconds(playTime);

        // Fade out
        float t = 0f;
        float startVol = demoSource.volume;

        while (t < demoFadeOutTime)
        {
            t += Time.deltaTime;
            demoSource.volume = Mathf.Lerp(startVol, 0f, t / demoFadeOutTime);
            yield return null;
        }

        demoSource.Stop();
        demoSource.volume = 1f;

        if (currentDemoCard != null)
        {
            currentDemoCard.ForceStopDemo();
            currentDemoCard = null;
        }

        demoRoutine = null;
    }

    public void StopDemo()
    {
        if (demoRoutine != null)
        {
            StopCoroutine(demoRoutine);
            demoRoutine = null;
        }

        if (demoSource.isPlaying)
            demoSource.Stop();

        demoSource.volume = 1f;

        if (currentDemoCard != null)
        {
            currentDemoCard.ForceStopDemo();
            currentDemoCard = null;
        }
    }

    public void ResetMusicSelection()
    {
        selectedSong = null;
    }
}
