#!/usr/bin/en sh

CAFFEPATH=/home/usrname/usr/caffe
ROOT=/home/usrname/Desktop/learning/VGG_TransferLearning/data
DATA=$ROOT

TOOLS=$CAFFEPATH/build/tools

TRAIN_DATA_ROOT=$DATA/
TEST_DATA_ROOT=$DATA/

rm -rf $ROOT/train_lmdb
rm -rf $ROOT/test_lmdb

RESIZE=true
if $RESIZE; then
  RESIZE_HEIGHT=224
  RESIZE_WIDTH=224
else
  RESIZE_HEIGHT=0
  RESIZE_WIDTH=0
fi

echo "Creating train lmdb.."

GLOG_logtostderr=1 $TOOLS/convert_imageset \
    --resize_height=$RESIZE_HEIGHT \
    --resize_width=$RESIZE_WIDTH \
    --shuffle \
    $TRAIN_DATA_ROOT/ \
    $DATA/train.txt \
    $DATA/train_lmdb
	
echo "Creating test lmdb.."

GLOG_logtostderr=1 $TOOLS/convert_imageset \
    --resize_height=$RESIZE_HEIGHT \
    --resize_width=$RESIZE_WIDTH \
    --shuffle \
    $TEST_DATA_ROOT/ \
    $DATA/test.txt \
    $DATA/test_lmdb

echo "Done"
