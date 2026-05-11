# 🧠 Cute Huffman File Compressor & Decompressor

## 📌 Overview

Cute Huffman Compressor is a full file compression and decompression system implemented using the **Huffman Coding algorithm**.

The project consists of:

- **C Backend** – Implements the Huffman compression/decompression logic.
- **C# Windows Forms GUI** – User-friendly graphical interface.
- Real-time progress tracking.
- Custom themed design.
- Drag-and-drop functionality.

The system allows users to compress files into a custom format (`.ece2103`) and restore them back to their original state.

---

## Demo Video

A short demonstration video of the application workflow is included in this repository.

[Watch the Huffman Project Demo Video](huffman%20project%20demo%20video_compressed.mp4)

The demo showcases:
- Application startup and navigation
- Compression and decompression modes
- Drag-and-drop file handling
- Real-time progress tracking
- File restoration workflow
  
---

## 🏗 System Architecture

The project follows a **two-layer architecture**:

### 🔹 Backend (C – Huffman Engine)

Responsible for:
- Building the Huffman Tree
- Generating binary codes
- File encoding (compression)
- File decoding (decompression)
- Writing and reading custom file headers
- Bit-level data manipulation
- Buffer-based processing

The backend runs as a separate executable (`Huffy.exe`) and is called by the GUI using command-line arguments.

---

### 🔹 Frontend (C# – Windows Forms GUI)

Features:
- Welcome screen
- Mode selection (Compress / Decompress)
- Drag-and-drop interface
- Real-time progress bar (0% → 100%)
- Multi-file support
- Duplicate file protection
- Background music toggle
- Themed user interface

The GUI launches the backend process and reads progress output to update the interface dynamically.

---

## 🧮 Huffman Algorithm Implementation

### ✔ Compression Process

1. Count frequency of each byte (0–255).
2. Build a priority queue.
3. Construct the Huffman Tree.
4. Generate binary codes for each byte.
5. Encode file data bit-by-bit.
6. Write custom file header:
   - Magic number (`HUFF`)
   - Filename
   - Padding bits
   - Frequency table
7. Generate compressed file with `.ece2103` extension.

---

### ✔ Decompression Process

1. Read and validate file header.
2. Rebuild Huffman Tree from frequency table.
3. Decode binary data.
4. Restore the original file.

---

## ⚡ Performance Features

- Buffer-based file processing.
- Bit-level manipulation.
- Real-time progress calculation.
- ETA estimation during processing.
- Proper memory management and cleanup.

---

## 🎨 User Interface Features

- Welcome screen with start button.
- Mode selection screen.
- Drag-and-drop file area.
- Custom progress bars.
- Visual file indicators.
- Themed design.
- Background music integration.

---

## 📁 Project Structure

### Frontend (C#)

- WelcomeForm.cs
- ModeForm.cs
- DragDropForm.cs
- Program.cs
- Resources folder

### Backend (C)

- huffman.c
- compression.c
- decompression.c
- priority_queue.c
- huffman.h
- priority_queue.h
- Huffy.exe

---

## ▶ How To Run

### Requirements

- Windows OS
- Visual Studio
- .NET Framework
- C Compiler (to build backend)

### Steps

1. Compile the C backend to generate `Huffy.exe`.
2. Place `Huffy.exe` inside the `Resources` folder of the GUI project.
3. Open the C# solution in Visual Studio.
4. Build and run the application.
5. Choose:
   - Compress
   - Decompress
6. Drag files into the interface.
7. Click **Process**.

---

## 📌 Custom File Format
Compressed files use the extension:
.ece2103

The file format includes:
- Magic header (`HUFF`)
- Original filename
- Padding bits
- Frequency table
- Encoded binary data

---

## 🧠 Skills Demonstrated

- Data Structures (Priority Queue, Binary Tree)
- Huffman Coding Algorithm
- Bit-level Programming
- File I/O in C
- Cross-language integration (C + C#)
- Process management
- Multithreading in C#
- GUI Development
- Performance optimization
- Memory management

---

## 🎓 Project Purpose

This project was developed as an academic implementation of the Huffman Compression Algorithm, combining:

- Systems programming in C
- GUI development in C#
- Real-world file processing
- Software architecture design

---

## 👨‍💻 Technologies Used

- C
- C#
- .NET Windows Forms
- Huffman Coding Algorithm
- Priority Queue (Linked List Implementation)
- Bit Manipulation
- Process Communication


Compressed files use the extension:
