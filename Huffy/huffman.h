#pragma once
#include <stdio.h>
#include <time.h>

// We Used AI for The Special cases and Debugging //

typedef struct HuffNode {
    unsigned char byte;
    int frequency;
    struct HuffNode* left, * right;
} HuffNode;

HuffNode* createHuffmanNode(unsigned char byte, int freq, HuffNode* left, HuffNode* right);
HuffNode* buildHuffmanTree(int* frequencies);
void freeHuffmanTree(HuffNode* root);
void generateTheCodes(HuffNode* root, char* path, int depth, char** codes);
void writeHeader(FILE* out, int* frequencies, const char* filename, int paddingBits);
void compressFile(FILE* input, FILE* output, int bufferSize, const char* filename);
int readHeader(FILE* in, int* frequencies, int* paddingBits, char* filename);
void decompressFile(FILE* input, FILE* output, int bufferSize);