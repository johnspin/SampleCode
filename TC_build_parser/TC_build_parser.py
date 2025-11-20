import json
import os
import uuid
from datetime import datetime, timezone
import argparse

start_time = ""
now = ""

parser = argparse.ArgumentParser()
parser.add_argument("exampleOutput", default="TPCi_TeamCity_data_form.json", help="Final format of the output file")
parser.add_argument("translationSpec", default="schemaMapping.json", help="Specifies the translation of environment variables and modified data to output format")
args = parser.parse_args()
print(args.exampleOutput)
print(args.translationSpec)
outputFile = args.exampleOutput
translationSpec = args.translationSpec

exit(1)


def getFieldArguments():
    pass


def main():
    getFieldArguments()
    with open("TPCi_TeamCity_data_form.json", "r") as infile:
        build_data = json.load(infile)

    with open("schemaMapping.json", "r") as map_file:
        mapping = json.load(map_file)

    for key, value in mapping.items():
        result = "ERROR: value never assigned"
        if value[0]:
            result = search_env_variable(value[0])

        if value[1]:
            method = value[1]
            if globals()[method]:
                # result = getattr(main, method)()
                result = globals()[method]()
            else:
                result = "method not found!!!"

        build_data[key] = result

    print(json.dumps(build_data))


def get_guid():
    return str(uuid.uuid4())


def get_start_time():
    global start_time
    start_str = search_env_variable("BUILD_START_DATE") + search_env_variable("BUILD_START_TIME")
    start_str = "20210803" + "194246"
    # start_time = datetime.astimezone(tz=timezone.utc).strptime(start_str, '%Y%m%d%H%M%S')
    start_time = datetime.strptime(start_str, '%Y%m%d%H%M%S')
    start_time.replace(tzinfo=timezone.utc)
    # return start_time.astimezone().replace(microsecond=0).isoformat()
    return start_time.replace(microsecond=0).isoformat()  # .now(tz=timezone.utc)


def get_stop_time():
    global now
    now = datetime.utcnow()
    return now.now(tz=timezone.utc).replace(microsecond=0).isoformat()
    # return now.astimezone().replace(microsecond=0).isoformat()


def get_total_time():
    diff_time = now - start_time
    days, seconds = diff_time.days, diff_time.seconds
    hours = days * 24 + seconds // 3600
    minutes = (seconds % 3600) // 60
    seconds = seconds % 60
    diff_display = f"HH:MM:SS {hours:02}:{minutes:02}:{seconds:02}"
    return diff_display


def search_env_variable(field_name):
    try:
        found_value = os.environ[field_name]
    except KeyError:
        found_value = f"{field_name} not found!!!"

    return found_value


if __name__ == '__main__':
    main()
