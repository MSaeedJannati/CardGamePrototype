using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardMngr : MonoBehaviour {
#region Varibales
    public bool alive=true;
    public int Dmg = 1;
    public int Hlth = 1;
    public int Cost = 1;
    public bool atckdThsTrn=true;
    public bool plyd = false;
    public int owner = 1;
    public GameObject prefab;
    public GameObject Mngr;
#endregion
	// Use this for initialization
	void Start () {
        Mngr = GameObject.Find("Board");
        (gameObject.transform.Find("Dmg")).GetChild(0).gameObject.GetComponent<Text>().text = Dmg.ToString();
        (gameObject.transform.Find("Hlth")).GetChild(0).gameObject.GetComponent<Text>().text = Hlth.ToString();
        (gameObject.transform.Find("Cost")).GetChild(0).gameObject.GetComponent<Text>().text = Cost.ToString();
        alive = true;
	}
    public void Attacked(int incomingDmg)
    {
        Hlth -= incomingDmg;
        if(Hlth<=0)
        { 
            GameObject.DestroyImmediate(gameObject);
            return;

        }
        (gameObject.transform.Find("Hlth")).GetChild(0).gameObject.GetComponent<Text>().text = Hlth.ToString();
    }
    public void Attack(GameObject obj)
    {
        if (obj.GetComponent<CardMngr>() != null && !atckdThsTrn)
        {
            obj.GetComponent<CardMngr>().Attacked(Dmg);
            atckdThsTrn = true;
        }
       
        
    }
    public void Pshd()
    {
        Mngr.GetComponent<GameMngr>().crdSlctd(gameObject);
    }
	
}
