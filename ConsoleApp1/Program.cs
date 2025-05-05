namespace Functions;

public class ArrayFunctions
{
    static void Main() { }

    public static long FindSumOfTwoSmallestNumbers(int[] array)
    {
        try
        {
            int firstMin = Math.Min(array[0], array[1]);
            int secondMin = Math.Max(array[0], array[1]);

            // нахождение искомых минимумов
            for (int i = 2; i < array.Length; i++)
            {
                if (array[i] < firstMin)
                {
                    secondMin = firstMin;
                    firstMin = array[i];
                }
                else if (array[i] < secondMin)
                    secondMin = array[i];
            }
            // приведение к long для предотвращения переполнения
            return (long)firstMin + secondMin;
        }
        // исключение - длина переданного массива меньше 2
        catch (System.IndexOutOfRangeException)
        {
            Console.WriteLine("Передан массив недостаточного размера (array.Length < 2)");
            return 0;
        }
    }
}
