using System.Collections;
using System.Collections.Generic;
using UI.Map;
using UnityEngine;

public class MapInit : MonoBehaviour
{
    private void Awake()
    {
        MapBinder.BindAll();
    }

}
