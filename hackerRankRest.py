#!/bin/python3

import math
import os
import random
import re
import sys
import pip._vendor.requests as r
import json


#
# Complete the 'getCapitalCity' function below.
#
# The function is expected to return a STRING.
# The function accepts STRING country as parameter.
# API URL: https://jsonmock.hackerrank.com/api/countries?name=<country>
# capital city count = 1 or 0 return name or -1
#{"page":1,"per_page":10,"total":1,"total_pages":1,"data":[{"name":"Afghanistan","nativeName":"افغانستان","topLevelDomain":[".af"],"alpha2Code":"AF","numericCode":"004","alpha3Code":"AFG","currencies":["AFN"],"callingCodes":["93"],"capital":"Kabul","altSpellings":["AF","Afġānistān"],"relevance":"0","region":"Asia","subregion":"Southern Asia","language":["Pashto","Dari"],"languages":["ps","uz","tk"],"translations":{"de":"Afghanistan","es":"Afganistán","fr":"Afghanistan","it":"Afghanistan","ja":"アフガニスタン","nl":"Afghanistan","hr":"Afganistan"},"population":26023100,"latlng":[33,65],"demonym":"Afghan","borders":["IRN","PAK","TKM","UZB","TJK","CHN"],"area":652230,"gini":27.8,"timezones":["UTC+04:30"]}]}
#<bound method Response.json of <Response [200]>>

def getCapitalCity(country):
    url = f"https://jsonmock.hackerrank.com/api/countries?name={country}"
    header = {'Content-Type':'application/json'}
    

    result = r.get(url, headers=header)
    print(result.status_code)
    #print(result.text)
    print(country)
    js = result.json()
    print(len(js["data"]))
    if (len(js["data"]) > 0):
        print(js["data"][0]["capital"])
    else:
        print("-1")
    

    #result = r.get(url, headers=header)
    #print(result.status_code)
    #print(result.text)
    #print(result.res)
    #jsonResp = result.json
    #print(jsonResp)
    #if 'RestResponse' in jsonResp:
     #   if 'result' in jsonResp['RestResponse']:
      #      print(jsonResp['RestResponse']['result'])

    #mymatch = re.match("capital\"\:\"([A-Za-z]+)\"\,\"", result.text)
    #mymatch = re.match("capital(.)", result.text)
    #print(mymatch)
    #resObj = result.text.
    #print()
    #r.Request.json = ""
    # Write your code here
    

if __name__ == '__main__':
    #fptr = open(os.environ['OUTPUT_PATH'], 'w')

    #country = input()
    countries = [ "Italy", "France", "Brazil", "Afganistan", "Afghanistan" ]

    for country in countries:
        result = getCapitalCity(country)

    #fptr.write(result + '\n')

    #fptr.close()
