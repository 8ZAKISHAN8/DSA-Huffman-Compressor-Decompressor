#include <stdlib.h>
#include <string.h>
#include "huffman.h"

// We Used AI for The Special cases and Debugging //

int readHeader(FILE* in, int* frequencies, int* paddingBits, char* filename)
{
    char magic[5] = { 0 };
    if (fread(magic, 1, 4, in) != 4 || strcmp(magic, "HUFF") != 0)
    {
        printf("Invalid file format\n");
        return 0;
    }

    unsigned short nameLen;
    if (fread(&nameLen, sizeof(unsigned short), 1, in) != 1)
    {
        printf("Corrupted header\n");
        return 0;
    }

    if (fread(filename, 1, nameLen, in) != nameLen)
    {
        printf("Corrupted filename\n");
        return 0;
    }

    if (fread(paddingBits, sizeof(int), 1, in) != 1 || fread(frequencies, sizeof(int), 256, in) != 256)
    {
        printf("Corrupted frequency table\n");
        return 0;
    }

    return 1;
}


void decompressFile(FILE* input, FILE* output, int bufferSize)
{
    int frequencies[256];
    int paddingBits;
    char filename[256];

    if (!readHeader(input, frequencies, &paddingBits, filename))
    {
        return;
    }

    HuffNode* huffmanTree = buildHuffmanTree(frequencies);
    HuffNode* currentNode = huffmanTree;
    unsigned char* inputBuffer = (unsigned char*)malloc(bufferSize);
    if (!inputBuffer)
    {
        printf("Error: Could not allocate input buffer\n");
        exit(1);
    }
    unsigned char* outputBuffer = (unsigned char*)malloc(bufferSize);
    int outputPos = 0;
    size_t bytesRead;

    long headerOffset = ftell(input);
    fseek(input, 0, SEEK_END);
    long totalSize = ftell(input);
    fseek(input, headerOffset, SEEK_SET);  // go back to data, not file start
    long processedSize = 0;

    clock_t startTime = clock();

    // DECOMPRESSION LOOP | we used Ai for the structure of the code 

    while ((bytesRead = fread(inputBuffer, 1, bufferSize, input)) > 0)
    {
        processedSize += bytesRead;

        for (size_t i = 0; i < bytesRead; i++)
        {
            for (int bit = 7; bit >= 0; bit--)
            {
                if (feof(input) && i == bytesRead - 1 && bit < paddingBits) break;

                if (inputBuffer[i] & (1 << bit))
                    currentNode = currentNode->right;
                else
                    currentNode = currentNode->left;

                if (!currentNode->left && !currentNode->right)
                {
                    outputBuffer[outputPos++] = currentNode->byte;
                    currentNode = huffmanTree;

                    if (outputPos == bufferSize)
                    {
                        fwrite(outputBuffer, 1, bufferSize, output);
                        outputPos = 0;
                    }
                }
            }
        }

        
        double percent = (double)processedSize / totalSize * 100.0;
        double elapsed = (double)(clock() - startTime) / CLOCKS_PER_SEC;
        double estimatedTotal = (elapsed / processedSize) * totalSize;
        double remaining = estimatedTotal - elapsed;
        printf("\rProgress: %.2f%% | ETA: %.1f seconds", percent, remaining);
        fflush(stdout);
    }


    if (outputPos > 0)
    {
        fwrite(outputBuffer, 1, outputPos, output);
    }

    free(inputBuffer);
    free(outputBuffer);
    freeHuffmanTree(huffmanTree);
}
