CPPFLAGS = -c -O2
COMPILER = g++
SOURCE = VoterDatabase.cpp ReadVoterDatabase.cpp BuildVoterDatabase.cpp DbIO.cpp
OBJS = VoterDatabase.o BuildVoterDatabase.o DbIO.o ReadVoterDatabase.o
VoterDatabase: $(OBJS)
	$(COMPILER) -o $@ $(OBJS)

$(OBJS) : $(SOURCE) GlobalDefs.h DbIO.h ReadVoterDatabase.h BuildVoterDatabase.h
	$(COMPILER) -c $(CPPFLAGS) $(SOURCE)
clean:
	rm *.o
	rm VoterDatabase

