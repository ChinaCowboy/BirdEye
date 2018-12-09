# BirdEye
This is impetementation of detect birds via video and image.

# Setup guide

http://www.siafoo.net/article/77

git clone https://github.com/matterport/Mask_RCNN.git

# The web application BirdEye (.src/BirdEyeDetetorWeb/)
The web application is used for call the BirdEye module to detect the image and video . and provide the way for user to indentify the bird species.
1. Bird Eye Detection
    1.1 Input the vedio file and detect the frame which have birds
2. Bird Eye Segmentation
    2.1 segment the bird frame 
    2.2 Select the segmentation of bird 
    2.3 User can indentify the bird with given bird species
    2.4 The segmentation and indentified the frame can be saved and achived
#BirdEyeServer 
..

# Conda Install
conda create -n MaskRCNN python=3.6 pip

activate MaskRCNN

pip install -r .\requirements2.txt (from git hub and you tube)

pip install imgaug

pip install opencv-python


(AttributeError: module 'keras.engine.topology' has no attribute 'load_weights_from_hdf5_group_by_name')

pip install 'keras==2.1.6' --force-reinstall

[pip install --upgrade PyYAML]
@JulienDufour @farzadzare A better solution
You need to clean your build dir first.
Then mrcnn/model.py, replace the "topology" with "saving".
Then run python3 setup.py install

Mac  - tensorflow 


0down vote
* On mac use following command:
    conda install -c conda-forge tensorflow 

This will install the latest Tensorflow on your system. if you wish to upgrade it to newer verion then you can use the following command
    conda update -f -c conda-forge tensorflow

* install pycocotools 




git  clone https://github.com/philferriere/cocoapi.git

pip install git+https://github.com/philferriere/cocoapi.git#subdirectory=PythonAPI
                         

On Linux, run pip install git+https://github.com/waleedka/cocoapi.git#egg=pycocotools&subdirectory=PythonAPI

On Windows, run 
pip install git+https://github.com/philferriere/cocoapi.git#egg=pycocotools^&subdirectory=PythonAPI



https://github.com/matterport/Mask_RCNN/realease

jupyter notebook

mac :

conda install opencv
conda install matplotlib
--------------------------------------------------Video 

https://pixabay.com/en/videos/bird-animal-water-nature-wildlife-6542/

https://github.com/markjay4k/YOLO-series/blob/master/downsample_video.py

https://github.com/markjay4k/Mask-RCNN-series/blob/master/process_video.py

https://github.com/markjay4k/Mask-RCNN-series/blob/master/visualize_cv.py


------------------------windows -performance---------------------------------------
pip3 install --upgrade tensorflow-gpu





