using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    #region Variables
   
    public static bool turn = true;
    public bool GameisRunning = false;
    public GameObject PausePnl = null;
    public static GameLogic Instanse=null;
    public bool trnGhost = false;
    public PlayerInfo plyr1 = null;
    public PlayerInfo plyr2 = null;
   
    #endregion

    #region properties
 
    
    #endregion
    #region Functions
    // Use this for initialization
	void Start () {
        //EventManeger.Instance.AddListener(EVENT_TYPE.GAME_START,gameStart);
        Instanse = this;
        trnGhost = turn;
	}

   

   private void gameStart(EVENT_TYPE Event_Type, Component Sender, object param = null)
    {
        turn =true;
        EventManeger.Instance.PostNotification(EVENT_TYPE.TURN_END,this);
    }
	
	
	
   public void newGame() 
    {
        GameisRunning = true;
        EventManeger.Instance.PostNotification(EVENT_TYPE.GAME_START, this);
        turn = true;
        EventManeger.Instance.PostNotification(EVENT_TYPE.TURN_END, this);
        
        
    }
   public void endTurn()
   {

       turn = !turn;
       trnGhost = !trnGhost;
       EventManeger.Instance.PostNotification(EVENT_TYPE.TURN_END, this);

   }
   public void ExitGame()
   {
       Application.Quit();
   
   }
   public void Resume()
   {
       if (GameisRunning)
       {
           PausePnl.gameObject.SetActive(false);
       }
   }
   public void GameEnd()
   {
       PausePnl.gameObject.SetActive(true);
   }
    #endregion
}
