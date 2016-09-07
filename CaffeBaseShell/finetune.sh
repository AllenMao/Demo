#!/usr/bin/env sh

TOOLS=./build/tools

$TOOLS/caffe train \
    --solver=/home/zkpk/usr/caffe/learning/finetune_solver.prototxt \
    --weights=/home/zkpk/usr/caffe/learning/status/lr_iter_8000.caffemodel #\
    #>>CIFAR-10/log.txt 2>&1 
