using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignoreenviro : MonoBehaviour {

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(4, 8, true);
        Physics2D.IgnoreLayerCollision(4, 12, true);
        Physics2D.IgnoreLayerCollision(4, 10, true);
    }
}
