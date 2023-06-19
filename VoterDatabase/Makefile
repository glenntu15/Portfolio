CPPFLAGS = -c -O2
COMPILER = g++
OBJS = VoterDatabase.o BuildVoterDatabase.o DbIO.o ReadVoterDatabase.o
VoterDatabase: $(OBJS)
	$(COMPILER) -o $@ $(OBJS)
$(OBJS) : GlobalVars.h
$(OBJS) : DbIO.h
clean:
	rm *.o
	rm VoterDatabase

