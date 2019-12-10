using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class BlobUIManager : MonoBehaviour
{

    [SerializeField] protected List<Image> UI;
    [SerializeField] protected int maxBlobs = 10;
    [SerializeField] protected AudioSource source;
    [SerializeField] protected AudioClip clip;
    [SerializeField] protected GameObject winText;
    
    private int[] _collectedBlobs = new int[5];

    private static BlobUIManager _instance;

    public static BlobUIManager Instance => _instance;

    private void Start()
    {
        foreach (var ui in UI)
        {
            ui.fillAmount = 0;
        }

        for (int i = 0; i < 5; i++)
        {
            _collectedBlobs[i] = 0;
        }
        _instance = this;
    }


    public void IncreaseBlobUI(BlobType blob)
    {
        
        int b = (int)blob;
        _collectedBlobs[b]++;
        int newNum = _collectedBlobs[b];
        if (newNum > maxBlobs)
        {
            maxBlobs = newNum;
            _collectedBlobs[b] = maxBlobs;
            UI[b].fillAmount = 1f;
            for (int i = 0; i < 5; i++)
            {
                if (i == b) continue;

                _collectedBlobs[i]--;
                if (_collectedBlobs[i] < 0)
                    _collectedBlobs[i] = 0;
                float fill = (float)_collectedBlobs[i] / maxBlobs;
                UI[i].fillAmount = fill;
            }
        }
        else
        {
            float fill = (float)_collectedBlobs[b] / maxBlobs;
            UI[b].fillAmount = fill;
        }
        if (_collectedBlobs[0] == maxBlobs && _collectedBlobs[1] == maxBlobs && _collectedBlobs[2] == maxBlobs &&
            _collectedBlobs[3] == maxBlobs && _collectedBlobs[4] == maxBlobs)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        source.clip = clip;
        source.Play();
        winText.SetActive(true);
    }

    void Update()
    {
        
    }
}
