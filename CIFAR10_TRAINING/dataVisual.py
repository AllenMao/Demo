import sys,os;

sys.path.append("/home/yourname/usr/caffe/python")

import caffe
import numpy as np
import matplotlib.pyplot as plt

from PIL import Image

caffe_root = '/home/yourname/Desktop/learning/'

if not os.path.isfile(caffe_root + 'CIFAR-10/snapshot/log10k_iter_10000.caffemodel'):
    print("caffemodel is not exist...")

caffe.set_mode_gpu()
net = caffe.Net(caffe_root + 'CIFAR-10/deploy.prototxt',
                caffe_root + 'CIFAR-10/snapshot/log10k_iter_10000.caffemodel',
                caffe.TEST)

print 'data.shape'
print net.blobs['data'].data.shape

im = caffe.io.load_image(caffe_root + 'CIFAR-10/Test_00491.jpg')#test image

#npyMean=caffe_root+'CIFAR-10/mean.npy'

#图片预处理设置
transformer = caffe.io.Transformer({'data': net.blobs['data'].data.shape})  #设定图片的shape格式(1,3,28,28)
transformer.set_transpose('data', (2,0,1))    #改变维度的顺序，由原始图片(28,28,3)变为(3,28,28)
#transformer.set_mean('data', np.load(npyMean)[0].mean(1).mean(1))    #减去均值.
transformer.set_raw_scale('data', 1)    # 缩放到【0，1】之间
transformer.set_channel_swap('data', (2,1,0))   #交换通道，将图片由RGB变为BGR

net.blobs['data'].data[...] = transformer.preprocess('data',im)

inputData=net.blobs['data'].data
#print inputData.shape

#show sub mean image
plt.figure()
plt.subplot(1,2,1),plt.title("origin")
plt.imshow(im)
plt.axis('off')

plt.subplot(1,2,2),
plt.title("input data")
plt.imshow(transformer.deprocess('data', inputData[0]))
plt.axis('off')

print 'data value'
#print inputData[0][0]
#print transformer.deprocess('data', inputData[0])

#run net
net.forward()
#show data info of layer
print [(k, v.data.shape) for k, v in net.blobs.items()]


#show params info of layer
print [(k, v[0].data.shape) for k, v in net.params.items()]


# In[23]:

#show data with image
def show_data(data, padsize=1, padval=0):
    data -= data.min()
    data /= data.max()
    
    # force the number of filters to be square
    n = int(np.ceil(np.sqrt(data.shape[0])))
    padding = ((0, n ** 2 - data.shape[0]), (0, padsize), (0, padsize)) + ((0, 0),) * (data.ndim - 3)
    data = np.pad(data, padding, mode='constant', constant_values=(padval, padval))
    
    # tile the filters into an image
    data = data.reshape((n, n) + data.shape[1:]).transpose((0, 2, 1, 3) + tuple(range(4, data.ndim + 1)))
    data = data.reshape((n * data.shape[1], n * data.shape[3]) + data.shape[4:])
    plt.figure()
    plt.imshow(data,cmap='gray')
    plt.axis('off')
plt.rcParams['figure.figsize'] = (8, 8)
plt.rcParams['image.interpolation'] = 'nearest'
plt.rcParams['image.cmap'] = 'gray'


# In[25]:

show_data(net.blobs['conv1'].data[0, :16]) #show pre-16 feature maps

plt.show()

print 'done'
