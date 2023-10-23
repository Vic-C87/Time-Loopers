using System;
using System.Collections;
using System.Collections.Generic;

public class Heap<T> where T : IHeapItem<T>
{
    T[] myItems;
    int myCurrentItemCount;

    public Heap(int aHeapSize)
    {
        myItems = new T[aHeapSize];
    }

    public void Add(T anItem)
    {
        anItem.HeapIndex = myCurrentItemCount;
        myItems[myCurrentItemCount] = anItem;
        SortUp(anItem);
        myCurrentItemCount++;
    }

    public T RemoveFirst()
    {
        T firstItem = myItems[0];
        myCurrentItemCount--;
        myItems[0] = myItems[myCurrentItemCount];
        myItems[0].HeapIndex = 0;
        SortDown(myItems[0]);

        return firstItem;
    }

    public bool Contains(T anItem)
    {
        return Equals(myItems[anItem.HeapIndex], anItem);
    }

    public int Count
    {
        get
        {
            return myCurrentItemCount;
        }
    }

    public void UpdateItem(T anItem)
    {
        SortUp(anItem);
    }

    void SortUp(T anItem)
    {
        int parentIndex = (anItem.HeapIndex - 1) / 2;

        while (true)
        {
            T parentItem = myItems[parentIndex];
            if (anItem.CompareTo(parentItem) > 0)
            {
                Swap(anItem, parentItem);
            }
            else
            {
                break;
            }

            parentIndex = (anItem.HeapIndex - 1) / 2;
        }
    }

    void SortDown(T anItem)
    {
        while (true)
        {
            int childIndexLeft = anItem.HeapIndex * 2 + 1;
            int childIndexRight = anItem.HeapIndex * 2 + 2;
            int swapIndex = 0;

            if (childIndexLeft < myCurrentItemCount)
            {
                swapIndex = childIndexLeft;

                if (childIndexRight < myCurrentItemCount)
                {
                    if (myItems[childIndexLeft].CompareTo(myItems[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }

                if (anItem.CompareTo(myItems[swapIndex]) < 0)
                {
                    Swap(anItem, myItems[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    void Swap(T anItemA, T anItemB)
    {
        myItems[anItemA.HeapIndex] = anItemB;
        myItems[anItemB.HeapIndex] = anItemA;
        int itemAHeapIndex = anItemA.HeapIndex;
        anItemA.HeapIndex = anItemB.HeapIndex;
        anItemB.HeapIndex = itemAHeapIndex;
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    public int HeapIndex {  get; set; }
}
