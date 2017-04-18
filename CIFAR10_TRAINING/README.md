### 前言
> 本实验首先训练好自定义的网络“mynet”模型（step 1 to step 5），然后了解网络中间层的参数和输出信息（step 6）。
### 1 Create lmdb

> 运行环境为Caffe根目录，执行create_cifar10.sh 实际上是调用./build/examples/cifar10/convert_cifar_data.bin，生成cifar10_train_lmdb和cifar10_test_lmdb数据文件

### 2 Network architecture

> conv1->relu->pool1->lrn->conv2->relu->pool2->lrn->fc1->fc2

### 3 部分超参数介绍
> net: "mynet.prototxt"

> test_iter: 100  #迭代100次可以覆盖全部10000个测试集

> test_interval: 100  #每迭代100次测试一次

> base_lr: 0.001  #学习率

> momentum: 0.99

> weight_decay: 0.0004

> lr_policy: "fixed"

> snapshot: 5000

> snapshot_prefix: "snapshot/log10k"

> solver_mode: GPU

### 4 Train

> 运行环境为Caffe根目录，执行sole.py, 开始训练网络，迭代10000次，没迭代100次统计测试的acc和训练的loss，最后显示acc-loss曲线。

### 5 Results

* 1: 学习率0.01

![cifar_0.001.png](https://github.com/AllenMao/Demo/blob/master/CIFAR10_TRAINING/results/cifar_0.001.png?raw=true)

* 2: 学习率0.001

![cifar_0.0001.png](https://github.com/AllenMao/Demo/blob/master/CIFAR10_TRAINING/results/cifar_0.0001.png?raw=true)

* 3: 在1的全连接层之间添加Dropout

![cifar_0.001_dropout.png](https://github.com/AllenMao/Demo/blob/master/CIFAR10_TRAINING/results/cifar_0.001_dropout.png?raw=true)

* 4: 在3的基础上添加一层conv3

![cifar_0.001_conv3.png](https://github.com/AllenMao/Demo/blob/master/CIFAR10_TRAINING/results/cifar_0.001_conv3.png?raw=true)

### 6 Data visualization

> 在执行dataVisual.py之前，准备好deploy文件（修改input层，并去掉accuracy、 loss层，具体查看mynet_deploy.prototxt）。训练得到log10k_iter_10000.caffemodel后，输入一张测试图像，显示对应的中间层信息：

* 1: 测试图像

![test_input.png](https://github.com/AllenMao/Demo/blob/master/CIFAR10_TRAINING/results/test_input.png?raw=true)

* 2: conv1 输出特征图

![conv1_16.png](https://github.com/AllenMao/Demo/blob/master/CIFAR10_TRAINING/results/conv1_16.png?raw=true)
