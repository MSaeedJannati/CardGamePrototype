using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlyrMngr : MonoBehaviour
{

    #region Variables 
    public ArrayList crds= new ArrayList();
    public ArrayList cardsAthnd= new ArrayList();
    public ArrayList cardsInPlay= new ArrayList();

    public int mana = 1;
    public int maxMana = 1;
    public int plyrId = 0;
    public int asb = 0;

    public bool plyrTrn = false;
    public string plyrName = "Younes";

    public GameObject hand;
    public GameObject plydPnl;
    //public GameObject OtherPlyr;
    //public GameObject turnPnl;
    public GameObject manaText;
    public GameObject[] plyrDeck;

    #endregion 
    void start()
    {
        
        
    
    }
    void Awake()
    {
        DrawCard(3);
    }
    public void startTurn()
    {
        maxMana++;
        mana = maxMana;
        manaText.GetComponent<Text>().text = mana.ToString();
        DrawCard(1);
        if(cardsInPlay.Count>0){
        foreach(GameObject GmObj in cardsInPlay)
           {
               if (GmObj != null)
               {
                   GmObj.GetComponent<CardMngr>().plyd = false;
               }
               else
               {
                   cardsInPlay.Remove(GmObj);
               }
           }
        }
        
    }
    public void DrawCard(int inputNom)
    {
        for (int i = 0; i < inputNom;i++ )
        {
           GameObject a=plyrDeck[Random.Range(0,plyrDeck.Length)].GetComponent<CardMngr>().prefab.gameObject;
            a.GetComponent<CardMngr>().owner=plyrId;
            cardsAthnd.Add(Instantiate(a,hand.transform,false));
        }
    }
    public bool plyCrd(GameObject crdTPly)
    {
        if (crdTPly.GetComponent<CardMngr>().Cost <= mana)
        {
            mana -= crdTPly.GetComponent<CardMngr>().Cost;
            manaText.GetComponent<Text>().text = mana.ToString();
            GameObject h =(GameObject) Instantiate((GameObject)crdTPly, plydPnl.transform, false);
            //h.GetComponent<Button>().interactable = true;
            cardsInPlay.Add(h);
            
            
                   
                   
                    //GameObject.DestroyImmediate(crdTPly);
            
                    //cardsAthnd.Remove(crdTPly);
                   
            
            return true;
        }
        else
        {
            return false;
        }
    }
  
    
}
