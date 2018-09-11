using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is an abstract parent class that makes inheriting classes
//to implement a function that sets their position.
public abstract class ChangePlatformPosition : MonoBehaviour {

    public abstract void setNewPosition(float maxXZ, float minY, float maxY);
}
