import pip._vendor.requests as r

class RestReader(object):

    def __init__(self, endpoint):
        self.endpoint = endpoint
        self.resp = None

    def GETdata(self, header={'Content-Type':'application/json'}):
        self.resp = r.get(self.endpoint,  headers=header)
        if 200 <= self.resp.status_code < 300:
            jsonResp = self.resp.json()  # json.loads(self.resp.content)

            if 'RestResponse' in jsonResp:
                if 'result' in jsonResp['RestResponse']:
                    return jsonResp['RestResponse']['result']

        raise Exception("No data returned or data is missing 'result' key.  Response code - {}   Original request - {}".format(self.resp.status_code, self.resp.request))
    def GETresponse(self):
        return self.resp

    def debug(self):
        print ("==>Debug RestReader<==")
        print (self.resp.content[:520])