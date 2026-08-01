#import collections  #.deque
from collections import deque
#import collections.deque([iterable[, maxlen]])
#from json import loads
import json

# [ ["8", "/"], ["5", "4"], ["9", "0"], ["X"], ["X"], ["5", "/"], ["5", "3"], ["6", "3"], ["9", "/"], ["9", "/", "X"] ]
# [ 15, 24, 33, 58, 78, 93, 101, 110, 129, 149 ]

def ScoreGame(frames):
    frames = json.loads(frames)
    if len(frames) > 10:
        raise ValueError(f"Standard rounds of bowling should not have more than 10 frames. Actual={len(frames)}")

    frame_score = [0] * len(frames)
    frame_queue = [] #deque()

    for i, frame in enumerate(frames):
        #find empty frames or too long frames
        #frames 1 - 9 are 1 or 2 long
        if i < 9 and (len(frame) < 1 or len(frame) > 2):
            raise ValueError(f"The first 9 frames must have only 1 or 2 rolls  Frame#={i+1}  Rolls={frame}")
        # 10th frame has different rules
        if i == 9 and (len(frame) < 1 or len(frame) > 3):
            raise ValueError(f"The 10th frame must have only 1, 2 or 3 rolls  Frame#={i+1}  Rolls={frame}")

        # a number roll is anything less than a strike or a spare
        # everytime there are 2 number rolls in a row, they cannot add up to more than 10, also a strike cannot follow a number roll
        number_roll = 0
        for j, roll in enumerate(frame):
            roll = str(roll)

            #checking 10th frame validity
            if (j == 2 and i == 9) and ((str(frame[1]) not in "xX/") and str(frame[0]) not in "xX"):
                raise ValueError(f"There is no 3rd roll on the tenth frame unless there is a strike on the 1st or 2nd roll or a spare on the 2nd roll. Current Frame#={i+1}  Roll#={j+1} Rolls={frame}")
            
            #handle spares, strikes, etc.
            if roll == "/":
                if j == 0 or number_roll == 0:
                    raise ValueError(f"A spare must always be the 2nd roll of frame (or the 2nd or 3rd roll of the final 10th frame). Frame#={i+1} Roll={j+1} Rolls={frame}")
                if j == 2 and i == 9 and frame[0] not in "xX":
                    raise ValueError(f"The third roll of the final 10th frame cannot be a spare unless the first roll is a strike.")
                pins = 10 - int(frame[j-1])
                frame_score[i] += pins
                number_roll = 0
                #mark this frame to get the score for 1 more roll
                frame_queue.append( { "toframe": i, "times": 1} )
            elif roll in "xX":
                if j > 0 and i < 9:
                    raise ValueError(f"A strike must always be the first and only roll of a frame except the final 10th frame. Frame#={i+1} Roll={j+1} Rolls={frame}")
                if i == 9 and number_roll == 1:
                    raise ValueError(f"A strike cannot be after non-spare or non-strike  Frame#={i+1} Rolls={frame}")
                pins = 10
                frame_score[i] += pins
                #mark this frame to get the score for 2 more rolls
                frame_queue.append( { "toframe": i, "times": 2} )
            elif roll.isnumeric():
                # if on 2nd roll (non-spare), check total of 2 rolls
                number_roll += 1
                pins = int(roll)
                if number_roll > 1 and pins + int(frame[j-1]) > 9:
                    raise ValueError(f"The total of 2 non-spare rolls cannot be more than 9  Frame#={i+1}  Frame={frame}")
                if (0 <= pins <= 9):  # and is 2nd roll and total is too much:
                    frame_score[i] += pins
                else:
                    raise ValueError(f"non-spare or non-strike frame values must be from 0 to 9. Actual={pins}")
            else:
                raise ValueError(f"Non-valid roll value entered. Frame#={i+1} Value={roll}")

            # run once for each roll for each previous frame that needs this pins
            for k, fq in enumerate(frame_queue):
                if fq["toframe"] < i:
                    frame_score[fq["toframe"]] += pins
                    fq["times"] -= 1
                    if fq["times"] == 0:
                        fq["toframe"] = 1000  #removing while iterating is a bad idea
                        #del frame_queue[k]


    frame_totals = [ 0 ] * len(frames)
    frame_totals[0] = frame_score[0]
    for i in range(1,len(frames)):
        frame_totals[i] = frame_totals[i-1] + frame_score[i]
    return frame_score, frame_totals
