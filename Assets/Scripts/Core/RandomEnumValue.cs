using UnityEngine;

namespace Core
{
    public struct RandomEnumValue<T>
    {
        public T GetRandomEnumValue()
        {
            var enumValues = System.Enum.GetValues(enumType: typeof(T));
            return (T)enumValues.GetValue(Random.Range(0, enumValues.Length));
        }
    }
}
