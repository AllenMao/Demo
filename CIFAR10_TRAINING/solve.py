import sys,os

sys.path.append("/home/programer/usr/caffe/python")

import caffe

import numpy as np
import matplotlib.pyplot as plt




# init
#caffe.set_device(int(sys.argv[1]))
caffe.set_mode_gpu()

solver = caffe.SGDSolver('solver2.prototxt')

max_iter = 10000
test_iter = 100	#test_size / batch_size
test_interval = 100	#test frequency
display = 50
test_acc = np.zeros(int(np.ceil(max_iter * 1.0 / test_interval)))
train_loss = np.zeros(int(np.ceil(max_iter * 1.0 / display)))
loss = 0; acc = 0
for i in range(max_iter):
    solver.step(1)
    loss += solver.net.blobs['loss'].data	#compute batch_size images in every iter.
    if i % display == 0:	#display
        print 'Iteration', i, '......loss:', loss / display
        train_loss[i / display] = loss / display
        loss = 0
    if i % test_interval == 0:
        acc = 0
        for j in range(test_iter):
            solver.test_nets[0].forward()
            acc += solver.test_nets[0].blobs['accuracy'].data#accuracy
        acc /= test_iter 
        print 'Iteration', i, 'testing... accuracy:', acc
        test_acc[i / test_interval] = acc
_, ax1 = plt.subplots()
ax2 = ax1.twinx()
ax1.plot(display * np.arange(len(train_loss)), train_loss,'b')
ax2.plot(test_interval * np.arange(len(test_acc)), test_acc,'r')
ax1.set_xlabel('iter')
ax1.set_ylabel('loss')
ax2.set_ylabel('acc')
plt.show()
