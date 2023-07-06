#include "ReadVoterDatabase.h"

#include <iostream>
#include <iomanip>
#include <fstream>
#include <sstream>
#include <string.h>
#include <time.h>

#include "GlobalVars.h"
#include <time.h>
#include "DbIO.h"

/// <summary>
/// constructor
/// </summary>
ReadVoterDatabase::ReadVoterDatabase()
{ 
	dictionary_block_offset_ = 0; 
};
/// <summary>
/// Driver for reading database
/// </summary>
void ReadVoterDatabase::BeganRead()
{
	std::cout << " Rebuilding Dictionary\n";

	std::clock_t c_start = std::clock();
	int total = RebuildDictionary();
	std::clock_t c_end = std::clock();

	double time_elapsed_seconds = (double)(c_end - c_start) / (double)CLOCKS_PER_SEC;
	std::cout << " Number of records added to dictionary: " << total << std::endl;
	std::cout << std::fixed;
	std::cout << std::setprecision(2);
	std::cout << " CPU time to build dictionary: " << time_elapsed_seconds << "s\n\n";


	c_start = std::clock();
	int num = ProcessKeys();
	c_end = std::clock();

	time_elapsed_seconds = (double)(c_end - c_start) / (double)CLOCKS_PER_SEC;
	std::cout << std::fixed;
	std::cout << std::setprecision(2);
	std::cout << " CPU time to process keys: " << time_elapsed_seconds << "s\n";
#ifdef _DEBUG
	std::cout << " Number of keys processed: " << std::dec << num << std::endl;
#else
	std::cout << " Number of keys processed: " << num << std::endl;
#endif 

}
/// <summary>
/// 
/// </summary>
/// <returns></returns>
int ReadVoterDatabase::RebuildDictionary()
{
	int header[4] = { 0, 0, 0, 0 };
	//int datarecords = 0;
	DbIO* pDb = DbIO::GetInstance();
	std::cout << " Opening database: " << glbl::database_name << std::endl;
	pDb->OpenRead(glbl::database_name);

// get a physical record into the buffer

int rcode = pDb->ReadNextLogicalRecord(header, DRECL);
#ifdef _DEBUG
std::cout << " dictionary entries " << header[0] << " BLKSIZE " << header[1] << std::endl;
std::cout << " LRECL " << header[2] << " DRECL " << header[3] << std::endl;
#endif 

// make sure current parmaters are consistent with what was used int the database
// since the buffer must be allocated before reading we just hard code the size, 
// - we can do some realloication as a future improvement
if (header[1] != BLKSIZE) {
	std::cout << " *** ERROR ***\n" << " Inconsistent database blocksize - ending\n";
	return -1;
}
if (header[2] != LRECL) {
	std::cout << " *** ERROR ***\n" << " Inconsistent database logical record length - ending\n";
	return -1;
}
if (header[3] != DRECL) {
	std::cout << " *** ERROR ***\n" << " Inconsistent database dictionary record length - ending\n";
	return -1;
}
//  Calculate the number of blocks used by the dictionary - added to block number for records for actual block 
int nbytes = (header[0] * DRECL);
dictionary_block_offset_ = nbytes / BLKSIZE;
if ((nbytes % BLKSIZE) != 0)
dictionary_block_offset_++;
//dictionary_block_offset_++;		// add one to number of blocks to get first data block

dictrecord record;

for (int i = 0; i < header[0]; i++) {
	pDb->ReadNextLogicalRecord(&record, DRECL);
	//std::cout << " vid " << record.vid << std::endl;
	dictentry* pent = new dictentry();
	pent->sosid = record.sosid;
	pent->blockno = record.blockno;
	pent->offset = record.offset;

	glbl::dictionary[record.vid] = *pent;

	delete pent;
}
int num_entries = static_cast<int>(glbl::dictionary.size());
#ifdef _DEBUG
std::cout << " -debug- Number of dictionary entries " << num_entries << std::endl;
#endif
return num_entries;
}
/// <summary>
/// 
/// </summary>
/// <returns></returns>
int ReadVoterDatabase::ProcessKeys()
{
	int num_processed = 0;
	std::fstream file;
	std::string line;
	std::string word;

	char ret_record[LRECL];
	int iend = LRECL - 1;

	DbIO* pDb = DbIO::GetInstance();
	//pDb->OpenRead(glbl::database_name);

	std::string input_file = glbl::data_path + std::string("searchkeys.txt");
	std::cout << "  - processing file: " << input_file << std::endl;
	file.open(input_file, std::ios::in);
#ifdef _DEBUG
	unsigned int iddb = 80609209;
	dictentry ent = glbl::dictionary.at(iddb);
	//std::cout << " block " << ent.blockno << " offset " << ent.offset << std::endl;
	pDb->ReadSpecificRecord(&ret_record, LRECL, ent.blockno + dictionary_block_offset_, ent.offset);
	ret_record[iend] = NULL;
	std::cout << ret_record << std::endl;
	DebugPrintRecord(ret_record, LRECL);

	iddb = 84610229;
	ent = glbl::dictionary.at(iddb);
	//std::cout << " block " << ent.blockno << " offset " << ent.offset << std::endl;
	pDb->ReadSpecificRecord(&ret_record, LRECL, ent.blockno + dictionary_block_offset_, ent.offset);
	ret_record[iend] = NULL;
	std::cout << ret_record << std::endl;
#endif
	while (getline(file, line))
	{
		std::stringstream str(line);
		// this may need to be changed for other file formats in these files
		// the first word is "active" status, second word is voter id, third word is sosid
		getline(str, word, ',');
		getline(str, word, ',');

		std::string::size_type sz;
		long int lvid = std::stol(word, &sz);
		unsigned int id = static_cast<unsigned int>(lvid);

		dictentry ent = glbl::dictionary.at(id);
		//std::cout << " block " << ent.blockno << " offset " << ent.offset << std::endl;
		pDb->ReadSpecificRecord(&ret_record, LRECL, ent.blockno + dictionary_block_offset_, ent.offset);
		if ((num_processed % 2000) == 0){
			ret_record[iend] = '\0';
			std::string word;
			int nfields = 0;
			std::stringstream str(line);
			// This may need to be changed for other file formats in these files
			// the first word is "active" status, second word is Voter ID, third word is SOSID
			getline(str, word, ',');
			getline(str, word, ',');

			//std::cout << "debug vid string is " << word << std::endl;
			//const char* pword = word.c_str();
			//long int lvid = strtol(pword, NULL, 0);

			std::string::size_type sz;
			long int lvid = std::stol(word, &sz);

			int dbvid = static_cast<unsigned>(lvid);
			if (dbvid != id) {
				std::cout << " error: id = " << id << " but record shows id: " << dbvid << std::endl;
			}
		}
		//std::cout << ret_record << std::endl;
		num_processed++;
	}
	file.close();
	return num_processed;
}
void ReadVoterDatabase::DebugPrintRecord(char* data, int lrecl)
{
	for (int i = 0; i < lrecl; i++) {
		std::cout << std::dec << i << ".  " << *(data + i) << " X: " << std::hex << (int) * (data + i) << std::endl;
	}
}