// dmpf.c
// This program reads a binary file and prints out the contents in dump format: that is hex codes and chars
// It builds a string of hex codes LINLEN long and a string of characters.  These are then printed side by side.
//

#include <fcntl.h>
#include <stdio.h>
#include <stdlib.h>
#ifdef _WINDOWS
#include <io.h>
#else
#include <unistd.h>
#endif
//    **** function prototypes
//
void printline(char* charline, char* hexline, int nprint);
void setprint(char* charline, char* hexline);
#define MAX 520
#define LINELEN 28

//    **** Global variables   ****

char* hexpoint;
char* charpoint;
int addr;
int  total;                             /* bytes printed so far*/
///
// **** Main Program ****
///
int main(int argc, char* argv[])
{
	int len, n, nprint;
	char temp[4];
	char line[MAX];
	char* s;
	s = line;
	char hexline[LINELEN * 4];
	char charline[LINELEN + 1];

	int fd;									// file descriptior

	if (argc < 2) {
		printf(" Usage: dmpf file.name\n");
		return -1;
	}

	argv++;
	printf(" dmpf version 1.2 for file %s \n", argv[0]);

#ifdef _WINDOWS
	fd = _open(argv[0], (O_BINARY | O_RDONLY));
#else
	fd = open(argv[0], O_RDONLY);
#endif
	if (fd < 0) {
		printf("Unable to open file! %s", argv[0]);
		return 1;
	}
	setprint(charline, hexline);
	nprint = 0;
	total = 0;
	addr = 0;
	n = 0;

	for (;;) {

#ifdef _WINDOWS
		len = _read(fd, line, MAX);
#else
		len = read(fd, line, MAX);
#endif

		//	printf(" len after eofcheck: %d \n",len);
		/* printf(" debug len = %d\n",len);*/
		if (len == 0)
		{
			if (nprint > 0)
			{
				printline(charline, hexline, nprint);
				setprint(charline, hexline);
			}
			printf(" zero length record,(max = %d)\n", MAX);
			exit(0);
		}
		else if (len < 0) {
			printf(" read error\n");
			return 1;
		}
		s = line;
		n = 0;
		do {
			sprintf(temp, "%2.2X", *s);
			/* printf("writing %s to %X\n",temp,hexpoint); */
			*hexpoint = temp[0];
			hexpoint++;
			*hexpoint = temp[1];
			hexpoint++;
			if ((*s >= ' ') && (*s <= '~')) {
				*charpoint = *s;
			}
			else {
				*charpoint = '.';
			}
			charpoint++;
			nprint++;
			s++;
			if (nprint % 2 == 0) {
				hexpoint++;               /* blank between halfwords */
			}
			if (nprint % LINELEN == 0) {
				/* printf("%5.5X,%s      %s\n",total,hexline,charline); */
				printline(charline, hexline, nprint);
				setprint(charline, hexline);
				nprint = 0;
			}  /* End of if end of line */
		} while (++n <= len);
	}
#ifdef _WINDOWS
	_close(fd);
#else
	close(fd);
#endif
	
	return 0;
}
/*****************************************************************************/
void setprint(char* charline, char* hexline)
{
	int i;
	int charlen = (LINELEN * 2 + (LINELEN / 2)) + 1;
	for (i = 0; i < charlen; i++) {
		hexline[i] = ' ';
	}
	hexpoint = hexline;
	hexline[charlen] = '\0';

	for (i = 0; i < LINELEN; i++) {
		charline[i] = ' ';
	}
	charpoint = charline;
	return;
}
/*****************************************************************************/
void printline(char* charline, char* hexline, int nprint)
{
	// null terminate before priting
	charline[LINELEN] = '\0';
	total = total + nprint;
	printf("%6.6X   %s      %s\n", addr, hexline, charline);
	addr = addr + nprint;
	return;
}
