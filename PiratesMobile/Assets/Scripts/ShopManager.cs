using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Timeline.TimelinePlaybackControls;
public class ShopManager : MonoBehaviour
{
    public bool isDefaultShip, isMortairShip;
    ItemCollect itemCollect;

    // Start is called before the first frame update
    void Start()
    {
        itemCollect = FindObjectOfType<ItemCollect>();

        isDefaultShip = (PlayerPrefs.GetInt("defaultShip") != 0);
        isMortairShip = (PlayerPrefs.GetInt("mortairShip") != 0);

        if (!PlayerPrefs.HasKey("defaultShip"))
        {
            isDefaultShip = false;
        }
        if (!PlayerPrefs.HasKey("mortairShip"))
        {
            isMortairShip = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void BuyDefaultShip()
    {
        if (!isDefaultShip)
        {
            if (itemCollect.coin > 3)
            {
                isDefaultShip = true;
                itemCollect.coin -= 3;
                PlayerPrefs.SetInt("coins", itemCollect.coin);

                PlayerPrefs.SetInt("defaultShip", isDefaultShip ? 1 : 0);
                isDefaultShip = (PlayerPrefs.GetInt("defaultShip") != 0);
            }
        }    
    }

    public void BuyMortairShip()
    {
        if (!isMortairShip)
        {
            if (itemCollect.coin > 5)
            {
                isMortairShip = true;
                itemCollect.coin -= 5;
                PlayerPrefs.SetInt("coins", itemCollect.coin);

                PlayerPrefs.SetInt("mortairShip", isDefaultShip ? 1 : 0);
                isMortairShip = (PlayerPrefs.GetInt("mortairShip") != 0);
            }
        }
    }


}
