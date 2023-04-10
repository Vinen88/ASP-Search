
import json


def main ():
    f = open("washingtonElectricCarData.json", "r")
    data = json.load(f)
    new_data = []
    count = 0
    f.close()
    for item in data["data"]:
        new_item = {}
        new_item["id"] = count
        new_item["location"] = str(item[9]) + ", " + str(item[10]) + ", " + str(item[11])
        new_item["zip"] = item[12]
        new_item["year"] = item[13]
        new_item["maker"] = item[14]
        new_item["model"] = item[15]
        new_item["type"] = item[16]
        new_item["eligibility"] = item[17]
        new_data.append(new_item) 
        count += 1
    json_obj = json.dumps(new_data, indent=4)
    with open("washingtonElectricCarData_restructured.json", "w") as f:
        f.write(json_obj)

if __name__ == "__main__":
    main()