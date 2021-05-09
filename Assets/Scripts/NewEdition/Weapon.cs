using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    #region Variables
    public GameObject prefab = null;
    public CardInfo crdInfo=null;

    #endregion
    #region Properties
   
    #endregion
    #region Functions
    // Use this for initialization
	void Start () {
        //EventManeger.Instance.AddListener(EVENT_TYPE.Weapon_Equiped,wpnEqpd);
	}

    private void wpnEqpd(EVENT_TYPE Event_Type, Component Sender, object param = null)
    {
        prefab = Sender.gameObject;
        crdInfo = prefab.GetComponent<CardInfo>();
    }
	
	// Update is called once per frame
	void Update () {

    }
    #endregion
}
