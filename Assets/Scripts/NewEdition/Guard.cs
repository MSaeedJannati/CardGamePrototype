using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour
{
    #region Variables
    public GameObject objPrnt = null;
    public CardInfo prntCrd = null;
    #endregion

    #region functions
    void Awake()
    {
        objPrnt = gameObject;
        prntCrd = gameObject.GetComponent<CardInfo>();
    }
    // Use this for initialization
	void Start () {
       // EventManeger.Instance.AddListener(EVENT_TYPE.CheckFor_Guards, IsThisPlyrHasGaurd);
	}

    private void IsThisPlyrHasGaurd(EVENT_TYPE Event_Type, Component Sender, object param = null)
    {
        if (prntCrd.ownrTg != "Grnd")
            return;
        if (prntCrd.ownr == GameLogic.turn)
            return;
        
        Debug.Log(prntCrd.ownrTg.ToString()+prntCrd.cardName.ToString());

        prntCrd.crdOwnr.hasGuard = true;
    }
	
	// Update is called once per frame
	void Update () {

    }
    #endregion
}
