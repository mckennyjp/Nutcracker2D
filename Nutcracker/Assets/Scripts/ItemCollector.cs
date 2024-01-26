using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int pineapples = 0;

    [SerializeField] private Text pineapplesText;

    [SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pineapple"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            pineapples++;
            pineapplesText.text = "Pineapples: " + pineapples;
        }
    }
}
