using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_BuildingName
{
    Tent,
}

public class Build : MonoBehaviour
{
    //��� ���� ����Ʈ�� �����ؾ� �˴ϴ�.
    [SerializeField] private List<BuildSO> buildList;
    public List<BuildSO> BuildList { get { return buildList; } }
}
