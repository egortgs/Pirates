using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemCollect : MonoBehaviour
{

    public int wood;
    public int metall;
    public int coin;
    public int gold;

    public int woodTake;
    public int metallTake;
    public int coinTake;
    public int goldTake;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("woods"))
        {
            wood = PlayerPrefs.GetInt("woods");
        }
        if (PlayerPrefs.HasKey("metall"))
        {
            metall = PlayerPrefs.GetInt("metall");
        }
        if (PlayerPrefs.HasKey("coins"))
        {
            coin = PlayerPrefs.GetInt("coins");
        }
        if(!PlayerPrefs.HasKey("woods") || !PlayerPrefs.HasKey("coins"))
        {
            wood = PlayerPrefs.GetInt("woods", 0);
            metall = PlayerPrefs.GetInt("metall", 0);
            coin = PlayerPrefs.GetInt("coins", 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Materials"))
        {
            woodTake = Random.Range(3, 12);
            metallTake = Random.Range(2, 8);
            wood += woodTake;
            metall += metallTake;
          
            PlayerPrefs.SetInt("woods", wood + woodTake);
            PlayerPrefs.SetInt("metall", metall + metallTake);

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Coins"))
        {
            coinTake += Random.Range(2, 8);
            PlayerPrefs.SetInt("coins", coin + coinTake);

            Destroy(other.gameObject);
        }
    }
}
