#ifndef Read_defined
#define Read_defined
#include <string>

#include <unordered_map>
#include "GlobalDefs.h"

class ReadVoterDatabase
{
private: 

	int dictionary_block_offset_;
	void DebugPrintRecord(char* data, int lrecl);

	//static std::unordered_map<unsigned int, dictentry> dictionary_;

public:

	ReadVoterDatabase();
	void BeganRead();
	int RebuildDictionary();
	int ProcessKeys();

};
#endif
