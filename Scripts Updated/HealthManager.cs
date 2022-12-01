using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    
    public static int health = 3;
    public Image[] hearts;     //array to store the health sprites
    public Sprite fullHealth;
    public Sprite emptyHealth;



     void Awake ()
     {
          health = 3; //calling in awake so that when we replay the game, we see full 3 hearts instead of the empty sprite like at game end
     }
    // Update is called once per frame
    void Update()
    {
        foreach(Image img in hearts)
        //foreach loops through all elements in the group using internal pointers or indexes; 
        //you just supply a variable of the correct type, and foreach assigns to it a new element each iteration.
        {
            img.sprite = emptyHealth;
        }

        for (int i =0; i < health; i++)   // the sprites will change from full to empty
        {
            hearts[i].sprite = fullHealth;
        }
    }
}
