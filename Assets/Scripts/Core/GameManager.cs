using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager gm = null;
    public static GameManager Instance {
        get {
            if (gm == null) {
                gm = FindObjectOfType<GameManager>();
            }
            return gm;
        }
    }
}
