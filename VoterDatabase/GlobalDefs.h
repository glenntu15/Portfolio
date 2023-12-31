#pragma once
#ifndef _Global_defined
#define _Global_defined
#include <iostream>
//#include <map>
#include <unordered_map>

#define BLKSIZE 8192
#define LRECL 128
#define DRECL 16

#ifdef _CONSOLE                 // these wil be for Windows
static std::string DATABASE_NAME = "C:\\tmp\\database.bin";
static std::string INDEXFILE_NAME = "C: \\tmp\\index.bin";
static std::string DATA_PATH = "C:\\tmp\\";
#else
static std::string DATABASE_NAME = "/home/glenntu/data/database.bin";
static std::string INDEXFILE_NAME = "/home/glenntu/data/index.bin";
static std::string DATA_PATH = "/home/glenntu/data/";
#endif

// two methods of creating a struct -- just because
// dictentry is a object in the dictionary, the voter id (vid) is the key
typedef struct
{
    unsigned int sosid;
    int blockno;
    int offset;
} dictentry;
// dict record is what is written to disk. It contains the voter id
struct dictrecord
{
    unsigned int vid;
    unsigned int sosid;
    int blockno;
    int offset;
    dictrecord(int id, dictentry ent)
    {
        vid = id;
        sosid = ent.sosid;
        blockno = ent.blockno;
        offset = ent.offset;
    }
    dictrecord() 
    {
        vid = 0;
        sosid = 0;
        blockno = 0;
        offset = 0;
    }
};
// unordered_map is a dictionary like a python dictionary
static std::unordered_map<unsigned int, dictentry> dictionary_;
// with std:: map elements are sorted
//static std::map<unsigned int, dictentry> dictionary_;


#endif



