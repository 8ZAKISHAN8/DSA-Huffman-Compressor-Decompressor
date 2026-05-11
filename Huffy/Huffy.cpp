#include <stdlib.h>
#include <string.h>
#include<time.h>
#include "priority_queue.h"

// We Used AI for The Special cases and Debugging //


HuffNode* createHuffNode(unsigned char byte, int freq, HuffNode* left, HuffNode* right)
{
    HuffNode* node = (HuffNode*)malloc(sizeof(HuffNode));
    if (!node) return NULL;
    node->byte = byte;
    node->frequency = freq;
    node->left = left;
    node->right = right;
    return node;
}


HuffNode* buildHuffmanTree(int* frequencies)
{
    PriorityQueue* pq = createPriorityQueue();

 
    for (int i = 0; i < 256; i++)
    {
        if (frequencies[i] > 0)
        {
            HuffNode* node = createHuffNode((unsigned char)i, frequencies[i], NULL, NULL);
            if (!node)
            {
                printf("Memory allocation failed\n");
                exit(1);
            }
            enqueue(pq, node);
        }
    }

    // case file is empty
    if (!pq->header)
    {
        return createHuffNode('\0', 0, NULL, NULL);
    }

    // case single byte
    if (hasSingleNode(pq))
    {
        HuffNode* onlyNode = dequeue(pq);
        return createHuffNode('\0', onlyNode->frequency, onlyNode, NULL);
    }

    // Make the tree 
    while (!hasSingleNode(pq))
    {
        HuffNode* left = dequeue(pq); 
        HuffNode* right = dequeue(pq);

        HuffNode* parent = createHuffNode('\0', left->frequency + right->frequency, left, right); // Virtual parent to the 2 nodes 
        if (!parent) 
        {
            printf("Memory allocation failed\n");
            exit(1);
        }
        enqueue(pq, parent);
    }

    HuffNode* root = dequeue(pq);
    free(pq);
    return root;
}


void freeHuffmanTree(HuffNode* root)
{
    if (!root) return;
    freeHuffmanTree(root->left);
    freeHuffmanTree(root->right);
    free(root);
}


// Codes For each byte
void generateTheCodes(HuffNode* root, char* path, int depth, char** codes) // ** for dynamic pointer 
{ 
    if (!root->left && !root->right) 
    {
        path[depth] = '\0';
        /* codes[root->byte] = strdup(path);*/
        codes[root->byte] = (char*)malloc(strlen(path) + 1);
        strcpy(codes[root->byte], path);
        return;
    }
    path[depth] = '0';
    generateTheCodes(root->left, path, depth + 1, codes); // shmal 0 
    path[depth] = '1';
    generateTheCodes(root->right, path, depth + 1, codes); // yemen 1 
}
