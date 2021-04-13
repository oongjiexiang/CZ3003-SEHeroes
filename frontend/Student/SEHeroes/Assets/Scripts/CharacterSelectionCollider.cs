using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterSelectionCollider : MonoBehaviour
{
    private static BoxCollider2D[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        colliders = gameObject.GetComponentsInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public static void disableCollider() {
       foreach(BoxCollider2D co in colliders)
            co.enabled = false;
    }

    public static void enableCollider() {
        foreach(BoxCollider2D co in colliders)
            co.enabled = true;
    }
}
