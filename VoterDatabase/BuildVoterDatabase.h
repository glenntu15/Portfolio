#ifndef _Build_defined
#define _Build_defined

#include "GlobalDefs.h"

class BuildVoterDatabase
{
public:
	BuildVoterDatabase();							// default constructo
	int BeganBuild();								// driver function

	//
private:
	int BuildWithSingleFile();
	int BuildWithIndexFile();
	int dictionary_blocks;							// needed to calculate actual block number for records
	int BuildDictionary(std::string infile_name, int &status_flag, int &current_block, int &current_offset);   // returns number of entries, may set status flag
	int WriteDictionary(void);
	int PutRecordsInDatabase(std::string infile_name);
	int ParseLine(std::string line, unsigned int &vid, unsigned int &sosid);
	
	int BuildInexandDataFiles(std::string path, int& status_flag, int& current_block, int& current_offset);

	//static std::unordered_map<unsigned int, dictentry> dictionary_;
};
#endif 

