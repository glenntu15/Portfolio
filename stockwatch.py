
import sys
from collections import namedtuple
#from operator import attrgetter
import yfinance as yf

Stockentry = namedtuple("stockentry", 'sym name currentPrice change annyield')

def Returnstockdata(sysmbol):

    try:
        data = yf.Ticker(symbol).info
    except:
        print("ERROR symbol ",symbol," not found")
        exit()

    scurrentPrice = data['currentPrice']
    previousClose = data['previousClose']
    open = data['open']
    sname = data['shortName']
    if 'dividendRate' in data:
        rate = data['dividendRate']
        annualyield = (rate / previousClose) * 100.
    else:
        annualyield = 0.0
    schange = scurrentPrice -previousClose
   

    #print(sname, "current price %6.2f open %6.2f prev close %6.2f, change %+8.2f" % (scurrentPrice, open, previousClose, schange))

    se = Stockentry(sym=symbol, name = sname, currentPrice = scurrentPrice, change=schange, annyield = annualyield)
    #print(" se. ",se.name)
    return se


#----------------- Main starts here  -------------------------------------------
Dowsymbols = ["AXP",  "AMGN", "AAPL", "CAT", "CSCO",
              "CVX",  "GS",   "HD",   "HON",  "IBM",
              "INTC", "JNJ",  "KO",   "JPM",  "MCD",
              "MMM",  "MRK",  "MSFT", "NKE",  "PG",
              "TRV",  "UNH",  "CRM",  "VZ",   "V",
              "WBA",  "WMT",  "DIS",  "DOW",  "BA"]
Mysymbols =  ["AAPL", "RIO",  "XOM",  "SPOT", "NRG",
              "TGT",  "ET",   "FANG", "AES",  "LYB",
              "NWL"]

watchlist = []

n = len(sys.argv)
mylist = True
if (n > 1):
    if sys.argv[1] == '-d':
        mylist = False

if mylist == True:
    for symbol in Mysymbols:
        #print("trying: ",symbol)
        s = Returnstockdata(symbol)
        watchlist.append(s)
else:  
    for symbol in Dowsymbols:
        s = Returnstockdata(symbol)
        watchlist.append(s)

watchlist.sort(reverse=True, key = lambda a: a[3])
for s in watchlist:
    print(" %s \t %6.2f  %+7.2f %s (yield = %5.2f)" % (s.sym, s.currentPrice, s.change, s.name, s.annyield))
