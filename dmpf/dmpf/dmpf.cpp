// dmpf.cpp : Defines the entry point for the console application.
//
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <io.h>
#include <fcntl.h>
// function prototypes
//
void printline(char *charline, char * hexline, int nprint);
void setprint(char * charline, char *hexline);
//
#define MAX 520
#define LINELEN 26
/*
*    **** Global variables   ****
*/

char *hexpoint;
char *charpoint;
int addr;
int  total;                             /* bytes printed so far*/
int main(int argc, char* argv[])
{
	//	FILE *fp;
	int len;						/* number of characters read */
	int max = MAX;					/* max buffer size           */
	int n, fd;						/* characer this buffer      */
	int nprint;						/* number printed on current line */
	char temp[3];
	char line[MAX];
	char *s;
	s = line;
	char hexline[LINELEN * 4];
	char charline[LINELEN + 1];

	argv++;
	if (argc < 2) {
		printf(" Usage: dmpf file.nam\n");
		exit(8);
	}
	if ((fd = _open(argv[0], (O_BINARY | O_RDONLY))) < 0)
	{
		printf(" cant open %s, errno = %d\n", argv[0], errno);
		exit(4);
	}
	printf(" %s opened successfully\n", argv[0]);
	setprint(charline, hexline);
	nprint = 0;
	total = 0;
	addr = 0;
	n = 0;
	for (;;) {

		len = _read(fd, line, max);

		//	printf(" len after eofcheck: %d \n",len);
		/* printf(" debug len = %d\n",len);*/
		if (len == 0)
		{
			if (nprint > 0)
			{
				printline(charline, hexline, nprint);
				setprint(charline, hexline);
			}
#ifdef _DEBUG
			printf(" zero length record,(max = %d)\n", max);
#endif
			exit(4);
		}
		//printf("len = %d\n", len);
		if (len < 0) {
			printf(" read error\n");
			exit(8);
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

	}  /* End of FOR loop on records */

	return 0;

}
/*******************************************************************/
void setprint(char * charline, char *hexline)
{
	int i;
	int charlen = (LINELEN * 2 + (LINELEN / 2)) + 1;
	for (i = 0; i<charlen; i++) {
		hexline[i] = ' ';
	}
	hexpoint = hexline;
	hexline[charlen] = '\0';

	for (i = 0; i<LINELEN; i++) {
		charline[i] = ' ';
	}
	charpoint = charline;
	return;
}
/*******************************************************************/
void printline(char *charline, char * hexline, int nprint)
{
	/* *hexpoint = '\0'; */
	charline[LINELEN] = '\0';
	total = total + nprint;
	printf("%6.6X   %s      %s\n", addr, hexline, charline);
	addr = addr + nprint;
	return;
}