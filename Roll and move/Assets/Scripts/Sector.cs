using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SectorType{
  normal,
  bonus,
  fail,
  finish
}

public class Sector : MonoBehaviour {
  public SectorType sectorType;
  
  public Transform[] standPoints = new Transform[4];
}
