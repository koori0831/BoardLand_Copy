using System;

namespace Core
{
    public struct RandomEnumValue<T>
    {
        public T RandomEnum;
        public RandomEnumValue()
        {
            var enumValues = System.Enum.GetValues(enumType: typeof(T));
            RandomEnum = (T)enumValues.GetValue(new Random().Next(0, enumValues.Length));
        }
    }
}
