This project was written using the Python 3.7 and pytest 9.1.1 libraries.


  SUMMARY
* supports incomplete games
* returns 2 lists:
    . each individual frame's scores
    . each frame's total score up to that point
* returns errors with descriptions for incorrectly formed games
* supports numbers with or without quotes
* suports x or X for strike (quotes required)
* spare is denoted / (quotes required)
* has custom test harness
* has parameterized pytest tests


  DETAILS
The BowlingScores.py file has a ScoreGame function that accepts a string of frames, converts it to a list of frames and then iterates through each frame. If a spare or strike is scored, a request for the pins from the next 1 or 2 rolls be added to that frame is put on a queue for later addition. Initially, it just gets the total for each indivual frame, but later the scores are totalled and both lists are returned.

It tests for game validity such as too long or too short frames or games, incorrect ordering of strikes, spares and pin counts both for the first 9 frames and for the complicated 10th frame.

It also supports incomplete games including the supplied example:  [ ["x"], [5,2] ] in which I found a bug in that the actual scoring is different than described in the statement of work.  If there were no other frames then the frames should look like this [ 17, 24 ] not [ 17, 22] since the [5,2] frame is added to the strike as well as itself.

There should be test cases (games) for each of these scenarios in the TestCases.txt file.


  HOW TO RUN
This project can be run 2 ways from the command prompt in the same directory as the project:

1) python TestRunner.py
This runs a custom test harness that uses the TestCases.txt file with the requisite positive and negative scenarios

2) python -m pytest -sv ./BowlingScores_test.py
This runs the pytest file which has some parameterized tests
