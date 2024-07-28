using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallBuy : MonoBehaviour
{
    private GameObject playerObject;
    private BuyManager buyManager;
    public string playerTag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag(playerTag);
        this.buyManager = playerObject.GetComponent<BuyManager>();
    }

    // Update is called once per frame
    public void callBuyEnergy()
    {
        buyManager.applyBattery();
    }

    public void callBuyEngine()
    {
        buyManager.applyEngine();
    }

    public void callBuyHook()
    {
        buyManager.applyHook();
    }
}
