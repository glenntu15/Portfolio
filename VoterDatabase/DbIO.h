#pragma once
#ifndef _dbio_defined
#define _dbio_defined
#include <iostream>
#include <iomanip>
#include <fstream>
#include "GlobalVars.h"


// all binary IO performed through this class
// this is a singleton
// it keeps the buffer for IO with the database file and it keeps the buffer pointer


class DbIO
{
private:
	char* buffer{};
protected:

	DbIO()						// protected default constructor
	{
		buffer = new char[BLKSIZE];
		for (int i = 0; i < BLKSIZE; i++) {
			buffer[i] = ' ';
		}
	}

	static DbIO* _dbio;

	std::ofstream writedb;
	std::ifstream readdb;

	int buffer_pos_ = 0;
	int blocks_read_ = 0;
	int blocks_written_ = 0;
	int current_block_ = 0;
	bool isOpenRead_ = false;
	bool isOpenWrite_ = false;

	bool debug_out_ = false;

/// @brief Outputs the physical record from the also protected buffer
/// @return None
	void WriteRecord();
	void ReadRecord(int blockno);
	void ReadRecord();

public:
	// singleton stuff

	static DbIO* GetInstance();
	// destructor closes files and deletes buffer array
	~DbIO()
	{
		readdb.close();
		writedb.close();
	};
	// accessor
	
	int GetBlocksWritten() { return blocks_written_; }
	int GetBlocksRead() { return blocks_read_; }

	bool OpenRead(std::string filename);
	bool OpenWrite(std::string filename);
	int FlushPartialBuffer();
	void CloseFile();

	void OutputLogicalRecord(void* data, int length);
	int ReadNextLogicalRecord(void* data, int length);
	int ReadSpecificRecord(void* data, int length, int blockno, int offset);

	void SetUpDebugout() { debug_out_ = true; }
	void Write_debug();

};
#endif

