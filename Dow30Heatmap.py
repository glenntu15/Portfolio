import sys
import matplotlib.pyplot as plt
import numpy as np
import seaborn as sns
import yfinance as yf
from matplotlib.colors import ListedColormap
nrows = 6
ncols = 5
import matplotlib.pyplot as plt
Dowsymbols = [["AXP",  "AMGN", "AAPL", "CAT", "CSCO"],
             ["CVX",  "GS",   "HD",   "HON",  "IBM"],
             ["INTC", "JNJ",  "KO",   "JPM",  "MCD"],
             ["MMM",  "MRK",  "MSFT", "NKE",  "PG"],
             ["TRV",  "UNH",  "CRM",  "VZ",   "V"],
             ["WBA",  "WMT",  "DIS",  "DOW",  "BA"]]

#color_numbers =[[0 for _ in range(nrows)] for _ in range(ncols)]
color_numbers = np.random.randint(0, 3, (6, 5))
templist = []
for i in range(nrows):
    for j in range(ncols):
        symbol = Dowsymbols[i][j]
        tk = yf.Ticker(symbol)
        cp = tk.fast_info["lastPrice"]
        pc = tk.fast_info["previousClose"]
        change = cp - pc
        t = (symbol,change)
        templist.append((symbol,change))
        
sorted_by_price = sorted(templist, key=lambda x: x[1], reverse=True)
index = 0    
for i in range(nrows):
    for j in range(ncols):       
        (symbol,change) = sorted_by_price[index]
        index += 1
        sss = f"{symbol}\n {change:.2f}"
        Dowsymbols[i][j] = sss
        #print("symbol: ", sss)
        if change > 0:
            color_numbers[i][j] = 1
        elif (abs(change) < .001):
            color_numbers[i][j] = 0
        else:
            color_numbers[i][j] = -1
            
            
                
#print ("color_numbers: ", color_numbers)
#("Dowsymbols = ",Dowsymbols)
cmap = ListedColormap(['red', 'grey', 'green']) # (['crimson', 'deepskyblue', 'palegoldenrod', 'midnightblue'])
ax = sns.heatmap(color_numbers, cmap=cmap, center= 0.0, annot=Dowsymbols, xticklabels = False, yticklabels = False, fmt='s', cbar=False)
ax.tick_params(left=False, bottom=False)
plt.show()
