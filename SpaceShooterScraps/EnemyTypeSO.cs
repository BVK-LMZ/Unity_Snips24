using System.IO.Enumeration;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyType", menuName = "EnemyType",order = 0)]
public class EnemyTypeSO : ScriptableObject
{
    public GameObject _theTangoPrefab;
    public GameObject _theWeaponPrefab2;
    public float _theShipSpeed;
}
