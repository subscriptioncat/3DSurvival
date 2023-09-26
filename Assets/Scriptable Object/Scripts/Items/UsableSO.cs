using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 말 소모품. 허기, 갈증, 체력, 스테미너 등에 영향을 준다. 어떤 효과를 가지는 지에 대한 정보는 eTag 로 나타낸다.
/// </summary>
public class UsableSO : ItemSO
{
    public enum E_Tag
    {
        EIDIBLE,
    }
}
