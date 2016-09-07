#!/usr/bin/env sh
DATAPATH=/home/zkpk/usr/caffe/learning/datasets/te/cleandata
ROOT=/home/zkpk/usr/caffe/learning

echo "Create train lmdb.."
rm -rf $ROOT/img_train_lmdb
build/tools/convert_imageset \
--shuffle \
--resize_height=256 \
--resize_width=256 \
$DATAPATH/ \
$ROOT/train.txt \
$ROOT/img_train_lmdb

echo "Create test lmdb.."
rm -rf $ROOT/img_test_lmdb
build/tools/convert_imageset \
--shuffle \
--resize_width=256 \
--resize_height=256 \
$DATAPATH/ \
$ROOT/test.txt \
$ROOT/img_test_lmdb

echo "All Done.."
