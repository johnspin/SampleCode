import pytest
import BowlingScores
import json

@pytest.mark.parametrize("testcase_name, game, expected_scores", [
("Basic 1", '[ ["8", "/"], ["5", "4"], ["9", "0"], ["X"], ["X"], ["5", "/"], ["5", "3"], ["6", "3"], ["9", "/"], ["9", "/", "X"] ]','[ 15, 24, 33, 58, 78, 93, 101, 110, 129, 149 ]'),
("Basic 2", '[ [6,3], [9,"/"], [9,"/"], [5,4], [8,1], [9,"/"], ["X"], ["X"], [8,"/"], [7,1] ]','[ 9, 28, 43, 52, 61, 81, 109, 129, 146, 154 ]'),
("All Strikes", '[["X"], ["x"], ["X"], ["X"], ["x"], ["X"], ["X"], ["x"], ["X"], ["X", "x", "X"] ]','[ 30, 60, 90, 120, 150, 180, 210, 240, 270, 300 ]'),
("All Spares", '[ ["5", "/"], ["5", "/"], ["5", "/"], ["5", "/"], ["5", "/"], ["5", "/"], ["5", "/"], ["5", "/"], ["5", "/"], ["5", "/", 5] ]','[ 15, 30, 45, 60, 75, 90, 105, 120, 135, 150]'),
("Incomplete Game", '[ ["x"], [5,2] ]','[ 17, 24 ]')
])
def test_names_and_fields(testcase_name, game, expected_scores):
    _, frames_added_up = BowlingScores.ScoreGame(game)
    expected_scores = json.loads(expected_scores)
    assert expected_scores == frames_added_up, testcase_name
