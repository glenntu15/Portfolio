// VoterDatabase.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
#include "GlobalDefs.h"
#include "BuildVoterDatabase.h"
#include "ReadVoterDatabase.h"
#include <stdio.h>

int main(int argc, char* argv[])
{
 
    static int mode = 0;  // 0 is build database 1 is read database
    static int useIndexFile = 0;  // 0 is do not use index file

    std::ios::sync_with_stdio(false);

    if (argc > 1) {
        std::string arg1 = argv[1];
        if (arg1 == "-r") {
            mode = 1;
        }
	    // the -i option is only for building the database it would not be used with -r
	    else if (arg1 == "-i") {
    	    useIndexFile = 1;
        }
    }


    if (mode ==0) {
        std::cout << " Running to build database with block size " << BLKSIZE << std::endl;
        if (useIndexFile == 1)
            std::cout << " Will use index file " << INDEXFILE_NAME << std::endl;
        BuildVoterDatabase* builder = new BuildVoterDatabase();
        builder->BeganBuild();
    }
    else {
        std::cout << " Running to read database" << std::endl;
        ReadVoterDatabase* reader =  new ReadVoterDatabase();
        reader->BeganRead();
    }
    
}

