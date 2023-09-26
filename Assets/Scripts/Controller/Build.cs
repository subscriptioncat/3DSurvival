using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_BuildingName
{
    Tent,
}

public class Build : MonoBehaviour
{
    //모든 건축 리스트를 저장해야 됩니다.
    [SerializeField] private List<BuildSO> buildList;
    public List<BuildSO> BuildList { get { return buildList; } }
}
