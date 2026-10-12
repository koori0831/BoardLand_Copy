using System;

namespace Core
{
    public struct RandomEnumValue<T>
    {
        private static Random random = new Random();

        public T RandomEnum;
        public RandomEnumValue()
        {
            var enumValues = System.Enum.GetValues(enumType: typeof(T));
            RandomEnum = (T)enumValues.GetValue(random.Next(0, enumValues.Length));
        }
    }
}
