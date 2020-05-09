using Densyakun.CSWave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Main : MonoBehaviour
{

    public const float LINE_WIDTH = 0.01f;

    // Inspector
    public AudioSource audioSource;
    public Material lineMat;
    public Text speedText;
    public Text mcText;
    public Slider mcSlider;
    public InputField brakeInput;
    public InputField powerInput;
    public InputField decelerationInput;
    public InputField accelerationInput;

    private float speed = 0f;
    private int brake = 1;
    private int power = 1;
    private float deceleration = 0f;
    private float acceleration = 0f;

    private List<Wave> waves;
    private List<LineRenderer> waveRenderers;
    private int wn;

    void Start()
    {
        // 初期化
        waveRenderers = new List<LineRenderer>();
        brakeInput.text = "7";
        powerInput.text = "4";
        decelerationInput.text = "3.5";
        accelerationInput.text = "3.3";
        setButton();

        // 波形生成、保存
        waves = new List<Wave>();
        for (var a = 0; a < 4; a++)
        {
            var w = new Wave((ushort)1, (uint)44100, 60d);
            w.set(1d);
            w.chop(55d * Mathf.Pow(2, a + 1), 0.1d);
            waves.Add(w);
            w.saveFile(getWaveFilePath(a));
        }
    }

    void Update()
    {
        // 速度の変更
        if (mcSlider.value < 0)
            speed = Mathf.Max(0f, speed + mcSlider.value * deceleration * Time.deltaTime / brake);
        else
            speed += mcSlider.value * acceleration * Time.deltaTime / power;

        // UI
        speedText.text = speed.ToString("F2") + " km/h";
        mcText.text = mcSlider.value == 0 ? "N" : mcSlider.value > 0 ? "P" + mcSlider.value : "B" + -mcSlider.value;

        // チョッパ制御
        var wn_ = wn;
        if (mcSlider.value > 0)
        {
            wn = Mathf.Min(3, Mathf.FloorToInt(speed) - 1);
            if (wn >= 0)
            {
                if (wn != wn_ && waves.Count > wn)
                    StartCoroutine(GetAudioClip(getWaveFilePath(wn)));
                audioSource.pitch = 1f;
            }
            else
                audioSource.pitch = 0f;
        }
        else if (mcSlider.value < 0)
        {
            if (speed >= 1f)
            {
                wn = 3;
                if (wn != wn_ && waves.Count > wn)
                    StartCoroutine(GetAudioClip(getWaveFilePath(wn)));
                audioSource.pitch = 1f;
            }
            else
                audioSource.pitch = 0f;
        }
        else
            audioSource.pitch = 0f;

        // 表示している波形を初期化
        foreach (var r in waveRenderers)
            Destroy(r.gameObject);
        waveRenderers.Clear();

        // 波形表示
        if (audioSource.isPlaying && audioSource.pitch != 0f)
        {
            for (var n1 = 0; n1 < waves[wn].channels; n1++)
            {
                try
                {
                    var r = new GameObject().AddComponent<LineRenderer>();
                    waveRenderers.Add(r);

                    var p = new Vector3[(int)Mathf.Min(waves[wn].wave.Length * audioSource.pitch, waves[wn].samplerate + 1, Screen.width + 1)];
                    var l = (int)Mathf.Min(waves[wn].wave.Length * audioSource.pitch, waves[wn].samplerate, Screen.width);
                    var a = (float)Screen.width / Screen.height;
                    for (var n = 0; n < p.Length; n++)
                        p[n] = new Vector3(2f * a * (float)n / l - a,
                            (float)waves[wn].wave[Mathf.RoundToInt(Mathf.Repeat((float)n * waves[wn].samplerate * audioSource.pitch / l, waves[wn].wave.Length))] / waves[wn].channels +
                            2f * n1 / waves[wn].channels + 1f / waves[wn].channels - 1f, 0f);
                    if (p[p.Length - 1] == null)
                        p[p.Length - 1] = new Vector3(2f * a - a,
                                2f * n1 / waves[wn].channels + 1f / waves[wn].channels - 1f, 0f);

                    r.positionCount = p.Length;
                    r.SetPositions(p);
                    r.endWidth = r.startWidth = LINE_WIDTH;
                    r.material = lineMat;
                }
                catch { };
            }
        }
    }

    public IEnumerator GetAudioClip(string path)
    {
        for (var n = 0; n < waveRenderers.Count; n++)
        {
            Destroy(waveRenderers[n].gameObject);
            waveRenderers.RemoveAt(n);
        }
        using (var request = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV))
        {
            yield return request.SendWebRequest();
            audioSource.clip = DownloadHandlerAudioClip.GetContent(request);
            audioSource.Play();
        }
    }

    public void setButton()
    {
        try
        {
            brake = Mathf.Max(0, int.Parse(brakeInput.text));
            power = Mathf.Max(0, int.Parse(powerInput.text));
            deceleration = Mathf.Max(0f, float.Parse(decelerationInput.text));
            acceleration = Mathf.Max(0f, float.Parse(accelerationInput.text));
        }
        catch { };
        mcSlider.minValue = -brake;
        mcSlider.maxValue = power;
    }

    private string getWaveFilePath(int n)
    {
        return Path.Combine(Application.persistentDataPath, n + ".wav");
    }
}
