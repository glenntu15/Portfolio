#pragma once
#include <string>

class BuildVoterDatabase
{
public:
	BuildVoterDatabase();							// default constructo
	int StartBuild();								// driver funciton

private:

	int dictionary_blocks;							// needed to calculate actual block number for records
	int BuildDictionary(std::string infile_name);   // returns number of entries
	int WriteDictionary(void);
	int PutRecordsInDatabase(std::string infile_name);
	int ParseLine(std::string line, unsigned int &vid, unsigned int &sosid);
};


