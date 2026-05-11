#include <stdlib.h>
#include "priority_queue.h"

// we took this part from dr nour and edit in it 
PriorityQueue* createPriorityQueue()
{
    PriorityQueue* pq = (PriorityQueue*)malloc(sizeof(PriorityQueue));
    if (!pq) return NULL;
    pq->header = NULL;
    return pq;
}

void enqueue(PriorityQueue* pq, HuffNode* hNode)
{
    PQNode* newNode = (PQNode*)malloc(sizeof(PQNode));
    if (!newNode) return;
    newNode->huffmanNode = hNode;
    newNode->next = newNode->prev = NULL;

    if (!pq->header) {
        pq->header = newNode;
        return;
    }

    PQNode* current = pq->header;
    PQNode* prev = NULL;

    while (current && current->huffmanNode->frequency < hNode->frequency)
    {
        prev = current;
        current = current->next;
    }

    if (!prev)
    {
        newNode->next = pq->header;
        pq->header->prev = newNode;
        pq->header = newNode;
    }
    else
    {
        newNode->next = prev->next;
        newNode->prev = prev;
        prev->next = newNode;
        if (newNode->next)
            newNode->next->prev = newNode;
    }
}

// Dequeue the smallest node
HuffNode* dequeue(PriorityQueue* pq)
{
    if (!pq->header) return NULL;

    HuffNode* result = pq->header->huffmanNode;
    PQNode* temp = pq->header;

    pq->header = pq->header->next;
    if (pq->header)
        pq->header->prev = NULL;

    free(temp);
    return result;
}


int hasSingleNode(PriorityQueue* pq)
{
    return pq->header && !pq->header->next;
}