using UnityEngine;
using System.Collections;
public enum KeyWordst 
{
 guard=1,charge=2,none
};
public class KeyWord : MonoBehaviour {

    #region variables
    public KeyWordst kyWrd = KeyWordst.charge;
    #endregion
   // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
