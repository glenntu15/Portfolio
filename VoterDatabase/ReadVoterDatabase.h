#pragma once
#include <string>


class ReadVoterDatabase
{
private: 

	int dictionary_block_offset_;
	void DebugPrintRecord(char* data, int lrecl);

public:

	ReadVoterDatabase();
	void BeganRead();
	int RebuildDictionary();
	int ProcessKeys();

};

