#!pip install yfinance

import datetime
import sys
from datetime import date
import yfinance as yf

n = len(sys.argv)
verbose = False
if (n > 1):
    symbol = sys.argv[1]
    if (n > 2):
        if sys.argv[2] == '-v':
            verbose = True
else:
    symbol = 'XOM'
data = yf.Ticker(symbol).info
print("longname: ", data['longName'])
if verbose == True:
    for k, n in data.items():
        print(k, n)
currentPrice = data['currentPrice']
previousClose = data['previousClose']
open = data['open']
name = data['shortName']
if 'dividendRate' in data:
    rate = data['dividendRate']
    annualyield = (rate / previousClose) * 100.
else:
    annualyield = 0.0

change = currentPrice -previousClose
#print(name, "  price: ",currentPrice, " change ",(currentPrice-open), "open: ", open, " prev close: ", previousClose)
print(name, ": Current price %6.2f Open %6.2f Prev close %6.2f, Change %+8.2f (yield = %5.2f)" % (currentPrice, open, previousClose, change, annualyield))


#print(" close",data.loc['Close'])

