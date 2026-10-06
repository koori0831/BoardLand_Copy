using Alchemy.Inspector;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New User", menuName = "Scriptable Objects/User")]
public class UserDataSO : ScriptableObject
{
    public string NickName;
    public string GUID;

    [LabelText("새 GUID 만들기")]
    [Button]
    private void CreateNewGUID()
    {
        GUID = Guid.NewGuid().ToString();
    }
}
