#pragma once
#include "huffman.h"
// We Used AI for The Special cases and Debugging //

typedef struct PQNode {
    HuffNode* huffmanNode;
    struct PQNode* next, * prev;
} PQNode;

typedef struct {
    PQNode* header;
} PriorityQueue;

PriorityQueue* createPriorityQueue();
void enqueue(PriorityQueue* pq, HuffNode* hNode);
HuffNode* dequeue(PriorityQueue* pq);
int hasSingleNode(PriorityQueue* pq);
