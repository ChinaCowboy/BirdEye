# import app.visualize_cv2
import sys
from app.visualize_cv3 import process


def main():
    # print command line arguments
    for arg in sys.argv[1:]:
        print (arg)

if __name__ == "__main__":
    inputfile ,outputfile=sys.argv[1:]
    print (inputfile)
    print(outputfile)
    process(inputfile,outputfile)