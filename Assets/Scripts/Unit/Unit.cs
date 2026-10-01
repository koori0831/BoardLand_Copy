using UnityEngine;

namespace Unit
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private UnitStatus status;
        public UnitStatus Status => status;
    }
}
