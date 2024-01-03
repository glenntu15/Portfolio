# Portfolio 
This is a collection of "hobby "projects" currently, or recently in development. 

## VoterDatabase
This is not actually a database but is a C++ version of a Python program developed as a project for Cloud Computing graduate class. The purpose is to create many disk accesses of varying block sizes 
to evaluate the performance on various types of Amazon Web Service instances. This C++ version was developed to compare the performance of C++ vs Python on Windows Subsystem for Linux. 

The program functions as follows.  A collection (1.2 million) voter registration records are read and saved on disk in random order in blocks on disk. A dictionary is constructed with the key being the voter ID and location of the record.
specified by the block number and offset in the block. Then 200,000 voter ID records, in random order, are read and the associated registration record is retrieved. 

The program this functions in two modes. The first is to read the data from a .csv file and construct the dictionary and the stored records. These are saved to disk.  The second mode is to load this dictionary in memory then read the list of voter IDs and retrieve the associated records. This second mode is selected by the single command line argument "-r".

## Some Github notes
'''…or create a new repository on the command line
echo "# Portfolio" >> README.md
git init
git add README.md
git commit -m "first commit"
git branch -M main
git remote add origin https://github.com/glenntu15/Portfolio.git
git push -u origin main
…or push an existing repository from the command line
git remote add origin https://github.com/glenntu15/Portfolio.git
git branch -M main
git push -u origin main
…or import code from another repository
You can initialize this repository with code from a Subversion, Mercurial, or TFS project.
'''
