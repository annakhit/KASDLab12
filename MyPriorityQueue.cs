using System;
using System.Collections.Generic;
using System.Linq;

public class MyPriorityQueue<T>
{
    private MyPriotiryTask<T>[] queue;
    private int size = 0;
    private readonly IComparer<MyPriotiryTask<T>> comparator = new MyPriotiryTaskComparer<T>();

    public MyPriorityQueue()
    {
        queue = new MyPriotiryTask<T>[11];
    }

    public MyPriorityQueue(params T[] array)
    {
        foreach (T element in array) Add(element);
    }

    public MyPriorityQueue(uint initialCapacity)
    {
        queue = new MyPriotiryTask<T>[initialCapacity];
    }

    public MyPriorityQueue(uint initialCapacity, IComparer<MyPriotiryTask<T>> comparator)
    {
        queue = new MyPriotiryTask<T>[initialCapacity];
        this.comparator = comparator;
    }

    public MyPriorityQueue(MyPriorityQueue<T> priorityQueue)
    {
        queue = (MyPriotiryTask<T>[])priorityQueue.queue.Clone();
        size = priorityQueue.size;
        comparator = priorityQueue.comparator;
    }

    public void Add(T element, int priority = 0)
    {
        if (size == queue.Length) UpdateCapacity();
        queue[size] = new MyPriotiryTask<T>(element, priority);
        size++;
        Array.Sort(queue, 0, size, comparator);
    }

    public void AddAll(params T[] array)
    {
        foreach (T element in array) Add(element);
    }

    public void Clear()
    {
        queue = new MyPriotiryTask<T>[11];
        size = 0;
    }

    public bool Contains(object obj)
    {
        foreach (MyPriotiryTask<T> element in queue)
        {
            if (element == null) return false;
            if (obj.Equals(element.value)) return true;
        }
        return false;
    }

    public bool ContainsAll(params T[] array)
    {
        foreach (T element in array)
        {
            if (!Contains(element)) return false;
        }
        return true;
    }

    public bool IsEmpty() => size == 0;

    public void Remove(object obj)
    {
        int offset = 0;

        MyPriotiryTask<T>[] newQueue = new MyPriotiryTask<T>[size];

        for (int index = 0; index < size; index++)
        {
            if (obj.Equals(queue[index].value)) offset++;
            newQueue[index] = queue[index + offset];
        }

        size -= offset;
        queue = newQueue;
    }

    public void RemoveAll(params T[] array)
    {
        foreach (T element in array) Remove(element);
    }

    public void RetainAll(params T[] array)
    {
        foreach (T element in array)
        {
            if (!Contains(element)) Remove(element);
        }
    }

    public int Size() => size;
    public T[] ToArray()
    {
        T[] result = new T[size];
        for (int index = 0; index < size; index++)
        {
            result[index] = queue[index].value;
        }
        return result;
    }

    public T[] ToArray(ref T[] array)
    {
        if (array == null) ToArray();

        if (array.Length < size)
            throw new ArgumentOutOfRangeException();

        for (int index = 0; index < size; index++)
        {
            array[index] = queue[index].value;
        }
        return array;
    }

    public T Peek()
    {
        if (size == 0) return default;
        return queue[0].value;
    }

    public MyPriotiryTask<T> Pull()
    {
        if (size == 0) return default;

        MyPriotiryTask<T> element = queue[0];

        for (int index = 0; index < size - 1; index++) queue[index] = queue[index + 1];
        queue[size - 1] = default;
        size--;

        return element;
    }

    private void UpdateCapacity()
    {
        MyPriotiryTask<T>[] newQueue = new MyPriotiryTask<T>[size < 64 ? size * 2 + 1 : (int)(size * 1.5) + 1];
        for (int i = 0; i < size; i++) newQueue[i] = queue[i];
        queue = newQueue;
    }

    public void Print()
    {
        foreach (MyPriotiryTask<T> task in queue.Where(value => value != null)) Console.WriteLine("Значение: {0}  Приоритет: {1}", task.value, task.priority);
    }
}