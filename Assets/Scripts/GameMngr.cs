using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMngr : MonoBehaviour
{

    #region Variables
    public GameObject[] Plyrs;
    public GameObject trnPnl;
    public int activeplyr;
    public GameObject[] slctdItm;
    public GameObject[] plyPnls;
    public GameObject khrs;
    #endregion
    void Start()
    {
        activeplyr = 0;
        Plyrs[activeplyr].GetComponent<PlyrMngr>().startTurn();
        trnPnl.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Turn:" + Plyrs[activeplyr].GetComponent<PlyrMngr>().plyrName;
    }
    void startTrn()
    { 
    
    }
   public  void EndTrn()
    {

        Plyrs[activeplyr].GetComponent<PlyrMngr>().plyrTrn = false;
        if (slctdItm[activeplyr].transform.childCount>0)
        { 
       GameObject.DestroyImmediate( slctdItm[activeplyr].transform.GetChild(0).gameObject);
       }
        activeplyr++;
        if (activeplyr > 1)
        {
            activeplyr = 0;
        }
        Plyrs[activeplyr].GetComponent<PlyrMngr>().startTurn();
        trnPnl.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Turn:" + Plyrs[activeplyr].GetComponent<PlyrMngr>().plyrName;
    
    }
   public void crdSlctd(GameObject inptObj)
   { 
    if(inptObj.GetComponent<CardMngr>().owner==(activeplyr+1))
     {
         if (slctdItm[activeplyr].transform.childCount > 0)
         {
             GameObject.DestroyImmediate(slctdItm[activeplyr].transform.GetChild(0).gameObject);
         }
         khrs = inptObj;
         GameObject b = (GameObject)Instantiate(inptObj, slctdItm[activeplyr].transform);
         b.transform.localPosition = Vector3.zero;
         b.GetComponent<Button>().interactable = false;
     }
  
   }
   public void pnlSlctd(GameObject inptObj)
   {
       if (inptObj == plyPnls[activeplyr] && slctdItm[activeplyr].transform.childCount>0)
       {
           if (Plyrs[activeplyr].GetComponent<PlyrMngr>().plyCrd(slctdItm[activeplyr].transform.GetChild(0).gameObject))
           {

               GameObject.DestroyImmediate(slctdItm[activeplyr].transform.GetChild(0).gameObject);
               DestroyImmediate(khrs);
           }

       }
   }
}
