#include "BuildVoterDatabase.h"

#include <iostream>
#include <iomanip>
#include <fstream>
#include <sstream>
#include <string.h>
#include <time.h>
#include "DbIO.h"

#define NUM_FILES 5			// for debugging use this many files for input

BuildVoterDatabase::BuildVoterDatabase()
{
	dictionary_blocks = 0;
}
int BuildVoterDatabase::BeganBuild()
{
	return BuildWithSingleFile();
}
int BuildVoterDatabase::BuildWithSingleFile()
{
	
	int status_flag = 0;
	int current_block = 0;
	int current_offset = 0;
	std::cout << " Starting build\n";
	dictionary_.clear();
	int total = 0;

	std::string DATA_FILE_NAME_ROOT = "registered_voters";	// Might also be "test_file"
	//std::string DATA_FILE_NAME_ROOT = "test_file";

	std::clock_t c_start = std::clock();
	for (int i = 0; i < NUM_FILES; i++) {
		std::string path = DATA_PATH + DATA_FILE_NAME_ROOT + std::to_string(i) + ".csv";

		total += BuildDictionary(path, status_flag, current_block, current_offset);

	}
	std::clock_t c_end = std::clock();
	double time_elapsed_seconds = (double)(c_end - c_start) / (double)CLOCKS_PER_SEC;
	std::cout << " Number of records added to dictionary: " << total << std::endl;
	std::cout << std::fixed;
	std::cout << std::setprecision(2);
	std::cout << " CPU time to build dictionary: " << time_elapsed_seconds << "s\n\n";

	c_start = std::clock();
	int calculated_blocks_needed = WriteDictionary();
	c_end = std::clock();
	time_elapsed_seconds = (double)(c_end - c_start) / (double)CLOCKS_PER_SEC;
	std::cout << std::fixed;
	std::cout << std::setprecision(2);
	std::cout << " CPU time to write dictionary: " << time_elapsed_seconds << "s\n";

	DbIO* pDb = DbIO::GetInstance();
	int dbblocks = pDb->GetBlocksWritten();
	std::cout << " Calculated datbase blocks used for dictionary: " << calculated_blocks_needed << "\n";
	std::cout << " Actual datbase blocks used for dictionary: " << dbblocks << "\n\n";

	total = 0;
	c_start = std::clock();
	for (int i = 0; i < NUM_FILES; i++) {
		std::string path = DATA_PATH + DATA_FILE_NAME_ROOT + std::to_string(i) + ".csv";

		total += PutRecordsInDatabase(path);
	}
	c_end = std::clock();
	time_elapsed_seconds = (double)(c_end - c_start) / (double)CLOCKS_PER_SEC;

	pDb->CloseFile();

	std::cout << " Number of records added to database: " << total << std::endl;
	std::cout << " Number of blocks written: " << pDb->GetBlocksWritten() << std::endl;
	std::cout << std::fixed;
	std::cout << std::setprecision(2);
	std::cout << " CPU time to build database: " << time_elapsed_seconds << "s\n" << std::endl;
	return status_flag;
}
int BuildVoterDatabase::BuildDictionary(std::string infile_name, int& status_flag, int& current_block, int& current_offset)
{
	int num = 0;
	//int current_block_ = 0;
	//int current_offset = 0;
	std::string line;

	std::cout << "  - processing file: " << infile_name << std::endl;
	std::fstream file;
	file.open(infile_name, std::ios::in);
	unsigned int vid, sosid;

	// skip header
	getline(file, line);

	if (file.is_open())
	{
		// this is the createion of the DbIO class instance
		//DbIO* dbfile = DbIO::GetInstance();

		
		while (getline(file, line))
		{
			
			ParseLine(line, vid, sosid);
		
			dictentry* pent = new dictentry();
			pent->sosid = sosid;
			pent->blockno = current_block;
			pent->offset = current_offset;
			//if (dictionary_.find(vid) != dictionary_.end()) {
			//	std::cout << "Key found " << vid << std::endl;
			//}
			dictionary_[vid] = *pent;
			//nadded++;
			//dictrecord* precord = new dictrecord(vid, *pent);
			//dbfile->OutputLogicalRecord((void*)pent, DRECL);
			delete pent;
			//delete precord;

			num++;
			current_offset += LRECL;
			if (current_offset >= BLKSIZE) {
				current_block += 1;
				current_offset = 0;
			}
		}
	}
	else {
		std::cout << "***> Could not open the file:" << infile_name << "\n";
		status_flag++;
	}
		
	file.close();
	//std::cout << "debug number added to dict: " << nadded << std::endl;
	return num;
}

