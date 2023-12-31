#include "DbIO.h"
#include <string.h>
#include <time.h>
//#include <iostream>
//#include <fstream>

DbIO* DbIO::_dbio = NULL;
/// <summary>
/// This is the singleton
/// </summary>
/// <returns></returns>
DbIO* DbIO::GetInstance()
{
	if (DbIO::_dbio == nullptr) {
		DbIO::_dbio = new DbIO();
	}
	return _dbio;
};
/// <summary>
///  Opens the database binary file for input,
///  also sets the buffer pointer at the end of the buffer to force a read
///  before buffer is accessed.
/// </summary>
/// <param name="filename">
///  file to open
/// </param>
/// <returns>
/// true if successful, false if not
/// </returns>
bool DbIO::OpenRead(std::string filename)
{
	readdb.open(filename, std::ios::in | std::ios::binary);
	if (!readdb)
		return false;
	buffer_pos_ = BLKSIZE;
	isOpenRead_ = true;
	blocks_read_ = 0;
	return true;
}
/// <summary>
/// Opens the binary file for writing
/// </summary>
/// <param name="filename">bname of file to be opened</param>
/// <returns>true if succssful, false if not</returns>
bool DbIO::OpenWrite(std::string filename)
{
	writedb.open(filename, std::ios::out | std::ios::binary);
	if (!writedb)
		return false;
	buffer_pos_ = 0;
	isOpenWrite_ = true;
	blocks_written_ = 0;
	return true;
}
/// <summary>
/// 
/// This function is used to clear a partially full buffer.  An integral number of records need to
/// be put in the buffers, so we clear after writing the shorter dictionary entries before writing the
/// longer actual records
/// 
/// </summary>
/// <returns>
/// returns -1 if file not open, 1 if no need to write, else returns 0
/// </returns>
int DbIO::FlushPartialBuffer() 
{
	if (!isOpenWrite_)
		return -1;
	if (buffer_pos_ == 0)
		return 1;

	WriteRecord();
	return 0;
}
/// <summary>
/// This moves data from to the buffer to be written when the buffer is full
/// Record lengths are calulates so that an integral number of records go into a 
/// buffer, so, no overrun is calculated before copying into buffer. 
/// </summary>
/// <param name="data">record to be added to output buffer</param>
/// <param name="length">number of bytes</param>
void DbIO::OutputLogicalRecord(void* data, int length)
{
	if (buffer_pos_ >= BLKSIZE)
		WriteRecord();
	memcpy(buffer+buffer_pos_, data, length);
	buffer_pos_ += length;
}
/// <summary>
/// Writes the current buffer to the file
/// </summary>
void DbIO::WriteRecord()
{
	writedb.write(buffer, BLKSIZE);
	buffer_pos_ = 0;
	blocks_written_++;
	if (debug_out_)
	{
		Write_debug();
	}
}
void DbIO::Write_debug()
{
	std::ofstream writedbdb;
	writedbdb.open("c:\\tmp\\debug_block.bin", std::ios::out | std::ios::binary);
	writedbdb.write(buffer, BLKSIZE);
	writedbdb.close();
	debug_out_ = false;
}
/// <summary>
/// 
/// </summary>
/// <param name="record"></param>
/// <param name="length"></param>

void DbIO::ReadNextLogicalRecord(void* record, int length)
{
	if (buffer_pos_ == BLKSIZE) {
		ReadRecord(BLKSIZE);		// sets buffer_pos_ to zero
	}
	memcpy(record, buffer + buffer_pos_, length);
	buffer_pos_ += length;
}
int DbIO::ReadSpecificRecord(void* data, int length, int blockno, int offset)
{
	if (current_block_ != blockno) {
		ReadRecord(blockno, BLKSIZE);
	}
	memcpy(data, buffer + offset, length);
	//buffer_pos_ += length;
	return current_block_;
}
/// <summary>
/// overloaded function -- reads the next physical record
/// </summary>
void DbIO::ReadRecord(int size)
{
	readdb.read(buffer, size);
	buffer_pos_ = 0;
	blocks_read_++;
}
/// <summary>
/// reads a specific block number
/// </summary>
/// <param name="blockno">
/// block number to be read
/// </param>
void DbIO::ReadRecord(int blockno, int size)
{
	std::streampos p = blockno * size;
	readdb.seekg(p, std::ios_base::beg);
	//auto pos = readdb.tellg();	check seekg

	// assume open
	readdb.read(buffer, size);
	/*if (readdb.is_open())
		readdb.read(buffer, size);
	else
		std::cout << " stream not oppen" << std::endl;*/
	// may be an abuse of the overload of istream
	if (!readdb)
		std::cout << " ERROR ON READ" << std::endl;
	buffer_pos_ = 0;
	blocks_read_++;
	current_block_ = blockno;
}
/// <summary>
/// Just closes the file
/// </summary>
void DbIO::CloseFile()
{
	if (isOpenRead_) {
		readdb.close();
		isOpenRead_ = false;
	}
	if (isOpenWrite_) {
		if (buffer_pos_ >= 0) {
			WriteRecord();
			std::cout << " writing final record, count is " << blocks_written_ << std::endl;
		}
			
		writedb.close();
		isOpenWrite_ = false;
	}
		
}
