using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionPlayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            //healthmanager instance
            HealthManager.health --;
            if(HealthManager.health <=0)
           {
               Debug.Log("Game Over!");
               ScoreManager.isGameOver = true;
              gameObject.SetActive(false);
           }

          else 
          {
            StartCoroutine(GetHurt());
            ScoreManager.instance.MinusPoints();  //if the player successfully jumps over the obstacle banana, it will score 5 points 
          }      
        }
    }

    IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(7,8);    //collision detection based on layers 
        yield return new WaitForSeconds(3);    // The player is okay for 3 seconds and then can take damage again
        Physics2D.IgnoreLayerCollision(7,8, false);
    }

}