/// <summary>
/// Writes the map entries as records in the database file
/// </summary>
/// <param name=""></param>
/// <returns></returns>
int BuildVoterDatabase::WriteDictionary(void)
{ 
	unsigned int vid;
	dictentry ent;

	DbIO* pDb = DbIO::GetInstance();

	std::cout << " Opening database: " << DATABASE_NAME << std::endl;
	pDb->OpenWrite(DATABASE_NAME);

	// before the dictionary 

	int num_entries = static_cast<int>(dictionary_.size());
#ifdef _DEBUG
	std::cout << " -debug- Number of dictionary entries " << num_entries << std::endl;
#endif

	// first write one record giving: number of dict entries, block size, logical record size, and dictionary record size 
	int header[4];
	header[0] = num_entries;
	header[1] = BLKSIZE;
	header[2] = LRECL;
	header[3] = DRECL;
	// we assume this is the size: DRECL -- the size of all dictionary entries written 
	pDb->OutputLogicalRecord(header, DRECL);


	// iterate over dictionary and write each to buffer
	for (auto i = dictionary_.begin(); i != dictionary_.end(); i++) {
		vid = i->first;
		ent = i->second;
		//std::cout << " id " << vid << " sosid " << ent.sosid << std::endl;
		dictrecord* record = new dictrecord(vid, ent);
		pDb->OutputLogicalRecord(record, DRECL);
	}
	pDb->FlushPartialBuffer();
//  Calculate the number of blocks used by the dictionary - to compare to actual
	int nbytes = (num_entries * DRECL);
	int num_blocks = nbytes / BLKSIZE;
	if ((nbytes % BLKSIZE) != 0)
		num_blocks++;
	return num_blocks;
}
/// <summary>
/// This is a routine to parse the voter record
/// </summary>
/// <param name="line">input </param>
/// <param name="vid">voter id is set</param>
/// <param name="sosid">sos id is set</param>
/// <returns></returns>
int BuildVoterDatabase::ParseLine(std::string line, unsigned int& vid, unsigned int& sosid)
{
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

	vid = static_cast<unsigned>(lvid);
	getline(str, word, ',');
	long lsosid = strtoul(word.c_str(), NULL, 0);
	sosid = (unsigned)lsosid;


	return nfields;
}
/// <summary>
/// This function reads voter records from a file and stacks them in the database
/// </summary>
/// <param name="infile_name"></param>
/// <returns></returns>
int BuildVoterDatabase::PutRecordsInDatabase(std::string infile_name)
{
	int count = 0;
	int len, padlength;
	std::cout << " - processing file: " << infile_name << std::endl;
	std::fstream file;
	file.open(infile_name, std::ios::in);
	std::string line;

	std::string blanks(128, ' ');
	//const char *pblanks = blanks.c_str();

	char lineout[LRECL];

	// this is the createion of the DbIO class instance
	DbIO* pDb = DbIO::GetInstance();

#ifdef DEBUG_
	pDb->SetUpDebugout();
#endif
	// skip header
	getline(file, line);

	if (file.is_open())
	{
		while (getline(file, line))
		{
			len = static_cast<int>(line.length());
			if (len >= LRECL) {
				len = LRECL;
				memcpy(lineout, line.c_str(), len);
			}
			else {
				memcpy(lineout, line.c_str(), len);
				padlength = LRECL - len;
				memcpy(lineout+len-1, blanks.c_str(), padlength);
			}
			pDb->OutputLogicalRecord(lineout, LRECL);
			count++;

		}
	}
	return count;
}
int BuildVoterDatabase::BuildWithIndexFile()
{
	std::string DATA_FILE_NAME_ROOT = "registered_voters";	// Might also be "test_file"
	//std::string DATA_FILE_NAME_ROOT = "test_file";
	int total = 0;
	int status_flag = 0;
	int current_block = 0;
	int current_offset = 0;
	std::clock_t c_start = std::clock();
	for (int i = 0; i < NUM_FILES; i++) {
		std::string path = DATA_PATH + DATA_FILE_NAME_ROOT + std::to_string(i) + ".csv";

		total += BuildInexandDataFiles(path, status_flag, current_block, current_offset);
		
	}
	std::clock_t c_end = std::clock();
	double time_elapsed_seconds = (double)(c_end - c_start) / (double)CLOCKS_PER_SEC;
	return 0;
}
int BuildVoterDatabase::BuildInexandDataFiles(std::string path, int& status_flag, int& current_block, int& current_offset)
{
	int total = 0;

	return total;
}
