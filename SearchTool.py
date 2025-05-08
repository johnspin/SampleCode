import string as str

class SearchTool(object):

    def __init__(self, data):
        self.data = data

    def SearchForKeyWithValue(self, key, value):
        # print 'value-->', value
        for entry in self.data:
            # print 'key-->', key, '   entry-->', entry

            if key in entry:
                # print 'entry[key]', entry[key]
                if str.upper(entry[key]) == value:
                    # print "FOUND entry", entry
                    return entry, True

        return None, False

    def RefreshData(self, data):
        self.data = data

    def debug(self):
        print ("==>Debug SearchTool<==")
        print (self.data[:520])