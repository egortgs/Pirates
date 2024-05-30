using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Items : MonoBehaviour
{


    [SerializeField] public ItemCollect itemCollect;

    [SerializeField] public TMP_Text woodText;
    [SerializeField] public TMP_Text metallText;
    [SerializeField] public TMP_Text coinText;
    [SerializeField] public TMP_Text goldText;
    // Start is called before the first frame update
    void Start()
    {
        itemCollect = FindObjectOfType<ItemCollect>();
        if (PlayerPrefs.HasKey("woods"))
        {
            woodText.text = itemCollect.wood.ToString();
        }
        if (PlayerPrefs.HasKey("metall"))
        {
            metallText.text = itemCollect.metall.ToString();
        }
        if (PlayerPrefs.HasKey("coins"))
        {
            coinText.text = itemCollect.coin.ToString();
        }
        if (!PlayerPrefs.HasKey("woods"))
        {
            woodText.text = "0";
        }
        if (!PlayerPrefs.HasKey("metall"))
        {
            metallText.text = "0";
        }
        if (!PlayerPrefs.HasKey("coins"))
        {
            coinText.text = "0";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
