using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameController : MonoSingleton<InGameController>
{
    [SerializeField]
    private InGameTime gameTime;
    // Start is called before the first frame update

    public float GetGameTime() {
        return gameTime.Time;
    }

}
