import sys,os

sys.path.append("/home/zkpk/usr/caffe/python")

import caffe

import numpy as np
import matplotlib.pyplot as plt

data_dir = 'data'
#update path---os.getcwd():get current path
root_ = '/voc-fcn8s'
#os.chdir(os.getcwd()+root_)

weights = './models/vgg16_emo.caffemodel'

# init
#caffe.set_device(int(sys.argv[1]))
caffe.set_mode_gpu()

solver = caffe.SGDSolver('./models/solver2.prototxt')
solver.net.copy_from(weights)


# scoring
max_iter = 10000
test_iter = 100
train_acc = np.zeros(int(np.ceil(max_iter / test_iter)))
train_loss = np.zeros(int(np.ceil(max_iter / test_iter)))

for i in range(max_iter):#25*4000
    solver.step(1)

    if i % 100 == 0:
        print 'Iteration', i, '......loss:', solver.net.blobs['loss'].data
    if i % test_iter == 0:
    	solver.test_nets[0].forward(start = 'conv1_1')
        acc = solver.test_nets[0].blobs['accuracy'].data#accuracy
        print 'Iteration', i, 'testing... accuracy:', acc
        train_loss[i / test_iter] = solver.net.blobs['loss'].data
        train_acc[i / test_iter] = acc
_, ax1 = plt.subplots()
ax2 = ax1.twinx()
ax1.plot(np.arange(int(np.ceil(max_iter / test_iter))), train_loss,'b')
ax2.plot(np.arange(int(np.ceil(max_iter / test_iter))), train_acc,'r')
ax1.set_xlabel('iter')
ax1.set_ylabel('loss')
ax2.set_ylabel('acc')
plt.show()
