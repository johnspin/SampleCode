'''
my_dict = {"name": "John", "age": 30, "city": "New York"}

# Iterate over keys
for key in my_dict:
    print(key

# Iterate over values
for value in my_dict.values():
    print(value)

# Iterate over key-value pairs
for key, value in my_dict.items():
    print(key, value)
'''

#print(False)
#print(True)

testCases = {}
testCases[""] = 0
testCases["("] = 1 # validate test harness
testCases["()"] = 1
testCases["[]"] = 1
testCases["{}"] = 1
testCases["([{}]"] = 0
testCases["[{()}]"] = 1
testCases["{{{{{{{{"] = 0

#print(testCases)

def TestParens(targ):
    if targ == "":
        return 0
    
    ts = [] # test stack

    parenDepth = 0
    squareDepth = 0
    curlyDepth = 0
    for c in targ:
        if IsLeft(c):
            ts.append(c)
        if IsRight(c) and (ts.count == 0 or ts.pop):  #ts.count same func at if(stack)?
            pass

    return 1

def IsLeft(c):
    return c in "[{("

def GetLeft(c):
    return brackets.get(c, "")

def IsRight(c):
    return c in "]})"

def GetRight(c):
    return brackets2.get(c, "")

def ValidParen(c):
    return c in "{{()}}"

def samples(c):
    if IsLeft(c):
        return GetRight(c)
    
    if IsRight(c):
        return GetLeft(c)
    
    return ""

def RunTestCases(tc):
    for item in tc.items():
        print(f"testCase '{item[0]}' with expected result '{'valid' if bool(item[1]) else 'invalid'}'", end='  ')
        actual = TestParens(item[0])
        print(f"test case {'PASSES' if actual == bool(item[1]) else 'FAILS'}")

def RunTestCases2(tc):
    for key, item in tc.items():
        print(f"testCase '{key}' with expected result {'valid' if bool(item) else 'invalid'}", end='  ')
        actual = TestParens(key)
        print(f"test case {'PASSES' if actual == bool(item) else 'FAILS'}")


#RunTestCases(testCases)

brackets = { ')': '(', '}': '{', ']': '[' }  #keys=right/end parens  values=left/start parens
brackets2= { '(': ')', '{': '}', '[': ']' }  #keys=left/start parens  values=right/end parens

def is_valid(string):
    stack = []

    for char in string:
        if char in brackets.values():
            stack.append(char)
        elif char in brackets.keys():
            if not stack or stack.pop() != brackets[char]:
                return False
        else:
            continue  # Ignore non-bracket characters

    return not stack  # True if stack is empty

testList = [
    "({[]})",
    "([)]",
    "()[{}]",
    "((())))",
    "((()))",
    "((({{{[[[[]]]]]}}})))",
    "((({{{[[[[]]]][][]}}}({}))))",
    "((({{{[[[[]]]]]}})))",
    "((([]{{{[[[[]]]][][]()}{}}})))"
]

def RunIsValids(tl):
    for tc in tl:
        print(tc + " " + str(is_valid(tc)))

RunIsValids(testList)

'''
print(is_valid("({[]})"))
print(is_valid("([)]"))
print(is_valid("()[{}]"))
print(is_valid("((())))"))
print(is_valid("((({{{[[[[]]]]]}}})))"))
print(is_valid("((({{{[[[[]]]]]}})))")) 
'''