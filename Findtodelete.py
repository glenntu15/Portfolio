import os
import sys


#for dirName, subdirList, fileList in os.walk(rootDir):
##
#    for i in subdirList:
#        print(" next level i",i)
#    if (dirName == ".git"):
#        continue
#    for fname in fileList:
#        print('\t%s' % fname)

def ListThisDir(path):

    dirlist = os.listdir(path)
    for f in dirlist:
       
        fullsubpath = path + "/" + f
        isdir = os.path.isdir(fullsubpath)
        if isdir:
            if (f == ".vs"):
                print(" Need to delete: ",fullsubpath)
                print(" **************")
                print("")
            else:
                #print(" calling with ",fullsubpath)
                ListThisDir(fullsubpath)
            #print("        dir: ", f)
        
        else:
            #print("        file ",f)
            if (f.find(".circ") > 1):
                print("  ----> Check this one:   ", fullsubpath)
            if (f.find(".log") > 1):
                print(" Deleting: ",fullsubpath)
                os.remove(fullsubpath)
            if (f.find(".obj") > 1):
                print(" Deleting: ",fullsubpath)
                os.remove(fullsubpath)
            if (f.find(".tlog") > 1):
                print(" Deleting: ",fullsubpath)
                os.remove(fullsubpath)
            if (f.find(".exe") > 1):
                print(" Deleting: ",fullsubpath)
                os.remove(fullsubpath)
            loc = f.find(".cache")
            if (loc > 1 ):
                diff = len(f)  - loc;
                if (diff == 6):
                    print(" Deleting: ",fullsubpath)
                    os.remove(fullsubpath)

                #print(" file is: ",fullsubpath," diff is ",diff)
            if (f.find(".pdb") > 1):
                print(" Deleting: ",fullsubpath)
                os.remove(fullsubpath)
    
#------------------------------------------------------------------------------
#  Main program starts here
#------------------------------------------------------------------------------

rootDir ="c:/projects"
nargs = len(sys.argv)
print(" nargs = ",nargs)
#print("argv ", sys.argv)


if (nargs > 1):
    rootDir = sys.argv[1]
dirlevel = 0;

ListThisDir(rootDir)
    
