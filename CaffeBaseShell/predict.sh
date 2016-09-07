# /usr/bin/env sh
build/tools/caffe test \
-model /home/zkpk/usr/caffe/learning/train_val.prototxt \
-weights /home/zkpk/usr/caffe/learning/status/lr_iter_8000.caffemodel \
-iterations 4
