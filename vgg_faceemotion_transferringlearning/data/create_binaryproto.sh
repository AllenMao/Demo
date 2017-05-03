# /usr/bin/env sh
CAFFEPATH=/home/usrname/usr/caffe
DATAPATH=data

echo "create mean.binaryproto..."
rm -rf $ROOT/mean.binaryproto

$CAFFEPATH/build/tools/compute_image_mean \
$DATAPATH/train_lmdb \
$DATAPATH/mean.binaryproto
