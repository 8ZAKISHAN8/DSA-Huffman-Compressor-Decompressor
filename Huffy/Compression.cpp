#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <math.h>
#include "huffman.h"

// We Used AI for The Special cases and Debugging //


void writeHeader(FILE* out, int* frequencies, const char* filename, int paddingBits)
{
    fwrite("HUFF", 1, 4, out);
    unsigned short nameLen = (unsigned short)strlen(filename) + 1;
    fwrite(&nameLen, sizeof(unsigned short), 1, out);
    fwrite(filename, 1, nameLen, out);
    fwrite(&paddingBits, sizeof(int), 1, out);
    fwrite(frequencies, sizeof(int), 256, out);
}


void compressFile(FILE* input, FILE* output, int bufferSize, const char* filename)
{
 
    int frequencies[256] = { 0 };
    unsigned char* BufferIn = (unsigned char*)malloc(bufferSize);
    size_t bytesRead;

    while ((bytesRead = fread(BufferIn, 1, bufferSize, input)) > 0)
    {
        for (size_t i = 0; i < bytesRead; i++)
        {
          
            frequencies[BufferIn[i]]++;
        }
    }

    
    HuffNode* huffmanTree = buildHuffmanTree(frequencies);
    char* codes[256] = { NULL };
    char path[256] = { 0 };

    generateTheCodes(huffmanTree, path, 0, codes);

    rewind(input);
    writeHeader(output, frequencies, filename, 0); 

    unsigned char* BufferOut = (unsigned char*)calloc(bufferSize, 1);
    int outputPos = 0;
    int bitPos = 0;

   
   // Get total size of input Used Ai for this 
   fseek(input, 0, SEEK_END);
   long totalSize = ftell(input);
   rewind(input);
   long processedSize = 0;

   clock_t startTime = clock();

// COMPRESSION LOOP | we used Ai for the structure of the code 
  while ((bytesRead = fread(BufferIn, 1, bufferSize, input)) > 0)
  {
    processedSize += bytesRead;

    for (size_t i = 0; i < bytesRead; i++)
    {
        char* code = codes[BufferIn[i]];
        for (int j = 0; code[j]; j++) 
        {
            if (code[j] == '1')
                BufferOut[outputPos] |= (1 << (7 - bitPos));

            bitPos++;
            if (bitPos == 8)
            {
                outputPos++;
                bitPos = 0;
                if (outputPos == bufferSize)
                {
                    fwrite(BufferOut, 1, bufferSize, output);
                    outputPos = 0;
                    memset(BufferOut, 0, bufferSize);
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


    if (bitPos > 0)
    {
        outputPos++;
    }
    if (outputPos > 0)
    {
        fwrite(BufferOut, 1, outputPos, output);
    }
    printf("\rProgress: 100.00%% | ETA: 0.0 seconds\n");

    // Calculate padding bits | we used ai for the calc 
    int paddingBits = (8 - bitPos) % 8;
    fseek(output, 4 + 2 + strlen(filename) + 1, SEEK_SET);
    fwrite(&paddingBits, sizeof(int), 1, output);

   
    free(BufferIn);
    free(BufferOut);
    for (int i = 0; i < 256; i++)
    {
        if (codes[i]) free(codes[i]);
    }
    freeHuffmanTree(huffmanTree);
}