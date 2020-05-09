using Densyakun.CSWave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class Main : MonoBehaviour
{

    public const float LINE_WIDTH = 0.01f;

    public static string waveFilePath;

    // Inspector
    public AudioSource audioSource;
    public Material lineMat;

    [NonSerialized]
    public Wave wave;
    [NonSerialized]
    public AudioClip clip;
    [NonSerialized]
    public List<LineRenderer> waveRenderers;

    void Start()
    {
        waveFilePath = Path.Combine(Application.persistentDataPath, "test.wav");
        waveRenderers = new List<LineRenderer>();

        wave = new Wave((ushort)2, (uint)44100, 1d);
        wave.generateTriangle(440d);

        wave.saveFile(waveFilePath);
        StartCoroutine(GetAudioClip(waveFilePath));
    }

    void Update()
    {
        if (waveRenderers.Count == 0 && clip != null)
        {
            for (var n1 = 0; n1 < clip.channels; n1++)
            {
                var r = new GameObject().AddComponent<LineRenderer>();
                waveRenderers.Add(r);

                var p = new Vector3[(int)Mathf.Min(wave.wave.Length, wave.samplerate + 1, Screen.width + 1)];
                var l = (int)Mathf.Min(wave.wave.Length, wave.samplerate, Screen.width);
                var a = (float)Screen.width / Screen.height;
                for (var n = 0; n < (l < wave.wave.Length ? l + 1 : l); n++)
                    p[n] = new Vector3(2f * a * (float)n / l - a,
                        (float)wave.wave[Mathf.RoundToInt((float)n * wave.samplerate / l)] / wave.channels +
                        2f * n1 / wave.channels + 1f / wave.channels - 1f, 0f);
                if (p[p.Length - 1] == null)
                    p[p.Length - 1] = new Vector3(2f * a - a,
                            2f * n1 / wave.channels + 1f / wave.channels - 1f, 0f);

                r.positionCount = p.Length;
                r.SetPositions(p);
                r.endWidth = r.startWidth = LINE_WIDTH;
                r.material = lineMat;
            }
        }
    }

    public IEnumerator GetAudioClip(string path)
    {
        clip = null;
        for (var n = 0; n < waveRenderers.Count; n++)
        {
            Destroy(waveRenderers[n].gameObject);
            waveRenderers.RemoveAt(n);
        }
        using (var request = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV))
        {
            yield return request.SendWebRequest();
            clip = DownloadHandlerAudioClip.GetContent(request);
        }
    }

    public void playButton()
    {
        audioSource.PlayOneShot(clip);
    }
}
