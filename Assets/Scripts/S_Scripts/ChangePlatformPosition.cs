using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChangePlatformPosition : MonoBehaviour {

    public abstract Vector3 setNewPosition(float maxXZ, float minY, float maxY);
}
