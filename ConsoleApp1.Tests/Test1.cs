using System.Diagnostics;
using Functions;

namespace ConsoleApp1.Tests;

[TestClass]
public sealed class ArrayFunctions_FindSumOfTwoSmallestNumbersShould
{
    [TestMethod]
    // когда обычный массив
    public void FindSumOfTwoSmallestNumbers_WhenNormalArray()
    {
        int[] numbers = { 1, 4, -10, 496, 0, 2 };

        long result = ArrayFunctions.FindSumOfTwoSmallestNumbers(numbers);

        Assert.AreEqual(-10, result);
    }

    [TestMethod]
    // когда длина массива меньше требуемой
    public void FindSumOfTwoSmallestNumbers_WhenArrayInsufficientLength()
    {
        int[] numbers = { };

        long result = ArrayFunctions.FindSumOfTwoSmallestNumbers(numbers);

        Assert.AreEqual(0, result);
    }

    [TestMethod]
    // когда результат меньше минимального integer
    public void FindSumOfTwoSmallestNumbers_WhenResultIntegerOverflowFromBottom()
    {
        int[] numbers = { 2, -2147483648, -2147483648, 1, 4, 5 };

        long result = ArrayFunctions.FindSumOfTwoSmallestNumbers(numbers);

        Assert.AreEqual(-4294967296, result);
    }

    [TestMethod]
    // когда результат больше минимального integer
    public void FindSumOfTwoSmallestNumbers_WhenResultIntegerOverflowFromAbove()
    {
        int[] numbers = { 2147483647, 2147483647, 2147483647, 2147483647 };

        long result = ArrayFunctions.FindSumOfTwoSmallestNumbers(numbers);

        Assert.AreEqual(4294967294, result);
    }

    [TestMethod]
    // когда массив содержит повторяющиеся значения
    public void FindSumOfTwoSmallestNumbers_WhenArrayHasRepeatedValues()
    {
        int[] numbers = { 1, 1, 1, 1, 1, 1, 1 };

        long result = ArrayFunctions.FindSumOfTwoSmallestNumbers(numbers);

        Assert.AreEqual(2, result);
    }

    [TestMethod]
    // когда длина массива 2
    public void FindSumOfTwoSmallestNumbers_WhenArrayLengthIsTwo()
    {
        int[] numbers = { 1, -598 };

        long result = ArrayFunctions.FindSumOfTwoSmallestNumbers(numbers);

        Assert.AreEqual(-597, result);
    }

    [TestMethod]
    // когда массив большой - требуется O(n) временная сложность
    [Timeout(10000)]
    [DataRow(1_000_000)]
    [DataRow(10_000_000)]
    [DataRow(100_000_000)]
    public void FindSumOfTwoSmallestNumbers_OnHugeArray(int size)
    {
        //const int size = 100_000_000;
        var random = new Random();

        int min1 = int.MinValue;
        int min2 = int.MinValue + 1;

        int[] numbers = GenerateHugeArray(size, min1, min2, random);

        var stopwatch = Stopwatch.StartNew();
        long result = ArrayFunctions.FindSumOfTwoSmallestNumbers(numbers);
        stopwatch.Stop();

        Console.WriteLine(
            "Время исполнения функции FindSumOfTwoSmallestNumbers() для массива размером {0} - {1} миллисекунды",
            size,
            stopwatch.ElapsedMilliseconds
        );

        Assert.AreEqual((long)min1 + min2, result);
    }

    private static int[] GenerateHugeArray(int size, int min1, int min2, Random random)
    {
        var array = new int[size];
        array[0] = min1;
        array[1] = min2;

        for (int i = 2; i < size; i++)
        {
            array[i] = random.Next(min2 + 1, int.MaxValue);
        }

        return array;
    }
}
