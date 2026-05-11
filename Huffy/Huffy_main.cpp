#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <math.h>
#include "huffman.h"

// We Used AI for The Special cases and Debugging //


int main(int argc, char* argv[])
{
  
    int mode = 0; // 0=invalid, 1=compress, 2=decompress
    char inputPath[256] = { 0 };
    char outputPath[256] = { 0 };
    int bufferSize = 65536;

    
    for (int i = 1; i < argc; i++)
    {
        if (strcmp(argv[i], "-c") == 0 && i + 2 < argc)
        {
            mode = 1;
            strncpy(inputPath, argv[++i], 255);
            strncpy(outputPath, argv[++i], 255);
        }
        else if (strcmp(argv[i], "-d") == 0 && i + 2 < argc)
        {
            mode = 2;
            strncpy(inputPath, argv[++i], 255);
            strncpy(outputPath, argv[++i], 255);
        }
        else if (strcmp(argv[i], "-b") == 0 && i + 1 < argc)
        {
            bufferSize = atoi(argv[++i]);
            if (bufferSize <= 0) {
                printf("Error: Invalid buffer size\n");
                return 1;
            }
        }
    }

    
    if (mode == 0)
    {
        printf("Usage:\n");
        printf("  Compression:   app.exe -c input.txt output.ece2103 [-b 8192]\n");
        printf("  Decompression: app.exe -d input.ece2103 output.txt [-b 8192]\n");
        return 1;
    }

    // Check if input and output paths are the same | the user should input the input file twice so that we can change the output extension
    if (strcmp(inputPath, outputPath) == 0)
    {
       // Output Extension 
        snprintf(outputPath, sizeof(outputPath), "%s.ece2103", inputPath);
    }

    FILE* input = fopen(inputPath, "rb");
    if (!input)
    {
        printf("Error: Cannot open input file '%s'\n", inputPath);
        return 1;
    }

    clock_t start = clock();

    if (mode == 1)
    {
        FILE* output = fopen(outputPath, "wb");
        if (!output)
        {
            printf("Error: Cannot create output file '%s'\n", outputPath);
            fclose(input);
            return 1;
        }

        compressFile(input, output, bufferSize, inputPath);
        fclose(output);
        printf("\n");
        printf("Compression complete. Output: %s\n", outputPath);

    }
    else if (mode == 2)
    {
        if (strstr(inputPath, ".ece2103") == NULL)
        {
            printf("Error: Input must be a .ece2103 file\n");
            fclose(input);
            return 1;
        }

        // If inputPath and outputPath are the same or user reused the .ece2103 path remove the extension
        if (strcmp(inputPath, outputPath) == 0 || strstr(outputPath, ".ece2103") != NULL)
        {
            strncpy(outputPath, inputPath, sizeof(outputPath));
            outputPath[sizeof(outputPath) - 1] = '\0';
            char* ext = strstr(outputPath, ".ece2103");
            if (ext != NULL)
            {
                *ext = '\0';  // Remove the .ece extension | our method Trashy
            }
        }

        FILE* output = fopen(outputPath, "wb");
        if (!output)
        {
            printf("Error: Cannot create output file '%s'\n", outputPath);
            fclose(input);
            return 1;
        }

        decompressFile(input, output, bufferSize);
        fclose(output);
        printf("\n");
        printf("Decompression complete. Output: %s\n", outputPath);

    }


    fclose(input);

    clock_t end = clock();
    printf("Time taken: %.2f seconds\n", (double)(end - start) / CLOCKS_PER_SEC);


    return 0;
}
