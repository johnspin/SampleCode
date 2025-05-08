import RestReader as RR
import SearchTool as ST
import string as str
import sys
import os
import re

reader = None
search = None

def main():
    InitializeObjects()
    text = ''
    while(text != 'QUIT'):
        # if sys.version >= (3,):
        text = input(UiOptions)  # Python 3 and up
        # else:
        #    text = raw_input(UiOptions)  # Python 2.7 minor=13 and below

        if sys.platform == 'win32':
            os.system('cls')
        else:
            os.system('clear')

        text = str.upper(text).strip()
        ExecuteCommand(text)
        print ('')

UiOptions = """WriteReadActions (not case-sensitive):
    * Search for state/territory data by name:
          Type the name. e.g. Washington, IDAHO, oregon

    * Search for state/territory data by 2 letter abreviation: 
          Type the abbreviation - e.g. WA, id, Or, cA
    
    *  Refresh data:
          Command:  REFRESH
        
    *  Quit:
          Command:  QUIT
    >_ """

def InitializeObjects():
    global reader, search
    reader = RR.RestReader('https://en.wikipedia.org/wiki/Microsoft')  # ('http://services.groupkt.com/state/get/USA/all')
    resp = reader.GETdata()
    search = ST.SearchTool(resp)
    # reader.debug()
    # search.debug()

def RefreshData():
    global search, reader
    search.RefreshData(reader.GETdata())

def ExecuteCommand(text):
    if text == 'QUIT': return
    if text == 'REFRESH':
        RefreshData()
    elif text == '1':
        reader.debug()
    elif text == '2':
        search.debug()
    elif len(text) < 2:
        print("Insufficent input -->"+text+"<-- Please retry.")
    else:
        HandleStateSearch(text)

def HandleStateSearch(text):
    if len(text) == 2:
        SearchByAbbrev(text)
    else:
        SearchByStateName(text)

def SearchByAbbrev(text):
    global search
    element, success = search.SearchForKeyWithValue('abbr', text)
    PrintResults(text, element, success) # TODO: refactor up into HandleStateSearch? maybe

def SearchByStateName(text):
    global search
    element, success = search.SearchForKeyWithValue('name', text)
    PrintResults(text, element, success)

def PrintResults(text, element, success):
    print('')
    if success:
        print ("Request:" + text)
        finalCity = element['capital'] if IsLargestCityDate(element['largest_city']) else element['largest_city']
        print ('largest_city == ' + finalCity)
        print ('capital == ' + element['capital'])
    else:
        print ("No data found for entry: " + text)

p = re.compile('^.+\d, \d{4}$') # begining, 1 or more of any chars, 1 digit, a comma, a space, exactly 4 digits, end
def IsLargestCityDate(txt):
    return p.match(txt)


if __name__ == "__main__": 
    main()