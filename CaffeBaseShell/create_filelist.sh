# /usr/bin/env sh
DATAPATH=learning/datasets/te/cleandata
ROOT=/home/zkpk/usr/caffe/learning

echo "Create train.txt..."
rm -rf $ROOT/train.txt
for i in 00 01 02 03 04 05 06 07 08 09 10 11
do
find $DATAPATH/train -name $i*.jpg | cut -d '/' -f5-7 | sed "s/$/ $i/">>$ROOT/train.txt
done

echo "Create test.txt..."
rm -rf $ROOT/test.txt
for i in 00 01 02 03 04 05 06 07 08 09 10 11
do
find $DATAPATH/test -name $i*.jpg | cut -d '/' -f5-7 | sed "s/$/ $i/">>$ROOT/test.txt
done
echo "All done"

