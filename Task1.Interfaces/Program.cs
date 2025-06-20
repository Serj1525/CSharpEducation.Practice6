using System.Collections;
using System.Globalization;

namespace CSharpEducation.Practice6;

using System;
using System.Collections.Generic;

public interface IStorage<T>
{
    void Add(T item);
    T Get(int index);
    int Count { get; }
}

public class ListStorage<T> : IStorage<T>
{
    private List<T> items = new List<T>();

    public void Add(T item)
    {
        items.Add(item);
    }

    public T Get(int index)
    {
        return items[index];
    }

    public int Count => items.Count;
}

class Program
{
    static void Main()
    {
        ListStorage<string> storage = new ListStorage<string>();
        storage.Add("Hello");
        storage.Add("World");

        for (int i = 0; i < storage.Count; i++)
        {
            Console.WriteLine(storage.Get(i));
        }
        
        //Пример использования методов расширения 
        
        string testString = "A man a plan a canal Panama";
        Console.WriteLine($"Is palindrome: {testString.IsPalindrome()}");

        string titleCaseString = "hello world";
        Console.WriteLine($"Title case: {titleCaseString.ToTitleCase()}");
        
    }
}

//ICloneable
public class Person : ICloneable
{
    public string Name { get; set; }
    public int Age { get; set; }

    public object Clone()
    {
        return new Person { Name = this.Name, Age = this.Age };
    }
}

public class Rectangle : ICloneable
{
    public double Width { get; set; }
    public double Height { get; set; }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}

//IComparable и IComparer

public class Book : IComparable<Book>
{
    public string Title { get; set; }
    public string Author { get; set; }

    public int CompareTo(Book other)
    {
        return string.Compare(Title, other.Title, StringComparison.Ordinal);
    }
}

public class Car : IComparer<Car>
{
    public string Make { get; set; }
    public string Model { get; set; }

    public int Compare(Car x, Car y)
    {
        int makeComparison = string.Compare(x.Make, y.Make, StringComparison.Ordinal);
        return makeComparison != 0 ? makeComparison : string.Compare(x.Model, y.Model, StringComparison.Ordinal);
    }
}

//IEnumerable и IEnumerator

public class MyList : IEnumerable<int>
{
    private List<int> items = new List<int>();

    public void Add(int item)
    {
        items.Add(item);
    }

    public IEnumerator<int> GetEnumerator()
    {
        return items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class MyDictionary : IEnumerator<KeyValuePair<string, int>>
{
    private Dictionary<string, int> items;
    private int position = -1;

    public MyDictionary(Dictionary<string, int> items)
    {
        this.items = items;
    }

    public KeyValuePair<string, int> Current => GetCurrent();

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        position++;
        return position < items.Count;
    }

    public void Reset()
    {
        position = -1;
    }

    public void Dispose() { }

    private KeyValuePair<string, int> GetCurrent()
    {
        foreach (var item in items)
        {
            if (position-- == 0)
                return item;
        }
        throw new InvalidOperationException();
    }
}

public static class Extensions
{
    public static IEnumerable<int> GetEvenNumbers(int start, int end)
    {
        for (int i = start; i <= end; i++)
        {
            if (i % 2 == 0)
            {
                yield return i;
            }
        }
    }
}

//Индексаторы

public class Matrix
{
    private int[,] data;

    public Matrix(int rows, int columns)
    {
        data = new int[rows, columns];
    }

    public int this[int row, int column]
    {
        get => data[row, column];
        set => data[row, column] = value;
    }
}

//Методы расширения

public static class StringExtensions
{
    public static bool IsPalindrome(this string str)
    {
        if (string.IsNullOrEmpty(str)) return false;

        int left = 0;
        int right = str.Length - 1;

        while (left < right)
        {
            if (char.ToLower(str[left]) != char.ToLower(str[right]))
                return false;

            left++;
            right--;
        }
        return true;
    }

    public static string ToTitleCase(this string str)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
    }
}
